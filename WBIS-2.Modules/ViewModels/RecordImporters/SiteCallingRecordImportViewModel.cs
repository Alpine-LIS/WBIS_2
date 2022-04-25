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

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public class SiteCallingRecordImportViewModel : RecordImporterBase
    {
        public SiteCallingRecordImportViewModel()
        {
                   AddDetectionCommand = new DelegateCommand(AddDetection);
        }


        public override List<PropertyType> AvailibleFields => GetProperties(typeof(SiteCalling));


        public override void FileSelectClick()
        {
           OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;
            var tempShape = Shapefile.OpenFile(ofd.FileName);
            if (tempShape.FeatureType != FeatureType.Point )
            {
                MessageBox.Show("The selected shapefile does not contain points.");
                return;
            }
            ImportShapefile = Shapefile.OpenFile(ofd.FileName);
        }

        public override IInformationType ReturnRecordId(string link)
        {
            throw new NotImplementedException();
        }

        public override IInformationType ReturnRecordSpacial(Feature link)
        {
            throw new NotImplementedException();
        }

        public override void SaveClick()
        {
            if (IsSubElement) return;
        }
        public override void SaveClick(List<object> ExcludeIds)
        {
            foreach (var feat in ImportShapefile.Features)
            {

            }
        }
        public override List<object> GenerateChild()
        {
            return null;
        }



        public override bool BoolSaveCheck()
        {
            if (DetectionImport != null)
                return DetectionImport.CheckSave();

            return true;
        }
        public override List<string> ListSaveCheck()
        {
            List<string> issues = new List<string>();
            issues.AddRange(CheckAvailibleOptions(typeof(StaticOptions.SiteCalling)));
            issues.AddRange(CheckTpes(typeof(SiteCalling)));
            if (DetectionImport != null)
                issues.AddRange(DetectionImport.ListSaveCheck());

            return issues;
        }

        SiteCallingDetectionRecordImportViewModel DetectionImport;
        public ICommand AddDetectionCommand { get; set; }
        public void AddDetection()
        {
            if (DetectionImport == null)
            {
                var a = new SiteCallingDetectionRecordImportView();
                DetectionImport = (SiteCallingDetectionRecordImportViewModel)a.DataContext;
                Holder.AddImportControl(a);
            }
        }

        public override SiteCalling BuildAttributes(object unit, DataRow dataRow)
        {
            SiteCalling siteCalling = (SiteCalling)unit;
            Type type = typeof(SiteCalling);

            var attributes = PropertyCrosswalk.Where(_ => _.PropertyType != null);

            foreach (var attribute in attributes)
            {
                var prop = type.GetProperty(attribute.PropertyType.PropertyName);
                if (prop.PropertyType.IsPrimitive)
                {
                    var val = ValueProcessors.GetParseValue(dataRow[attribute.Attribute], prop.PropertyType);
                    prop.SetValue(siteCalling, val);
                }
                else
                {

                }
            }

            return siteCalling;
        }
    }
}
