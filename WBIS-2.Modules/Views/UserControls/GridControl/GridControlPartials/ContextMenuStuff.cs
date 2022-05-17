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
            {
                if (((ListViewModelBase)DataContext).ListManager.CanSetActive)
                    ActiveUnitsMenuItems(ref contextMenu);
                if (((ListViewModelBase)DataContext).ListManager.DeleteRestoreRecord)
                    DeleteUnitsMenuItems(ref contextMenu);
                if (((ListViewModelBase)DataContext).ListManager.RepositoryRecord)
                    RepositoryUnitsMenuItems(ref contextMenu);
            }
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

        private void DeleteUnitsMenuItems(ref ContextMenu contextMenu)
        {
            MenuItem DeleteRecords = new MenuItem() { Header = "Delete selected records" };
            DeleteRecords.Click += DeleteRecords_Click; ;
            contextMenu.Items.Add(DeleteRecords);
            MenuItem RestoreRecords = new MenuItem() { Header = "Restore selected records" };
            RestoreRecords.Click += RestoreRecords_Click; ;
            contextMenu.Items.Add(RestoreRecords);
        }

        private void DeleteRecords_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).DeleteRecords();
        }

        private void RestoreRecords_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).RestoreRecords();
        }

        private void RepositoryUnitsMenuItems(ref ContextMenu contextMenu)
        {
            MenuItem RepositoryRecords = new MenuItem() { Header = "Store records as repository data" };
            RepositoryRecords.Click += RepositoryRecords_Click; ;
            contextMenu.Items.Add(RepositoryRecords);
            MenuItem ReviveRepository = new MenuItem() { Header = "Revive repository data" };
            ReviveRepository.Click += ReviveRepository_Click; ;
            contextMenu.Items.Add(ReviveRepository);
        }

        private void ReviveRepository_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).RepositoryReviveRecords();
        }

        private void RepositoryRecords_Click(object sender, RoutedEventArgs e)
        {
            ((ListViewModelBase)DataContext).RepositoryStoreRecords();
        }
    }
}