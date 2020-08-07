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

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RuleViewerBusiness:BaseFixture
    {
        TA_AssignRuleParameterTableAdapter assignRuleTA = new TA_AssignRuleParameterTableAdapter();
        TA_RuleTableAdapter ruleTA = new TA_RuleTableAdapter();
        TA_RuleCategoryTableAdapter catTA = new TA_RuleCategoryTableAdapter();
        TA_RuleParameterTableAdapter parameterTA = new TA_RuleParameterTableAdapter();

        BRuleViewer businessRuleViwer;

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

            businessRuleViwer = new BRuleViewer(ADORuleCat.ID);
        }

        [TearDown]
        new public void TreatDown()
        {
            assignRuleTA.DeleteByRuleID(ADORule.ID);
            ruleTA.DeleteByIdentifierCode(ADORule.IdentifierCode);
            catTA.DeleteByCustomCode("00-00test1");
        }

        [Test]
        public void GetAllRuletypesTest() 
        {
            businessRuleViwer.GetAllRuleTypes();
            Assert.Pass();
        }

        [Test]
        public void GetAllRulesTest()
        {
            IList<RuleType> list= businessRuleViwer.GetAllRuleTypes();
            businessRuleViwer.GetAllRules(list[0].ID);
            Assert.Pass();
        }

        [Test]
        public void GetAllParameterRangesTest()
        {
            businessRuleViwer.GetAllRuleParametersRange(ADORule.ID);            
            Assert.Pass();
        }

        [Test]
        public void GetAllRuleParametersTest() 
        {
           IList<RuleParameter> param= businessRuleViwer.GetAllRuleParameters(ADOAssignRuleParam1.ID);

           Assert.AreEqual(param.Count, 2);
        }

        
    }
}
