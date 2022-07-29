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
    public class BotanicalSurveyImportViewModel : RecordImporterBase
    {
        public BotanicalSurveyImportViewModel()
        {

        }

        public override string HelperText => "\t‘Create new survey areas’ will create new records without geometry to represent the survey areas for surveys where no area could be found. " +
            "If unchecked these records will be excluded form the import. " +
            "\n\n\t‘Repository Data’ if selected new records will be marked as repository. ";
        public override List<PropertyType> AvailibleFields => GetProperties(typeof(BotanicalSurvey));

        public override void FileSelectClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;
            var tempShape = Shapefile.OpenFile(ofd.FileName);
            if (tempShape.FeatureType != FeatureType.Line)
            {
                MessageBox.Show("The selected shapefile does not contain lines.");
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
            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "BotanicalSurveyArea.THP_Area.THPName").Attribute;
            string nameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "BotanicalSurveyArea.AreaName").Attribute;
            foreach (var feat in ImportShapefile.Features)
            {
                if (feat.DataRow[thpCol].ToString() == "" || feat.DataRow[nameCol].ToString() == "")
                {
                    return "There are records missing either a THP name or area name.";
                }
            }
            return "";
        }

       //Botanical surveys cannot import duplicates.
        public override string CheckDupIds()
        {           
            return "";
        }


        public override void SaveClick()
        {
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "BotanicalSurveyArea.THP_Area.THPName").Attribute;
            string nameCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "BotanicalSurveyArea.AreaName").Attribute;

            List<THP_Area> newThps = new List<THP_Area>();
            List<BotanicalSurveyArea> newAreas = new List<BotanicalSurveyArea>();


            foreach (var feat in ImportShapefile.Features)
            {
                BotanicalSurveyArea? surveyArea = null;
                var thp = DbHelp.ThpExistance(Database, feat.DataRow[thpCol].ToString());
                if (thp == null)
                    thp = newThps.FirstOrDefault(_ => DbHelp.ThpQueryName(feat.DataRow[thpCol].ToString()) == DbHelp.ThpQueryName(_.THPName));

                if (thp != null)
                {
                    surveyArea = Database.BotanicalSurveyAreas
                        .Include(_ => _.THP_Area)
                        .Include(_ => _.BotanicalScoping)
                        .FirstOrDefault(_ => !_._delete && _.THP_Area == thp && _.AreaName == feat.DataRow[nameCol].ToString());

                    if (surveyArea == null)
                        surveyArea = newAreas.FirstOrDefault(_ => !_._delete && _.THP_Area == thp
                            && _.AreaName == feat.DataRow[nameCol].ToString());
                }

                if (surveyArea == null)
                {
                    if (NewSurveyAreas)
                    {
                        if (thp == null)
                        {
                            thp = new THP_Area() { THPName = feat.DataRow[thpCol].ToString() };
                            Database.THP_Areas.Add(thp);
                            newThps.Add(thp);
                        }

                        surveyArea = new BotanicalSurveyArea()
                        {
                            AreaName = feat.DataRow[nameCol].ToString(),
                            THP_Area = thp,
                            Repository = RepositoryData
                        };
                        Database.BotanicalSurveyAreas.Add(surveyArea);
                        newAreas.Add(surveyArea);
                    }
                    else
                        continue;
                }

                BotanicalSurvey survey = new BotanicalSurvey()
                {
                    BotanicalSurveyArea = surveyArea,
                    THP_Area = surveyArea.THP_Area,
                    Repository = RepositoryData
                };
                BuildAttributes(ref survey, feat.DataRow);

                if (feat.Geometry.SRID == 0) feat.Geometry.SRID = 26710;

                if (feat.Geometry is LineString) survey.Geometry = new MultiLineString(new LineString[] { (LineString)feat.Geometry });
                else survey.Geometry = (MultiLineString)feat.Geometry;

                survey._delete = false;
                Database.BotanicalSurveys.Add(survey);
            }
            Database.SaveChanges();
            w.Stop();
        }

        public override void BuildAttributes<BotanicalSurvey>(ref BotanicalSurvey unit, DataRow dataRow)
        {
            BotanicalSurvey survey = (BotanicalSurvey)unit;

            var attributes = PropertyCrosswalk.Where(_ => _.PropertyType != null);
            attributes = attributes.Where(_ => !_.PropertyType.PropertyName.Contains("."));

            foreach (var attribute in attributes)
            {
                var prop = typeof(BotanicalSurvey).GetProperty(attribute.PropertyType.PropertyName);
                var val = ValueProcessors.GetParseValue(dataRow[attribute.Attribute], prop.PropertyType);
                prop.SetValue(survey, val);
            }
        }
        public override List<string> RecordTypeSaveCheck()
        {
            List<string> issues = new List<string>();
            //issues.AddRange(CheckTpes(typeof(BotanicalSurveyArea)));

            string dupIds = CheckBlanks();
            if (dupIds != "") issues.Add(dupIds);
            
            return issues;
        }

        public bool NewSurveyAreas { get; set; } = true;
    }
}
