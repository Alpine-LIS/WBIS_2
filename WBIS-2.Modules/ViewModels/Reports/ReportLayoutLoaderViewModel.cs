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
using System.Text.Json.Nodes;
using System.Text.Json;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class ReportLayoutLoaderViewModel : UserDataValidator
    {
        public string FolderLocation
        {
            get { return GetProperty(()=>FolderLocation); }
            set { SetProperty(()=>FolderLocation, value);
                GetFiles();
            }
        }
        public ReportLayout[] AvailibleReports { get; set; }
        public ReportLayout SelectedReport
        {
            get { return GetProperty(() => SelectedReport); }
            set {  SetProperty(() => SelectedReport, value); }
        }


        public ReportLayoutLoaderViewModel()
        {
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Reports"))
            {
                FolderLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Reports";
                RaisePropertyChanged(nameof(FolderLocation));
            }
        }

        private void GetFiles()
        {
            List<ReportLayout> files = new List<ReportLayout>();
            foreach(var fileName in Directory.GetFiles(FolderLocation, "*.report"))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    var json = JsonObject.Parse(sr.ReadToEnd());
                    files.Add((ReportLayout)json.Deserialize(typeof(ReportLayout)));
                }
            }
            AvailibleReports = files.ToArray();
            if (AvailibleReports.Length > 0)
                SelectedReport = AvailibleReports[0];
            RaisePropertiesChanged(new string[] { nameof(AvailibleReports), nameof(SelectedReport) });
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
        
        public bool LoadLayout()
        {
            if (SelectedReport == null)
            {
                MessageBox.Show("There is no report slected");
                return false;
            }
            if (HasErrors())
            {
                MessageBox.Show("Please ensure that all fields are filled.");
                return false;
            }

            if (FolderLocation == null)
            {
                MessageBox.Show("No folder is selected.");
                return false;
            }


            return true;
        }

      

       
    }


    public class ReportLayout : UserDataValidator
    {
        [Required, StringLength(int.MaxValue, MinimumLength = 1)]
        public string Name { get; set; }
        [Required, StringLength(int.MaxValue,MinimumLength =1)]
        public string Description { get; set; }
        public string Table { get; set; }
        public string FilterString { get; set; }
        public List<ReportField> ReportFields { get; set; }
    }
    public class ReportField
    {
        public string FieldName { get; set; }
        public int AreaName { get; set; }
        public int SummaryType { get; set; }
        public int GroupInterval { get; set; }
        public int AreaIndex { get; set; }
    }
}
