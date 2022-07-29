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
using Point = NetTopologySuite.Geometries.Point;

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public class PermanentCallStationImportViewModel : RecordImporterBase
    {
        public PermanentCallStationImportViewModel()
        {
            AttemptReplace = false;
        }

        public bool AttemptReplace { get; set; } = true;
        public override string HelperText => "\t‘Attempt Replace’ will attempt to replace the geometry and attribute data of permanent call stations. " +
                "If not selected, then all records will be treated as new." +
                "\n\n\t‘Repository Data’ if selected new records will be marked as repository. ";
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(PermanentCallStation));

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

        public string CheckBlanks()
        {           
            string idCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "PCS_ID").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                if (feat.DataRow[idCol].ToString() == "")
                {
                    return "There are records missing a permanent call station id.";
                }
            }
            return "";
        }

        public override string CheckDupIds()
        {
            List<string> dupIds = new List<string>();
            List<string> usedIds = new List<string>();
            string idCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "PCS_ID").Attribute;
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

            string idCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "PCS_ID").Attribute;
                   
            foreach (var feat in ImportShapefile.Features)
            {
                PermanentCallStation? pcs = null;

                if (AttemptReplace)
                    pcs = Database.PermanentCallStations
                    .FirstOrDefault(_ => !_._delete && !_.Repository && _.PCS_ID == feat.DataRow[idCol].ToString());

                if (pcs == null)
                {
                    pcs = new PermanentCallStation()
                    {
                        PCS_ID = feat.DataRow[idCol].ToString(),
                        Repository = RepositoryData
                    };
                    Database.PermanentCallStations.Add(pcs);
                }

                if (feat.Geometry.SRID == 0) feat.Geometry.SRID = 26710;
                pcs.Geometry = (Point)feat.Geometry;

                pcs._delete = false;
            }
            Database.SaveChanges();

            w.Stop();
        }
        public override List<string> RecordTypeSaveCheck()
        {
            List<string> issues = new List<string>();
            //issues.AddRange(CheckTpes(typeof(PermanentCallStation)));

            string dupIds = CheckBlanks();
            if (dupIds != "") issues.Add(dupIds);
            //dupIds = CheckDupIds();
            //if (dupIds != "") issues.Add(dupIds);

            return issues;
        }

        public bool NewSurveyAreas { get; set; } = true;
    }
}
