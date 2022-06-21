using Atlas.Data;
using Atlas.Projections;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;
using NetTopologySuite.Geometries;
using DevExpress.Mvvm.POCO;
using System.Windows.Input;
using System.IO.Compression;
using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using WBIS_2.Modules.Tools;
using Microsoft.EntityFrameworkCore;
using DevExpress.Xpf.Editors.Helpers;
using WBIS_2.Modules.Views.UserControls;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class ParentReport : WBISViewModelBase
    {
        WBIS2Model Database = new WBIS2Model();
        public ParentReport(IInformationType[] records, IInfoTypeManager manager)
        {            
            List<IInformationType> informationTypes = manager.GetChildren(false, CurrentUser.TypeGroup).OrderBy(_ => _.Manager.DisplayName).ToList();
            informationTypes.Insert(0, (IInformationType)Activator.CreateInstance(manager.InformationType));

            ParentReportControl parentReportControl = new ParentReportControl(informationTypes.ToArray());
            CustomControlWindow window = new CustomControlWindow(parentReportControl);
            if (!window.DialogResult) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XLSX|*.xlsx";
            sfd.OverwritePrompt = false;
            sfd.FileName = $"ParentReport {DateTime.Now.ToShortDateString().Replace("\\", "-").Replace("/", "-")}_{DateTime.Now.ToShortTimeString().Replace(":", "-").Replace("/", "-")}";
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (File.Exists(sfd.FileName))
            {
                MessageBox.Show("The selected name is already in use");
                goto HERE;
            }


            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var excel = new Excel.Application { Visible = false };
            var misValue = System.Reflection.Missing.Value;
            var wb = excel.Workbooks.Add(misValue);

            var useName = parentReportControl.ReturnTypes.Reverse();
            foreach (string val in useName)
            {
                Excel.Worksheet sheet = wb.Sheets.Add();
                sheet.Name = val;

                var childMan = informationTypes.First(_ => _.Manager.DisplayName == val).Manager;
                var writeRecords = childMan.GetQueryable(records, manager.InformationType, Database, showDelete: false, showRepository: true);
               
                DataTable hexDt = ExcelTools.EntityToDatatable(childMan, writeRecords);
                ExcelTools.DatatableToSheet(sheet, hexDt, true, true, Excel.XlRgbColor.rgbLightGrey, true);
            }

            wb.Sheets["Sheet1"].Delete();

            wb.SaveAs(sfd.FileName);
            wb.Close(true);
            excel.Quit();

            w.Stop();
            if (MessageBox.Show("Would you like to open your report?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
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
