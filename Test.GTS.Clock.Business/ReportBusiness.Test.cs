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
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class Report_Test : BaseFixture
    {
        #region Table Adapters
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGatewayTableAdapters.TA_ReportTableAdapter reportTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ReportTableAdapter();
        DatabaseGatewayTableAdapters.TA_ReportFileTableAdapter reportFileTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ReportFileTableAdapter();
        #endregion

        #region ADOObjects
        BReport busReport;
        Report report_testObject;
        Person ADOPerson1 = new Person();
        Report ADOReportRoot = new Report();
        Report ADOReport1 = new Report();
        Report ADOReport2 = new Report();
        Report ADOReport3 = new Report();
        ReportFile ADOReportFile1 = new ReportFile();
        ReportFile ADOReportFile2 = new ReportFile();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            report_testObject = new Report();
            busReport = new BReport();
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

            reportFileTA.Insert("TestReportFile1", "xml");
            reportFileTA.Insert("TestReportFile2", "xml");

            DatabaseGateway.TA_ReportFileDataTable reportFileDT = new DatabaseGateway.TA_ReportFileDataTable();
            reportFileDT = reportFileTA.GetDataByName("TestReportFile1");
            ADOReportFile1.ID = Convert.ToInt32(reportFileDT.Rows[0]["reportfile_ID"]);
            ADOReportFile1.Name = Convert.ToString(reportFileDT.Rows[0]["reportfile_Name"]);

            reportFileDT = reportFileTA.GetDataByName("TestReportFile2");
            ADOReportFile2.ID = Convert.ToInt32(reportFileDT.Rows[0]["reportfile_ID"]);
            ADOReportFile2.Name = Convert.ToString(reportFileDT.Rows[0]["reportfile_Name"]);

            reportTA.Insert("TestReport3", ADOReport2.ID, "", ADOReportFile2.ID, true);
            reportTable = reportTA.GetDataByName("TestReport3");
            ADOReport3.ID = Convert.ToInt32(reportTable.Rows[0]["report_ID"]);
            ADOReport3.ParentId = Utility.ToInteger(reportTable.Rows[0]["report_ParentID"]);
            ADOReport3.Name = Convert.ToString(reportTable.Rows[0]["report_Name"]);
            ADOReport3.ReportFile = new ReportFile() { ID = ADOReportFile2.ID };
            ADOReport3.IsReport = true;

        }

        [TearDown]
        public void TreatDown()
        {
            reportTA.DeleteByName("TestReport1");
            reportTA.DeleteByName("TestReport2");
            reportTA.DeleteByName("TestRoot");
            reportTA.DeleteByName("Updated");
            reportTA.DeleteByName("TestReport3");
            reportTA.DeleteByName("TestFileReport1");

            reportFileTA.DeleteByName("TestReportFile1");
            reportFileTA.DeleteByName("TestReportFile2");
        } 

        [Test]
        public void GetAll_Test()
        {
            Assert.IsTrue(busReport.GetAll().Where(x => x.ID == ADOReport1.ID).Count() > 0);
            Assert.IsTrue(busReport.GetAll().Where(x => x.ID == ADOReport2.ID).Count() > 0);

        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busReport.SaveChanges(ADOReport1, UIActionType.DELETE);
                ClearSession();
                report_testObject = busReport.GetByID(ADOReport1.ID);
                Assert.Fail("Item is not deleted");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void Delete_RootTest()
        {
            try
            {
                busReport.SaveChanges(ADOReportRoot, UIActionType.DELETE);
                Assert.Fail("ریشه نمیتواند حذف شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportRootDeleteIllegal));
            }

        }

        [Test]
        public void Insert_NameNullValidate()
        {
            try
            {
                report_testObject.ID = ADOReport1.ID;
                report_testObject.Name = null;
                report_testObject.ParentId = ADOReport1.ParentId;
                busReport.SaveChanges(report_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportNameRequierd));
            }
        }

        [Test]
        public void Insert_DublicateNameValidate()
        {
            try
            {
                report_testObject.ParentId = ADOReport1.ParentId;
                report_testObject.Name = ADOReport1.Name;
                busReport.SaveChanges(report_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportRepeatedName));
            }
        }

        [Test]
        public void Insert_DuplicateNameInOtheLevelTest()
        {
            try
            {
                report_testObject.Name = ADOReport1.Name;
                report_testObject.ParentId = ADOReport1.ID;
                busReport.SaveChanges(report_testObject, UIActionType.ADD);
                Assert.Greater(report_testObject.ID, 0);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(" نام تکراری پذیرفته شود که در سطوح مختلف باشد");
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_EmptyParentIdTest() 
        {
            try
            {
                report_testObject.Name = "Test123";
                busReport.SaveChanges(report_testObject, UIActionType.ADD);
                Assert.Fail("Emty Parent Id");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportParentIDRequierd));
            }
        }

        [Test]
        public void Insert_EmptyFileIdTest()
        {
            try
            {
                report_testObject.Name = "Test123";
                busReport.InsertReport(ADOReport1.ID,0,"TestingReport");
                Assert.Fail("File is empty");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportFileNotSpecified));
            }
        }


        [Test]
        public void Insert_ReportAsParentIdTest()
        {
            try
            {
                report_testObject.Name = "Test123";
                report_testObject.ParentId = ADOReport3.ID;
                busReport.SaveChanges(report_testObject, UIActionType.ADD);
                Assert.Fail("Report As Parent Id");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportCanNotBeParent));
            }
        }

        [Test]
        public void Update_Test()
        {
            report_testObject.ID = ADOReport1.ID;
            report_testObject.Name = "Updated";
            report_testObject.ParentId = ADOReport1.ParentId;
            busReport.SaveChanges(report_testObject, UIActionType.EDIT);
            ClearSession();
            report_testObject = busReport.GetByID(ADOReport1.ID);
            Assert.AreEqual(report_testObject.Name, "Updated");
        }

        [Test]
        public void Update_InvalideParentValidateTest()
        {
            try
            {
                report_testObject.ID = ADOReport1.ID;
                report_testObject.Name = ADOReport1.Name;
                busReport.SaveChanges(report_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportParentIDRequierd));
            }
        }

        [Test]
        public void Update_DublicateNameValidate()
        {
            try
            {
                report_testObject.ID = ADOReport2.ID;
                report_testObject.ParentId = ADOReport2.ParentId;
                report_testObject.Name = ADOReport1.Name;
                busReport.SaveChanges(report_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ReportRepeatedName));
            }
        }

        [Test]
        public void Update_RootTest()
        {
            report_testObject.ID = ADOReportRoot.ID;
            report_testObject.Name = "RootUpdated";
            busReport.SaveChanges(report_testObject, UIActionType.EDIT);
            ClearSession();
            report_testObject = busReport.GetByID(ADOReportRoot.ID);
            Assert.AreEqual(report_testObject.Name, "RootUpdated");
        }

        [Test]
        public void GetAll_ReportFile() 
        {
            IList<ReportFile> list= busReport.GetAllReportFiles();
            Assert.IsTrue(list.Where(x => x.ID == ADOReportFile1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOReportFile2.ID).Count() == 1);
        }

        [Test]
        public void GetChild_GetFileList_Test() 
        {
            try
            {
                dataAccessReportTA.Insert(BUser.CurrentUser.ID, ADOReport1.ID, false);
                dataAccessReportTA.Insert(BUser.CurrentUser.ID, ADOReport2.ID, false);
                dataAccessReportTA.Insert(BUser.CurrentUser.ID, ADOReport3.ID, false);

                report_testObject = busReport.GetReportRoot();
                IList<Report> childs = busReport.GetReportChilds(report_testObject.ID);

                Assert.IsNotNull(childs);
                Assert.IsTrue(childs.Count > 0);
                Assert.IsTrue(childs.Where(x => x.ID == ADOReport2.ID).Count() == 1);

                report_testObject = childs.Where(x => x.ID == ADOReport2.ID).First();
                Assert.AreEqual(2, childs.Count);

                childs = busReport.GetReportChilds(ADOReport2.ID);
                Assert.AreEqual(ADOReportFile2.ID, childs.First().ReportFile.ID);
                Assert.IsTrue(childs.First().IsReport);
            }
            catch (Exception ex) 
            {
                Assert.Fail();
            }
        }

        [Test]
        public void InsertReport_Test() 
        {
            decimal id = busReport.InsertReport(ADOReportRoot.ID, ADOReportFile1.ID, "TestFileReport1");
            ClearSession();
            report_testObject = busReport.GetByID(id);
            Assert.IsTrue(report_testObject.IsReport);
            
        }

        [Test]
        public void UpdateReport_Test()
        {
            decimal id = busReport.UpdateReport(ADOReport3.ID, ADOReportFile1.ID, "TestFileReport1");
            ClearSession();
            report_testObject = busReport.GetByID(id);
            Assert.IsTrue(report_testObject.IsReport);
            Assert.AreEqual(report_testObject.ReportFile.ID, ADOReportFile1.ID);

        }

        [Test]
        public void Test2222() 
        {
            dataAccessReportTA.Insert(BUser.CurrentUser.ID, null, true);
            report_testObject= busReport.GetReportRoot();
            IList<Report> list= busReport.GetReportChilds(report_testObject.ID);
          
        }
    }
}
