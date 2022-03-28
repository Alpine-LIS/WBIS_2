﻿using DevExpress.Data.Linq;
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
    public abstract class WBISViewModelBase : ViewModelBase, IDocumentModule, ISupportState<WBISViewModelBase.Info>, IDisposable, IDataErrorInfo
    {
        public string UnsavedMessageText
        {
            get
            {
                return "Are you sure to close the unsaved document?";
            }
        }
        public string ChangedSign
        {
            get
            {
                if (Changed)
                {
                    return "*";
                }
                return "";
            }
        }

        private bool _changed;
        public bool Changed
        {
            get
            {
                return _changed;
            }
            set
            {
                _changed = value;
                RaisePropertyChanged("Title");
                RaisePropertyChanged("Changed");
            }
        }
        protected WBIS2Model _database { get; set; }
        public WBIS2Model Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new WBIS2Model();
                }
                return _database;
            }
        }
        public virtual string Caption { get; set; }
        public virtual string Content { get; set; }
        ~WBISViewModelBase()
        {
            Dispose();
        }
        protected WBISViewModelBase()
        {
            RefreshDataSource();

            Records.AreSourceRowsThreadSafe = true;
            SelectedItems = new List<object>();

            Tracker.ChangesSaved += Tracker_ChangesSaved;
            Tracker.ChangesSaved += Tracker_ChangesSavedUser;
            CurrentUser.CurrentUserChanged += CurrentUserChanged;

            ShowDetailsCommand = new DelegateCommand(ShowDetails);
            SaveCommand = new DelegateCommand(Save);
            AddRecordCommand = new DelegateCommand(AddRecord);
            DeleteRecordCommand = new DelegateCommand(DeleteRecord);
            RecordsRefreshCommand = new DelegateCommand(RecordsRefresh);
            ClosingFormCommand = new DelegateCommand(CloseForm);
            ViewEditsCommand = new DelegateCommand(ViewEdits);
   
            SaveFilterCommand = new DelegateCommand(SaveFilterClick);
            LoadFilterCommand = new DelegateCommand(LoadFilterClick);


            ManageRequiredPassesCommand = new DelegateCommand(ManageRequiredPassesClick);
            RemoveRequiredPassesCommand = new DelegateCommand(RemoveRequiredPassesClick);
            DropHexagonsCommand = new DelegateCommand(DropHexagonsClick);

        Privileges();

        }



        #region Serialization
        [Serializable]
        public class Info
        {
            public string Content { get; set; }
            public string Caption { get; set; }
        }
        Info ISupportState<Info>.SaveState()
        {
            return new Info()
            {
                Content = this.Content,
                Caption = this.Caption,
            };
        }

        void ISupportState<Info>.RestoreState(Info state)
        {
            this.Content = state.Content;
            this.Caption = state.Caption;
        }
        public void Dispose()
        {
            Database.DisposeAsync();
        }
        #endregion


        #region "Validation"
        List<string> Errors = new List<string>();
        public string this[string columnName]
        {
            get
            {
                string ErrorStr = IDataErrorInfoHelper.GetErrorText(this, columnName);
                if (ErrorStr == "") Errors.Remove(columnName);
                else if (!Errors.Contains(columnName)) Errors.Add(columnName);
                return ErrorStr;
            }
        }
        public string Error { get { return string.Empty; } }
        public bool HasErrors() { return Errors.Count > 0; }
        #endregion

        public event EventHandler RecordSaved;
        protected virtual void OnRecordSaved(object sender, EventArgs e)
        {
            // Safely raise the event for all subscribers
            RecordSaved?.Invoke(this, e);
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
            Records.DismissQueryable += Records_DismissQueryable;
            Records.Refresh();
            RaisePropertyChanged(nameof(Records));
        }
        public abstract void Records_GetQueryable(object sender, GetQueryableEventArgs e);
        internal void Records_DismissQueryable(object sender, GetQueryableEventArgs e)
        {

        }


        public abstract void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e);
        private void Tracker_ChangesSavedUser(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            RecordSaved?.Invoke(this, new EventArgs());
            if (e.Any(_ => _.State != EntityState.Unchanged && _.Entity is ApplicationUser))
            {
                RefreshDataSource();
            }
        }
        private void CurrentUserChanged(object sender, EventArgs e)
        {
            Privileges();
            RefreshDataSource();
        }

        //public event EventHandler ViewDeletedChanged;
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


        #region "User control Visibilities/Formats"
        //Should times be shown with date?
        bool isSupport = false;
        public virtual bool IsSupport
        {
            get
            {
                return isSupport;
            }
            set
            {
                isSupport = value;
            }
        }
        public bool ShowDateTimes { get; set; } = false;

    
        private bool _AdminUser = false;
        public bool AdminUser
        {
            get { return _AdminUser; }
            set
            {
                if (_AdminUser != value) _AdminUser = value;
            }
        }



      


        public bool LockedRecord { get; set; }

     
        public bool IsReadonlyEditField { get; set; }
        public bool EnableDetailMenuItems { get; set; }
        public void Privileges()
        {
            if (CurrentUser.User == null) return;

            EnableDetailMenuItems = !LockedRecord;
            RaisePropertyChanged(nameof(EnableDetailMenuItems));
            IsReadonlyEditField = LockedRecord;
            RaisePropertyChanged(nameof(IsReadonlyEditField));

            AdminUser = CurrentUser.AdminPrivileges;
            RaisePropertyChanged(nameof(AdminUser));

            AddRecordToolTip = "Add new (insert)";
            RaisePropertyChanged(nameof(AddRecordToolTip));
        }
        #endregion

        #region "Commands"
        public string AddRecordToolTip { get; set; }
        public ICommand ViewEditsCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand AddRecordCommand { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public ICommand RecordsRefreshCommand { get; set; }
        public ICommand ClosingFormCommand { get; set; }
        
        public ICommand SaveFilterCommand { get; set; }
        public event EventHandler SaveFilterEvent;
        public void SaveFilterClick()
        {
            SaveFilterEvent?.Invoke(new object(), new EventArgs());
        }
        public ICommand LoadFilterCommand { get; set; }
        public event EventHandler LoadFilterEvent;
        public void LoadFilterClick()
        {
            LoadFilterEvent?.Invoke(new object(), new EventArgs());
        }


        public abstract void Save();
        public abstract void ShowDetails();
        public abstract void AddRecord();
        public abstract void DeleteRecord();
        public void RecordsRefresh()
        {
            Records.Refresh();
            RaisePropertyChanged(nameof(Records));
        }
        public IDocumentOwner DocumentOwner { get; set; }
        public abstract void CloseForm();
        public abstract void ViewEdits();
        #endregion

       


        public bool ToggleAutoZoom { get; set; } = true;
        public bool HasPhotos { get; set; } = false;
        public ICommand ViewEvaluationPhotosCommand { get; set; }
        public bool IsActive { get; set; }



        public event EventHandler SelectionUpdated;
        public void UpdateSelection(IList<object> newSelection)
        {
            SelectedItems = newSelection;
            RaisePropertyChanged(nameof(SelectedItems));
            SelectionUpdated?.Invoke(new object(), new EventArgs());
        }
        private IList<object> _SelectedItems;
        public IList<object> SelectedItems
        {
            get
            {
                return _SelectedItems;
            }
            set
            {
                if (_SelectedItems != value)
                {
                    _SelectedItems = value;
                }
            }
        }


        public bool IsDistrictList { get; set; } = false;
        public bool IsQuad75List { get; set; } = false;
        public bool IsWatershedList { get; set; } = false;
        public bool IsHex160List { get; set; } = false;
        public bool IsHex160PassesList { get; set; } = false;
        public bool IsSiteCallingList { get; set; } = false;
        public bool IsCnddbOccurrenceList { get; set; } = false;
        public bool IsCdfwSpottedOwlList { get; set; } = false;
        public void SetListType(Type type)
        {            
            IsDistrictList = (type == typeof(District));
            IsQuad75List = (type == typeof(Quad75));
            IsWatershedList = (type == typeof(Watershed));
            IsHex160List = (type == typeof(Hex160));
            IsHex160PassesList = (type == typeof(Hex160RequiredPass));
            IsSiteCallingList = (type == typeof(SiteCalling));
            IsCnddbOccurrenceList = (type == typeof(CNDDBOccurrence));
            IsCdfwSpottedOwlList = (type == typeof(CDFW_SpottedOwl));

            RaisePropertyChanged(nameof(IsDistrictList));
            RaisePropertyChanged(nameof(IsQuad75List));
            RaisePropertyChanged(nameof(IsWatershedList));
            RaisePropertyChanged(nameof(IsHex160List));
            RaisePropertyChanged(nameof(IsHex160PassesList));
            RaisePropertyChanged(nameof(IsSiteCallingList));
            RaisePropertyChanged(nameof(IsCnddbOccurrenceList));
            RaisePropertyChanged(nameof(IsCdfwSpottedOwlList));
        }



        public ICommand ManageRequiredPassesCommand { get; set; }
        public ICommand RemoveRequiredPassesCommand { get; set; }
        public ICommand DropHexagonsCommand { get; set; }
        private void ManageRequiredPassesClick()
        {
            ManageRequiredPassesView manageRequiredPassesView = new ManageRequiredPassesView();
            manageRequiredPassesView.DataContext = new ManageRequiredPassesViewModel(SelectedItems.Cast<Hex160>().ToArray());
            CustomControlWindow userWindow = new CustomControlWindow(manageRequiredPassesView);
        }
        private void RemoveRequiredPassesClick()
        {
            Hex160RequiredPass[] hex160RequiredPasses = Database.Hex160RequiredPasses
                .Include(_ => _.Hex160)
                .Where(_ => SelectedItems.Cast<Hex160>().ToArray().Contains(_.Hex160)).ToArray();
            foreach (Hex160RequiredPass hex160RequiredPass in hex160RequiredPasses)
                Database.Hex160RequiredPasses.Remove(hex160RequiredPass);
            Database.SaveChanges();
            MessageBox.Show("Operation complete.");
        }
        private void DropHexagonsClick()
        {



        }




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
            if (DontSaveLayout || Tracker.ChangesSaving) return;
            SaveGridLayoutEvent?.Invoke(new object(), new EventArgs());
        }

        private string _TableName = "";
        public string TableName
        {
            get => _TableName;
            set
            {
                if (_TableName != value)
                {
                    _TableName = value;
                    if (_TableName == null)
                        RestoreGridColumnDefaultVisable = false;
                    else if (_TableName == "")
                        RestoreGridColumnDefaultVisable = false;
                    else
                        RestoreGridColumnDefaultVisable = true;
                    RaisePropertyChanged(nameof(RestoreGridColumnDefaultVisable));
                }
            }
        }
        public bool RestoreGridColumnDefaultVisable { get; set; }
        public ICommand RestoreGridColumnDefaultCommand { get; set; }
        bool DontSaveLayout = false;
        public void DoRestoreGridColumnDefault()
        {
            DontSaveLayout = true;
            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}\GridLayouts";
            if (File.Exists($@"{ path}\{ TableName}.xml")) File.Delete($@"{ path}\{ TableName}.xml");
            RefreshDataSource();
            DontSaveLayout = false;
        }

        private bool _ViewSpecificRecord = false;
        public bool ViewSpecificRecord
        {
            get { return _ViewSpecificRecord; }
            set
            {
                _ViewSpecificRecord = value;
                RefreshDataSource();
                RaisePropertyChanged(nameof(Records));
            }
        }
    }


    public class ColumnVisClass : BindableBase
    {
        public WBISViewModelBase WBISViewModelBase { get; set; }
        public ColumnVisClass(WBISViewModelBase wBISViewModelBase) => WBISViewModelBase = wBISViewModelBase;
        private bool _IsVisable;
        public bool IsVisable
        {
            get => _IsVisable;
            set
            {
                if (_IsVisable != value)
                {
                    _IsVisable = value;
                    SaveGridLayout();//RMSViewModelBase.SaveGridLayout();
                }
            }
        }
        private int _VisableIndex;
        public int VisableIndex
        {
            get => _VisableIndex;
            set
            {
                if (_VisableIndex != value)
                {
                    _VisableIndex = value;
                    SaveGridLayout();// RMSViewModelBase.SaveGridLayout();
                }
            }
        }
        private void SaveGridLayout()
        {
            //IsVisable = _IsVisable;
            //RaisePropertyChanged(nameof(IsVisable));
            //VisableIndex = _VisableIndex;
            //RaisePropertyChanged(nameof(VisableIndex));
            WBISViewModelBase.SaveGridLayout();
        }
    }
}

