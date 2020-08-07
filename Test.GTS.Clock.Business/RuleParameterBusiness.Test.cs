using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business;
using GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RuleParameterBusiness : BaseFixture
    {
        TA_AssignRuleParameterTableAdapter assignRuleTA = new TA_AssignRuleParameterTableAdapter();
        TA_RuleTableAdapter ruleTA = new TA_RuleTableAdapter();
        TA_RuleCategoryTableAdapter catTA = new TA_RuleCategoryTableAdapter();
        TA_RuleParameterTableAdapter parameterTA = new TA_RuleParameterTableAdapter();

        BRuleParameter businessAssignRule;

        RuleCategory ADORuleCat = new RuleCategory();
        Rule ADORule = new Rule();
        AssignRuleParameter ADOAssignRuleParam1 = new AssignRuleParameter();
        AssignRuleParameter ADOAssignRuleParam2 = new AssignRuleParameter();
        AssignRuleParameter assignRuleParameter_testObject;

        RuleParameter ADOParameter1 = new RuleParameter();
        RuleParameter ADOParameter2 = new RuleParameter();
        RuleParameter ruleParameter_testObject;

        [SetUp]
        public void TestSetup()
        {
            try
            {
                assignRuleParameter_testObject = new AssignRuleParameter();
                int ruleTypeID = Convert.ToInt32(new TA_RuleTypeTableAdapter().GetData().Rows[0]["RuleType_ID"]);
                catTA.Insert("TestCategory", "", false, "00-00test1");
                DatabaseGateway.TA_RuleCategoryDataTable cat = new DatabaseGateway.TA_RuleCategoryDataTable();
                cat = catTA.GetDataByName("TestCategory");
                ADORuleCat.ID = Convert.ToInt32(cat.Rows[0]["ruleCat_ID"]);
                ADORuleCat.Name = Convert.ToString(cat.Rows[0]["ruleCat_Name"]);

                ruleTA.Insert(1033, "a", "a", "a", 67, false, ADORuleCat.ID, ruleTypeID, 0);
                DatabaseGateway.TA_RuleDataTable ruleTable = new DatabaseGateway.TA_RuleDataTable();
                ruleTable = ruleTA.GetByIdentifierCode(1033);
                ADORule.ID = Convert.ToInt32(ruleTable.Rows[0]["rule_ID"]);
                ADORule.IdentifierCode = Convert.ToInt32(ruleTable.Rows[0]["rule_IdentifierCode"]);
                ADORule.TemplateId = Convert.ToInt32(ruleTable.Rows[0]["rule_RuleTmpId"]);
                ADORule.CategoryId = Convert.ToInt32(ruleTable.Rows[0]["rule_RuleCategoryId"]);

                assignRuleTA.Insert(new DateTime(2000, 1, 1), new DateTime(2001, 1, 1), ADORule.ID);
                assignRuleTA.Insert(new DateTime(2002, 1, 1), new DateTime(2003, 1, 1), ADORule.ID);
                DatabaseGateway.TA_AssignRuleParameterDataTable assignTable = new DatabaseGateway.TA_AssignRuleParameterDataTable();
                assignTable = assignRuleTA.GetDataByRuleID(ADORule.ID);

                ADOAssignRuleParam1.ID = Convert.ToInt32(assignTable.Rows[0]["AsgRuleParam_ID"]);
                ADOAssignRuleParam1.FromDate = Convert.ToDateTime(assignTable.Rows[0]["AsgRuleParam_FromDate"]);
                ADOAssignRuleParam1.ToDate = Convert.ToDateTime(assignTable.Rows[0]["AsgRuleParam_ToDate"]);

                ADOAssignRuleParam2.ID = Convert.ToInt32(assignTable.Rows[1]["AsgRuleParam_ID"]);
                ADOAssignRuleParam2.FromDate = Convert.ToDateTime(assignTable.Rows[1]["AsgRuleParam_FromDate"]);
                ADOAssignRuleParam2.ToDate = Convert.ToDateTime(assignTable.Rows[1]["AsgRuleParam_ToDate"]);

                parameterTA.Insert(ADOAssignRuleParam1.ID, "Param1", "0", 0, "");
                parameterTA.Insert(ADOAssignRuleParam1.ID, "Param2", "0", 0, "");

                DatabaseGateway.TA_RuleParameterDataTable paramTable = new DatabaseGateway.TA_RuleParameterDataTable();
                parameterTA.FillByAssignID(paramTable, ADOAssignRuleParam1.ID);

                ADOParameter1.ID = Convert.ToInt32(paramTable.Rows[0]["RuleParam_ID"]);
                ADOParameter1.Value = Convert.ToString(paramTable.Rows[0]["RuleParam_Value"]);
                ADOParameter1.Name = Convert.ToString(paramTable.Rows[0]["RuleParam_Name"]);
                ADOParameter1.Title = Convert.ToString(paramTable.Rows[0]["RuleParam_Title"]);
                ADOParameter1.Type = (RuleParamType)Convert.ToInt32(paramTable.Rows[0]["RuleParam_Type"]);

                ADOParameter2.ID = Convert.ToInt32(paramTable.Rows[1]["RuleParam_ID"]);
                ADOParameter2.Value = Convert.ToString(paramTable.Rows[1]["RuleParam_Value"]);
                ADOParameter2.Name = Convert.ToString(paramTable.Rows[1]["RuleParam_Name"]);
                ADOParameter2.Title = Convert.ToString(paramTable.Rows[1]["RuleParam_Title"]);
                ADOParameter2.Type = (RuleParamType)Convert.ToInt32(paramTable.Rows[1]["RuleParam_Type"]);

                businessAssignRule = new BRuleParameter(ADORule.TemplateId, ADORule.CategoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TearDown]
        new public void TreatDown()
        {
            parameterTA.DeleteByRuleIdentifier(ADORule.IdentifierCode);
            assignRuleTA.DeleteByRuleIdentifier(ADORule.IdentifierCode);
            assignRuleTA.DeleteByRuleID(ADORule.ID);
            ruleTA.DeleteByIdentifierCode(ADORule.IdentifierCode);
            
            catTA.DeleteByCustomCode("00-00test1");
        }

        [Test]
        public void InvalidRuleIDTest()
        {
            try
            {
                businessAssignRule = new BRuleParameter(ADORule.IdentifierCode, ADORule.CategoryId * 2 + 1);
                Assert.Fail("شناسه قانون نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterRuleIDInvalid);
            }
        }

        [Test]
        public void Insert_HasIntersectValidation()
        {
            try
            {
                assignRuleParameter_testObject.FromDate = ADOAssignRuleParam1.FromDate.AddDays(1);
                assignRuleParameter_testObject.ToDate = ADOAssignRuleParam1.ToDate;
                businessAssignRule.SaveChanges(assignRuleParameter_testObject, UIActionType.ADD);
                Assert.Fail("بازها با اشتراک نباید ذخیره شوند");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterDateHasIntersect);
            }
        }

        [Test]
        public void Insert_InvalidDateValidation()
        {
            try
            {
                assignRuleParameter_testObject.FromDate = new DateTime(1, 1, 1);
                assignRuleParameter_testObject.ToDate = ADOAssignRuleParam1.ToDate;
                businessAssignRule.SaveChanges(assignRuleParameter_testObject, UIActionType.ADD);
                Assert.Fail("تاریخ نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterDateIsInvalid);
            }
        }

        [Test]
        public void Insert_FromGreaterThanToValidation()
        {
            try
            {
                assignRuleParameter_testObject.FromDate = ADOAssignRuleParam1.ToDate;
                assignRuleParameter_testObject.ToDate = ADOAssignRuleParam1.FromDate;
                businessAssignRule.SaveChanges(assignRuleParameter_testObject, UIActionType.ADD);
                Assert.Fail("تاریخ شروع بزرگتر از پایان");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterFromDateGreaterThanToDate);
            }
        }

        [Test]
        public void Insert_AddParameterTest()
        {
            IList<RuleTemplateParameter> pList = businessAssignRule.GetTemplateParameters();
            pList.RemoveAt(0);
            pList[0].Value = "test";
            pList[1].Value = "test2";

            decimal assgnId = businessAssignRule.InsertParameter(pList, DateTime.Now, DateTime.Now.AddYears(1));

            ClearSession();
            assignRuleParameter_testObject = businessAssignRule.GetByID(assgnId);
            Assert.IsTrue(assignRuleParameter_testObject.RuleParameterList.Count == businessAssignRule.GetTemplateParameters().Count);
        }

        [Test]
        public void Insert_ParameterCheckTest()
        {
            IList<RuleTemplateParameter> pList = businessAssignRule.GetTemplateParameters();
            pList.RemoveAt(0);
            pList[0].Value = "test";
            pList[1].Value = "test2";

            decimal assgnId = businessAssignRule.InsertParameter(pList, DateTime.Now, DateTime.Now.AddYears(1));

            ClearSession();
            assignRuleParameter_testObject = businessAssignRule.GetByID(assgnId);
            Assert.AreEqual(pList[0].Value, assignRuleParameter_testObject.RuleParameterList.Where(x => x.Name == pList[0].Name).First().Value);
        }

        [Test]
        public void Update_HasIntersectValidation()
        {
            try
            {
                assignRuleParameter_testObject.ID = ADOAssignRuleParam1.ID;
                assignRuleParameter_testObject.FromDate = ADOAssignRuleParam1.FromDate;
                assignRuleParameter_testObject.ToDate = ADOAssignRuleParam2.ToDate;
                businessAssignRule.SaveChanges(assignRuleParameter_testObject, UIActionType.EDIT);
                Assert.Fail("بازها با اشتراک نباید ذخیره شوند");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterDateHasIntersect);
            }
        }

        [Test]
        public void Update_InvalidDateValidation()
        {
            try
            {
                assignRuleParameter_testObject.ID = ADOAssignRuleParam1.ID;
                assignRuleParameter_testObject.FromDate = new DateTime(1, 1, 1);
                assignRuleParameter_testObject.ToDate = ADOAssignRuleParam1.ToDate;
                businessAssignRule.SaveChanges(assignRuleParameter_testObject, UIActionType.EDIT);
                Assert.Fail("تاریخ نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterDateIsInvalid);
            }
        }

        [Test]
        public void Update_FromGreaterThanToValidation()
        {
            try
            {
                assignRuleParameter_testObject.ID = ADOAssignRuleParam1.ID;
                assignRuleParameter_testObject.FromDate = ADOAssignRuleParam1.ToDate;
                assignRuleParameter_testObject.ToDate = ADOAssignRuleParam1.FromDate;
                businessAssignRule.SaveChanges(assignRuleParameter_testObject, UIActionType.EDIT);
                Assert.Fail("تاریخ شروع بزرگتر از پایان");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.AssignParameterFromDateGreaterThanToDate);
            }
        }

        [Test]
        public void Update_EditParametersTest()
        {
            IList<RuleParameter> pList = businessAssignRule.GetRuleParameters(ADOAssignRuleParam1.ID);
            pList.RemoveAt(0);
            pList[0].Value = "test";
            ClearSession();
            decimal assgnId = businessAssignRule.UpdateParameter(pList, ADOAssignRuleParam1.ID, ADOAssignRuleParam1.FromDate.AddYears(-1), ADOAssignRuleParam1.ToDate);

            ClearSession();
            assignRuleParameter_testObject = businessAssignRule.GetByID(assgnId);
            Assert.IsTrue(assignRuleParameter_testObject.RuleParameterList.Count == businessAssignRule.GetRuleParameters(ADOAssignRuleParam1.ID).Count);
        }

        [Test]
        public void Update_CheckParametersTest()
        {
            IList<RuleParameter> pList = businessAssignRule.GetRuleParameters(ADOAssignRuleParam1.ID);
            pList.RemoveAt(0);
            pList[0].Value = "test";
            ClearSession();
            decimal assgnId = businessAssignRule.UpdateParameter(pList, ADOAssignRuleParam1.ID, ADOAssignRuleParam1.FromDate.AddYears(-1), ADOAssignRuleParam1.ToDate);

            ClearSession();
            assignRuleParameter_testObject = businessAssignRule.GetByID(assgnId);
            Assert.AreEqual(pList[0].Value, assignRuleParameter_testObject.RuleParameterList.Where(x => x.Name == pList[0].Name).First().Value);
        }

        [Test]
        public void Delete_Test1()
        {
            try
            {
                ClearSession();
                businessAssignRule.DeleteParameter(ADOAssignRuleParam1.ID);
                ClearSession();
                assignRuleParameter_testObject = businessAssignRule.GetByID(ADOAssignRuleParam1.ID);
                Assert.Fail("سرویس حذف کار نمیکند ");
            }
            catch (ItemNotExists ex)
            {
               
            }
        }

        [Test]
        public void Delete_Test2()
        {
            try
            {
                ClearSession();
                businessAssignRule.SaveChanges(ADOAssignRuleParam1, UIActionType.DELETE);
                ClearSession();
                assignRuleParameter_testObject = businessAssignRule.GetByID(ADOAssignRuleParam1.ID);
                Assert.Fail("سرویس حذف کار نمیکند ");
            }
            catch (ItemNotExists ex)
            {
                
            }
        }

    }
}

