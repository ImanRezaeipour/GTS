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
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-01-02 11:03:53 AM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class SubstituteBusinessTest : BaseSubstituteOperator
    {
        BSubstitute busSubstitute;
        Substitute substitute_testObject;       

        [SetUp]
        public void TestSetup()
        {
            busSubstitute = new BSubstitute();
            substitute_testObject = new Substitute();

            #region Substitute
            substituteTA.Insert(ADOManager1.ID, ADOPerson4.ID, DateTime.Now, DateTime.Now.AddDays(1), true);
            DatasetGatewayWorkFlow.TA_SubstituteDataTable subTable = substituteTA.GetByManager(ADOManager1.ID, ADOPerson4.ID);
            ADOSubstitute1.ID = (subTable.Rows[0] as DatasetGatewayWorkFlow.TA_SubstituteRow).sub_ID;
            ADOSubstitute1.TheFromDate = Utility.ToPersianDate(DateTime.Now);
            ADOSubstitute1.TheToDate = Utility.ToPersianDate(DateTime.Now.AddDays(1));

            substitutePrecardTA.Insert(ADOPrecardHourlyDuty1.ID, ADOSubstitute1.ID);
            substitutePrecardTA.Insert(ADOPrecardHourlyLeave1.ID, ADOSubstitute1.ID);
            substitutePrecardTA.Insert(ADOPrecardHourlyLeave2.ID, ADOSubstitute1.ID);
            substitutePrecardTA.Insert(ADOPrecardDailyLeave1.ID, ADOSubstitute1.ID);
            substitutePrecardTA.Insert(ADOPrecardDailyDuty1.ID, ADOSubstitute1.ID);
            #endregion

            #region Operator
            operatorTA.Insert(ADOPerson1.ID, true, ADOFlow1.ID, "");
            operatorTA.Insert(ADOPerson2.ID, true, ADOFlow2.ID, "");//مدیر و اپراتور

            DatasetGatewayWorkFlow.TA_OperatorDataTable opTable = operatorTA.GetByPesonId(ADOPerson1.ID);
            ADOOperator1.ID = (opTable.Rows[0] as DatasetGatewayWorkFlow.TA_OperatorRow).opr_ID;

            opTable = operatorTA.GetByPesonId(ADOPerson2.ID);
            ADOOperator2.ID = (opTable.Rows[0] as DatasetGatewayWorkFlow.TA_OperatorRow).opr_ID;


            //operatorManagerTA.Insert(ADOManager1.ID, ADOOperator1.ID);
            //operatorManagerTA.Insert(ADOManager1.ID, ADOOperator2.ID);
            #endregion
            

        }

        [TearDown]
        public void TreatDown()
        {
        }

        [Test]
        public void GetById_Test()
        {
            substitute_testObject = busSubstitute.GetByID(ADOSubstitute1.ID);
            Assert.AreEqual(ADOSubstitute1.ID, substitute_testObject.ID);
        }

        [Test]
        public void Insert_Test()
        {
            substitute_testObject.TheFromDate = Utility.ToPersianDate(DateTime.Now);
            substitute_testObject.TheToDate = Utility.ToPersianDate(DateTime.Now.AddDays(1));
            substitute_testObject.ManagerPersonId = ADOManager1.Person.ID;
            substitute_testObject.Person = ADOPerson5;
            substitute_testObject.Active = true;
            busSubstitute.SaveChanges(substitute_testObject, UIActionType.ADD);
            ClearSession();
            Assert.IsTrue(substitute_testObject.ID > 0);
            substitute_testObject = busSubstitute.GetByID(substitute_testObject.ID);
            Assert.IsNotNull(substitute_testObject.PrecardList);
            Assert.IsTrue(substitute_testObject.PrecardList.Count > 0);
        }

        [Test]
        public void Insert_Empty_Test1() 
        {
            try 
            {
                busSubstitute.SaveChanges(substitute_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteDateRequired));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteManagerRequiered));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstitutePersonRequiered));
            }
        }

        [Test]
        public void Insert_DateValidate_Test1()
        {
            try
            {
                substitute_testObject.TheFromDate = Utility.ToPersianDate(DateTime.Now);
                substitute_testObject.TheToDate = Utility.ToPersianDate(DateTime.Now.AddDays(-1));
                busSubstitute.SaveChanges(substitute_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteFromDateGreaterThanToDate));
            }
        }

        [Test]
        public void Update_Test()
        {
            substitute_testObject.ID = ADOSubstitute1.ID;
            substitute_testObject.TheFromDate = Utility.ToPersianDate(DateTime.Now);
            substitute_testObject.TheToDate = Utility.ToPersianDate(DateTime.Now.AddDays(2));
            substitute_testObject.ManagerPersonId = ADOManager1.Person.ID;
            substitute_testObject.Person = ADOPerson5;
            substitute_testObject.Active = true;
            busSubstitute.SaveChanges(substitute_testObject, UIActionType.EDIT);
            ClearSession();
            substitute_testObject = busSubstitute.GetByID(ADOSubstitute1.ID);
            Assert.AreEqual(ADOPerson5.ID, substitute_testObject.Person.ID);
        }

        [Test]
        public void Update_Empty_Test1()
        {
            try
            {
                busSubstitute.SaveChanges(substitute_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteIsNotSpecified));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteDateRequired));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteManagerRequiered));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstitutePersonRequiered));
            }
        }

        [Test]
        public void Update_DateValidate_Test1()
        {
            try
            {
                substitute_testObject.TheFromDate = Utility.ToPersianDate(DateTime.Now);
                substitute_testObject.TheToDate = Utility.ToPersianDate(DateTime.Now.AddDays(-1));
                busSubstitute.SaveChanges(substitute_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.SubstituteFromDateGreaterThanToDate));
            }
        }

        [Test]
        public void Delete_Test()
        {
            try 
            {
                busSubstitute.SaveChanges(ADOSubstitute1, UIActionType.DELETE);
                ClearSession();
                substitute_testObject = busSubstitute.GetByID(ADOSubstitute1.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void ManagerCount_UserAsOperator_Test() 
        {
            
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager1.ID, false);
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager2.ID, false);
            busSubstitute = new BSubstitute();
            int count = busSubstitute.GetAllManagerCount();
            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetAllManager_UserAsOperator_Test()
        {
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager1.ID, false);
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager2.ID, false);
            IList<Person> list = busSubstitute.GetAllManager(0, 10);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(ADOPerson2.ID, list.First().ID);
        }

        [Test]
        public void GetAllManager_UserAsOperator_ExtraPage_Test()
        {
            try
            {
                base.UpdateCurrentUserPersonId(ADOPerson1.ID);
                dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager1.ID, false);
                dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager2.ID, false);

                busSubstitute = new BSubstitute(ADOPerson1.ID);
                IList<Person> list = busSubstitute.GetAllManager(2, 10);
                Assert.Fail();
            }
            catch (OutOfExpectedRangeException ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetAllManager_UserAsOperatorAndManagerCount_Test() 
        {
            base.UpdateCurrentUserPersonId(ADOPerson2.ID);  
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager2.ID, false);
            busSubstitute = new BSubstitute(ADOPerson2.ID);
            int count = busSubstitute.GetAllManagerCount();
            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetAllManager_UserAsOperatorAndManager_Test()
        {
            base.UpdateCurrentUserPersonId(ADOPerson2.ID);
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager1.ID, false);

            busSubstitute = new BSubstitute(ADOPerson2.ID);
            IList<Person> list = busSubstitute.GetAllManager(0, 10);
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() == 1);
        }

        [Test]
        public void GetAllByManager_Test() 
        {
            IList<Substitute> list = busSubstitute.GetAllByManager(ADOManager1.Person.ID, "");
            Assert.IsTrue(list.Where(x => x.ID == ADOSubstitute1.ID).Count() > 0);
        }

        [Test]
        public void GetFlows_Test() 
        {
            IList<Flow> list = busSubstitute.GetAllFlowByManager(ADOSubstitute1.ID, ADOManager1.Person.ID);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void UpdateAccessGroup_Test1() 
        {
            List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

            AccessGroupProxy proxy = new AccessGroupProxy();
            proxy.ID = ADOPrecardHourlyEstelji1.ID;
            proxy.Checked = true;
            proxyList.Add(proxy);

            bool contains = busSubstitute.UpdateByProxy(ADOSubstitute1.ID, ADOFlow1.ID, proxyList);

            ClearSession();
            substitute_testObject = busSubstitute.GetByID(ADOSubstitute1.ID);
            Assert.IsTrue(substitute_testObject.PrecardList != null);
            Assert.AreEqual(1, substitute_testObject.PrecardList.Count);
            Assert.IsTrue(substitute_testObject.PrecardList.Where(x => x.ID == ADOPrecardHourlyEstelji1.ID).Count() == 1);
        }

        [Test]
        public void UpdateAccessGroup_Test2()
        {
            List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

            AccessGroupProxy proxy = new AccessGroupProxy();
            proxy.ID = ADOPrecardHourlyEstelji1.ID;
            proxy.Checked = true;
            proxyList.Add(proxy);

            proxy = new AccessGroupProxy();
            proxy.ID = ADOPrecardHourlyDuty1.ID;
            proxy.Checked = true;
            proxyList.Add(proxy);

            proxy = new AccessGroupProxy();
            proxy.ID = ADOPrecardHourlyLeave2.ID;
            proxy.Checked = false;
            proxyList.Add(proxy);

            bool contains = busSubstitute.UpdateByProxy(ADOSubstitute1.ID, ADOFlow1.ID, proxyList);

            ClearSession();
            substitute_testObject = busSubstitute.GetByID(ADOSubstitute1.ID);
            Assert.IsTrue(substitute_testObject.PrecardList != null);
            Assert.AreEqual(2, substitute_testObject.PrecardList.Count);
            Assert.IsTrue(substitute_testObject.PrecardList.Where(x => x.ID == ADOPrecardHourlyEstelji1.ID).Count() == 1);
            Assert.IsTrue(substitute_testObject.PrecardList.Where(x => x.ID == ADOPrecardHourlyDuty1.ID).Count() == 1);
            Assert.IsTrue(substitute_testObject.PrecardList.Where(x => x.ID == ADOPrecardHourlyLeave2.ID).Count() == 0);
            Assert.IsTrue(contains);
        }

        [Test]
        public void DeleteFlow_Test2()
        {
            bool contains = busSubstitute.UpdateByProxy(ADOSubstitute1.ID, ADOFlow1.ID, null);

            ClearSession();
            substitute_testObject = busSubstitute.GetByID(ADOSubstitute1.ID);
            Assert.IsTrue(substitute_testObject.PrecardList == null || substitute_testObject.PrecardList.Count == 0);
            Assert.AreEqual(0, substitute_testObject.PrecardList.Count);
            Assert.IsFalse(contains);
        }

        [Test]
        public void Test222222() 
        {
            busSubstitute = new BSubstitute(32688);
            IList<Person> list= busSubstitute.GetAllManager(0,10);
        }

    }
}
