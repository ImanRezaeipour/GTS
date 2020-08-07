using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class ManagerBusinessTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter undermanagmentTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter mangFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter userTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter();
        DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter departmentTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
        #endregion

        #region ADOObjects
        BManager busManager;
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        Flow ADOFlow1 = new Flow();
        Manager manager_testObject;
        OrganizationUnit ADOOrganParent = new OrganizationUnit();
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        OrganizationUnit ADOOrgan2 = new OrganizationUnit();
        OrganizationUnit ADOOrgan3 = new OrganizationUnit();
        PrecardAccessGroup ADOAccessGroup1 = new PrecardAccessGroup();
        PrecardAccessGroup ADOAccessGroup2 = new PrecardAccessGroup();
        User ADOUser1 = new User();
        User ADOUser2 = new User();
        ManagerFlow ADOmangFlow = new ManagerFlow();

        #endregion

        [SetUp]
        public void TestSetup()
        {
            manager_testObject = new Manager();
            busManager = new BManager();

            userTA.InsertQuery(ADOPerson1.ID, "TestADOPerson1");
            userTA.InsertQuery(ADOPerson3.ID, "TestADOPerson3");
            DatabaseGateway.TA_SecurityUserDataTable userTable = new DatabaseGateway.TA_SecurityUserDataTable();
            userTable = userTA.GetDataByUserName("TestADOPerson1");
            ADOUser1.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
            ADOUser1.UserName = "TestADOPerson1";
            userTable = userTA.GetDataByUserName("TestADOPerson3");
            ADOUser2.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
            ADOUser2.UserName = "TestADOPerson3";

            DatabaseGateway.TA_OrganizationUnitDataTable table = organTA.GetDataByParent();
            ADOOrganParent.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrganParent.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);

            organTA.Insert("TestLevel2_1", "0-1", ADOPerson2.ID, ADOOrganParent.ID, String.Format(",{0},", ADOOrganParent.ID));
            table = organTA.GetDataByCustomCode("0-1");
            ADOOrgan.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrgan.ParentID = Convert.ToInt32(table.Rows[0]["organ_ParentID"]);
            ADOOrgan.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrgan.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);
            ADOOrgan.PersonID = Convert.ToInt32(table.Rows[0]["organ_PersonID"]);

            organTA.Insert("TestLevel2_2", "0-2", ADOPerson1.ID, ADOOrganParent.ID, String.Format(",{0},", ADOOrganParent.ID));
            table = organTA.GetDataByCustomCode("0-2");
            ADOOrgan2.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrgan2.ParentID = Convert.ToInt32(table.Rows[0]["organ_ParentID"]);
            ADOOrgan2.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrgan2.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);
            ADOOrgan2.PersonID = Convert.ToInt32(table.Rows[0]["organ_PersonID"]);

            organTA.Insert("TestLevel2_3", "0-3", ADOPerson3.ID, ADOOrganParent.ID, String.Format(",{0},", ADOOrganParent.ID));
            table = organTA.GetDataByCustomCode("0-3");
            ADOOrgan3.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrgan3.ParentID = Convert.ToInt32(table.Rows[0]["organ_ParentID"]);
            ADOOrgan3.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrgan3.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);
            ADOOrgan3.PersonID = Convert.ToInt32(table.Rows[0]["organ_PersonID"]);

            managerTA.Insert(ADOPerson1.ID, null);
            managerTA.Insert(null, ADOOrgan.ID);

            DatasetGatewayWorkFlow.TA_ManagerDataTable managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;
            
            managerTA.FillByOrganID(managetTable, ADOOrgan.ID);
            ADOManager2.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.OrganizationUnit = ADOOrgan;

            accessGroupTA.Insert("AccessGroup1_2");
            accessGroupTA.Insert("AccessGroup1_3");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = accessGroupTA.GetDataBy1("AccessGroup1_2");
            ADOAccessGroup1.ID = Convert.ToInt32(accessTable.Rows[0][0]);
            ADOAccessGroup1.Name = "AccessGroup1_2";
            accessTable = accessGroupTA.GetDataBy1("AccessGroup1_3");
            ADOAccessGroup2.ID = Convert.ToInt32(accessTable.Rows[0][0]);
            ADOAccessGroup2.Name = "AccessGroup1_3";  

            flowTA.Insert(ADOAccessGroup1.ID, false, false, "FlowTest");
            DatasetGatewayWorkFlow.TA_FlowDataTable mangTAble = flowTA.GetDataByName("FlowTest");
            ADOFlow1.ID = Convert.ToInt32(mangTAble.Rows[0][0]);
            ADOFlow1.FlowName = "FlowTest";
            ADOFlow1.ActiveFlow = false;
            ADOFlow1.WorkFlow = false;

            mangFlowTA.Insert(ADOManager2.ID, 1, ADOFlow1.ID, true);
            mangFlowTA.Insert(ADOManager1.ID, 2, ADOFlow1.ID, true);

            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson3.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson5.ID, ADODepartment2.ID, false, false);

            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager1.ID, false);
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager2.ID, false);
        }

        [TearDown]
        public void TreatDown()
        {
            flowTA.DeleteByName("FlowTest");
            flowTA.DeleteByName("FlowTest1");
            flowTA.DeleteByName("TestInsertedFlow");
       
            organTA.DeleteByCustomCode("0-1");
            organTA.DeleteByCustomCode("0-2");
            organTA.DeleteByCustomCode("0-3");
           
            accessGroupTA.DeleteByName("AccessGroup1_2");
            accessGroupTA.DeleteByName("AccessGroup1_3");
            
         
        }

        [Test]
        public void GetByID_Test()
        {
            manager_testObject = busManager.GetByID(ADOManager1.ID);
            Assert.IsNotNull(manager_testObject);
            Assert.IsTrue(manager_testObject.ID == ADOManager1.ID);

        }

        [Test]
        public void GetAllByPage_Test()
        {
            IList<Manager> list = busManager.GetAllByPage(0, 10);
            int count = busManager.GetRecordCount();
            if (count < 10)
            {
                Assert.AreEqual(list.Count, count);
            }
            else
            {
                Assert.AreEqual(list.Count, 10);
            }
        }

        [Test]
        public void GetAll_Test()
        {
            IList<Manager> list = busManager.GetAll();
            Assert.AreEqual(list.Count, managerTA.GetCount());
        }

        [Test]
        public void GetByUsername_Test() 
        {
            manager_testObject= busManager.GetManagerByUsername(ADOUser1.UserName);
            Assert.AreEqual(manager_testObject.ID, ADOManager1.ID);
        }
     
        #region Search

        [Test]
        public void GetSearchCount_NameTest()
        {

            int count = busManager.GetRecordCountBySearch(ADOPerson1.LastName, ManagerSearchFields.PersonName);
            Assert.AreEqual(1, count);
            count = busManager.GetRecordCountBySearch("lian", ManagerSearchFields.PersonName);
            Assert.GreaterOrEqual(5, count);
        }

        [Test]
        public void GetSearchCount_CodeTest()
        {
            int count = busManager.GetRecordCountBySearch(ADOPerson1.PersonCode, ManagerSearchFields.PersonCode);
            Assert.AreEqual(1, count);
            count = busManager.GetRecordCountBySearch(ADOPerson2.PersonCode, ManagerSearchFields.PersonCode);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetSearchCount_OrganNameTest()
        {
            int count = busManager.GetRecordCountBySearch(ADOOrgan.Name, ManagerSearchFields.OrganizationUnitName);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetSearchResult_NameTest() 
        {
            IList<Manager> list = busManager.SearchByPage(ADOPerson1.LastName, ManagerSearchFields.PersonName, 0, 10);
            Assert.AreEqual(list[0].ID, ADOManager1.ID);
            list = busManager.SearchByPage("lian", ManagerSearchFields.PersonName, 0, 10);
            Assert.AreEqual(list[1].ID, ADOManager2.ID);
        }

        [Test]
        public void GetSearchResult_CodeTest()
        {
            IList<Manager> list = busManager.SearchByPage(ADOPerson1.PersonCode, ManagerSearchFields.PersonCode, 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOManager1.ID).Count() == 1);
            list = busManager.SearchByPage(ADOPerson2.PersonCode, ManagerSearchFields.PersonCode, 0, 100);
            Assert.IsTrue(list.Where(x => x.ID == ADOManager2.ID).Count() == 1);
        }

        [Test]
        public void GetSearchResult_OrganNameTest()
        {
            IList<Manager> list = busManager.SearchByPage(ADOOrgan.Name, ManagerSearchFields.OrganizationUnitName, 0, 10);
            Assert.AreEqual(ADOManager2.ID, list[0].ID);
        }

        [Test]
        public void GetSearchResult_PersonOrganNameTest()
        {
            IList<Manager> list = busManager.SearchByPage(ADOOrgan2.Name, ManagerSearchFields.OrganizationUnitName, 0, 10);
            Assert.AreEqual(ADOManager1.ID, list[0].ID);
        }

        [Test]
        public void QuickSearch_Test() 
        {
            IList<Person> list = busManager.QuickSearchPersonByPage(ADOPerson1.LastName.Remove(0, 2), 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }

        [Test]
        public void QuickSearch2_Test()
        {
            int count = busManager.GetRecordCountBySearch(ADOPerson1.LastName.Remove(0, 2),ManagerSearchFields.NotSpecified);
            Assert.IsTrue(count >= 1);
        }

        [Test]
        public void QuickSearch3_Test()
        {
            IList<Manager> list = busManager.SearchByPage(ADOPerson1.LastName.Remove(0, 2), ManagerSearchFields.NotSpecified, 0, 10);
            Assert.IsTrue(list.Count >= 1);
        }


        [Test]
        public void QuickSearchPersonCount_EmptyTest() 
        {
            IDataAccess access=new BUser();
            int count = busManager.QuickSearchPersonCount("");
            int count2 = access.GetAccessiblePersonByDepartment().Count;
            Assert.AreEqual(count2, count);
        }     

        [Test]
        public void GetCountByAccessID_Test() 
        {            

            int count = busManager.GetRecordCountByAccessGroupFilter(ADOAccessGroup1.ID);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetByAccessID_Test()
        {          

            IList<Manager> list = busManager.SearchByAccessGroup(ADOAccessGroup1.ID, 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOManager1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOManager2.ID).Count() == 1);
        }

        [Test]
        public void GetByAccessID_EmptyTest()
        {
            IList<Manager> list1 = busManager.SearchByAccessGroup(0, 0, 10);
            IList<Manager> list2 = busManager.GetAllByPage(0, 10);
            Assert.AreEqual(list2.Count, list1.Count);
        }

        [Test]
        public void QuickSearchByOrganizationUnitName_Test() 
        {
            dataAccessOrganTA.Insert(BUser.CurrentUser.ID, ADOOrganParent.ID, false);

            IList<OrganizationUnit> list = busManager.QuickSearchByOrganizationUnitName(ADOOrgan2.Name);
            Assert.IsTrue(list.Where(x => x.ID == ADOOrgan2.ID).Count() > 0);
        }

        [Test]
        public void QuickSearchByOrganizationUnitName_NotHavePermisionOnOrganiztionUnit_Test()
        {
            IList<OrganizationUnit> list = busManager.QuickSearchByOrganizationUnitName(ADOOrgan2.Name);
            Assert.AreEqual(0, list.Count);
        }

        #endregion

        [Test]
        public void ManagerDetailTest() 
        {
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, ADOManager1.ID, false);
            dataAccessFlowTA.Insert(BUser.CurrentUser.ID, ADOFlow1.ID, false);

            IList<Flow> list = busManager.GetManagerDetail(ADOManager1.ID);
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list[0].ID== ADOFlow1.ID);
        }

        [Test]
        public void ManagmentFlow_Test()
        {
            IList<Manager> list= busManager.GetManagerFlow(ADOFlow1.ID);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(list[0].ID, ADOManager2.ID);
            Assert.AreEqual(list[1].ID, ADOManager1.ID);
        }     

        #region Insert
       
        [Test]
        public void InsertFlow_Test()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByPerson(ADOPerson3.ID, ADOAccessGroup2.ID, "TestInsertedFlow", unders);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }     

        [Test]
        public void InsertFlow_AccessGroupRequierdTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByPerson(ADOPerson3.ID, 0, "TestInsertedFlow", unders);
                Assert.Fail("گروه دسترسی نباید خالی باشد");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(((UIValidationExceptions)ex).Exists(ExceptionResourceKeys.FlowAccessGroupRequierd));
            }
        }

        [Test]
        public void InsertFlow_FlowNameRequierdTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByPerson(ADOPerson3.ID, ADOAccessGroup2.ID, "", unders);
                Assert.Fail("نام گردش کار نباید خالی باشد");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(((UIValidationExceptions)ex).Exists(ExceptionResourceKeys.FlowNameRequierd));
            }
        }

        [Test]
        public void InsertFlow_FlowNameRepeatedTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByPerson(ADOPerson3.ID, ADOAccessGroup2.ID, ADOFlow1.FlowName, unders);
                Assert.Fail("نام گردش کار نباید تکراری باشد");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(((UIValidationExceptions)ex).Exists(ExceptionResourceKeys.FlowNameRepeated));
            }
        }


        [Test]
        public void InsertFlowByOrgan_Test()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByOrganization(ADOOrgan3.ID, ADOAccessGroup2.ID, "TestInsertedFlow", unders);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void InsertFlowByOrgan_AccessGroupRequierdTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByOrganization(ADOOrgan3.ID, 0, "TestInsertedFlow", unders);
                Assert.Fail("گروه دسترسی نباید خالی باشد");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(((UIValidationExceptions)ex).Exists(ExceptionResourceKeys.FlowAccessGroupRequierd));
            }
        }

        [Test]
        public void InsertFlowByOrgan_FlowNameRequierdTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                ADOFlow1.FlowName = "";
                decimal id = busManager.InsertFlowByOrganization(ADOOrgan3.ID, ADOAccessGroup2.ID, "", unders);
                Assert.Fail("نام گردش کار نباید خالی باشد");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(((UIValidationExceptions)ex).Exists(ExceptionResourceKeys.FlowNameRequierd));
            }
        }

        [Test]
        public void InsertFlowByOrgan_FlowNameRepeatedTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                decimal id = busManager.InsertFlowByOrganization(ADOOrgan3.ID, ADOAccessGroup2.ID, ADOFlow1.FlowName, unders);
                Assert.Fail("نام گردش کار نباید تکراری باشد");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(((UIValidationExceptions)ex).Exists(ExceptionResourceKeys.FlowNameRepeated));
            }
        }

        [Test]
        public void InsertFlowByOrgan_ChangeManagerByOrganTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);
                ADOFlow1.FlowName = ADOFlow1.FlowName + "1";
                decimal id = busManager.InsertFlowByPerson(ADOPerson3.ID, ADOAccessGroup2.ID, ADOFlow1.FlowName, unders);
                id = busManager.InsertFlowByOrganization(ADOOrgan3.ID, ADOAccessGroup2.ID, ADOFlow1.FlowName, unders);
               
            }
            catch (Exception ex)
            {
                ClearSession();
                Manager mng = busManager.GetManagerByUsername(ADOUser2.UserName);

                Assert.IsTrue(mng.OrganizationUnit.ID > 0);
            }
        }



        #endregion

        #region Update
       
        [Test]
        public void UpdateFlow_Test() 
        {
            IList<UnderManagment> unders = new List<UnderManagment>();
            UnderManagment under1 = new UnderManagment();
            under1.Department = new Department() { ID = ADODepartment1.ID };
            under1.Contains = true;
            UnderManagment under2 = new UnderManagment();
            under2.Department = new Department() { ID = ADODepartment2.ID };
            under2.Contains = true;
            under2.Person = new Person() { ID = ADOPerson4.ID };
            unders.Add(under1);
            unders.Add(under2);

            busManager.UpdateFlow(ADOFlow1.ID, ADOAccessGroup2.ID, "FlowTest", unders);
            ClearSession();

            BFlow bflow = new BFlow();
            Flow testObject = bflow.GetByID(ADOFlow1.ID);
            Assert.AreEqual(ADOAccessGroup2.ID, testObject.AccessGroup.ID);
            Assert.IsNotNull(testObject.UnderManagmentList);
            Assert.AreEqual(2, testObject.UnderManagmentList.Count);
            Assert.IsTrue(testObject.UnderManagmentList.Where(x =>x.Person!=null && x.Person.ID == ADOPerson4.ID).Count() == 1);
        }

        [Test]
        public void UpdateFlow_EmptyNameTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);

                busManager.UpdateFlow(ADOFlow1.ID, ADOAccessGroup2.ID, "", unders);
                Assert.Fail("نام گردش کار نباید خالی باشد");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.FlowNameRequierd));
            }
        }

        [Test]
        public void UpdateFlow_AccessGroupTest()
        {
            try
            {
                IList<UnderManagment> unders = new List<UnderManagment>();
                UnderManagment under1 = new UnderManagment();
                under1.Department = new Department() { ID = ADODepartment1.ID };
                under1.Contains = true;
                UnderManagment under2 = new UnderManagment();
                under2.Department = new Department() { ID = ADODepartment2.ID };
                under2.Contains = true;
                under2.Person = new Person() { ID = ADOPerson4.ID };
                unders.Add(under1);
                unders.Add(under2);

                busManager.UpdateFlow(ADOFlow1.ID, 0, "", unders);
                Assert.Fail("گروه دسترسی نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.FlowAccessGroupRequierd));
            }
        }

        #endregion

        #region Delete

        [Test]
        public void DeleteManager_UsedByFlowTest() 
        {
            try
            {
                busManager.DeleteManager(ADOManager1.ID);
                Assert.Fail("بوسیله جریان کاری استفاده شده است");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ManagerUsedByFlow));
            }
        }

        [Test]
        public void DeleteManager_Test()
        {
            try
            {
                mangFlowTA.DeleteQuery(ADOFlow1.ID);
                busManager.DeleteManager(ADOManager1.ID);
                ClearSession();
                busManager.GetByID(ADOManager1.ID);
                Assert.Fail("نباید در دیتابیس موجود باشد");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void Delete_FlowTest()
        {
            try
            {
                busManager.DeleteFlow(ADOFlow1.ID);
                Flow f = new BFlow().GetByID(ADOFlow1.ID);
                Assert.Fail("نباید در دیتابیس موجود باشد");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        #endregion

       
    }
}
