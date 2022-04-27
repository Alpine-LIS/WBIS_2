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
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.Views.RecordImporters;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public class SiteCallingDetectionImportViewModel : RecordImporterBase
    {
        public SiteCallingDetectionImportViewModel()
        {
            AddDetectionCommand = new DelegateCommand(AddDetection);
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
            if (tempShape.FeatureType != FeatureType.Point)
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

        SiteCallingDetectionImportViewModel DetectionImport;
        public ICommand AddDetectionCommand { get; set; }

        public override List<int> SkipFids => throw new NotImplementedException();

        public void AddDetection()
        {
            if (DetectionImport == null)
            {
                var a = new SiteCallingDetectionImportView();
                DetectionImport = (SiteCallingDetectionImportViewModel)a.DataContext;
                Holder.AddImportControl(a);
            }
        }


        public override List<object> GenerateChild(string parentId)
        {
            throw new NotImplementedException();
        }
    }
}
