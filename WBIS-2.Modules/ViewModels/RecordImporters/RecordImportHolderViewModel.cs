using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBIS_2.DataModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Atlas.Data;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Controls;
using WBIS_2.Modules.Views.RecordImporters;

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public class RecordImportHolderViewModel:BindableBase
    {
        public RecordImportHolderViewModel(UserControl _startingRecordImport, RecordImportHolderView view)
        {
            StartingRecordImport = (RecordImporterBase)_startingRecordImport.DataContext;
            StartingRecordImport.Holder = this;
            View = view;
            View.AddRecordImporterControl(_startingRecordImport);
        }
        private RecordImportHolderView View { get; set; }
        public RecordImporterBase StartingRecordImport { get; set; }
     
        
        public bool SaveClick()
        {
            //Check that requirements are met for import.
            if (!StartingRecordImport.CheckSave())
                return false;
            StartingRecordImport.SaveClick();
            return true;
        }
        public void RemoveImportControl(object RemoveViewModel)
        {
            View.RemoveRecordImporterControl(RemoveViewModel);
        }
        public void AddImportControl(UserControl AddViewModel)
        {
            View.AddRecordImporterControl(AddViewModel);
        }
    }
}
