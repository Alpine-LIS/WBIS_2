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
    public class BotanicalSurveyAreaImportViewModel : RecordImporterBase
    {
        public BotanicalSurveyAreaImportViewModel()
        {                       
    }

        public override string HelperText => "\t‘Attempt Replace’ will attempt to replace the geometry and attribute data of botanical survey area where the THP area and area name are the same. " +
                "If not selected, then all records will be treated as new area." +
                "\n\n\t‘Connect to Scoping’ will attempt to create connection between imported areas and botanical scoping records with the same THP area." +
            "\n\n\t‘Repository Data’ if selected new records will be marked as repository. ";
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(BotanicalSurveyArea));

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
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();
            int updateCount = 0;
            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "THP_Area.THPName").Attribute;
            string nameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "AreaName").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                var thp = DbHelp.ThpExistance(Database, feat.DataRow[thpCol].ToString());
                if (Database.BotanicalSurveyAreas
                    .Include(_=>_.THP_Area)
                    .Any(_ => !_._delete && !_.Repository && _.THP_Area == thp && _.AreaName == feat.DataRow[nameCol].ToString()))
                    updateCount++;
            }
            w.Stop();
            return updateCount;
        }

        public string CheckBlanks()
        {           
            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "THP_Area.THPName").Attribute;
            string nameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "AreaName").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                if (feat.DataRow[thpCol].ToString() == "" || feat.DataRow[nameCol].ToString() == "")
                {
                    return "There are records missing either a THP name or area name.";
                }
            }
            return "";
        }

        public override string CheckDupIds()
        {
            List<string> dupIds = new List<string>();
            List<string> usedIds = new List<string>();
            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "THP_Area.THPName").Attribute;
            string nameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "AreaName").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                string fId = $"THP:'{feat.DataRow[thpCol].ToString()}' Area'{feat.DataRow[nameCol].ToString()}'";
                if (fId != "")
                {
                    if (usedIds.Contains(fId))
                        dupIds.Add(fId);
                    else usedIds.Add(fId);
                }
            }
            if (dupIds.Count > 0)
                return $"The following area names were found to have duplicates in the chosen file.\n\t" +
                                string.Join("\n\t", dupIds);
            else return "";
        }


        public override void SaveClick()
        {
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "THP_Area.THPName").Attribute;
            string nameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "AreaName").Attribute;

            List<THP_Area> newThps = new List<THP_Area>();


            foreach (var feat in ImportShapefile.Features)
            {
                var thp = DbHelp.ThpExistance(Database, feat.DataRow[thpCol].ToString());
                if (thp == null)
                    thp = newThps.FirstOrDefault(_ => DbHelp.ThpQueryName(feat.DataRow[thpCol].ToString()) == DbHelp.ThpQueryName(_.THPName));
                if (thp == null)
                {
                    thp = new THP_Area() { THPName = feat.DataRow[thpCol].ToString() };
                    Database.THP_Areas.Add(thp);
                    newThps.Add(thp);
                }

                BotanicalSurveyArea? surveyArea =  Database.BotanicalSurveyAreas
                    .Include(_ => _.THP_Area)
                    .Include(_ => _.BotanicalScoping)
                    .FirstOrDefault(_ => !_._delete && !_.Repository && _.THP_Area == thp && _.AreaName == feat.DataRow[nameCol].ToString());

                if (surveyArea == null || !AttemptReplace)
                {
                    surveyArea = new BotanicalSurveyArea() 
                    { 
                    AreaName = feat.DataRow[nameCol].ToString(),
                    THP_Area = thp,
                    Repository = RepositoryData
                    };
                    Database.BotanicalSurveyAreas.Add(surveyArea);
                }

                if (ConnectScoping || thp != null)
                {
                    var scoping = Database.BotanicalScopings
                        .Include(_ => _.BotanicalSurveyAreas)
                        .Include(_ => _.THP_Area)
                        .FirstOrDefault(_ => thp == _.THP_Area);
                    if (scoping != null)
                        surveyArea.BotanicalScoping = scoping;
                }

                if (feat.Geometry.SRID == 0) feat.Geometry.SRID = 26710;
                if (feat.Geometry is NetTopologySuite.Geometries.Polygon) surveyArea.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
                else surveyArea.Geometry = (MultiPolygon)feat.Geometry;

                surveyArea._delete = false;
            }
            Database.SaveChanges();
            w.Stop();
        }
        public override List<string> RecordTypeSaveCheck()
        {
            List<string> issues = new List<string>();
            //issues.AddRange(CheckTpes(typeof(BotanicalSurveyArea)));

            string dupIds = CheckBlanks();
            if (dupIds != "") issues.Add(dupIds);
            dupIds = CheckDupIds();
            if (dupIds != "") issues.Add(dupIds);

            return issues;
        }

        public bool AttemptReplace { get; set; } = true;
        public bool ConnectScoping { get; set; } = true;
    }
}
