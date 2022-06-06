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

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class WatershedReportViewModel : WBISViewModelBase, IDocumentContent
    {
        public object Title => $"Watershed Report";      
      
        public WatershedReportViewModel()
        {
            SelectedItems = new ObservableCollection<Watershed>();
        }

        WordHelper WH = new WordHelper();

        public ObservableCollection<Watershed> SelectedItems { get; set; }

        public static WatershedReportViewModel Create()
        {
            return ViewModelSource.Create(() => new WatershedReportViewModel()
            { Caption = "Watershed Report", 
            });
        }
        public ICommand SaveCommand => new DelegateCommand(WriteReport);
        public void WriteReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"DistrictExport {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
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

            //var districts = SelectableDistricts.Where(_=>_.IsSelected).Select(_=>_.District).ToArray();
            //foreach(var infoType in SelectableInfoTypes.Where(_=>_.Selected))
            //{
            //    IInformationType i = (IInformationType)Activator.CreateInstance(infoType.InfoType);
            //    var records = i.Manager.GetQueryable(districts, typeof(District), Database, false, IncludeRepository, true);              
            //    string fileStr = $@"{sfd.FileName}\{i.Manager.DisplayName.Replace(" ","")}.shp";
            //    PostGisShapefileizer(i.GetType(),records,fileStr);
            //}

            ZipFile.CreateFromDirectory(sfd.FileName, sfd.FileName + ".zip");            

            Directory.Delete(sfd.FileName, true);
            w.Stop();
            System.Windows.MessageBox.Show("The district export has finished");
        }












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


            string wshdStr = String.Join(", ", Database.Watersheds.Where(_ => watersheds.Contains(_.Guid)).Select(_ => _.WatershedName + "(" + _.WatershedID + ")"));
            string wshdAdjStr = String.Join(", ", Database.Watersheds.Where(_ => adjacentWatersheds.Contains(_.Guid)).Select(_ => _.WatershedName + "(" + _.WatershedID + ")"));
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


        private void WatershedTable(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
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
            dt.Columns.Add("Total Acres");
            dt.Columns.Add("SPI%");
            dt.Columns.Add("Public%");
            dt.Columns.Add("Impaired? 303(d) listed");
            dt.Columns.Add("Anadromous Watershed");

            var wshds = Database.Watersheds.Where(_ => watersheds.Contains(_.Guid) || adjacentWatersheds.Contains(_.Guid));
            foreach(var w in wshds)
            {
                DataRow row = dt.NewRow();
                row["CPW #"] = w.WatershedID;
                row["Watershed Name"] = w.WatershedName;
                row["Hydro Unit"] = w.Hydrologic;
                row["Total Acres"] = w.WshdAcres;
                row["SPI%"] = (w.SPIAcres/w.WshdAcres) * 100;
                row["Public%"] = ((w.WshdAcres - w.SPIAcres) / w.WshdAcres) * 100;
                row["Impaired? 303(d) listed"] = w.D303;
                if (w.ESU.Value) row["Anadromous Watershed"] = "Yes";
                else if (!w.ESU.Value) row["Anadromous Watershed"] = "No";
                row["Anadromous Watershed"] = w.WatershedID;
                dt.Rows.Add(row);
            }


            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [WSHD_ID] as 'CPW #', [WSHD_NAME] as 'Watershed Name', [HYDROLOGIC] as 'Hydro Unit', [WSHD_ACRES] as 'Total Acres'," +
                "(([SPI_ACRES]/[WSHD_ACRES])*100) as 'SPI%',((([WSHD_ACRES]-[SPI_ACRES])/[WSHD_ACRES])*100) as 'Public%'," +
                "[D303] as 'Impaired? 303(d) listed',[ESU] as 'Anadromous Watershed' FROM [Watershed] " +
                "WHERE [WSHD_ID] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') ORDER BY [WSHD_ID]", SqlTools.SqlConn))
            { filler.Fill(dt); }

            foreach (DataRow r in dt.Rows)
            {
                if (r["Anadromous Watershed"].ToString().ToUpper() == "TRUE")
                    r["Anadromous Watershed"] = "Yes";
                else if (r["Anadromous Watershed"].ToString().ToUpper() == "FALSE")
                    r["Anadromous Watershed"] = "No";
            }

            WordTableWriter(sectionRange, dt);

            dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [FID] FROM [Watershed] WHERE [WSHD_ID] IN ('" + watershedIds + "','" + adjacentWatershedIds + "')", SqlTools.SqlConn))
            { filler.Fill(dt); }
            string fileName = dirName + "\\Watershed.shp";
            ExportSame(fileName, "Watershed", FeatureType.Polygon, dt);
        }

        private void WatershedCnddbTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT w.[WSHD_NAME] as 'CPW Name',cnddb.[SNAME] as 'Scientific Name',cnddb.[CNAME] as 'Common Name'," +
                "cnddb.[OCCNUMBER] as 'CNDDB Occurrence #',cnddb.[ELMDATE] as 'Date',cnddb.[EONDX] as 'Map ID (EONDX)'," +
                "cnddb.[CALLIST] as 'State Status',cnddb.[FEDLIST] as 'Federal Status',cnddb.[GRANK] as 'NatureServe Ranking' " +
                "FROM [CNDDBOccurrenceWatershedConnection] as 'conn'" +
                "INNER JOIN [Watershed] as 'w' ON w.[WSHD_ID] = conn.[Connection] " +
                "INNER JOIN [CNDDB Occurrence] as 'cnddb' ON cnddb.[EONDX] = conn.[ID] " +
                "WHERE conn.[Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') " +
                "ORDER BY conn.[Connection],cnddb.[SNAME]", SqlTools.SqlConn))
            { filler.Fill(dt); }

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
            foreach (DataRow r in dt.Rows)
            {
                if (salmonOptions.Contains(r["Scientific Name"].ToString().ToUpper()))
                {
                    salmon = true;
                    break;
                }
            }

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

            //WordTableWriterCnddb(sectionRange, dt);
            WordTableWriter(sectionRange, dt);

            dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [FID] FROM [CNDDB Occurrence] WHERE [EONDX] IN " +
                "(SELECT [ID] FROM [CNDDBOccurrenceWatershedConnection] WHERE [Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "'))", SqlTools.SqlConn))
            { filler.Fill(dt); }
            string fileName = dirName + "\\CNDDB Occurrences.shp";
            ExportSame(fileName, "CNDDB Occurrence", FeatureType.Polygon, dt);
        }

        private void WatershedCdfwOwlTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT w.[WSHD_NAME] as 'CPW Name',owl.[MASTEROWL] AS 'Masterowl'," +
"owl.[TYPEOBS] AS 'Typeobs'," +
"owl.[DATEOBS] AS 'Dateobs'," +
"owl.[NUMADOBS] AS 'Numasobs'," +
"owl.[AGESEX] AS 'Agesex'," +
"owl.[PAIR] AS 'Pair'," +
"owl.[NEST] AS 'Nest'," +
"owl.[NUMYOUNG] AS 'Numyoung'," +
"owl.[SUBSPECIES] AS 'Subspecies'," +
"owl.[LATDD_N83] AS 'LatDD_N83'," +
"owl.[LONDD_N83] AS 'LonDD_N83'" +
                "FROM [CDFWSpottedOwlWatershedConnection] as 'conn'" +
                "INNER JOIN [Watershed] as 'w' ON w.[WSHD_ID] = conn.[Connection] " +
                "INNER JOIN [CDFW Spotted Owl] as 'owl' ON owl.[OBSID] = conn.[ID] " +
                "WHERE conn.[Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') " +
                "ORDER BY conn.[Connection]", SqlTools.SqlConn))
            { filler.Fill(dt); }


            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "CDFW Spotted Owl Observations (Listed by CPW Name)";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            //WordTableWriterCnddb(sectionRange, dt);
            WordTableWriter(sectionRange, dt);

            dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [FID] FROM [CDFW Spotted Owl] WHERE [OBSID] IN " +
                "(SELECT [ID] FROM [CDFWSpottedOwlWatershedConnection] WHERE [Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "'))", SqlTools.SqlConn))
            { filler.Fill(dt); }
            string fileName = dirName + "\\CDFW Spotted Owl.shp";
            ExportSame(fileName, "CDFW Spotted Owl", FeatureType.Polygon, dt);
        }

        private void WatershedSpiTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
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

            PlantTables(sectionRange, watersheds, adjacentWatersheds, watershedIds, adjacentWatershedIds, dirName);


            //AddPageSection(sectionRange);
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Wildlife (Listed by CPW Name)";
            sectionRange.Paragraphs.Add();


            NOGOTables(sectionRange, watershedIds, adjacentWatershedIds, dirName);
            GGOWTables(sectionRange, watershedIds, adjacentWatershedIds, dirName);
            SPOWTables(sectionRange, watershedIds, adjacentWatershedIds, dirName);
            OtherWildlifeTables(sectionRange, watershedIds, adjacentWatershedIds, dirName);
        }

        private void PlantTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
        {
            DataTable dt = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(SqlTools.SqlConn);
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter(cmd))
            {
                foreach (string wId in watersheds.Union(adjacentWatersheds))
                {
                    cmd.CommandText = "SELECT (SELECT [WSHD_NAME] FROM [Watershed] WHERE [WSHD_ID] = '" + wId + "' LIMIT 1) as 'CPW Name',ps.[SciName] as 'Scientific Name',ps.[ComName] as 'Common Name'," +
                        "ps.[RPLANTRANK] as 'California Rare Plant Rank'," +
                        "ps.[CALLIST] as 'State Status',ps.[FEDLIST] as 'Federal Status',ps.[GRANK] as 'NatureServe Ranking' " +
                        "FROM [PlantSpecies] as 'ps'" +
                        "WHERE ps.[SciName] IN (SELECT [SciName] FROM [PWwild Plant Occurrence] WHERE [ID] IN (SELECT [ID] FROM [PWwildPlantOccurrenceWatershedConnection] WHERE [Connection] = '" + wId + "')) OR " +
                        "ps.[SciName] IN (SELECT [SciName] FROM [Plants of Interest] WHERE [ID] IN (SELECT [ID] FROM [SurveyElementsWatershedConnection] WHERE [Connection] = '" + wId + "')) OR " +
                        "ps.[SciName] IN (SELECT [SciName] FROM [SPI Plant Polygon] WHERE [ID] IN (SELECT [ID] FROM [SPIPlantPolygonWatershedConnection] WHERE [Connection] = '" + wId + "'))" +
                        "GROUP BY ps.[SciName]" +
                        "ORDER BY ps.[SciName]";
                    filler.Fill(dt);
                }
            }

            WordTableWriter(sectionRange, dt);

            dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [FID] FROM [PWwild Plant Occurrence] WHERE [ID] IN " +
                "(SELECT [ID] FROM [PWwildPlantOccurrenceWatershedConnection] WHERE [Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "'))", SqlTools.SqlConn))
            { filler.Fill(dt); }
            string fileName = dirName + "\\PWwild Plant Occurrence.shp";
            ExportSame(fileName, "PWwild Plant Occurrence", FeatureType.Point, dt);
            dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [FID] FROM [SPI Plant Polygon] WHERE [ID] IN " +
                "(SELECT [ID] FROM [SPIPlantPolygonWatershedConnection] WHERE [Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "'))", SqlTools.SqlConn))
            { filler.Fill(dt); }
            fileName = dirName + "\\SPI Plant Polygon.shp";
            ExportSame(fileName, "SPI Plant Polygon", FeatureType.Polygon, dt);
            dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT [FID] FROM [Plants of Interest] WHERE [ID] IN " +
                "(SELECT [ID] FROM [SurveyElementsWatershedConnection] WHERE [Connection] IN ('" + watershedIds + "','" + adjacentWatershedIds + "'))", SqlTools.SqlConn))
            { filler.Fill(dt); }
            fileName = dirName + "\\Plants of Interest.shp";
            ExportSame(fileName, "Survey Elements", FeatureType.Polygon, dt);
        }

        private void NOGOTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
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
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT w.[WSHD_NAME] as 'CPW Name',[Territory] as 'Territory Name',[NestName] as 'NestName',[District ID] as 'District ID'," +
                "[Year] as 'Year','' as 'Map ID',[Longitude] as 'LON (NAD27)'," +
                "[Latitude] as 'LAT (NAD27)',[Territory Status] as 'Status',[Nest] as 'Nest' " +
                "FROM [SPI_NOGO]" +
                "INNER JOIN [Watershed] as 'w' ON w.[WSHD_ID] = [SPI_NOGO].[Watershed Number] " +
                "WHERE [Watershed Number] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') " +
                "ORDER BY [Watershed Number],[District ID],[Year]", SqlTools.SqlConn))
            { filler.Fill(dt); }

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
        private void SPOWTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
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
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT w.[WSHD_NAME] as 'CPW Name',[Subspecies_1] as 'Subspecies',[District ID] as 'District ID'" +
                ",[CDFW ID] as 'CDFW ID',[Territory] as 'Territory',[Year] as 'Year'," +
                "'' as 'Map ID',[Bird Status] as 'Site Status',[Longitude] as 'LON (NAD27)'," +
                "[Latitude] as 'LAT (NAD27)', [HCP Status_2] as 'HCP Status' " +
                "FROM [SPI_SPOW]" +
                 "INNER JOIN [Watershed] as 'w' ON w.[WSHD_ID] = [SPI_SPOW].[Watershed Number] " +
               "WHERE [Watershed Number] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') " +
                "ORDER BY [Watershed Number] ASC,[District ID] ASC,[Year] ASC", SqlTools.SqlConn))
            { filler.Fill(dt); }

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
        private void GGOWTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
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
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT w.[WSHD_NAME] as 'CPW Name',[SPI District] as 'District ID',[Territory] as 'Territory',[Year] as 'Year'," +
                "'' as 'Map ID',[LONG] as 'LON (NAD27)'," +
                "[LAT] as 'LAT (NAD27)', [Results] as 'Status' " +
                "FROM [SPI_GGOW]" +
                 "INNER JOIN [Watershed] as 'w' ON w.[WSHD_ID] = [SPI_GGOW].[WS Num] " +
                "WHERE [WS Num] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') " +
                "ORDER BY [WS Num],[District ID],[Year]", SqlTools.SqlConn))
            { filler.Fill(dt); }

            string fileName = dirName + "\\SPI_GGOW.shp";
            dt = ExportNewShapes(dt, fileName);
            WordTableWriter(sectionRange, dt);
        }
        private void OtherWildlifeTables(Word.Range sectionRange, List<Guid> watersheds, List<Guid> adjacentWatersheds, string dirName)
        {
            //            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Size = 12;
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "Other Wildlife Sighting";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Font.Size = 10;

            DataTable dt = new DataTable();
            using (SQLiteDataAdapter filler = new SQLiteDataAdapter("SELECT w.[WSHD_NAME] as 'CPW Name',[Wildlife Species] as 'Common Name',[Genus] as 'Genus',[Species] as 'Species',[Year] as 'Year'" +
                ",[# Observed] as 'Quantity'," +
                "[Longitude] as 'LON (NAD27)',[Latitude] as 'LAT (NAD27)', '' as 'Map ID', [IUCN rating] as 'NatureServe Ranking' " +
                "FROM [SPI_Wildlife Sightings]" +
                   "INNER JOIN [Watershed] as 'w' ON w.[WSHD_ID] = [SPI_Wildlife Sightings].[WS_CAL22] " +
              "WHERE [WS_CAL22] IN ('" + watershedIds + "','" + adjacentWatershedIds + "') " +
                "ORDER BY [WS_CAL22],[Year]", SqlTools.SqlConn))
            { filler.Fill(dt); }

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
                            WordCellItalicSpecies(wordTbl.Cell(rowCount + 2, c + 1), dt.Rows[i][c].ToString());
                        }
                        else if ((dt.Columns[c].ColumnName == "Genus" || dt.Columns[c].ColumnName == "Species") && i > 0)
                        {
                            wordTbl.Cell(rowCount + 2, colCounter + 1).Range.Text = dt.Rows[i][c].ToString();
                            WordCellItalicSpecies(wordTbl.Cell(rowCount + 2, c + 1), dt.Rows[i][c].ToString());
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
                        WordCellItalicSpecies(wordTbl.Cell(i + 2, c + 1), dt.Rows[i][c].ToString());
                    }
                    else if ((dt.Columns[c].ColumnName == "Genus" || dt.Columns[c].ColumnName == "Species") && i > 0)
                    {
                        wordTbl.Cell(i + 2, c + 1).Range.Text = dt.Rows[i][c].ToString();
                        WordCellItalicSpecies(wordTbl.Cell(i + 2, c + 1), dt.Rows[i][c].ToString());
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

        private void ExportSame(string newFileName, string layerName, FeatureType featureType, DataTable fids)
        {
            var initialLayer = UxMap.FindFeatureLayer(layerName);

            Shapefile newFile = new PolygonShapefile(newFileName);
            newFile.Projection = ProjectionInfo.FromEpsgCode(26710);
            foreach (var c in initialLayer.DataSet.GetColumns())
            {
                newFile.DataTable.Columns.Add(c.Clone());
            }
            newFile.SaveAs(newFileName, true);

            foreach (DataRow r in fids.Rows)
            {
                newFile.Features.Add(initialLayer.DataSet.Features[(int)r[0]]);
            }
            newFile.SaveAs(newFileName, true);
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
                    Atlas.Projections.Reproject.ReprojectPoints(StartXy, new double[] { }, ProjectionInfo.FromEpsgCode(4267), UxMap.Map.Projection, 0, 1);
                    //r["StartLon27"] = StartXy[0];// ((GeoAPI.Geometries.IPoint)gs[0]).X;
                    //r["StartLat27"] = StartXy[1];//((GeoAPI.Geometries.IPoint)gs[0]).Y;

                    r["Map ID"] = featCounter;

                    var f = new Atlas.Data.Feature(new GeoAPI.Geometries.Coordinate(StartXy[0], StartXy[1]));

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

    public class WordHelper
    {
        public int AddIntroDescriptiveText(string field, string values, Word.Document doc, int runningTab, bool first = false)
        {
            if (!first) doc.Paragraphs.Add();
            doc.Paragraphs.Last.Range.Text = field + "  " + values;
            object start = runningTab;
            object filedEnd = runningTab + field.Length + 2;
            object textEnd = runningTab + field.Length + 2 + values.Length;

            doc.Range(ref start, ref filedEnd).Bold = 1;
            doc.Range(ref filedEnd, ref textEnd).Bold = 0;
            //doc.Range(ref start, ref textEnd).Cut();
            //doc.Paragraphs.Last.Range.Paste();
            //doc.Paragraphs.Add();
            return (int)textEnd + 2;
        }
        public int AddIntroDescriptiveTextWithTabs(string field1, string values1, string field2, string values2, Word.Document doc, int runningTab, bool first = false)
        {
            //if (!first) doc.Paragraphs.Add();
            doc.Paragraphs.Last.Range.Text = field1 + "  " + values1 + "\t\t" + field2 + "  " + values2;
            object start = runningTab;
            object filedEnd1 = runningTab + field1.Length + 2;
            object textEnd1 = runningTab + field1.Length + 2 + values1.Length + 2;

            object filedEnd2 = runningTab + field1.Length + 2 + values1.Length + 2 + field2.Length + 2;
            object textEnd2 = runningTab + field1.Length + 2 + values1.Length + 2 + field2.Length + 2 + values2.Length;

            doc.Range(ref start, ref filedEnd1).Bold = 1;
            doc.Range(ref filedEnd1, ref textEnd1).Bold = 0;
            doc.Range(ref textEnd1, ref filedEnd2).Bold = 1;
            doc.Range(ref filedEnd2, ref textEnd2).Bold = 0;
            //doc.Range(ref start, ref textEnd2).Cut();
            //doc.Paragraphs.Last.Range.Paste();
            doc.Paragraphs.Add();
            return (int)textEnd2;
        }

        public void FillCell(Word.Cell c, string field, string txt, Word.Document doc)
        {
            doc.Paragraphs.First.Range.Text = field + "  " + txt;
            object start = 0;
            object filedEnd = field.Length + 2;
            object textEnd = field.Length + 2 + txt.Length;

            doc.Range(ref start, ref filedEnd).Bold = 1;
            doc.Range(ref filedEnd, ref textEnd).Bold = 0;
            doc.Range(ref start, ref textEnd).Cut();
            c.Range.Paste();
        }


        public void FillCell(Word.Cell c, string field, string txt, Word.Range sectionRange)
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
        public void WriteTextField(Word.Range sectionRange, string field, string txt)
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
        public void WriteReferencesField(Word.Range sectionRange, string txt)
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

            //foreach (string re in refs)
            //{
            //    doc.Paragraphs.Add();
            //    if (re == refs[0])doc.Paragraphs.Last.Format.TabHangingIndent(1);
            //    doc.Paragraphs.Last.Range.Font.Bold = 0;
            //    doc.Paragraphs.Last.Range.Text = re;
            //}
        }
        public void WordCellItalicSpecies(Word.Cell cell, string species)
        {
            cell.Range.Italic = 1;
            cell.Range.Text = species;

            //Species with text such as var. or ssp. should have this text not be italicized
            if (species.Contains("."))
            {
                string nuance = species.Split(new char[] { ' ' }).First(_ => _.Contains("."));

                //Microsoft word indexes start at 1 not 0
                int index = species.Split(new char[] { ' ' }).ToList().IndexOf(nuance) + 1;
                var q = cell.Range.Paragraphs.First.Range;
                q.Words[index].Italic = 0;
            }
        }
    }
}
