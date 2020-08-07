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


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-01-29 2:43:24 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class LeaveBudgetBusinessTest : BaseFixture
    {
        TA_RuleCategoryTableAdapter ruleCatTA = new TA_RuleCategoryTableAdapter();
        TA_BudgetTableAdapter budgetTA = new TA_BudgetTableAdapter();

        RuleCategory AdoRuleCat = new RuleCategory();
        PersonRuleCatAssignment AdoRleCatAsg = new PersonRuleCatAssignment();
        DatabaseGateway.TA_RuleCategoryDataTable table;

        BLeaveBudget busLeave;
        Budget budget_testObject;
        Budget ADOBudget1 = new Budget();

        [SetUp]
        public void TestSetup()
        {
            busLeave = new BLeaveBudget();
            budget_testObject = new Budget();

            ruleCatTA.Insert("RuleCategory000", "0000", false, "00-00test1");
            table = ruleCatTA.GetDataByName("RuleCategory000");
            AdoRuleCat.ID = (Decimal)table[0]["RuleCat_ID"];
            AdoRuleCat.Name = (String)table[0]["RuleCat_Name"];

            budgetTA.Insert(Utility.ToMildiDate("1390/01/01"), 10,10, AdoRuleCat.ID, (int)BudgetType.Usual, "");
            DatabaseGateway.TA_BudgetDataTable budgetTable = budgetTA.GetDataByRuleCategoryId(AdoRuleCat.ID);

            ADOBudget1.ID = ((DatabaseGateway.TA_BudgetRow)budgetTable.Rows[0]).Budget_ID;
            ADOBudget1.Day = ((DatabaseGateway.TA_BudgetRow)budgetTable.Rows[0]).Budget_Day;
            ADOBudget1.Minute = ((DatabaseGateway.TA_BudgetRow)budgetTable.Rows[0]).Budget_Minute;
            ADOBudget1.Date = ((DatabaseGateway.TA_BudgetRow)budgetTable.Rows[0]).Budget_Date;
        }

        [TearDown]
        public void TreatDown()
        {
            budgetTA.DeleteByRuleCAtegoryCustomCode("00-00test1");
            ruleCatTA.DeleteByCustomCode("00-00test1");
        }

        [Test]
        public void GetById_Test()
        {
            budget_testObject = busLeave.GetByID(ADOBudget1.ID);
            Assert.Pass();
        }

        [Test]
        public void GetBudget_Test()
        {
            LeaveBudgetProxy proxy = busLeave.GetRuleBudget(AdoRuleCat.ID, 1390);
            Assert.IsTrue(proxy.TotoalDay.Length > 0);
        }

        [Test]
        public void Insert_Test()
        {
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "3";
            proxy.TotoalTime = "25:30:00";

            bool sucess = busLeave.SaveBudget(AdoRuleCat.ID, 1391, proxy);
            Assert.IsTrue(sucess);

        }

        [Test]
        public void Insert_ToalCheck()
        {
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.ID = 0;
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "3";
            proxy.TotoalTime = "23:30:00";

            bool sucess = busLeave.SaveBudget(AdoRuleCat.ID, 1391, proxy);
            Assert.IsTrue(sucess);

            proxy = busLeave.GetRuleBudget(AdoRuleCat.ID, 1391);

            Assert.IsTrue(proxy.ID > 0);
            Assert.AreEqual("3", proxy.TotoalDay);
            Assert.AreEqual("23:30", proxy.TotoalTime);

            budget_testObject = busLeave.GetByID(proxy.ID);

            Assert.AreEqual(3, budget_testObject.Day);

        }

        [Test]
        public void Insert_MonthCheck()
        {
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.ID = 0;
            proxy.BudgetType = BudgetType.PerMonth;
            proxy.Day1 = "3";
            proxy.Time1 = "23:30:00";

            bool sucess = busLeave.SaveBudget(AdoRuleCat.ID, 1391, proxy);
            Assert.IsTrue(sucess);

            proxy = busLeave.GetRuleBudget(AdoRuleCat.ID, 1391);

            Assert.IsTrue(proxy.ID > 0);
            Assert.AreEqual("3", proxy.Day1);
            Assert.AreEqual("23:30", proxy.Time1);

        }

        [Test]
        public void Update_Test()
        {
            try
            {
                busLeave.SaveChanges(budget_testObject, UIActionType.EDIT);
                Assert.Fail();
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
                busLeave.SaveChanges(ADOBudget1, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (IllegalServiceAccess ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Test222222() 
        {
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.ID = 0;
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "31";
            proxy.TotoalTime = "08:00:00";

            bool sucess = busLeave.SaveBudget(AdoRuleCat.ID, 1391, proxy);
            Assert.IsTrue(sucess);

            proxy = busLeave.GetRuleBudget(AdoRuleCat.ID, 1391);
        }


    }
}
