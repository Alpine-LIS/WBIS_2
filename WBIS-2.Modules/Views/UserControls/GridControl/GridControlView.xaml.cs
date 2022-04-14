﻿using Atlas.Data;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WBIS_2.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WBIS_2.Modules.ViewModels;

namespace WBIS_2.Modules.Views
{
    /// <summary>
    /// Interaction logic for GridControlView.xaml
    /// </summary>
    public partial class GridControlView : UserControl
    {
        public GridControlView()
        {
            InitializeComponent();
            //Messenger.Default.Register<string>(this, OnMessage);
            MapDataPasser.MapSelectionChangedEvent += MapDataPasser_MapSelectionChangedEvent;
            DataContextChanged += GridControlView_DataContextChanged;
            MyGrid.DefaultSorting = "Guid";

            MyGrid.AutoGeneratedColumns += MyGrid_AutoGeneratedColumns;

            MyGrid.TotalSummary.CollectionChanged += TotalSummary_CollectionChanged;
            AFS = new AlternativeFocusingSelection(MyGrid);
            AFS.AfsMapEvent += AFS_AfsMapEvent;


            FocusHelper focusHelper = new FocusHelper { View = MyView };
            focusHelper.FocusedRowChanged += OnFocusedRowChanged;
            focusHelper.RowDoubleClicked += TableView_RowDoubleClick;


            MyGrid.SelectionChanged += GridControl_SelectionChanged;
            MyGrid.ItemsSourceChanged += GridControl_ItemsSourceChanged;
            MyGrid.FilterChanged += MyGrid_FilterChanged;
            MyGrid.GroupRowCollapsed += MyGrid_GroupRowChanged;
            MyGrid.GroupRowExpanded += MyGrid_GroupRowChanged;

            MyView.PreviewMouseDown += MyView_PreviewMouseDown;
            MyView.CustomRowAppearance += MyView_CustomRowAppearance;
        }

        private bool SelectInProgress = false;
        private void MyGrid_GroupRowChanged(object sender, RowEventArgs e)
        {
            SelectInProgress = true;
            MyGrid.SelectedItems.Clear();

            for (int i = 0; i < MyGrid.VisibleRowCount; i++)
            {
                int rowHandle = MyGrid.GetRowHandleByVisibleIndex(i);
                if (rowHandle >= 0)
                {
                    MyGrid.SelectItem(rowHandle);
                }
            }
            SelectInProgress = false;
            MyGrid.SelectionChangedManual();
        }

        private void GridControlView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null) return;
            DataContextChanged -= GridControlView_DataContextChanged;

            ((ListViewModelBase)DataContext).ExportToXlsxEvent += GridControlView_ExportToXlsxEvent;
            //((ListViewModelBase)DataContext).SaveFilterEvent += GridControlView_SaveFilterEvent;
            //((ListViewModelBase)DataContext).LoadFilterEvent += GridControlView_LoadFilterEvent;
            ((ListViewModelBase)DataContext).FilterFromGridSelection += GridControlView_FilterFromGridSelection;

            ((ListViewModelBase)DataContext).SaveGridLayoutEvent += GridControlView_SaveGridLayoutEvent;
        }



        private void MapDataPasser_MapSelectionChangedEvent(object sender, EventArgs e)
        {
            if (MapDataPasser.SelectionFromGrid) return;
            //if (this.DataContext is WBIS_2.Modules.Interfaces.IMapNavigation || DataContext.GetType().BaseType == typeof(ActivitiesListViewModel))
            //{
            //    try
            //    {
            //        List<IFeature> selection = (List<IFeature>)sender;
            //        if (selection.Count == 0) return;

            //        LayerField layerField = new RMS3Model().LayerFields.FirstOrDefault(_ => _.Layer.ToUpper() == selection[0].ParentFeatureSet.Name.ToUpper());

            //        if (layerField == null) return;
            //        if (layerField.Layer == "ACTIVITIES" && !((RMSViewModelBase)DataContext).IsActivityList) return;

            //        GridColumn col = MyGrid.Columns.Where(_ => _.Header != null).FirstOrDefault(_ => _.Header.ToString() == layerField.Field);
            //        if (col == null) return;

            //        var selectedIDs = "";
            //        foreach (var feature in selection)
            //        {

            //            //if (feature.ParentFeatureSet.Name.ToLower() == mn.LayerName.ToLower())
            //            //{
            //            var selectedFeatureID = feature.DataRow[col.Header.ToString()];
            //            selectedIDs = selectedIDs + $"'{selectedFeatureID}',";
            //            //this.MyGrid.FilterString = $"[RegenID] IN '{selectedFeatureID}'";
            //            //break;
            //            //}
            //        }
            //        if (selectedIDs.Length > 0)
            //        {
            //            selectedIDs = selectedIDs.Remove(selectedIDs.Length - 1);
            //            string filter = $"[{col.FieldName}] IN ({selectedIDs})";
            //            if (filter != MyGrid.FilterString)
            //                this.MyGrid.FilterString = filter;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return;
            //    }
            //}
        }

      
        private void GridControl_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            if (SelectInProgress) return;
            // Messenger.Default.Send(this.MyGrid);

            var rows = this.MyGrid.GetSelectedRowHandles();
            if (MyGrid.SelectedItems == null)
            {
                return;
            }

            if ((MyGrid.Columns.Count == 1 && MyGrid.Columns[0].FieldName == "Message"))
                return;
            MyGrid.SelectedItems.Clear();
            AFS.ClearSelection();

            //if (rows.Count() == MyGrid.VisibleRowCount)
            //{
            //    SelectAll();
            //    return;
            //}



            //If not all rows are loaded then load all selected rows.
            foreach (int rowId in rows)
            {
                if (rowId < 0) continue;
                var row = MyGrid.GetRow(rowId);

                if (row is DevExpress.Data.NotLoadedObject)
                {
                    AsyncLoadRows(rows);
                    return;
                }
                else
                {
                    MyGrid.SelectedItems.Add(row);
                }
            }

            FinishSelectionChanged();
        }

        private async void AsyncLoadRows(int[] rows)
        {
            bool Disabled = true;
            try
            { Application.Current.MainWindow.IsEnabled = false; }
            catch (Exception)
            { Disabled = false; }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            MyGrid.SelectedItems.Clear();
            await GetRows(rows);

            if (Disabled)
                Application.Current.MainWindow.IsEnabled = true;

            FinishSelectionChanged();

            w.Stop();
        }

        private void FinishSelectionChanged()
        {
            if (DataContext is WBIS_2.Modules.Interfaces.IMapNavigation)
                ((WBIS_2.Modules.Interfaces.IMapNavigation)DataContext).ZoomToLayer();

            MyGrid.SelectedItem = MyGrid.GetFocusedRow();
        }

        private async Task GetRows(int[] rows)
        {
            foreach (int rowId in rows)
            {
                var row = await MyGrid.GetRowAsync(rowId);
                MyGrid.SelectedItems.Add(row);
            }
        }


        private void GridControlView_LoadFilterEvent(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RmsFilter|*.RmsFilter";
            if (ofd.ShowDialog().Value)
            {
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    MyGrid.FilterString = sr.ReadToEnd();
                }
            }
        }

        private void GridControlView_SaveFilterEvent(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "RmsFilter|*.RmsFilter";
            if (sfd.ShowDialog().Value)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.Write(MyGrid.FilterString);
                }
            }
        }


        private void GridControlView_ExportToXlsxEvent(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XLSX|*.xlsx";
            if (sfd.ShowDialog().Value)
            {
                MyView.ExportToXlsx(sfd.FileName);
                if (MessageBox.Show("Would you like to open your export?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
            }
        }







        private void MyView_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var hitInfo = MyView.CalcHitInfo(e.GetPosition(MyView));

            if (hitInfo.HitTest == TableViewHitTest.ColumnButton)
            {
                MyGrid.SelectAll();
            }
            else if (hitInfo.HitTest == TableViewHitTest.RowIndicator)
            {
                MyView.FocusedRowHandle = hitInfo.RowHandle;
            }
        }








        private void GridControlView_FilterFromGridSelection(object sender, EventArgs e)
        {
            string selectedIDs = "";
            foreach (var r in MyGrid.SelectedItems)
            {
                var valType = r.GetType();
                var guidProperty = valType.GetProperty("Guid");
                var newVal = guidProperty.GetValue(r, null);

                selectedIDs = selectedIDs + $"'{newVal}',";
            }

            if (selectedIDs.Length > 0)
            {
                selectedIDs = selectedIDs.Remove(selectedIDs.Length - 1);
                this.MyGrid.FilterString = $"[Guid] IN ({selectedIDs})";
            }
        }

        AlternativeFocusingSelection AFS;
        private void AFS_AfsMapEvent(object sender, EventArgs e)
        {
            if (DataContext is WBIS_2.Modules.Interfaces.IMapNavigation)
                ((WBIS_2.Modules.Interfaces.IMapNavigation)DataContext).MapShowAFS(AFS.Selection);
        }



        private void TableView_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            if (e.HitInfo.Column == null)
            {
                if (DataContext is WBIS_2.Modules.Interfaces.IMapNavigation)
                    ((WBIS_2.Modules.Interfaces.IMapNavigation)DataContext).ZoomToFeature(MyView.FocusedRow);
            }
            else
            {
                ((ListViewModelBase)DataContext).ShowDetails();
            }
        }
        public void OnFocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (MyGrid.IsSubSelection && DataContext is WBIS_2.Modules.Interfaces.IMapNavigation)
            {
                string unitCol = ((WBIS_2.Modules.Interfaces.IMapNavigation)DataContext).LayerKeyField;
                unitCol = MyGrid.Columns.First(_ => _.FieldName.EndsWith(unitCol)).FieldName;

                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    AFS.AddReplaceSelection(MyView.FocusedRowHandle, true, unitCol);
                }
                else if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    int low = 0;
                    int high = MyView.FocusedRowHandle;
                    if (e.OldRow != null) low = MyGrid.OldFocusedHandle;
                    if (low > high)
                    {
                        int temp = low;
                        low = high;
                        high = temp;
                    }
                    for (int i = low; i < high + 1; i++)
                    {
                        AFS.AddReplaceSelection(i, false, unitCol);
                    }
                }
                else
                {
                    AFS.ClearSelection();
                    AFS.AddReplaceSelection(MyView.FocusedRowHandle, false, unitCol);
                }
                AFS.RefreshRows();
            }
            MyGrid.IsSubSelection = false;
        }

        private void MyView_CustomRowAppearance(object sender, CustomRowAppearanceEventArgs e)
        {
            //https://docs.devexpress.com/WPF/116513/controls-and-libraries/data-grid/conditional-formatting/conditional-formats/formatting-focused-cells-and-rows

            var test = MyGrid.GetCellValue(e.RowHandle, "Guid");
            if (test is DevExpress.Data.NotLoadedObject)
            {
                e.Handled = false;
            }
            else
            {
                if (test == null)
                    e.Handled = false;
                else if (AFS.Selection.ContainsKey((Guid)test))
                {
                    if (e.Property.Name == "Background")
                    {
                        e.Result = Brushes.LightYellow;
                        e.Handled = true;
                    }
                    else if (e.Property.Name == "Foreground")
                    {
                        e.Result = Brushes.Black;
                        e.Handled = true;
                    }
                    else e.Handled = false;
                }
                else
                    e.Handled = false;
            }
        }

        private void MyGrid_FilterChanged(object sender, RoutedEventArgs e)
        {
            AFS.ClearSelection();
            //SubSelection.Clear();
        }


    }
}