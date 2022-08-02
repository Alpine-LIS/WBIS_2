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
    public class SPIPlantPointImportViewModel : RecordImporterBase
    {
        public SPIPlantPointImportViewModel()
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
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(SPIPlantPoint));

        public override void FileSelectClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;
            var tempShape = Shapefile.OpenFile(ofd.FileName);
            if (tempShape.FeatureType != FeatureType.Point)
            {
                MessageBox.Show("The selected shapefile does not contain points.");
                return;
            }
            ImportShapefile = Shapefile.OpenFile(ofd.FileName);
            Holder.ShapeCount = $"Features: {ImportShapefile.Features.Count.ToString("N0")}";
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

            if (ReplaceList)
            {
                var records = Database.SPIPlantPoints;
                foreach (var item in records)
                    Database.SPIPlantPoints.Remove(item);
            }

            foreach(DataRow r in ImportDataTable.Rows)
            {
                SPIPlantPoint record = new SPIPlantPoint();
                BuildAttributes(ref record, r);
                Database.SPIPlantPoints.Add(record);
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
