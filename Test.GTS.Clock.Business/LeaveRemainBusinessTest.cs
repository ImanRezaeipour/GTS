using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-01-29 2:43:24 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class LeaveRemainBusinessTest : BaseFixture
    { 
        DatabaseGatewayTableAdapters.TA_LeaveYearRemainTableAdapter leaveYearRemainTA = new TA_LeaveYearRemainTableAdapter();
        DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter dataAcesDepTA = new TA_DataAccessDepartmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_LeaveSettingsTableAdapter leaveSetTA = new TA_LeaveSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_UsedBudgetTableAdapter usedTA = new TA_UsedBudgetTableAdapter();
        DatabaseGatewayTableAdapters.TA_BudgetYearTableAdapter budgetYearTA = new TA_BudgetYearTableAdapter();
        DatabaseGatewayTableAdapters.TA_LeaveCalcResultTableAdapter calcResultTA = new TA_LeaveCalcResultTableAdapter();
        DatabaseGatewayTableAdapters.TA_UsedLeaveDetailTableAdapter UsedLeaveDtlTA = new TA_UsedLeaveDetailTableAdapter();

        DatabaseGateway.TA_LeaveYearRemainDataTable leaveTable = new DatabaseGateway.TA_LeaveYearRemainDataTable();
        DatabaseGateway.TA_UsedLeaveDetailDataTable UsedleaveTable = new DatabaseGateway.TA_UsedLeaveDetailDataTable();

        BRemainLeave busRemainLeave;
        LeaveYearRemain leave_testObject;
        LeaveYearRemain ADOLeaveYear = new LeaveYearRemain();

        [SetUp]
        public void TestSetup()
        {
            busRemainLeave = new BRemainLeave();
            leave_testObject = new LeaveYearRemain();

            leaveYearRemainTA.Insert(Utility.ToMildiDate("1389/01/01"), 10, 10, 9, 9, ADOPerson1.ID);

            leaveTable = leaveYearRemainTA.GetDataByPersonId(ADOPerson1.ID, Utility.ToMildiDate("1389/01/01"));
            ADOLeaveYear.ID = ((DatabaseGateway.TA_LeaveYearRemainRow)leaveTable.Rows[0]).LeaveYearRemain_ID;
            ADOLeaveYear.PersonId = ADOPerson1.ID;
            ADOLeaveYear.Date = Utility.ToMildiDate("1389/01/01");
            ADOLeaveYear.DayRemainOK = 10;
            ADOLeaveYear.MinuteRemainOK = 10;
            ADOLeaveYear.DayRemainReal = 9;
            ADOLeaveYear.MinuteRemainReal = 9;

            leaveSetTA.InsertQuery(ADOPerson1.ID, DateTime.Now.AddMonths(-1), true, 1440);

            DateTime  endYear = Utility.ToMildiDate("1389/05/10");

            UsedLeaveDtlTA.Insert(endYear, 480, 0, 480, ADOPerson1.ID, 0);
            UsedleaveTable = UsedLeaveDtlTA.GetDataByValue(ADOPerson1.ID);
            calcResultTA.Insert(endYear, 0, 0, ADOPerson1.ID, 0, 0, 0, 0, 480, (decimal)UsedleaveTable.Rows[0][0], "ULD");

            dataAcesDepTA.Insert(BUser.CurrentUser.ID, ADODepartment1.ID, false);
        }

        [TearDown]
        public void TreatDown()
        {
            leaveYearRemainTA.DeleteByPersonId(ADOPerson1.ID);

            UsedLeaveDtlTA.DeleteByPersonId(ADOPerson1.ID);
        }

        [Test]
        public void GetById_Test()
        {
            leave_testObject = busRemainLeave.GetByID(ADOLeaveYear.ID);
            Assert.Pass();
        }

        [Test]
        public void GetRemainLeave_ByPersonId_Test()
        {
            IList<RemainLeaveProxy> proxyList = busRemainLeave.GetRemainLeave(1389, 1390, 0, 10);
            Assert.IsTrue(proxyList.Count >= 1);
            Assert.IsTrue(proxyList.Where(x => x.ID == ADOLeaveYear.ID).Count() == 1);
        }

        [Test]
        public void GetRemainLeaveCount_ByPersonId_Test()
        {
            int count = busRemainLeave.GetRemainLeaveCount(1389, 1390);
            Assert.IsTrue(count >= 1);
        }

        [Test]
        public void Insert_LeaveTest() 
        {
            decimal id = busRemainLeave.InsertLeaveYear(1391, ADOPerson1.ID, "5", "05:00:00");
            Assert.IsTrue(id > 0);           
        }

        [Test]
        public void Insert_Leave_RepeatTest()
        {
            try
            {
                decimal id = busRemainLeave.InsertLeaveYear(1389, ADOPerson1.ID, "5", "05:00:00");
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RemainLeaveExists));
            }
        }

        [Test]
        public void Update_test() 
        {
            busRemainLeave.UpdateLeaveYear(ADOLeaveYear.ID, "1", "01:00:00");
            ClearSession();
            leave_testObject = busRemainLeave.GetByID(ADOLeaveYear.ID);
            Assert.AreEqual(1389, 1389);
            Assert.AreEqual(1, leave_testObject.DayRemainOK);
            Assert.AreEqual(60, leave_testObject.MinuteRemainOK);
        }

        [Test]
        public void Transfer_Test() 
        {
            busRemainLeave.TransferToNextYear(ADOPerson1.ID, 1389, 1390);

            int count = busRemainLeave.GetRemainLeaveCount(ADOPerson1.ID, 1390, 1390);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void test_22222222() 
        {
            IList<RemainLeaveProxy> list = busRemainLeave.GetRemainLeave(32682, 1390, 1390, 0, 10);

        }

    }
}
