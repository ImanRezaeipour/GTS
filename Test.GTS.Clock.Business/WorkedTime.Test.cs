using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit;
using System.Web;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.MonthlyReport;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class WorkedTimeTest : BaseFixture
    {
        #region variables
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter undermanagmentTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter precardAccessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter managerFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();

        BWorkedTime bussWorkTime;
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        Manager ADOManager3 = new Manager();
        Flow ADOFlow1 = new Flow();
        Flow ADOFlow2 = new Flow();
        Flow ADOFlow3 = new Flow();
        UnderManagment ADOUnderManagment = new UnderManagment();
        OrganizationUnit ADOOrganRoot = new OrganizationUnit();
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        PrecardAccessGroup ADOaccessGroup = new PrecardAccessGroup();

        #endregion

        [SetUp]
        public void TestSetup()
        {

            #region organization unit
            DatabaseGateway.TA_OrganizationUnitDataTable organTable = new DatabaseGateway.TA_OrganizationUnitDataTable();
            organTable = organTA.GetDataByParent();
            ADOOrganRoot.ID = Convert.ToInt32(organTable.Rows[0]["organ_ID"]);
            ADOOrganRoot.Name = Convert.ToString(organTable.Rows[0]["organ_Name"]);
            ADOOrganRoot.CustomCode = Convert.ToString(organTable.Rows[0]["organ_CustomCode"]);

            organTA.Insert("Level2_1", "2020_11", ADOPerson2.ID, ADOOrganRoot.ID, String.Format(",{0},", ADOOrganRoot.ID));
            organTable = organTA.GetDataByCustomCode("2020_11");
            ADOOrgan.ID = Convert.ToInt32(organTable.Rows[0]["organ_ID"]);
            ADOOrgan.Name = Convert.ToString(organTable.Rows[0]["organ_Name"]);
            ADOOrgan.CustomCode = Convert.ToString(organTable.Rows[0]["organ_CustomCode"]);
            #endregion

            #region managers
            managerTA.Insert(ADOPerson1.ID, null);
            DatasetGatewayWorkFlow.TA_ManagerDataTable masterTable = managerTA.GetDataByPersonID(ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(masterTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;
            ADOManager1.OrganizationUnit = null;

            managerTA.Insert(null, ADOOrgan.ID);
            masterTable = managerTA.GetDataByOrganID(ADOOrgan.ID);
            ADOManager2.ID = Convert.ToInt32(masterTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.Person = null;
            ADOManager2.OrganizationUnit = ADOOrgan;

            managerTA.Insert(ADOPerson3.ID, null);
            masterTable = managerTA.GetDataByPersonID(ADOPerson3.ID);
            ADOManager3.ID = Convert.ToInt32(masterTable.Rows[0]["MasterMng_ID"]);
            ADOManager3.Person = ADOPerson3;
            ADOManager3.OrganizationUnit = null;
            #endregion

            #region pishcart access group
            precardAccessGroupTA.Insert("PrecardAccessGroupTest");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = precardAccessGroupTA.GetDataBy1("PrecardAccessGroupTest");
            ADOaccessGroup.ID = Convert.ToInt32(accessTable.Rows[0]["accessGrp_ID"]);
            ADOaccessGroup.Name = Convert.ToString(accessTable.Rows[0]["accessGrp_Name"]);
            #endregion


            #region Flow
            flowTA.Insert(ADOaccessGroup.ID, false, false, "FlowTest1");
            DatasetGatewayWorkFlow.TA_FlowDataTable flowTable = flowTA.GetDataByName("FlowTest1");
            ADOFlow1.ID = Convert.ToInt32(flowTable.Rows[0]["flow_ID"]);
            ADOFlow1.AccessGroup = ADOaccessGroup;
            ADOFlow1.ActiveFlow = false;
            ADOFlow1.WorkFlow = false;
            ADOFlow1.FlowName = "FlowTest1";

            flowTA.Insert(ADOaccessGroup.ID, false, false, "FlowTest2");
            flowTable = flowTA.GetDataByName("FlowTest2");
            ADOFlow2.ID = Convert.ToInt32(flowTable.Rows[0]["flow_ID"]);
            ADOFlow2.AccessGroup = ADOaccessGroup;
            ADOFlow2.ActiveFlow = false;
            ADOFlow2.WorkFlow = false;
            ADOFlow2.FlowName = "FlowTest2";

            flowTA.Insert(ADOaccessGroup.ID, false, false, "FlowTest3");
            flowTable = flowTA.GetDataByName("FlowTest3");
            ADOFlow3.ID = Convert.ToInt32(flowTable.Rows[0]["flow_ID"]);
            ADOFlow3.AccessGroup = ADOaccessGroup;
            ADOFlow3.ActiveFlow = false;
            ADOFlow3.WorkFlow = false;
            ADOFlow3.FlowName = "FlowTest3";
            #endregion

            #region manager Flow
            managerFlowTA.Insert(ADOManager1.ID, 1, ADOFlow1.ID, true);
            managerFlowTA.Insert(ADOManager1.ID, 2, ADOFlow2.ID, true);
            managerFlowTA.Insert(ADOManager3.ID, 2, ADOFlow3.ID, true);
            #endregion

            #region under managment
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson2.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson5.ID, ADODepartment1.ID, false, true);
            DatasetGatewayWorkFlow.TA_UnderManagmentDataTable underManagmentTable = new DatasetGatewayWorkFlow.TA_UnderManagmentDataTable();
            underManagmentTable = undermanagmentTA.GetDataByManagmentID(ADOFlow1.ID);
            ADOUnderManagment.ID = Convert.ToInt32(underManagmentTable.Rows[0]["underMng_ID"]);
            ADOUnderManagment.Contains = true;
            ADOUnderManagment.ContainInnerChilds = false;
            ADOUnderManagment.Person = ADOPerson2;
            ADOUnderManagment.Flow = ADOFlow1;

            undermanagmentTA.Insert(ADOFlow2.ID, ADOPerson4.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow3.ID, ADOPerson5.ID, null, false, true);

            #endregion



            bussWorkTime = new BWorkedTime(ADOUser1.UserName);
        }

        [TearDown]
        public void TreatDown()
        {
            managerTA.DeleteByBarcode(ADOPerson1.PersonCode);
            managerTA.DeleteByBarcode(ADOPerson3.PersonCode);
            managerTA.DeleteByOrganCustomCode(ADOOrgan.CustomCode);
            organTA.DeleteByCustomCode("2020_11");
            managerFlowTA.DeleteQuery(ADOFlow1.ID);
            managerFlowTA.DeleteQuery(ADOFlow2.ID);
            flowTA.DeleteByName("FlowTest1");
            flowTA.DeleteByName("FlowTest2");
            flowTA.DeleteByName("FlowTest3");


            precardAccessGroupTA.DeleteByName(ADOaccessGroup.Name);
        }

        [Test]
        public void GetDepartmentTreeTest()
        {
            Department dep = bussWorkTime.GetManagerDepartmentTree();
            Assert.IsTrue(dep.Visible);
            Assert.IsTrue(dep.ChildList.Where(x => x.ID == ADODepartment1.ID).First().Visible);
            if (dep.ChildList.Count > 1)
            {
                Assert.IsFalse(dep.ChildList.Where(x => x.ID != ADODepartment1.ID).First().Visible);
            }
        }

        [Test]
        public void GetDepartmentTree_DepartmentNullTest()
        {
            try
            {
                bussWorkTime = new BWorkedTime(ADOUser3.UserName);
                Department dep = bussWorkTime.GetManagerDepartmentTree();

                Assert.Fail("بخش برای افراد تحت مدیریت نباید تهی باشد");
            }
            catch (InvalidDatabaseStateException ex)
            {
                Assert.AreEqual(ex.FatalExceptionIdentifier, UIFatalExceptionIdentifiers.UnderManagmentDepartmentNull);
            }
        }

        [Test]
        public void GetDepartmentTree_PersonNotManagerTest()
        {
            try
            {
                bussWorkTime = new BWorkedTime(ADOUser4.UserName);
                Department dep = bussWorkTime.GetManagerDepartmentTree();

                Assert.Fail("فقط مدیران میتوانند به این سرویس دسترسی داشته باشند");
            }
            catch (IllegalServiceAccess ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void GetUnderManagmentByDepartment_Test()
        {
            IList<UnderManagementPerson> list = bussWorkTime.GetUnderManagmentByDepartment(0, ADODepartment1.ID, 0, 10, GridOrderFields.gridFields_BarCode, GridOrderFieldType.desc);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Where(x => x.PersonId == ADOPerson5.ID).Count() > 0);
            Assert.IsTrue(list.Count == 3);
        }

        [Test]
        public void GetUnderManagmentByDepartment_PropertyTest()
        {
            IList<UnderManagementPerson> list = bussWorkTime.GetUnderManagmentByDepartment(0, ADODepartment1.ID, 0, 10, GridOrderFields.gridFields_BarCode, GridOrderFieldType.desc);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list[0].DailyAbsence != null);
        }

        [Test]
        public void GetUnderManagmentByDepartment_NameFillTest()
        {
            IList<UnderManagementPerson> list = bussWorkTime.GetUnderManagmentByDepartment(0, ADODepartment1.ID, 0, 10, GridOrderFields.gridFields_BarCode, GridOrderFieldType.desc);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Where(x => x.PersonId == ADOPerson5.ID).Count() > 0);
            Assert.IsNotNullOrEmpty(list[0].PersonName, "نام نباید خالی باشد");
        }

        [Test]
        public void GetUnderManagmentBySearch_BarcodeTest()
        {

            IList<UnderManagementPerson> list = bussWorkTime.GetUnderManagmentBySearch(0, "4", GridSearchFields.PersonCode, 0, 10, GridOrderFields.gridFields_BarCode, GridOrderFieldType.desc);
            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Where(x => x.PersonId == ADOPerson4.ID).Count() > 0);
        }

        [Test]
        public void GetUnderManagmentBySearch_NameTest()
        {
            IList<UnderManagementPerson> list = bussWorkTime.GetUnderManagmentBySearch(0, "ali", GridSearchFields.PersonName, 0, 10, GridOrderFields.gridFields_BarCode, GridOrderFieldType.desc);
            Assert.IsTrue(list != null);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void GetUnderManagmentByDepartmentCount_Test()
        {
            int result = bussWorkTime.GetUnderManagmentByDepartmentCount(0, ADODepartment1.ID);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void GetUnderManagmentBySearchNameCount_Test()
        {
            int result = bussWorkTime.GetUnderManagmentBySearchCount(0, "alian", GridSearchFields.PersonName);
            Assert.IsTrue(result >= 3);
        }

        [Test]
        public void GetUnderManagmentBySearchBarcodeCount_Test()
        {
            int result = bussWorkTime.GetUnderManagmentBySearchCount(0, "00004", GridSearchFields.PersonCode);
            Assert.IsTrue(result >= 1);
        }

        [Test]
        public void GetUnderManagmentBySearchCountZero_Test()
        {
            int result = bussWorkTime.GetUnderManagmentBySearchCount(0, "125546alian1231", GridSearchFields.PersonName);
            Assert.AreEqual(0, result);
        }


        [Test]
        public void GetUnderManagmentBySearch_BarcodeTest22222()
        {
            try
            {

                base.UpdateCurrentUserPersonId(32678);
                bussWorkTime = new BWorkedTime();


                Department dep = bussWorkTime.GetManagerDepartmentTree();
                IList<UnderManagementPerson> list1 = bussWorkTime.GetUnderManagmentByDepartment(9, dep.ID, 0, 20, GridOrderFields.gridFields_BarCode, GridOrderFieldType.asc);

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

    }
}
