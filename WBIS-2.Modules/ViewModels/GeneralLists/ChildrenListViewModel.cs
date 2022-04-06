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

namespace WBIS_2.Modules.ViewModels
{
    public class ChildrenListViewModel : WBISViewModelBase, IDocumentContent, IMapNavigation
    {
        public object Title
        {
            get { return $"{ParentType.DisplayName} Children"; }
        }
        public IInformationType ParentType { get; set; }

        public static ChildrenListViewModel Create(object[] parentQuery, IInformationType parentType)
        {
            return ViewModelSource.Create(() => new ChildrenListViewModel()
            {
                Caption = $"{((IInformationType)parentType).DisplayName} Children",
                Content = $"{((IInformationType)parentType).DisplayName} Children",
                SelectedItems = new ObservableCollection<object>(),
                ParentType = parentType,
                ParentQuery = parentQuery,
                AvailibleChildren = parentType.AvailibleChildren
            });
        }

        public override void CloseForm()
        {
            DocumentOwner.Close(this);
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
            IDocument document = service.FindDocumentById(CurrentChild.DisplayName + " Children");
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

        public override void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }

            if (CurrentChild.AvailibleChildren.Count() > 0)
            {
                IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
                IDocument document = service.FindDocumentById(CurrentChild.DisplayName + " Children");
                if (document == null)
                {
                    ChildrenListViewModel ChildrenView = ChildrenListViewModel.Create(SelectedItems.ToArray(), CurrentChild);

                    document = service.CreateDocument("ChildrenListView", ChildrenView, CurrentChild.DisplayName + " Children", this);
                    document.Id = CurrentChild.DisplayName + " Children";
                }
                document.Show();
            }
            else
            { }
           


        }



        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            
        }
        
        public IInformationType[] AvailibleChildren { get; set; }
        public IInformationType _CurrentChild;
        public IInformationType CurrentChild
        {
            get { return _CurrentChild; }
            set
            {
                if (_CurrentChild != value)
                {
                    _CurrentChild = value;
                    RefreshDataSource();
                }
            }
        }
        public object[] ParentQuery
        {
            get
            {
                return GetProperty(() => ParentQuery);
                //this.Caption = $"Regens {SelectedItems.Count}";
            }
            set { SetProperty(() => ParentQuery, value); 
                RefreshDataSource(); }
        }


        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
                       
            e.QueryableSource = ((IQueryStuff)CurrentChild).GetQueryable(ParentQuery, ParentType.GetType(), Database);
        }



        public override void Save()
        {
           
        }

        public object CurrentRecord { get; set; }
      
        public override void AddRecord()
        {
            
        }
        public override void DeleteRecord()
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

        public string TableKeyField { get { return ""; } }

        public string LayerKeyField { get { return ""; } }

        public string LayerName { get { return ""; } }

        public void ZoomToLayer()
        {
        }

        public void ZoomToFeature(object ZoomObject)
        {
            //if (CurrentUser.RegenUser)
            //    MapDataPasser.ZoomToFeature(LayerName, LayerKeyField, ((Regen)ZoomObject).RegenID);
            //else
            //    MapDataPasser.ZoomToFeature(LayerName, LayerKeyField, ((Fuelbreak)ZoomObject).FuelbreakID);
        }
        public void MapShowAFS(Dictionary<Guid, string> selection)
        {
            MapDataPasser.MapShowAFS(LayerName, LayerKeyField, selection.Values.Cast<object>().Distinct().ToList());
            // MapDataPasser.MapShowAFS(LayerName, "Guid", selection.Cast<object>().ToList());
        }

        public override void SelectionChanged()
        {
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(ParentType.DisplayName + " Children");
            if (document != null)
            {
                ((ChildrenListViewModel)document.Content).UpdateParentQuery(SelectedItems.ToArray());
            }
        }
    }
}
