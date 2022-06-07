using Atlas.Data;
using Atlas.Projections;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;
using NetTopologySuite.Geometries;
using DevExpress.Mvvm.POCO;
using System.Windows.Input;
using System.IO.Compression;
using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using WBIS_2.Modules.Tools;
using Microsoft.EntityFrameworkCore;
using DevExpress.Xpf.Editors.Helpers;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class BotanicalScopingReport : WBISViewModelBase
    {
     
        public BotanicalScopingReport(THP_Area tHP_Area)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "DOCX|*.docx";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"Scoping Report {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")} {tHP_Area.THPName}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (File.Exists(sfd.FileName) )
            {
                MessageBox.Show("The selected file already exists.");
                goto HERE;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var app = new Word.Application { Visible = false };
            var doc = app.Documents.Add();

            object styleName = "No Spacing";
            doc.Paragraphs.set_Style(ref styleName);

            BotanicalScoping botanicalScoping = Database.BotanicalScopings
                .Include(_ => _.Watersheds)
                .Include(_ => _.THP_Area)
                .Include(_ => _.Districts)
                .Include(_ => _.Region)
                .First(_ => _.THP_Area == tHP_Area && !_._delete && !_.Repository);


            //Write into
            string region = WriteBotanicalScopingReportIntro(botanicalScoping, doc);

            //Text fields and plants
            WriteScopingPlantsAndFields(doc, botanicalScoping, region);


            WriteScopingFormatting(doc, tHP_Area.THPName);

            doc.SaveAs(sfd.FileName);
            doc.Close(true);
            app.Quit();

            w.Stop();
            System.Windows.MessageBox.Show("The watershed report has finished");
        }

        WordHelper WH = new WordHelper();


        private void WriteScopingPlantsAndFields(Word.Document doc, BotanicalScoping botanicalScoping, string region)
        {
            string[] specificFields = new string[] { "References", "CRPR3_4", "SpecialStatus", "PreFieldScoping" };
            var textFields = Database.ScopingTexts.Where(_ => !specificFields.Contains(_.Field));
            foreach (var text in textFields)
            {
                doc.Paragraphs.Last.Format.TabHangingIndent(-1);               
                WriteTextField(doc, text.Header, text.Text);
            }

            List<string> usedSpecies = new List<string>();


            var plantSpecies = Database.BotanicalScopingSpecies
                .Include(_ => _.PlantSpecies).Include(_ => _.BotanicalScoping)
                .Where(_ => _.BotanicalScoping == botanicalScoping && !_._delete && !_.Repository).ToArray();

            WriteBotanicalScopingCRPR3_4(botanicalScoping, doc, plantSpecies);
            WriteBotanicalScopingSpecialStatus(botanicalScoping, doc, plantSpecies);
            WriteBotanicalScopingPreFieldScoping(botanicalScoping, doc, region, plantSpecies);
            WriteReferencesField(doc);
        }
        private void WriteScopingFormatting(Word.Document doc, string thp)
        {
            int i = 1;
            foreach (Word.Section sec in doc.Sections)
            {
                var r = sec.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                r.Paragraphs.Last.Range.Font.Bold = 1;
                r.Paragraphs.Last.Range.Font.Size = 14;
                r.Paragraphs.Last.Range.Text = "Sierra Pacific Industries";
                r.Paragraphs.Add();
                r.Paragraphs.Last.Range.Font.Bold = 1;
                r.Paragraphs.Last.Range.Font.Size = 11;
                r.Paragraphs.Last.Range.Text = "Botany Scoping Report";
            }

            object missing = System.Reflection.Missing.Value;
            Word.Window activeWindow = doc.Application.ActiveWindow;
            object currentPage = Word.WdFieldType.wdFieldPage;
            object totalPages = Word.WdFieldType.wdFieldNumPages;

            // Go to the Footer view
            activeWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;
            // Right-align the current selection
            activeWindow.ActivePane.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            // Type the page number in 'Page X of Y' format
            activeWindow.Selection.TypeText("Page ");
            activeWindow.Selection.Fields.Add(activeWindow.Selection.Range, ref currentPage, ref missing, ref missing);
            activeWindow.Selection.TypeText(" of ");
            activeWindow.Selection.Fields.Add(activeWindow.Selection.Range, ref totalPages, ref missing, ref missing);

            // Go back to the Main Document view
            activeWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
        }

        private void WriteTextField(Word.Document doc, string field, string txt)
        {
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Format.TabHangingIndent(-1);

            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Text = field + ":";
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Range.Font.Bold = 0;
            doc.Paragraphs.Last.Range.Text = txt;
        }
        private void WriteReferencesField(Word.Document doc)
        {
            ScopingText scopingText = Database.ScopingTexts.First(_ => _.Field == "References");
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Format.TabHangingIndent(-1);
            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Text = scopingText.Header + ":";
            doc.Paragraphs.Add();

            var refs = scopingText.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < refs.Count(); i++)
            {
                doc.Paragraphs.Add();
                if (i == 0) doc.Paragraphs.Last.Format.TabHangingIndent(1);
                else doc.Paragraphs.Add();
                doc.Paragraphs.Last.Range.Font.Bold = 0;
                doc.Paragraphs.Last.Range.Text = refs[i];
            }
        }

        private string WriteBotanicalScopingReportIntro(BotanicalScoping botanicalScoping, Word.Document doc)
        {
            string region = "";
            int runningTab = 0;

            string districts = string.Join(", ", botanicalScoping.Districts.Select(_ => _.DistrictName));

            runningTab = WH.AddIntroDescriptiveTextWithTabs("THP Name:", botanicalScoping.THP_Area.THPName, "SPI Forester:", botanicalScoping.Forester, doc, runningTab, true);
            runningTab = WH.AddIntroDescriptiveTextWithTabs("Date:", botanicalScoping.DateModified.ToShortDateString(), "SPI District:", districts, doc, runningTab);

            runningTab += 1;

            string elevationFt = $"{botanicalScoping.ElevationMin.ToString("N0")}-{botanicalScoping.ElevationMax.ToString("N0")}";
            string elevationM = $"{(botanicalScoping.ElevationMin * 0.3048d).ToString("N0")}-{(botanicalScoping.ElevationMax * 0.3048d).ToString("N0")}";
            string elevationWshdFt = $"{botanicalScoping.WshdElevationMin.ToString("N0")}-{botanicalScoping.WshdElevationMax.ToString("N0")}";
            string elevationWshdM = $"{(botanicalScoping.WshdElevationMin * 0.3048d).ToString("N0")}-{(botanicalScoping.WshdElevationMax * 0.3048d).ToString("N0")}";

            runningTab = WH.AddIntroDescriptiveText("THP Elevation:", $"{elevationFt} ft.   ({elevationM} m.)", doc, runningTab);
            runningTab = WH.AddIntroDescriptiveText("Watershed Elevation:", $"{elevationWshdFt} ft.   ({elevationWshdM} m.)", doc, runningTab);
            runningTab = WH.AddIntroDescriptiveText("Ecological Unit:", botanicalScoping.EcologicalUnit, doc, runningTab);

            return region;
        }

        private void WriteWatershedQuad(Word.Document doc, BotanicalScoping botanicalScoping)
        {
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Format.TabHangingIndent(-1);
            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Text = "Watersheds and USGS 7.5-minute quadrangles:";
            doc.Paragraphs.Add();

            object styleName = "Table Grid";

            var watersheds = botanicalScoping.Watersheds.ToArray();
            var quads = botanicalScoping.Quad75s.ToArray();


            int rowCount = watersheds.Length;
            if (quads.Length > watersheds.Length) rowCount = quads.Length;

            var wdsTbl = doc.Tables.Add(doc.Paragraphs.Last.Range, rowCount + 1, 6);
            // wdsTbl.Columns.DistributeWidth();
            wdsTbl.AllowAutoFit = true;
            wdsTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            wdsTbl.set_Style(ref styleName);
            wdsTbl.Rows[1].Range.Font.Bold = 1;
            wdsTbl.Borders.Enable = 1;

            wdsTbl.Cell(1, 1).Range.Text = "WSHD_ID";
            wdsTbl.Cell(1, 2).Range.Text = "WSHD_NAME";
            wdsTbl.Cell(1, 3).Range.Text = "";
            wdsTbl.Cell(1, 4).Range.Text = "QUADCODE";
            wdsTbl.Cell(1, 5).Range.Text = "USGSCODE";
            wdsTbl.Cell(1, 6).Range.Text = "QUADNAME";
            wdsTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            wdsTbl.Cell(1, 3).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite;

            int rowInt = 2;
            //Watersheds
            foreach (Watershed r in watersheds)
            {
                wdsTbl.Cell(rowInt, 1).Range.Text = r.WatershedID;
                wdsTbl.Cell(rowInt, 2).Range.Text = r.WatershedName;
                rowInt++;
            }
            //USGS 75' Quads
            rowInt = 2;
            foreach (Quad75 r in quads)
            {
                wdsTbl.Cell(rowInt, 4).Range.Text = r.QuadCode;
                wdsTbl.Cell(rowInt, 5).Range.Text = r.UsgsCode; 
                wdsTbl.Cell(rowInt, 6).Range.Text = r.QuadName;
                rowInt++;
            }
        }

      

        private void WriteBotanicalScopingCRPR3_4(BotanicalScoping botanicalScoping, Word.Document doc, BotanicalScopingSpecies[] plantSpecies)
        {
            ScopingText scopingText = Database.ScopingTexts.First(_ => _.Field == "CRPR3_4");
            WH.AddPage(doc);
            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            doc.Paragraphs.Last.Range.Text = "SCOPING RESULTS";

            doc.Paragraphs.Add();
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Text = scopingText.Header + ":";
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Format.TabHangingIndent(-1);
            doc.Paragraphs.Last.Range.Font.Bold = 0;
            doc.Paragraphs.Last.Range.Text = scopingText.Text;
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();

            //Table 1 is rare plant rank 3 or 4 and is not excluded or excluded form the report

            var species = plantSpecies
               .Where(_ => !_.ExcludeReport && !_.Exclude
               && (_.PlantSpecies.RPlantRank.StartsWith("4") || _.PlantSpecies.RPlantRank.StartsWith("3") || (_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList) == ""))
               .OrderBy(_ => _.PlantSpecies.SciName).ToArray();

         

            object styleName = "Table Grid";
            var plantTbl = doc.Tables.Add(doc.Paragraphs.Last.Range, species.Length + 1, 5);
            plantTbl.Columns.DistributeWidth();
            plantTbl.AllowAutoFit = true;
            plantTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            plantTbl.set_Style(ref styleName);
            plantTbl.Borders.Enable = 0;
            plantTbl.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;


            plantTbl.Rows[1].Range.Font.Bold = 1;

            plantTbl.Cell(1, 1).Range.Text = "Species";
            plantTbl.Cell(1, 2).Range.Text = "Habitat Description";
            plantTbl.Cell(1, 3).Range.Text = "CRPR";
            plantTbl.Cell(1, 4).Range.Text = "Federal";
            plantTbl.Cell(1, 5).Range.Text = "State";
            plantTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0; i < species.Length; i++)
            {
                string sciName = species[i].PlantSpecies.SciName;
                WH.WordCellItalicSpecies(plantTbl.Cell(i + 2, 1), sciName);



                plantTbl.Cell(i + 2, 2).Range.Text = species[i].HabitatDescription;
                plantTbl.Cell(i + 2, 3).Range.Text = species[i].PlantSpecies.RPlantRank;
                plantTbl.Cell(i + 2, 4).Range.Text = species[i].PlantSpecies.FedList;
                plantTbl.Cell(i + 2, 5).Range.Text = species[i].PlantSpecies.CalList;
                if (((double)i / 2d).ToString().Contains("."))
                {
                    plantTbl.Rows[i + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                }
            }
        }

        private void WriteBotanicalScopingSpecialStatus(BotanicalScoping botanicalScoping, Word.Document doc, BotanicalScopingSpecies[] plantSpecies)
        {
            ScopingText scopingText = Database.ScopingTexts.First(_ => _.Field == "SpecialStatus");

            WH.AddPage(doc);
            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Text = scopingText.Header + ":";
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Format.TabHangingIndent(-1);
            doc.Paragraphs.Last.Range.Font.Bold = 0;
            doc.Paragraphs.Last.Range.Text = scopingText.Text;
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();

            //Table 2 rare plant is 1 or 2 OR federal/state 1= None and exclude from report = false
            var species = plantSpecies
                .Where(_ => !_.ExcludeReport
                && ((_.PlantSpecies.RPlantRank.StartsWith("1") || _.PlantSpecies.RPlantRank.StartsWith("2")) || (_.PlantSpecies.CalList.ToUpper() != "NONE" || _.PlantSpecies.FedList.ToUpper() != "NONE"))
            && ((_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList) != "" || _.Exclude)).OrderBy(_ => _.PlantSpecies.SciName).ToArray();
                       
            object styleName = "Table Grid";
            var plantTbl = doc.Tables.Add(doc.Paragraphs.Last.Range, species.Length + 1, 7);
            plantTbl.Columns.DistributeWidth();
            plantTbl.AllowAutoFit = true;
            plantTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            plantTbl.set_Style(ref styleName);
            plantTbl.Borders.Enable = 0;
            plantTbl.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;


            plantTbl.Rows[1].Range.Font.Bold = 1;

            plantTbl.Cell(1, 1).Range.Text = "Species";
            plantTbl.Cell(1, 2).Range.Text = "Habitat Description";
            plantTbl.Cell(1, 3).Range.Text = "CRPR";
            plantTbl.Cell(1, 4).Range.Text = "Federal";
            plantTbl.Cell(1, 5).Range.Text = "State";
            plantTbl.Cell(1, 6).Range.Text = "Exclude";
            plantTbl.Cell(1, 7).Range.Text = "Justification";
            plantTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0; i < species.Length; i++)
            {
                string sciName = species[i].PlantSpecies.SciName;
                WH.WordCellItalicSpecies(plantTbl.Cell(i + 2, 1), sciName);



                plantTbl.Cell(i + 2, 2).Range.Text = species[i].HabitatDescription;
                plantTbl.Cell(i + 2, 3).Range.Text = species[i].PlantSpecies.RPlantRank;
                plantTbl.Cell(i + 2, 4).Range.Text = species[i].PlantSpecies.FedList;
                plantTbl.Cell(i + 2, 5).Range.Text = species[i].PlantSpecies.CalList;
                if (species[i].Exclude)
                    plantTbl.Cell(i + 2, 6).Range.Text = "Yes";
                else 
                    plantTbl.Cell(i + 2, 6).Range.Text = "No";
                               
                plantTbl.Cell(i + 2, 7).Range.Text = species[i].ExcludeText;
                if (((double)i / 2d).ToString().Contains("."))
                {
                    plantTbl.Rows[i + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                }
            }
        }

        private void WriteBotanicalScopingPreFieldScoping(BotanicalScoping botanicalScoping, Word.Document doc, string region, BotanicalScopingSpecies[] plantSpecies)
        {
            ScopingText scopingText = Database.ScopingTexts.First(_ => _.Field == "PreFieldScoping");

            WH.AddPage(doc);
            doc.Paragraphs.Last.Range.Font.Bold = 1;
            doc.Paragraphs.Last.Range.Text = scopingText.Header + ":";
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();
            doc.Paragraphs.Last.Format.TabHangingIndent(-1);
            doc.Paragraphs.Last.Range.Font.Bold = 0;
            doc.Paragraphs.Last.Range.Text = scopingText.Text;
            doc.Paragraphs.Add();
            doc.Paragraphs.Add();

            //Table 3 is table 2 where exclude = false
            //      rare plant is 1 or 2 OR federal/state 1= None and exclude from report = false
            var species = plantSpecies
                .Where(_=> !_.Exclude && !_.ExcludeReport
                && ((_.PlantSpecies.RPlantRank.StartsWith("1") || _.PlantSpecies.RPlantRank.StartsWith("2")) || (_.PlantSpecies.CalList.ToUpper() != "NONE" || _.PlantSpecies.FedList.ToUpper() != "NONE")) 
            && ((_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList) != "")).OrderBy(_=>_.PlantSpecies.SciName).ToArray();
          
            object styleName = "Table Grid";
            var plantTbl = doc.Tables.Add(doc.Paragraphs.Last.Range, species.Length + 1, 5);
            plantTbl.Columns.DistributeWidth();
            plantTbl.AllowAutoFit = true;
            plantTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            plantTbl.set_Style(ref styleName);
            plantTbl.Borders.Enable = 0;
            plantTbl.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;


            plantTbl.Rows[1].Range.Font.Bold = 1;

            plantTbl.Cell(1, 1).Range.Text = "Species";
            plantTbl.Cell(1, 2).Range.Text = "CRPR";
            plantTbl.Cell(1, 3).Range.Text = "Federal";
            plantTbl.Cell(1, 4).Range.Text = "State";
            plantTbl.Cell(1, 5).Range.Text = "Plant Protection Measure";
            plantTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0; i < species.Length; i++)
            {
                string sciName = species[i].PlantSpecies.SciName;
                WH.WordCellItalicSpecies(plantTbl.Cell(i + 2, 1), sciName);


                plantTbl.Cell(i + 2, 2).Range.Text = species[i].PlantSpecies.RPlantRank;
                plantTbl.Cell(i + 2, 3).Range.Text = species[i].PlantSpecies.FedList;
                plantTbl.Cell(i + 2, 4).Range.Text = species[i].PlantSpecies.CalList;
                plantTbl.Cell(i + 2, 5).Range.Text = species[i].ProtectionSummary;
                if (((double)i / 2d).ToString().Contains("."))
                {
                    plantTbl.Rows[i + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                }
            }
        }



        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public override void CloseForm()
        {
        }

        public void OnDestroy()
        {
        }
    }


}
