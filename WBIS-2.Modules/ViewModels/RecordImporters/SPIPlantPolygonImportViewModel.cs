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
    public class SPIPlantPolygonImportViewModel : RecordImporterBase
    {
        public SPIPlantPolygonImportViewModel()
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
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(SPIPlantPolygon));

        public override void FileSelectClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;
            var tempShape = Shapefile.OpenFile(ofd.FileName);
            if (tempShape.FeatureType != FeatureType.Polygon)
            {
                MessageBox.Show("The selected shapefile does not contain polygons.");
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
                var records = Database.SPIPlantPolygons;
                foreach (var item in records)
                    Database.SPIPlantPolygons.Remove(item);
            }

            var sciNameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).FirstOrDefault(_ => _.PropertyType.PropertyName == "PlantSpecies.SciName");
            var comNameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).FirstOrDefault(_ => _.PropertyType.PropertyName == "PlantSpecies.ComName");
            List<PlantSpecies> newPlants = Database.PlantSpecies.ToList();

            foreach (var feat in ImportShapefile.Features)
            {
                SPIPlantPolygon record = new SPIPlantPolygon();
                BuildAttributes(ref record, feat.DataRow);

                //if (sciNameCol != null && comNameCol != null)
                //    record.PlantSpecies = Database.PlantSpecies.FirstOrDefault(_ => _.SciName.ToUpper() == feat.DataRow[sciNameCol.Attribute].ToString().ToUpper() && _.ComName.ToUpper() == feat.DataRow[comNameCol.Attribute].ToString().ToUpper());
                //if (record.PlantSpecies == null)
                //    record.PlantSpecies = Database.PlantSpecies.FirstOrDefault(_ => _.SciName.ToUpper() == feat.DataRow[sciNameCol.Attribute].ToString().ToUpper());
                if (comNameCol != null)
                    record.PlantSpecies = newPlants.FirstOrDefault(_ => _.SciName.ToUpper() == feat.DataRow[sciNameCol.Attribute].ToString().ToUpper() && _.ComName.ToUpper() == feat.DataRow[comNameCol.Attribute].ToString().ToUpper());
                if (record.PlantSpecies == null)
                    record.PlantSpecies = newPlants.FirstOrDefault(_ => _.SciName.ToUpper() == feat.DataRow[sciNameCol.Attribute].ToString().ToUpper());


                if (record.PlantSpecies == null)
                {
                    PlantSpecies plantSpecies = new PlantSpecies()
                    {
                        ComName = feat.DataRow[sciNameCol.Attribute].ToString()
                     };
                    if (comNameCol != null)
                        plantSpecies.ComName = feat.DataRow[comNameCol.Attribute].ToString();
                    newPlants.Add(plantSpecies);
                    Database.PlantSpecies.Add(plantSpecies);
                                      
                    record.PlantSpecies = plantSpecies;
                }
                if (feat.Geometry.SRID == 0) feat.Geometry.SRID = 26710;
                if (feat.Geometry is NetTopologySuite.Geometries.Polygon) record.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
                else record.Geometry = (MultiPolygon)feat.Geometry;
                record._delete = false;

                Database.SPIPlantPolygons.Add(record);
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
