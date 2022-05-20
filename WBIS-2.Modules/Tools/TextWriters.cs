using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace WBIS_2.Modules.Tools
{
    public class TextWriters
    {
        public static void WriteTextList(string fileType, string rowIndecator, List<string> records)
        {
            string fileName = SaveFileName(fileType);
            if (fileName == null) return;

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach(string record in records)
                    sw.WriteLine(rowIndecator + record);
            }
            if (MessageBox.Show("Open the saved file?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(fileName) { UseShellExecute = true } }.Start();
        }
        public static string SaveFileName(string fileType)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = fileType;
        HERE:;
            if (!sfd.ShowDialog().Value) return null;
            if (File.Exists(sfd.FileName))
            {
                MessageBox.Show("The selected file name already exists.");
                goto HERE;
            }
            return sfd.FileName;
        }
        public static string SaveDirectoryName()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
        HERE:;
            if (!sfd.ShowDialog().Value) return null;
            if (Directory.Exists(sfd.FileName))
            {
                MessageBox.Show("The selected folder already exists.");
                goto HERE;
            }
            return sfd.FileName;
        }
    }
}
