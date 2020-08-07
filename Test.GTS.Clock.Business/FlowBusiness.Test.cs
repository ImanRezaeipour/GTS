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
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class FlowBusinessTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter undermanagmentTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter mangFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonDetailTableAdapter personDtilTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        #endregion

        #region ADOObjects
        BFlow busflow;
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        Flow ADOFlow1 = new Flow();
        Flow flow_testObject;
        OrganizationUnit ADOOrganParent = new OrganizationUnit();
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        OrganizationUnit ADOOrgan2 = new OrganizationUnit();
        PrecardAccessGroup ADOAccessGroup1 = new PrecardAccessGroup();
        PrecardAccessGroup ADOAccessGroup2 = new PrecardAccessGroup();
        User ADOUser = new User();
        ManagerFlow ADOmangFlow = new ManagerFlow();
        ManagerFlow ADOManagerFlow1 = new ManagerFlow();
        ManagerFlow ADOManagerFlow2 = new ManagerFlow();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            busflow = new BFlow();
            flow_testObject = new Flow();


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

            DatasetGatewayWorkFlow.TA_ManagerFlowDataTable nbgFlowTable = mangFlowTA.GetDataByFlowID(ADOFlow1.ID);
            ADOManagerFlow1.ID = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_ID"]);
            ADOManagerFlow1.Level = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_LEVEL"]);

            ADOManagerFlow2.ID = Convert.ToInt32(nbgFlowTable.Rows[1]["mngrFlow_ID"]);
            ADOManagerFlow2.Level = Convert.ToInt32(nbgFlowTable.Rows[1]["mngrFlow_LEVEL"]);

            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson3.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson5.ID, ADODepartment2.ID, false, false);

        }

        [TearDown]
        public void TreatDown()
        {
            flowTA.DeleteByName("FlowTest");
            flowTA.DeleteByName("InsertedFlow");
     
            organTA.DeleteByCustomCode("0-1");
            organTA.DeleteByCustomCode("0-2");
           
            accessGroupTA.DeleteByName("AccessGroup1_2");
            accessGroupTA.DeleteByName("AccessGroup1_3");
     
        }

        [Test]
        public void GetByID_Test()
        {
            flow_testObject = busflow.GetByID(ADOFlow1.ID);
            Assert.IsNotNull(flow_testObject);
            Assert.IsTrue(flow_testObject.ID == ADOFlow1.ID);
        }

        [Test]
        public void GetAll_Test()
        {
            dataAccessFlowTA.Insert(BUser.CurrentUser.ID, ADOFlow1.ID,false);         

            IList<Flow> list = busflow.GetAll();
            Assert.AreEqual(1,list.Count);
        }

        [Test]
        public void GetAllManagetFlow_Test()
        {
            IList<ManagerProxy> list = busflow.GetAllManagers(ADOFlow1.ID);          
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(ADOOrgan.ID, list.Where(x => x.ManagerType == ManagerType.OrganizationUnit).First().OwnerID);
            Assert.AreEqual(ADOPerson1.ID, list.Where(x => x.ManagerType == ManagerType.Person).First().OwnerID);
        }

        [Test]
        public void LoadMAnagerFlow_Test()
        {
            flow_testObject = busflow.GetByID(ADOFlow1.ID);
            Assert.IsNotNull(flow_testObject.ManagerFlowList);
            Assert.AreEqual(2, flow_testObject.ManagerFlowList.Count);
        }

        [Test]
        public void Update_DeleteExtraManagerFlowTest1()
        {
            ManagerProxy mf = new ManagerProxy();
            mf.Level = 3;
            mf.OwnerID = ADOManager2.OrganizationUnit.ID;
            mf.ManagerType = ManagerType.OrganizationUnit;
            List<ManagerProxy> mngFlows = new List<ManagerProxy>();
            mngFlows.Add(mf);
            busflow.UpdateManagerFlows(ADOFlow1.ID, false, false, mngFlows);
            ClearSession();
            flow_testObject = new Flow();
            flow_testObject = busflow.GetByID(ADOFlow1.ID);
            Assert.AreEqual(3, flow_testObject.ManagerFlowList.Count);
            Assert.AreEqual(3, flow_testObject.ManagerFlowList[2].Level);
            Assert.AreEqual(false, flow_testObject.ManagerFlowList[1].Active);
            Assert.AreEqual(true, flow_testObject.ManagerFlowList[2].Active);
            Assert.AreEqual(false, flow_testObject.ActiveFlow);
        }

        [Test]
        public void Update_DeleteExtraManagerFlowTest2()
        {
            ManagerProxy mf = new ManagerProxy();
            mf.Level = 3;
            mf.OwnerID = ADOManager2.OrganizationUnit.PersonID;
            mf.ManagerType = ManagerType.Person;
            List<ManagerProxy> mngFlows = new List<ManagerProxy>();
            mngFlows.Add(mf);
            busflow.UpdateManagerFlows(ADOFlow1.ID, false, false, mngFlows);
            ClearSession();
            flow_testObject = new Flow();
            flow_testObject = busflow.GetByID(ADOFlow1.ID);
            Assert.AreEqual(3, flow_testObject.ManagerFlowList.Count);
            Assert.AreEqual(3, flow_testObject.ManagerFlowList[2].Level);
            Assert.AreEqual(false, flow_testObject.ActiveFlow);
        }        

        [Test]
        public void Update_InsertTest()
        {
            flow_testObject = busflow.GetByID(ADOFlow1.ID);
            ManagerFlow mf = new ManagerFlow();
            mf.Level = 3;
            mf.Manager = ADOManager2;
            mf.Flow = flow_testObject;
            flow_testObject.ManagerFlowList.Add(mf);
            ClearSession();
            busflow.SaveChanges(flow_testObject, UIActionType.EDIT);
            ClearSession();
            flow_testObject = new Flow();
            flow_testObject = busflow.GetByID(ADOFlow1.ID);
            Assert.AreEqual(3, flow_testObject.ManagerFlowList.Count);
        }

        [Test]
        public void Update_CountValidateTest()
        {
            try
            {
                List<ManagerProxy> mngFlows = new List<ManagerProxy>();
                busflow.UpdateManagerFlows(ADOFlow1.ID, false, false, mngFlows);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.FlowMustHaveOneManagerFlow));
            }
        }
        
        [Test]
        public void GetDepartmentChilds_Test()
        {
            IList<Department> list = busflow.GetDepartmentChilds(ADORoot.ID, ADOFlow1.ID);
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADODepartment1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADODepartment2.ID).Count() == 0);
        }

        [Test]
        public void GetDepartmentPerson_Test()
        {
            IList<Person> list = busflow.GetDepartmentPerson(ADODepartment1.ID, ADOFlow1.ID);
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson3.ID).Count() == 1);
            list = busflow.GetDepartmentPerson(ADODepartment2.ID, ADOFlow1.ID);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Delete_Test() 
        {
            try
            {
                busflow.SaveChanges(ADOFlow1, UIActionType.DELETE);
                ClearSession();
                busflow.GetByID(ADOFlow1.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void SearchFlow_FlowNameTest() 
        {
            dataAccessFlowTA.Insert(BUser.CurrentUser.ID, ADOFlow1.ID, false);

            IList<Flow> list = busflow.SearchFlow(FlowSearchFields.FlowName, ADOFlow1.FlowName);
            Assert.IsTrue(list.Where(x => x.ID == ADOFlow1.ID).Count() > 0);
        }

        [Test]
        public void SearchFlow_AccessGroupNameTest()
        {
            dataAccessFlowTA.Insert(BUser.CurrentUser.ID, ADOFlow1.ID, false);

            IList<Flow> list = busflow.SearchFlow(FlowSearchFields.AccessGroupName, ADOAccessGroup1.Name);
            Assert.IsTrue(list.Where(x => x.ID == ADOFlow1.ID).Count() > 0);
        }

        [Test]
        public void GetManagerFlow_Test() 
        {
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, null, true);

            IList<Manager> list = busflow.GetManagerFlow(ADOFlow1.ID);
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void GetManagerFlow_AfterUpdate_Test()
        {
            dataAccessMngTA.Insert(BUser.CurrentUser.ID, null, true);

            ManagerProxy mf = new ManagerProxy();
            mf.Level = 3;
            mf.OwnerID = ADOManager2.OrganizationUnit.ID;
            mf.ManagerType = ManagerType.OrganizationUnit;
            List<ManagerProxy> mngFlows = new List<ManagerProxy>();
            mngFlows.Add(mf);
            busflow.UpdateManagerFlows(ADOFlow1.ID, false, false, mngFlows);
            ClearSession();
            flow_testObject = new Flow();

            IList<Manager> list = busflow.GetManagerFlow(ADOFlow1.ID);
            Assert.AreEqual(1, list.Count);            
        }
      
    }
}
