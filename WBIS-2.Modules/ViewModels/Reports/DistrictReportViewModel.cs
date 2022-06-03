using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class DistrictReportViewModel : WBISViewModelBase
    {
        public List<InfoTypeChooser> SelectableInfoTypes { get; set; }
        public DistrictReportViewModel()
        {
            District d = new District();
            SelectableInfoTypes = new List<InfoTypeChooser>();
            foreach ( var childType in d.Manager.AvailibleChildren)
            {

            }
        }

        class InfoTypeChooser : BindableBase
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

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public override void CloseForm()
        {
        }
    }
}
