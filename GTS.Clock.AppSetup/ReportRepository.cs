using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportReport
{
    class ReportRepository
    {
        ReportEntities report = new ReportEntities();
        TA_ReportParameter reportParameter = new TA_ReportParameter();
        TA_Report rep = new TA_Report();
        TA_ReportFile reportFile = new TA_ReportFile();

        public Dictionary<decimal, string> GetParameters()
        {
            return report.TA_ReportUIParameter.Select(s => new { s.RptUIParameter_ID, s.RptUIParameter_Name }).ToDictionary(key=>key.RptUIParameter_ID,value=>value.RptUIParameter_Name);
        }

        public Dictionary<decimal, string> GetReports()
        {
            return report.TA_Report.Where(w => w.Report_IsReport == true).Select(s => new { s.Report_ID, s.Report_Name }).ToDictionary(key => key.Report_ID, value => value.Report_Name);
        } 

        public Dictionary<decimal, string> GetRoots()
        {
            return report.TA_Report.Where(w => w.Report_IsReport == false).Select(s => new { s.Report_ID, s.Report_Name }).ToDictionary(key=>key.Report_ID,value=>value.Report_Name);
        }

        public bool DeleteReportParameterByName(string name)
        {
            try
            {
                report.TA_ReportParameter
                    .Where(w=>w.ReportParameter_RptFileId==report.TA_ReportFile
                        .Where(x=>x.ReportFile_Name==name)
                        .Select(s=>s.ReportFile_ID).FirstOrDefault())
                        .ToList()
                        .ForEach(report.TA_ReportParameter.DeleteObject);
                report.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteReportByName(string name)
        {
            try
            {
                report.TA_Report.Where(w=>w.Report_ReportFileId==report.TA_ReportFile.Where(x=>x.ReportFile_Name==name).Select(s=>s.ReportFile_ID).FirstOrDefault()).ToList().ForEach(report.TA_Report.DeleteObject);
                report.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteReportFileByName(string name)
        {
            try
            {
                report.TA_ReportFile.Where(w=>w.ReportFile_Name==name).ToList().ForEach(report.TA_ReportFile.DeleteObject);
                report.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddReportFile(string name,string file,string description)
        {
            try
            {
                reportFile.ReportFile_Name = name;
                reportFile.ReportFile_File=file;
                reportFile.ReportFile_Description=description;
                report.TA_ReportFile.AddObject(reportFile);
                report.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddReport(string name, decimal parentId, decimal reportFileId, bool isReport, string parentPath,int order)
        {
            try
            {
                rep.Report_Name = name;
                rep.Report_ParentId = parentId;
                rep.Report_ReportFileId = reportFileId;
                rep.Report_IsReport = isReport;
                rep.Report_ParentPath = parentPath;
                rep.Report_Order = order;
                report.TA_Report.AddObject(rep);
                report.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddReportParameter(decimal reportUiParameterId, string code, decimal reportFileId)
        {
            try
            {
                switch (code)
                {
                    case "01":
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@fromDate", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@toDate", ReportParameter_RptFileId = reportFileId });
                        break;
                    case "02":
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@Order", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@ToDate", ReportParameter_RptFileId = reportFileId });
                        break;
                    case "03":
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@Order", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@ToDate", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@Value", ReportParameter_RptFileId = reportFileId });
                        break;
                    case "04":
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@toDate", ReportParameter_RptFileId = reportFileId });
                        break;
                    case "05":
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@fromDate", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@toDate", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@stationID", ReportParameter_RptFileId = reportFileId });
                        report.TA_ReportParameter.AddObject(new TA_ReportParameter() { ReportParameter_RptUIParamId = reportUiParameterId, ReportParameter_Name = "@ClockID", ReportParameter_RptFileId = reportFileId });
                        break;
                }

                report.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public decimal GetReportFileIdByName(string name)
        {
            try
            {
                return report.TA_ReportFile.Where(w => w.ReportFile_Name == name)
                      .Select(s => s.ReportFile_ID)
                      .SingleOrDefault();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int GetMaxReportOrder()
        {
            try
            {
                return report.TA_Report.Select(s => s.Report_Order).Max()??0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public decimal GetReportUiParameterIdByName(string name)
        {
            try
            {
                return report.TA_ReportUIParameter.Where(w => w.RptUIParameter_Name == name)
                      .Select(s => s.RptUIParameter_ID)
                      .FirstOrDefault();
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
