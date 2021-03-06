﻿using System.Collections.Generic;
using Bytescout.Spreadsheet;
using System.Data;
using System.Drawing;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    public class SimpleXMLConvertor
    {
        private readonly Spreadsheet document;

        public SimpleXMLConvertor(Spreadsheet document)
        {
            this.document = document;
        }

        private Dictionary<string,string> columnsCol;
        public Dictionary<string, string> ColumnsCol
        {
            get
            {
                return this.columnsCol;
            }
            set
            {
                this.columnsCol = value;
            }
        }

        private string workSheetName;
        public string WorkSheetName 
        { 
            get
            {
                return this.workSheetName;
            }
            set
            {
                this.workSheetName = value;
            }
        }

        public void LoadXML(string path)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);

            foreach (DataTable table in dataSet.Tables)
            {
                Worksheet worksheet = document.Workbook.Worksheets.Add(table.TableName);

                worksheet.Name = this.WorkSheetName;

                worksheet.Columns.Insert(0, table.Columns.Count);

                worksheet.Rows.Insert(0, table.Rows.Count);

                for (int k = 0; k < table.Columns.Count; k++)
                {
                        worksheet.Columns[k].Width = 100;
                        worksheet.Cell(0, k).Value = this.columnsCol[table.Columns[k].Caption];
                        worksheet.Cell(0, k).FontColor = Color.Red;
                        worksheet.Cell(0, k).BottomBorderStyle = Bytescout.Spreadsheet.Constants.LineStyle.Medium;
                        worksheet.Cell(0, k).BottomBorderColor = Color.Blue;
                        worksheet.Cell(0, k).LeftBorderStyle = Bytescout.Spreadsheet.Constants.LineStyle.Medium;
                        worksheet.Cell(0, k).LeftBorderColor = Color.Blue;
                        worksheet.Cell(0, k).RightBorderStyle = Bytescout.Spreadsheet.Constants.LineStyle.Medium;
                        worksheet.Cell(0, k).RightBorderColor = Color.Blue;
                        worksheet.Cell(0, k).AlignmentHorizontal = Bytescout.Spreadsheet.Constants.AlignmentHorizontal.Centered;
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 1, j).Value = table.Rows[i][j].ToString();
                        worksheet.Cell(i + 1, j).AlignmentHorizontal = Bytescout.Spreadsheet.Constants.AlignmentHorizontal.Centered;
                        worksheet.Cell(i + 1, j).BottomBorderStyle = Bytescout.Spreadsheet.Constants.LineStyle.Medium;
                        worksheet.Cell(i + 1, j).BottomBorderColor = Color.Blue;
                        worksheet.Cell(i + 1, j).LeftBorderStyle = Bytescout.Spreadsheet.Constants.LineStyle.Medium;
                        worksheet.Cell(i + 1, j).LeftBorderColor = Color.Blue;
                        worksheet.Cell(i + 1, j).RightBorderStyle = Bytescout.Spreadsheet.Constants.LineStyle.Medium;
                        worksheet.Cell(i + 1, j).RightBorderColor = Color.Blue;
                    }
                }
            }
        }

        public void SaveXML(string path)
        {
            DataSet dataSet = new DataSet();

            for (int i = 0; i < document.Workbook.Worksheets.Count; i++)
            {
                Worksheet worksheet = document.Workbook.Worksheets[i];

                DataTable table = dataSet.Tables.Add(worksheet.Name);

                #region Add Columns

                for (int column = 0; column <= worksheet.UsedRangeColumnMax; column++)
                {
                    table.Columns.Add(
                        string.Format("Column_{0}", column));
                }

                #endregion

                #region Add rows

                for (int row = 0; row <= worksheet.UsedRangeRowMax; row++)
                {
                    object[] data = new object[worksheet.UsedRangeColumnMax + 1];

                    for (int column = 0; column <= worksheet.UsedRangeColumnMax; column++)
                    {
                        data[column] = worksheet.Cell(row, column).Value;
                    }

                    table.Rows.Add(data);
                }

                #endregion
            }

            dataSet.WriteXml(path);
        }

    }
}
