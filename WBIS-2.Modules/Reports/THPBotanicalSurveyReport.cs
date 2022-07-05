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
    public class THPBotanicalSurveyReport : WBISViewModelBase
    {
     
        public THPBotanicalSurveyReport(THP_Area tHP_Area)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"THP Botanical Survey Report {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (Directory.Exists(sfd.FileName) || File.Exists(sfd.FileName + ".zip"))
            {
                MessageBox.Show("The selected name is already in use");
                goto HERE;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var excel = new Excel.Application { Visible = false };
            var misValue = System.Reflection.Missing.Value;
            var wb = excel.Workbooks.Add(misValue);

            //Write excel sheets
            ExportSurveyArea(wb, tHP_Area);
            ExportBotanicalSurvey(tHP_Area, sfd.FileName);
            ExportPlantsOfInterest(wb, tHP_Area, sfd.FileName);
            ExportThpBotanicalSurvey(wb, tHP_Area);

            wb.Sheets["Sheet1"].Delete();

            wb.SaveAs(sfd.FileName + "\\THP Botanical Survey Report.xlsx");
            wb.Close(true);
            excel.Quit();

            ZipFile.CreateFromDirectory(sfd.FileName, sfd.FileName + ".zip");
            Directory.Delete(sfd.FileName, true);

            w.Stop();
            if (MessageBox.Show("Would you like to open your report?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
        }
             
        private void ExportThpBotanicalSurvey(Excel.Workbook wb, THP_Area tHP_Area)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Survey Summary";

            sheet.get_Range("A1:A1").Value2 = "THP Area:";
            sheet.get_Range("B1:B1").Value2 = tHP_Area.THPName;
            sheet.get_Range("C1:C1").Value2 = "Total Hours";

            var hours = Database.BotanicalSurveys
                .Include(_=>_.THP_Area)
                .Where(_=>!_._delete && !_.Repository && _.THP_Area.Id == tHP_Area.Id).Sum(_=>(double)_.TimeSpent.Hours + ((double)_.TimeSpent.Minutes/60d));
                      
            if (hours is DBNull) hours = 0d;
            sheet.get_Range("D1:D1").Value2 = ((double)hours).ToString("N2");
            sheet.get_Range("A1:D1").Font.Bold = true;


            DataTable SurveyDt = new DataTable();
            SurveyDt.Columns.Add("SurveyAreaName");
            SurveyDt.Columns.Add("Survey Type");
            SurveyDt.Columns.Add("Surveyor");
            SurveyDt.Columns.Add("Other Surveyors");
            SurveyDt.Columns.Add("Start Time", typeof(DateTime));
            SurveyDt.Columns.Add("Time Spent");

            var surveys = Database.BotanicalSurveys
                .Include(_ => _.BotanicalSurveyArea)
                .Include(_ => _.User)
                .Include(_ => _.THP_Area)
                .Where(_ => !_._delete && !_.Repository && _.THP_Area.Id == tHP_Area.Id);
            foreach(BotanicalSurvey survey in surveys)
            {
                DataRow r = SurveyDt.NewRow();
                r["SurveyAreaName"] = survey.BotanicalSurveyArea.AreaName;
                r["Survey Type"] = survey.SurveyType;
                r["Surveyor"] = survey.User.UserName;
                r["Other Surveyors"] = survey.OtherSurveyors;
                r["Start Time"] = survey.StartTime;
                r["Time Spent"] = $"{survey.TimeSpent.Hours}:{survey.TimeSpent.Minutes}";
                SurveyDt.Rows.Add(r);
            }
            ExcelTools.WriteBotanyThpReportTable(SurveyDt, sheet, 2, true);


            DataTable dt = new DataTable();
            dt.Columns.Add("Scientific Name");
            dt.Columns.Add("Common Name");
            dt.Columns.Add("Family");

            var plants1 = Database.BotanicalPlantsOfInterest
                 .Include(_ => _.PlantSpecies)
                 .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                 .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && _.BotanicalElement.BotanicalSurveyArea.THP_Area.Id == tHP_Area.Id)
                 .Select(_ => _.PlantSpecies);
            var plants2 = Database.BotanicalPlantsList
                 .Include(_ => _.PlantSpecies)
                 .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                 .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && _.BotanicalElement.BotanicalSurveyArea.THP_Area.Id == tHP_Area.Id)
                 .Select(_ => _.PlantSpecies);
            var plants = plants1.Append(plants2).Distinct().OrderBy(_ => _.SciName);

            foreach (PlantSpecies plant in plants)
            {
                DataRow r = dt.NewRow();
                r["Scientific Name"] = plant.SciName;
                r["Common Name"] = plant.ComName;
                r["Family"] = plant.Family;
                dt.Rows.Add(r);
            }
            ExcelTools.WriteBotanyThpReportTable(dt, sheet, SurveyDt.Rows.Count + 4, false);

            sheet.Rows.RowHeight = 15;
            sheet.Columns.AutoFit();
        }






        private void ExportSurveyArea(Excel.Workbook wb, THP_Area tHP_Area)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Survey Area";

            IInfoTypeManager manager = new InformationTypeManager<BotanicalSurveyArea>();
            var records = manager.GetQueryable(new object[] {tHP_Area}, typeof(THP_Area), Database, includeGeometry: true, showDelete: false, showRepository: false);

            DataTable dt = ExcelTools.EntityToDatatable(manager, records);
            ExcelTools.WriteBotanyTable(dt, sheet);
        }
        private void ExportBotanicalSurvey(THP_Area tHP_Area, string path)
        {
            IInfoTypeManager manager = new InformationTypeManager<BotanicalSurvey>();
            var records = manager.GetQueryable(new object[] { tHP_Area }, typeof(THP_Area), Database, includeGeometry: true, showDelete: false, showRepository: false);

            new PostGisShapefileConverter(manager.InformationType, records, path + "\\Botanical Survey.shp");
        }
        private void ExportPlantsOfInterest(Excel.Workbook wb, THP_Area tHP_Area, string path)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Plants of Interest";

            var records = Database.BotanicalPlantsOfInterest
                .Include(_=>_.PlantSpecies)
                .Include(_=>_.BotanicalElement).ThenInclude(_=>_.BotanicalSurveyArea).ThenInclude(_=>_.THP_Area)
                .Include(_=>_.BotanicalElement).ThenInclude(_=>_.BotanicalSurvey).ThenInclude(_=>_.THP_Area)
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.User)
                .Include(_=>_.AssociatedPlants).ThenInclude(_=>_.PlantSpecies)
                .Where (_=> !_.BotanicalElement._delete && !_.BotanicalElement.Repository && _.BotanicalElement.BotanicalSurveyArea.THP_Area.Id == tHP_Area.Id)
                .OrderBy(_=>_.PlantSpecies.SciName);

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
            new PostGisShapefileConverter(typeof(BotanicalPlantOfInterest), records, path + "\\Plants of Interest.shp");
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
