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
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using WBIS_2.Modules.Tools;
using Microsoft.EntityFrameworkCore;
using DevExpress.Xpf.Editors.Helpers;
using System.Diagnostics;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class SiteCallingCoverageReport : WBISViewModelBase
    {     
        public SiteCallingCoverageReport(Hex160[] queryRecords)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XLSX|*.xlsx";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"Coverage Report {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (File.Exists(sfd.FileName))
            {
                MessageBox.Show("The selected name is already in use");
                goto HERE;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var excel = new Excel.Application { Visible = false };
            var misValue = System.Reflection.Missing.Value;
            var wb = excel.Workbooks.Add(misValue);

            ExportOtherWildlifeDetection(wb, queryRecords.Select(_=>_.Id).ToArray());

            string[] recordTypes = new string[] { "Drop", "Skip", "Follow-Up", "Calling" };

            foreach(string recordType in recordTypes)
            {
                if (new string[] { "Follow-Up", "Calling" }.Contains(recordType))
                    ExportSiteCallingDetection(wb, queryRecords, recordType);
                ExportSiteCalling(wb, queryRecords, recordType);
            }
           
            ExportHex160(wb, queryRecords);
            WriteCoverage(wb, queryRecords);

            wb.Sheets["Sheet1"].Delete();

            wb.SaveAs(sfd.FileName);
            wb.Close(true);
            excel.Quit();

            w.Stop();
            if (MessageBox.Show("Would you like to open your report?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
        }
             


        private void WriteCoverage(Excel.Workbook wb, Hex160[] queryRecords)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Coverage";

            WriteCoverageCounts(sheet, queryRecords.Select(_=>_.Id).ToArray());
            WriteCoverageActivities(sheet, queryRecords);
            WriteCoverageActivitiesCount(sheet, queryRecords);
            WriteCoveragePasses(sheet, queryRecords.Select(_ => _.Id).ToArray());

            sheet.Columns.AutoFit();
        }

        private void WriteCoverageCounts(Excel.Worksheet sheet, Guid[] queryRecords)
        {
            double totalHex = Database.Hex160s.Count();            
            double queryHex = queryRecords.Count();

            var callings = Database.SiteCallings
                .Include(_ => _.Hex160)
                .Where(_ => queryRecords.Contains(_.Hex160.Id) && !_._delete && !_.Repository);

            double totalCalling = Database.SiteCallings.Where(_=>_.RecordType.Contains("Calling") && !_._delete && !_.Repository).Count();
            double queryCalling = callings.Where(_ => _.RecordType.Contains("Calling")).Count();

            double totalFollow = Database.SiteCallings.Where(_ => _.RecordType.Contains("Follow") && !_._delete && !_.Repository).Count();
            double queryfollow = callings.Where(_ => _.RecordType.Contains("Follow")).Count();

            double totalSkip = Database.SiteCallings.Where(_ => _.RecordType.Contains("Skip") && !_._delete && !_.Repository).Count();
            double querySkip = callings.Where(_ => _.RecordType.Contains("Skip")).Count();

            double totalDrop = Database.SiteCallings.Where(_ => _.RecordType.Contains("Drop") && !_._delete && !_.Repository).Count();
            double queryDrop = callings.Where(_ => _.RecordType.Contains("Drop")).Count();


            DataTable dt = new DataTable();
            dt.Columns.Add("Filter", typeof(string));
            dt.Columns.Add("Count", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Percentage", typeof(string));

            DataRow r1 = dt.NewRow();
            r1["Filter"] = "Hex Records";
            r1["Count"] = queryHex.ToString("N0");
            r1["Total"] = totalHex.ToString("N0");
            r1["Percentage"] = (queryHex / totalHex).ToString("P2");
            dt.Rows.Add(r1);

            DataRow r2 = dt.NewRow();
            r2["Filter"] = "Calling Records";
            r2["Count"] = queryCalling.ToString("N0");
            r2["Total"] = totalCalling.ToString("N0");
            r2["Percentage"] = (queryCalling / totalCalling).ToString("P2");
            dt.Rows.Add(r2);

            DataRow r3 = dt.NewRow();
            r3["Filter"] = "Follow-Up Records";
            r3["Count"] = queryfollow.ToString("N0");
            r3["Total"] = totalFollow.ToString("N0");
            r3["Percentage"] = (queryfollow / totalFollow).ToString("P2");
            dt.Rows.Add(r3);

            DataRow r4 = dt.NewRow();
            r4["Filter"] = "Skip Records";
            r4["Count"] = querySkip.ToString("N0");
            r4["Total"] = totalSkip.ToString("N0");
            r4["Percentage"] = (querySkip / totalSkip).ToString("P2");
            dt.Rows.Add(r4);

            DataRow r5 = dt.NewRow();
            r5["Filter"] = "Drop Records";
            r5["Count"] = queryDrop.ToString("N0");
            r5["Total"] = totalDrop.ToString("N0");
            r5["Percentage"] = (queryDrop / totalDrop).ToString("P2");
            dt.Rows.Add(r5);

            ExcelTools.DatatableToSheet(sheet, dt, false, true, Excel.XlRgbColor.rgbLightGrey, true);
        }
        private void WriteCoverageActivities(Excel.Worksheet sheet, Hex160[] queryRecords)
        {          
            double totalHex = queryRecords.Count();
            double callHex = queryRecords.Where(_ => _.RecentActivity.Contains("Calling")).Count();
            double followHex = queryRecords.Where(_ => _.RecentActivity.Contains("Follow")).Count();
            double skipHex = queryRecords.Where(_ => _.RecentActivity.Contains("Skip")).Count();
            double dropHex = queryRecords.Where(_ => _.RecentActivity.Contains("Drop")).Count();
            double unvisitedHex = queryRecords.Where(_ => _.RecentActivity == "Unvisited").Count();

            DataTable dt = new DataTable();
            dt.Columns.Add("Recent Activity", typeof(string));
            dt.Columns.Add("Count", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Percentage", typeof(string));

            DataRow r1 = dt.NewRow();
            r1["Recent Activity"] = "Calling";
            r1["Count"] = callHex.ToString("N0");
            r1["Total"] = totalHex.ToString("N0");
            r1["Percentage"] = (callHex / totalHex).ToString("P2");
            dt.Rows.Add(r1);

            DataRow r2 = dt.NewRow();
            r2["Recent Activity"] = "Follow-Up";
            r2["Count"] = followHex.ToString("N0");
            r2["Total"] = totalHex.ToString("N0");
            r2["Percentage"] = (followHex / totalHex).ToString("P2");
            dt.Rows.Add(r2);

            DataRow r3 = dt.NewRow();
            r3["Recent Activity"] = "Skip";
            r3["Count"] = skipHex.ToString("N0");
            r3["Total"] = totalHex.ToString("N0");
            r3["Percentage"] = (skipHex / totalHex).ToString("P2");
            dt.Rows.Add(r3);

            DataRow r4 = dt.NewRow();
            r4["Recent Activity"] = "Drop";
            r4["Count"] = dropHex.ToString("N0");
            r4["Total"] = totalHex.ToString("N0");
            r4["Percentage"] = (dropHex / totalHex).ToString("P2");
            dt.Rows.Add(r4);

            DataRow r5 = dt.NewRow();
            r5["Recent Activity"] = "Unvisited";
            r5["Count"] = unvisitedHex.ToString("N0");
            r5["Total"] = totalHex.ToString("N0");
            r5["Percentage"] = (unvisitedHex / totalHex).ToString("P2");
            dt.Rows.Add(r5);           

            ExcelTools.DatatableToSheet(sheet, dt, false, true, Excel.XlRgbColor.rgbLightGrey, true, 5);
        }
        private void WriteCoverageActivitiesCount(Excel.Worksheet sheet, Hex160[] queryRecords)
        {
            Guid[] guids = queryRecords.Select(_ => _.Id).ToArray();
            var callings = Database.SiteCallings
                .Include(_ => _.Hex160)
                .Where(_ => guids.Contains(_.Hex160.Id) && !_._delete && !_.Repository);

            double totalCallings = callings.Count();
            double queryCalling = callings.Where(_ => _.RecordType.Contains("Calling")).Count();

            double queryfollow = callings.Where(_ => _.RecordType.Contains("Follow")).Count();

            double querySkip = callings.Where(_ => _.RecordType.Contains("Skip")).Count();

            double queryDrop = callings.Where(_ => _.RecordType.Contains("Drop")).Count();

            double totalHex = queryRecords.Count();
            double unvisitedHex = queryRecords.Where(_ => _.RecentActivity == "Unvisited").Count();

            DataTable dt = new DataTable();
            dt.Columns.Add("Hex Coverage", typeof(string));
            dt.Columns.Add("Count", typeof(string));
            dt.Columns.Add("Percentage", typeof(string));

            DataRow r2 = dt.NewRow();
            r2["Hex Coverage"] = "Calling";
            r2["Count"] = queryCalling.ToString("N0");
            r2["Percentage"] = (queryCalling / totalCallings).ToString("P2");
            dt.Rows.Add(r2);

            DataRow r3 = dt.NewRow();
            r3["Hex Coverage"] = "Follow-Up";
            r3["Count"] = queryfollow.ToString("N0");
            r3["Percentage"] = (queryfollow / totalCallings).ToString("P2");
            dt.Rows.Add(r3);

            DataRow r4 = dt.NewRow();
            r4["Hex Coverage"] = "Skip";
            r4["Count"] = querySkip.ToString("N0");
            r4["Percentage"] = (querySkip / totalCallings).ToString("P2");
            dt.Rows.Add(r4);

            DataRow r5 = dt.NewRow();
            r5["Hex Coverage"] = "Drop";
            r5["Count"] = queryDrop.ToString("N0");
            r5["Percentage"] = (queryDrop / totalCallings).ToString("P2");
            dt.Rows.Add(r5);

            DataRow r1 = dt.NewRow();
            r1["Hex Coverage"] = "Unvisited";
            r1["Count"] = unvisitedHex.ToString("N0");
            r1["Percentage"] = (unvisitedHex / totalHex).ToString("P2");
            dt.Rows.Add(r1);

            ExcelTools.DatatableToSheet(sheet, dt, false, true, Excel.XlRgbColor.rgbLightGrey, true, 10);
        }
        private void WriteCoveragePasses(Excel.Worksheet sheet, Guid[] queryRecords)
        {
            var requiredPasses = Database.Hex160RequiredPasses
                .Include(_ => _.BirdSpecies)
                .Include(_ => _.Hex160)
                .Where(_ => queryRecords.Contains(_.Hex160.Id) && !_._delete && !_.Repository).ToArray();

            var birds = requiredPasses.Select(_=>_.BirdSpecies).Distinct();

            DataTable dt = new DataTable();
            dt.Columns.Add("Target Species", typeof(string));
            dt.Columns.Add("Required", typeof(string));
            dt.Columns.Add("No Pass", typeof(string));
            dt.Columns.Add("Fewer", typeof(string));
            dt.Columns.Add("Match", typeof(string));
            dt.Columns.Add("Greater", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Pcnt", typeof(string));

            foreach (BirdSpecies birdSpecies in birds)
            {
                DataRow r = dt.NewRow();
                r["Target Species"] = birdSpecies.Species;
                r["Required"] = requiredPasses.First(_=>_.BirdSpecies == birdSpecies).RequiredPasses;

                double noPass = requiredPasses.Where(_ => _.BirdSpecies == birdSpecies && _.CurrentPasses == 0).Count();
                double fewer = requiredPasses.Where(_ => _.BirdSpecies == birdSpecies && _.RequiredPasses > _.CurrentPasses && _.CurrentPasses > 0).Count();
                double match = requiredPasses.Where(_ => _.BirdSpecies == birdSpecies && _.RequiredPasses == _.CurrentPasses).Count();
                double greater = requiredPasses.Where(_ => _.BirdSpecies == birdSpecies && _.RequiredPasses < _.CurrentPasses).Count();
                double total = requiredPasses.Where(_ => _.BirdSpecies == birdSpecies).Count();


                r["No Pass"] = (noPass / total).ToString("P2");
                r["Fewer"] = (fewer / total).ToString("P2");
                r["Match"] = (match / total).ToString("P2");
                r["Greater"] = (greater / total).ToString("P2");

                double total2 = noPass + fewer + match + greater;
                r["Total"] = total2.ToString("N2");
                r["Pcnt"] = (total2 / total).ToString("P2");
                dt.Rows.Add(r);
            }

            ExcelTools.DatatableToSheet(sheet, dt, false, true, Excel.XlRgbColor.rgbLightGrey, true, 14);
        }



        private void ExportHex160(Excel.Workbook wb, Hex160[] queryRecords)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Hex160";

            DataTable hexDt = ExcelTools.EntityToDatatable(new InformationTypeManager<Hex160>(), queryRecords.AsQueryable());
            ExcelTools.DatatableToSheet(sheet, hexDt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
        }

        private void ExportSiteCalling(Excel.Workbook wb, Hex160[] queryRecords, string recordType)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = $"{recordType}";

            var records = new InformationTypeManager<SiteCalling>().GetQueryable(queryRecords, typeof(Hex160), Database, includeGeometry: true, showDelete: false, showRepository: false)
                .Cast<SiteCalling>().Where(_ => _.RecordType.Contains(recordType));
            DataTable dt = ExcelTools.EntityToDatatable(new InformationTypeManager<SiteCalling>(), records);
            ExcelTools.DatatableToSheet(sheet, dt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
        }
        private void ExportSiteCallingDetection(Excel.Workbook wb, Hex160[] queryRecords, string recordType)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = $"{recordType} Detection";

            var records = new InformationTypeManager<SiteCallingDetection>().GetQueryable(queryRecords, typeof(Hex160), Database, includeGeometry: true, showDelete: false, showRepository: false)
                .Cast<SiteCallingDetection>().Where(_=>_.SiteCalling.RecordType.Contains(recordType));
            DataTable dt = ExcelTools.EntityToDatatable(new InformationTypeManager<SiteCallingDetection>(), records);
            ExcelTools.DatatableToSheet(sheet, dt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
        }
        private void ExportOtherWildlifeDetection(Excel.Workbook wb, Guid[] queryRecords)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Other Wildlife";

            var records = Database.OtherWildlifeRecords
                .Include(_ => _.WildlifeSpecies)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.User)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.Hex160)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.District)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.Watershed)
                .Where(_ => !_._delete && !_.SiteCalling._delete && !_.SiteCalling.Repository && queryRecords.Contains(_.SiteCalling.Hex160.Id));

            DataTable dt = new DataTable();
            dt.Columns.Add("SpCode");
            dt.Columns.Add("SpName");
            dt.Columns.Add("Class");
            dt.Columns.Add("Date");
            dt.Columns.Add("Time");
            dt.Columns.Add("Detection");
            dt.Columns.Add("Number");
            dt.Columns.Add("Lat", typeof(double));
            dt.Columns.Add("Lon", typeof(double));
            dt.Columns.Add("Datum");
            dt.Columns.Add("surveyor");
            dt.Columns.Add("District");
            dt.Columns.Add("Wshd ID");

            foreach(OtherWildlife record in records)
            {
                DataRow r = dt.NewRow();
                r["SpCode"] = record.WildlifeSpecies.AlphaCode;
                r["SpName"] = record.WildlifeSpecies.Species;
                r["Class"] = record.WildlifeSpecies.Class;
                r["Date"] = record.DateTime.ToShortDateString();
                r["Time"] = record.DateTime.ToShortTimeString();
                r["Detection"] = record.Detection;
                r["Number"] = record.Number;
                r["Lat"] = record.SiteCalling.Lat;
                r["Lon"] = record.SiteCalling.Lon;
                r["Datum"] = record.SiteCalling.Datum;
                r["surveyor"] = record.SiteCalling.User.UserName;
                r["District"] = record.SiteCalling.District.DistrictName;
                r["Wshd ID"] = record.SiteCalling.Watershed.WatershedID;
                dt.Rows.Add(r);
            }

            ExcelTools.DatatableToSheet(sheet, dt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
        }


      


        private object[] ColumnsToItemArray(DataColumnCollection columns)
        {
            var itemArray = new object[columns.Count];
            for (int i = 0; i < columns.Count; i++)
            {
                itemArray[i] = columns[i].ColumnName;
            }
            return itemArray;
        }
        private void ExportSurvey(Excel.Workbook wb, BotanicalSurveyArea[] areaRecords, string path, bool includeShapes)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Surveys";

            IInfoTypeManager manager = new InformationTypeManager<BotanicalSurvey>();
            var records = manager.GetQueryable(areaRecords, typeof(BotanicalSurveyArea), Database, includeGeometry: true, showDelete: false, showRepository: false);

            DataTable dt = ExcelTools.EntityToDatatable(manager, records);
            ExcelTools.WriteBotanyTable(dt, sheet);
            if (includeShapes) new PostGisShapefileConverter(typeof(BotanicalSurvey), records, path + "\\Surveys.shp");
        }
        private void ExportPlantsOfInterest(Excel.Workbook wb, Guid[] areaRecords, string path, bool includeShapes)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Plants of Interest";

            var records = Database.BotanicalPlantsOfInterest
                .Include(_=>_.PlantSpecies)
                .Include(_=>_.BotanicalElement).ThenInclude(_=>_.BotanicalSurveyArea).ThenInclude(_=>_.THP_Area)
                .Include(_=>_.BotanicalElement).ThenInclude(_=>_.BotanicalSurvey).ThenInclude(_=>_.THP_Area)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.User)
                .Include(_=>_.AssociatedPlants).ThenInclude(_=>_.PlantSpecies)
                .Where (_=> !_.BotanicalElement._delete && !_.BotanicalElement.Repository && areaRecords.Contains(_.BotanicalElement.BotanicalSurveyArea.Id))
                .OrderBy(_=>_.DateTime);

            DataTable dt = new DataTable();
            dt.Columns.Add("Area Name");
            dt.Columns.Add("THP Area");
            dt.Columns.Add("SciName");
            dt.Columns.Add("ComName");
            dt.Columns.Add("Family");
            dt.Columns.Add("Tentative Identification");
            dt.Columns.Add("Species Found");
            dt.Columns.Add("Species Found Txt");
            dt.Columns.Add("Subsequent Visit");
            dt.Columns.Add("Num Individuals");
            dt.Columns.Add("Num Individuals Max");
            dt.Columns.Add("Existing NDDB");
            dt.Columns.Add("Occ#");
            dt.Columns.Add("Surveyor");
            dt.Columns.Add("DateTime", typeof(DateTime));
            dt.Columns.Add("Lat", typeof(double));
            dt.Columns.Add("Lon", typeof(double));
            dt.Columns.Add("Datum");
            dt.Columns.Add("Radius", typeof(double));
            dt.Columns.Add("Site Quality");
            dt.Columns.Add("Land Use");
            dt.Columns.Add("Vegetative", typeof(double));
            dt.Columns.Add("Flowering", typeof(double));
            dt.Columns.Add("Fruiting", typeof(double));
            dt.Columns.Add("Disturbances");
            dt.Columns.Add("Threats");
            dt.Columns.Add("Habitat Description");
            dt.Columns.Add("Comments");
            dt.Columns.Add("Associated Plants");

            foreach(BotanicalPlantOfInterest record in records)
            {
                DataRow r = dt.NewRow();
                r["Area Name"] = record.BotanicalElement.BotanicalSurveyArea.AreaName;
                r["THP Area"] = record.BotanicalElement.BotanicalSurveyArea.THP_Area.THPName;
                r["SciName"] = record.PlantSpecies.SciName;
                r["ComName"] = record.PlantSpecies.ComName;
                r["Family"] = record.PlantSpecies.Family;
                r["Tentative Identification"] = record.TentativeIdentification;
                r["Species Found"] = record.SpeciesFound;
                r["Species Found Txt"] = record.SpeciesFoundText;
                r["Subsequent Visit"] = record.SubsequentVisit;
                r["Num Individuals"] = record.NumInd;
                r["Num Individuals Max"] = record.NumIndMax;
                r["Existing NDDB"] = record.ExistingCNDDB;
                r["Occ#"] = record.OccNum;
                r["Surveyor"] = record.BotanicalElement.User.UserName;
                r["DateTime"] = record.DateTime;
                r["Lat"] = record.BotanicalElement.Lat;
                r["Lon"] = record.BotanicalElement.Lon;
                r["Datum"] = record.BotanicalElement.Datum;
                r["Radius"] = record.Radius;
                r["Site Quality"] = record.SiteQuality;
                r["Land Use"] = record.LandUse;
                r["Vegetative"] = record.Vegetative;
                r["Flowering"] = record.Flowering;
                r["Fruiting"] = record.Fruiting;
                r["Disturbances"] = record.Disturbances;
                r["Threats"] = record.Threats;
                r["Habitat Description"] = record.Habitat;
                r["Comments"] = record.Comments;
                r["Associated Plants"] = string.Join(", ", record.AssociatedPlants.Select(_ => _.PlantSpecies.SciName));
                dt.Rows.Add(r);
            }

            ExcelTools.WriteBotanyTable(dt, sheet);
            if (includeShapes) new PostGisShapefileConverter(typeof(BotanicalPlantOfInterest), records, path + "\\Plants of Interest.shp");
        }
        private void ExportPointsOfInterest(Excel.Workbook wb, Guid[] areaRecords, string path, bool includeShapes)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Points of Interest";

            var records = Database.BotanicalPointsOfInterest
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurvey).ThenInclude(_ => _.THP_Area)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.User)
                .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && areaRecords.Contains(_.BotanicalElement.BotanicalSurveyArea.Id))
                .OrderBy(_ => _.DateTime);

            DataTable dt = new DataTable();
            dt.Columns.Add("Area Name");
            dt.Columns.Add("THP Area");
            dt.Columns.Add("Record Type");
            dt.Columns.Add("Surveyor");
            dt.Columns.Add("DateTime", typeof(DateTime));
            dt.Columns.Add("Lat", typeof(double));
            dt.Columns.Add("Lon", typeof(double));
            dt.Columns.Add("Datum");
            dt.Columns.Add("Radius", typeof(double));
            dt.Columns.Add("Rechecks Needed");
            dt.Columns.Add("Recheck");
            dt.Columns.Add("Stream");
            dt.Columns.Add("Herbaceous Vegetation");
            dt.Columns.Add("Inundated");
            dt.Columns.Add("Woody Vegetation");
            dt.Columns.Add("Isolated");
            dt.Columns.Add("Littoral Zone");
            dt.Columns.Add("Stream Substrate");
            dt.Columns.Add("Stream Gradient");
            dt.Columns.Add("Comments");
                     

            foreach (BotanicalPointOfInterest record in records)
            {
                DataRow r = dt.NewRow();
                r["Area Name"] = record.BotanicalElement.BotanicalSurveyArea.AreaName;
                r["THP Area"] = record.BotanicalElement.BotanicalSurveyArea.THP_Area.THPName;
                r["Record Type"] = record.RecordType;
                r["Surveyor"] = record.BotanicalElement.User.UserName;
                r["DateTime"] = record.DateTime;
                r["Lat"] = record.BotanicalElement.Lat;
                r["Lon"] = record.BotanicalElement.Lon;
                r["Datum"] = record.BotanicalElement.Datum;
                r["Radius"] = record.Radius;
                r["Rechecks Needed"] = record.RechecksNeeded;
                r["Recheck"] = record.Recheck;
                r["Stream"] = record.Instream;
                r["Herbaceous Vegetation"] = record.HerbaceousVegetation;
                r["Inundated"] = record.Inundated;
                r["Woody Vegetation"] = record.WoodyVegetation;
                r["Isolated"] = record.Isolated;
                r["Littoral Zone"] = record.LittoralZone;
                r["Stream Substrate"] = record.Substrate;
                r["Stream Gradient"] = record.Gradient;
                r["Comments"] = record.Comments;
                dt.Rows.Add(r);
            }

            ExcelTools.WriteBotanyTable(dt, sheet);
            if (includeShapes) new PostGisShapefileConverter(typeof(BotanicalPointOfInterest), records, path + "\\Points of Interest.shp");
        }
        private void ExportPlantsList(Excel.Workbook wb, Guid[] areaRecords, string path)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Plants List";

            var records = Database.BotanicalPlantsList
                .Include(_ => _.PlantSpecies)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurvey).ThenInclude(_ => _.THP_Area)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.User)
                .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && areaRecords.Contains(_.BotanicalElement.BotanicalSurveyArea.Id))
                .OrderBy(_ => _.DateTime);

            DataTable dt = new DataTable();
            dt.Columns.Add("Area Name");
            dt.Columns.Add("THP Area");
            dt.Columns.Add("SciName");
            dt.Columns.Add("ComName");
            dt.Columns.Add("Family");
            dt.Columns.Add("DateTime", typeof(DateTime));

            foreach (BotanicalPlantList record in records)
            {
                DataRow r = dt.NewRow();
                r["Area Name"] = record.BotanicalElement.BotanicalSurveyArea.AreaName;
                r["THP Area"] = record.BotanicalElement.BotanicalSurveyArea.THP_Area.THPName;
                r["SciName"] = record.PlantSpecies.SciName;
                r["ComName"] = record.PlantSpecies.ComName;
                r["Family"] = record.PlantSpecies.Family;               
                r["DateTime"] = record.DateTime;
                dt.Rows.Add(r);
            }

            ExcelTools.WriteBotanyTable(dt, sheet);
        }



        private int WriteBotanyTableHierarchy(object[] vals, DataColumnCollection columns, Excel.Worksheet sheet, bool headerRow, Excel.XlRgbColor rowColor, int indent, int currentRow)
        {
            object[,] printArray = new object[1, columns.Count];

            for (int c = 0; c < columns.Count; c++)
            {
                if (headerRow)
                {
                    printArray[0, c] = vals[c].ToString();
                    continue;
                }

                if (vals[c] is DBNull)
                {
                    printArray[0, c] = "";
                    continue;
                }

                if (columns[c].ColumnName == "Lat" || columns[c].ColumnName == "Lon")
                {
                    printArray[0, c] = ((double)vals[c]).ToString("N5");
                }
                else if (columns[c].DataType == typeof(DateTime))
                {
                    printArray[0, c] = ((DateTime)vals[c]).ToShortDateString() + " " + ((DateTime)vals[c]).ToShortTimeString();
                }
                else if (columns[c].DataType == typeof(double))
                {
                    printArray[0, c] = ((double)vals[c]).ToString("N2");
                }
                else
                {
                    printArray[0, c] = vals[c].ToString();
                }
            }

            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(columns.Count - 1) + (currentRow + 1)).Value2 = printArray;
            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(columns.Count - 1) + (currentRow + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(columns.Count - 1) + (currentRow + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;

            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(columns.Count - 1) + (currentRow + 1)).Interior.Color = rowColor;

            return currentRow + 1;
        }
        private int WriteBotanyTableHierarchy(DataTable dt, Excel.Worksheet sheet, Excel.XlRgbColor rowColor, int indent, int currentRow)
        {
            object[,] printArray = new object[dt.Rows.Count + 1, dt.Columns.Count];
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                printArray[0, c] = dt.Columns[c].ColumnName;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Rows[i][c] is DBNull)
                    {
                        printArray[i + 1, c] = "";
                        continue;
                    }

                    if (dt.Columns[c].ColumnName == "Lat" || dt.Columns[c].ColumnName == "Lon")
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N5");
                    }
                    else if (dt.Columns[c].DataType == typeof(DateTime))
                    {
                        printArray[i + 1, c] = ((DateTime)dt.Rows[i][c]).ToShortDateString() + " " + ((DateTime)dt.Rows[i][c]).ToShortTimeString();
                    }
                    else if (dt.Columns[c].DataType == typeof(double))
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N2");
                    }
                    else
                    {
                        printArray[i + 1, c] = dt.Rows[i][c].ToString();
                    }
                }
            }


            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (currentRow + dt.Rows.Count + 1)).Value2 = printArray;
            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (currentRow + dt.Rows.Count + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (currentRow + 1)).Interior.Color = rowColor;
            sheet.get_Range(ExcelTools.NumToLetter(indent) + (currentRow + 1) + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (currentRow + dt.Rows.Count + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;

            return currentRow + dt.Rows.Count + 1;
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
