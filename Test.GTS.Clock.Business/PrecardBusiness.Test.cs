using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class PrecardBusinesTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter groupPrecardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_BaseTrafficTableAdapter basicTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_BaseTrafficTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_SubstitutePrecardAccessTableAdapter subTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_SubstitutePrecardAccessTableAdapter();
        #endregion

        #region ADOObjects
        BPrecard bussPrecard = new BPrecard();
        Precard precard_testObject = new Precard();
        Precard ADOPrecard1 = new Precard();
        Precard ADOPrecard2 = new Precard();
        PrecardGroups ADOGroup = new PrecardGroups();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            precard_testObject = new Precard();
            bussPrecard = new BPrecard();

            groupPrecardTA.Insert("TestPrecardGroup", "TestGroup1");
            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable groupTable;
            groupTable = groupPrecardTA.GetDataByName("TestGroup1");
            ADOGroup.ID = Convert.ToDecimal(groupTable.Rows[0][0]);

            precardTA.Insert("TestPish1", true, ADOGroup.ID, true, false, false, "1001", false);
            precardTA.Insert("TestPish2", true, ADOGroup.ID, false, true, false, "1002", false);
            
            DatasetGatewayWorkFlow.TA_PrecardDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            precardTable = precardTA.GetDataByName("TestPish1");
            ADOPrecard1.ID = Convert.ToDecimal(precardTable.Rows[0][0]);
            ADOPrecard1.Name = "TestPish1";
            ADOPrecard1.Active = true;
            ADOPrecard1.IsHourly = true;
            ADOPrecard1.Code = "1001";
            ADOPrecard1.PrecardGroup = ADOGroup;
            precardTable = precardTA.GetDataByName("TestPish2");
            ADOPrecard2.ID = Convert.ToDecimal(precardTable.Rows[0][0]);
            ADOPrecard2.Name = "TestPish2";
            ADOPrecard2.Active = true;
            ADOPrecard2.IsDaily = true;
            ADOPrecard2.Code = "1002";
            ADOPrecard2.PrecardGroup = ADOGroup;
            
            basicTA.Insert(ADOPrecard1.ID, ADOPerson1.ID, DateTime.Now, 0, false, true, false, false);

            subTA.Insert(ADOPrecard1.ID, null);
        }

        [TearDown]
        public void TreatDown()
        {
            basicTA.DeleteByPrecard(ADOPrecard1.ID);
            subTA.DeleteByPrecard(ADOPrecard1.ID);
            precardTA.DeleteByID("1001");
            precardTA.DeleteByID("1002");
            precardTA.DeleteByID("1003");
            groupPrecardTA.DeleteByName("TestPrecardGroup");

        }

        [Test]
        public void GetByID_Test()
        {
             precard_testObject= bussPrecard.GetByID(ADOPrecard1.ID);
             Assert.AreEqual(precard_testObject.Name, ADOPrecard1.Name);
        }

        [Test]
        public void GetAll_Test()
        {
            dataAccessPrecardTA.Insert(BUser.CurrentUser.ID, ADOPrecard1.ID, false);
            IList<Precard> list = bussPrecard.GetAll();
            Assert.IsTrue(list.Where(x => x.ID == ADOPrecard1.ID).Count() > 0);
        }

        [Test]
        public void Insert_RepeateValidateTest() 
        {
            try
            {
                precard_testObject.Code = ADOPrecard2.Code;
                precard_testObject.Name = ADOPrecard2.Name;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("پیشکارت تکراری");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardCodeRepeated));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardNameRepeated));
            }
        }

        [Test]
        public void Insert_EmptyValidateTest()
        {
            try
            {
                precard_testObject.ID = 0;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("پیشکارت خالی");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardCodeRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardNameRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardGroupRequierd));
            }
        }

        [Test]
        public void Insert_StatusValidateTest1()
        {
            try
            {
                precard_testObject.IsDaily = false;
                precard_testObject.IsHourly = false;
                precard_testObject.IsPermit = false;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("وضعیت نامعتبر");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardInvalidStatus));
            }
        }

        [Test]
        public void Insert_StatusValidateTest2()
        {
            try
            {
                precard_testObject.IsDaily = true;
                precard_testObject.IsHourly = true;
                precard_testObject.IsPermit = false;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("وضعیت نامعتبر");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardInvalidStatus));
            }
        }

        [Test]
        public void Insert_StatusValidateTest3()
        {
            try
            {
                precard_testObject.IsDaily = true;
                precard_testObject.IsHourly = true;
                precard_testObject.IsPermit = true;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("وضعیت نامعتبر");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardInvalidStatus));
            }
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                precard_testObject.Code = "1003";
                precard_testObject.Name = "TestPrecard3";
                precard_testObject.IsDaily = true;
                precard_testObject.PrecardGroup = new PrecardGroups() { ID = ADOGroup.ID };
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                ClearSession();
                precard_testObject = bussPrecard.GetByID(precard_testObject.ID);
                Assert.AreEqual(precard_testObject.Name, "TestPrecard3");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_RepeateValidateTest()
        {
            try
            {
                precard_testObject.ID = ADOPrecard1.ID;
                precard_testObject.Code = ADOPrecard2.Code;
                precard_testObject.Name = ADOPrecard2.Name;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.EDIT);
                Assert.Fail("پیشکارت تکراری");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardNameRepeated));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardCodeRepeated));
            }
        }

        [Test]
        public void Update_EmptyValidateTest()
        {
            try
            {
                precard_testObject.ID = 0;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("پیشکارت خالی");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardCodeRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardNameRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardGroupRequierd));
            }
        }

        [Test]
        public void Update_StatusValidateTest1()
        {
            try
            {
                precard_testObject.IsDaily = false;
                precard_testObject.IsHourly = false;
                precard_testObject.IsPermit = false;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("وضعیت نامعتبر");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardInvalidStatus));
            }
        }

        [Test]
        public void Update_StatusValidateTest2()
        {
            try
            {
                precard_testObject.IsDaily = true;
                precard_testObject.IsHourly = true;
                precard_testObject.IsPermit = false;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("وضعیت نامعتبر");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardInvalidStatus));
            }
        }

        [Test]
        public void Update_StatusValidateTest3()
        {
            try
            {
                precard_testObject.IsDaily = true;
                precard_testObject.IsHourly = true;
                precard_testObject.IsPermit = true;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.ADD);
                Assert.Fail("وضعیت نامعتبر");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardInvalidStatus));
            }
        }

        [Test]
        public void Update_Test()
        {
            try
            {
                precard_testObject.ID = ADOPrecard1.ID;
                precard_testObject.Code = ADOPrecard1.Code;
                precard_testObject.Name = "TestPrecard3";
                precard_testObject.IsDaily = true;
                precard_testObject.PrecardGroup = new PrecardGroups() { ID = ADOGroup.ID };
                bussPrecard.SaveChanges(precard_testObject, UIActionType.EDIT);
                ClearSession();
                precard_testObject = new Precard();
                precard_testObject = bussPrecard.GetByID(ADOPrecard1.ID);
                Assert.AreEqual(precard_testObject.Name, "TestPrecard3");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Delete_UsedByBasicTrafficTest() 
        {
            try
            {
                precard_testObject.ID = ADOPrecard1.ID;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.DELETE);
                Assert.Fail("");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardUsedByBasicTraffic));
            }
        }

        [Test]
        public void Delete_UsedBySubstituteTest()
        {
            try
            {
                precard_testObject.ID = ADOPrecard1.ID;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.DELETE);
                Assert.Fail("");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrecardUsedBySubestitute));
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                precard_testObject.ID = ADOPrecard2.ID;
                bussPrecard.SaveChanges(precard_testObject, UIActionType.DELETE);
                ClearSession();
                precard_testObject = bussPrecard.GetByID(ADOPrecard2.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void GetAllGroup_Test() 
        {
            IList<PrecardGroups> list = bussPrecard.GetAllPrecardGroups();
            Assert.IsTrue(list.Where(x => x.ID == ADOGroup.ID).Count() > 0);
        }
    }
}
