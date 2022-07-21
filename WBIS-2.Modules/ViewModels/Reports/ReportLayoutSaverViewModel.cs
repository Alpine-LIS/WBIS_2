using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.PivotGrid;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Npgsql;
using System.Data;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class ReportLayoutSaverViewModel : UserDataValidator
    {
        public PivotGridControl MyPivotGridControl;
        public string FolderLocation
        {
            get { return GetProperty(()=>FolderLocation); }
            set { SetProperty(()=>FolderLocation, value);
            }
        }
        public ReportLayout ReportLayout { get; set; }

        public ReportLayoutSaverViewModel(string recordType, PivotGridControl myPivotGridControl)
        {
            MyPivotGridControl = myPivotGridControl;
            ReportLayout = new ReportLayout()
            {
                FilterString = MyPivotGridControl.FilterString,
                Table = recordType,
                ReportFields = new List<ReportField>()
            };
            RaisePropertyChanged(nameof(ReportLayout));

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Reports"))
            {
                FolderLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Reports";
                RaisePropertyChanged(nameof(FolderLocation));
            }
        }

        public ICommand FolderSelectCommand => new DelegateCommand(FolderSelect);
        private void FolderSelect()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderLocation = dialog.SelectedPath;
                RaisePropertyChanged(nameof(FolderLocation));
            }
        }
        
        public bool SaveLayout()
        {
            if (HasErrors() || ReportLayout.HasErrors())
            {
                MessageBox.Show("Please ensure that all fields are filled.");
                return false;
            }

            if (FolderLocation == null)
            {
                MessageBox.Show("No folder is selected.");
                return false;
            }

            string fileName = $@"{FolderLocation}\{ReportLayout.Name}.report";
            if (File.Exists(fileName))
            {
                if (MessageBox.Show("The selected name and destination already exists. Would you like to overwrite this file?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;
                File.Delete(fileName);
            }

            foreach (var field in MyPivotGridControl.Fields)
            {
                ReportField reportField = new ReportField()
                {
                    FieldName = field.FieldName,
                    AreaName = (int)field.Area,
                    SummaryType = (int)field.SummaryType,
                    GroupInterval = (int)field.GroupInterval
                };
                ReportLayout.ReportFields.Add(reportField);
            }
            
            var content = System.Text.Json.JsonSerializer.Serialize(ReportLayout);
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(content);
            }
            return true;
        }









    }
}
