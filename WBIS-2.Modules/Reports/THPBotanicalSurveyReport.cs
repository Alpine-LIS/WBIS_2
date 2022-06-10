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

            w.Stop();
            System.Windows.MessageBox.Show("The THP survey report has finished");
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
                .Where(_=>!_._delete && !_.Repository && _.THP_Area.Guid == tHP_Area.Guid).Sum(_=>(double)_.TimeSpent.Hours + ((double)_.TimeSpent.Minutes/60d));
                      
            if (hours is DBNull) hours = 0d;
            sheet.get_Range("D1:D1").Value2 = ((double)hours).ToString("N2");
            sheet.get_Range("A1:D1").Font.Bold = true;


            DataTable SurveyDt = new DataTable();
            SurveyDt.Columns.Add("SurveyAreaName");
            SurveyDt.Columns.Add("Survey Type");
            SurveyDt.Columns.Add("Surveyor");
            SurveyDt.Columns.Add("Other Surveyors");
            SurveyDt.Columns.Add("Start Time");
            SurveyDt.Columns.Add("Time Spent");

            var surveys = Database.BotanicalSurveys
                .Include(_ => _.BotanicalSurveyArea)
                .Include(_ => _.User)
                .Include(_ => _.THP_Area)
                .Where(_ => !_._delete && !_.Repository && _.THP_Area.Guid == tHP_Area.Guid);
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
            WriteBotanyThpReportTable(SurveyDt, sheet, 2, true);


            DataTable dt = new DataTable();
            dt.Columns.Add("Scientific Name");
            dt.Columns.Add("Common Name");
            dt.Columns.Add("Family");

            var plants1 = Database.BotanicalPlantsOfInterest
                 .Include(_ => _.PlantSpecies)
                 .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                 .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid)
                 .Select(_ => _.PlantSpecies);
            var plants2 = Database.BotanicalPlantsList
                 .Include(_ => _.PlantSpecies)
                 .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurveyArea).ThenInclude(_ => _.THP_Area)
                 .Where(_ => !_.BotanicalElement._delete && !_.BotanicalElement.Repository && _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid)
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
            WriteBotanyThpReportTable(dt, sheet, SurveyDt.Rows.Count + 4, false);

            sheet.Rows.RowHeight = 15;
            sheet.Columns.AutoFit();
        }






        private void ExportSurveyArea(Excel.Workbook wb, THP_Area tHP_Area)
        {
            Excel.Worksheet sheet = wb.Sheets.Add();
            sheet.Name = "Survey Area";

            IInfoTypeManager manager = new InformationTypeManager<BotanicalSurveyArea>();
            var records = manager.GetQueryable(new object[] {tHP_Area}, typeof(THP_Area), Database, includeGeometry: true, showDelete: false, showRepository: false);

            DataTable dt = EntityToDatatable(manager, records);
            WriteBotanyTable(dt, sheet);
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
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.User)
                .Include(_=>_.AssociatedPlants).ThenInclude(_=>_.PlantSpecies)
                .Where (_=> !_.BotanicalElement._delete && !_.BotanicalElement.Repository && _.BotanicalElement.BotanicalSurveyArea.THP_Area.Guid == tHP_Area.Guid)
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
            dt.Columns.Add("DateTime");
            dt.Columns.Add("Lat");
            dt.Columns.Add("Lon");
            dt.Columns.Add("Datum");
            dt.Columns.Add("Radius");
            dt.Columns.Add("Site Quality");
            dt.Columns.Add("Land Use");
            dt.Columns.Add("Vegetative");
            dt.Columns.Add("Flowering");
            dt.Columns.Add("Fruiting");
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

            WriteBotanyTable(dt, sheet);
            new PostGisShapefileConverter(typeof(BotanicalPlantOfInterest), records, path + "\\Botanical Survey.shp");
        }





        public DataTable EntityToDatatable(IInfoTypeManager manager, IQueryable records)
        {
            Type i = manager.InformationType;
            DataTable dt = new DataTable();
            var propertyColumns = manager.DisplayFields;
            foreach (var property in propertyColumns)
            {
                if (property.DataType.BaseType == typeof(Geometry)) continue;
                dt.Columns.Add(new DataColumn(property.ShapefileColumn, property.DataType));
            }

            foreach (var record in records)
            {
                DataRow dataRow = dt.NewRow();  
                foreach (var col in propertyColumns)
                {
                    if (col.FullName.Contains("."))
                    {
                        PropertyInfo prop = null;
                        object val = null;
                        var parts = col.FullName.Split('.');
                        foreach (var part in parts)
                        {
                            if (prop == null)
                            {
                                prop = i.GetProperty(part);
                                val = prop.GetValue(record);
                            }
                            else
                            {
                                prop = prop.PropertyType.GetProperty(part);
                                if (val != null)
                                    val = prop.GetValue(val);
                            }
                        }
                        if (val != null)
                            dataRow[col.ShapefileColumn] = val;
                    }
                    else
                    {
                        var prop = i.GetProperty(col.FullName);
                        var val = prop.GetValue(record);
                        if (val != null) 
                            dataRow[col.ShapefileColumn] = val;
                    }
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }



        private void WriteBotanyThpReportTable(DataTable dt, Excel.Worksheet sheet, int startRow, bool survey)
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
                    else if (dt.Columns[c].ColumnName == "TimeSpent")
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N2") + "h";
                    }
                    else if (dt.Columns[c].ColumnName.Contains("Time"))
                    {
                        if (survey) printArray[i + 1, c] = ((DateTime)dt.Rows[i][c]).ToShortDateString();
                        else printArray[i + 1, c] = ((DateTime)dt.Rows[i][c]).ToShortDateString() + " " + ((DateTime)dt.Rows[i][c]).ToShortTimeString();
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

            sheet.Rows.RowHeight = 15;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + startRow)).Value2 = printArray;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + startRow)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + startRow)).Borders.Weight = Excel.XlBorderWeight.xlThin;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + startRow).Interior.Color = Excel.XlRgbColor.rgbYellow;
            sheet.Columns.AutoFit();
        }

        private void WriteBotanyTable(DataTable dt, Excel.Worksheet sheet)
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
                    else if (dt.Columns[c].ColumnName == "TimeSpent")
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N2") + "h";
                    }
                    else if (dt.Columns[c].ColumnName.Contains("Time"))
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

            sheet.Rows.RowHeight = 15;
            sheet.get_Range("A1:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Value2 = printArray;
            sheet.get_Range("A1:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.get_Range("A2:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;
            sheet.get_Range("A1:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + 1).Interior.Color = Excel.XlRgbColor.rgbYellow;
            sheet.Columns.AutoFit();
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
