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
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-02-06 11:40:18 AM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class LeaveIncDecBusinessTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_LeaveYearRemainTableAdapter leaveYearTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LeaveYearRemainTableAdapter();
        DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter dataAcesDepTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_LeaveSettingsTableAdapter leaveSetTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LeaveSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_UsedBudgetTableAdapter usedTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UsedBudgetTableAdapter();
        DatabaseGatewayTableAdapters.TA_BudgetYearTableAdapter budgetYearTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_BudgetYearTableAdapter();
        DatabaseGatewayTableAdapters.TA_LeaveIncDecTableAdapter leaveIncDecTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LeaveIncDecTableAdapter();

        DatabaseGateway.TA_LeaveYearRemainDataTable leaveTable = new DatabaseGateway.TA_LeaveYearRemainDataTable();
        BRemainLeave busRemainLeave;   

        LeaveIncDec leaveIncDec_testObject;
        LeaveIncDec ADOLeaveIncDec = new LeaveIncDec();
        BLeaveIncDec busLeaveIncDec;

        [SetUp]
        public void TestSetup()
        {

            leaveIncDec_testObject = new LeaveIncDec();
            busLeaveIncDec = new BLeaveIncDec();

            dataAcesDepTA.Insert(BUser.CurrentUser.ID, ADODepartment1.ID, false);

            leaveIncDecTA.Insert(ADOPerson1.ID, DateTime.Now, 0, 0, false, "");

            DatabaseGateway.TA_LeaveIncDecDataTable tableIncDec = leaveIncDecTA.GetDataPersonId(ADOPerson1.ID);
            ADOLeaveIncDec.ID = (tableIncDec.Rows[0] as DatabaseGateway.TA_LeaveIncDecRow).LeaveIncDec_ID;
        }

        [TearDown]
        public void TreatDown()
        {
            leaveYearTA.DeleteByPersonId(ADOPerson1.ID);
        }
        [Test]
        public void GetAllTest() 
        {
            IList<LeaveIncDecProxy> list = busLeaveIncDec.GetAllByPersonId(ADOPerson1.ID);
            Assert.IsTrue(list.Where(x => x.ID == ADOLeaveIncDec.ID).Count() == 1);
        }

        [Test]
        public void Insert_Test()
        {
           decimal id= busLeaveIncDec.InsertLeaveIncDec(ADOPerson2.ID, "2", "01:00:00", LeaveIncDecAction.Decrease, "", Utility.ToPersianDate(DateTime.Now));
            ClearSession();
            leaveIncDec_testObject = busLeaveIncDec.GetByID(id);
            Assert.AreEqual(LeaveIncDecAction.Decrease, leaveIncDec_testObject.Type);


        }

        [Test]
        public void Update_Test()
        {
            try 
            {
                decimal id = busLeaveIncDec.InsertLeaveIncDec(ADOPerson1.ID, "1", "01:00:00", LeaveIncDecAction.Increase, "derfew", Utility.ToPersianDate(DateTime.Now));
                ClearSession();
                leaveIncDec_testObject = busLeaveIncDec.GetByID(id);
                Assert.AreEqual(1, leaveIncDec_testObject.Day);
                Assert.AreEqual(60, leaveIncDec_testObject.Minute);
            }
            catch (IllegalServiceAccess ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busLeaveIncDec.DeleteLeaveIncDec(ADOLeaveIncDec.ID);
                ClearSession();
                busLeaveIncDec.GetByID(ADOLeaveIncDec.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void test_22222222() 
        {
            busLeaveIncDec.GetAllByPersonId(32682);
        }
    }
}
