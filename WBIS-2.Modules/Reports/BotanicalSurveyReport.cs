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
            System.Windows.MessageBox.Show("The watershed report has finished");
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

            FillCell(introTbl.Cell(2, 2), "SPI Forester:", botanicalScoping.Forester, sectionRange);
                    FillCell(introTbl.Cell(3, 1), "Date:", botanicalScoping.DateModified.ToShortDateString(), sectionRange);
                    FillCell(introTbl.Cell(3, 2), "SPI District:", districts, sectionRange);
                    FillCell(introTbl.Cell(2, 1), "THP Name:", botanicalScoping.THP_Area.THPName, sectionRange);
            introTbl.LeftPadding = (float)-.02;
        }


        //Write the canned text fields for the reprt.
        //post tablest is for any text that comes after the species tables
        private void WriteCannedTextFields(Word.Range sectionRange, bool postTables)
        {
            if (postTables)
            {
                WriteReferencesField(sectionRange, "California Department of Fish and Wildlife (CDFW).  2018.  Protocols for Surveying and Evaluating Impacts to Special Status Native Plant Populations and Sensitive Natural Communities." +
                    "  State of California, California Natural Resources Agency, Department of Fish and Wildlife.  March 20, 2018.  12 pp.");
            }
            else
            {
                WriteTextField(sectionRange, "Summary", "<This will be the “Executive Summary,” add relevant summary information….. " +
                    "Surveys and habitat evaluations for all scoped species were completed on XXXXXX, 2020. XXX special-status species were observed. At the end add: " +
                    "Maps showing the survey routes, results, and other relevant information are included as Appendix A.  A list of all plant species observed during the surveys is included as Appendix B.>");
                WriteTextField(sectionRange, "General Description", "<copy from Scoping Report; add relevant information obtained from survey efforts if needed>");
                WriteTextField(sectionRange, "THP Elevation", "<copy from Scoping Report>");
                WriteTextField(sectionRange, "Watershed Elevation", "<copy from Scoping Report>");
                WriteTextField(sectionRange, "Ecological Unit", "<copy from Scoping Report>");
                WriteTextField(sectionRange, "Geology & Soils", "<copy from Scoping Report>");
                WriteTextField(sectionRange, "Methods", "<SPI conducted botanical surveys for the XXXXX THP following the guidelines described in CDFW (2018)." +
                    "  We performed intuitive-controlled pedestrian surveys of potential habitat for all scoped species within the THP Area." +
                    "  Surveys were conducted during appropriate flowering periods for the scoped species or when the species were otherwise identifiable." +
                    "  SPI collected a list of all plant taxa observed during the surveys, and all plant taxa were identified to the taxonomic level necessary to determine whether or not they are a special-status plant." +
                    "  No climatic, timing, or other issues occurred that may have affected the comprehensiveness of our surveys results (or modify this and add other relevant info. as necessary…..).>" +
                    "\n\n" +
                    "<Add a paragraph here describing any nuances (e.g., early season surveys planned for next year, all units except xxx, etc., as appropriate…>" +
                    "\n\n" +
                    "<This report summarizes the botanical survey results.  SPI will prepare and submit all special-status plant observations made during these surveys to the CNDDB.>");
                WriteTextField(sectionRange, "Results", "<SPI prepared a Botanical Scoping Report for the xxxxx THP, dated xxxxx, which identified xxxxx known or potentially occurring special-status taxa in the THP area.  " +
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



        private void FillCell(Word.Cell c, string field, string txt, Word.Range sectionRange)
        {
            sectionRange.Paragraphs.First.Range.Text = field + "  " + txt;
            object start = 0;
            object filedEnd = field.Length + 2;
            object textEnd = field.Length + 2 + txt.Length;

            sectionRange.Document.Range(ref start, ref filedEnd).Bold = 1;
            sectionRange.Document.Range(ref filedEnd, ref textEnd).Bold = 0;
            sectionRange.Document.Range(ref start, ref textEnd).Cut();
            c.Range.Paste();
        }
        private void WriteTextField(Word.Range sectionRange, string field, string txt)
        {
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Format.TabHangingIndent(-1);

            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = field + ":";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Text = txt;
        }
        private void WriteReferencesField(Word.Range sectionRange, string txt)
        {
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Format.TabHangingIndent(-1);
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "References:";

            var refs = txt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < refs.Count(); i++)
            {
                sectionRange.Paragraphs.Add();
                if (i == 0) sectionRange.Paragraphs.Last.Format.TabHangingIndent(1);
                else sectionRange.Paragraphs.Add();
                sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
                sectionRange.Paragraphs.Last.Range.Text = refs[i];
            }
        }






        private void BotanySurveyTable1(Word.Range sectionRange, THP_Area tHP_Area)
        {
            AddPageSection(sectionRange);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Table 1. Summary of Botanical Survey Results for the " + thpArea + " THP:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //Table 1 is all found species within a THP

            var species = Database.BotanicalSurveyAreas
                .Include(_=>_.THP_Area)
                .Include(_=>_.BotanicalElement).ThenInclude(_=>_.BotanicalPlantOfInterest).ThenInclude(_=>_.PlantSpecies)
                .Where(_=>_.THP_Area == tHP_Area)
                .Select(_=>_.BotanicalElement)
                .Where(_=>_.)
                .Include(_=>_.BotanicalPlantsOfInterest).ThenInclude(_=>_.BotanicalElement).ThenInclude(_=>_.BotanicalSurveyArea).ThenInclude(_=>_.THP_Area)
                .Where(_=>_.BotanicalPlantsOfInterest.ele)

            DataTable dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [SciName] AS 'Species',[RPlantRank] AS 'CRPR',[FedList] AS 'Federal',[CalList] AS 'State'," +
                "'<add locations>' AS 'Location','<add numbers, occurrences, etc., as appropriate>' AS 'Summary' FROM [PlantSpecies] WHERE [SciName] IN " +
                "(SELECT [SciName] FROM [Plants of Interest] WHERE [ID] IN (SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN " +
                "(SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "')) AND [Species Found] = TRUE) GROUP BY [SciName] ORDER BY [SciName] ASC", SqlTools.SqlConn))
            { filler.Fill(dt); }


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
            sectionRange.Paragraphs.Last.Range.Text = "Table 2. Summary of Botanical Survey Results for the " + thpArea + " THP by Plan Component:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //Table 1 is all found species within a THP, split by area.

            DataTable areaDt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT DISTINCT[Area Name] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "' ORDER BY [Area Name] ASC", SqlTools.SqlConn))
            { filler.Fill(areaDt); }


            DataTable dt = new DataTable();
            foreach (DataRow area in areaDt.Rows)
            {
                using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT '" + area[0] + "' AS 'Component',[RPlantRank] AS 'CRPR',[FedList] AS 'Federal',[CalList] AS 'State',[SciName] AS 'Species'" +
                    ",'<add numbers, occurrences, etc., as appropriate>' AS 'Summary' FROM [PlantSpecies] WHERE [SciName] IN " +
                    "(SELECT [SciName] FROM [Plants of Interest] WHERE [ID] IN (SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN " +
                    "(SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "' AND [Area Name] = '" + area[0] + "')) AND [Species Found] = TRUE) GROUP BY [SciName] ORDER BY [SciName] ASC", SqlTools.SqlConn))
                { filler.Fill(dt); }
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
            sectionRange.Paragraphs.Last.Range.Text = "Table 3. Summary of Botanical Survey Results for the " + thpArea + " THP by Species:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //Table 1 and 3 from botanical scoping and if the species was found.
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [Species],[HabDesc] AS 'Habitat Description',[RPlantRank] AS 'CRPR',[FedList] AS 'Federal',[CalList] AS 'State'," +
                "(SELECT COUNT(*) > 0 FROM [Survey Elements] WHERE [Botanical Species].[Species] = [Survey Elements].[Record Type] " +
                        "AND [ID] IN (SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN (SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "'))) AS 'Species Found'," +
                        "'' AS 'Habitat and Survey Results', '' AS 'References', '' AS 'Reference sites' " +
                "FROM [Botanical Species]" +
                " WHERE [Source ID] = '" + scopingId + "' AND (" +
            //Scoping table 1
                "(([RPlantRank] LIKE '4%' OR [RPlantRank] LIKE '3%' OR [RPlantRank]||[FedList]||[CalList] = '') AND [Exclude] = FALSE AND [ExcludeReport] = FALSE AND" +
                //Species found
                "(UPPER([Species]) IN (SELECT UPPER([Record Type]) FROM [Survey Elements] WHERE [Botanical Species].[Species] = [Survey Elements].[Record Type] " +
                        "AND [ID] IN (SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN (SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "'))))) OR " +
                //Scoping table 2
                "((([RPlantRank] LIKE '1%' OR [RPlantRank] LIKE '2%') OR (UPPER([FedList]) != 'NONE' OR UPPER([CalList]) != 'NONE')) AND ([RPlantRank]||[FedList]||[CalList] != '') AND [Exclude] = FALSE AND [ExcludeReport] = FALSE)" +
                ") ORDER BY [Species] ASC", SqlTools.SqlConn))
            { filler.Fill(dt); }



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

                if ((long)dt.Rows[i][5] == 0) Table3RowValues(plantTbl.Rows[rowCounter], "Species Found", "No", false);
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
            sectionRange.Paragraphs.Last.Range.Text = "Appendix B.  Plant species observed during botanical surveys for the " + thpArea + " THP (<add dates……>). ";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            DataTable dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [SciName],[ComName],[Family] FROM [Plants of Interest] WHERE [ID] IN " +
                "(SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN (SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "')) AND [Species Found] = TRUE GROUP BY [SciName]", SqlTools.SqlConn))
            { filler.Fill(dt); }

            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [SciName],[ComName],[Family] FROM [Plants List] WHERE [ID] IN " +
                "(SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN (SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "')) " +
                "AND [SciName] NOT IN (SELECT [SciName] FROM [Plants of Interest] WHERE [ID] IN " +
                    "(SELECT [ID] FROM [SurveyElementsSurveyAreaConnection] WHERE [Connection] IN (SELECT [ID] FROM [Survey Area] WHERE UPPER([THP Area]) = '" + thpArea.ToUpper() + "')) AND [Species Found] = TRUE)" +
                " GROUP BY [SciName]", SqlTools.SqlConn))
            { filler.Fill(dt); }


            dt.DefaultView.Sort = "SciName ASC";
            dt = dt.DefaultView.ToTable();


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
