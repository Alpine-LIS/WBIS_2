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

namespace WBIS_2.Modules.ViewModels
{
    public class Quad75ChildrenViewModel : WBISViewModelBase, IDocumentContent, IMapNavigation
    {
        public object Title
        {
            get { return $"Quad75 Children"; }
        }

        public static Quad75ChildrenViewModel Create(Quad75[] quad75s)
        {
            return ViewModelSource.Create(() => new Quad75ChildrenViewModel()
            {
                Caption = "Quad75 Children",
                Content = "Quad75 Children",
                SelectedItems = new ObservableCollection<object>(),
                ParentQuery = quad75s,
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

        protected Quad75ChildrenViewModel()
        {
            LockedRecord = !CurrentUser.AdminPrivileges;
            Privileges();
        }
        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {

        }


        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new SiteCalling(), new CNDDBOccurrence(), new CDFW_SpottedOwl() }; }
        }
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
                    SetListType(CurrentChild.GetType());
                }
            }
        }
        public Quad75[] ParentQuery
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
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
            if (CurrentChild.GetType() == typeof(SiteCalling))
            {
                e.QueryableSource = Database.Set<SiteCalling>()
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Quad75s)
                    .Include(_ => _.SurveySpecies)
                    .Include(_ => _.SpeciesFound)
                    .Include(_ => _.User)
                    .Where(_ => _.Hex160.Quad75s.Any(d => ParentQuery.Contains(d)))
                               .AsNoTracking();
            }
            else if (CurrentChild.GetType() == typeof(CNDDBOccurrence))
            {
                e.QueryableSource = Database.Set<CNDDBOccurrence>()
                    .Include(_ => _.Quad75s)
                    .Where(_ => _.Quad75s.Any(d => ParentQuery.Contains(d)))
                              .AsNoTracking();
            }
            else if (CurrentChild.GetType() == typeof(CDFW_SpottedOwl))
            {
                e.QueryableSource = Database.Set<CDFW_SpottedOwl>()
                    .Include(_ => _.Quad75s)
                    .Where(_ => _.Quad75s.Any(d => ParentQuery.Contains(d)))
                              .AsNoTracking();
            }
        }



        public override void Save()
        {

        }

        public object CurrentRecord { get; set; }
        public override void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }

            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();

        }
        public override void AddRecord()
        {

        }
        public override void DeleteRecord()
        {

        }
        public override void ViewEdits()
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
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return GetProperty(() => SelectedItems);
                //this.Caption = $"Regens {SelectedItems.Count}";
            }
            set { SetProperty(() => SelectedItems, value); }
        }

        public string TableKeyField => throw new NotImplementedException();

        public string LayerKeyField => throw new NotImplementedException();

        public string LayerName => throw new NotImplementedException();

        public void ZoomToLayer()
        {
        }
    }
}
