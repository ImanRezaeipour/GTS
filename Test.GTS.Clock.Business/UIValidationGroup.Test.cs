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
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 4/4/2012 12:47:18 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class UIValidationGroupTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter validationGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationGroupingTableAdapter groupingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupingTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleTableAdapter ruleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleParameterTableAdapter parmTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleParameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleTempPatameterTableAdapter paramTmpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleTempPatameterTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();


        UIValidationRule ADORule1 = new UIValidationRule();
        UIValidationRule ADORule2 = new UIValidationRule();
        UIValidationGroup ADOGroup1 = new UIValidationGroup();
        UIValidationGroup ADOGroup2 = new UIValidationGroup();
        UIValidationGroup validationGroup_testObject;
        UIValidationGrouping ADOGrouping1 = new UIValidationGrouping();
        BUIValidationGroup busValidationGroup;



        [SetUp]
        public void TestSetup()
        {
            validationGroup_testObject = new UIValidationGroup();
            busValidationGroup = new BUIValidationGroup();

            ruleTA.InsertQuery(111000111,"TestRule1", "000-001", true);
            ruleTA.InsertQuery(111000112,"TestRule2", "000-002", true);

            DatabaseGateway.TA_UIValidationRuleDataTable ruleTable = ruleTA.GetDataByCode("000-001");
            ADORule1.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            ruleTable = ruleTA.GetDataByCode("000-002");
            ADORule2.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            paramTmpTA.Insert(0, "Rule TMP Param Test 1", ADORule1.ID);
            paramTmpTA.Insert(1, "Rule TMP Param Test 2", ADORule1.ID);
            paramTmpTA.Insert(2, "Rule TMP Param Test 3", ADORule1.ID);

            validationGroupTA.InsertQuery("TestGroup00");
            DatabaseGateway.TA_UIValidationGroupDataTable groupTable = validationGroupTA.GetDataByName("TestGroup00");
            ADOGroup1.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;

            validationGroupTA.InsertQuery("TestGroup01");
            groupTable = validationGroupTA.GetDataByName("TestGroup01");
            ADOGroup2.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;


            groupingTA.Insert(ADORule1.ID, ADOGroup1.ID, false,true);

            personTA.UpdateValidationGroup(ADOGroup1.ID, ADOPerson1.ID);


        }

        [TearDown]
        public void TreatDown()
        {
            ruleTA.DeleteByCode("000-001");
            ruleTA.DeleteByCode("000-002");
            validationGroupTA.DeleteByName("TestGroup00");
            validationGroupTA.DeleteByName("TestGroup01");
            validationGroupTA.DeleteByName("TestGroup03");


        }

        [Test]
        public void GetById_Test()
        {
            validationGroup_testObject = busValidationGroup.GetByID(ADOGroup1.ID);
            Assert.AreEqual("TestGroup00", validationGroup_testObject.Name);
        }

        [Test]
        public void GetRuleList_Insert_Test() 
        {
            DatabaseGateway.TA_UIValidationRuleDataTable ruleTable = ruleTA.GetActive();        
        
            DatabaseGateway.TA_UIValidationGroupingDataTable table = groupingTA.GetDataByGroupId(ADOGroup2.ID);
            IList<UIValidationGroupingProxy> ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            Assert.AreEqual(0, table.Rows.Count);
            table = groupingTA.GetDataByGroupId(ADOGroup2.ID);
            Assert.AreEqual(ruleTable.Rows.Count, table.Rows.Count);
            Assert.AreEqual(ruleTable.Rows.Count, ruleList.Count);

        }

        [Test]
        public void UpdateRuleList_Update_Test()
        {
            DatabaseGateway.TA_UIValidationRuleDataTable ruleTable = ruleTA.GetData();          
            IList<UIValidationGroupingProxy> ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            IList<UIValidationGroupingProxy> l = ruleList.Where(x => x.RuleID == ADORule1.ID).ToList(); 
            ClearSession();
            Assert.AreEqual(ruleTable.Rows.Count , ruleList.Count);
            l[0].Active = true;
            l[0].OpratorRestriction = true;
            busValidationGroup.UpdateRuleList(l);
            ClearSession();
            ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            l = ruleList.Where(x => x.RuleID == ADORule1.ID).ToList();
            Assert.AreEqual(ruleTable.Rows.Count, ruleList.Count);
            Assert.IsTrue(l[0].Active);
            Assert.IsTrue(l[0].OpratorRestriction);
        }

        [Test]
        public void GetRuleParameter_Insert_Test() 
        {
            DatabaseGateway.TA_UIValidationRuleDataTable ruleTable = ruleTA.GetData();            
        
            IList<UIValidationGroupingProxy> ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            IList<UIValidationGroupingProxy> l = ruleList.Where(x => x.RuleID == ADORule1.ID).ToList();
            DatabaseGateway.TA_UIValidationRuleParameterDataTable table = parmTA.GetDataByGroupingID(l[0].ID);
            Assert.AreEqual(0, table.Rows.Count);
            ClearSession();
            IList<UIValidationRuleParameter> paramList = busValidationGroup.GetRuleParameter(l[0].ID, l[0].RuleID);
            DatabaseGateway.TA_UIValidationRuleParameterDataTable table2 = parmTA.GetDataByGroupingID(l[0].ID);
            Assert.AreEqual(3, table2.Rows.Count);
            Assert.AreEqual(3, paramList.Count);
        }

        [Test]
        public void GetRuleParameter_CheckType_Test()
        {
            IList<UIValidationGroupingProxy> ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            ClearSession();
            IList<UIValidationGroupingProxy> l = ruleList.Where(x => x.RuleID == ADORule1.ID).ToList();
            IList<UIValidationRuleParameter> paramList = busValidationGroup.GetRuleParameter(l[0].ID, l[0].RuleID);
            Assert.AreEqual(3, paramList.Count);
            Assert.AreEqual(RuleParamType.Date, paramList[2].Type);
        }      

        [Test]
        public void Insert_Test() 
        {

            validationGroup_testObject.Name = "TestGroup03";
            validationGroup_testObject.CustomCode = "0-0";
            busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);
            Assert.IsTrue(validationGroup_testObject.ID > 0);
        }
   
        public void Insert_RulesGrouping_Test()
        {
            validationGroup_testObject.Name = "TestGroup01";
            validationGroup_testObject.GroupingList = new List<UIValidationGrouping>();

            UIValidationGrouping groupting = new UIValidationGrouping() { RuleID = ADORule1.ID };
            validationGroup_testObject.GroupingList.Add(groupting);

            groupting = new UIValidationGrouping() { RuleID = ADORule2.ID };
            validationGroup_testObject.GroupingList.Add(groupting);

            busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);

            ClearSession();

            UIValidationGroup group = busValidationGroup.GetByID(validationGroup_testObject.ID);
            Assert.IsNotNull(group.GroupingList);
            Assert.AreEqual(2, group.GroupingList.Count);

        }

        [Test]
        public void Insert_EmptyName()
        {
            try
            {
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupNameIsEmpty));
            }
        }

        [Test]
        public void Insert_RepeatName()
        {
            try
            {
                validationGroup_testObject.Name = "TestGroup00";
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupNameIsRepeated));
            }
        }

        
        public void Insert_GroupingNull() 
        {
            try
            {
                validationGroup_testObject.Name = "TestGroup01";
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupRulesIsEmpty));
            }

        }

     
        public void Insert_GroupingEmpty()
        {
            try
            {
                validationGroup_testObject.Name = "TestGroup01";
                validationGroup_testObject.GroupingList = new List<UIValidationGrouping>();
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupRulesIsEmpty));
            }
        }

        public void Insert_GroupingRuleEmpty()
        {
            try
            {
                validationGroup_testObject.Name = "TestGroup01";
                validationGroup_testObject.GroupingList = new List<UIValidationGrouping>();

                //UIValidationGrouping groupting = new UIValidationGrouping();
                //validationGroup_testObject.GroupingList.Add(groupting);
                //busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupRulesIsEmpty));
            }
        }
     
        public void Update_RulesGrouping_Test()
        {
            validationGroup_testObject = busValidationGroup.GetByID(ADOGroup1.ID);
            validationGroup_testObject.Name = "TestGroup01";
            validationGroup_testObject.GroupingList = new List<UIValidationGrouping>();

            UIValidationGrouping groupting = new UIValidationGrouping() { RuleID = ADORule2.ID };
            validationGroup_testObject.GroupingList.Add(groupting);

            busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);

            ClearSession();

            UIValidationGroup group = busValidationGroup.GetByID(validationGroup_testObject.ID);
            Assert.IsNotNull(group.GroupingList);
            Assert.AreEqual(1, group.GroupingList.Count);
            Assert.AreEqual(ADORule2.ID, group.GroupingList.First().RuleID);
        }

        [Test]
        public void Update_Test() 
        {
            try
            {
                validationGroup_testObject.ID = ADOGroup1.ID;
                validationGroup_testObject.Name = "TestGroup03";
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);
                ClearSession();
                validationGroup_testObject = new UIValidationGroup();
                validationGroup_testObject = busValidationGroup.GetByID(ADOGroup1.ID);
                Assert.AreEqual("TestGroup03", validationGroup_testObject.Name);
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_EmptyName()
        {
            try
            {
                validationGroup_testObject.ID = ADOGroup1.ID;
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupNameIsEmpty));
            }
        }


        [Test]
        public void Update_RepeatName()
        {
            try
            {
                validationGroup_testObject.ID = ADOGroup2.ID;
                validationGroup_testObject.Name = "TestGroup00";
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupNameIsRepeated));
            }
        }


        public void Update_GroupingNull()
        {
            try
            {
                validationGroup_testObject.ID = ADOGroup1.ID;
                validationGroup_testObject.Name = "TestGroup01";
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupRulesIsEmpty));
            }

        }

       
        public void Update_GroupingEmpty()
        {
            try
            {
                validationGroup_testObject.ID = ADOGroup1.ID;
                validationGroup_testObject.Name = "TestGroup01";
                validationGroup_testObject.GroupingList = new List<UIValidationGrouping>();
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupRulesIsEmpty));
            }
        }

      
        public void Update_GroupingRuleEmpty()
        {
            try
            {
                validationGroup_testObject.ID = ADOGroup1.ID;
                validationGroup_testObject.Name = "TestGroup01";
                validationGroup_testObject.GroupingList = new List<UIValidationGrouping>();

                UIValidationGrouping groupting = new UIValidationGrouping();
                validationGroup_testObject.GroupingList.Add(groupting);
                busValidationGroup.SaveChanges(validationGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ValidationGroupRulesIsEmpty));
            }
        }

        [Test]
        public void UpdateRuleParameter_Insert_Test()
        {
            IList<UIValidationGroupingProxy> ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            IList<UIValidationGroupingProxy> l = ruleList.Where(x => x.RuleID == ADORule1.ID).ToList();
            ClearSession();
            IList<UIValidationRuleParameter> paramList = busValidationGroup.GetRuleParameter(l[0].ID, l[0].RuleID);
            Assert.AreEqual(3, paramList.Count);
            ClearSession();
            paramList[0].TheValue = "5";
            busValidationGroup.UpdateRuleParameter(paramList);
            ClearSession();
            paramList = busValidationGroup.GetRuleParameter(l[0].ID, l[0].RuleID);
            Assert.AreEqual("5", paramList[0].Value);
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busValidationGroup.SaveChanges(ADOGroup1, UIActionType.DELETE);
                ClearSession();
                busValidationGroup.GetByID(ADOGroup1.ID);
                Assert.Fail();
            }
            catch(ItemNotExists ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Delete_SetNullPersonRelation_Test()
        {
            busValidationGroup.SaveChanges(ADOGroup1, UIActionType.DELETE);
            ClearSession();
            Person p = new BPerson().GetByID(ADOPerson1.ID);
            if (p.UIValidationGroup != null)
            {
                Assert.AreEqual(0, p.UIValidationGroup.ID);
            }
            else 
            {
                Assert.IsNull(p.UIValidationGroup);
            }
        }

        [Test]
        public void Test222222222() 
        {
            //IList<UIValidationGroupingProxy> ruleList = busValidationGroup.GetRuleList(ADOGroup2.ID);
            //DatabaseGateway.TA_UIValidationRuleParameterDataTable table = parmTA.GetDataByGroupingID(ruleList[0].ID);
          
            //ClearSession();
            //IList<UIValidationRuleParameter> paramList = busValidationGroup.GetRuleParameter(ruleList[0].ID, ruleList[0].RuleID);
            //paramList = busValidationGroup.GetRuleParameter(621, ruleList[2].RuleID);
            //DatabaseGateway.TA_UIValidationRuleParameterDataTable table2 = parmTA.GetDataByGroupingID(ruleList[0].ID);
      

        }

    }
}
