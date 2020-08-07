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
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Model.Charts;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-02-16 1:08:29 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class DataAccessTest : BaseFixture
    {
        #region Variables
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workgrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter groupPrecardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();

        BDataAccess busDataAccess;

        Shift ADOShift = new Shift();
        WorkGroup ADOWorkGroup = new WorkGroup();
        Precard ADOPrecard = new Precard();
        PrecardGroups ADOGroup = new PrecardGroups();

        DAWorkGroup ADODAWorkGroup = new DAWorkGroup();
        DAShift ADODAShift = new DAShift();
        DAPrecard ADODAPrecard = new DAPrecard();
        DACtrlStation ADODACtrlStation = new DACtrlStation();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            busDataAccess = new BDataAccess();

            #region Shift
            shiftTA.Insert("ShiftTest", 1, 11, null, 100, false, false, false, "2", "0-00");
            ADOShift = new Shift();

            DatabaseGateway.TA_ShiftDataTable shiftTable = new DatabaseGateway.TA_ShiftDataTable();
            shiftTA.FillByName(shiftTable, "ShiftTest");
            ADOShift.ID = Convert.ToInt32(shiftTable.Rows[0]["shift_ID"]);
            ADOShift.Name = Convert.ToString(shiftTable.Rows[0]["shift_Name"]);
            ADOShift.Color = Convert.ToString(shiftTable.Rows[0]["shift_Color"]);
            ADOShift.CustomCode = Convert.ToString(shiftTable.Rows[0]["shift_CustomCode"]);

            dataAccessShiftTA.Insert(BUser.CurrentUser.ID, ADOShift.ID, false);
            ADODAShift.ID = (dataAccessShiftTA.GetByShiftId(BUser.CurrentUser.ID, ADOShift.ID).Rows[0] as DatabaseGateway.TA_DataAccessShiftRow).DataAccessShift_ID;
            #endregion

            #region WorkGroup
            workgrpTA.Insert("WorkGroupTest1", "0-1", 0);

            DatabaseGateway.TA_WorkGroupDataTable workGrouptable = new DatabaseGateway.TA_WorkGroupDataTable();
            workgrpTA.FillByName(workGrouptable, "WorkGroupTest1");
            ADOWorkGroup.ID = Convert.ToInt32(workGrouptable.Rows[0]["workgroup_ID"]);
            ADOWorkGroup.Name = Convert.ToString(workGrouptable.Rows[0]["workgroup_Name"]);
            ADOWorkGroup.CustomCode = Convert.ToString(workGrouptable.Rows[0]["workgroup_CustomCode"]);

            dataAccessWorkGroupTA.Insert(BUser.CurrentUser.ID, ADOWorkGroup.ID, false);
            ADODAWorkGroup.ID = (dataAccessWorkGroupTA.GetDataByWorkGroupId(ADOWorkGroup.ID, BUser.CurrentUser.ID).Rows[0] as DatabaseGateway.TA_DataAccessWorkGroupRow).DataAccessWorkGrp_ID;
            #endregion

            #region Precard
            groupPrecardTA.Insert("TestPrecardGroup", "TestGroup1");
            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable groupTable;
            groupTable = groupPrecardTA.GetDataByName("TestGroup1");
            ADOGroup.ID = Convert.ToDecimal(groupTable.Rows[0][0]);

            precardTA.Insert("TestPish2", true, ADOGroup.ID, false, true, false, "1002", false);
            
            DatasetGatewayWorkFlow.TA_PrecardDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            precardTable = precardTA.GetDataByName("TestPish2");
            ADOPrecard.ID = Convert.ToDecimal(precardTable.Rows[0][0]);

            dataAccessPrecardTA.Insert(BUser.CurrentUser.ID, ADOPrecard.ID, false);
            ADODAPrecard.ID = (dataAccessPrecardTA.GetDataByPrecardId(BUser.CurrentUser.ID, ADOPrecard.ID).Rows[0] as DatabaseGateway.TA_DataAccessPrecardRow).DataAccessPreCard_ID;
            #endregion

            #region Station
            dataAccessControlStationTA.Insert(BUser.CurrentUser.ID, ADOStaion1.ID, false);
            ADODACtrlStation.ID = (dataAccessControlStationTA.GetDataByControlIdId(BUser.CurrentUser.ID, ADOStaion1.ID).Rows[0] as DatabaseGateway.TA_DataAccessCtrlStationRow).DataAccessCtrlStation_ID;
            #endregion

        }

        [TearDown]
        public void TreatDown()
        {
            shiftTA.DeleteByCustomCode("0-00");
            workgrpTA.DeleteByCustomCode("0-1");
            precardTA.DeleteByID("1002");
            groupPrecardTA.DeleteByName("TestPrecardGroup");
        }

        #region Shift
        [Test]
        public void GetAllShifts_Test()
        {
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Shift, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void InsertShift_All_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.Shift, 0, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Shift, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(true, l.First().All);
        }

        [Test]
        public void InsertShift_Repeat_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.Shift,ADOShift.ID, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Shift, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void DeleteShift_Test()
        {
            busDataAccess.DeleteAccess(DataAccessParts.Shift, ADODAShift.ID, 0);
            ClearSession();
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Shift, BUser.CurrentUser.ID);
            Assert.AreEqual(0, l.Count);
        }
        #endregion

        #region WorkGroup
        [Test]
        public void GetAllWorkGroups_Test()
        {
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.WorkGroup, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void InsertWorkGroup_All_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.WorkGroup, 0, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.WorkGroup, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(true, l.First().All);
        }

        [Test]
        public void InsertWorkGroup_Repeat_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.WorkGroup, ADOWorkGroup.ID, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.WorkGroup, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void DeleteWorkGroup_Test()
        {
            busDataAccess.DeleteAccess(DataAccessParts.WorkGroup, ADODAWorkGroup.ID,0);
            ClearSession();
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.WorkGroup, BUser.CurrentUser.ID);
            Assert.AreEqual(0, l.Count);
        }
        #endregion

        #region Precard
        [Test]
        public void GetAllPrecards_Test()
        {
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Precard, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void InsertPrecard_All_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.Precard, 0, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Precard, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(true, l.First().All);
        }

        [Test]
        public void InsertPrecard_Repeat_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.Precard, ADOPrecard.ID, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Precard, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void DeletePrecard_Test()
        {
            busDataAccess.DeleteAccess(DataAccessParts.Precard, ADODAPrecard.ID,0);
            ClearSession();
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.Precard, BUser.CurrentUser.ID);
            Assert.AreEqual(0, l.Count);
        }
        #endregion

        #region Station
        [Test]
        public void GetAllStations_Test()
        {
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.ControlStation, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void InsertStation_All_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.ControlStation, 0, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.ControlStation, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(true, l.First().All);
        }

        [Test]
        public void InsertStation_Repeat_Test()
        {
            busDataAccess.InsertDataAccess(DataAccessParts.ControlStation, ADOStaion1.ID, BUser.CurrentUser.ID);

            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.ControlStation, BUser.CurrentUser.ID);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void DeleteStation_Test()
        {
            busDataAccess.DeleteAccess(DataAccessParts.ControlStation, ADODACtrlStation.ID,0);
            ClearSession();
            IList<DataAccessProxy> l = busDataAccess.GetAllByUserId(DataAccessParts.ControlStation, BUser.CurrentUser.ID);
            Assert.AreEqual(0, l.Count);
        }
        #endregion

        #region Doctor
        [Test]
        public void Call_GetAllDoctor_Test() 
        {
            busDataAccess.GetAll(DataAccessParts.Doctor);
        }

        [Test]
        public void Call_GetAllDoctorByUser_Test()
        {
            busDataAccess.GetAllByUserId(DataAccessParts.Doctor, BUser.CurrentUser.ID);
        }
        #endregion

        #region Manager
        [Test]
        public void Call_GetAllManager_Test()
        {
            busDataAccess.GetAll(DataAccessParts.Manager);
        }

        [Test]
        public void Call_GetAllManagerByUser_Test()
        {
            busDataAccess.GetAllByUserId(DataAccessParts.Manager , BUser.CurrentUser.ID);
        }
        #endregion

        #region Rule
        [Test]
        public void Call_GetAllRule_Test()
        {
            busDataAccess.GetAll(DataAccessParts.RuleGroup);
        }

        [Test]
        public void Call_GetAllRuleByUser_Test()
        {
            busDataAccess.GetAllByUserId(DataAccessParts.RuleGroup ,BUser.CurrentUser.ID);
        }
        #endregion

        #region Flow
        [Test]
        public void Call_GetAllFlow_Test()
        {
            busDataAccess.GetAll(DataAccessParts.Flow);
        }

        [Test]
        public void Call_GetAllFlowByUser_Test()
        {
            busDataAccess.GetAllByUserId(DataAccessParts.Flow ,BUser.CurrentUser.ID);
        }
        #endregion

        #region Report
        [Test]
        public void Call_GetAllReport_Test()
        {
            busDataAccess.GetAll(DataAccessParts.Report);
        }

        [Test]
        public void Call_GetAllReportByUser_Test()
        {
            busDataAccess.GetAllByUserId(DataAccessParts.Report,BUser.CurrentUser.ID);
        }
        #endregion

        [Test]
        public void test_2222222()
        {
           
        }
    }
}
