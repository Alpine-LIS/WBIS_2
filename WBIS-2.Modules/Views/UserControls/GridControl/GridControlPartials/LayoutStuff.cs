﻿using Atlas.Controls;
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

namespace WBIS_2.Modules.Views
{
    public partial class GridControlView : UserControl
    {
        bool DontSaveLayout = false;
        //private void Columns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    SaveLayout();
        //}

        string LayoutPath = @$"{AppDomain.CurrentDomain.BaseDirectory}\GridLayouts";
        private void GridControlView_SaveGridLayoutEvent(object sender, EventArgs e)
        {
            if (DontSaveLayout) return;
            string tableName = ((ListViewModelBase)DataContext).TableName;
            if (tableName == null) return;
            if (tableName == "") return;

            if ((MyGrid.Columns.Count == 1 && MyGrid.Columns[0].FieldName == "Message"))
                return;
            if (MyGrid.IsAsyncOperationInProgress) return;

            if (File.Exists($@"{ LayoutPath}\{ tableName}.xml")) File.Delete($@"{ LayoutPath}\{ tableName}.xml");

            foreach (var c in MyGrid.Columns)
            {
                var binding = BindingOperations.GetBinding(c, GridColumn.VisibleProperty);
                if (binding == null) continue;
                ColumnVisClass columnVisClass = (ColumnVisClass)binding.Source;
                c.VisibleIndex = columnVisClass.VisableIndex;
                c.Visible = columnVisClass.IsVisable;
            }
            if (!Directory.Exists(LayoutPath)) Directory.CreateDirectory(LayoutPath);
            MyGrid.SaveLayoutToXml($@"{ LayoutPath}\{ tableName}.xml");
        }
        private void TotalSummary_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            GridControlView_SaveGridLayoutEvent(new object(), new EventArgs());
        }

        Dictionary<GridColumn, int> addColumns;
        private void MyGrid_AutoGeneratedColumns(object sender, RoutedEventArgs e)
        {
            // ShowPassedREcords();
            SetColumnDisplay();
        }
        private void GridControl_ItemsSourceChanged(object sender, DevExpress.Xpf.Grid.ItemsSourceChangedEventArgs ee)
        {
            //ShowPassedREcords();
            MyGrid.FilterString = "";
            SetUnitsContextMenu();
            SetColumnDisplay();
        }
        //bool settingColProperties = false;
        private void SetColumnDisplay()
        {
            if (DontSaveLayout) return;
            DontSaveLayout = true;

            MyGrid.View.ShowFixedTotalSummary = true;
            MyGrid.TotalSummary.Clear();
            MyGrid.GroupSummary.Clear();

            if (addColumns != null)
            {
                foreach (GridColumn c in addColumns.Keys)
                {
                    MyGrid.Columns.Remove(c);
                }
                addColumns = null;
            }
            DisplayFileds(MyGrid);

            foreach (var c in MyGrid.Columns)
            {
                ColumnVisClass columnVisClass = new ColumnVisClass((ListViewModelBase)DataContext);
                columnVisClass.IsVisable = c.Visible;
                c.SetBinding(GridColumn.VisibleProperty, new Binding() { Path = new PropertyPath("IsVisable"), Source = columnVisClass, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                c.SetBinding(GridColumn.VisibleIndexProperty, new Binding() { Path = new PropertyPath("VisableIndex"), Source = columnVisClass, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

                //c.FilterPopupMode = FilterPopupMode.ExcelSmart;
            }

            string tableName = ((ListViewModelBase)DataContext).TableName;
            if (tableName == null) tableName = "";

            if (File.Exists($@"{ LayoutPath}\{ tableName}.xml"))
            {
                var filter = MyGrid.FilterString;
                MyGrid.RestoreLayoutFromXml($@"{ LayoutPath}\{ tableName}.xml");
                MyGrid.FilterString = filter;
            }
            else
            {
                MyGrid.TotalSummary.Add(new GridSummaryItem() { SummaryType = DevExpress.Data.SummaryItemType.Count, FieldName = "Guid", DisplayFormat = "Records: {0:n0}", Alignment = GridSummaryItemAlignment.Right });
                MyGrid.GroupSummary.Add(new GridSummaryItem() { SummaryType = DevExpress.Data.SummaryItemType.Count, FieldName = "Guid", DisplayFormat = "Records: {0:n0}" });

                Dictionary<string, int> ActivityColumns = new Dictionary<string, int>();
                foreach (var c in MyGrid.Columns)
                {
                    if (c.FieldName.ToUpper().Contains("GUID")) c.Visible = false;
                    //c.ColumnFilterMode = ColumnFilterMode.DisplayText;// = DevExpress.Utils.DefaultBoolean.True;
                    c.AllowIncrementalSearch = true;
                }
            }

            DontSaveLayout = false;
        }
            
        private void DisplayFileds(GridControl gc)
        {
            addColumns = new Dictionary<GridColumn, int>();
            if (((ListViewModelBase)DataContext).ListManager == null) return;

            foreach(var displayField in ((ListViewModelBase)DataContext).ListManager.DisplayFields)
            {
                if (gc.Columns.Any(_ => _.FieldName == displayField.FullName)) continue;
                GridColumn c = new GridColumn() { FieldName = $"{displayField.FullName }", Header = displayField.FieldName };
                addColumns.Add(c,0);
            }

            foreach (var col in gc.Columns)
            {
                //if (col.FieldType.GetInterfaces().Contains(typeof(IInformationType)) || col.FieldType.Namespace.Contains("WBIS_2.DataModel") || col.FieldType.Namespace.Contains("Alpine.FlexForms"))
                if (! col.FieldType.Namespace.StartsWith("System") || col.FieldType.Name.Contains("Collection"))
                {
                    //IInformationType instance = (IInformationType)Activator.CreateInstance(col.FieldType);
                    //var colValues = instance.Manager.DisplayFields;
                    //if (colValues != null)
                    //{
                    //    for (int i = 0; i < colValues.Count; i++)
                    //    {
                    //        var colVal = colValues[i];
                    //        //GridColumn c = new GridColumn() { FieldName = $"{colVal.Value}.{colVal.Key}", Header = colVal.Key };
                    //        GridColumn c = new GridColumn() { FieldName = $"{col.FieldName}.{colVal.Key}", Header = colVal.Key };
                    //        if (c.FieldName == "UpdateUser.UserName") c.Header = "UpdateUser";
                    //        c.VisibleIndex = col.VisibleIndex;
                    //        addColumns.Add(c, col.VisibleIndex);
                    //    }
                    //}
                    col.Visible = false;
                }
                else if (col.FieldType == typeof(Guid) || col.FieldType == typeof(Guid?) || col.FieldType.BaseType == typeof(NetTopologySuite.Geometries.Geometry))
                {
                    col.Visible = false;
                }
                else
                {
                    col.Header = col.FieldName;
                    StandardColumnFormat(col,  gc);
                }
            }

            foreach (var addCol in addColumns)
            {
                gc.Columns.Add(addCol.Key);
                //gc.Columns.Insert(addCol.Value, addCol.Key);
            }
        }

        private void StandardColumnFormat(GridColumn col, GridControl gc)
        {
            if (col.FieldName.ToUpper().Contains("ACRE"))
            {
                col.EditSettings = new TextEditSettings() { DisplayFormat = "N2" };
            }
            else if ((col.FieldName.ToUpper().Contains("LAT") || col.FieldName.ToUpper().Contains("LON")) && col.FieldType == typeof(double))
            {
                col.EditSettings = new TextEditSettings() { DisplayFormat = "N5" };
            }
            else if (col.FieldName == "_delete") col.VisibleIndex = 100;
            else if (col.FieldName.Contains("Time"))
            {
                col.EditSettings = new DateEditSettings() { DisplayFormat = "MM/dd/yyyy HH:mm" };
            }
        }

        //Columns should show up in order of how they'll be displayed
        private List<KeyValuePair<string, string>> DisplayValues(GridColumn col)
        {
             return null;
        }
    }
}