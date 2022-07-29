using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBIS_2.DataModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Atlas.Data;
using System.IO;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using WBIS_2.Modules.Views.RecordImporters;
using WBIS_2.Modules.Tools;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public class SPIWildlifeSightingImportViewModel : RecordImporterBase
    {
        public SPIWildlifeSightingImportViewModel()
        {
            ReplaceList = true;
        }

        public bool ReplaceList { get { return GetProperty(() => ReplaceList); }
            set {
                if (ReplaceList == value) return;
                SetProperty(() => ReplaceList, value);
                AppendList = !ReplaceList;
            } } 
        public bool AppendList
        {
            get { return GetProperty(() => AppendList); }
            set
            {
                if (AppendList == value) return;
                SetProperty(() => AppendList, value);
                ReplaceList = !AppendList;
            }
        }
        public override string HelperText => "\t‘Replace list’ will delete all current record and replace them with the selected import. 'Append to list' will add new records.";
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(SPI_WildlifeSighting));

        public override void FileSelectClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XLSX|*.xlsx";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;

            ImportDataTable = ExcelTools.GetExcelTableFirst(ofd.FileName);
            Holder.ShapeCount = $"Records: {ImportDataTable.Rows.Count.ToString("N0")}";
            RaisePropertyChanged(nameof(Holder.ShapeCount));
        }

        public override int GetUpdateCount()
        {           
            return 0;
        }

        public override void SaveClick()
        {
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();
            DateTime startImport = DateTime.Now;

            var wshdCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).FirstOrDefault(_ => _.PropertyType.PropertyName == "Watershed.WatershedID");//.Attribute;
            var distCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).FirstOrDefault(_ => _.PropertyType.PropertyName == "District.DistrictName");//.Attribute;

            if (ReplaceList)
            {
                var records = Database.SPI_WildlifeSightings;
                foreach (var item in records)
                    Database.SPI_WildlifeSightings.Remove(item);
            }

            foreach(DataRow r in ImportDataTable.Rows)
            {
                SPI_WildlifeSighting record = new SPI_WildlifeSighting();
                BuildAttributes(ref record, r);
                if (distCol != null)
                    record.District = Database.Districts.FirstOrDefault(_ => _.DistrictName.ToUpper() == r[distCol.Attribute].ToString().ToUpper());
                if (record.District == null) record.District = Database.Districts.FirstOrDefault(_ => _.DistrictName.ToUpper() == "N/A");
                if (wshdCol != null)
                {
                    string wshdId = r[wshdCol.Attribute].ToString();
                    wshdId = ValueProcessors.BuildWshdtring(wshdId);
                    record.Watershed = Database.Watersheds.FirstOrDefault(_ => _.WatershedID == wshdId);
                }
                Database.SPI_WildlifeSightings.Add(record);
            }

            Database.SaveChanges();
            
            w.Stop();
        }
        public override List<string> RecordTypeSaveCheck()
        {
            List<string> issues = new List<string>();
            //issues.AddRange(CheckTpes(typeof(SPI_GGOW)));

            return issues;
        }

        public override string CheckDupIds()
        {
            return "";
        }

        public bool NewSurveyAreas { get; set; } = true;
    }
}
