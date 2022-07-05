using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WBIS_2.DataModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for FloweringTimelineControl.xaml
    /// </summary>
    public partial class FloweringTimelineControl : System.Windows.Controls.UserControl
    {
        public DataGridView Dgv;
        BotanicalScopingSpecies[] SpeciesList;
        WBIS2Model model = new WBIS2Model();
        public FloweringTimelineControl(BotanicalScopingSpecies[] speciesList, string ThpStr, int elevMin, int elevMax)
        {
            InitializeComponent();
            Dgv = MakeDgv();
            WFH.Child = Dgv;
            SpeciesList = speciesList.OrderBy(_=>_.PlantSpecies.SciName).ToArray();

            LblThp.Text = ThpStr;
            LblElevation.Text = "Elevation: " + elevMin.ToString("N0") + "-" + elevMax.ToString("N0") + " ft. (" + (elevMin * 0.3048d).ToString("N0") + "-" + (elevMax * 0.3048d).ToString("N0") + " m.)";
            BuildTimeline();
        }

        private DataGridView MakeDgv()
        {
            DataGridView dgv = new DataGridView();
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 225);
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.CellPainting += Dgv_CellPainting;


            dgv.Columns.Add("Mar", "Mar");//2
            dgv.Columns.Add("Apr", "Apr");//3
            dgv.Columns.Add("May", "May");//4
            dgv.Columns.Add("Jun", "Jun");//5
            dgv.Columns.Add("Jul", "Jul");//6
            dgv.Columns.Add("Aug", "Aug");//7
            dgv.Columns.Add("Sep", "Sep");//8
            dgv.Columns.Add("Species", "Species");
            dgv.Columns.Add("Status", "Status");
            dgv.Columns.Add("CalList", "CalList");
            dgv.Columns.Add("FedList", "FedList");

            return dgv;
        }

        private void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.ColumnIndex > 1 && e.RowIndex >= 0)
            if (e.ColumnIndex <= 6)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            }
        }

        private void BuildTimeline()
        {
            string[] listed = new string[] { "ENDANGERED", "RARE" };
            foreach(var bs in SpeciesList)
            {
                if (bs.Exclude || bs.ExcludeReport) continue;

                Dgv.Rows.Add();
                DataGridViewRow row = Dgv.Rows[Dgv.Rows.Count - 1];
                row.Cells["Species"].Value = bs.PlantSpecies.SciName;
                row.Cells["Status"].Value = bs.PlantSpecies.RPlantRank; 
                row.Cells["CalList"].Value = bs.PlantSpecies.CalList;
                row.Cells["FedList"].Value = bs.PlantSpecies.FedList;

                int activeFrom = -1;
                int activeTo = -1;

                var timeline = model.FloweringTimelines.FirstOrDefault(_ => _.PlantSpecies.Id == bs.PlantSpecies.Id);
                if (timeline != null)
                {
                    activeFrom = GetMonthInt(timeline.ActiveFrom);
                    activeTo = GetMonthInt(timeline.ActiveTo);
                }

                System.Drawing.Color color;
                if (NullString(bs.PlantSpecies.RPlantRank).StartsWith("3") || NullString(bs.PlantSpecies.RPlantRank).StartsWith("4")) { color = System.Drawing.Color.Salmon; }
                else { color = System.Drawing.Color.CornflowerBlue; }

                if (listed.Contains(NullString(bs.PlantSpecies.CalList).ToUpper()) || listed.Contains(NullString(bs.PlantSpecies.FedList).ToUpper()))
                    color = System.Drawing.Color.Yellow;

                if (activeFrom != -1 && activeTo != -1)
                {
                    if (activeTo < activeFrom)
                    {
                        for (int t = 2; t < activeTo + 1; t++) { row.Cells[t].Style.BackColor = color; }
                        for (int t = activeFrom; t < 14; t++) { row.Cells[t].Style.BackColor = color; }
                    }
                    else
                    {
                        for (int t = activeFrom; t < activeTo + 1; t++) { row.Cells[t].Style.BackColor = color; }
                    }
                }
                else if (activeFrom != -1)
                {
                    row.Cells[activeFrom].Style.BackColor = color;
                }
                else if (activeTo != -1)
                {
                    row.Cells[activeTo].Style.BackColor = color;
                }
            }
        }

        private string NullString(string val)
        {
            if (val == null) return "";
            else return val;
        }

        private int GetMonthInt(string val)
        {
            if (val == "N/A") { return -1; }
            else if (val == "January") { return 0; }
            else if (val == "February") { return 0; }
            else if (val == "March") { return 0; }
            else if (val == "April") { return 1; }
            else if (val == "May") { return 2; }
            else if (val == "June") { return 3; }
            else if (val == "July") { return 4; }
            else if (val == "August") { return 5; }
            else if (val == "September") { return 6; }
            else if (val == "October") { return 6; }
            else if (val == "November") { return 6; }
            else if (val == "December") { return 6; }
            else { return -1; }
        }


        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "XLSX|*.xlsx";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    WaitWindowHandler w = new WaitWindowHandler();
                    w.Start();

                    var excel = new Excel.Application { Visible = false };
                    var misValue = System.Reflection.Missing.Value;
                    var wb = excel.Workbooks.Add(misValue);
                    Excel.Worksheet sh = wb.Sheets[1];
                    sh.Rows.RowHeight = 15;

                    for (int t = 0; t < Dgv.Columns.Count; t++)
                    {
                        sh.Cells[2, NumToLetter(t + 1)].Value2 = Dgv.Columns[t].HeaderText;
                    }
                    sh.get_Range("B2:" + NumToLetter(Dgv.Columns.Count) + 2).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("B2:" + NumToLetter(Dgv.Columns.Count) + 2).Borders.Weight = Excel.XlBorderWeight.xlThin;
                    sh.get_Range("B2:" + NumToLetter(Dgv.Columns.Count) + 2).Interior.Color = Excel.XlRgbColor.rgbLightGray;
                    sh.get_Range("B2:" + NumToLetter(Dgv.Columns.Count) + 2).Font.Bold = true;

                    for (int i = 0; i < Dgv.Rows.Count; i++)
                    {
                        for (int t = 0; t < Dgv.Columns.Count; t++)
                        {
                            if (Dgv.Rows[i].Cells[t].Style.BackColor == System.Drawing.Color.CornflowerBlue)
                            {
                                sh.Cells[i + 3, NumToLetter(t + 1)].Interior.Color = Excel.XlRgbColor.rgbCornflowerBlue;
                            }
                            else if (Dgv.Rows[i].Cells[t].Style.BackColor == System.Drawing.Color.Salmon)
                            {
                                sh.Cells[i + 3, NumToLetter(t + 1)].Interior.Color = Excel.XlRgbColor.rgbSalmon;
                            }
                            else if (Dgv.Rows[i].Cells[t].Style.BackColor == System.Drawing.Color.Yellow)
                            {
                                sh.Cells[i + 3, NumToLetter(t + 1)].Interior.Color = Excel.XlRgbColor.rgbYellow;
                            }
                            else
                            {
                                sh.Cells[i + 3, NumToLetter(t + 1)].Value2 = Dgv.Rows[i].Cells[t].Value;
                                sh.Cells[i + 3, NumToLetter(t + 1)].Interior.Color = Excel.XlRgbColor.rgbWhite;
                            }

                            if (t > 6)
                            {
                                sh.Cells[i + 3, NumToLetter(t + 1)].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Weight = Excel.XlBorderWeight.xlThin;
                            }
                            else
                            {
                                sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Item[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                                sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Item[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThin;
                                sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Item[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                                sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Item[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThin;
                                if (t == Dgv.Columns.Count - 1)
                                {
                                    sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Item[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                                    sh.Cells[i + 3, NumToLetter(t + 1)].Borders.Item[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThin;
                                }
                            }
                        }
                    }


                    sh.Cells[2, "A"].Value2 = "THP";
                    sh.Cells[2, "A"].Font.Bold = true;
                    sh.get_Range("A2:A2").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("A2:A2").Borders.Weight = Excel.XlBorderWeight.xlMedium;

                    sh.Range[sh.Cells[3, "A"], sh.Cells[18, "A"]].Merge();
                    sh.Cells[3, "A"].Font.Bold = true;
                    sh.Cells[3, "A"].Font.Size = 18;
                    sh.Cells[3, "A"].Value2 = LblThp.Text;
                    sh.get_Range("A3:A18").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("A3:A18").Borders.Weight = Excel.XlBorderWeight.xlMedium;
                    sh.Cells[3, "A"].Orientation = Excel.XlOrientation.xlUpward;
                    sh.Cells[3, "A"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    sh.Cells[3, "A"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    sh.Range[sh.Cells[1, "B"], sh.Cells[1, "D"]].Merge();
                    sh.Cells[1, "B"].Font.Bold = true;
                    sh.Cells[1, "B"].Value2 = LblElevation.Text;
                    sh.get_Range("B1:D1").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("B1:D1").Borders.Weight = Excel.XlBorderWeight.xlMedium;
                    sh.Cells[1, "B"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    sh.Range[sh.Cells[1, "E"], sh.Cells[1, "F"]].Merge();
                    sh.Cells[1, "E"].Font.Bold = true;
                    sh.Cells[1, "E"].Value2 = "Listed";
                    sh.Cells[1, "E"].Interior.Color = Excel.XlRgbColor.rgbYellow;
                    sh.get_Range("E1:F1").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("E1:F1").Borders.Weight = Excel.XlBorderWeight.xlMedium;
                    sh.Cells[1, "E"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    sh.Range[sh.Cells[1, "G"], sh.Cells[1, "H"]].Merge();
                    sh.Cells[1, "G"].Font.Bold = true;
                    sh.Cells[1, "G"].Value2 = "CRPR 1, 2";
                    sh.Cells[1, "G"].Interior.Color = Excel.XlRgbColor.rgbCornflowerBlue;
                    sh.get_Range("G1:H1").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("G1:H1").Borders.Weight = Excel.XlBorderWeight.xlMedium;
                    sh.Cells[1, "G"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    sh.Range[sh.Cells[1, "I"], sh.Cells[1, "J"]].Merge();
                    sh.Cells[1, "I"].Font.Bold = true;
                    sh.Cells[1, "I"].Value2 = "CRPR 3, 4";
                    sh.Cells[1, "I"].Interior.Color = Excel.XlRgbColor.rgbSalmon;
                    sh.get_Range("I1:J1").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sh.get_Range("I1:J1").Borders.Weight = Excel.XlBorderWeight.xlMedium;
                    sh.Cells[1, "I"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    sh.Columns.AutoFit();
                    sh.Columns["B:H"].ColumnWidth = 14;
                    wb.SaveAs(sfd.FileName);
                    wb.Close(true);
                    excel.Quit();

                    w.Stop();

                    if (System.Windows.MessageBoxResult.Yes == System.Windows.MessageBox.Show("The timeline report has been written to '" + sfd.FileName + "'. Would you like to open this document?", "", MessageBoxButton.YesNo))
                    {
                        new Process { StartInfo = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true } }.Start();
                    }

                    //if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("The timeline report has been written to '" + sfd.FileName + "'. Would you like to open this document?", "", MessageBoxButtons.YesNo))
                    //{
                    //    System.Diagnostics.Process.Start(sfd.FileName);
                    //}
                }
            }
        }


        private string NumToLetter(int i)
        {
            int dividend = i + 1;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
    }
}
