using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.RequestFlow;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RegisteredRequest : BaseSubstituteOperator
    {
        #region Table Adapters
       
        DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter requestTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_DoctorTableAdapter doctorTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_DoctorTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_IllnessTableAdapter illnessTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_IllnessTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_DutyPlaceTableAdapter dutyPlcTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_DutyPlaceTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestDetailTableAdapter requestDetailTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestDetailTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestStatusTableAdapter requestStatusTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestStatusTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter mangFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter accessGroupDtlTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter departmentTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter undermanagmentTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PermitPairTableAdapter permitPairTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PermitPairTableAdapter();
        #endregion

        #region Flow ADOObjects
        BFlow busflow;
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        Flow ADOFlow1 = new Flow();
        Flow ADOFlow2 = new Flow();
        Flow flow_testObject;
        OrganizationUnit ADOOrganParent = new OrganizationUnit();
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        OrganizationUnit ADOOrgan2 = new OrganizationUnit();
        PrecardAccessGroup ADOAccessGroup1 = new PrecardAccessGroup();
        PrecardAccessGroup ADOAccessGroup2 = new PrecardAccessGroup();
        //User ADOUser = new User();
        ManagerFlow ADOmangFlow = new ManagerFlow();
        ManagerFlow ADOManagerFlow1 = new ManagerFlow();
        ManagerFlow ADOManagerFlow2 = new ManagerFlow();
        ManagerFlow ADOManagerFlow3 = new ManagerFlow();
        #endregion

        #region Request ADOObjects
        IHourlyAbsenceBRequest busHourlyAbsenceRequest = new BRequest();
        IDailyAbsenceBRequest busDailyAbsenceRequest = new BRequest();
        IOverTimeBRequest busOverTime = new BRequest();
        ITrafficBRequest busTrafficRequest = new BRequest();
        IDashboardBRequest busPersonelRequest = new BRequest();
        Request request_testObject = new Request();
        Request ADORequestHourlyLeave1 = new Request();
        Request ADORequestHourlyLeave2 = new Request();
        Request ADORequestHourlyDuty1 = new Request();
        Request ADORequestTraffic1 = new Request();
        Request ADORequestDailyLeave1 = new Request();
        Request ADORequestDailyDuty1 = new Request();
        Request ADOOpRequest1 = new Request();
        Request ADOOpRequest2 = new Request(); 
        PrecardGroups ADOPrecardGroup1 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup2 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup3 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup4 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup5 = new PrecardGroups();
        Precard ADOPrecardHourlyLeave1 = new Precard();
        Precard ADOPrecardHourlyLeave2 = new Precard();
        Precard ADOPrecardHourlyDuty1 = new Precard();
        Precard ADOPrecardHourlyEstelji1 = new Precard();
        Precard ADOPrecardTraffic1 = new Precard();
        Precard ADOPrecardDailyLeave1 = new Precard();
        Precard ADOPrecardDailyDuty1 = new Precard();
        Precard ADOPrecardOverTime1 = new Precard();
        Doctor ADODoctor1 = new Doctor();
        Illness ADOIllness1 = new Illness();
        DutyPlace ADODuty1 = new DutyPlace();
        DutyPlace ADODuty2 = new DutyPlace();
        Permit ADOPermit1 = new Permit();
        PermitPair ADOPermitPair1 = new PermitPair();
        #endregion


        IRegisteredRequests busRegisteredRequest;

        [SetUp]
        public void TestSetup()
        {
            busRegisteredRequest = new BKartabl(ADOPerson3.ID, ADOUser3.ID, ADOUser3.UserName);
            busflow = new BFlow();
            flow_testObject = new Flow();

            #region Flow Entry
                       
            
            personTA.UpdateDepartmentId(ADODepartment1.ID, ADOPerson3.ID);
            personTA.UpdateDepartmentId(ADODepartment1.ID, ADOPerson4.ID);
            personTA.UpdateDepartmentId(ADODepartment2.ID, ADOPerson5.ID);
            personTA.UpdateDepartmentId(ADODepartment2.ID, ADOPerson6.ID);           

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
            managerTA.Insert(null, ADOOrgan.ID);//ADOPerson2

            DatasetGatewayWorkFlow.TA_ManagerDataTable managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;

            managerTA.FillByOrganID(managetTable, ADOOrgan.ID);
            ADOManager2.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.OrganizationUnit = ADOOrgan;


            #region precards

            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable();
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leave.ToString());
            ADOPrecardGroup1.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup1.Name = "HourlyLeave";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.duty.ToString());
            ADOPrecardGroup2.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup2.Name = "HourlyDuty";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leaveestelajy.ToString());
            ADOPrecardGroup3.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup3.Name = "Estelaji";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.traffic.ToString());
            ADOPrecardGroup4.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup4.Name = "Traffic";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.overwork.ToString());
            ADOPrecardGroup5.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup5.Name = "OwerWork";


            precardTA.Insert("TestPrecard1", true, ADOPrecardGroup1.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard4", true, ADOPrecardGroup1.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard6", true, ADOPrecardGroup1.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard7", true, ADOPrecardGroup2.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard2", true, ADOPrecardGroup2.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard3", true, ADOPrecardGroup3.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard5", true, ADOPrecardGroup4.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard8", true, ADOPrecardGroup5.ID, true, false, true, "99999999", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable pTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            pTable = precardTA.GetDataByName("TestPrecard1");
            ADOPrecardHourlyLeave1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyLeave1.Name = "TestPrecard1";
            pTable = precardTA.GetDataByName("TestPrecard2");
            ADOPrecardHourlyDuty1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyDuty1.Name = "TestPrecard2";
            pTable = precardTA.GetDataByName("TestPrecard3");
            ADOPrecardHourlyEstelji1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyEstelji1.Name = "TestPrecard3";
            pTable = precardTA.GetDataByName("TestPrecard4");
            ADOPrecardHourlyLeave2.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyLeave2.Name = "TestPrecard4";
            pTable = precardTA.GetDataByName("TestPrecard5");
            ADOPrecardTraffic1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardTraffic1.Name = "TestPrecard6";
            pTable = precardTA.GetDataByName("TestPrecard6");
            ADOPrecardDailyLeave1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardDailyLeave1.Name = "TestPrecard7";
            pTable = precardTA.GetDataByName("TestPrecard7");
            ADOPrecardDailyDuty1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardDailyDuty1.Name = "TestPrecard7";
            pTable = precardTA.GetDataByName("TestPrecard8");
            ADOPrecardOverTime1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardOverTime1.Name = "TestPrecard8";
            #endregion

            accessGroupTA.Insert("AccessGroup1_2");
            accessGroupTA.Insert("AccessGroup1_3");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = accessGroupTA.GetDataBy1("AccessGroup1_2");
            ADOAccessGroup1.ID = Convert.ToInt32(accessTable.Rows[0][0]);
            ADOAccessGroup1.Name = "AccessGroup1_2";
            accessTable = accessGroupTA.GetDataBy1("AccessGroup1_3");
            ADOAccessGroup2.ID = Convert.ToInt32(accessTable.Rows[0][0]);
            ADOAccessGroup2.Name = "AccessGroup1_3";

            accessGroupDtlTA.Insert(ADOAccessGroup1.ID, ADOPrecardHourlyDuty1.ID);
            accessGroupDtlTA.Insert(ADOAccessGroup1.ID, ADOPrecardHourlyLeave1.ID);
            accessGroupDtlTA.Insert(ADOAccessGroup1.ID, ADOPrecardHourlyLeave2.ID);
            accessGroupDtlTA.Insert(ADOAccessGroup2.ID, ADOPrecardDailyLeave1.ID);
            accessGroupDtlTA.Insert(ADOAccessGroup2.ID, ADOPrecardDailyDuty1.ID);

            flowTA.Insert(ADOAccessGroup1.ID, false, false, "FlowTest1");
            flowTA.Insert(ADOAccessGroup2.ID, false, false, "FlowTest2");
            DatasetGatewayWorkFlow.TA_FlowDataTable mangTAble = flowTA.GetDataByName("FlowTest1");
            ADOFlow1.ID = Convert.ToInt32(mangTAble.Rows[0][0]);
            ADOFlow1.FlowName = "FlowTest1";
            ADOFlow1.ActiveFlow = false;
            ADOFlow1.WorkFlow = false;

            mangTAble = flowTA.GetDataByName("FlowTest2");
            ADOFlow2.ID = Convert.ToInt32(mangTAble.Rows[0][0]);
            ADOFlow2.FlowName = "FlowTest2";
            ADOFlow2.ActiveFlow = false;
            ADOFlow2.WorkFlow = false;

            mangFlowTA.Insert(ADOManager1.ID, 1, ADOFlow1.ID, true);//مدیر اولیه
            mangFlowTA.Insert(ADOManager2.ID, 2, ADOFlow1.ID, true);//مدیر ثانویه
            mangFlowTA.Insert(ADOManager1.ID, 1, ADOFlow2.ID, true);//مدیر اولیه

            DatasetGatewayWorkFlow.TA_ManagerFlowDataTable nbgFlowTable = mangFlowTA.GetDataByFlowID(ADOFlow1.ID);
            ADOManagerFlow1.ID = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_ID"]);
            ADOManagerFlow1.Level = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_LEVEL"]);

            ADOManagerFlow2.ID = Convert.ToInt32(nbgFlowTable.Rows[1]["mngrFlow_ID"]);
            ADOManagerFlow2.Level = Convert.ToInt32(nbgFlowTable.Rows[1]["mngrFlow_LEVEL"]);

            nbgFlowTable = mangFlowTA.GetDataByFlowID(ADOFlow2.ID);
            ADOManagerFlow3.ID = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_ID"]);
            ADOManagerFlow3.Level = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_LEVEL"]);

            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson3.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson4.ID, ADODepartment1.ID, false, false);

            undermanagmentTA.Insert(ADOFlow2.ID, null, ADODepartment2.ID, true, true);//ADOPerson4,ADOPerson5
            #endregion

            #region Request Entry
           
            requestTA.Insert(ADOPrecardHourlyLeave1.ID, ADOPerson3.ID, new DateTime(2010, 5, 1), new DateTime(2010, 5, 1), 420, 600, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardHourlyLeave1.ID, ADOPerson3.ID, new DateTime(2010, 5, 2), new DateTime(2010, 5, 2), 900, 1020, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardHourlyDuty1.ID, ADOPerson3.ID, new DateTime(2010, 5, 2), new DateTime(2010, 5, 2), 700, 800, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardTraffic1.ID, ADOPerson4.ID, new DateTime(2010, 5, 3), new DateTime(2010, 5, 3), 900, 1020, "", DateTime.Now, ADOUser1.ID);
            
            requestTA.Insert(ADOPrecardDailyLeave1.ID, ADOPerson5.ID, new DateTime(2010, 5, 4), new DateTime(2010, 5, 5), 0, 0, "", DateTime.Now, ADOUser3.ID);
            requestTA.Insert(ADOPrecardDailyDuty1.ID, ADOPerson6.ID, new DateTime(2010, 5, 6), new DateTime(2010, 5, 7), 0, 0, "", DateTime.Now, ADOUser3.ID);

            DatasetGatewayWorkFlow.TA_RequestDataTable requestTable = new DatasetGatewayWorkFlow.TA_RequestDataTable();
            requestTable = requestTA.GetDataByPersonId(ADOPerson3.ID);
            ADORequestHourlyLeave1.ID = Convert.ToInt32(requestTable.Rows[0][0]);
            ADORequestHourlyLeave2.ID = Convert.ToInt32(requestTable.Rows[1][0]);
            ADORequestHourlyDuty1.ID = Convert.ToInt32(requestTable.Rows[2][0]);
            requestTable = requestTA.GetDataByPersonId(ADOPerson4.ID);
            ADORequestTraffic1.ID = Convert.ToInt32(requestTable.Rows[0][0]);
            requestTable = requestTA.GetDataByPersonId(ADOPerson5.ID);
            ADORequestDailyLeave1.ID = Convert.ToInt32(requestTable.Rows[0][0]);
            ADORequestDailyLeave1.FromDate = Convert.ToDateTime(requestTable.Rows[0]["request_fromdate"]);
            requestTable = requestTA.GetDataByPersonId(ADOPerson6.ID);
            ADORequestDailyDuty1.ID = Convert.ToInt32(requestTable.Rows[0][0]);

            #region Request Status
            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyDuty1.ID, false, true, "", DateTime.Now, false);

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyLeave2.ID, true, false, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow2.ID, ADORequestHourlyLeave2.ID, true, true, "", DateTime.Now, false);

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyLeave1.ID, true, false, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow2.ID, ADORequestHourlyLeave1.ID, false, true, "", DateTime.Now, false);

            requestStatusTA.Insert(ADOManagerFlow3.ID, ADORequestDailyDuty1.ID, true, true, "", DateTime.Now, false);

            //requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestTraffic1.ID, true, false, "", DateTime.Now);
            
            #endregion

            #endregion

            #region Operator
            operatorTA.Insert(ADOPerson1.ID, true, ADOFlow1.ID, "");
            operatorTA.Insert(ADOPerson6.ID, true, ADOFlow2.ID, "");

            DatasetGatewayWorkFlow.TA_OperatorDataTable opTable = operatorTA.GetByPesonId(ADOPerson1.ID);
            ADOOperator1.ID = (opTable.Rows[0] as DatasetGatewayWorkFlow.TA_OperatorRow).opr_ID;
            ADOOperator1.Person = ADOPerson1;

            opTable = operatorTA.GetByPesonId(ADOPerson6.ID);
            ADOOperator2.ID = (opTable.Rows[0] as DatasetGatewayWorkFlow.TA_OperatorRow).opr_ID;
            ADOOperator2.Person = ADOPerson6;

            #endregion

    
        }

        [TearDown]
        public void TreatDown()
        {
            
            managerTA.DeleteByBarcode("00001");
            doctorTA.DeleteByLastName("TestDoctorLastName1");
            illnessTA.DeleteByName("TestIllness1");
            requestTA.DeleteByPerson(ADOPerson3.ID);
            requestTA.DeleteByPerson(ADOPerson4.ID);
            requestTA.DeleteByPerson(ADOPerson5.ID);
            requestTA.DeleteByPerson(ADOPerson6.ID);
            precardTA.DeleteByID("99999999");           
            dutyPlcTA.DeleteByName("TestDutyPlc1");
            dutyPlcTA.DeleteByName("TestDutyPlc2");

            flowTA.DeleteByName("FlowTest1");
            flowTA.DeleteByName("FlowTest2");
            flowTA.DeleteByName("InsertedFlow");
            accessGroupTA.DeleteByName("AccessGroup1_2");
            accessGroupTA.DeleteByName("AccessGroup1_3");
            userTA.DeleteByUsername("ADOPerson1");
            userTA.DeleteByUsername("TestUserName1");
            userTA.DeleteByUsername("TestUserName2");
           
            organTA.DeleteByCustomCode("0-1");
            organTA.DeleteByCustomCode("0-2");
      


        }

        [Test]
        public void GetRequestCount_Test()
        {
            try
            {
                int count = busRegisteredRequest.GetUserRequestCount(RequestState.UnKnown, 1389, 2);
                Assert.AreEqual(3, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequestCount_Confirm_Test()
        {
            try
            {
                int count = busRegisteredRequest.GetUserRequestCount(RequestState.Confirmed, 1389, 2);
                Assert.AreEqual(1, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequestCount_UnConfirm_Test()
        {
            try
            {
                int count = busRegisteredRequest.GetUserRequestCount(RequestState.Unconfirmed, 1389, 2);
                Assert.AreEqual(2, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequestCount_Filter_Test1() 
        {
            try
            {
                UserRequestFilterProxy proxy = new UserRequestFilterProxy();
                proxy.RequestType = RequestType.Hourly;
                int count = busRegisteredRequest.GetFilterUserRequestsCount(proxy);
                Assert.AreEqual(3, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequestCount_Filter_Test2()
        {
            try
            {
                busRegisteredRequest = new BKartabl(ADOPerson5.ID,ADOUser5.ID, ADOUser5.UserName);
                UserRequestFilterProxy proxy = new UserRequestFilterProxy();
                proxy.RequestType = RequestType.Daily;
                int count = busRegisteredRequest.GetFilterUserRequestsCount(proxy);
                Assert.AreEqual(1, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequestCount_Filter_Test3()
        {
            try
            {
                busRegisteredRequest = new BKartabl(ADOPerson3.ID, ADOUser3.ID, ADOUser3.UserName);
                UserRequestFilterProxy proxy = new UserRequestFilterProxy();
                proxy.ToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                int count = busRegisteredRequest.GetFilterUserRequestsCount(proxy);
                Assert.AreEqual(1, count);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        ///////////

        [Test]
        public void GetRequest_Test()
        {
            try
            {
                IList<KartablProxy> list = busRegisteredRequest.GetAllUserRequests(RequestState.UnKnown, 1389, 2, 0, 10);
                Assert.AreEqual(3, list.Count);
               
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave1.ID).Count() == 1);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave1.ID).First().FlowStatus == RequestState.Unconfirmed);

                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave2.ID).Count() == 1);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave2.ID).First().FlowStatus == RequestState.Confirmed);

                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty1.ID).Count() == 1);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty1.ID).First().FlowStatus == RequestState.Unconfirmed);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequest_Confirm_Test()
        {
            try
            {
                IList<KartablProxy> list = busRegisteredRequest.GetAllUserRequests(RequestState.Confirmed, 1389, 2, 0, 10);
                Assert.AreEqual(1, list.Count);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave2.ID).Count() == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequest_UnConfirm_Test()
        {
            try
            {
                IList<KartablProxy> list = busRegisteredRequest.GetAllUserRequests(RequestState.Unconfirmed, 1389, 2, 0, 10);
                Assert.AreEqual(2, list.Count);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave1.ID).Count() == 1);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty1.ID).Count() == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequest_Filter_Test1()
        {
            try
            {
                UserRequestFilterProxy proxy = new UserRequestFilterProxy();
                proxy.RequestType = RequestType.Hourly;
                IList<KartablProxy> list = busRegisteredRequest.GetFilterUserRequests(proxy, 0, 10);
                Assert.AreEqual(3, list.Count);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave1.ID).Count()==1);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave2.ID).Count() == 1);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty1.ID).Count() == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequest_Filter_Test2()
        {
            try
            {
                busRegisteredRequest = new BKartabl(ADOPerson5.ID, ADOUser5.ID, ADOUser5.UserName);
                UserRequestFilterProxy proxy = new UserRequestFilterProxy();
                proxy.RequestType = RequestType.Daily;
                IList<KartablProxy> list = busRegisteredRequest.GetFilterUserRequests(proxy, 0, 10);
                Assert.AreEqual(1, list.Count);
                Assert.IsTrue(list.Where(x => x.ID == ADORequestDailyLeave1.ID).Count() == 1);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetRequest_Filter_Test3()
        {
            try
            {
                busRegisteredRequest = new BKartabl(ADOPerson3.ID, ADOUser3.ID, ADOUser3.UserName);
                UserRequestFilterProxy proxy = new UserRequestFilterProxy();
                proxy.ToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                IList<KartablProxy> list = busRegisteredRequest.GetFilterUserRequests(proxy, 0, 10);
                Assert.AreEqual(1, list.Count);
                Assert.IsTrue(list.Where(x => x.RequestID == ADORequestHourlyLeave1.ID).Count() == 1);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Test222222222222() 
        {
            ClearSession();
            base.UpdateCurrentUserPersonId(32688);

            BKartabl b = new BKartabl();
            busRegisteredRequest = new BKartabl();
            UserRequestFilterProxy proxy = new UserRequestFilterProxy();
            proxy.RequestType = RequestType.Daily;
            IList<KartablProxy> list = busRegisteredRequest.GetAllUserRequests(RequestState.UnKnown,1390,12,0,14);
            list = busRegisteredRequest.GetFilterUserRequests(proxy, 0, 10);
            busRegisteredRequest.GetAllUserRequests(RequestState.UnKnown, 1390, 10, 0, 12);
            busRegisteredRequest.GetAllUserRequests(RequestState.UnKnown, 1390, 10, 1, 12);

        }

        //////////////////
       

        [Test]
        public void Insert_Test()
        {
            try
            {
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2011, 12, 24));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2011, 12, 24));
                request_testObject.TheFromTime = "10:00";
                request_testObject.TheToTime = "11:00";
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave1.ID };

                busRegisteredRequest.InsertRequest(request_testObject, 1390, 10);
                ClearSession();
                Assert.IsTrue(request_testObject.ID > 0);
                Request request = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(request.ID, request_testObject.ID);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
            finally
            {
                ClearSession();
                busRegisteredRequest.DeleteRequest(request_testObject.ID);
            }
        }

        


        #region Operator

        [Test]
        public void Operator_GetRequestCount1() 
        {
            busRegisteredRequest = new BKartabl(ADOPerson1.ID, ADOUser1.ID, ADOUser1.UserName);

            int count = busRegisteredRequest.GetUserRequestCount(RequestState.UnKnown, 1389, 2);
            Assert.AreEqual(3, count);
        }
       
        [Test]
        public void Operator_GetRequestCount2()
        {
            busRegisteredRequest = new BKartabl(ADOPerson6.ID, ADOUser6.ID, ADOUser6.UserName);

            int count = busRegisteredRequest.GetUserRequestCount(RequestState.UnKnown, 1389, 2);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void Operator_GetAllRequest()
        {
            busRegisteredRequest = new BKartabl(ADOPerson6.ID, ADOUser6.ID, ADOUser6.UserName);

            IList<KartablProxy> list= busRegisteredRequest.GetAllUserRequests(RequestState.UnKnown, 1389, 2, 0, 10);
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Where(x => x.RequestID == ADORequestDailyLeave1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.RequestID == ADORequestDailyDuty1.ID).Count() == 1);
        }

        [Test]
        public void Operator_GetFilterRequestCount1()
        {
            busRegisteredRequest = new BKartabl(ADOPerson1.ID, ADOUser1.ID, ADOUser1.UserName);
            UserRequestFilterProxy proxy = new UserRequestFilterProxy();
            proxy.ToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
            int count = busRegisteredRequest.GetFilterUserRequestsCount(proxy);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Operator_GetFilterRequest1()
        {
            busRegisteredRequest = new BKartabl(ADOPerson1.ID, ADOUser1.ID, ADOUser1.UserName);
            UserRequestFilterProxy proxy = new UserRequestFilterProxy();
            proxy.ToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
            IList<KartablProxy> list = busRegisteredRequest.GetFilterUserRequests(proxy, 0, 10);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(ADORequestHourlyLeave1.ID, list.First().RequestID);
        }

        [Test]
        public void Operator_Test2222222() 
        {
            UpdateCurrentUserPersonId(32682);
            busRegisteredRequest = new BKartabl();
            busRegisteredRequest.GetAllUserRequests(RequestState.Deleted, 1390, 12, 0, 20);
            //request_testObject.TheFromDate = "1390/12/01";
            //request_testObject.TheToDate = "1390/12/29";
            //request_testObject.Precard = new Precard() { ID = 61 };

            //busRegisteredRequest.InsertCollectiveRequest(request_testObject, new List<decimal>(), 1390, 11);
        }

        [Test]
        public void Operator_InsertRequest_OnePerson_Test() 
        {

            try
            {
                int count1 = requestTA.GetDataByPersonId(ADOPerson3.ID).Rows.Count;

                busRegisteredRequest = new BKartabl(ADOPerson1.ID, ADOUser1.ID, ADOUser1.UserName);

                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2011, 12, 24));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2011, 12, 24));
                request_testObject.TheFromTime = "10:00";
                request_testObject.TheToTime = "11:00";
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave1.ID };

                busRegisteredRequest.InsertRequest(request_testObject, 1390, 10, ADOPerson3.ID);
                ClearSession();
                Assert.IsTrue(request_testObject.ID > 0);
                Request request = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(request.ID, request_testObject.ID);
                int count2 = requestTA.GetDataByPersonId(ADOPerson3.ID).Rows.Count;
                Assert.AreEqual(count1 + 1, count2);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
            finally
            {
                ClearSession();
                busRegisteredRequest.DeleteRequest(request_testObject.ID);
            }
        }
        

        #endregion
  
    }
}
