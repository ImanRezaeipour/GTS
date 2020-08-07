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
using GTS.Clock.Business.Proxy;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 3/12/2012 3:02:17 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class ChangeOrganicInfoTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assignWorkGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assinTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter prsRleCatAsgTA = new DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workgrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter dateRangeGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter ruleCatTA = new DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleTableAdapter ruleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_RuleTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleTypeTableAdapter ruleTypeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_RuleTypeTableAdapter();
        DatabaseGatewayTableAdapters.QueriesTableAdapter queris = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.QueriesTableAdapter();

        BChangeOrganicInfo busChnageInfo;
        ISearchPerson searchTool;
        WorkGroup ADOWorkGroup1 = new WorkGroup();
        WorkGroup ADOWorkGroup2 = new WorkGroup();
        RuleCategory ADORuleCat1 = new RuleCategory();
        RuleCategory ADORuleCat2 = new RuleCategory();
        CalculationRangeGroup ADOGroup1 = new CalculationRangeGroup();
        CalculationRangeGroup ADOGroup2 = new CalculationRangeGroup();
        Person person_testObject;
        OrganicInfoProxy infoProxy = new OrganicInfoProxy();
        IList<ChangeInfoErrorProxy> errorList = new List<ChangeInfoErrorProxy>();

        [SetUp]
        public void TestSetup()
        {
            busChnageInfo = new BChangeOrganicInfo();
            person_testObject = new Person();
            infoProxy = new OrganicInfoProxy();
            errorList = new List<ChangeInfoErrorProxy>();

            workgrpTA.Insert("WorkGroupTest1", "0-0", 0);
            workgrpTA.Insert("WorkGroupTest2", "0-1", 0);

            DatabaseGateway.TA_WorkGroupDataTable table = new DatabaseGateway.TA_WorkGroupDataTable();
            DatabaseGateway.TA_CalculationRangeGroupDataTable dateRangeGroupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            DatabaseGateway.TA_RuleCategoryDataTable ruleTable = new DatabaseGateway.TA_RuleCategoryDataTable();

            workgrpTA.FillByName(table, "WorkGroupTest1");
            ADOWorkGroup1.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup1.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup1.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            workgrpTA.FillByName(table, "WorkGroupTest2");
            ADOWorkGroup2.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup2.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup2.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson1.ID, new DateTime(2011, 01, 01));

            dateRangeGroupTA.Insert("RangeGroup1", "", 1);
            dateRangeGroupTA.Insert("RangeGroup2", "", 1);

            dateRangeGroupTA.FillByGroupName(dateRangeGroupTable, "RangeGroup1");
            ADOGroup1.ID = Convert.ToDecimal(dateRangeGroupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup1.Name = Convert.ToString(dateRangeGroupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup1.Description = Convert.ToString(dateRangeGroupTable.Rows[0]["CalcRangeGroup_Des"]);

            dateRangeGroupTA.FillByGroupName(dateRangeGroupTable, "RangeGroup2");

            ADOGroup2.ID = Convert.ToDecimal(dateRangeGroupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup2.Name = Convert.ToString(dateRangeGroupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup2.Description = Convert.ToString(dateRangeGroupTable.Rows[0]["CalcRangeGroup_Des"]);

            assinTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2011, 01, 01));

            ruleCatTA.Insert("RuleGroupTest1", "0000", false, "00-00test1");
            ruleCatTA.Insert("RuleGroupTest2", "0000", false, "00-00test1");

            ruleTable = ruleCatTA.GetDataByName("RuleGroupTest1");
            ADORuleCat1.ID = (Decimal)ruleTable[0]["RuleCat_ID"];
            ADORuleCat1.Name = (String)ruleTable[0]["RuleCat_Name"];

            ruleTable = ruleCatTA.GetDataByName("RuleGroupTest2");
            ADORuleCat2.ID = (Decimal)ruleTable[0]["RuleCat_ID"];
            ADORuleCat2.Name = (String)ruleTable[0]["RuleCat_Name"];

            ruleTA.Insert(0001, "", "", "", null, false, ADORuleCat1.ID, Utility.ToInteger(ruleTypeTA.GetData().Rows[0][0]), 0);
            ruleTA.Insert(0001, "", "", "", null, false, ADORuleCat2.ID, Utility.ToInteger(ruleTypeTA.GetData().Rows[0][0]), 0);

            prsRleCatAsgTA.Insert(ADOPerson1.ID, ADORuleCat2.ID, "2011/01/01", "2011/06/01", null);

        }

        [TearDown]
        public void TreatDown()
        {
            workgrpTA.DeleteByCustomCode("0-0");
            workgrpTA.DeleteByCustomCode("0-1");
            dateRangeGroupTA.DeleteByName("RangeGroup1");
            dateRangeGroupTA.DeleteByName("RangeGroup2");

            prsRleCatAsgTA.DeleteByRuleCategory("00-00test1");
            ruleCatTA.DeleteByCustomCode("00-00test1");
        }

        [Test]
        public void ChangeInfo_AsgWrkGrp_LessDate()
        {
            OrganicInfoProxy infoProxy = new OrganicInfoProxy();
            infoProxy.WorkGroupID = ADOWorkGroup2.ID;
            infoProxy.WorkGroupFromDate = Utility.ToPersianDate(new DateTime(2010, 12, 01));

            IList<ChangeInfoErrorProxy> errorList = new List<ChangeInfoErrorProxy>();
            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgWrkGrp_EqDate()
        {

            infoProxy.WorkGroupID = ADOWorkGroup2.ID;
            infoProxy.WorkGroupFromDate = Utility.ToPersianDate(new DateTime(2011, 01, 01));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgWrkGrp_Test()
        {

            infoProxy.WorkGroupID = ADOWorkGroup2.ID;
            infoProxy.WorkGroupFromDate = Utility.ToPersianDate(new DateTime(2011, 02, 01));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            Assert.AreEqual(2, p.AssignedWorkGroupList.Count);
        }

        [Test]
        public void ChangeInfo_AsgRangGrp_LessDate()
        {

            infoProxy.DateRangeID = ADOGroup2.ID;
            infoProxy.DateRangeFromDate = Utility.ToPersianDate(new DateTime(2010, 12, 01));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgRangGrp_EqDate()
        {

            infoProxy.DateRangeID = ADOGroup2.ID;
            infoProxy.DateRangeFromDate = Utility.ToPersianDate(new DateTime(2011, 01, 01));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgRangGrp_SameMonthDate()
        {

            infoProxy.DateRangeID = ADOGroup2.ID;
            infoProxy.DateRangeFromDate = Utility.ToPersianDate(new DateTime(2011, 01, 10));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgRangGrp_Test()
        {

            infoProxy.DateRangeID = ADOGroup2.ID;
            infoProxy.DateRangeFromDate = Utility.ToPersianDate(new DateTime(2011, 02, 01));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            Assert.AreEqual(2, p.PersonRangeAssignList.Count);
        }

        [Test]
        public void ChangeInfo_AsgRuleGrp_OverLap()
        {

            infoProxy.RuleGroupID = ADORuleCat2.ID;
            infoProxy.RuleGroupFromDate = Utility.ToPersianDate(new DateTime(2010, 12, 01));
            infoProxy.RuleGroupToDate = Utility.ToPersianDate(new DateTime(2011, 01, 02));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgRuleGrp_EqDate()
        {

            infoProxy.RuleGroupID = ADORuleCat2.ID;
            infoProxy.RuleGroupFromDate = Utility.ToPersianDate(new DateTime(2011, 01, 01));
            infoProxy.RuleGroupToDate = Utility.ToPersianDate(new DateTime(2011, 06, 01));


            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Count > 0);
            Assert.IsTrue(errorList.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() > 0);
        }

        [Test]
        public void ChangeInfo_AsgRuleGrp_Test()
        {
            ClearSession();

            infoProxy.RuleGroupID = ADORuleCat2.ID;
            infoProxy.RuleGroupFromDate = Utility.ToPersianDate(new DateTime(2012, 01, 01));
            infoProxy.RuleGroupToDate = Utility.ToPersianDate(new DateTime(2012, 05, 01));

            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            p.InitializeForExecuteRule(new DateTime(2010, 1, 1), new DateTime(2013, 1, 1), new DateTime(2010, 1, 1), new DateTime(2013, 1, 1));
            Assert.AreEqual(2, p.PersonRuleCatAssignList.Count);
        }

        [Test]
        public void ChangeInfo_Department_Test()
        {
            infoProxy.DepartmentID = ADODepartment2.ID;

            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            Assert.AreEqual(ADODepartment2.ID, p.Department.ID);
        }

        [Test]
        public void ChangeInfo_Emploee_Test()
        {
            infoProxy.EmploymentTypeID = ADOEmploymentType2.ID;

            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            Assert.AreEqual(ADOEmploymentType2.ID, p.EmploymentType.ID);
        }

        [Test]
        public void ChangeInfo_Department_Rule_Test()
        {
            infoProxy.EmploymentTypeID = ADOEmploymentType2.ID;
            infoProxy.RuleGroupID = ADORuleCat2.ID;
            infoProxy.RuleGroupFromDate = Utility.ToPersianDate(new DateTime(2012, 01, 01));
            infoProxy.RuleGroupToDate = Utility.ToPersianDate(new DateTime(2012, 05, 01));

            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            p.InitializeForExecuteRule(new DateTime(2010, 1, 1), new DateTime(2013, 1, 1), new DateTime(2010, 1, 1), new DateTime(2013, 1, 1));
            Assert.AreEqual(2, p.PersonRuleCatAssignList.Count);
            Assert.AreEqual(ADOEmploymentType2.ID, p.EmploymentType.ID);
        }


        [Test]
        public void ChangeInfo_All_Test()
        {
            infoProxy.DepartmentID = ADODepartment2.ID;
            infoProxy.EmploymentTypeID = ADOEmploymentType2.ID;

            infoProxy.RuleGroupID = ADORuleCat2.ID;
            infoProxy.RuleGroupFromDate = Utility.ToPersianDate(new DateTime(2012, 01, 01));
            infoProxy.RuleGroupToDate = Utility.ToPersianDate(new DateTime(2012, 05, 01));

            infoProxy.DateRangeID = ADOGroup2.ID;
            infoProxy.DateRangeFromDate = Utility.ToPersianDate(new DateTime(2011, 02, 01));

            infoProxy.WorkGroupID = ADOWorkGroup2.ID;
            infoProxy.WorkGroupFromDate = Utility.ToPersianDate(new DateTime(2011, 02, 01));

            bool result = busChnageInfo.ChangeInfo(ADOPerson1.PersonCode, infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();

            Person p = new BPerson().GetByID(ADOPerson1.ID);
            p.InitializeForExecuteRule(new DateTime(2010, 1, 1), new DateTime(2013, 1, 1), new DateTime(2010, 1, 1), new DateTime(2013, 1, 1));
            Assert.AreEqual(2, p.PersonRuleCatAssignList.Count);
            Assert.AreEqual(2, p.PersonRangeAssignList.Count);
            Assert.AreEqual(2, p.AssignedWorkGroupList.Count);
            Assert.AreEqual(ADOEmploymentType2.ID, p.EmploymentType.ID);
            Assert.AreEqual(ADODepartment2.ID, p.Department.ID);
        }

        [Test]
        public void ChangeInfo_2Person_All_Test()
        {
            infoProxy.DepartmentID = ADODepartment2.ID;
            infoProxy.EmploymentTypeID = ADOEmploymentType2.ID;

            infoProxy.RuleGroupID = ADORuleCat2.ID;
            infoProxy.RuleGroupFromDate = Utility.ToPersianDate(new DateTime(2012, 01, 01));
            infoProxy.RuleGroupToDate = Utility.ToPersianDate(new DateTime(2012, 05, 01));

            infoProxy.DateRangeID = ADOGroup2.ID;
            infoProxy.DateRangeFromDate = Utility.ToPersianDate(new DateTime(2011, 02, 01));

            infoProxy.WorkGroupID = ADOWorkGroup2.ID;
            infoProxy.WorkGroupFromDate = Utility.ToPersianDate(new DateTime(2011, 02, 01));
            
            bool result = busChnageInfo.ChangeInfo("TestAlian", infoProxy, out errorList);
            Assert.IsTrue(result);

            ClearSession();
            Person p = new BPerson().GetByID(ADOPerson1.ID);

            p.InitializeForExecuteRule(new DateTime(2010, 1, 1), new DateTime(2013, 1, 1), new DateTime(2010, 1, 1), new DateTime(2013, 1, 1));

            Assert.AreEqual(2, p.PersonRuleCatAssignList.Count);
            Assert.AreEqual(2, p.PersonRangeAssignList.Count);
            Assert.AreEqual(2, p.AssignedWorkGroupList.Count);
            Assert.AreEqual(ADOEmploymentType2.ID, p.EmploymentType.ID);
            Assert.AreEqual(ADODepartment2.ID, p.Department.ID);

            p = new BPerson().GetByID(ADOPerson2.ID);
            p.InitializeForExecuteRule(new DateTime(2010, 1, 1), new DateTime(2013, 1, 1), new DateTime(2010, 1, 1), new DateTime(2013, 1, 1));
            Assert.AreEqual(1, p.PersonRuleCatAssignList.Count);
            Assert.AreEqual(1, p.PersonRangeAssignList.Count);
            Assert.AreEqual(1, p.AssignedWorkGroupList.Count);
            Assert.AreEqual(ADOEmploymentType2.ID, p.EmploymentType.ID);
            Assert.AreEqual(ADODepartment2.ID, p.Department.ID);
        }
    }
}
