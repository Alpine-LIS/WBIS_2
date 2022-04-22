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

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public class SiteCallingRecordImportViewModel : RecordImporterBase
    {
        public SiteCallingRecordImportViewModel()
        {
                   AddRepositoryCommand = new DelegateCommand(AddRepository);
        }


        public override List<PropertyType> AvailibleFields => GetProperties(typeof(SiteCalling));

        public bool RepositoryData { get; set; } = false;

        public override void FileSelectClick()
        {
           OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;
            var tempShape = Shapefile.OpenFile(ofd.FileName);
            if (tempShape.FeatureType != FeatureType.Point && tempShape.FeatureType != FeatureType.MultiPoint)
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
        }
        public override List<string> RecordTypeSaveCheck()
        {
            List<string> issues = new List<string>();
            issues.AddRange(CheckAvailibleOptions(typeof(StaticOptions.SiteCalling)));
            issues.AddRange(CheckTpes(typeof(SiteCalling)));

            return issues;
        }

        SiteCallingDetectionRecordImportViewModel DetectionImport;
        public ICommand AddRepositoryCommand { get; set; }
        public void AddRepository()
        {
            if (DetectionImport == null)
            {
                var a = new SiteCallingDetectionRecordImportView();
                DetectionImport = (SiteCallingDetectionRecordImportViewModel)a.DataContext;
                Holder.AddImportControl(a);
            }
        }
    }
}
