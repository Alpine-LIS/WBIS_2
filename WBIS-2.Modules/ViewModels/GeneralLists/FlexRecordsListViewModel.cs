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
using Alpine.FlexForms;
using Alpine.FlexForms.Controls;

namespace WBIS_2.Modules.ViewModels
{
    public class FlexRecordsListViewModel : ListViewModelBase, IDocumentContent
    {
        public object Title
        {
            get { return $"{Template.Name} List"; }
        }
      
        Template Template
        {
            get { return GetProperty(() => Template); }
            set { SetProperty(() => Template, value); } 
        }

        public static FlexRecordsListViewModel Create(Template template)
        {
            return ViewModelSource.Create(() => new FlexRecordsListViewModel()
            {
                Caption = $"{template.Name}",
                Content = $"{template.Name}",
                Template = template,
                ListManager = new UserFlexRecord().Manager
            });
        }

        public override void CloseForm()
        {
            DocumentOwner.Close(this);
        }

        //public void OnClose(CancelEventArgs e)
        //{
        //    if (Changed)
        //    {
        //        var result = ThemedMessageBox.Show(title: "Confirmation", text: UnsavedMessageText, messageBoxButtons: MessageBoxButton.YesNo);
        //        if (result != MessageBoxResult.Yes)
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //}

        public void OnDestroy()
        {

        }

        protected FlexRecordsListViewModel()
        {
            Privileges();
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            if (ListManager == null) return;
            Type type = ListManager.InformationType;
            if (e.Any(_ => _.Entity.GetType() == type))
            {
                RefreshDataSource();
            }                
        }


       
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            e.QueryableSource = Database.UserFlexRecords
                 .Include(_ => _.User)
                 .Include(_ => _.UserModified)
                 .Include(_ => _.DataForm).ThenInclude(_ => _.Template)
                 .Where(_ => _.DataForm.Template == Template && (!_._delete || _._delete == ViewDeleted) && (!_.Repository || _.Repository == ViewRepository));
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
        }




        public override void ShowChildren()
        {
            
        }
        public override void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(CurrentRecord.Id);
            if (document == null)
            {
                DataFormDetailsViewModel dataFormDetailsViewModel = DataFormDetailsViewModel.Create(CurrentRecord.Id, Template.Id, Guid.Empty);
                document = service.CreateDocument("DataFormDetailsView", dataFormDetailsViewModel, CurrentRecord.Id, this);
                document.Id = CurrentRecord.Id;
            }
            document.Show();
        }
        public override void AddSingle()
        {
            Guid guid = Guid.NewGuid();
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(guid);
            if (document == null)
            {
                DataFormDetailsViewModel dataFormDetailsViewModel = DataFormDetailsViewModel.Create(guid, Template.Id, Guid.Empty);
                document = service.CreateDocument("DataFormDetailsView", dataFormDetailsViewModel, guid, this);
                document.Id = guid;
            }
            document.Show();
        }
    }
}
