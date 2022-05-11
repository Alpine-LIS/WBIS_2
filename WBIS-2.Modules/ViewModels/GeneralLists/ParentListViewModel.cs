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
    public class ParentListViewModel : ListViewModelBase, IDocumentContent
    {
        public object Title
        {
            get { return $"{ParentType.Manager.DisplayName}"; }
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
                ListManager = ParentType.Manager;
                RaisePropertyChanged(nameof(ListManager));
                CurrentUser.AddRemoveInfoType(ListManager.DisplayName, true);
            }
        }

        public static ParentListViewModel Create(IInformationType parentType)
        {
            return ViewModelSource.Create(() => new ParentListViewModel()
            {
                Caption = $"{parentType.Manager.DisplayName}",
                Content = $"{parentType.Manager.DisplayName}",
                ParentType = parentType,
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

        protected ParentListViewModel()
        {
            //Tracker.ChangesSaved += Tracker_ChangesSaved;
            Privileges();
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

            if (ParentType.Manager.AvailibleChildren.Count() > 0)
            {
                IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
                IDocument document = service.FindDocumentById(ParentType.Manager.DisplayName + " Children");
                if (document == null)
                {
                    ChildrenListViewModel ChildrenView = ChildrenListViewModel.Create(SelectedItems.ToArray(), ParentType);

                    document = service.CreateDocument("ChildrenListView", ChildrenView, ParentType.Manager.DisplayName + " Children", this);
                    document.Id = ParentType.Manager.DisplayName + " Children";
                }
                document.Show();
            }
            //else
            //{ }



        }



        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            
        }


       
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            e.QueryableSource = ParentType.Manager.GetQueryable(CurrentUser.Districts.ToArray(), typeof(District), Database);

            //if (ParentType is District)
            //    e.QueryableSource = Database.Set<District>()
            //        .AsNoTracking();
            //else if (ParentType is Watershed)
            //    e.QueryableSource = Database.Set<Watershed>()
            //        .AsNoTracking();
            //else if (ParentType is Quad75)
            //    e.QueryableSource = Database.Set<Quad75>()
            //        .AsNoTracking();
            //else if (ParentType is Hex160)
            //    e.QueryableSource = Database.Set<Hex160>()
            //        .AsNoTracking();
        }



        public override void Save()
        {
           
        }

        //public object CurrentRecord { get; set; }
      
        //public override void AddRecord()
        //{
            
        //}
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
