using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace WBIS_2.Modules.Tools
{
    public class ExcelTools
    {
        public static DataTable GetExcelTableFirst(string FileName)
        {
            DataTable dataTable = new DataTable();
            var app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(FileName);
            Excel.Worksheet sheet = workbook.Sheets[1];
            Excel.Range last = sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var rowArray = sheet.Range["A1", last].Value;
            app.Workbooks.Close();

            for (int c = 0; c < rowArray.GetUpperBound(1); c++)
            {
                dataTable.Columns.Add();
            }
            for (int r = 0; r < rowArray.GetUpperBound(0); r++)
            {
                dataTable.Rows.Add();
            }

            for (int r = 0; r < rowArray.GetUpperBound(0); r++)
            {
                for (int c = 0; c < rowArray.GetUpperBound(1); c++)
                {
                    dataTable.Rows[r][c] = rowArray[r + 1, c + 1];
                }
            }

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                dataTable.Columns[i].ColumnName = dataTable.Rows[0][i].ToString();
            }
            dataTable.Rows.RemoveAt(0);
            return dataTable;
        }
        public static void BasicExcelSave(string FileName, DataTable dt, bool BoldHeader, bool HeaderGrid)
        {
            var excel = new Excel.Application { Visible = false };
            var misValue = System.Reflection.Missing.Value;
            var wb = excel.Workbooks.Add(misValue);

            Excel.Worksheet sh = wb.Sheets.Add();

            object[,] printArray = new object[dt.Rows.Count + 1, dt.Columns.Count + 1];
            for (int c = 0; c < dt.Columns.Count; c++)
                printArray[0, c] = dt.Columns[c].ColumnName;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                    if (dt.Rows[i][c] is DBNull)
                        printArray[i + 1, c] = "";
                    else
                        printArray[i + 1, c] = dt.Rows[i][c].ToString();
            }
            sh.get_Range("A1" + ":" + NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Value2 = printArray;

            if (HeaderGrid)
            {
                sh.get_Range("A1" + ":" + NumToLetter(dt.Columns.Count - 1) + 1).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                sh.get_Range("A1" + ":" + NumToLetter(dt.Columns.Count - 1) + 1).Borders.Weight = Excel.XlBorderWeight.xlThin;
            }
            if (BoldHeader)
                sh.get_Range("A1" + ":" + NumToLetter(dt.Columns.Count - 1) + 1).Font.Bold = true;

            sh.Cells.EntireColumn.AutoFit();

            int wsCount = wb.Sheets.Count + 1;
            if (wsCount > 1)
            {
                for (int i = 1; i < wsCount; i++)
                {
                    Excel.Worksheet wsDelete = wb.Sheets[i];
                    if (wsDelete.Name == "Sheet1")
                    {
                        wsDelete.Delete();
                        break;
                    }
                }
            }

            wb.SaveAs(FileName);
            wb.Close(true);
            excel.Quit();
        }

        public static string NumToLetter(int i)
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



        public static DataTable EntityToDatatable(IInfoTypeManager manager, IQueryable records)
        {
            Type i = manager.InformationType;
            DataTable dt = new DataTable();
            var propertyColumns = manager.DisplayFields;
            foreach (var property in propertyColumns)
            {
                if (property.DataType == typeof(Guid) && property.FullName != "Guid") continue;
                if (property.DataType.Namespace == "NetTopologySuite.Geometries") continue;
                dt.Columns.Add(new DataColumn(property.ShapefileColumn, property.DataType));
            }

            foreach (var record in records)
            {
                DataRow dataRow = dt.NewRow();
                foreach (var col in propertyColumns)
                {
                    if (col.DataType == typeof(Guid) && col.FullName != "Guid") continue;
                    if (col.FullName.Contains("."))
                    {
                        PropertyInfo prop = null;
                        object val = null;
                        var parts = col.FullName.Split('.');
                        foreach (var part in parts)
                        {
                            if (prop == null)
                            {
                                prop = i.GetProperty(part);
                                val = prop.GetValue(record);
                            }
                            else
                            {
                                prop = prop.PropertyType.GetProperty(part);
                                if (val != null)
                                    val = prop.GetValue(val);
                            }
                        }
                        if (val != null)
                            dataRow[col.ShapefileColumn] = val;
                    }
                    else
                    {
                        var prop = i.GetProperty(col.FullName);
                        var val = prop.GetValue(record);
                        if (val != null)
                            dataRow[col.ShapefileColumn] = val;
                    }
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
          

        public static void WriteBotanyThpReportTable(DataTable dt, Excel.Worksheet sheet, int startRow, bool survey)
        {
            object[,] printArray = new object[dt.Rows.Count + 1, dt.Columns.Count];
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                printArray[0, c] = dt.Columns[c].ColumnName;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Rows[i][c] is DBNull)
                    {
                        printArray[i + 1, c] = "";
                        continue;
                    }

                    if (dt.Columns[c].ColumnName == "Lat" || dt.Columns[c].ColumnName == "Lon")
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N5");
                    }
                    else if (dt.Columns[c].DataType == typeof(DateTime))
                    {
                        if (survey) printArray[i + 1, c] = ((DateTime)dt.Rows[i][c]).ToShortDateString();
                        else printArray[i + 1, c] = ((DateTime)dt.Rows[i][c]).ToShortDateString() + " " + ((DateTime)dt.Rows[i][c]).ToShortTimeString();
                    }
                    else if (dt.Columns[c].DataType == typeof(double))
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N2");
                    }
                    else
                    {
                        printArray[i + 1, c] = dt.Rows[i][c].ToString();
                    }
                }
            }

            sheet.Rows.RowHeight = 15;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + startRow)).Value2 = printArray;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + startRow)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + startRow)).Borders.Weight = Excel.XlBorderWeight.xlThin;
            sheet.get_Range("A" + startRow + ":" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + startRow).Interior.Color = Excel.XlRgbColor.rgbYellow;
            sheet.Columns.AutoFit();
        }

        public static void WriteBotanyTable(DataTable dt, Excel.Worksheet sheet)
        {
            object[,] printArray = new object[dt.Rows.Count + 1, dt.Columns.Count];
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                printArray[0, c] = dt.Columns[c].ColumnName;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Rows[i][c] is DBNull)
                    {
                        printArray[i + 1, c] = "";
                        continue;
                    }

                    if (dt.Columns[c].ColumnName == "Lat" || dt.Columns[c].ColumnName == "Lon")
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N5");
                    }
                    else if (dt.Columns[c].DataType == typeof(DateTime))
                    {
                        printArray[i + 1, c] = ((DateTime)dt.Rows[i][c]).ToShortDateString() + " " + ((DateTime)dt.Rows[i][c]).ToShortTimeString();
                    }
                    else if (dt.Columns[c].DataType == typeof(double))
                    {
                        printArray[i + 1, c] = ((double)dt.Rows[i][c]).ToString("N2");
                    }
                    else
                    {
                        printArray[i + 1, c] = dt.Rows[i][c].ToString();
                    }
                }
            }

            sheet.Rows.RowHeight = 15;
            sheet.get_Range("A1:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Value2 = printArray;
            sheet.get_Range("A1:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.get_Range("A2:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + (dt.Rows.Count + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;
            sheet.get_Range("A1:" + ExcelTools.NumToLetter(dt.Columns.Count - 1) + 1).Interior.Color = Excel.XlRgbColor.rgbYellow;
            sheet.Columns.AutoFit();
        }
    }
}
