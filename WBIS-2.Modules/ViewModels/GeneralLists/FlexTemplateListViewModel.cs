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
using DevExpress.Mvvm.DataAnnotations;

namespace WBIS_2.Modules.ViewModels
{
    [POCOViewModel]
    public class FlexTemplateListViewModel
    {
        public bool IsActive { get; set; }
        public string Caption => "Syrvey Types";
        private DbContext _database { get; } = AutoConfiguration.Database;
        public EntityInstantFeedbackSource Records { get; set; }
        public Template? CurrentRecord { get; set; }
        /// <summary>
        /// Returns POCO ViewModel for TemplatesListView
        /// </summary>
        /// <param name="database">Instance of Application DBContext</param>
        /// <returns></returns>
        public static FlexTemplateListViewModel Create()
        {
            return ViewModelSource.Create(() => new FlexTemplateListViewModel());
        }
        /// <summary>
        /// Main constractor for ViewModel.
        /// if application uses DevExpress.Mvvm.ModuleInjection, avoid use this constractor 
        /// and use static TemplatesListViewModel.Create method what returns POCO model
        /// https://docs.devexpress.com/WPF/17352/mvvm-framework/viewmodels/runtime-generated-poco-viewmodels
        /// </summary>
        /// <param name="database"></param>
        public FlexTemplateListViewModel()
        {
            Records = new EntityInstantFeedbackSource
            {
                AreSourceRowsThreadSafe = true,
                KeyExpression = $"Id",
            };
            Records.GetQueryable += Records_GetQueryable;
            Records.DismissQueryable += Records_DismissQueryable;
            Records.AreSourceRowsThreadSafe = true;
        }

        public ICommand ShowDetailsCommand => new DelegateCommand(ShowDetails);
        public void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(CurrentRecord.Name);
            if (document == null)
            {
                FlexRecordsListViewModel FlexRecordsListViewModel = FlexRecordsListViewModel.Create(CurrentRecord);
                document = service.CreateDocument("FlexRecordsListView", FlexRecordsListViewModel, CurrentRecord.Name, this);
                document.Id = CurrentRecord.Name;
            }
            document.Show();
        }
       
      
        private void Records_DismissQueryable(object? sender, GetQueryableEventArgs e)
        {
            _database.Dispose();
        }

        private void Records_GetQueryable(object? sender, GetQueryableEventArgs e)
        {
            e.QueryableSource = _database.Set<Template>().Where(_=>!_.IsDeleted);
        }
    }
}
