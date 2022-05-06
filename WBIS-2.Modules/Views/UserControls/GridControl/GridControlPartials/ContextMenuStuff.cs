using Atlas.Controls;
using Atlas.Data;
using Atlas.Symbology;
using DevExpress.Data.Filtering;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WBIS_2.Common;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Npgsql;

namespace WBIS_2.Modules.Views
{
    public partial class GridControlView : UserControl
    {
        private void SetUnitsContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            if (((ListViewModelBase)DataContext).ListManager != null)
                if (((ListViewModelBase)DataContext).ListManager.CanSetActive)
                    ActiveUnitsMenuItems(ref contextMenu);

            MyGrid.ContextMenu = contextMenu;
        }
        private void ActiveUnitsMenuItems(ref ContextMenu contextMenu)
        {
            MenuItem SetActive = new MenuItem() { Header = "Set as active" };
            SetActive.Click += SetActive_Click;
            contextMenu.Items.Add(SetActive);
            MenuItem AppendActive = new MenuItem() { Header = "Append to active" };
            AppendActive.Click += AppendActive_Click;
            contextMenu.Items.Add(AppendActive);
            MenuItem RemoveActive = new MenuItem() { Header = "Remove from active" };
            RemoveActive.Click += RemoveActive_Click;
            contextMenu.Items.Add(RemoveActive);
        }

        private void RemoveActive_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).RemoveActiveUnits(false);
            ((ListViewModelBase)DataContext).RefreshDataSource();
        }

        private void AppendActive_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).AddAppendActiveUnits(false);
            ((ListViewModelBase)DataContext).RefreshDataSource();
        }

        private void SetActive_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).AddAppendActiveUnits(true);
            ((ListViewModelBase)DataContext).RefreshDataSource();
        }
    }
}