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
    public class WatershedReport : WBISViewModelBase
    {
     
        public WatershedReport(Watershed[] watersheds)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"WatershedReport {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (Directory.Exists(sfd.FileName) || File.Exists(sfd.FileName + ".zip"))
            {
                MessageBox.Show("The selected name is already in use");
                goto HERE;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            Directory.CreateDirectory(sfd.FileName);

            var app = new Word.Application { Visible = false };
            var doc = app.Documents.Add();

            object styleName = "Normal";
            //object styleName = "No Spacing";
            doc.Paragraphs.set_Style(ref styleName);


            watersheds = Database.Watersheds.Where(_ => watersheds.Select(w => w.Id).Contains(_.Id)).ToArray();
            //var geo = watersheds.Select(_ => _.Geometry);
            var guids = watersheds.Select(_ => _.Id);
            Watershed[] adjacentWatersheds = new Watershed[0];// = Database.Watersheds.Where(_=> geo.Any(w=>w.Touches(_.Geometry)) && !guids.Contains(_.Guid)).ToArray();
           foreach(Watershed water in watersheds)
            {
                guids = guids.Append(adjacentWatersheds.Select(_ => _.Id));
                var adj = Database.Watersheds.Where(_ => _.Geometry.Touches(water.Geometry) && !guids.Contains(_.Id));
                adjacentWatersheds = adjacentWatersheds.Append(adj).ToArray();
            }
            //Write into
            WriteWatershedReportIntro(doc.Sections.Last.Range, watersheds.Select(_ => _.Id).ToList(), adjacentWatersheds.Select(_=>_.Id).ToList());
                       
            DateTime date = Database.CdfwVintages.Max(_ => _.date).Date;
            WatershedReportCannedTextFields(doc.Sections.Last.Range, $"{date.Year}-{date.Month}");


            watersheds = watersheds.Append(adjacentWatersheds).ToArray();


            WatershedTable(doc.Sections.Last.Range, watersheds, sfd.FileName);
            doc.Sections.Add();
            doc.Sections.Last.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            WatershedCnddbTables(doc.Sections.Last.Range, watersheds, sfd.FileName);
            doc.Sections.Add();
            WatershedCdfwOwlTables(doc.Sections.Last.Range, watersheds, sfd.FileName);
            doc.Sections.Add();
            WatershedSpiTables(doc.Sections.Last.Range, watersheds, sfd.FileName);

            WatershedReportFormatting(doc, $"{date.Year}-{date.Month}");

            string filename = sfd.FileName + "\\WatershedReport " + DateTime.Now.ToString("yyyy-MM-dd") + ".docx";
            doc.SaveAs(filename);
            doc.Close(true);
            app.Quit();

            ZipFile.CreateFromDirectory(sfd.FileName, sfd.FileName + ".zip");

            Directory.Delete(sfd.FileName, true);
            w.Stop();
            System.Windows.MessageBox.Show("The watershed report has finished");
        }

        WordHelper WH = new WordHelper();

       
        private void WatershedReportFormatting(Word.Document doc, string cnddbDate)
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
                // doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                //doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();
                //if (i < doc.Sections.Count - 1)
                //{
                //WatershedReportFooter(doc, i + 1, totalPages, cnddb);
                WatershedReportHeader(doc, i + 1);
                //}
            }

            Microsoft.Office.Interop.Word.WdStatistic PagesCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
            object Miss = System.Reflection.Missing.Value;
            int totalPages = (int)doc.ComputeStatistics(PagesCountStat, ref Miss);
            for (int i = 0; i < doc.Sections.Count; i++)
            {
                doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
                doc.Sections[i + 1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Delete();
                //if (i < doc.Sections.Count - 1)
                //{
                WatershedReportFooter(doc, i + 1, totalPages, cnddbDate);
                //}
            }
        }

        private void WatershedReportFooter(Word.Document doc, int section, object totalPages, string cnddbPrintDate)
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
            //footerTbl.Cell(1, 1).Range.Paragraphs.Last.Range.Text = "CNDDB Release Date: " + cnddbPrintDate;
            footerTbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            footerTbl.Cell(1, 2).Range.Font.Size = 10;
            footerTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            footerTbl.Cell(1, 2).Range.Select();//.Text = thp;

            activeWindow.Selection.TypeText("Page ");
            activeWindow.Selection.Fields.Add(activeWindow.Selection.Range, ref currentPage, ref missing, ref missing);
            activeWindow.Selection.TypeText(" of " + totalPages);

            footerTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
        }

        private void WatershedReportHeader(Word.Document doc, int section)
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
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Last.Range.Text = "Watershed and Biological Assessment Area Report";
            HeaderTbl.Cell(2, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            if (section == 1)
            {
                HeaderTbl.Cell(1, 2).Merge(HeaderTbl.Cell(2, 2));

                HeaderTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                //HeaderTbl.Cell(1, 2).Select();
                var logo = HeaderTbl.Cell(1, 2).Range.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "\\ReportResources\\SpiLogo.png");
            }
            else
            {
                HeaderTbl.Cell(1, 2).Range.Paragraphs.Last.Range.Font.Bold = 1;
                HeaderTbl.Cell(1, 2).Range.Paragraphs.Last.Range.Font.Size = 11;
                HeaderTbl.Cell(1, 2).Range.Paragraphs.Last.Range.Text = "THP Name: XXXX";
                HeaderTbl.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            }
            HeaderTbl.Columns.PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthPercent;//.DistributeWidth();
            HeaderTbl.Columns[1].PreferredWidth = 75f;
            HeaderTbl.LeftPadding = (float)-.02;
        }






        private void WriteWatershedReportIntro(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds)
        {
            sectionRange.Paragraphs.Add();

            //string watershedIds = string.Join(", ", watersheds);
            //string adjacentWatershedIds = string.Join(", ", adjacentWatersheds);


            string wshdStr = String.Join(", ", Database.Watersheds.Where(_ => watersheds.Contains(_.Id)).Select(_ => _.WatershedName + "(" + _.WatershedID + ")"));
            string wshdAdjStr = String.Join(", ", Database.Watersheds.Where(_ => adjacentWatersheds.Contains(_.Id)).Select(_ => _.WatershedName + "(" + _.WatershedID + ")"));
            int runningTab = 0;
            runningTab = WH.AddIntroDescriptiveText("THP Watershed(s):", wshdStr, sectionRange.Document, runningTab);
            runningTab = WH.AddIntroDescriptiveText("Adjacent Watershed(s):", wshdAdjStr, sectionRange.Document, runningTab);


            object styleName = "Table Grid";
            //sectionRange.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            sectionRange.Paragraphs.Last.Range.ParagraphFormat.TabHangingIndent(-1);
            var introTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.First.Range, 3, 2);
            //var introTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, 3, 2);
            introTbl.AllowAutoFit = true;
            introTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            introTbl.set_Style(ref styleName);
            introTbl.Borders.Enable = 0;
            //introTbl.Range.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            WH.FillCell(introTbl.Cell(2, 1), "THP Name:", "XXX", sectionRange);
            WH.FillCell(introTbl.Cell(2, 2), "SPI Forester:", "XXX", sectionRange);
            WH.FillCell(introTbl.Cell(3, 1), "Date:", "X/XX/XXXX", sectionRange);
            WH.FillCell(introTbl.Cell(3, 2), "SPI District:", "XXX", sectionRange);
            introTbl.Rows.First.Delete();// = 0f;

            introTbl.LeftPadding = (float)-.02;

        }

        private void WatershedReportCannedTextFields(Word.Range sectionRange, string cnddbDate)
        {
            // sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Format.TabHangingIndent(-1);

            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Methods:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Text = "SPI determined potentially occurring special-status biological resources in the THP Watershed and Biological Assessment Area by " +
                "reviewing the California Natural Diversity Database (CNDDB) (CNDDB Version: " + cnddbDate + ", Query Date: " + DateTime.Now.ToShortDateString() + "), the California Department of Fish and Wildlife’s Spotted Owl Database, and our corporate WBIS database.";
            // sectionRange.Paragraphs.Add();
            // sectionRange.Paragraphs.Last.Range.ListFormat.ApplyBulletDefault();
            // //sectionRange.Paragraphs.Add();
            // sectionRange.Paragraphs.Last.Range.Text = "CNDDB Watershed Search including the xxx, xxx, and xxx planning watersheds (CNDDB Version: xxx, Query Date: xxx).";
            // //sectionRange.Paragraphs.Add();
            // sectionRange.Paragraphs.Add();
            // sectionRange.Paragraphs.Last.Range.Text = "CNDDB Quadrangle Search including the xxx, xxx, and xxx quadrangles (CNDDB Version: xxx, Query Date: xxx).";




            //// sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();

            // object styleName = "Normal";
            // //object styleName = "No Spacing";
            // sectionRange.Paragraphs.Last.set_Style(ref styleName);

            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Regulatory Status Determinations Considered:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Text = "SPI considered special-status plant and wildlife species for the Watershed and Biological Resources assessment.  " +
                "For the purpose of this assessment, special-status plants include (1) plant species listed as endangered (FE) or threatened (FT) under the Federal Endangered Species Act, " +
                "and candidate (FC) or proposed (FP) species for federal listing; (2) plant species listed as endangered (SE), threatened (ST), or rare (SR) under the California Endangered Species Act, " +
                "and candidate (SC) or proposed (SP) species for state listing; or (3) have a California Department of Fish and Wildlife California Rare Plant Rank (CRPR) of 1, 2, 3, or 4. ";
            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Text = "Also for the purpose of this assessment, special-status wildlife includes (1) wildlife species listed as endangered (FE) or threatened (FT) " +
                "under the Federal Endangered Species Act, and candidate (FC) or proposed (FP) species for federal listing; (2) wildlife species listed as endangered (SE), or threatened (ST) " +
                "under the California Endangered Species Act, and candidate (SC) or proposed (SP) species for state listing; (3) designated as Sensitive Species by the Board of Forestry (BOF) pursuant to 14 CCR 898.2(d); " +
                "or (4) considered California Species of Special Concern by the California Department of Fish and Wildlife (SSC).";
            // sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Text = "Additionally, the Watershed and Biological Resources assessment considers species rankings by the NatureServe Global Conservation Status Ranks.  " +
                "SPI uses G1 and G2 rankings to identify or select ecologically important sites for potential protection measures and demonstrate our commitment to the Sustainable Forestry Initiative program.";
            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Text = "SPI confirmed the regulatory status of all known or potentially occurring special-status species during the scoping " +
                "using current lists provided by the CDFW Biogeographic Data Branch and NatureServe.";



            //sectionRange.Paragraphs.Add();
            //sectionRange.Paragraphs.Add();
            sectionRange.Document.Sections.Add();
            sectionRange.Paragraphs.Last.Format.TabHangingIndent(-1);

            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Results:";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Text = "XXX special-status species were evaluated during the assessment.  XXX" +
                " species are determined to occur or potentially occur in the THP area and are further discussed in Section II of the THP.  " +
                "Summaries of the Watershed and Biological Assessment results are included in the tables below.";
            //SetHighlightPartial(sectionRange.Paragraphs.Last.Range, 0, sectionRange.Paragraphs.Last.Range.Text.IndexOf("XXX", 8) + 3);
            SetColorPartial(sectionRange.Paragraphs.Last.Range, 0, 4, Word.WdColor.wdColorRed);
            SetColorPartial(sectionRange.Paragraphs.Last.Range, sectionRange.Paragraphs.Last.Range.Text.IndexOf("XXX", 8), 4, Word.WdColor.wdColorRed);
            SetHighlightPartial(sectionRange.Paragraphs.Last.Range, 0, 4, Word.WdColorIndex.wdYellow);
            SetHighlightPartial(sectionRange.Paragraphs.Last.Range, sectionRange.Paragraphs.Last.Range.Text.IndexOf("XXX", 8), 4, Word.WdColorIndex.wdYellow);
        }


        private void WatershedTable(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            //AddPageSection(sectionRange);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            sectionRange.Paragraphs.Last.Range.Font.Size = 14;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            //var border = sectionRange.Paragraphs.Last.Range.Borders[Word.WdBorderType.wdBorderBottom];
            //border.LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            //border.LineWidth = Word.WdLineWidth.wdLineWidth100pt;
            //border.Color = Word.WdColor.wdColorBlack;
            sectionRange.Paragraphs.Last.Range.Text = "Watershed Summary";
            sectionRange.Paragraphs.Last.Range.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleNone;
            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;
            sectionRange.Paragraphs.Last.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;


            DataTable dt = new DataTable();
            dt.Columns.Add("CPW #");
            dt.Columns.Add("Watershed Name");
            dt.Columns.Add("Hydro Unit");
            dt.Columns.Add("Total Acres", typeof(double));
            dt.Columns.Add("SPI%", typeof(double));
            dt.Columns.Add("Public%", typeof(double));
            dt.Columns.Add("Impaired? 303(d) listed");
            dt.Columns.Add("Anadromous Watershed");

            foreach(var w in watersheds)
            {
                DataRow row = dt.NewRow();
                row["CPW #"] = w.WatershedID;
                row["Watershed Name"] = w.WatershedName;
                row["Hydro Unit"] = w.Hydrologic;
                row["Total Acres"] = w.WshdAcres;
                row["SPI%"] = (w.SPIAcres/w.WshdAcres) * 100;
                row["Public%"] = ((w.WshdAcres - w.SPIAcres) / w.WshdAcres) * 100;
                row["Impaired? 303(d) listed"] = w.D303;
                if (w.ESU != null)
                {
                    if (w.ESU.Value) row["Anadromous Watershed"] = "Yes";
                    else if (!w.ESU.Value) row["Anadromous Watershed"] = "No";
                }
                dt.Rows.Add(row);
            }

            var tv = dt.AsDataView();
            tv.Sort = "CPW # ASC";
            dt = tv.ToTable();

            WordTableWriter(sectionRange, dt);
            new PostGisShapefileConverter(typeof(Watershed), watersheds.AsQueryable(), dirName + "\\Watershed.shp");
        }




    


        private void WatershedCnddbTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("Scientific Name");
            dt.Columns.Add("Common Name");
            dt.Columns.Add("CNDDB Occurrence #");
            dt.Columns.Add("Date");
            dt.Columns.Add("Map ID (EONDX)");
            dt.Columns.Add("State Status");
            dt.Columns.Add("Federal Status");
            dt.Columns.Add("NatureServe Ranking");



            string[] salmonOptions = new string[] {"ONCORHYNCHUS KISUTCH POP. 2",
"ONCORHYNCHUS MYKISS IRIDEUS POP. 1",
"ONCORHYNCHUS MYKISS IRIDEUS POP. 11",
"ONCORHYNCHUS MYKISS IRIDEUS POP. 16",
"ONCORHYNCHUS MYKISS IRIDEUS POP. 36",
"ONCORHYNCHUS TSHAWYTSCHA POP. 11",
"ONCORHYNCHUS TSHAWYTSCHA POP. 13",
"ONCORHYNCHUS TSHAWYTSCHA POP. 17",
"ONCORHYNCHUS TSHAWYTSCHA POP. 30",
"ONCORHYNCHUS TSHAWYTSCHA POP. 7",
};
            bool salmon = false;

            var cnddbs = new CNDDBOccurrence().Manager.GetQueryable(watersheds, typeof(Watershed), Database, ForceInclude: new List<string>() { "Watersheds"},includeGeometry: true);
            foreach (CNDDBOccurrence c in cnddbs)
            {
                foreach (var w in c.Watersheds)
                {
                    DataRow row = dt.NewRow();

                    row["WshdId"] = w.WatershedID;
                    row["CPW Name"] = w.WatershedName;
                    row["Scientific Name"] = c.SNAME;
                    row["Common Name"] = c.CNAME;
                    row["CNDDB Occurrence #"] = c.OCCNUMBER;
                    row["Date"] = c.ELMDATE;
                    row["Map ID (EONDX)"] = c.EONDX;
                    row["State Status"] = c.CALLIST;
                    row["Federal Status"] = c.FEDLIST;
                    row["NatureServe Ranking"] = c.GRANK;
                    dt.Rows.Add(row);

                    if (salmonOptions.Contains(row["Scientific Name"].ToString().ToUpper()))
                    {
                        salmon = true;
                        break;
                    }
                }
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC, Scientific Name ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "CNDDB (Listed by CPW Name)";
            if (salmon)
            {
                sectionRange.Paragraphs.Add();
                sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
                sectionRange.Paragraphs.Last.Range.Font.Size = 11;
                sectionRange.Paragraphs.Last.Range.Text = "Note: Protection and conservation measures for special-status anadromous salmonids are covered by " +
                    "SPI’s Habitat Conservation Plan and Safe Harbor Agreement for Seven Anadromous Fish Populations, dated 26 May 2020 (HCP/SHA), " +
                    "and a determination by the California Department of Fish and Wildlife that the HCP/SHA is consistent with the terms and conditions of the California Endangered Species Act (dated xxxxx).";
                int start = sectionRange.Paragraphs.Last.Range.Text.IndexOf("Habitat");
                int last = sectionRange.Paragraphs.Last.Range.Text.IndexOf("dated");
                SetItealicPartial(sectionRange.Paragraphs.Last.Range, start, last - start);
            }
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            WordTableWriter(sectionRange, dt);
            new PostGisShapefileConverter(typeof(CNDDBOccurrence), cnddbs, dirName + "\\CNDDB Occurrences.shp");
        }

        private void WatershedCdfwOwlTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            var cdfwOwls = new CDFW_SpottedOwl().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });

            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("Masterowl");
            dt.Columns.Add("Typeobs");
            dt.Columns.Add("Dateobs");
            dt.Columns.Add("Numasobs");
            dt.Columns.Add("Agesex");
            dt.Columns.Add("Pair");
            dt.Columns.Add("Nest");
            dt.Columns.Add("Numyoung");
            dt.Columns.Add("Subspecies");
            dt.Columns.Add("LatDD_N83", typeof(double));
            dt.Columns.Add("LonDD_N83", typeof(double));

            foreach(CDFW_SpottedOwl c in cdfwOwls)
            {
                DataRow row = dt.NewRow();

                row["WshdId"] = c.Watershed.WatershedID;
                row["CPW Name"] = c.Watershed.WatershedName;
                row["Masterowl"] = c.MASTEROWL;
                row["Typeobs"] = c.TYPEOBS;
                row["Dateobs"] = c.DATEOBS;
                row["Numasobs"] = c.NUMADOBS;
                row["Agesex"] = c.AGESEX;
                row["Pair"] = c.PAIR;
                row["Nest"] = c.NEST;
                row["Numyoung"] = c.NUMYOUNG;
                row["Subspecies"] = c.SUBSPECIES;
                row["LatDD_N83"] = c.LATDD_N83;
                row["LonDD_N83"] = c.LONDD_N83;

                dt.Rows.Add(row);
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "CDFW Spotted Owl Observations (Listed by CPW Name)";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //WordTableWriterCnddb(sectionRange, dt);
            WordTableWriter(sectionRange, dt);
            new PostGisShapefileConverter(typeof(CDFW_SpottedOwl), cdfwOwls, dirName + "\\CDFW Spotted Owl.shp");
        }



        private void WatershedSpiTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "SPI Database";
            sectionRange.Paragraphs.Last.Range.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleNone;
            sectionRange.Paragraphs.Last.Range.Text = "Plants (Listed by CPW Name)";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            PlantTables(sectionRange, watersheds, dirName);


            //AddPageSection(sectionRange);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Wildlife (Listed by CPW Name)";
            sectionRange.Paragraphs.Add();


            NOGOTables(sectionRange, watersheds, dirName);
            GGOWTables(sectionRange, watersheds, dirName);
            SPOWTables(sectionRange, watersheds, dirName);
            OtherWildlifeTables(sectionRange, watersheds, dirName);
        }



        private void PlantTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            watersheds = watersheds.OrderBy(_ => _.WatershedName).ToArray();

            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("Scientific Name");
            dt.Columns.Add("Common Name");
            dt.Columns.Add("California Rare Plant Rank");
            dt.Columns.Add("State Status");
            dt.Columns.Add("Federal Status");
            dt.Columns.Add("NatureServe Ranking");

            foreach(Watershed w in watersheds)
            {
                var sciNames = new List<string>();

                var a = new SPIPlantPoint().Manager.GetQueryable(new object[] { w }, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });
                foreach(SPIPlantPoint p in a)
                {                    
                    if (!sciNames.Contains(p.PlantSpecies.SciName))
                        AddPlantSpecies(ref sciNames, ref dt, p.PlantSpecies, w);
                }

                a = new SPIPlantPolygon().Manager.GetQueryable(new object[] { w }, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watersheds" });
                foreach (SPIPlantPolygon p in a)
                {
                    if (!sciNames.Contains(p.PlantSpecies.SciName))
                        AddPlantSpecies(ref sciNames, ref dt, p.PlantSpecies, w);
                }

                a = new BotanicalElement().Manager.GetQueryable(new object[] { w }, typeof(Watershed), Database,
                    ForceInclude: new List<string>() { "BotanicalPlantOfInterest.PlantSpecies", "Watershed" }, includeGeometry: true)
                .Cast<BotanicalElement>().Where(_ => _.BotanicalPlantOfInterest != null && !_._delete && !_.Repository);
                foreach (BotanicalElement p in a)
                {
                    if (!sciNames.Contains(p.BotanicalPlantOfInterest.PlantSpecies.SciName))
                        AddPlantSpecies(ref sciNames, ref dt, p.BotanicalPlantOfInterest.PlantSpecies, w);
                }
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC, Scientific Name ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            WordTableWriter(sectionRange, dt);

            var spiPoints = new SPIPlantPoint().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });
            new PostGisShapefileConverter(typeof(SPIPlantPoint), spiPoints, dirName + "\\SPI Plant Point.shp");
            var spiPolys = new SPIPlantPolygon().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watersheds" });
            new PostGisShapefileConverter(typeof(SPIPlantPolygon), spiPolys, dirName + "\\SPI Plant Polygon.shp");
            var elements = new BotanicalElement().Manager.GetQueryable(watersheds, typeof(Watershed), Database,
                ForceInclude: new List<string>() { "BotanicalPlantOfInterest.PlantSpecies", "Watershed" }, includeGeometry: true)
                .Cast<BotanicalElement>().Where(_=>_.BotanicalPlantOfInterest != null && !_._delete && !_.Repository);
            new PostGisShapefileConverter(typeof(BotanicalElement), elements, dirName + "\\Plants of Interest.shp");
        }

        private void AddPlantSpecies(ref List<string> sciNames, ref DataTable dt, PlantSpecies plantSpecies, Watershed watershed)
        {
            var row = dt.NewRow();

            row["WshdId"] = watershed.WatershedID;
            row["CPW Name"] = watershed.WatershedName;
            row["Scientific Name"] = plantSpecies.SciName;
            row["Common Name"] = plantSpecies.ComName;
            row["California Rare Plant Rank"] = plantSpecies.RPlantRank;
            row["State Status"] = plantSpecies.CalList;
            row["Federal Status"] = plantSpecies.FedList;
            row["NatureServe Ranking"] = plantSpecies.GRank;

            dt.Rows.Add(row);
            sciNames.Add(plantSpecies.SciName);
        }

        private void NOGOTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            sectionRange.Paragraphs.Last.Range.Text = "Northern Goshawk (Accipter gentilis, listed by CPW Name)\t\t";

            int start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("BOF: Listed\t\t");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 5);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("NatureServe Ranking: G5");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 21);

            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("Territory Name");
            dt.Columns.Add("NestName");
            dt.Columns.Add("District ID");
            dt.Columns.Add("Year", typeof(int));
            dt.Columns.Add("Map ID");
            dt.Columns.Add("LON (NAD27)", typeof(double));
            dt.Columns.Add("LAT (NAD27)", typeof(double));
            dt.Columns.Add("Status");
            dt.Columns.Add("Nest");

            var owls = new SPI_NOGO().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });

            foreach(SPI_NOGO owl in owls)
            {
                DataRow row = dt.NewRow();

                row["WshdId"] = owl.Watershed.WatershedID;
                row["CPW Name"] = owl.Watershed.WatershedName;
                row["Territory Name"] = owl.Territory;
                row["NestName"] = owl.NestName;
                row["District ID"] = owl.Dist_ID;
                row["Year"] = owl.Year;
                row["LON (NAD27)"] = owl.Longitude;
                row["LAT (NAD27)"] = owl.Latitude;
                row["Status"] = owl.TerritoryStatus;
                row["Nest"] = owl.Nest;

                dt.Rows.Add(row);
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC, District ID ASC, Year ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            string fileName = dirName + "\\SPI_NOGO.shp";
            dt = ExportNewShapes(dt, fileName);
            WordTableWriter(sectionRange, dt);


            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 9;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.Text = "AU=nesting unknown number of young, ";
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 2);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("A#=nest with # of young, ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 2);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("AN=active, nest not found, ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 2);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("F=failed nest, I-territory inactive, ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 1);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("NP=not surveyed to protocol, ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 2);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("NV=Not visited, ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 2);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("P=predated nest.");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 1);
        }
        private void SPOWTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Spotted Owl Sites (listed by CPW Name)";
            sectionRange.Paragraphs.Add();
            object styleName = "No Spacing";
            sectionRange.Paragraphs.Last.set_Style(ref styleName);
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 9;
            sectionRange.Paragraphs.Last.Range.Text = "California spotted owl (CSO, Strix occidentalis occidentalis)\tState Status: Species of Special Concern\tFederal Status: not listed\tNatureServe Ranking: G3G4";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Text = "Northern spotted owl (NSO, Strix occidentalis caurina)\t\tState Status: Threatened\t\t\tFederal Status: Threatened\tNatureServe Ranking: G3G4";

            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.ListFormat.ApplyBulletDefault();
            sectionRange.Paragraphs.Last.Range.Text = "All pertinent data from the California Department of Fish and Wildlife’s Spotted owl database have been included in this SPI dataset.";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Text = "Activities near spotted owl sites are covered under the Habitat Conservation Plan for Northern and California Spotted Owls (HCP, Permit #TE84089D-0)";


            sectionRange.Paragraphs.Add();
            styleName = "Normal";
            sectionRange.Paragraphs.Last.set_Style(ref styleName);


            //sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("Subspecies");
            dt.Columns.Add("District ID");
            dt.Columns.Add("CDFW ID");
            dt.Columns.Add("Territory");
            dt.Columns.Add("Year", typeof(int));
            dt.Columns.Add("Map ID");
            dt.Columns.Add("Site Status");
            dt.Columns.Add("LON (NAD27)", typeof(double));
            dt.Columns.Add("LAT (NAD27)", typeof(double));
            dt.Columns.Add("HCP Status");

            var owls = new SPI_SPOW().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });

            foreach (SPI_SPOW owl in owls)
            {
                DataRow row = dt.NewRow();

                row["WshdId"] = owl.Watershed.WatershedID;
                row["CPW Name"] = owl.Watershed.WatershedName;
                row["Subspecies"] = owl.SubSpecies1;
                row["District ID"] = owl.Dist_ID;
                row["CDFW ID"] = owl.CDFW_ID;
                row["Territory"] = owl.Territory;
                row["Year"] = owl.Year;
                row["Site Status"] = owl.BirdStatus;
                row["LON (NAD27)"] = owl.Longitude;
                row["LAT (NAD27)"] = owl.Latitude;
                row["HCP Status"] = owl.HCP_Status_2;

                dt.Rows.Add(row);
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC, District ID ASC, Year ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            string fileName = dirName + "\\SPI_SPOW.shp";
            dt = ExportNewShapes(dt, fileName);
            WordTableWriter(sectionRange, dt);

            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
            sectionRange.Paragraphs.Last.Range.ListFormat.ApplyBulletDefault();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            int start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.Text = "Active=recently occupied, ";
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 7);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("Retired=no owl response from 3 consecutive years of HCP protocol surveys, ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 8);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("Historical=USFWS abandoned, HCP removed, No habitat remains, long-term no response.  ");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 11);

            start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("BPI=Beyond Potential Impacts as determined by USFWS in HCP.");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 4);

            sectionRange.Paragraphs.Add();
            styleName = "Normal";
            sectionRange.Paragraphs.Last.set_Style(ref styleName);

            sectionRange.Paragraphs.Add();
            styleName = "Normal";
            sectionRange.Paragraphs.Last.set_Style(ref styleName);
        }

        private void SetBoldPartial(Word.Range range, int startBold, int endBold)
        {

            object objStart = range.Start + startBold;
            object objEnd = range.Start + startBold + endBold;
            object endText = range.End;

            range.Document.Range(ref objStart, ref objEnd).Bold = 1;
            range.Document.Range(ref objEnd, ref endText).Bold = 0;
        }
        private void SetHighlightPartial(Word.Range range, int startBold, int endBold, Word.WdColorIndex color)
        {

            object objStart = range.Start + startBold;
            object objEnd = range.Start + startBold + endBold;
            object endText = range.End;

            range.Document.Range(ref objStart, ref objEnd).HighlightColorIndex = color;
            range.Document.Range(ref objEnd, ref endText).HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
        }
        private void SetColorPartial(Word.Range range, int startBold, int endBold, Word.WdColor color)
        {

            object objStart = range.Start + startBold;
            object objEnd = range.Start + startBold + endBold;
            object endText = range.End;

            range.Document.Range(ref objStart, ref objEnd).Font.Color = color;
            range.Document.Range(ref objEnd, ref endText).Font.Color = Word.WdColor.wdColorBlack;
        }
        private void SetItealicPartial(Word.Range range, int startBold, int endBold)
        {

            object objStart = range.Start + startBold;
            object objEnd = range.Start + startBold + endBold;
            object endText = range.End;

            range.Document.Range(ref objStart, ref objEnd).Font.Italic = 1;
            range.Document.Range(ref objEnd, ref endText).Font.Italic = 0;
        }
        private void GGOWTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Great Gray Owl Sites (Strix nebulosa, listed by CPW Name)\t\t ";

            int start = sectionRange.Paragraphs.Last.Range.Text.Length - 1;
            sectionRange.Paragraphs.Last.Range.InsertAfter("NatureServe Ranking: G5");
            SetBoldPartial(sectionRange.Paragraphs.Last.Range, start, 21);

            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;


            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("District ID");
            dt.Columns.Add("Year", typeof(int));
            dt.Columns.Add("Map ID");
            dt.Columns.Add("LON (NAD27)", typeof(double));
            dt.Columns.Add("LAT (NAD27)", typeof(double));
            dt.Columns.Add("Status");

            var owls = new SPI_GGOW().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });

            foreach (SPI_GGOW owl in owls)
            {
                DataRow row = dt.NewRow();

                row["WshdId"] = owl.Watershed.WatershedID;
                row["CPW Name"] = owl.Watershed.WatershedName;
                row["District ID"] = owl.District.DistrictName;
                row["Year"] = owl.Year;
                row["LON (NAD27)"] = owl.Longitude;
                row["LAT (NAD27)"] = owl.Latitude;
                row["Status"] = owl.Results;

                dt.Rows.Add(row);
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC, District ID ASC, Year ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            string fileName = dirName + "\\SPI_GGOW.shp";
            dt = ExportNewShapes(dt, fileName);
            WordTableWriter(sectionRange, dt);
        }
        private void OtherWildlifeTables(Word.Range sectionRange, Watershed[] watersheds, string dirName)
        {
            //            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Other Wildlife Sighting";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;


            DataTable dt = new DataTable();
            dt.Columns.Add("WshdId");
            dt.Columns.Add("CPW Name");
            dt.Columns.Add("Common Name");
            dt.Columns.Add("Species");
            dt.Columns.Add("Year", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("LON (NAD27)", typeof(double));
            dt.Columns.Add("LAT (NAD27)", typeof(double));
            dt.Columns.Add("Map ID");
            dt.Columns.Add("NatureServe Ranking");

            var owls = new SPI_WildlifeSighting().Manager.GetQueryable(watersheds, typeof(Watershed), Database, includeGeometry: true, ForceInclude: new List<string>() { "Watershed" });

            foreach (SPI_WildlifeSighting owl in owls)
            {
                DataRow row = dt.NewRow();

                row["WshdId"] = owl.Watershed.WatershedID;
                row["CPW Name"] = owl.Watershed.WatershedName;
                row["Common Name"] = owl.WildlifeSpecies;
                row["Species"] = owl.Species;
                row["Year"] = owl.Year;
                row["Quantity"] = owl.NumObserved;
                row["LON (NAD27)"] = owl.Longitude;
                row["LAT (NAD27)"] = owl.Latitude;
                row["NatureServe Ranking"] = owl.IUCN_Rating;

                dt.Rows.Add(row);
            }

            var tv = dt.AsDataView();
            tv.Sort = "WshdId ASC, Year ASC";
            dt = tv.ToTable();
            dt.Columns.Remove("WshdId");

            string fileName = dirName + "\\SPI_Wildlife Sightings.shp";
            dt = ExportNewShapes(dt, fileName);
            WordTableWriter(sectionRange, dt);
        }




        private void WordTableWriterCnddb(Word.Range sectionRange, DataTable dt)
        {
            object styleName = "Table Grid";
            var wordTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, (dt.Rows.Count * 2) + 1, dt.Columns.Count - 1);
            wordTbl.Columns.DistributeWidth();
            wordTbl.AllowAutoFit = true;
            wordTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            wordTbl.set_Style(ref styleName);
            wordTbl.Borders.Enable = 1;
            wordTbl.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTbl.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTbl.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            wordTbl.Rows[1].HeadingFormat = -1;
            wordTbl.ApplyStyleHeadingRows = true;

            //
            int colCounter = 0;
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (dt.Columns[c].ColumnName == "Habitat Description") continue;
                wordTbl.Cell(1, colCounter + 1).Range.Text = dt.Columns[c].ColumnName;
                colCounter++;
            }
            wordTbl.Rows[1].Range.Font.Bold = 1;
            wordTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            //plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            if (dt.Rows.Count == 0)
            {
                wordTbl.Rows.Add();
                wordTbl.Cell(2, 1).Range.Text = "None";
            }

            int rowCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                colCounter = 0;
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Columns[c].ColumnName != "Habitat Description")
                    {
                        if (dt.Rows[i][c] is DBNull)
                        {
                            if (dt.Columns[c].ColumnName.Contains("%")) dt.Rows[i][c] = 0d;
                            // else dt.Rows[i][c] = "";
                        }

                        if (dt.Columns[c].ColumnName.Contains("%"))
                            wordTbl.Cell(rowCount + 2, colCounter + 1).Range.Text = ((double)dt.Rows[i][c]).ToString("N2");
                        else if (dt.Columns[c].ColumnName.Contains("Scientific"))
                        {
                            wordTbl.Cell(rowCount + 2, colCounter + 1).Range.Text = dt.Rows[i][c].ToString();
                            WH.WordCellItalicSpecies(wordTbl.Cell(rowCount + 2, c + 1), dt.Rows[i][c].ToString());
                        }
                        else if ((dt.Columns[c].ColumnName == "Genus" || dt.Columns[c].ColumnName == "Species") && i > 0)
                        {
                            wordTbl.Cell(rowCount + 2, colCounter + 1).Range.Text = dt.Rows[i][c].ToString();
                            WH.WordCellItalicSpecies(wordTbl.Cell(rowCount + 2, c + 1), dt.Rows[i][c].ToString());
                        }
                        else
                            wordTbl.Cell(rowCount + 2, colCounter + 1).Range.Text = dt.Rows[i][c].ToString();
                        colCounter++;
                    }
                }
                rowCount++;

                wordTbl.Cell(rowCount + 2, 1).Merge(wordTbl.Cell(rowCount + 2, 2));
                wordTbl.Cell(rowCount + 2, 1).Range.Text = "Habitat Description";

                wordTbl.Cell(rowCount + 2, 2).Merge(wordTbl.Cell(rowCount + 2, 8));
                wordTbl.Cell(rowCount + 2, 2).Range.Text = dt.Rows[i]["Habitat Description"].ToString();


                if (((double)i / 2d).ToString().Contains("."))
                {
                    wordTbl.Rows[rowCount + 1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10;
                    wordTbl.Rows[rowCount + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10;
                }
                rowCount++;
            }
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
        }
        private void WordTableWriter(Word.Range sectionRange, DataTable dt)
        {
            object styleName = "Table Grid";
            var wordTbl = sectionRange.Tables.Add(sectionRange.Paragraphs.Last.Range, dt.Rows.Count + 1, dt.Columns.Count);
            wordTbl.Columns.DistributeWidth();
            wordTbl.AllowAutoFit = true;
            wordTbl.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            wordTbl.set_Style(ref styleName);
            wordTbl.Borders.Enable = 1;
            wordTbl.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTbl.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTbl.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTbl.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            wordTbl.Rows[1].HeadingFormat = -1;
            wordTbl.ApplyStyleHeadingRows = true;

            //
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                wordTbl.Cell(1, c + 1).Range.Text = dt.Columns[c].ColumnName;
            }
            wordTbl.Rows[1].Range.Font.Bold = 1;
            wordTbl.Rows[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
            //plantTbl.Rows[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

            if (dt.Rows.Count == 0)
            {
                wordTbl.Rows.Add();
                wordTbl.Cell(2, 1).Range.Text = "None";
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Rows[i][c] is DBNull)
                    {
                        if (dt.Columns[c].ColumnName.Contains("%")) dt.Rows[i][c] = 0d;
                        // else dt.Rows[i][c] = "";
                    }

                    if (dt.Columns[c].ColumnName.Contains("%"))
                        wordTbl.Cell(i + 2, c + 1).Range.Text = ((double)dt.Rows[i][c]).ToString("N2");
                    else if (dt.Columns[c].ColumnName.Contains("Scientific"))
                    {
                        wordTbl.Cell(i + 2, c + 1).Range.Text = dt.Rows[i][c].ToString();
                        WH.WordCellItalicSpecies(wordTbl.Cell(i + 2, c + 1), dt.Rows[i][c].ToString());
                    }
                    else if ((dt.Columns[c].ColumnName == "Genus" || dt.Columns[c].ColumnName == "Species") && i > 0)
                    {
                        wordTbl.Cell(i + 2, c + 1).Range.Text = dt.Rows[i][c].ToString();
                        WH.WordCellItalicSpecies(wordTbl.Cell(i + 2, c + 1), dt.Rows[i][c].ToString());
                    }
                    else if ((dt.Columns[c].ColumnName == "LAT (NAD27)" || dt.Columns[c].ColumnName == "LON (NAD27)"))
                    {
                        if (!(dt.Rows[i][c] is DBNull))
                            wordTbl.Cell(i + 2, c + 1).Range.Text = ((double)dt.Rows[i][c]).ToString("N5");
                    }
                    else if ((dt.Columns[c].ColumnName == "LatDD_N83" || dt.Columns[c].ColumnName == "LonDD_N83"))
                    {
                        if (!(dt.Rows[i][c] is DBNull))
                            wordTbl.Cell(i + 2, c + 1).Range.Text = ((double)dt.Rows[i][c]).ToString("N5");
                    }
                    else if (dt.Columns[c].ColumnName == "Dateobs")
                    {
                        if (dt.Rows[i][c].ToString().Length > 4)
                            wordTbl.Cell(i + 2, c + 1).Range.Text = dt.Rows[i][c].ToString().Substring(0, 4);
                        else wordTbl.Cell(i + 2, c + 1).Range.Text = dt.Rows[i][c].ToString();
                    }
                    else
                        wordTbl.Cell(i + 2, c + 1).Range.Text = dt.Rows[i][c].ToString();
                }
                //string species = dt.Rows[i][0].ToString();
                //WordCellItalicSpecies(plantTbl.Cell(i + 2, 1), species);

                if (((double)i / 2d).ToString().Contains("."))
                {
                    wordTbl.Rows[i + 2].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10;
                }
            }
            sectionRange.Paragraphs.Last.Range.Font.Size = 11;
        }

        private DataTable ExportNewShapes(DataTable dt, string fileName)
        {
            Shapefile newFile = new PointShapefile(fileName);
            newFile.Projection = ProjectionInfo.FromEpsgCode(26710);

            string latCol = "";
            string lonCol = "";
            Dictionary<string, string> originalNames = new Dictionary<string, string>();

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName == "LAT (NAD27)") latCol = c.ColumnName;
                if (c.ColumnName == "LON (NAD27)") lonCol = c.ColumnName;

                string newCol = c.ColumnName.Replace(" ", "_").Replace("(", "").Replace(")", "");
                if (newCol.Length > 10) newCol = newCol.Substring(0, 10);
                newFile.DataTable.Columns.Add(newCol, c.DataType);
                originalNames.Add(c.ColumnName, newCol);
            }
            newFile.SaveAs(fileName, true);

            int featCounter = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r[latCol] is DBNull || r[lonCol] is DBNull)
                {
                    r["Map ID"] = "UnMapped";
                }
                else
                {
                    double[] StartXy = new double[] { (double)r[lonCol], (double)r[latCol] };
                    Atlas.Projections.Reproject.ReprojectPoints(StartXy, new double[] { }, ProjectionInfo.FromEpsgCode(4267), ProjectionInfo.FromEpsgCode(26710), 0, 1);
                    //r["StartLon27"] = StartXy[0];// ((GeoAPI.Geometries.IPoint)gs[0]).X;
                    //r["StartLat27"] = StartXy[1];//((GeoAPI.Geometries.IPoint)gs[0]).Y;

                    r["Map ID"] = featCounter;

                    var f = new Atlas.Data.Feature(new Coordinate(StartXy[0], StartXy[1]));

                    var newFeat = newFile.AddFeature(f.Geometry);
                    newFile.InitializeVertices();

                    foreach (DataColumn c in dt.Columns)
                    {
                        newFeat.DataRow[originalNames[c.ColumnName]] = r[c];
                    }

                    featCounter++;
                }
            }
            newFile.SaveAs(fileName, true);

            return dt;
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
