using Atlas.Data;
using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using WBIS_2.Modules.Tools;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Controls;
using WBIS_2.Modules.Views;

namespace WBIS_2.Modules.ViewModels
{
    public class BotanicalElementViewModel : WBISViewModelBase, IDetailView
    {
        public static bool AddSingle => false;
        public static BotanicalElementViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new BotanicalElementViewModel(guid));
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public override void CloseForm()
        {
        }

        public Guid RecordId { get; set; }
        public BotanicalElementViewModel(Guid guid)
        {
            RecordId = guid;
            if (Database.BotanicalPlantsOfInterest.Any(_ => _.Guid == guid))
            {
                UserControl = new PlantSpeciesListView();
            }
            else if (Database.BotanicalPointsOfInterest.Any(_ => _.Guid == guid))
            {

            }
            else
            {

            }
        }
        public UserControl UserControl { get; set; }
    }
}
