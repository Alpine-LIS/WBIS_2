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
        public WBIS2Model Database = new WBIS2Model();
        public Dictionary<Type, List<object>> NewListElements = new Dictionary<Type, List<object>>();

        public string ShapeCount
        {
            get
            {
                return GetProperty(() => ShapeCount);
            }
            set
            {
                SetProperty(() => ShapeCount, value);
            }
        }

        public bool RepositoryData { get; set; } = true;
        public RecordImportHolderViewModel(UserControl _startingRecordImport, RecordImportHolderView view)
        {
            StartingRecordImport = (RecordImporterBase)_startingRecordImport.DataContext;
            StartingRecordImport.Holder = this;
            View = view;
            View.AddRecordImporterControl(_startingRecordImport);
            IsExpanded = true;
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


        public void HelpClick()
        {
            Window window = Window.GetWindow(View);
            if (HelpButtonText == "Show Help")
            {
                HelpButtonText = "Hide Help";
                //window.Width = window.Width + 180;
                //View.MainGrid.ColumnDefinitions[1].Width = new GridLength(180);
            }
            else
            {
                HelpButtonText = "Show Help";
                //window.Width = window.Width - 120;
                //View.MainGrid.ColumnDefinitions[1].Width = new GridLength(0);
            }
            IsExpanded = !IsExpanded;
        }
        public ICommand HelpCommand { get; set; }
        public string HelpButtonText { get; set; } = "Show Help";
        public string HelpText { get; set; }
        public bool IsExpanded
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
    }
}
