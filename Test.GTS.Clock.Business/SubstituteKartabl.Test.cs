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
    public class SubstituteKartablTest : BaseFixture
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
        DatasetGatewayWorkFlowTableAdapters.TA_SubstituteTableAdapter substituteTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_SubstituteTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_SubstitutePrecardAccessTableAdapter substituteAccessTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_SubstitutePrecardAccessTableAdapter();
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
        Request ADORequestHourlyDuty2 = new Request();
        Request ADORequestHourlyDuty3 = new Request();
        Request ADORequestTraffic1 = new Request();
        Request ADORequestDailyLeave1 = new Request();
        Request ADORequestDailyDuty1 = new Request();   
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
        Substitute ADOSubstitute1 = new Substitute();
        Substitute ADOSubstitute2 = new Substitute();
        Substitute ADOSubstitute3 = new Substitute();
        Substitute ADOSubstitute4 = new Substitute();
       
        #endregion

        IKartablRequests busKartabl = new BKartabl();
        IReviewedRequests busReviewd = new BKartabl();
        [SetUp]
        public void TestSetup()
        {
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
            requestTA.Insert(ADOPrecardHourlyDuty1.ID, ADOPerson3.ID, new DateTime(2010, 5, 10), new DateTime(2010, 5, 10), 700, 800, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardHourlyDuty1.ID, ADOPerson3.ID, new DateTime(2010, 5, 10), new DateTime(2010, 5, 10), 1000, 1100, "", DateTime.Now, ADOUser1.ID);
           
            
            requestTA.Insert(ADOPrecardDailyLeave1.ID, ADOPerson5.ID, new DateTime(2010, 5, 4), new DateTime(2010, 5, 5), 0, 0, "", DateTime.Now, ADOUser1.ID);
            requestTA.Insert(ADOPrecardDailyDuty1.ID, ADOPerson6.ID, new DateTime(2010, 5, 6), new DateTime(2010, 5, 7), 0, 0, "", DateTime.Now, ADOUser1.ID);


            DatasetGatewayWorkFlow.TA_RequestDataTable requestTable = new DatasetGatewayWorkFlow.TA_RequestDataTable();
            requestTable = requestTA.GetDataByPersonId(ADOPerson3.ID);
            ADORequestHourlyLeave1.ID = Convert.ToInt32(requestTable.Rows[0][0]);
            ADORequestHourlyLeave2.ID = Convert.ToInt32(requestTable.Rows[1][0]);
            ADORequestHourlyDuty1.ID = Convert.ToInt32(requestTable.Rows[2][0]);
            ADORequestHourlyDuty2.ID = Convert.ToInt32(requestTable.Rows[3][0]);
            ADORequestHourlyDuty2.FromDate = Convert.ToDateTime(requestTable.Rows[3]["request_fromdate"]);
            ADORequestHourlyDuty3.ID = Convert.ToInt32(requestTable.Rows[4][0]);
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

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyDuty2.ID, true, false, "", DateTime.Now, false);

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestHourlyDuty3.ID, true, false, "", DateTime.Now, false);

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestTraffic1.ID, true, false, "", DateTime.Now, false);
            
            #endregion

            #endregion

            #region Substitute
            substituteTA.Insert(ADOManager1.ID, ADOPerson6.ID, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true);
            DatasetGatewayWorkFlow.TA_SubstituteDataTable subTable = substituteTA.GetByManager(ADOManager1.ID,ADOPerson6.ID);
            ADOSubstitute1.ID = (subTable.Rows[0] as DatasetGatewayWorkFlow.TA_SubstituteRow).sub_ID;

            substituteAccessTA.Insert(ADOPrecardDailyLeave1.ID, ADOSubstitute1.ID);
            substituteAccessTA.Insert(ADOPrecardHourlyDuty1.ID, ADOSubstitute1.ID);

            substituteTA.Insert(ADOManager1.ID, ADOPerson2.ID/*Manager 2*/, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true);

            subTable = substituteTA.GetByManager(ADOManager1.ID,ADOPerson2.ID);
            ADOSubstitute2.ID = (subTable.Rows[0] as DatasetGatewayWorkFlow.TA_SubstituteRow).sub_ID;

            substituteAccessTA.Insert(ADOPrecardDailyLeave1.ID, ADOSubstitute2.ID);

            substituteTA.Insert(ADOManager1.ID, ADOPerson5.ID, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true);
            substituteTA.Insert(ADOManager2.ID, ADOPerson5.ID, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true);

            subTable = substituteTA.GetByManager(ADOManager1.ID, ADOPerson5.ID);
            ADOSubstitute3.ID = (subTable.Rows[0] as DatasetGatewayWorkFlow.TA_SubstituteRow).sub_ID;

            substituteAccessTA.Insert(ADOPrecardDailyLeave1.ID, ADOSubstitute3.ID);

            subTable = substituteTA.GetByManager(ADOManager2.ID, ADOPerson5.ID);
            ADOSubstitute4.ID = (subTable.Rows[0] as DatasetGatewayWorkFlow.TA_SubstituteRow).sub_ID;

            substituteAccessTA.Insert(ADOPrecardHourlyDuty1.ID, ADOSubstitute4.ID);

            #endregion
        }

        [TearDown]
        public void TreatDown()
        {
            substituteTA.DeleteByManagerId(ADOManager1.ID);
            substituteTA.DeleteByManagerId(ADOManager2.ID);
            
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
        public void GetCount_Simple_Test() 
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);
            int count = busKartabl.GetRequestCount(RequestType.None, 1389, 2);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetAllRequest_Simple_Test()
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);
            IList<KartablProxy> list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 20, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(ADORequestDailyLeave1.ID, list.First().RequestID);
        }

        [Test]
        public void GetCount_SubstituteIsManager_Test() 
        {
            busKartabl = new BKartabl(ADOPerson2.ID,ADOUser2.ID, ADOUser2.UserName);
            int count = busKartabl.GetRequestCount(RequestType.None, 1389, 2);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void GetAllRequest_SubstituteIsManager_Test()
        {
            busKartabl = new BKartabl(ADOPerson2.ID,ADOUser2.ID, ADOUser2.UserName);
            IList<KartablProxy> list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 20, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestDailyLeave1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty2.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty3.ID).Count() == 1);
        }

        [Test]
        public void GetCount_SubstituteFor2Manager_Test()
        {
            busKartabl = new BKartabl(ADOPerson5.ID,ADOUser5.ID, ADOUser5.UserName);
            int count = busKartabl.GetRequestCount(RequestType.None, 1389, 2);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void GetAllRequest_SubstituteFor2Manager_Test()
        {
            busKartabl = new BKartabl(ADOPerson5.ID,ADOUser5.ID, ADOUser5.UserName);
            IList<KartablProxy> list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 20, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestDailyLeave1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty2.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADORequestHourlyDuty3.ID).Count() == 1);
        }

        [Test]
        public void GetAllRequest_QuickSearch_SubstituteFor2Manager_Test()
        {
            UpdateCurrentUserPersonId(ADOPerson5.ID);

            busKartabl = new BKartabl(ADOPerson5.ID, ADOUser5.ID, ADOUser5.UserName);
            int count = busKartabl.GetRequestCount("003", 1389, 2);
            IList<KartablProxy> list = busKartabl.GetAllRequests("003", 1389, 2, 0, 20, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(count, list.Count);
            Assert.AreEqual(3, count);
        }


        [Test]
        public void SetStatusBySubstitute_ConfirmTest()
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);
            int count1 = busKartabl.GetRequestCount(RequestType.None, 1389, 2);
            IList<KartablProxy> list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 10, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(1, count1);
            Assert.AreEqual(1, list.Count);
            var reqList = from o in list
                          select new KartableSetStatusProxy(o.RequestID, o.ManagerFlowID);
            bool result = busKartabl.SetStatusOfRequest(reqList.ToList<KartableSetStatusProxy>(), RequestState.Confirmed, "");
            Assert.IsTrue(result);
            count1 = busKartabl.GetRequestCount(RequestType.None, 1389, 2);
            list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 10, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(0, count1);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void SetStatusBySubstitute_UnConfirmTest()
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);
            int count1 = busKartabl.GetRequestCount(RequestType.None, 1389, 2);
            IList<KartablProxy> list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 10, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(1, count1);
            Assert.AreEqual(1, list.Count);
            var reqList = from o in list
                          select new KartableSetStatusProxy(o.RequestID, o.ManagerFlowID);
            bool result = busKartabl.SetStatusOfRequest(reqList.ToList<KartableSetStatusProxy>(), RequestState.Unconfirmed, "");
            Assert.IsTrue(result);
            count1 = busKartabl.GetRequestCount(RequestType.None, 1389, 2);

            DatasetGatewayWorkFlow.TA_PermitPairDataTable table = permitPairTA.GetDataByRequestId(list[0].ID);
            Assert.AreEqual(0, table.Rows.Count);

            list = busKartabl.GetAllRequests(RequestType.None, 1389, 2, 0, 10, KartablOrderBy.RegisteredBy);
            Assert.AreEqual(0, count1);
            Assert.AreEqual(0, list.Count);


        }

        [Test]
        public void SetStatusBySubstitute_ConfirmEnflow_PermitCheck_Test()
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);

            KartableSetStatusProxy proxy = new KartableSetStatusProxy(ADORequestDailyLeave1.ID, ADOManagerFlow3.ID);
            IList<KartableSetStatusProxy> list = new List<KartableSetStatusProxy>();
            list.Add(proxy);
            bool result = busKartabl.SetStatusOfRequest(list, RequestState.Confirmed, "");
            DatasetGatewayWorkFlow.TA_PermitPairDataTable table = permitPairTA.GetDataByRequestId(ADORequestDailyLeave1.ID);
            Assert.AreEqual(ADORequestDailyLeave1.FromDate, Convert.ToDateTime(table.Rows[0]["permit_fromdate"]));
            Assert.AreEqual(1, table.Rows.Count);


        }

        [Test]
        public void SetStatusBySubstitute_ConfirmNotEnflow_PermitCheck_Test()
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);

            KartableSetStatusProxy proxy = new KartableSetStatusProxy(ADORequestTraffic1.ID, ADOManagerFlow1.ID);
            IList<KartableSetStatusProxy> list = new List<KartableSetStatusProxy>();
            list.Add(proxy);
            bool result = busKartabl.SetStatusOfRequest(list, RequestState.Confirmed, "");
            DatasetGatewayWorkFlow.TA_PermitPairDataTable table = permitPairTA.GetDataByRequestId(ADORequestDailyLeave1.ID);
            Assert.AreEqual(0, table.Rows.Count);


        }

        [Test]
        public void SetStatusBySubstitute_ConfirmEnflow_UniquePermitCheck_Test()
        {
            busKartabl = new BKartabl(ADOPerson6.ID,ADOUser6.ID, ADOUser6.UserName);

            KartableSetStatusProxy proxy = new KartableSetStatusProxy(ADORequestHourlyDuty2.ID, ADOManagerFlow2.ID);
            IList<KartableSetStatusProxy> list = new List<KartableSetStatusProxy>();
            list.Add(proxy);
            bool result = busKartabl.SetStatusOfRequest(list, RequestState.Confirmed, "");

            ClearSession();

            proxy = new KartableSetStatusProxy(ADORequestHourlyDuty3.ID, ADOManagerFlow2.ID);
            list = new List<KartableSetStatusProxy>();
            list.Add(proxy);
            result = busKartabl.SetStatusOfRequest(list, RequestState.Confirmed, "");

            DatasetGatewayWorkFlow.TA_PermitPairDataTable table = permitPairTA.GetDataByRequestId(ADORequestHourlyDuty2.ID);
            decimal firstPErmitID = Convert.ToDecimal(table.Rows[0]["permit_ID"]);
            Assert.AreEqual(ADORequestHourlyDuty2.FromDate, Convert.ToDateTime(table.Rows[0]["permit_fromdate"]));
            Assert.AreEqual(1, table.Rows.Count);

            table = permitPairTA.GetDataByRequestId(ADORequestHourlyDuty3.ID);
            decimal secondPErmitID = Convert.ToDecimal(table.Rows[0]["permit_ID"]);
            Assert.AreEqual(1, table.Rows.Count);
            Assert.AreEqual(firstPErmitID, secondPErmitID);



        }
        
    }
}
