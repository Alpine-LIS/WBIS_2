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
            Tracker.ChangesSaved += Tracker_ChangesSaved;
            CurrentUser.CurrentUserChanged += CurrentUserChanged;
            ClosingFormCommand = new DelegateCommand(CloseForm);
            //SaveCommand = new DelegateCommand(Save);

            Privileges();
            ModuleManager.DefaultManager.GetEvents(viewModel: this).ViewModelRemoving += WBISViewModelBase_ViewModelRemoving;
        }

        public virtual void WBISViewModelBase_ViewModelRemoving(object? sender, ViewModelRemovingEventArgs e)
        {
            Tracker.ChangesSaved -= Tracker_ChangesSaved;
            CurrentUser.CurrentUserChanged -= CurrentUserChanged;
        }

        public event EventHandler RecordSaved;
        protected virtual void OnRecordSaved(object sender, EventArgs e)
        {
            // Safely raise the event for all subscribers
            RecordSaved?.Invoke(this, e);
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
              

       

        public abstract void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e);      
        public virtual void CurrentUserChanged(object sender, EventArgs e)
        {
            Privileges();
        }

        //public event EventHandler ViewDeletedChanged;
       


           
        private bool _AdminUser = false;
        public bool AdminUser
        {
            get { return _AdminUser; }
            set
            {
                if (_AdminUser != value) _AdminUser = value;
            }
        }

        public void Privileges()
        {
            if (CurrentUser.User == null) return;

            AdminUser = CurrentUser.AdminPrivileges;
            RaisePropertyChanged(nameof(AdminUser));
        }

     
       
       
        public ICommand ClosingFormCommand { get; set; }
        public abstract void CloseForm();

       
        public IDocumentOwner DocumentOwner { get; set; }

       


        public bool HasPhotos { get; set; } = false;
        public ICommand ViewPhotosCommand { get; set; }


        public virtual void OnClose(CancelEventArgs e)
        {
            if (Changed)
            {
                var result = ThemedMessageBox.Show(title: "Confirmation", text: UnsavedMessageText, messageBoxButtons: MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }


        //    private string _TableName = "";
        //    public string TableName
        //    {
        //        get => _TableName;
        //        set
        //        {
        //            if (_TableName != value)
        //            {
        //                _TableName = value;
        //                if (_TableName == null)
        //                    RestoreGridColumnDefaultVisable = false;
        //                else if (_TableName == "")
        //                    RestoreGridColumnDefaultVisable = false;
        //                else
        //                    RestoreGridColumnDefaultVisable = true;
        //                RaisePropertyChanged(nameof(RestoreGridColumnDefaultVisable));
        //            }
        //        }
        //    }
        //    public bool RestoreGridColumnDefaultVisable { get; set; }
        //    public ICommand RestoreGridColumnDefaultCommand { get; set; }
        //    bool DontSaveLayout = false;
        //    public void DoRestoreGridColumnDefault()
        //    {
        //        DontSaveLayout = true;
        //        string path = @$"{AppDomain.CurrentDomain.BaseDirectory}\GridLayouts";
        //        if (File.Exists($@"{ path}\{ TableName}.xml")) File.Delete($@"{ path}\{ TableName}.xml");
        //        RefreshDataSource();
        //        DontSaveLayout = false;
        //    }
    }


   
}

