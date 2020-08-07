using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Model.Report;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class ReportParameterTest : BaseFixture
    {
        #region Table Adapters
        DatabaseGatewayTableAdapters.TA_ReportTableAdapter reportTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ReportTableAdapter();
        DatabaseGatewayTableAdapters.TA_ReportParameterTableAdapter reportParamTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ReportParameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_ReportUIParameterTableAdapter uiParamterTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ReportUIParameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_ReportFileTableAdapter reportFileTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ReportFileTableAdapter();
        #endregion

        #region ADOObjects
        BReportParameter busReportParameter;
        ReportParameter reportParameter_testObject;

        Report ADOReportRoot = new Report();
        Report ADOReport1 = new Report();
        Report ADOReport2 = new Report();

        ReportParameter ADOReportParam1 = new ReportParameter();
        ReportParameter ADOReportParam2 = new ReportParameter();

        ReportFile ADOReportFile1 = new ReportFile();

        ReportUIParameter ADOReportUIParam1 = new ReportUIParameter();

        #endregion

        [SetUp]
        public void TestSetup()
        {

            busReportParameter = new BReportParameter();
            reportParameter_testObject = new ReportParameter();

            DatabaseGateway.TA_ReportDataTable reportTable = new DatabaseGateway.TA_ReportDataTable();
            reportTable = reportTA.GetRoot();
            if (reportTable.Rows.Count == 0)
            {
                reportTA.Insert("TestRoot", 0, "", null, false);
                reportTable = reportTA.GetRoot();
            }

            ADOReportRoot.ID = Convert.ToInt32(reportTable.Rows[0]["report_ID"]);
            ADOReportRoot.ParentId = Utility.ToInteger(reportTable.Rows[0]["report_ParentID"]);
            ADOReportRoot.Name = Convert.ToString(reportTable.Rows[0]["report_Name"]);

            reportTA.Insert("TestReport1", ADOReportRoot.ID, "", null, false);
            reportTA.Insert("TestReport2", ADOReportRoot.ID, "", null, false);

            reportTable = reportTA.GetDataByName("TestReport1");
            ADOReport1.ID = Convert.ToInt32(reportTable.Rows[0]["report_ID"]);
            ADOReport1.ParentId = Utility.ToInteger(reportTable.Rows[0]["report_ParentID"]);
            ADOReport1.Name = Convert.ToString(reportTable.Rows[0]["report_Name"]);

            reportTable = reportTA.GetDataByName("TestReport2");
            ADOReport2.ID = Convert.ToInt32(reportTable.Rows[0]["report_ID"]);
            ADOReport2.ParentId = Utility.ToInteger(reportTable.Rows[0]["report_ParentID"]);
            ADOReport2.Name = Convert.ToString(reportTable.Rows[0]["report_Name"]);

            reportFileTA.Insert("TestReportFile1", "XML");
            reportFileTA.GetDataByName("TestReportFile1");

            DatabaseGateway.TA_ReportFileDataTable reportFileDT = new DatabaseGateway.TA_ReportFileDataTable();
            reportFileDT = reportFileTA.GetDataByName("TestReportFile1");
            ADOReportFile1.ID = Convert.ToInt32(reportFileDT.Rows[0]["reportfile_ID"]);
            ADOReportFile1.Name = Convert.ToString(reportFileDT.Rows[0]["reportfile_Name"]);


            uiParamterTA.Insert("TestUIParam1", "TestPage.aspx", "FNNAME", "ENNAME", 1, "Action1", false);
            DatabaseGateway.TA_ReportUIParameterDataTable uiParamtable = uiParamterTA.GetDataByKey("TestPage.aspx");
            ADOReportUIParam1.ID = Convert.ToDecimal(uiParamtable.Rows[0][0]);

            reportParamTA.Insert(ADOReportUIParam1.ID, "TestParam1", ADOReportFile1.ID);
            reportParamTA.Insert(ADOReportUIParam1.ID, "TestParam2", ADOReportFile1.ID);

            DatabaseGateway.TA_ReportParameterDataTable paramTable = reportParamTA.GetDataByReportId(ADOReportFile1.ID);
            ADOReportParam1.ID = Convert.ToDecimal(paramTable.Rows[0]["ReportParameter_ID"]);
            ADOReportParam1.Name = Convert.ToString(paramTable.Rows[0]["ReportParameter_Name"]);

            ADOReportParam2.ID = Convert.ToDecimal(paramTable.Rows[1]["ReportParameter_ID"]);
            ADOReportParam2.Name = Convert.ToString(paramTable.Rows[1]["ReportParameter_Name"]);



        }

        [TearDown]
        public void TreatDown()
        {
            reportTA.DeleteByName("TestReport1");
            reportTA.DeleteByName("TestReport2");
            reportParamTA.DeleteByName("TestParam1");
            reportParamTA.DeleteByName("TestParam2");
            uiParamterTA.DeleteByName("TestUIParam1");
            reportFileTA.DeleteByName("TestReportFile1");

        }

        [Test]
        public void GetReportParameter_Test()
        {
            IList<ReportUIParameter> list = busReportParameter.GetUIReportParameters(ADOReportFile1.ID);
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.First().ParameterTitle.Length > 0);
        }

        [Test]
        public void CheckReportFile_Test()
        {
            IList<ReportUIParameter> list = busReportParameter.GetUIReportParameters(ADOReportFile1.ID);
            Assert.AreEqual(ADOReportUIParam1.ID, list.First().ID);
        }

        [Test]
        public void UseRepartParameterFactory_Test() 
        {
            IBControlParameter bus = BusinessFactory.GetBusiness<IBControlParameter>("PersonDateRange");
            
        }

        [Test]
        public void ShowReport_Test() 
        {
            IList<ReportUIParameter> parmeters = new List<ReportUIParameter>();
            parmeters.Add(new ReportUIParameter() { Value = "@Order=1;@ToDate=2010/05/01", ActionId = "PersonDateRange" });
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.PersonId = 32682;

            Stimulsoft.Report.StiReport report = busReportParameter.GetReport(1, proxy, parmeters);
            
        }

        [Test]
        public void ShowReport_BControlParameter_ToDate_Explicit_StartOfYear_EndOfYear_Test()
        {
            IList<ReportUIParameter> parmeters = new List<ReportUIParameter>();
            parmeters.Add(new ReportUIParameter() { Value = "@FromDate=2012/03/20;@ToDate=2012/09/04", ActionId = "ToDate_Implicit_StartOfYear_EndOfYear" });
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.PersonId = 32682;

            Stimulsoft.Report.StiReport report = busReportParameter.GetReport(70, proxy, parmeters);

        }

        [Test]
        public void ParsParam_Test() 
        {
            IDictionary<string, object> parms = BaseControlParameter.ParsParameter("@fromDate=123;@toDate=1391/01/02;");
            Assert.AreEqual(2, parms.Count);
            Assert.AreEqual("123", parms["fromDate"]);
            Assert.AreEqual("1391/01/02", parms["toDate"]);
        }

        [Test]
        public void ParsParam_Format_SemiColonTest()
        {
            try
            {
                IDictionary<string, object> parms = BaseControlParameter.ParsParameter("@fromDate=123;@toDate=1391/01/02");
            }
            catch (ReportParameterIsNotMatchException ex) 
            {
                Assert.AreEqual(UIFatalExceptionIdentifiers.ReportParameterParsingSplitSign, ex.FatalExceptionIdentifier);
            }
        }

        [Test]
        public void ParsParam_Format_AtsignColonTest()
        {
            try
            {
                IDictionary<string, object> parms = BaseControlParameter.ParsParameter("@fromDate=123;toDate=1391/01/02;");
            }
            catch (ReportParameterIsNotMatchException ex)
            {
                Assert.AreEqual(UIFatalExceptionIdentifiers.ReportParameterParsingSplitSign, ex.FatalExceptionIdentifier);
            }
        }

        [Test]
        public void ParsParam_Format_equalColonTest()
        {
            try
            {
                IDictionary<string, object> parms = BaseControlParameter.ParsParameter("@fromDate=123;@toDate1391/01/02;");
            }
            catch (ReportParameterIsNotMatchException ex)
            {
                Assert.AreEqual(UIFatalExceptionIdentifiers.ReportParameterParsingSplitSign, ex.FatalExceptionIdentifier);
            }
        }

        [Test]
        public void ParsParam_Format_Test()
        {
            try
            {
                IDictionary<string, object> parms = BaseControlParameter.ParsParameter("@fromDate=123;toDate1391/01/02");
            }
            catch (ReportParameterIsNotMatchException ex)
            {
                Assert.Pass();
            }
        }
    }
}
