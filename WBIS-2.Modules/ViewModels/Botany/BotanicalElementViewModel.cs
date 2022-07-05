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
using WBIS_2.Modules.Views.Botany;

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
            if (Database.BotanicalPlantsOfInterest.Any(_ => _.Id == guid))
            {
                ViewName = typeof(BotanicalPlantOfInterestView).Name;
                ViewModel = typeof(BotanicalPlantOfInterestViewModel);
            }
            else if (Database.BotanicalPointsOfInterest.Any(_ => _.Id == guid))
            {
                ViewName = typeof(BotanicalPointOfInterestView).Name;
                ViewModel = typeof(BotanicalPointOfInterestViewModel);
            }
            else
            {
                ViewName = typeof(BotanicalPlantListView).Name;
                ViewModel = typeof(BotanicalPlantListViewModel);
            }
        }
        public string ViewName { get; set; }
        public Type ViewModel { get; set; }
    }
}
