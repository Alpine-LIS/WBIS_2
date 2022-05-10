﻿using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.ModuleInjection;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using WBIS_2.Modules.Tools;

namespace WBIS_2.Modules.ViewModels
{
    public class ChildrenListViewModel : ListViewModelBase, IDocumentContent
    {
        public object Title
        {
            get { return $"{ParentType.Manager.DisplayName} Children"; }
        }
        public IInformationType ParentType
        {
            get
            {
                return GetProperty(() => ParentType);
            }
            set
            {
                SetProperty(() => ParentType, value);
                AvailibleChildren = ParentType.Manager.AvailibleChildren;
            }
        }

        public IInformationType[] AvailibleChildren { get; set; }
        public IInformationType CurrentChild
        {
            get
            {
                return GetProperty(() => CurrentChild);
            }
            set
            {
                if (CurrentChild != null) 
                    CurrentUser.AddRemoveInfoType(CurrentChild.Manager.DisplayName, false);
                if (value != null) 
                    CurrentUser.AddRemoveInfoType(value.Manager.DisplayName, true);

                SetProperty(() => CurrentChild, value);
                ListManager = CurrentChild.Manager;
                SelectedItems = new ObservableCollection<IInformationType>();
                RaisePropertyChanged(nameof(ListManager));
                RaisePropertyChanged(nameof(SelectedItems));
                RefreshDataSource();               
            }
        }
        public object[] ParentQuery
        {
            get
            {
                return GetProperty(() => ParentQuery);
                //this.Caption = $"Regens {SelectedItems.Count}";
            }
            set
            {
                SetProperty(() => ParentQuery, value);
                RefreshDataSource();
            }
        }


        public static ChildrenListViewModel Create(object[] parentQuery, IInformationType parentType)
        {
            return ViewModelSource.Create(() => new ChildrenListViewModel()
            {
                Caption = $"{parentType.Manager.DisplayName} Children",
                Content = $"{parentType.Manager.DisplayName} Children",
                ParentType = parentType,
                ParentQuery = parentQuery,
            });
        }

        public override void CloseForm()
        {
            DocumentOwner.Close(this);
            if (CurrentChild != null)
                CurrentUser.AddRemoveInfoType(CurrentChild.Manager.DisplayName, false);
        }

        public void OnClose(CancelEventArgs e)
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

        public void OnDestroy()
        {
            if (CurrentChild != null)
                CurrentUser.AddRemoveInfoType(CurrentChild.Manager.DisplayName, false);
        }

        protected ChildrenListViewModel()
        {
            //Tracker.ChangesSaved += Tracker_ChangesSaved;
            Privileges();
            RaisePropertyChanged(nameof(AvailibleChildren));
        }

        private void UpdateChildren(object sender, EventArgs e)
        {
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(CurrentChild.Manager.DisplayName + " Children");
            if (document != null)
            {
                var ChildrenView = (ChildrenListViewModel)document.Content;
                ChildrenView.ParentQuery = SelectedItems.ToArray();
                RaisePropertyChanged(nameof(ChildrenView));
            }
        }

        public void UpdateParentQuery(object[] parentQuery)
        {
            ParentQuery = parentQuery;
            RefreshDataSource();
        }

        public override void ShowChildren()
        {
            if (SelectedItems.Count == 0)
            {
                return;
            }

            //if (SelectedItems.Count == 1 && HasDetailView)
            //{
            //    base.ShowDetails();
            //    return;
            //}

            if (CurrentChild.Manager.AvailibleChildren.Count() > 0)
            {
                IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
                IDocument document = service.FindDocumentById(CurrentChild.Manager.DisplayName + " Children");
                if (document == null)
                {
                    ChildrenListViewModel ChildrenView = ChildrenListViewModel.Create(SelectedItems.ToArray(), CurrentChild);

                    document = service.CreateDocument("ChildrenListView", ChildrenView, CurrentChild.Manager.DisplayName + " Children", this);
                    document.Id = CurrentChild.Manager.DisplayName + " Children";
                }
                document.Show();
            }
            else
            { }



        }



        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            
        }
            
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
                       
            e.QueryableSource = CurrentChild.Manager.GetQueryable(ParentQuery, ParentType.GetType(), Database);
        }



        public override void Save()
        {
           
        }

       // public object CurrentRecord { get; set; }
      
        public override void AddRecord()
        {
            Views.RecordImporters.RecordImportHolderView recordImportHolderView = new Views.RecordImporters.RecordImportHolderView(new Views.RecordImporters.SiteCallingImportView());
            CustomControlWindow window = new CustomControlWindow(recordImportHolderView);
            if (window.DialogResult)
            {

            }
        }
        public override void DeleteRecord()
        {
           
        }
        public override void RestoreRecord()
        {

        }


        public string FilterExpression
        {
            get
            {
                return "";
            }
            set
            {

            }
        }
        public bool ClosingFormEnabled = true;

        public override void SelectionChanged()
        {
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(ParentType.Manager.DisplayName + " Children");
            if (document != null)
            {
                ((ChildrenListViewModel)document.Content).UpdateParentQuery(SelectedItems.ToArray());
            }
        }
    }
}
