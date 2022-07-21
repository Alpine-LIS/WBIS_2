using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.PivotGrid;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WBIS_2.DataModel;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.Views.Reports;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class ReportBuilderViewModel : WBISViewModelBase, IDocumentContent
    {
        public object Title => "Report Builder";
        public static ReportBuilderViewModel Create()
        {
            return ViewModelSource.Create(() => new ReportBuilderViewModel()
            {
                Caption = "Report Builder"
            });
        }
        public ReportBuilderViewModel()
        {
            GetReportableInformationTypes();
        }
        private void GetReportableInformationTypes()
        {
            DataOptions = new IInformationType[0];
            var types = Database.Model.GetEntityTypes().Select(_ => _.ClrType);
            foreach(var t in types)
            {
                if (t.GetCustomAttributes(typeof(ReportableTable), true).Any())
                {
                    var grouping = t.GetCustomAttribute(typeof(TypeGrouper));
                    if (grouping == null) continue;
                    if (CurrentUser.TypeGroup.Contains(((TypeGrouper)grouping).GroupName) || ((TypeGrouper)grouping).IgnoreGroups)
                        DataOptions = DataOptions.Append(Activator.CreateInstance(t)).Cast<IInformationType>().ToArray();
                }                   
            }
        }

        public PivotGridControl? MyPivotGridControl { get; set; }

        public IInformationType[] DataOptions { get; set; }
        public IInformationType CurrentDataOption
        {
            get { return GetProperty(()=> CurrentDataOption); }
            set { SetProperty(()=> CurrentDataOption, value); 
            RefreshDataSource();}
        }
        public IQueryable Records { get; set; }
        private void RefreshDataSource()
        {
            if (CurrentDataOption == null) return;
            Records = CurrentDataOption.Manager.GetQueryable(CurrentUser.Districts.ToArray(), typeof(District), Database, showDelete: ViewDeleted, showRepository: ViewRepository);
            RaisePropertyChanged(nameof(Records));
            GetFields();
        }
        List<PivotGridField> fields;
        public void GetFields()
        {
            fields = new List<PivotGridField>();

            foreach(var field in CurrentDataOption.Manager.DisplayFields)
            {
                if (field.DataType == typeof(Guid))
                {
                    var prop = CurrentDataOption.GetType().GetProperty(field.FullName);
                    if (prop == null) continue;
                    else if(!prop.GetCustomAttributes(typeof(KeyAttribute), true).Any()) continue;
                }
                AddField(field.FullName, field.FieldName, FieldArea.FilterArea, field.DataType);
            }
            fields = fields.OrderBy(_ => _.FieldName).ToList();
            MyPivotGridControl.FieldsSource = fields;
        }
        private void AddField(string fieldName, string caption, FieldArea area, Type dataType, FieldSummaryType summaryType = FieldSummaryType.Max)
        {
            PivotGridField field = new PivotGridField();
            field.FieldName = fieldName;
            field.Caption = caption;
            field.Area = area;
            field.ShowGrandTotal = false;
            field.ShowTotals = false;
            field.SummaryType = summaryType;
            field.ShowValues = true;
            field.ShowGrandTotal = true;
            field.FilterPopupMode = FilterPopupMode.Excel;
            field.AllowRuntimeSummaryChange = true;
            field.AllowedAreas = FieldAllowedAreas.All;
            if (summaryType == FieldSummaryType.Average)
            {
                field.ValueFormat = "{0:N2}";
                field.CellFormat = "{0:N2}";
            }
            if (fieldName.Contains("Cost"))
            {
                field.ValueFormat = "{0:C2}";
                field.CellFormat = "{0:C2}";
            }
            else if (dataType == typeof(double))
            {
                field.ValueFormat = "{0:N3}";
                field.CellFormat = "{0:N3}";
            }

            field.AllowRuntimeSummaryChange = true;
            fields.Add(field);
        }




        public ICommand SaveReport => new DelegateCommand(SaveReportClick);
        public void SaveReportClick()
        {
            ReportLayoutSaverViewModel reportLayoutSaverViewModel = new ReportLayoutSaverViewModel(CurrentDataOption.Manager.DisplayName, MyPivotGridControl);
            ReportLayoutSaverView reportLayoutSaverView = new ReportLayoutSaverView();
            reportLayoutSaverView.DataContext = reportLayoutSaverViewModel;
            CustomControlWindow window = new CustomControlWindow(reportLayoutSaverView);
            if (window.DialogResult)
            {

            }
        }
        public ICommand LoadReport => new DelegateCommand(LoadReportClick);
        public void LoadReportClick()
        {
            ReportLayoutLoaderViewModel reportLayoutLoaderViewModel = new ReportLayoutLoaderViewModel();
            ReportLayoutLoaderView reportLayoutLoaderView = new ReportLayoutLoaderView();
            reportLayoutLoaderView.DataContext = reportLayoutLoaderViewModel;
            CustomControlWindow window = new CustomControlWindow(reportLayoutLoaderView);
            if (window.DialogResult)
            {
                ReportLayout reportLayout = reportLayoutLoaderViewModel.SelectedReport;
                if (!DataOptions.Any(_=>_.Manager.DisplayName == reportLayout.Table))
                {
                    MessageBox.Show($"There is no report for '{reportLayout.Table}' availible.");
                    return;
                }
                CurrentDataOption = DataOptions.First(_ => _.Manager.DisplayName == reportLayout.Table);
                RaisePropertyChanged(nameof(CurrentDataOption));

                foreach (var field in reportLayout.ReportFields)
                {
                    var pivotField = MyPivotGridControl.Fields.FirstOrDefault(_ => _.FieldName == field.FieldName);
                    if (pivotField == null) continue;
                    pivotField.Area = (FieldArea)field.AreaName;
                    pivotField.GroupInterval = (FieldGroupInterval)field.GroupInterval;
                    pivotField.SummaryType = (FieldSummaryType)field.SummaryType;
                    pivotField.AreaIndex = field.AreaIndex;
                }
                MyPivotGridControl.FilterString = reportLayout.FilterString;
            }
        }
        public ICommand ExportReport =>  new DelegateCommand(ExportReportClick);
        public void ExportReportClick()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XLSX|*.xlsx";
            if (sfd.ShowDialog().Value)
            {
                try
                {
                    MyPivotGridControl.ExportToXlsx(sfd.FileName, new PivotGridXlsxExportOptions() { ExportFilterAreaHeaders = DevExpress.Utils.DefaultBoolean.False });
                    if (MessageBox.Show("Would you like to open your export?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }







        private bool _viewDeleted = CurrentUser.ViewDeleted;
        public bool ViewDeleted
        {
            get { return _viewDeleted; }
            set
            {
                if (_viewDeleted != value)
                {
                    _viewDeleted = value;
                    CurrentUser.ViewDeleted = value;
                    RefreshDataSource();
                }
            }
        }

        private bool _ViewRepository = CurrentUser.ViewRepository;
        public bool ViewRepository
        {
            get { return _ViewRepository; }
            set
            {
                if (_ViewRepository != value)
                {
                    _ViewRepository = value;
                    CurrentUser.ViewRepository = value;
                    RefreshDataSource();
                }
            }
        }
        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public override void CloseForm()
        {
        }

        public void OnDestroy()
        {
        }
    }
}
