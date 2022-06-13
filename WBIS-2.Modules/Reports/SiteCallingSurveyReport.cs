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
    public class SiteCallingSurveyReport : WBISViewModelBase
    {     
        public SiteCallingSurveyReport(Hex160[] queryRecords)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"Survey Report {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (Directory.Exists(sfd.FileName) || File.Exists(sfd.FileName + ".zip"))
            {
                MessageBox.Show("The selected name is already in use");
                goto HERE;
            }

            bool includeShapes = MessageBox.Show("Include shapefiles?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var excel = new Excel.Application { Visible = false };
            var misValue = System.Reflection.Missing.Value;
            var wb = excel.Workbooks.Add(misValue);

            ExportOtherWildlifeDetection(wb, queryRecords.Select(_=>_.Guid).ToArray(), sfd.FileName);
            ExportSiteCallingDetection(wb, queryRecords, sfd.FileName, includeShapes);
            ExportSiteCalling(wb, queryRecords, sfd.FileName, includeShapes);
            ExportHex160(wb, queryRecords, sfd.FileName);

            wb.Sheets["Sheet1"].Delete();

            wb.SaveAs(sfd.FileName + "\\Survey Report.xlsx");
            wb.Close(true);
            excel.Quit();

            ZipFile.CreateFromDirectory(sfd.FileName, sfd.FileName + ".zip");
            Directory.Delete(sfd.FileName, true);

            w.Stop();
            System.Windows.MessageBox.Show("The survey report has finished");
        }
             
                         
        private void ExportHex160(Excel.Workbook wb, Hex160[] queryRecords, string path)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Hex160";

            DataTable hexDt = ExcelTools.EntityToDatatable(new InformationTypeManager<Hex160>(), queryRecords.AsQueryable());
            ExcelTools.DatatableToSheet(sheet, hexDt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
        }

        private void ExportSiteCalling(Excel.Workbook wb, Hex160[] queryRecords, string path, bool includeShapes)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Site Calling";

            var records = new InformationTypeManager<SiteCalling>().GetQueryable(queryRecords, typeof(Hex160), Database, includeGeometry: true, showDelete: false, showRepository: false);
            DataTable dt = ExcelTools.EntityToDatatable(new InformationTypeManager<SiteCalling>(), records);
            ExcelTools.DatatableToSheet(sheet, dt, true, true, Excel.XlRgbColor.rgbLightGrey, true);

            string id = ExcelTools.NumToLetter(dt.Columns["Guid"].Ordinal);
            sheet.get_Range(id + ":" + id).Interior.Color = Excel.XlRgbColor.rgbYellow;           
            string pzPass = ExcelTools.NumToLetter(dt.Columns["PZPassNumber"].Ordinal);
            sheet.get_Range(pzPass + ":" + pzPass).Interior.Color = Excel.XlRgbColor.rgbYellow;
            string pass = ExcelTools.NumToLetter(dt.Columns["PassNumber"].Ordinal);
            sheet.get_Range(pass + ":" + pass).Interior.Color = Excel.XlRgbColor.rgbYellow;

            if (includeShapes) new PostGisShapefileConverter(typeof(SiteCalling), records, path + "\\SiteCalling.shp");
        }
        private void ExportSiteCallingDetection(Excel.Workbook wb, Hex160[] queryRecords, string path, bool includeShapes)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Site Calling Detection";

            var records = new InformationTypeManager<SiteCallingDetection>().GetQueryable(queryRecords, typeof(Hex160), Database, includeGeometry: true, showDelete: false, showRepository: false);
            DataTable dt = ExcelTools.EntityToDatatable(new InformationTypeManager<SiteCallingDetection>(), records);
            ExcelTools.DatatableToSheet(sheet, dt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
            if (includeShapes) new PostGisShapefileConverter(typeof(SiteCallingDetection), records, path + "\\SiteCallingDetection.shp");
        }
        private void ExportOtherWildlifeDetection(Excel.Workbook wb, Guid[] queryRecords, string path)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Other Wildlife";

            var records = Database.OtherWildlifeRecords
                .Include(_ => _.WildlifeSpecies)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.User)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.Hex160)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.District)
                .Include(_ => _.SiteCalling).ThenInclude(_ => _.Watershed)
                .Where(_ => !_._delete && !_.SiteCalling._delete && !_.SiteCalling.Repository && queryRecords.Contains(_.SiteCalling.Hex160.Guid));

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


        private void BuildHierarchy(Excel.Workbook wb, IQueryable records)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Botanical Efforts";
            int currentRow = 0;

            foreach(BotanicalSurveyArea area in records)
            {
                DataTable areaDt = SurveyAreaHierarchyRows(area);
                if (currentRow == 0) currentRow = WriteBotanyTableHierarchy(ColumnsToItemArray(areaDt.Columns), areaDt.Columns, sheet, true, Excel.XlRgbColor.rgbWhite, 0, currentRow);
                currentRow = WriteBotanyTableHierarchy(areaDt.Rows[0].ItemArray, areaDt.Columns, sheet, false, Excel.XlRgbColor.rgbYellow, 0, currentRow);

                bool surveyAdded = false;
                foreach(BotanicalSurvey survey in area.BotanicalSurvey)
                {
                    DataTable surveyDt = SurveyHierarchyRows(survey);
                    if (!surveyAdded)
                    {
                        currentRow = WriteBotanyTableHierarchy(ColumnsToItemArray(surveyDt.Columns), surveyDt.Columns, sheet, true, Excel.XlRgbColor.rgbWhite, 1, currentRow);
                        surveyAdded = true;
                    }
                    currentRow = WriteBotanyTableHierarchy(surveyDt.Rows[0].ItemArray, surveyDt.Columns, sheet, false, Excel.XlRgbColor.rgbLightGreen, 1, currentRow);

                    var plantsOfInterest = survey.BotanicalElement.Where(_ => _.BotanicalPlantOfInterest != null).Select(_=>_.BotanicalPlantOfInterest);
                    var plantsOfInterestDt = PlantOfInterestHierarchyRows(plantsOfInterest.AsQueryable());
                    var pointsOfInterest = survey.BotanicalElement.Where(_ => _.BotanicalPointOfInterest != null).Select(_ => _.BotanicalPointOfInterest);
                    var pointsOfInterestDt = PointOfInterestHierarchyRows(pointsOfInterest.AsQueryable());
                    var plantsList = survey.BotanicalElement.Where(_ => _.BotanicalPlantList != null).Select(_ => _.BotanicalPlantList);
                    var plantsListDt = PlantListHierarchyRows(plantsList.AsQueryable());

                    currentRow = WriteBotanyTableHierarchy(plantsOfInterestDt, sheet, Excel.XlRgbColor.rgbLightBlue, 2, currentRow);
                    currentRow = WriteBotanyTableHierarchy(pointsOfInterestDt, sheet, Excel.XlRgbColor.rgbLightSalmon, 2, currentRow);
                    currentRow = WriteBotanyTableHierarchy(plantsListDt, sheet, Excel.XlRgbColor.rgbLightGray, 2, currentRow);
                }
            }

            sheet.Rows.RowHeight = 15;
            sheet.Columns.AutoFit();
        }

        private DataTable SurveyAreaHierarchyRows(BotanicalSurveyArea record)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("THP Area");
            dt.Columns.Add("Area Name");
            dt.Columns.Add("Survey Type");
            dt.Columns.Add("Aspect");
            dt.Columns.Add("Slope");
            dt.Columns.Add("Canopy");
            dt.Columns.Add("Rock Outcrops");
            dt.Columns.Add("Boulders");
            dt.Columns.Add("Substrate");
            dt.Columns.Add("Understory Vegetation");
            dt.Columns.Add("General Habitat");
            dt.Columns.Add("Talus/Scree");
            dt.Columns.Add("Lava Cap");
            dt.Columns.Add("Spring/Seep");
            dt.Columns.Add("Pond");


            DataRow r = dt.NewRow();
                r["Area Name"] = record.AreaName;
                r["THP Area"] = record.THP_Area.THPName;
            r["Survey Type"] = record.SurveyType;
            r["Aspect"] = record.Aspect;
            r["Slope"] = record.Slope;
            r["Canopy"] = record.Canopy;
            r["Rock Outcrops"] = record.RockOutcrops;
            r["Boulders"] = record.Boulders;
            r["Substrate"] = record.Substrate;
            r["Understory Vegetation"] = record.UnderstoryVegetation;
            r["General Habitat"] = record.GeneralHabitat;
            r["Talus/Scree"] = record.TalusScree;
            r["Lava Cap"] = record.LavaCap;
            r["Spring/Seep"] = record.SpringSeep;
            r["Pond"] = record.Pond;
            dt.Rows.Add(r);
            return dt;
        }
        private DataTable SurveyHierarchyRows(BotanicalSurvey record)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Survey Type");
            dt.Columns.Add("Surveyor");
            dt.Columns.Add("Other Surveyors");
            dt.Columns.Add("Start Time", typeof(DateTime));
            dt.Columns.Add("End Time", typeof(DateTime));
            dt.Columns.Add("Time Spent");

            DataRow r = dt.NewRow();
            r["Survey Type"] = record.SurveyType;
            r["Surveyor"] = record.User.UserName;
            r["Other Surveyors"] = record.OtherSurveyors;
            r["Start Time"] = record.StartTime;
            r["End Time"] = record.EndTime;
            r["Time Spent"] = $"{record.TimeSpent.Hours}:{record.TimeSpent.Minutes}";
            dt.Rows.Add(r);
            return dt;
        }
        private DataTable PlantOfInterestHierarchyRows(IQueryable records)
        {
            DataTable dt = new DataTable();
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

            foreach (BotanicalPlantOfInterest record in records)
            {
                DataRow r = dt.NewRow();
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
                dt.Rows.Add(r);
            }
            return dt;
        }
        private DataTable PointOfInterestHierarchyRows(IQueryable records)
        {
            DataTable dt = new DataTable();
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


            foreach (BotanicalPointOfInterest record in records)
            {
                DataRow r = dt.NewRow();
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
                dt.Rows.Add(r);
            }
            return dt;
        }
        private DataTable PlantListHierarchyRows(IQueryable records)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SciName");
            dt.Columns.Add("ComName");
            dt.Columns.Add("Family");
            dt.Columns.Add("DateTime", typeof(DateTime));

            foreach (BotanicalPlantList record in records)
            {
                DataRow r = dt.NewRow();
                r["SciName"] = record.PlantSpecies.SciName;
                r["ComName"] = record.PlantSpecies.ComName;
                r["Family"] = record.PlantSpecies.Family;
                r["DateTime"] = record.DateTime;
                dt.Rows.Add(r);
            }
            return dt;
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
                .Where (_=> !_.BotanicalElement._delete && !_.BotanicalElement.Repository && areaRecords.Contains(_.BotanicalElement.BotanicalSurveyArea.Guid))
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
                .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && areaRecords.Contains(_.BotanicalElement.BotanicalSurveyArea.Guid))
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
                .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && areaRecords.Contains(_.BotanicalElement.BotanicalSurveyArea.Guid))
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
