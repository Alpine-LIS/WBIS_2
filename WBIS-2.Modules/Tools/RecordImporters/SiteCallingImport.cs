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

namespace WBIS_2.Modules.Tools.RecordImporters
{
    public class SiteCallingImport
    {
        WBIS2Model Database = new WBIS2Model();

        public void ImportFromExcel()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XLSX|*.xlsx";
            ofd.Multiselect = false;
            if (!ofd.ShowDialog().Value) return;

            DataTable importDt = ExcelTools.GetExcelTableFirst(ofd.FileName);
            if (!CheckImportFile(importDt)) return;
        }

        private void PerformExcelImport(DataTable importDt)
        {
            foreach (DataRow importRow in importDt.Rows)
            {

            }
            Database.SaveChanges();
        }

        private bool CheckImportFile(DataTable dt)
        {
            if (ImportColumns.Any(_ => !dt.Columns.Contains(_)))
            {
                if (MessageBox.Show("The selected file is missing required fields. Would you like to export a template file for this import?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    ExportTemplate();
                return false;
            }
            return true;
        }

        public void ExportTemplate()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XLSX|*.xlsx";
            sfd.OverwritePrompt = false;
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (File.Exists(sfd.FileName))
            {
                MessageBox.Show("The selected file name already exists.");
                goto HERE;
            }

            DataTable dt = new DataTable();
            foreach (string col in ImportColumns)
                dt.Columns.Add(col);
            ExcelTools.BasicExcelSave(sfd.FileName, dt, false, false);
            if (MessageBox.Show("Would you like to open your template?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
        }
        public static string[] ImportColumns => new string[]
        {
            "User",
            "Sunset Time",
            "Area Description",
            "Record Type",
            "Survey Type1",
            "Survey Type2",
            "Survey Species",
            "Wind",
            "Precipitation",
            "Start Time",
            "End Time",
            "Hex ID",
            "Site Type",
            "Site ID",
            "Pass Number",
            "Target Species Present",
            "Detection Time",
            "Detection Method",
            "Species",
            "Sex",
            "Age",
            "Number of Young",
            "Species Site",
            "Male Banding",
            "Male Banding Color",
            "Female Banding",
            "Female Banding Color",
            "Distance",
            "Bearing",
            "Lat",
            "Lon",
            "Occupancy Status",
            "Nesting Status",
            "Reproductive Status",
            "Nest Tree",
            "Nest Type",
            "Tree Species",
            "DBH",
            "Nest Height",
            "Tagged",
            "UserLat",
            "UserLon",
            "DeviceLat",
            "DeviceLon",
            "ManualEntry",
            "Historic",
            "DeviceTimestamp",
            "Comments",
            "TrackInfo",
            "ID",
            "FID",
            "StartLat",
            "StartLon",
            "Moused",
            "Est Location",
            "PZ Pass Number",
            "PZ",
            "YAC",
            "Hex500",
            "Species Present"
        };
    }
}
