using DevExpress.Xpf.Grid;
using WBIS_2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace WBIS_2.Modules.Views
{
    class AlternativeFocusingSelection
    {
        public Dictionary<Guid,string> Selection = new Dictionary<Guid, string>();
        private List<int> RowsToRefresh = new List<int>();
        GridControlEx GridControlEx { get; set; }
        public AlternativeFocusingSelection(GridControlEx gridControlEx) => GridControlEx = gridControlEx;
              

        public void AddReplaceSelection(int rowHandle, bool ShouldRemove, string unitCol)
        {
            var test = GridControlEx.GetCellValue(rowHandle, "Guid");
            if (test is DevExpress.Data.NotLoadedObject) return;
            if (!Selection.ContainsKey((Guid)test))
            {
                var unit = GridControlEx.GetCellValue(rowHandle, unitCol);
                Selection.Add((Guid)test,(string)unit);
            }
            else if (ShouldRemove)
                Selection.Remove((Guid)test);
            RowsToRefresh.Add(rowHandle);
        }
        public event EventHandler AfsMapEvent;
        public void RefreshRows()
        {
            foreach (int row in RowsToRefresh) GridControlEx.RefreshRow(row);
            RowsToRefresh.Clear();

            //MapDataPasser.ZoomKeyValues = Selection.Cast<IInformationType>().ToList();
            AfsMapEvent?.Invoke(new object(), new EventArgs());

        }
        public void ClearSelection()
        {
            foreach (Guid guid in Selection.Keys)
                RowsToRefresh.Add(GridControlEx.FindRowByValue("Guid", guid));
            Selection.Clear();
            RefreshRows();
        }
    }
    public class FocusHelper
    {
        DispatcherTimer dispatcherTimer;
        bool doubleClicked = false;
        TableView view;
        public TableView View
        {
            get
            {
                return view;
            }
            set
            {
                if (value == view) return;
                view = value;
                SubscribeView();
            }
        }
        public event FocusedRowChangedEventHandler FocusedRowChanged;
        public event RowDoubleClickEventHandler RowDoubleClicked;

        public FocusHelper()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            dispatcherTimer.Tick += (o, eArg) => {
                dispatcherTimer.Stop();
                if (!doubleClicked && FocusedRowChanged != null)
                    FocusedRowChanged(o, dispatcherTimer.Tag as FocusedRowChangedEventArgs);
            };
        }
        private void SubscribeView()
        {
            if (View == null) return;
            View.RowDoubleClick += (o, e) => {
                doubleClicked = true;
                if (RowDoubleClicked != null) RowDoubleClicked(o, e);
            };
            View.FocusedRowChanged += (o, e) => {
                dispatcherTimer.Tag = e;
                doubleClicked = false;
                dispatcherTimer.Start();
            };
        }
    }


    public class GridControlEx : GridControl
    {
        

        public bool IsSubSelection = false;
        public int OldFocusedHandle;
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var hitInfo = ((TableView)View).CalcHitInfo(e.GetPosition(((TableView)View)));
            if (hitInfo.HitTest == TableViewHitTest.RowIndicator)
            {
                //base.OnPreviewMouseLeftButtonDown(e);
                OldFocusedHandle = ((TableView)View).FocusedRowHandle;
                IsSubSelection = true;
                return;
            }
            IsSubSelection = false;
            base.OnPreviewMouseLeftButtonDown(e);
        }
        protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e)
        {
            IsSubSelection = false;
            base.OnPreviewMouseDoubleClick(e);            
        }
        public void SelectionChangedManual()
        {
            base.RaiseSelectionChanged(new GridSelectionChangedEventArgs(this.View, System.ComponentModel.CollectionChangeAction.Refresh, 0));
        }
    }

    public static class UnitsActivitiesPasser
    {
        public static bool RegenActivities;
        public static bool FilterNeeded = false;
        public static List<string> UnitIds = null;
        public static event EventHandler NewFilterActivities;
        public static event EventHandler NewFilterUnits;
        public static void ApplyNewFilterActivities()
        {
            NewFilterActivities?.Invoke(new object(), new EventArgs());
        }
        public static void ApplyNewFilterUnits()
        {
            NewFilterUnits?.Invoke(new object(), new EventArgs());
        }
    }
}