using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WBIS_2.DataModel;
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
                //MapDataPasser.MapSelectionChangedEvent += MapDataPasser_MapSelectionChangedEvent;
                DataContextChanged += GridControlView_DataContextChanged;
                //MyGrid.DefaultSorting = "DateTime";
            }

            private void GridControlView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                if (DataContext == null) return;
                DataContextChanged -= GridControlView_DataContextChanged;

                ((WBISViewModelBase)DataContext).SaveFilterEvent += GridControlView_SaveFilterEvent;
                ((WBISViewModelBase)DataContext).LoadFilterEvent += GridControlView_LoadFilterEvent;
            }



            Dictionary<GridColumn, int> addColumns;
            private void GridControl_ItemsSourceChanged(object sender, DevExpress.Xpf.Grid.ItemsSourceChangedEventArgs e)
            {


                MyGrid.View.ShowFixedTotalSummary = true;
                MyGrid.TotalSummary.Clear();
                MyGrid.GroupSummary.Clear();

                //if (addColumns != null)
                //{
                //    foreach (GridColumn c in addColumns.Keys)
                //    {
                //        MyGrid.Columns.Remove(c);
                //    }
                //    addColumns = null;
                //}
               // DisplayFileds(MyGrid);

                //MyGrid.TotalSummary.Add(new GridSummaryItem() { SummaryType = DevExpress.Data.SummaryItemType.Count, FieldName = "Guid", DisplayFormat = "Records: {0:n0}", Alignment = GridSummaryItemAlignment.Right });
                //MyGrid.GroupSummary.Add(new GridSummaryItem() { SummaryType = DevExpress.Data.SummaryItemType.Count, FieldName = "Guid", DisplayFormat = "Records: {0:n0}" });

                //foreach (var c in MyGrid.Columns)
                //{
                //    if (c.FieldName.ToUpper().Contains("GUID")) c.Visible = false;
                //    c.ColumnFilterMode = ColumnFilterMode.DisplayText;// = DevExpress.Utils.DefaultBoolean.True;
                //    c.AllowIncrementalSearch = true;
                //}
            }
            private void DisplayFileds(GridControl gc)
            {
                bool ShowDateTimes = ((WBISViewModelBase)this.DataContext).ShowDateTimes;
                addColumns = new Dictionary<GridColumn, int>();

                foreach (var col in gc.Columns)
                {
                    if (col.FieldType.Namespace == "WBIS_2.DataModel")
                    {
                        var colValues = DisplayValues(col);
                        if (colValues != null)
                        {
                            for (int i = 0; i < colValues.Count; i++)
                            {
                                var colVal = colValues[i];
                                GridColumn c = new GridColumn() { FieldName = $"{colVal.Value}.{colVal.Key}", Header = colVal.Key };
                                c.VisibleIndex = col.VisibleIndex;
                                addColumns.Add(c, col.VisibleIndex);
                            }
                        }
                        col.Visible = false;
                    }
                    else
                    {
                        col.Header = col.FieldName;
                        StandardColumnFormat(col, ShowDateTimes, gc);
                    }
                }

                foreach (var addCol in addColumns)
                {
                    gc.Columns.Insert(addCol.Value, addCol.Key);
                }
            }

            private void StandardColumnFormat(GridColumn col, bool ShowDateTimes, GridControl gc)
            {
                //if (col.FieldName.ToString().Contains("Cost"))
                //{
                //    col.EditSettings = new TextEditSettings() { DisplayFormat = "C2" };
                //    if (col.FieldName == "TotalCost")
                //    {
                //        //if (gc.TotalSummary.Count == 0)
                //        //{
                //        gc.TotalSummary.Add(new GridSummaryItem() { SummaryType = DevExpress.Data.SummaryItemType.Sum, FieldName = "TotalCost", DisplayFormat = "Total Cost: {0:c2}", Alignment = GridSummaryItemAlignment.Right });
                //        gc.GroupSummary.Add(new GridSummaryItem() { SummaryType = DevExpress.Data.SummaryItemType.Sum, FieldName = "TotalCost", DisplayFormat = "Total Cost: {0:c2}" });
                //        //}
                //    }
                //}
                //else if (col.FieldName.ToString() == "Volume" || col.FieldName.ToString().Contains("Acre"))
                //{
                //    col.EditSettings = new TextEditSettings() { DisplayFormat = "N2" };
                //}
                //else if (col.FieldName.ToString() == "Lat" || col.FieldName.ToString() == "Lon")
                //{
                //    col.EditSettings = new TextEditSettings() { DisplayFormat = "N5" };
                //}
                //else if (col.FieldType == typeof(DateTime) && ShowDateTimes)
                //{
                //    col.EditSettings = new DateEditSettings() { DisplayFormat = "MM/dd/yyyy HH:mm:ss" };
                //}
                ////else if (col.FieldName == "Comments") col.Visible = false;
                //else if (col.FieldName == "_delete") col.VisibleIndex = 100;// = false;
            }

            //Columns should show up in order of how they'll be displayed
            private List<KeyValuePair<string, string>> DisplayValues(GridColumn col)
            {
                if (col.ActualVisibleIndex == -1) return null;
                return null;
            }

        //Patch for EntityInstantFeedbackSource
        private async void GridControl_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            var rows = this.MyGrid.GetSelectedRowHandles();
            if (MyGrid.SelectedItems == null)
            {
                return;
            }
            MyGrid.SelectedItems.Clear();

            //bool Disabled = true;
            //try
            //{ Application.Current.MainWindow.IsEnabled = false; }
            //catch (Exception)
            //{ Disabled = false; }

            //WaitWindowHandler w = new WaitWindowHandler();
            //w.Start();


            await GetRows(rows);

            //if (Disabled)
            //    Application.Current.MainWindow.IsEnabled = true;

            if (DataContext is WBIS_2.Modules.Interfaces.IMapNavigation)
                if (((WBISViewModelBase)DataContext).ToggleAutoZoom)
                    ((WBIS_2.Modules.Interfaces.IMapNavigation)DataContext).ZoomToLayer();
            ((WBISViewModelBase)DataContext).UpdateSelection((IList<object>)MyGrid.SelectedItems);
           // w.Stop();
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
            private void MyView_RowDoubleClick(object sender, RowDoubleClickEventArgs e)
            {
                if (e.HitInfo.Column == null)
                {
                    if (DataContext is WBIS_2.Modules.Interfaces.IMapNavigation)
                        ((WBIS_2.Modules.Interfaces.IMapNavigation)DataContext).ZoomToLayer();
                }
                else
                {
                    ((WBISViewModelBase)DataContext).ShowDetails();
                }
            }
        }
}
