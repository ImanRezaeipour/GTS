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
    public class RequestTest : BaseFixture
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
        #endregion

        #region ADOObjects
        IHourlyAbsenceBRequest busHourlyAbsenceRequest = new BRequest();
        IDailyAbsenceBRequest busDailyAbsenceRequest = new BRequest();
        IOverTimeBRequest busOverTime = new BRequest();
        ITrafficBRequest busTrafficRequest = new BRequest();
        IDashboardBRequest busPersonelRequest = new BRequest();
        Request request_testObject = new Request();
        Request ADORequestHourlyLeave1 = new Request();
        Request ADORequestHourlyLeave2 = new Request();
        Request ADORequestTraffic1 = new Request();
        Request ADORequestDailyLeave1 = new Request();
        Request ADORequestDailyDuty1 = new Request();

        Request ADORequestLeavePerson2_1 = new Request();
        Request ADORequestLeavePerson2_2 = new Request();
        Request ADORequestDutyPerson2_1 = new Request();

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
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        Manager ADOManager3 = new Manager();
        Flow ADOFlow1 = new Flow();
        Flow ADOFlow2 = new Flow();
        ManagerFlow ADOManagerFlow1 = new ManagerFlow();
        ManagerFlow ADOManagerFlow2 = new ManagerFlow();
        ManagerFlow ADOManagerFlow3 = new ManagerFlow();
        PrecardAccessGroup ADOAccessGroup1 = new PrecardAccessGroup();
        #endregion

        [SetUp]
        public void TestSetup()
        {

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

            requestTA.Insert(ADOPrecardHourlyLeave1.ID, ADOPerson1.ID, new DateTime(2010, 5, 1), new DateTime(2010, 5, 1), 420, 600, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardHourlyLeave1.ID, ADOPerson1.ID, new DateTime(2010, 5, 1), new DateTime(2010, 5, 1), 900, 1020, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardTraffic1.ID, ADOPerson1.ID, new DateTime(2010, 5, 1), new DateTime(2010, 5, 1), 900, 1020, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardDailyLeave1.ID, ADOPerson1.ID, new DateTime(2010, 5, 2), new DateTime(2010, 5, 6), 0, 0, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardDailyDuty1.ID, ADOPerson1.ID, new DateTime(2010, 5, 7), new DateTime(2010, 5, 7), 0, 0, "", DateTime.Now, ADOUser1.ID);

            requestTA.Insert(ADOPrecardHourlyLeave1.ID, ADOPerson5.ID, new DateTime(2010, 5, 7), new DateTime(2010, 5, 7), 420, 600, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardHourlyLeave1.ID, ADOPerson5.ID, new DateTime(2010, 5, 7), new DateTime(2010, 5, 7), 800, 900, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardHourlyDuty1.ID, ADOPerson5.ID, new DateTime(2010, 5, 8), new DateTime(2010, 5, 8), 100, 300, "", DateTime.Now, ADOUser1.ID);

            DatasetGatewayWorkFlow.TA_RequestDataTable requestTable = new DatasetGatewayWorkFlow.TA_RequestDataTable();
            requestTable = requestTA.GetDataByPersonId(ADOPerson1.ID);
            ADORequestHourlyLeave1.ID = Convert.ToInt32(requestTable.Rows[0][0]);
            ADORequestHourlyLeave2.ID = Convert.ToInt32(requestTable.Rows[1][0]);
            ADORequestTraffic1.ID = Convert.ToInt32(requestTable.Rows[2][0]);
            ADORequestDailyLeave1.ID = Convert.ToInt32(requestTable.Rows[3][0]);
            ADORequestDailyDuty1.ID = Convert.ToInt32(requestTable.Rows[4][0]);

            requestTable = requestTA.GetDataByPersonId(ADOPerson5.ID);

            ADORequestLeavePerson2_1.ID = Convert.ToInt32(requestTable.Rows[0][0]);
            ADORequestLeavePerson2_2.ID = Convert.ToInt32(requestTable.Rows[1][0]);
            ADORequestDutyPerson2_1.ID = Convert.ToInt32(requestTable.Rows[2][0]);

            #region Base Information
            doctorTA.Insert("TestDoctor1", "TestDoctorLastName1", "", "", "");
            illnessTA.Insert("TestIllness1", "");
            dutyPlcTA.Insert("TestDutyPlc1", "0-0", 0);


            DatasetGatewayWorkFlow.TA_DoctorDataTable doctorTable = doctorTA.GetDataByName("TestDoctorLastName1");
            DatasetGatewayWorkFlow.TA_IllnessDataTable illnessTable = illnessTA.GetDataByName("TestIllness1");
            DatasetGatewayWorkFlow.TA_DutyPlaceDataTable dutyPlaceTable = dutyPlcTA.GetDataByName("TestDutyPlc1");

            ADODoctor1.ID = Convert.ToInt32(doctorTable.Rows[0][0]);
            ADODoctor1.FirstName = "TestDoctor1";
            ADODoctor1.LastName = "TestDoctorLastName1";
            ADOIllness1.ID = Convert.ToInt32(illnessTable.Rows[0][0]);
            ADOIllness1.Name = "TestIllness1";
            ADODuty1.ID = Convert.ToInt32(dutyPlaceTable.Rows[0][0]);
            ADODuty1.Name = "TestDutyPlc1";

            dutyPlcTA.Insert("TestDutyPlc2", "0-0", ADODuty1.ID);
            dutyPlaceTable = dutyPlcTA.GetDataByName("TestDutyPlc2");
            ADODuty2.ID = Convert.ToInt32(dutyPlaceTable.Rows[0][0]);
            ADODuty2.Name = "TestDutyPlc2";


            #endregion

            #region Manager Flow
        
            #region MAnager

            managerTA.Insert(ADOPerson1.ID, null);

            DatasetGatewayWorkFlow.TA_ManagerDataTable managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;

            managerTA.Insert(ADOPerson3.ID, null);

            managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson3.ID);
            ADOManager2.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.Person = ADOPerson3;

            managerTA.Insert(ADOPerson4.ID, null);

            managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson4.ID);
            ADOManager3.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager3.Person = ADOPerson4; 
            #endregion

            accessGroupTA.Insert("AccessGroup1_2");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = accessGroupTA.GetDataBy1("AccessGroup1_2");
            ADOAccessGroup1.ID = Convert.ToInt32(accessTable.Rows[0][0]);
            ADOAccessGroup1.Name = "AccessGroup1_2";

            flowTA.Insert(ADOAccessGroup1.ID, false, false, "FlowTest1");
            DatasetGatewayWorkFlow.TA_FlowDataTable mangTAble = flowTA.GetDataByName("FlowTest1");
            ADOFlow1.ID = Convert.ToInt32(mangTAble.Rows[0][0]);
            ADOFlow1.FlowName = "FlowTest1";
            ADOFlow1.ActiveFlow = false;
            ADOFlow1.WorkFlow = false;

            flowTA.Insert(ADOAccessGroup1.ID, false, false, "FlowTest2");
            mangTAble = flowTA.GetDataByName("FlowTest2");
            ADOFlow2.ID = Convert.ToInt32(mangTAble.Rows[0][0]);
            ADOFlow2.FlowName = "FlowTest2";
            ADOFlow2.ActiveFlow = true;
            ADOFlow2.WorkFlow = true;

            mangFlowTA.Insert(ADOManager1.ID, 1, ADOFlow1.ID, true);

            mangFlowTA.Insert(ADOManager2.ID, 1, ADOFlow2.ID, true);
            mangFlowTA.Insert(ADOManager3.ID, 1, ADOFlow2.ID, true);


            DatasetGatewayWorkFlow.TA_ManagerFlowDataTable nbgFlowTable = mangFlowTA.GetDataByFlowID(ADOFlow1.ID);
            ADOManagerFlow1.ID = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_ID"]);
            ADOManagerFlow1.Level = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_LEVEL"]);

            nbgFlowTable = mangFlowTA.GetDataByFlowID(ADOFlow2.ID);
            ADOManagerFlow2.ID = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_ID"]);
            ADOManagerFlow2.Level = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_LEVEL"]);
            ADOManagerFlow3.ID = Convert.ToInt32(nbgFlowTable.Rows[1]["mngrFlow_ID"]);
            ADOManagerFlow3.Level = Convert.ToInt32(nbgFlowTable.Rows[1]["mngrFlow_LEVEL"]);

            #endregion

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyLeave1.ID, true, false, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyLeave2.ID, true, true, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestDailyDuty1.ID, false, true, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestDailyLeave1.ID, false, false, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestTraffic1.ID, true, false, "", DateTime.Now, false);

            requestStatusTA.Insert(ADOManagerFlow2.ID, ADORequestLeavePerson2_1.ID, true, false, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow3.ID, ADORequestLeavePerson2_1.ID, true, true, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow3.ID, ADORequestLeavePerson2_2.ID, true, false, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow3.ID, ADORequestLeavePerson2_2.ID, false, true, "", DateTime.Now, false);
            requestStatusTA.Insert(ADOManagerFlow3.ID, ADORequestDutyPerson2_1.ID, true, false, "", DateTime.Now, false);

            request_testObject = new Request();
            busHourlyAbsenceRequest = new BRequest(ADOPerson1.ID);
            busDailyAbsenceRequest = new BRequest(ADOPerson1.ID);
            busTrafficRequest = new BRequest(ADOPerson1.ID);
            busOverTime = new BRequest(ADOPerson1.ID);
            busPersonelRequest = new BRequest(ADOPerson1.ID);
        }

        [TearDown]
        public void TreatDown()
        {
            flowTA.DeleteByName("FlowTest1");
            flowTA.DeleteByName("FlowTest2");
            managerTA.DeleteByBarcode("00001");
            accessGroupTA.DeleteByName("AccessGroup1_2");
            doctorTA.DeleteByLastName("TestDoctorLastName1");
            illnessTA.DeleteByName("TestIllness1");
            requestTA.DeleteByPerson(ADOPerson1.ID);
            requestTA.DeleteByPerson(ADOPerson2.ID);
            requestTA.DeleteByPerson(ADOPerson5.ID);
            precardTA.DeleteByID("99999999");
            dutyPlcTA.DeleteByName("TestDutyPlc1");
            dutyPlcTA.DeleteByName("TestDutyPlc2");
        }

        #region Get All Concepts Detail
        
        [Test]
        public void GetAllHourlyLeaveDutyRequests_Test()
        {
            busHourlyAbsenceRequest = new BRequest(ADOPerson1.ID);
            IList<Request> list = busHourlyAbsenceRequest.GetAllHourlyLeaveDutyRequests("2010/5/1");
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyLeave1.ID).Count() > 0);
        }

        [Test]
        public void GetAllTrafficDetai_Test_Dependent()
        {
            busTrafficRequest = new BRequest();
            IList<MonthlyDetailReportProxy> list = busTrafficRequest.GetAllTrafic("2011/8/25");
            Assert.IsNotNull(list);
            
        }
        
        #endregion

        #region Get All Precard
       
        [Test]
        public void GetAllHourlyLeave_Test()
        {            
            IList<Precard> list = busHourlyAbsenceRequest.GetAllHourlyLeaves();
            Assert.IsTrue(list.Where(x => x.ID == ADOPrecardHourlyLeave1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.IsEstelajy).Count() > 0);
        }

        [Test]
        public void GetAllHourlyDuty_Test()
        {
            IList<Precard> list = busHourlyAbsenceRequest.GetAllHourlyDutis();
            Assert.IsTrue(list.Where(x => x.ID == ADOPrecardHourlyDuty1.ID).Count() > 0);
        }

        [Test]
        public void GetAllTrafficPrecards_Test() 
        {
            IList<Precard> list = busTrafficRequest.GetAllTraffics();
            Assert.IsTrue(list.Where(x => x.ID == ADOPrecardTraffic1.ID).Count() > 0);
        }

        [Test]
        public void GetAllOverWorksTraffics_Test() 
        {
            IList<Precard> list = busOverTime.GetAllOverWorks();
            Assert.IsTrue(list.Where(x => x.ID == ADOPrecardOverTime1.ID).Count() > 0);
        }
        
        #endregion

        #region Get All Request

        [Test]
        public void GetAllDailyLeaveDutyRequests_Test()
        {

            busDailyAbsenceRequest = new BRequest(ADOPerson1.ID);
            //busRequest = new BRequest(32687);
            IList<Request> list = busDailyAbsenceRequest.GetAllDailyLeaveDutyRequests("2010/5/3");
            //IList<Request> list = busRequest.GetAllDailyLeaveDutyRequests("2011/10/16");
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestDailyLeave1.ID).Count() > 0);
        }

        [Test]
        public void GetAllTrafficRequests_Test()
        {
            busTrafficRequest = new BRequest(ADOPerson1.ID);
            IList<Request> list = busTrafficRequest.GetAllTrafficRequests("2010/5/1");
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1, list.Where(x => x.ID == ADORequestTraffic1.ID).Count());
        }

        [Test]
        public void GetAllRequestByPerson_UnderReviewTest1() 
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson1.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.UnderReview);
                Assert.AreEqual(3,count);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void GetAllRequestByPerson_UnderReviewTest2()
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson5.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.UnderReview);
                Assert.AreEqual(1, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void GetAllRequestByPerson_ConfirmedTest1()
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson1.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.Confirmed);
                Assert.AreEqual(1, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void GetAllRequestByPerson_ConfirmedTest2()
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson5.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.Confirmed);
                Assert.AreEqual(1, count);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void GetAllRequestByPerson_UnConfirmedTest1()
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson1.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.Unconfirmed);
                Assert.AreEqual(1, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void GetAllRequestByPerson_UnConfirmedTest2()
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson5.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.Unconfirmed);
                Assert.AreEqual(1, count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void GetAllRequestByPerson_UnKnown()
        {
            ///=>Sys Language is Parsi
            try
            {
                busPersonelRequest = new BRequest(ADOPerson5.ID);
                int count = busPersonelRequest.GetAllRequestCount(1389, 2, RequestState.UnKnown);
                Assert.AreEqual(3, count);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        #endregion

        #region Insert Test

        [Test]
        public void Insert_RepeatValidateTest()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.Fail("درخواست تکراری است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestRepeated));
            }
        }

        [Test]
        public void Insert_RepeatValidateTest_Unconfirmed()
        {
            try
            {
                request_testObject.TheFromTime = "13:20";
                request_testObject.TheToTime = "15:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 7));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 7));
                request_testObject.Person = new Person() { ID = ADOPerson5.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                request_testObject = busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.IsTrue(request_testObject.ID > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [Test]
        public void Insert_TimeValidateTest()
        {
            try
            {
                request_testObject.TheFromTime = "";
                request_testObject.TheToTime = "12:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.Fail("زمان نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestTimeIsNotValid));
            }
        }

        [Test]
        public void Insert_TimeValidateGreaterTest()
        {
            try
            {
                request_testObject.TheFromTime = "15:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.Fail("زمان نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestFromTimeGreaterThanToTime));
            }
        }

        [Test]
        public void Insert_DateValidateGreaterTest()
        {
            try
            {
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 2));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));                
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave2.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.Fail("تاریخ نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestFromToDateNotEqual));
            }
        }

        [Test]
        public void Insert_PrecardValidateTest()
        {
            try
            {
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 2));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard();
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.Fail("پیشکارت نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestPrecardIsEmpty));
            }
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                busHourlyAbsenceRequest = new BRequest();
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyDuty1.ID };
                //request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(ADOPrecardHourlyDuty1.ID, request_testObject.Precard.ID);
                Assert.IsNullOrEmpty(request_testObject.Description);
                Assert.IsTrue(request_testObject.User.ID > 0);

                busHourlyAbsenceRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_InsertMethodTest()
        {
            try
            {
                busHourlyAbsenceRequest = new BRequest();
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyDuty1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                // request_testObject.User = new User() { ID = ADOUser1.ID };

                Request r = busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                Assert.IsTrue(r.ID > 0);
                Assert.IsTrue(r.RegisterDate.Year > 2000);
                Assert.AreEqual(RequestState.UnderReview, r.Status);
                Assert.IsTrue(r.RegistrationDate.Length > 0);

                busHourlyAbsenceRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_DoctorDescriptionTest()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyEstelji1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };
                request_testObject.DoctorID = ADODoctor1.ID;

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.IsNotNullOrEmpty(request_testObject.Description);
                Assert.IsTrue(request_testObject.Description.Contains(ADODoctor1.Name));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_IllnessDescriptionTest()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyEstelji1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };
                request_testObject.IllnessID = ADOIllness1.ID;

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.IsNotNullOrEmpty(request_testObject.Description);
                Assert.IsTrue(request_testObject.Description.Contains(ADOIllness1.Name));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_DoctorIllnessDescriptionTest()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyEstelji1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };
                request_testObject.IllnessID = ADOIllness1.ID;
                request_testObject.DoctorID = ADODoctor1.ID;

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.IsNotNullOrEmpty(request_testObject.Description);
                Assert.IsTrue(request_testObject.Description.Contains(ADOIllness1.Name));
                Assert.IsTrue(request_testObject.Description.Contains(ADODoctor1.Name));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_DutyPlaceDescriptionTest()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyDuty1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };
                request_testObject.DutyPositionID = ADODuty2.ID;

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.IsNotNullOrEmpty(request_testObject.Description);
                Assert.IsTrue(request_testObject.Description.Contains(ADODuty2.Name));
                Assert.IsTrue(request_testObject.Description.Contains(ADODuty1.Name));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_DescriptionManyToManyTest()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyDuty1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };
                request_testObject.DutyPositionID = ADODuty2.ID;

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                int count = (int)requestDetailTA.FillByRequestIDAndPositionId(request_testObject.ID, ADODuty2.ID);
                Assert.AreEqual(1, count);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_FromTodateValidate()
        {
            try
            {
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyLeave2.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                Assert.Fail("تاریخ نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestDateShouldNotEmpty));
            }
        }

        [Test]
        public void Insert_DailyTest() 
        {
            try
            {
                busDailyAbsenceRequest = new BRequest();               
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 12));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Precard = new Precard() { ID = ADOPrecardDailyLeave1.ID };

                busDailyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(ADOPrecardDailyLeave1.ID, request_testObject.Precard.ID);
                Assert.IsTrue(request_testObject.User.ID > 0);

                busDailyAbsenceRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TrafficRequestTest() 
        {
            try
            {
                busTrafficRequest = new BRequest(ADOPerson2.ID);
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Precard = new Precard() { ID = ADOPrecardTraffic1.ID };
                request_testObject.TheFromTime = "08:00";
                request_testObject.TheToTime = "10:00";

                busTrafficRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(ADOPrecardTraffic1.ID, request_testObject.Precard.ID);
                Assert.IsTrue(request_testObject.User.ID > 0);

                busTrafficRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TrafficRequestEmptyPrecardTest()
        {
            try
            {
                busTrafficRequest = new BRequest(ADOPerson2.ID);
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheFromTime = "08:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.Precard = new Precard() { ID = -1 };

                busTrafficRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual("0", request_testObject.Precard.Code);
                Assert.IsTrue(request_testObject.User.ID > 0);

                busTrafficRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TrafficRequestEmptyToTimeTest()
        {
            try
            {
                busTrafficRequest = new BRequest(ADOPerson2.ID);
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheFromTime = "08:00";
                request_testObject.TheToTime = "";
                request_testObject.Precard = new Precard() { ID = ADOPrecardTraffic1.ID };

                busTrafficRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(-1000, request_testObject.ToTime);

                busTrafficRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TrafficRequestEmptyFromTimeTest()
        {
            try
            {
                busTrafficRequest = new BRequest(ADOPerson2.ID);
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheFromTime = "";
                request_testObject.TheToTime = "17:00";
                request_testObject.Precard = new Precard() { ID = ADOPrecardTraffic1.ID };

                busTrafficRequest.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(-1000, request_testObject.FromTime);

                busTrafficRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TrafficRequestEmptyFromToTimeTest()
        {
            try
            {
                busTrafficRequest = new BRequest(ADOPerson2.ID);
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheFromTime = "";
                request_testObject.TheToTime = "";
                request_testObject.Precard = new Precard() { ID = ADOPrecardTraffic1.ID };

                busTrafficRequest.InsertRequest(request_testObject);
                Assert.Fail("زمان خالی است");

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestTimeIsNotValid));
            }
        }

        [Test]
        public void Insert_TimePlusTest()
        {
            try
            {
                busHourlyAbsenceRequest = new BRequest();
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "9:00";
                request_testObject.TimePlusFlag = true;
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.Precard = new Precard() { ID = ADOPrecardHourlyDuty1.ID };
              

                busHourlyAbsenceRequest.InsertRequest(request_testObject);
                ClearSession();
                decimal id=request_testObject.ID;
                IList<Request> list = busHourlyAbsenceRequest.GetAllHourlyLeaveDutyRequests(Utility.ToString(new DateTime(2010, 5, 12)));
                request_testObject = list.Where(x => x.ID == id).First();
                Assert.IsTrue(request_testObject.TheToTime.Contains("+"));

                busHourlyAbsenceRequest.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #endregion


        [Test]
        public void Delete_ValidateTest() 
        {
            try
            {
                busHourlyAbsenceRequest.DeleteRequest(ADORequestHourlyLeave1);
                Assert.Fail("استفاده شده در جریان کاری");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestUsedByFlow));
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busHourlyAbsenceRequest.DeleteRequest(ADORequestDailyLeave1);
                ClearSession();
                request_testObject = new BRequest().GetByID(ADORequestDailyLeave1.ID);
                Assert.Fail("مشکل حذف");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Test22222222() 
        {
            //IPersonelBRequest ip = new BRequest();
            // ip.GetAllRequests(1390, 7, RequestState.UnderReview);
        }
    }
}
