using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Rules;
using BusinessProxy = GTS.Clock.Business.Proxy;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class PersonAdvanceSearchTest : BaseFixture
    {
        #region variables
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workgrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assignWorkGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assinTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter groupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter ruleCatTA = new DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter prsRleCatAsgTA = new DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.QueriesTableAdapter queris = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.QueriesTableAdapter();
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();


        ISearchPerson searchTool;
        BPerson bPerson;
        WorkGroup ADOWorkGroup1 = new WorkGroup();
        WorkGroup ADOWorkGroup2 = new WorkGroup();
        RuleCategory ADORuleCat1 = new RuleCategory();
        RuleCategory ADORuleCat2 = new RuleCategory();
        CalculationRangeGroup ADOGroup1 = new CalculationRangeGroup();
        CalculationRangeGroup ADOGroup2 = new CalculationRangeGroup();
        Person person_testObject;
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        OrganizationUnit ADOOrganParent = new OrganizationUnit();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            searchTool = new BPerson();
            bPerson = new BPerson();
            person_testObject = new Person();          
           
            #region Workgroup
            workgrpTA.Insert("WorkGroupTest1", "0-0", 0);
            workgrpTA.Insert("WorkGroupTest2", "0-1", 0);
            DatabaseGateway.TA_WorkGroupDataTable table = new DatabaseGateway.TA_WorkGroupDataTable();
            workgrpTA.FillByName(table, "WorkGroupTest1");
            ADOWorkGroup1.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup1.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup1.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            workgrpTA.FillByName(table, "WorkGroupTest2");
            ADOWorkGroup2.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup2.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup2.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);


            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson1.ID, new DateTime(2007, 4, 5));
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2007, 4, 5));
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2012, 5, 5));
            assignWorkGroupTA.Insert(ADOWorkGroup2.ID, ADOPerson2.ID, new DateTime(2010, 11, 6));
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2009, 6, 15));
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2010, 8, 5));
            #endregion

            #region Calculation DateRange
            groupTA.Insert("RangeGroup1", "", 1);
            groupTA.Insert("RangeGroup2", "", 1);
            DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            groupTA.FillByGroupName(groupTable, "RangeGroup1");

            ADOGroup1.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup1.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup1.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            groupTA.FillByGroupName(groupTable, "RangeGroup2");

            ADOGroup2.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup2.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup2.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            assinTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2010, 2, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2010, 2, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2005, 5, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2007, 11, 5));
            assinTA.Insert(ADOPerson2.ID, ADOGroup2.ID, new DateTime(2010, 9, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2012, 9, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2008, 3, 1));
            #endregion

            #region RuleGroup

            ruleCatTA.Insert("RuleGroupTest1", "0000", false, "00-00test1");
            ruleCatTA.Insert("RuleGroupTest2", "0000", false, "00-00test1");

            DatabaseGateway.TA_RuleCategoryDataTable ruleTable = ruleCatTA.GetDataByName("RuleGroupTest1");
            ADORuleCat1.ID = (Decimal)ruleTable[0]["RuleCat_ID"];
            ADORuleCat1.Name = (String)ruleTable[0]["RuleCat_Name"];

            ruleTable = ruleCatTA.GetDataByName("RuleGroupTest2");
            ADORuleCat2.ID = (Decimal)ruleTable[0]["RuleCat_ID"];
            ADORuleCat2.Name = (String)ruleTable[0]["RuleCat_Name"];

            prsRleCatAsgTA.Insert(ADOPerson1.ID, ADORuleCat1.ID, "2005/05/15", "2007/05/08", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2005/05/15", "2007/05/08", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2008/01/01", "2010/01/01", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2010/01/01", "2010/12/01", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2010/12/01", "2011/03/01", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat2.ID, "2011/03/02", "2015/03/01", null);
         
	        #endregion

            #region OrganizationUnit
            DatabaseGateway.TA_OrganizationUnitDataTable organtable = organTA.GetDataByParent();
            ADOOrganParent.ID = Convert.ToInt32(organtable.Rows[0]["organ_ID"]);
            ADOOrganParent.CustomCode = Convert.ToString(organtable.Rows[0]["organ_CustomCode"]);

            organTA.Insert("TestLevel2_1", "0-1", ADOPerson2.ID, ADOOrganParent.ID, String.Format(",{0},", ADOOrganParent.ID));
            organtable = organTA.GetDataByCustomCode("0-1");
            ADOOrgan.ID = Convert.ToInt32(organtable.Rows[0]["organ_ID"]);
            ADOOrgan.ParentID = Convert.ToInt32(organtable.Rows[0]["organ_ParentID"]);
            ADOOrgan.Name = Convert.ToString(organtable.Rows[0]["organ_Name"]);
            ADOOrgan.CustomCode = Convert.ToString(organtable.Rows[0]["organ_CustomCode"]);
            ADOOrgan.PersonID = Convert.ToInt32(organtable.Rows[0]["organ_PersonID"]);
            #endregion

            #region Manager
            managerTA.Insert(ADOPerson1.ID, null);
            managerTA.Insert(null, ADOOrgan.ID);

            DatasetGatewayWorkFlow.TA_ManagerDataTable managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;

            managerTA.FillByOrganID(managetTable, ADOOrgan.ID);
            ADOManager2.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.OrganizationUnit = ADOOrgan;
            #endregion
        }

        [TearDown]
        public void TreatDown()
        {
            workgrpTA.DeleteByCustomCode("0-0");
            workgrpTA.DeleteByCustomCode("0-1");
            groupTA.DeleteByName("RangeGroup1");
            groupTA.DeleteByName("RangeGroup2");
            prsRleCatAsgTA.DeleteByRuleCategory("00-00test1");
            ruleCatTA.DeleteByCustomCode("00-00test1");          
            managerTA.DeleteByBarcode(ADOPerson1.PersonCode);
            managerTA.DeleteByOrganCustomCode("0-1");
            organTA.DeleteByCustomCode("0-1");

        }

        [Test]
        public void SexSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.Sex = PersonSex.Female;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
            Assert.AreEqual(ADOPerson2.ID, list.Where(x => x.ID == ADOPerson2.ID).First().ID);
        }

        [Test]
        public void SexSearch_WithoutPaging_Test()
        {            
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.Sex = PersonSex.Female;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
            Assert.AreEqual(ADOPerson2.ID, list.Where(x => x.ID == ADOPerson2.ID).First().ID);
        }

        [Test]
        public void MilitarySearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.Military = MilitaryStatus.HeineKhedmat;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() == 0);
            Assert.AreEqual(ADOPerson1.ID, list.Where(x => x.ID == ADOPerson1.ID).First().ID);
        }

        [Test]
        public void EducationSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.Education = "لیسانس";
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
        }

        [Test]
        public void MariageSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.MaritalStatus = MaritalStatus.Mojarad;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() == 0);
            Assert.AreEqual(ADOPerson1.ID, list.Where(x => x.ID == ADOPerson1.ID).First().ID);
        }

        [Test]
        public void DepartmentSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.DepartmentId = ADODepartment1.ID;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.AreEqual(ADOPerson1.ID, list.First().ID);
        }

        [Test]
        public void DepartmentSearchCount_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.DepartmentId = ADODepartment1.ID;
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void SubDepartmentSearch_Test() 
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.DepartmentId = ADORoot.ID;
            proxy.IncludeSubDepartments = true;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
        }

        [Test]
        public void WorkGroupSearch_Test() 
        { 
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.WorkGroupId = ADOWorkGroup1.ID;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
        }

        [Test]
        public void WorkGroupFromDateSearch_Test1()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.WorkGroupId = ADOWorkGroup1.ID;
            proxy.WorkGroupFromDate = "2007/4/5";
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
        }

        [Test]
        public void WorkGroupFromDateSearch_Test2()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.WorkGroupId = ADOWorkGroup1.ID;
            proxy.WorkGroupFromDate = "2007/4/1";
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() == 0);
        }

        [Test]
        public void RuleGroupSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.RuleGroupId = ADORuleCat1.ID;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
        }

        [Test]
        public void RuleGroupFromDateSearch_Test1()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.RuleGroupId = ADORuleCat1.ID;
            proxy.RuleGroupFromDate = Utility.ToString(new DateTime(2005, 05, 1));
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
        }

        [Test]
        public void RuleGroupFromToDateSearch_Test1()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.RuleGroupId = ADORuleCat1.ID;
            proxy.RuleGroupFromDate = Utility.ToString(new DateTime(2005, 05, 1));
            proxy.RuleGroupToDate = Utility.ToString(new DateTime(2007, 06, 1));
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count == 2);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() == 1);
        }

        [Test]
        public void RuleGroupFromDateSearch_Test2()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.RuleGroupId = ADORuleCat1.ID;
            proxy.RuleGroupFromDate = Utility.ToString(new DateTime(2007, 05, 1));
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
        }

        [Test]
        public void DateRangeGroupSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.CalculationDateRangeId = ADOGroup2.ID;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 0);
        }

        [Test]
        public void DateRangeFromDateSearch_Test1()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.CalculationDateRangeId = ADOGroup1.ID;
            proxy.CalculationFromDate = Utility.ToString(new DateTime(2008, 2, 1));
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
        }

        [Test]
        public void DateRangeFromDateSearch_Test2()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.CalculationDateRangeId = ADOGroup1.ID;
            proxy.CalculationFromDate = Utility.ToString(new DateTime(2011, 2, 1));
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0);
        }

        [Test]
        public void ControlStationSearch_Test() 
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.ControlStationId = ADOStaion1.ID;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.AreEqual(ADOPerson1.ID, list.First().ID);
        }

        [Test]
        public void EmployeeSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.EmploymentType = ADOEmploymentType1.ID;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Count > 0);
            Assert.AreEqual(ADOPerson1.ID, list.First().ID);
        }

        [Test]
        public void NameSearch_Test() 
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.FirstName = "ali";
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0,"FirstName Count");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0,"Person 1");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0,"Person 2");

            proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.LastName = "ali";
            list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0, "LastName Count");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0, "Person 1");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0, "Person 2");

            proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.FatherName = "HassanTest";
            list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null && list.Count > 0, "FatherName Count");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() > 0, "Person 1");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson2.ID).Count() > 0, "Person 2");
        }

        [Test]
        public void EmtySearch_Test() 
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy);
            Assert.AreEqual(6, list.Count);
        }

        [Test]
        public void ManagerSearchCount_Test() 
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.SearchInCategory = PersonCategory.Manager;
            proxy.LastName = ADOPerson2.LastName;
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            Assert.AreEqual(1, count);

            proxy.LastName = ADOPerson1.LastName;
            count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void ManagerSearch_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.SearchInCategory = PersonCategory.Manager;
            proxy.LastName = ADOPerson2.LastName;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy);
            Assert.AreEqual(ADOPerson2.ID, list.First().ID);

            proxy.LastName = ADOPerson1.LastName;
            list = searchTool.GetPersonInAdvanceSearch(proxy);
            Assert.AreEqual(ADOPerson1.ID, list.First().ID);
        }

        [Test]
        public void SubDepartmentSearch_Manager_Test()
        {
            BusinessProxy.PersonAdvanceSearchProxy proxy = new BusinessProxy.PersonAdvanceSearchProxy();
            proxy.DepartmentId = ADORoot.ID;
            proxy.IncludeSubDepartments = true;
            proxy.SearchInCategory = PersonCategory.Manager;
            proxy.LastName = ADOPerson2.LastName;
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 100);
            Assert.IsTrue(list != null);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(ADOPerson2.ID, list.First().ID);
        }

        [Test]
        public void Test222222222() 
        {
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            
            //IList<Person> l = busPerson.GetPersonInAdvanceSearch(0, 100, "");
        }
    }
}
