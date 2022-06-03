using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class DistrictReportViewModel : WBISViewModelBase
    {
        public List<DistrictBoolClass> SelectableDistricts { get; set; }
        public List<InfoTypeChooser> SelectableInfoTypes { get; set; }
        public DistrictReportViewModel()
        {
            District dist = new District();
            SelectableInfoTypes = new List<InfoTypeChooser>();
            foreach ( var childType in dist.Manager.AvailibleChildren)
            {
                SelectableInfoTypes.Add(new InfoTypeChooser() { InfoType = childType.GetType() });
            }
            SelectableDistricts = new List<DistrictBoolClass>();
            foreach(var d in Database.Districts)
            {
                SelectableDistricts.Add(new DistrictBoolClass() { District = d, IsSelected = CurrentUser.Districts.Contains(d) });
            }
        }

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

            var districts = SelectableDistricts.Where(_=>_.IsSelected).Select(_=>_.District).ToArray();
            foreach(var infoType in SelectableInfoTypes.Where(_=>_.Selected))
            {
                IInformationType i = (IInformationType)Activator.CreateInstance(infoType.InfoType);
                var records = i.Manager.GetQueryable(districts, typeof(District), Database);

                Dictionary<string, string> PropertyColumns = new Dictionary<string, string>();

                var properties = i.Manager.InformationType.GetProperties();
                foreach (var prop in properties)
                {

                }

            }
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
    }
}
