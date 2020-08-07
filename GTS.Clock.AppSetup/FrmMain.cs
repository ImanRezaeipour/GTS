using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImportReport
{
    public partial class FrmMain : Form
    {
        ReportRepository Report = new ReportRepository();

        public bool _valid = true;
        public bool Valid { get { return _valid; } set { _valid = value; } }

        public string _reportName = "";
        public string ReportName { get { return _reportName; } set { _reportName = value; } }

        public string _reportDescription = "";
        public string ReportDescription { get { return _reportDescription; } set { _reportDescription = value; } }

        public string _reportParameter = "";
        public string ReportParameter { get { return _reportParameter; } set { _reportParameter = value; } }

        new public bool Validate()
        {
            errorProvider.Clear();
            if (txtReport.Text.Trim() == "")
            {
                errorProvider.SetError(txtReport, "انتخاب فایل اجباری است");
                Valid = false;
            }
            return Valid;
        }

        public void AddNewReport()
        {
            Valid = true;
            if (Validate())
            {
                if (!Report.DeleteReportParameterByName(ReportName))
                    throw new Exception("فایل گزارش تکراری است");
                if (!Report.DeleteReportByName(ReportName))
                    throw new Exception("فایل گزارش تکراری است");
                if (!Report.DeleteReportFileByName(ReportName))
                    throw new Exception("فایل گزارش تکراری است");

                Report.AddReportFile(ReportName, new System.IO.StreamReader(txtReport.Text).ReadToEnd(),
                                      ReportDescription);

                Report.AddReport(ReportDescription,
                                  Report.GetRoots()
                                         .Where(w => w.Value == cmbRoot.SelectedValue.ToString())
                                         .Select(s => s.Key)
                                         .Single(),
                                  Report.GetReportFileIdByName(ReportName), true,
                                  "," +
                                  Report.GetRoots().Where(w => w.Value == cmbRoot.Items[0].ToString()).Select(s => s.Key).Single().ToString() +
                "," + Report.GetRoots().Where(w => w.Value == cmbRoot.SelectedValue.ToString()).Select(s => s.Key).Single().ToString() +
                ",",
                                  Report.GetMaxReportOrder() + 1);

                switch (ReportParameter)
                {
                    case Parameters.Date_FromTo_Date:
                        Report.AddReportParameter(Report.GetReportUiParameterIdByName(ReportParameter), "01",
                                              Report.GetReportFileIdByName(ReportName));
                        break;
                    case Parameters.Date_Range_Order:
                        Report.AddReportParameter(Report.GetReportUiParameterIdByName(ReportParameter), "02",
                                              Report.GetReportFileIdByName(ReportName));
                        break;
                    case Parameters.Date_Range_Order_Value:
                        Report.AddReportParameter(Report.GetReportUiParameterIdByName(ReportParameter), "03",
                                              Report.GetReportFileIdByName(ReportName));
                        break;
                    case Parameters.FirstDayYear_To_Date:
                        Report.AddReportParameter(Report.GetReportUiParameterIdByName(ReportParameter), "04",
                                              Report.GetReportFileIdByName(ReportName));
                        break;
                    case Parameters.Station_Clock_Date:
                        Report.AddReportParameter(Report.GetReportUiParameterIdByName(ReportParameter), "05",
                                              Report.GetReportFileIdByName(ReportName));
                        break;
                    default:
                        break;
                }

                MessageBox.Show("گزارش اضافه شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        public void ImportReport()
        {
            var openFileReport = new OpenFileDialog
            {
                Multiselect = false,
                Filter = @"Stimulsoft Report Files (*.mrt)|*.mrt"
            };
            if (openFileReport.ShowDialog().Equals(DialogResult.OK))
            {
                txtReport.Text = openFileReport.FileName;
                ReportName = ReportNameResolution(openFileReport.SafeFileName)[0];
                try
                {
                    ReportDescription = ReportNameResolution(openFileReport.SafeFileName)[1];
                }
                catch (Exception)
                {
                    ReportDescription = "";
                }
                try
                {
                    ReportParameter = ReportNameResolution(openFileReport.SafeFileName)[3];
                }
                catch (Exception)
                {
                    ReportParameter = "";
                }
            }
        }

        public string[] ReportNameResolution(string reportName)
        {
            return reportName.Split('[', ']');
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportReport();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbRoot.DataSource = Report.GetRoots().Select(s => s.Value).ToList();
            cmbRoot.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewReport();
        }
    }
}
