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
    public class ProtectionZoneImportViewModel : RecordImporterBase
    {
        public ProtectionZoneImportViewModel()
        {

        }

        public bool AttemptReplace { get; set; } = true;
        public override string HelperText => "\t‘Attempt Replace’ will attempt to replace the geometry and attribute data of protection zones. " +
                "If not selected, then all records will be treated as new." +
                "\n\n\t‘Repository Data’ if selected new records will be marked as repository. ";
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(ProtectionZone));

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

        public string CheckBlanks()
        {           
            string idCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "PZ_ID").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                if (feat.DataRow[idCol].ToString() == "")
                {
                    return "There are records missing a protection zone id.";
                }
            }
            return "";
        }

        public override string CheckDupIds()
        {
            List<string> dupIds = new List<string>();
            List<string> usedIds = new List<string>();
            string idCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "PZ_ID").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                string fId = feat.DataRow[idCol].ToString();
                if (fId != "")
                {
                    if (usedIds.Contains(fId))
                        dupIds.Add(fId);
                    else usedIds.Add(fId);
                }
            }
            if (dupIds.Count > 0)
                return $"The following ids were found to have duplicates in the chosen file.\n\t" +
                                string.Join("\n\t", dupIds);
            else return "";
        }


        public override void SaveClick()
        {
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();
            DateTime startImport = DateTime.Now;

            string idCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "PZ_ID").Attribute;
                   
            foreach (var feat in ImportShapefile.Features)
            {
                ProtectionZone? pz = null;
                
                if (AttemptReplace)
                    pz = Database.ProtectionZones
                    .FirstOrDefault(_ => !_._delete && !_.Repository && _.PZ_ID == feat.DataRow[idCol].ToString());



                if (pz == null)
                {
                    pz = new ProtectionZone()
                    {
                        PZ_ID = feat.DataRow[idCol].ToString(),
                        Repository = RepositoryData
                    };
                    Database.ProtectionZones.Add(pz);
                }

                if (feat.Geometry.SRID == 0) feat.Geometry.SRID = 26710;
                if (feat.Geometry is NetTopologySuite.Geometries.Polygon) pz.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
                else pz.Geometry = (MultiPolygon)feat.Geometry;

                pz._delete = false;
            }
            Database.SaveChanges();

            var hexs = Database.Hex160s
                .Include(_ => _.ProtectionZones)
                .Where(_ => _.ProtectionZones.Any(x => x.DateModified >= startImport)).Select(_=>_.Hex160ID).ToArray();
            new Hex160_PZs(hexs);

            w.Stop();
        }
        public override List<string> RecordTypeSaveCheck()
        {
            List<string> issues = new List<string>();
            //issues.AddRange(CheckTpes(typeof(ProtectionZone)));

            string dupIds = CheckBlanks();
            if (dupIds != "") issues.Add(dupIds);
            //dupIds = CheckDupIds();
            //if (dupIds != "") issues.Add(dupIds);

            return issues;
        }

        public bool NewSurveyAreas { get; set; } = true;
    }
}
