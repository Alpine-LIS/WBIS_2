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

namespace WBIS_2.Modules.ViewModels
{

    [POCOViewModel]
    public abstract class ListViewModelBase : WBISViewModelBase
    {
        protected ListViewModelBase()
        {
            RefreshDataSource();

            CurrentUser.CurrentUserChanged += CurrentUserChanged;

            SelectedItems = new ObservableCollection<object>();
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;

            ShowDetailsCommand = new DelegateCommand(ShowDetails);
            AddRecordCommand = new DelegateCommand(AddRecord);
            DeleteRecordCommand = new DelegateCommand(DeleteRecord);
            RecordsRefreshCommand = new DelegateCommand(RecordsRefresh);
        }

        public EntityInstantFeedbackSource Records { get; set; }
        internal virtual void RefreshDataSource()
        {
            Records = new EntityInstantFeedbackSource
            {
                AreSourceRowsThreadSafe = true,
                KeyExpression = $"Guid",
            };
            Records.GetQueryable += Records_GetQueryable;
            Records.Refresh();
            RaisePropertyChanged(nameof(Records));
        }
        public abstract void Records_GetQueryable(object sender, GetQueryableEventArgs e);

        private void CurrentUserChanged(object sender, EventArgs e)
        {
            RefreshDataSource();
        }


        private bool _viewDeleted;
        public bool ViewDeleted
        {
            get { return _viewDeleted; }
            set
            {
                if (_viewDeleted != value)
                {
                    _viewDeleted = value;
                    RefreshDataSource();
                }
            }
        }


        public IInfoTypeManager ListManager
        {
            get
            {
                return GetProperty(() => ListManager);
            }
            set
            {
                SetProperty(() => ListManager, value);
                SetRecordOptions();
            }
        }

        public string TableName => ListManager == null? "" : ListManager.DisplayName;


        private void SetRecordOptions()
        {            
                AddRecordsEnabled = ListManager.ImportRecords || ListManager.AddRecord;
                AddRecordsEnabled = ListManager.DeleteRecord;
                AddRecordsEnabled = ListManager.RestoreRecord;
                //if (ro.AddRecord && ro.ImportRecords)
                //    AddRecordToolTip = "Add/Import Record(s)";
                //else if (ro.AddRecord)
                //    AddRecordToolTip = "Add Record";
                //else
                    AddRecordToolTip = "Import Record(s)";

            RaisePropertyChanged(nameof(AddRecordsEnabled));
            RaisePropertyChanged(nameof(DeleteRecordsEnabled));
            RaisePropertyChanged(nameof(RestoreRecordsEnabled));
            RaisePropertyChanged(nameof(AddRecordToolTip));
        }



        public bool AddRecordsEnabled { get; set; }
        public string AddRecordToolTip { get; set; }
        public ICommand AddRecordCommand { get; set; }
        public abstract void AddRecord();

        public bool DeleteRecordsEnabled { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public abstract void DeleteRecord();

        public bool RestoreRecordsEnabled { get; set; }
        public ICommand RestoreRecordCommand { get; set; }
        public abstract void RestoreRecord();


        public ICommand ShowDetailsCommand { get; set; }
        public abstract void ShowDetails();
        public ICommand RecordsRefreshCommand { get; set; }
        public void RecordsRefresh()
        {
            Records.Refresh();
            RaisePropertyChanged(nameof(Records));
        }
        public bool ToggleAutoZoom { get; set; } = true;


        public ObservableCollection<object> SelectedItems { get; set; }
        private void SelectedItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectionChanged();
        }
        public abstract void SelectionChanged();


        public ICommand ExportToXlsxCommand { get; set; }
        public event EventHandler ExportToXlsxEvent;
        public void ExportToXlsxClick()
        {
            ExportToXlsxEvent?.Invoke(new object(), new EventArgs());
        }

        public ICommand FilterFromGridSelectionCommand { get; set; }
        public event EventHandler FilterFromGridSelection;
        private void FilterFromGridSelectionClick()
        {
            FilterFromGridSelection?.Invoke(new object(), new EventArgs());
        }



        public event EventHandler SaveGridLayoutEvent;
        public void SaveGridLayout()
        {
            //if (DontSaveLayout || Tracker.ChangesSaving) return;
            //SaveGridLayoutEvent?.Invoke(new object(), new EventArgs());
        }
    }
}

