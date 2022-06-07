using Atlas.Data;
using Atlas.Projections;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;
using NetTopologySuite.Geometries;
using DevExpress.Mvvm.POCO;
using System.Windows.Input;
using System.IO.Compression;
using WBIS_2.Modules.Tools;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class DistrictReportViewModel : WBISViewModelBase, IDocumentContent
    {
        public object Title => $"District Report";
      
        public bool IncludeRepository { get; set; } = true;
        public List<DistrictBoolClass> SelectableDistricts { get; set; }
        public List<InfoTypeChooser> SelectableInfoTypes { get; set; }
        public DistrictReportViewModel()
        {
            District dist = new District();
            SelectableInfoTypes = new List<InfoTypeChooser>();
            foreach ( var childType in dist.Manager.AvailibleChildren)
            {
                if (childType.GetType().GetProperty("Geometry") == null) continue;
                SelectableInfoTypes.Add(new InfoTypeChooser() { InfoType = childType.GetType() });
            }
            SelectableDistricts = new List<DistrictBoolClass>();
            foreach(var d in Database.Districts)
            {
                SelectableDistricts.Add(new DistrictBoolClass() { District = d, IsSelected = CurrentUser.Districts.Select(_=>_.Guid).Contains(d.Guid) });
            }
        }

        public static DistrictReportViewModel Create()
        {
            return ViewModelSource.Create(() => new DistrictReportViewModel()
            { Caption = "District Report", 
            });
        }
        public ICommand SaveCommand => new DelegateCommand(WriteReport);
        public void WriteReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"DistrictExport {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (Directory.Exists(sfd.FileName) || File.Exists(sfd.FileName + ".zip"))
            {
                MessageBox.Show("The selected name is already in use");
                goto HERE;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            Directory.CreateDirectory(sfd.FileName);

            var districts = SelectableDistricts.Where(_=>_.IsSelected).Select(_=>_.District).ToArray();
            foreach(var infoType in SelectableInfoTypes.Where(_=>_.Selected))
            {
                IInformationType i = (IInformationType)Activator.CreateInstance(infoType.InfoType);
                var records = i.Manager.GetQueryable(districts, typeof(District), Database, showDelete: false, showRepository: IncludeRepository, includeGeometry: true);              
                string fileStr = $@"{sfd.FileName}\{i.Manager.DisplayName.Replace(" ","")}.shp";
                new PostGisShapefileConverter(i.GetType(),records,fileStr);
            }

            ZipFile.CreateFromDirectory(sfd.FileName, sfd.FileName + ".zip");            

            Directory.Delete(sfd.FileName, true);
            w.Stop();
            System.Windows.MessageBox.Show("The district export has finished");
        }
























        public class InfoTypeChooser : BindableBase
        {
            public bool Selected { get; set; } = true;
            public string InfoTypeName { get; set; }
            public Type InfoType
            {
                get { return GetProperty(() => InfoType); }
                set
                {
                    SetProperty(() => InfoType, value);
                    InfoTypeName = InfoType.Name;
                }
            }
        }
        public class DistrictBoolClass
        {
            public District District { get; set; }
            public bool IsSelected { get; set; }
        }














        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public override void CloseForm()
        {
        }

        public void OnDestroy()
        {
        }
    }   
}
