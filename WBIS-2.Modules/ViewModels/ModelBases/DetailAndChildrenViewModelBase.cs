using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WBIS_2.Common;
using System.Collections.ObjectModel;
using System.Collections;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.Views.Wildlife;
using WBIS_2.Modules.ViewModels.Wildlife;
using System.IO;
using DevExpress.Mvvm.ModuleInjection;
using Npgsql;
using DevExpress.Mvvm.POCO;
using System.Windows.Controls;

namespace WBIS_2.Modules.ViewModels
{
    [POCOViewModel]
    public abstract class DetailAndChildrenViewModelBase : ChildrenListViewModel, IDetailViewModelBase
    {
        public IInformationType Record { get; set; }
        public abstract void Save();
        public void GeoChanged()
        {
            this.Changed = true;
            if (MessageBox.Show("The activity geometry has been updated, the new geometry won't be shown until the record is saved. Press ‘OK’ to save or ‘Cancel’ to continue editing.",
                      "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Save();
            }
        }
    }
}

