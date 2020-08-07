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
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Proxy;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-01-10 9:35:02 AM
    /// write your name here
    /// </summary>  
    public class BaseSubstituteOperator : BaseFixture
    {
        protected DatasetGatewayWorkFlowTableAdapters.TA_OperatorTableAdapter operatorTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_OperatorTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_SubstituteTableAdapter substituteTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_SubstituteTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_SubstitutePrecardAccessTableAdapter substitutePrecardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_SubstitutePrecardAccessTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter accessGroupDtlTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        protected DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter mangFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();

        protected Manager ADOManager1 = new Manager();
        protected Manager ADOManager2 = new Manager();
        protected Operator ADOOperator1 = new Operator();
        protected Operator ADOOperator2 = new Operator();
        protected Substitute ADOSubstitute1 = new Substitute();
        protected Substitute ADOSubstitute2 = new Substitute();
        protected PrecardAccessGroup ADOAccessGroup1 = new PrecardAccessGroup();
        protected PrecardAccessGroup ADOAccessGroup2 = new PrecardAccessGroup();
        protected PrecardGroups ADOPrecardGroup1 = new PrecardGroups();
        protected PrecardGroups ADOPrecardGroup2 = new PrecardGroups();
        protected PrecardGroups ADOPrecardGroup3 = new PrecardGroups();
        protected PrecardGroups ADOPrecardGroup4 = new PrecardGroups();
        protected PrecardGroups ADOPrecardGroup5 = new PrecardGroups();
        protected Precard ADOPrecardHourlyLeave1 = new Precard();
        protected Precard ADOPrecardHourlyLeave2 = new Precard();
        protected Precard ADOPrecardHourlyDuty1 = new Precard();
        protected Precard ADOPrecardHourlyEstelji1 = new Precard();
        protected Precard ADOPrecardTraffic1 = new Precard();
        protected Precard ADOPrecardDailyLeave1 = new Precard();
        protected Precard ADOPrecardDailyDuty1 = new Precard();
        protected Precard ADOPrecardOverTime1 = new Precard();
        protected Flow ADOFlow1 = new Flow();
        protected Flow ADOFlow2 = new Flow();


        [SetUp]
        public void TestSetup()
        {           

            managerTA.Insert(ADOPerson2.ID, null);
            managerTA.Insert(ADOPerson3.ID, null);

            DatasetGatewayWorkFlow.TA_ManagerDataTable managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson2.ID);
            ADOManager1.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson2;

            managerTA.FillByPersonID(managetTable, ADOPerson3.ID);
            ADOManager2.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.Person = ADOPerson3;


            #region Flow

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

            mangFlowTA.Insert(ADOManager2.ID, 1, ADOFlow2.ID, true);//مدیر اولیه
            #endregion

        }

        [TearDown]
        public void TreatDown()
        {
            substituteTA.DeleteByManagerId(ADOManager1.ID);
            operatorTA.DeleteByPersonId(ADOPerson1.ID);
            operatorTA.DeleteByPersonId(ADOPerson3.ID);
            managerTA.DeleteByBarcode(ADOPerson2.PersonCode);

            managerTA.DeleteByBarcode("00001");

            flowTA.DeleteByName("FlowTest1");
            flowTA.DeleteByName("FlowTest2");
            flowTA.DeleteByName("InsertedFlow");
            accessGroupTA.DeleteByName("AccessGroup1_2");
            accessGroupTA.DeleteByName("AccessGroup1_3");
            userTA.DeleteByUsername("ADOPerson1");
            userTA.DeleteByUsername("TestUserName1");
            userTA.DeleteByUsername("TestUserName2");

            precardTA.DeleteByID("99999999");
        }

    }
}
