using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Infrastructure.Utility;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class AssignRuleBusinessTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter assingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter groupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter();

        BAssignRule bussAssign;
        RuleCategory ADORuleCategory = new RuleCategory();
        PersonRuleCatAssignment ADOAssign1 = new PersonRuleCatAssignment();
        PersonRuleCatAssignment ADOAssign2 = new PersonRuleCatAssignment();
        PersonRuleCatAssignment ADOAssign3 = new PersonRuleCatAssignment();
        PersonRuleCatAssignment assign_testObject;

        [SetUp]
        public void TestSetup()
        {
            assign_testObject = new PersonRuleCatAssignment();
            bussAssign = new BAssignRule(SysLanguageResource.Parsi);

            groupTA.Insert("RuleGroup", "", false, "00-00test1");
            DatabaseGateway.TA_RuleCategoryDataTable groupTable = new DatabaseGateway.TA_RuleCategoryDataTable();
            groupTable=groupTA.GetDataByName("RuleGroup");

            ADORuleCategory.ID = Convert.ToDecimal(groupTable.Rows[0]["RuleCat_ID"]);
            ADORuleCategory.Name = Convert.ToString(groupTable.Rows[0]["RuleCat_Name"]);

            //assingTA.Insert(ADOPerson1.ID, ADORuleCategory.ID, new DateTime(2010, 2, 14).ToShortDateString(), new DateTime(2012, 9, 14).ToShortDateString(), null);
            //assingTA.Insert(ADOPerson1.ID, ADORuleCategory.ID, new DateTime(2005, 5, 14).ToShortDateString(), new DateTime(2007, 11, 5).ToShortDateString(), null);
            //assingTA.Insert(ADOPerson1.ID, ADORuleCategory.ID, new DateTime(2008, 3, 1).ToShortDateString(), new DateTime(2012, 9, 14).ToShortDateString(), null);

            assingTA.Insert(ADOPerson1.ID, ADORuleCategory.ID,Utility.ToString( new DateTime(2005, 2, 14)),Utility.ToString( new DateTime(2007, 9, 14)), null);
            assingTA.Insert(ADOPerson1.ID, ADORuleCategory.ID, Utility.ToString(new DateTime(2008, 5, 14)), Utility.ToString(new DateTime(2010, 11, 5)), null);
            assingTA.Insert(ADOPerson1.ID, ADORuleCategory.ID, Utility.ToString(new DateTime(2010, 11, 6)), Utility.ToString(new DateTime(2012, 9, 14)), null);


            assingTA.Insert(ADOPerson2.ID, ADORuleCategory.ID,Utility.ToString( new DateTime(2007, 1, 1)),Utility.ToString( new DateTime(2008, 1, 1)), null);
            assingTA.Insert(ADOPerson2.ID, ADORuleCategory.ID, Utility.ToString(new DateTime(2008, 1, 2)), Utility.ToString(new DateTime(2009, 1, 1)), null);

            DatabaseGateway.TA_PersonRuleCategoryAssignmentDataTable table = new DatabaseGateway.TA_PersonRuleCategoryAssignmentDataTable();
            assingTA.FillByFilter(table, ADOPerson1.ID, ADORuleCategory.ID);

            ADOAssign1.ID = (decimal)table.Rows[0]["PrsRulCatAsg_ID"];
            ADOAssign1.FromDate = (string)table.Rows[0]["PrsRulCatAsg_FromDate"];
            ADOAssign1.ToDate = (string)table.Rows[0]["PrsRulCatAsg_ToDate"];

            table = new DatabaseGateway.TA_PersonRuleCategoryAssignmentDataTable();
            assingTA.FillByFilter(table, ADOPerson2.ID, ADORuleCategory.ID);

            ADOAssign2.ID = (decimal)table.Rows[0]["PrsRulCatAsg_ID"];
            ADOAssign2.FromDate = (string)table.Rows[0]["PrsRulCatAsg_FromDate"];
            ADOAssign2.ToDate = (string)table.Rows[0]["PrsRulCatAsg_ToDate"];

            ADOAssign3.ID = (decimal)table.Rows[1]["PrsRulCatAsg_ID"];
            ADOAssign3.FromDate = (string)table.Rows[1]["PrsRulCatAsg_FromDate"];
            ADOAssign3.ToDate = (string)table.Rows[1]["PrsRulCatAsg_ToDate"];

        }

        [TearDown]
        public void TreatDown()
        {
            assingTA.DeleteByRuleCategory("00-00test1");
            groupTA.DeleteByCustomCode("00-00test1");
        }

        [Test]
        public void Update_ValidatePersonTest()
        {
            try
            {
                assign_testObject.ID = ADOAssign1.ID;
                assign_testObject.FromDate = DateTime.Now.Date.ToString();
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRulePersonIdNotExsits));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleIdNotExsits));
            }
        }

        [Test]
        public void Update_ValidateDateTest()
        {
            try
            {
                assign_testObject.ID = ADOAssign1.ID;
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(assign_testObject.ToDate, Utility.ToString(Utility.GTSMinStandardDateTime));
            }
        }

        [Test]
        public void Update_ValidateSmallDateTest()
        {
            try
            {
                assign_testObject.ID = ADOAssign1.ID;
                assign_testObject.UIFromDate = Utility.ToPersianDate(new DateTime(2000, 1, 1));
                assign_testObject.UIToDate = Utility.ToPersianDate(new DateTime(1800, 1, 1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleDateSmallerThanStandardValue));
            }
        }

        [Test]
        public void Update_Test() 
        {
            assign_testObject.ID = ADOAssign1.ID;
            assign_testObject.UIFromDate = "1392/01/01";
            assign_testObject.UIToDate = "1393/01/01";
            assign_testObject.Person = ADOPerson1;
            assign_testObject.RuleCategory = ADORuleCategory;
            bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);

            ClearSession();
            assign_testObject = new PersonRuleCatAssignment();
            assign_testObject = bussAssign.GetByID(ADOAssign1.ID);
            Assert.AreEqual(assign_testObject.FromDate, Utility.ToString(Utility.ToMildiDate("1392/01/01")));
        }

        [Test]
        public void Update_ValidateFromGreaterThanToTest()
        {
            try
            {
                assign_testObject.ID = ADOAssign1.ID;
                assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
                assign_testObject.UIToDate = Utility.ToPersianDate(DateTime.Now.AddYears(-1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleFromDateGreaterThanToDate));
            }
        }

        [Test]
        public void Update_DateConfilict_Test()
        {
            try
            {
                assign_testObject.ID = ADOAssign3.ID;
                assign_testObject.UIFromDate = Utility.ToPersianDate(new DateTime(2007, 1, 1).Date); 
                assign_testObject.UIToDate = Utility.ToPersianDate(new DateTime(2008, 5, 1).Date);
                assign_testObject.Person = ADOPerson2;
                assign_testObject.RuleCategory = ADORuleCategory;
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleDateHasConfilict));
            }
        }

        [Test]
        public void Insert_ValidatePersonTest()
        {
            try
            {
                assign_testObject.FromDate = DateTime.Now.Date.ToString();
                assign_testObject.ToDate= DateTime.Now.AddYears(1).Date.ToString();
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRulePersonIdNotExsits));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleIdNotExsits));
            }
        }

        [Test]
        public void Insert_ValidateDateTest()
        {
            try
            {
                assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(assign_testObject.ToDate, Utility.ToString(Utility.GTSMinStandardDateTime));
            }
        }

        [Test]
        public void Insert_ValidateSmallDateTest()
        {
            try
            {
                bussAssign = new BAssignRule(SysLanguageResource.English);
                assign_testObject.UIFromDate = Utility.ToString(DateTime.Now);
                assign_testObject.UIToDate = Utility.ToString(new DateTime(1800, 1, 1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleDateSmallerThanStandardValue));
            }
        }

        [Test]
        public void Insert_ValidateFromGreaterThanToTest()
        {
            try
            {
                assign_testObject.UIFromDate = Utility.ToPersianDate(Utility.ToString(DateTime.Now));
                assign_testObject.UIToDate = Utility.ToPersianDate(Utility.ToString(DateTime.Now.AddYears(-1)));
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleFromDateGreaterThanToDate));
            }
        }

        [Test]
        public void Insert_Test()
        {
            assign_testObject.FromDate = DateTime.Now.Date.ToShortDateString();
            assign_testObject.ToDate = DateTime.Now.AddYears(1).Date.ToShortDateString();
            assign_testObject.Person = ADOPerson1;
            assign_testObject.RuleCategory = ADORuleCategory;
            bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bussAssign.GetByID(assign_testObject.ID);
            Assert.IsTrue(assign_testObject.ID > 0);
        }

        [Test]
        public void Insert_UIFromDateTest()
        {
            assign_testObject.UIFromDate = "1392/2/5";
            assign_testObject.UIToDate = "1393/2/5"; ;
            assign_testObject.Person = ADOPerson1;
            assign_testObject.RuleCategory = ADORuleCategory;
            bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bussAssign.GetByID(assign_testObject.ID);

            Assert.AreEqual(assign_testObject.FromDate,Utility.ToString( Utility.ToMildiDate("1392/02/05")));
            Assert.AreEqual(assign_testObject.ToDate, Utility.ToString(Utility.ToMildiDate("1393/02/05")));
        }

        [Test]
        public void Insert_UIFromDateEnglishTest()
        {
            BAssignRule bassign = new BAssignRule(SysLanguageResource.English);
            assign_testObject.UIFromDate = "2013/4/05";
            assign_testObject.UIToDate = "2014/8/05";
            assign_testObject.Person = ADOPerson1;
            assign_testObject.RuleCategory = ADORuleCategory;
            bassign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bassign.GetByID(assign_testObject.ID);

            Assert.AreEqual(assign_testObject.FromDate,Utility.ToString( new DateTime(2013, 4, 5)));
            Assert.AreEqual(assign_testObject.ToDate, Utility.ToString(new DateTime(2014, 8, 5)));
        }

        [Test]
        public void Insert_UIFromToEqualDateEnglishTest()
        {
            try
            {
                BAssignRule bassign = new BAssignRule(SysLanguageResource.English);
                assign_testObject.UIFromDate = "2010/4/05";
                assign_testObject.UIToDate = "2010/4/05";
                assign_testObject.Person = ADOPerson1;
                assign_testObject.RuleCategory = ADORuleCategory;
                bassign.SaveChanges(assign_testObject, UIActionType.ADD);
                Assert.Fail("نباید مساوی باشند");
            }
            catch (UIValidationExceptions ex) 
            {
                ex.Exists(ExceptionResourceKeys.AssignRuleFromDateGreaterThanToDate);
            }            
        }

        [Test]
        public void Insert_DateConfilict_Test() 
        {
            try
            {
                assign_testObject.UIFromDate = Utility.ToPersianDate(new DateTime(2008, 5, 1).Date);
                assign_testObject.UIToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1).Date);
                assign_testObject.Person = ADOPerson2;
                assign_testObject.RuleCategory = ADORuleCategory;
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRuleDateHasConfilict));
            }
        }


        [Test]
        public void Delete_Test()
        {
            try
            {
                bussAssign.SaveChanges(ADOAssign1, UIActionType.DELETE);
                assign_testObject = bussAssign.GetByID(ADOAssign1.ID);
                Assert.IsTrue(ADOAssign1.ID == 0);
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }
    }
}
