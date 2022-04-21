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
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = fileType;
        HERE:;
            if (!sfd.ShowDialog().Value) return;
            if (File.Exists(sfd.FileName))
            {
                MessageBox.Show("The selected file name already exists.");
                goto HERE;
            }
            using(StreamWriter sw = new StreamWriter(sfd.FileName))
            {
                foreach(string record in records)
                    sw.WriteLine(rowIndecator + record);
            }
            if (MessageBox.Show("Open the saved file?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
        }
    }
}
