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
using System.Diagnostics;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class BotanicalSurveyReport : WBISViewModelBase
    {
     
        public BotanicalSurveyReport(THP_Area tHP_Area)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "DOCX|*.docx";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"Botany Survey Report {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")} {tHP_Area.THPName}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (File.Exists(sfd.FileName) )
            {
                MessageBox.Show("The selected file already exists.");
                goto HERE;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

         
            BotanicalScoping botanicalScoping = Database.BotanicalScopings
                .Include(_ => _.Watersheds)
                .Include(_ => _.THP_Area)
                .Include(_ => _.Districts)
                .Include(_ => _.Region)
                .First(_ => _.THP_Area == tHP_Area && !_._delete && !_.Repository);

            WriteBotanySurveyReport(sfd.FileName, tHP_Area, botanicalScoping);

            w.Stop();
            if (MessageBox.Show("Would you like to open your report?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
        }

        WordHelper WH = new WordHelper();


        private void WriteBotanySurveyReport(string fileName, THP_Area tHP_Area, BotanicalScoping botanicalScoping)
        {
            var app = new Word.Application { Visible = false };
            var doc = app.Documents.Add();

            object styleName = "No Spacing";
            doc.Paragraphs.set_Style(ref styleName);

            //Write into
            WriteBotanySurveyReportIntro(doc.Sections.Last.Range, botanicalScoping);

            //Write canned text
            WriteCannedTextFields(doc.Sections.Last.Range, false);

            //SPECIES TABLES
            BotanySurveyTable1(doc.Sections.Last.Range, tHP_Area);
            BotanySurveyTable2(doc.Sections.Last.Range, tHP_Area);

            doc.Sections.Add();
            doc.Sections.Last.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;
            //doc.Sections.Last.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            BotanySurveyTable3(doc.Sections.Last.Range, tHP_Area, botanicalScoping);

            //Write canned text
            doc.Sections.Add();
            doc.Sections.Last.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;
            WriteCannedTextFields(doc.Sections.Last.Range, true);


            Microsoft.Office.Interop.Word.WdStatistic PagesCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
            object Miss = System.Reflection.Missing.Value;
            int totalPages = (int)doc.ComputeStatistics(PagesCountStat, ref Miss);

            //Apendacies
            var appTemp = new Word.Application { Visible = false };
            var docTemp = appTemp.Documents.Open(AppDomain.CurrentDomain.BaseDirectory + "\\ReportResources\\Botany Survey Report Appendix Template.docx", false, true);



            object start = 0;
            object textEnd = docTemp.Content.StoryLength;//.end.Range.textEnd;

            docTemp.Range(ref start, ref textEnd).Copy();
            doc.Sections.Add();
            doc.Sections.Last.Range.Paragraphs.Last.Range.Paste();
            docTemp.Close(false);
            appTemp.Quit();

            BotanySurveyTableAppendixB(doc.Sections.Last.Range, tHP_Area);

            //docTemp.SaveAs(fileName + "\\" + thpArea + " Appendix.docx");


            SurveyFormatting_(doc, tHP_Area.THPName, totalPages);
            //WriteSurveyFormatting(doc, thpArea, totalPages);

            doc.SaveAs(fileName);
            doc.Close(true);
            app.Quit();            
        }



        private void SurveyFormatting_(Word.Document doc, string thp, object totalPages)
        {
            object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage;
            object which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToAbsolute;
            object count = 1; //change this number to specify the start of a different page

            object oMissing = System.Reflection.Missing.Value;
            doc.Application.Selection.GoTo(ref what, ref which, ref count, ref oMissing);
            doc.Application.Selection.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakContinuous);




            //Last two sections are the appendix. (word starts count at 1 not 0)
            for (int i = 0; i < doc.Sections.Count; i++)
            {
                doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();
                doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();
                if (i < doc.Sections.Count - 2)
                {
                    WriteSurveyFormattingFooter(doc, thp, totalPages, i + 1);
                    WriteSurveyFormattingHeader(doc, thp, i + 1);
                }
            }
        }

        private void WriteSurveyFormattingFooter(Word.Document doc, string thp, object totalPages, int section)
        {
            object missing = System.Reflection.Missing.Value;
            Word.Window activeWindow = doc.Application.ActiveWindow;
            object currentPage = Word.WdFieldType.wdFieldPage;
            object styleName = "Table Grid";
            var range = doc.Sections[section].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            var footerTbl = range.Tables.Add(range.Paragraphs.Last.Range, 1, 2);
            //introTbl.AllowAutoFit = true;
            footerTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);
            footerTbl.set_Style(ref styleName);
            footerTbl.Borders.Enable = 0;
            footerTbl.Cell(1, 1).Range.Font.Size = 10;
            footerTbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            footerTbl.Cell(1, 1).Range.Text = thp;

            footerTbl.Cell(1, 2).Range.Font.Size = 10;
            footerTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            footerTbl.Cell(1, 2).Range.Select();//.Text = thp;
            activeWindow.Selection.TypeText("Page ");
            activeWindow.Selection.Fields.Add(activeWindow.Selection.Range, ref currentPage, ref missing, ref missing);
            activeWindow.Selection.TypeText(" of " + totalPages);

            footerTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
        }

        private void WriteSurveyFormattingHeader(Word.Document doc, string thpName, int section)
        {
            object styleName = "Table Grid";
            var range = doc.Sections[section].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            range.Paragraphs.Last.Range.ParagraphFormat.TabHangingIndent(-1);
            var HeaderTbl = range.Tables.Add(range.Paragraphs.Last.Range, 2, 2);
            //introTbl.AllowAutoFit = true;
            HeaderTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);
            HeaderTbl.set_Style(ref styleName);
            HeaderTbl.Borders.Enable = 0;

            HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Font.Bold = 1;
            HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Font.Size = 14;
            HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Text = "Sierra Pacific Industries";
            HeaderTbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Font.Bold = 1;
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Font.Size = 11;
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Text = "Botany Survey Report";
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            if (section == 1)
            {
                HeaderTbl.Cell(1, 2).Merge(HeaderTbl.Cell(2, 2));

                HeaderTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                //HeaderTbl.Cell(1, 2).Select();
                var logo = HeaderTbl.Cell(1, 2).Range.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "\\ReportResources\\SpiLogo.png");
            }
            HeaderTbl.LeftPadding = (float)-.02;
        }








        private void WriteSurveyFormatting(Word.Document doc, string thp, object totalPages)
        {
            SurveyFormattingDocument(doc, thp);
            SurveyFormattingFirstPage(doc);

            object missing = System.Reflection.Missing.Value;
            Word.Window activeWindow = doc.Application.ActiveWindow;
            object currentPage = Word.WdFieldType.wdFieldPage;
            
            object styleName = "Table Grid";
            var range = doc.Sections[1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            var footerTbl = range.Tables.Add(range.Paragraphs.Last.Range, 1, 2);
            //introTbl.AllowAutoFit = true;
            footerTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);
            footerTbl.set_Style(ref styleName);
            footerTbl.Borders.Enable = 0;
            footerTbl.Cell(1, 1).Range.Font.Size = 10;
            footerTbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            footerTbl.Cell(1, 1).Range.Text = thp;

            footerTbl.Cell(1, 2).Range.Font.Size = 10;
            footerTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            footerTbl.Cell(1, 2).Range.Select();//.Text = thp;
            activeWindow.Selection.TypeText("Page ");
            activeWindow.Selection.Fields.Add(activeWindow.Selection.Range, ref currentPage, ref missing, ref missing);
            activeWindow.Selection.TypeText(" of " + totalPages);

            footerTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            //Remove headers and footers from apendix
            SurveyFormattingAppendix(doc);
        }

        private void SurveyFormattingDocument(Word.Document doc, string thpName)
        {
            object styleName = "Table Grid";
            var range = doc.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            range.Paragraphs.Last.Range.ParagraphFormat.TabHangingIndent(-1);
            var HeaderTbl = range.Tables.Add(range.Paragraphs.Last.Range, 2, 2);
            //introTbl.AllowAutoFit = true;
            HeaderTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);
            HeaderTbl.set_Style(ref styleName);
            HeaderTbl.Borders.Enable = 0;

            HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Font.Bold = 1;
            HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Font.Size = 14;
            HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Text = "Sierra Pacific Industries";
            HeaderTbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Font.Bold = 1;
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Font.Size = 11;
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Text = "Botany Survey Report";
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            HeaderTbl.Cell(1, 2).Merge(HeaderTbl.Cell(2, 2));

            HeaderTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            //HeaderTbl.Cell(1, 2).Select();
            var logo = HeaderTbl.Cell(1, 2).Range.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "\\ReportResources\\SpiLogo.png");

            HeaderTbl.LeftPadding = (float)-.02;
        }
        private void SurveyFormattingFirstPage(Word.Document doc)
        {
        }
        private void SurveyFormattingAppendix(Word.Document doc)
        {
            object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage;
            object which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToAbsolute;
            object count = 1; //change this number to specify the start of a different page

            object oMissing = System.Reflection.Missing.Value;
            doc.Application.Selection.GoTo(ref what, ref which, ref count, ref oMissing);
            doc.Application.Selection.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakContinuous);


            //Last two sections are the appendix. (word starts count at 1 not 0)
            for (int i = 0; i < doc.Sections.Count; i++)
            {
                if (i >= doc.Sections.Count - 2)
                {
                    doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                    doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();
                    doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                    doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();
                }
                else if (i > 0)
                {
                    doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                    doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();



                    object styleName = "Table Grid";
                    var range = doc.Sections[i + 1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    range.Paragraphs.Last.Range.ParagraphFormat.TabHangingIndent(-1);
                    var HeaderTbl = range.Tables.Add(range.Paragraphs.Last.Range, 2, 2);
                    //introTbl.AllowAutoFit = true;
                    HeaderTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);
                    HeaderTbl.set_Style(ref styleName);
                    HeaderTbl.Borders.Enable = 0;

                    HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Font.Bold = 1;
                    HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Font.Size = 14;
                    HeaderTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Text = "Sierra Pacific Industries";
                    HeaderTbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Font.Bold = 1;
                    HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Font.Size = 11;
                    HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Text = "Botany Survey Report";
                    HeaderTbl.Cell(2, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    HeaderTbl.LeftPadding = (float)-.02;

                }
            }
        }


        private void WriteBotanySurveyReportIntro(Word.Range sectionRange, BotanicalScoping botanicalScoping)
        {
            object styleName = "Table Grid";
            //sectionRange.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            sectionRange.Paragraphs.Last.Range.ParagraphFormat.TabHangingIndent(-1);
            var introTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, 3, 2);
            introTbl.AllowAutoFit = true;
            introTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            introTbl.set_Style(ref styleName);
            introTbl.Borders.Enable = 0;
            //introTbl.Range.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            string districts = string.Join(", ", botanicalScoping.Districts.Select(_ => _.DistrictName));

            WH.FillCell(introTbl.Cell(2, 2), "SPI Forester:", botanicalScoping.Forester, sectionRange);
            WH.FillCell(introTbl.Cell(3, 1), "Date:", botanicalScoping.DateModified.ToShortDateString(), sectionRange);
            WH.FillCell(introTbl.Cell(3, 2), "SPI District:", districts, sectionRange);
            WH.FillCell(introTbl.Cell(2, 1), "THP Name:", botanicalScoping.THP_Area.THPName, sectionRange);
            introTbl.LeftPadding = (float)-.02;
        }


        //Write the canned text fields for the reprt.
        //post tablest is for any text that comes after the species tables
        private void WriteCannedTextFields(Word.Range sectionRange, bool postTables)
        {
            if (postTables)
            {
                WH.WriteReferencesField(sectionRange, "California Department of Fish and Wildlife (CDFW).  2018.  Protocols for Surveying and Evaluating Impacts to Special Status Native Plant Populations and Sensitive Natural Communities." +
                    "  State of California, California Natural Resources Agency, Department of Fish and Wildlife.  March 20, 2018.  12 pp.");
            }
            else
            {
                WH.WriteTextField(sectionRange, "Summary", "<This will be the “Executive Summary,” add relevant summary information….. " +
                    "Surveys and habitat evaluations for all scoped species were completed on XXXXXX, 2020. XXX special-status species were observed. At the end add: " +
                    "Maps showing the survey routes, results, and other relevant information are included as Appendix A.  A list of all plant species observed during the surveys is included as Appendix B.>");
                WH.WriteTextField(sectionRange, "General Description", "<copy from Scoping Report; add relevant information obtained from survey efforts if needed>");
                WH.WriteTextField(sectionRange, "THP Elevation", "<copy from Scoping Report>");
                WH.WriteTextField(sectionRange, "Watershed Elevation", "<copy from Scoping Report>");
                WH.WriteTextField(sectionRange, "Ecological Unit", "<copy from Scoping Report>");
                WH.WriteTextField(sectionRange, "Geology & Soils", "<copy from Scoping Report>");
                WH.WriteTextField(sectionRange, "Methods", "<SPI conducted botanical surveys for the XXXXX THP following the guidelines described in CDFW (2018)." +
                    "  We performed intuitive-controlled pedestrian surveys of potential habitat for all scoped species within the THP Area." +
                    "  Surveys were conducted during appropriate flowering periods for the scoped species or when the species were otherwise identifiable." +
                    "  SPI collected a list of all plant taxa observed during the surveys, and all plant taxa were identified to the taxonomic level necessary to determine whether or not they are a special-status plant." +
                    "  No climatic, timing, or other issues occurred that may have affected the comprehensiveness of our surveys results (or modify this and add other relevant info. as necessary…..).>" +
                    "\n\n" +
                    "<Add a paragraph here describing any nuances (e.g., early season surveys planned for next year, all units except xxx, etc., as appropriate…>" +
                    "\n\n" +
                    "<This report summarizes the botanical survey results.  SPI will prepare and submit all special-status plant observations made during these surveys to the CNDDB.>");
                WH.WriteTextField(sectionRange, "Results", "<SPI prepared a Botanical Scoping Report for the xxxxx THP, dated xxxxx, which identified xxxxx known or potentially occurring special-status taxa in the THP area.  " +
                    "We conducted intuitive controlled surveys for these species on (add dates…).  The surveys were conducted by SPI Botanists Ms. XXXX (add degree, add school; add yrs. of experience) and " +
                    "Mr. XXXX (add degree, add school; add yrs. of experience).  If we had help, include these additional sentences = The SPI Botanists were assisted by Mr. XXXX (add degree, add school; " +
                    "add yrs. of experience) and Ms. XXXX (add degree, add school; add yrs. of experience).  All survey work was overseen by SPI Botanists.  " +
                    "SPI spent xxxxx person-hours of survey time during the field efforts.>" +
                    "" +
                    "\n\n" +
                    "<xxxx special-status species were observed during the botanical surveys.  Summaries of the overall survey results and THP components are shown in Tables 1 and 2.  " +
                    "Detailed survey results are shown in Table 3 and summarized by the plant taxa included in Table 3 of the Botanical Scoping Report and other special-status species observations.  " +
                    "Maps showing the survey routes, results, and other relevant information are included as Appendix A.  A list of all plant species observed during the surveys is included as Appendix B.>");
            }
        }



       

        private void BotanySurveyTable1(Word.Range sectionRange, THP_Area tHP_Area)
        {
            AddPageSection(sectionRange);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Table 1. Summary of Botanical Survey Results for the " + tHP_Area.THPName + " THP:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //Table 1 is all found species within a THP

            var plantSpecies = Database.BotanicalPlantsOfInterest
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                .Where(_ => _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid && _.SpeciesFound && !_.BotanicalElement._delete && !_.BotanicalElement.Repository)
                .Select(_=>_.PlantSpecies).Distinct().OrderBy(_=>_.SciName);

            DataTable dt = new DataTable();
            dt.Columns.Add("Species");
            dt.Columns.Add("CRPR");
            dt.Columns.Add("Federal");
            dt.Columns.Add("State");
            dt.Columns.Add("Location");
            dt.Columns.Add("Summary");

            foreach(var species in plantSpecies)
            {
                DataRow r = dt.NewRow();
                r["Species"] = species.SciName;
                r["CRPR"] = species.RPlantRank;
                r["Federal"] = species.FedList;
                r["State"] = species.CalList;
                r["Location"] = "<add locations>";
                r["Summary"] = "<add numbers, occurrences, etc., as appropriate>";
                dt.Rows.Add(r);
            }

            object styleName = "Table Grid";
            var plantTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, dt.Rows.Count + 1, 4);
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
            plantTbl.Cell(1, 2).Range.Text = "CRPR/Federal/State";
            plantTbl.Cell(1, 3).Range.Text = "Location";
            plantTbl.Cell(1, 4).Range.Text = "Summary";
            plantTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string species = dt.Rows[i][0].ToString();
                WH.WordCellItalicSpecies(plantTbl.Cell(i + 2, 1), species);

                plantTbl.Cell(i + 2, 2).Range.Text = dt.Rows[i][1].ToString() + "/" + dt.Rows[i][2].ToString() + "/" + dt.Rows[i][3].ToString();
                plantTbl.Cell(i + 2, 3).Range.Text = dt.Rows[i][4].ToString();
                plantTbl.Cell(i + 2, 4).Range.Text = dt.Rows[i][5].ToString();
                if (((double)i / 2d).ToString().Contains("."))
                {
                    plantTbl.Rows[i + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                }
            }
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
        }

        private void BotanySurveyTable2(Word.Range sectionRange, THP_Area tHP_Area)
        {
            AddPageSection(sectionRange);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Table 2. Summary of Botanical Survey Results for the " + tHP_Area.THPName + " THP by Plan Component:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //Table 1 is all found species within a THP, split by area.
            DataTable dt = new DataTable();
            dt.Columns.Add("Component");
            dt.Columns.Add("CRPR");
            dt.Columns.Add("Federal");
            dt.Columns.Add("State");
            dt.Columns.Add("Species");
            dt.Columns.Add("Summary");

            var areas = Database.BotanicalSurveyAreas
                .Include(_ => _.THP_Area).Where(_ => _.THP_Area.Guid == tHP_Area.Guid).OrderBy(_=>_.AreaName).ToArray();
            foreach(var area in areas)
            {
                var plantSpecies = Database.BotanicalPlantsOfInterest
               .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea)
               .Where(_ => _.BotanicalElement.BotanicalSurveyArea == area && _.SpeciesFound && !_.BotanicalElement._delete && !_.BotanicalElement.Repository)
               .Select(_ => _.PlantSpecies).Distinct().OrderBy(_ => _.SciName);

                foreach(var species in plantSpecies)
                {
                    DataRow r = dt.NewRow();
                    r["Component"] = area.AreaName;
                    r["CRPR"] = species.RPlantRank;
                    r["Federal"] = species.FedList;
                    r["State"] = species.CalList;
                    r["Species"] = species.SciName;
                    r["Summary"] = "<add numbers, occurrences, etc., as appropriate>";
                    dt.Rows.Add(r);
                }
            }

            object styleName = "Table Grid";
            var plantTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, dt.Rows.Count + 1, 4);
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

            plantTbl.Cell(1, 1).Range.Text = "Component";
            plantTbl.Cell(1, 2).Range.Text = "CRPR/Federal/State";
            plantTbl.Cell(1, 3).Range.Text = "Species";
            plantTbl.Cell(1, 4).Range.Text = "Summary";
            plantTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string species = dt.Rows[i][4].ToString();
                WH.WordCellItalicSpecies(plantTbl.Cell(i + 2, 3), species);

                plantTbl.Cell(i + 2, 1).Range.Text = dt.Rows[i][0].ToString();
                plantTbl.Cell(i + 2, 2).Range.Text = dt.Rows[i][1].ToString() + "/" + dt.Rows[i][2].ToString() + "/" + dt.Rows[i][3].ToString();
                plantTbl.Cell(i + 2, 4).Range.Text = dt.Rows[i][5].ToString();
                if (((double)i / 2d).ToString().Contains("."))
                {
                    plantTbl.Rows[i + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                }
            }
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
        }


        private void BotanySurveyTable3(Word.Range sectionRange, THP_Area tHP_Area, BotanicalScoping botanicalScoping)
        {
            //AddPage(doc);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Table 3. Summary of Botanical Survey Results for the " + tHP_Area.THPName + " THP by Species:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //Table 1 and 3 from botanical scoping and if the species was found.
            DataTable dt = new DataTable();
            dt.Columns.Add("Species");
            dt.Columns.Add("Habitat Description");
            dt.Columns.Add("CRPR");
            dt.Columns.Add("Federal");
            dt.Columns.Add("State");
            dt.Columns.Add("Species Found");
            dt.Columns.Add("Habitat and Survey Results");
            dt.Columns.Add("References");
            dt.Columns.Add("Reference sites");





         

            var surveySpecies = Database.BotanicalPlantsOfInterest
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                .Include(_ => _.PlantSpecies)
                .Where(_ => _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid && !_.BotanicalElement._delete && !_.BotanicalElement.Repository)
                .Select(_ => _.PlantSpecies).Select(_=>_.Guid).ToArray();


            var scopingSpeces = Database.BotanicalScopingSpecies
                .Include(_ => _.BotanicalScoping)
                .Include(_ => _.PlantSpecies)
                .Where(_ => _.BotanicalScoping.Guid == botanicalScoping.Guid && !_._delete && !_.Repository &&
                (
                    (
                        (_.PlantSpecies.RPlantRank.StartsWith("4") || _.PlantSpecies.RPlantRank.StartsWith("3") || (_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList != ""))
                        && !_.Exclude && !_.ExcludeReport
                        && (surveySpecies.Contains(_.PlantSpecies.Guid))
                    )
                    ||
                    (((_.PlantSpecies.RPlantRank.StartsWith("1") || _.PlantSpecies.RPlantRank.StartsWith("2"))
                        || (_.PlantSpecies.CalList.ToUpper() != "NONE" || _.PlantSpecies.FedList.ToUpper() != "NONE"))
                        && (_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList != "")
                        && !_.Exclude && !_.ExcludeReport)
                ));


            //    && !_.ExcludeReport && !_.Exclude
            //  && (_.PlantSpecies.RPlantRank.StartsWith("4") || _.PlantSpecies.RPlantRank.StartsWith("3") || (_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList) == "")
            //  && ((surveySpecies.Contains(_.PlantSpecies.Guid))
            //  ||
            //  (((_.PlantSpecies.RPlantRank.StartsWith("1") || _.PlantSpecies.RPlantRank.StartsWith("2")) || (_.PlantSpecies.CalList.ToUpper() != "NONE" || _.PlantSpecies.FedList.ToUpper() != "NONE"))
            //&& ((_.PlantSpecies.RPlantRank + _.PlantSpecies.CalList + _.PlantSpecies.FedList) != ""))));
            //  .OrderBy(_ => _.PlantSpecies.SciName).ToArray();

            foreach (var species in scopingSpeces)
            {
                DataRow r = dt.NewRow();
                r["Species"] = species.PlantSpecies.SciName;
                r["Habitat Description"] = species.HabitatDescription;
                r["CRPR"] = species.PlantSpecies.RPlantRank;
                r["Federal"] = species.PlantSpecies.FedList;
                r["State"] = species.PlantSpecies.CalList;
                r["Species Found"] = surveySpecies.Contains(species.PlantSpecies.Guid);
                r["Habitat and Survey Results"] = "";
                r["References"] = "";
                r["Reference sites"] = "";

                dt.Rows.Add(r);
            }


            object styleName = "Table Grid";
            var plantTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, dt.Rows.Count * 7, 2);
            plantTbl.Columns.DistributeWidth();
            plantTbl.AllowAutoFit = true;
            plantTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            plantTbl.set_Style(ref styleName);
            plantTbl.Borders.Enable = 0;
            plantTbl.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            int rowCounter = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string species = dt.Rows[i][0].ToString();
                Table3RowValues(plantTbl.Rows[rowCounter], "Species", species, true);
                plantTbl.Cell(rowCounter, 1).Range.Text = "Species";
                plantTbl.Cell(rowCounter, 1).Range.Font.Bold = 1;
                WH.WordCellItalicSpecies(plantTbl.Cell(rowCounter, 2), species);
                plantTbl.Cell(rowCounter, 2).Range.Font.Bold = 0;
                plantTbl.Rows[rowCounter].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleDouble;
                plantTbl.Rows[rowCounter].Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleDouble;
                plantTbl.Rows[rowCounter].Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                plantTbl.Rows[rowCounter].Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                rowCounter++;

                Table3RowValues(plantTbl.Rows[rowCounter], "Habitat Description", dt.Rows[i][1].ToString(), false);
                rowCounter++;

                Table3RowValues(plantTbl.Rows[rowCounter], "CRPR/Federal/State", dt.Rows[i][2].ToString() + "/" + dt.Rows[i][3].ToString() + "/" + dt.Rows[i][4].ToString(), false);
                rowCounter++;

                if ((string)dt.Rows[i][5] != "True") Table3RowValues(plantTbl.Rows[rowCounter], "Species Found", "No", false);
                else Table3RowValues(plantTbl.Rows[rowCounter], "Species Found", "Yes", false);
                rowCounter++;

                Table3RowValues(plantTbl.Rows[rowCounter], "Habitat and Survey Results", dt.Rows[i][6].ToString(), false);
                plantTbl.Cell(rowCounter, 1).Range.Text = "Habitat and Survey Results";
                rowCounter++;

                Table3RowValues(plantTbl.Rows[rowCounter], "References", dt.Rows[i][7].ToString(), false);
                rowCounter++;

                Table3RowValues(plantTbl.Rows[rowCounter], "Reference sites\xB9", dt.Rows[i][8].ToString(), false);
                rowCounter++;
            }

            sectionRange.Paragraphs.Last.Range.Font.Size = 9;
            sectionRange.Paragraphs.Last.Range.Text = "\xB9 If used, “N/A” indicates one or more of the following apply regarding use of reference sites; (1) no reference site was visited, " +
                "(2) reference site inaccessible or unavailable, (3) adequate reference sites relative to the THP area do not occur, " +
                "or (4) visiting a reference site for this species is not particularly useful or necessary.";


            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
        }

        private void Table3RowValues(Word.Row row, string header, string value, bool species)
        {
            row.Cells[1].Range.Text = header;
            row.Cells[1].Range.Font.Bold = 1;

            if (species) WH.WordCellItalicSpecies(row.Cells[2], value);
            else row.Cells[2].Range.Text = value;
            row.Cells[2].Range.Font.Bold = 0;

            if (species)
            {
                row.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleDouble;
                row.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleDouble;
                row.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                row.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                row.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            }
            else
            {
                row.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                if (row.Borders[Word.WdBorderType.wdBorderTop].LineStyle != Word.WdLineStyle.wdLineStyleDouble) row.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                row.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                row.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                row.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite;
            }
        }

        private void AddAppendixCoverPages(Word.Range sectionRange)
        {
            //AddPageSection(sectionRange);
            for (int i = 0; i < 17; i++)
            {
                sectionRange.Paragraphs.Add();
            }

            sectionRange.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 28;
            sectionRange.Paragraphs.Last.Range.Text = "Appendix A";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;

            object range = sectionRange.Paragraphs.Last.Range;
            var line = sectionRange.Paragraphs.Last.Range.InlineShapes.AddHorizontalLineStandard(ref range);
            line.Height = 3;
            line.Fill.Solid();
            line.Fill.ForeColor.RGB = 0;
            line.HorizontalLineFormat.PercentWidth = 100;
            line.HorizontalLineFormat.Alignment = Word.WdHorizontalLineAlignment.wdHorizontalLineAlignCenter;
            line.HorizontalLineFormat.NoShade = true;


            // doc.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Font.Italic = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Botanical Survey Maps";




            AddPageSection(sectionRange);
            for (int i = 0; i < 17; i++)
            {
                sectionRange.Paragraphs.Add();
            }
            sectionRange.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Name = "Cambria (Headings)";
            sectionRange.Paragraphs.Last.Range.Font.Size = 28;
            sectionRange.Paragraphs.Last.Range.Text = "Appendix B";

            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Name = "Calibri (Body)";
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
            sectionRange.Paragraphs.Last.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; //= .Last.Range.Borders.bo = Word.WdLineStyle.
            //sectionRange.Paragraphs.Last.Borders.Enable = -3;


            //doc.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Font.Italic = 1;
            sectionRange.Paragraphs.Last.Range.Text = "List of Plant Species Observed";





            AddPageSection(sectionRange);
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Italic = 0;
            sectionRange.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
        }



        private void AddPageSection(Word.Range sectionRange)
        {
            sectionRange.Paragraphs.Add();
            Word.Paragraph Line20;
            Line20 = sectionRange.Paragraphs.Add();
            Line20.Range.Text = string.Empty;
            object oCollapseEnd = Word.WdCollapseDirection.wdCollapseEnd;
            object oPageBreak = Word.WdBreakType.wdPageBreak;
            Line20.Range.Collapse(ref oCollapseEnd);
            Line20.Range.InsertBreak(ref oPageBreak);
            Line20.Range.Collapse(ref oCollapseEnd);
            // Line20.Range.InsertParagraphAfter();
        }


        private void BotanySurveyTableAppendixB(Word.Range sectionRange, THP_Area tHP_Area)
        {
            AddPageSection(sectionRange);
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Italic = 0;
            sectionRange.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Appendix B.  Plant species observed during botanical surveys for the " + tHP_Area.THPName + " THP (<add dates……>). ";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            DataTable dt = new DataTable();
            dt.Columns.Add("SciName");
            dt.Columns.Add("ComName");
            dt.Columns.Add("Family");

            var plantSpecies = Database.BotanicalPlantsOfInterest
              .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
              .Include(_=>_.PlantSpecies)
              .Where(_ => _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid && _.SpeciesFound && !_.BotanicalElement._delete && !_.BotanicalElement.Repository)
              .Select(_ => _.PlantSpecies);
            var plantSpecies2 = Database.BotanicalPlantsList
              .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
              .Include(_ => _.PlantSpecies)
              .Where(_ => _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid && !_.BotanicalElement._delete && !_.BotanicalElement.Repository)
              .Select(_ => _.PlantSpecies);
            plantSpecies = plantSpecies.Append(plantSpecies2).AsQueryable();
            plantSpecies = plantSpecies.Distinct().OrderBy(_ => _.SciName);

            foreach(var species in plantSpecies)
            {
                DataRow r = dt.NewRow();
                r["SciName"] = species.SciName;
                r["ComName"] = species.ComName;
                r["Family"] = species.Family;
                dt.Rows.Add(r);
            }

            object styleName = "Table Grid";
            var plantTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, dt.Rows.Count + 1, 3);
            plantTbl.Columns.DistributeWidth();
            plantTbl.AllowAutoFit = true;
            plantTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            plantTbl.set_Style(ref styleName);
            plantTbl.Borders.Enable = 0;

            plantTbl.Rows[1].Range.Font.Bold = 1;

            plantTbl.Cell(1, 1).Range.Text = "Scientific Name";
            plantTbl.Cell(1, 2).Range.Text = "Common Name";
            plantTbl.Cell(1, 3).Range.Text = "Family";
            plantTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string species = dt.Rows[i][0].ToString();
                WH.WordCellItalicSpecies(plantTbl.Cell(i + 2, 1), species);

                plantTbl.Cell(i + 2, 2).Range.Text = dt.Rows[i][1].ToString();
                plantTbl.Cell(i + 2, 3).Range.Text = dt.Rows[i][2].ToString();
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
