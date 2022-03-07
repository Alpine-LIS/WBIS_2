using DevExpress.Data.Linq;
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
            LockedRecord = !CurrentUser.AdminPrivileges;
            Privileges();
            RaisePropertyChanged(nameof(AvailibleChildren));
            SelectionUpdated += UpdateChildren;
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
                    ChildrenListViewModel ChildrenView = new ChildrenListViewModel();
                    if (CurrentChild.GetType() == typeof(Watershed))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<Watershed>().ToArray(), new Watershed());
                    else if (CurrentChild.GetType() == typeof(Quad75))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<Quad75>().ToArray(), new Quad75());
                    else if (CurrentChild.GetType() == typeof(Hex160))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<Hex160>().ToArray(), new Hex160());
                    else if (CurrentChild.GetType() == typeof(Hex160RequiredPass))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<Hex160RequiredPass>().ToArray(), new Hex160RequiredPass());
                    else if (CurrentChild.GetType() == typeof(SiteCalling))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<SiteCalling>().ToArray(), new SiteCalling());
                    else if (CurrentChild.GetType() == typeof(CNDDBOccurrence))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<CNDDBOccurrence>().ToArray(), new CNDDBOccurrence());
                    else if (CurrentChild.GetType() == typeof(CDFW_SpottedOwl))
                        ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<CDFW_SpottedOwl>().ToArray(), new CDFW_SpottedOwl());

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
                    SetListType(CurrentChild.GetType());
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

            if (CurrentChild.GetType() == typeof(SiteCalling))
            {
                e.QueryableSource = Database.Set<SiteCalling>()
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Districts)
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Quad75s)
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Watersheds)
                    .Include(_ => _.SurveySpecies)
                    .Include(_ => _.SiteCallingDetection).ThenInclude(_=>_.SpeciesFound)
                    .Include(_ => _.User)
                    .Where(((SiteCalling)CurrentChild).GetParentWhere(ParentQuery, ParentType.GetType()))
                    .AsNoTracking();
            }
            else if(CurrentChild.GetType() == typeof(Hex160RequiredPass))
            {
                e.QueryableSource = Database.Set<Hex160RequiredPass>()
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Districts)
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Quad75s)
                    .Include(_ => _.Hex160).ThenInclude(_ => _.Watersheds)
                    .Include(_ => _.BirdSpecies)
                    .Include(_ => _.User)
                    .Where(((Hex160RequiredPass)CurrentChild).GetParentWhere(ParentQuery, ParentType.GetType()))
                    .AsNoTracking();
            }
            else if (CurrentChild.GetType() == typeof(CNDDBOccurrence))
            {
                e.QueryableSource = Database.Set<CNDDBOccurrence>()
                    .Include(_ => _.Districts)
                    .Include(_ => _.Watersheds)
                    .Include(_ => _.Quad75s)
                    .Where(((CNDDBOccurrence)CurrentChild).GetParentWhere(ParentQuery, ParentType.GetType()))
                    .AsNoTracking();
            }
            else if (CurrentChild.GetType() == typeof(CDFW_SpottedOwl))
            {
                e.QueryableSource = Database.Set<CDFW_SpottedOwl>()
                    .Include(_ => _.District)
                    .Include(_ => _.Watershed)
                    .Include(_ => _.Quad75)
                    .Where(((CDFW_SpottedOwl)CurrentChild).GetParentWhere(ParentQuery, ParentType.GetType()))
                    .AsNoTracking();
            }
            else if (CurrentChild.GetType() == typeof(Watershed))
            {
                e.QueryableSource = Database.Set<Watershed>()
                    .Include(_ => _.Districts)
                    .Where(((Watershed)CurrentChild).GetParentWhere(ParentQuery, ParentType.GetType()))
                    .AsNoTracking();
            }
            else if (CurrentChild.GetType() == typeof(Hex160))
            {
                e.QueryableSource = Database.Set<Hex160>()
                    .Include(_ => _.Districts)
                    .Include(_ => _.Quad75s)
                    .Include(_ => _.Watersheds)
                    .Where(((Hex160)CurrentChild).GetParentWhere(ParentQuery, ParentType.GetType()))
                    .AsNoTracking();
            }
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
     
        public string TableKeyField => throw new NotImplementedException();

        public string LayerKeyField => throw new NotImplementedException();

        public string LayerName => throw new NotImplementedException();

        public void ZoomToLayer()
        {
        }
    }
}
