using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.UI;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.GridSettings;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class GunChartClientSettingsTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_GunChartClientSettingsTableAdapter gunchartTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_GunChartClientSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter userTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter();
        DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter userSettingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_LanguagesTableAdapter langTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LanguagesTableAdapter();
        #endregion

        #region ADOObjects
        BGanttChartClientSettings busGuncharSettings;
        Languages ADOLAnguage1 = new Languages();
        Languages ADOLAnguage2 = new Languages();      
        UserSettings ADOUserSet1 = new UserSettings();
        UserSettings ADOUserSet2 = new UserSettings();
        GanttChartClientSettings ADOgrid1 = new GanttChartClientSettings();
        GanttChartClientSettings grid_testObject = new GanttChartClientSettings();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            grid_testObject = new GanttChartClientSettings();
          

            DatabaseGateway.TA_LanguagesDataTable langTable= langTA.GetData();
            ADOLAnguage1.ID = Convert.ToInt32(langTable.Rows[0][0]);
            ADOLAnguage2.ID = Convert.ToInt32(langTable.Rows[1][0]);

            userSettingTA.Insert(ADOUser1.ID, ADOLAnguage1.ID, null);
            userSettingTA.Insert(ADOUser2.ID, ADOLAnguage2.ID, null);
            
            DatabaseGateway.TA_UserSettingsDataTable userSetTanle = new DatabaseGateway.TA_UserSettingsDataTable();
            userSetTanle = userSettingTA.GetDataByUsername(ADOUser1.UserName);
            ADOUserSet1.ID = Convert.ToInt32(userSetTanle.Rows[0][0]);

            userSetTanle = userSettingTA.GetDataByUsername(ADOUser2.UserName);
            ADOUserSet2.ID = Convert.ToInt32(userSetTanle.Rows[0][0]);

            gunchartTA.Insert(ADOUserSet1.ID, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);

            //UpdateCurrentUserPersonId(ADOPerson1.ID);
            busGuncharSettings = new BGanttChartClientSettings(ADOUser1.UserName);
            DatasetGatewayWorkFlow.TA_GunChartClientSettingsDataTable table = new DatasetGatewayWorkFlow.TA_GunChartClientSettingsDataTable();
            table = gunchartTA.GetDataByUserName(ADOUser1.UserName);
            ADOgrid1.ID = Convert.ToInt32(table.Rows[0][0]);
        }

        [TearDown]
        public void TreatDown()
        {
            userTA.DeleteByUsername("TestADOPerson6");
        }

        [Test]
        public void GetGridSetting_Test()
        {
            //UpdateCurrentUserPersonId(ADOPerson1.ID, ADOUserSet1.ID);
            busGuncharSettings = new BGanttChartClientSettings(ADOUser1.UserName);
            GanttChartClientSettings gridSet = busGuncharSettings.GetGanttChartClientSettings();
            Assert.AreEqual(ADOgrid1.ID, gridSet.ID);
        }     
      
        [Test]
        public void GetGridSetting_InsertRecordTest()
        {
            try
            {
                //UpdateCurrentUserPersonId(ADOPerson2.ID);
                busGuncharSettings = new BGanttChartClientSettings(ADOUser2.UserName);
                GanttChartClientSettings gridSet = busGuncharSettings.GetGanttChartClientSettings();
                Assert.IsTrue(gridSet.ID > 0);
                Assert.AreEqual(ADOUserSet2.ID, gridSet.UserSetting.ID);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_InvalidTest() 
        {
            try
            {
                //UpdateCurrentUserPersonId(ADOPerson2.ID);
                busGuncharSettings = new BGanttChartClientSettings(ADOUser2.UserName);
                busGuncharSettings.SaveChanges(grid_testObject, UIActionType.EDIT);
                Assert.Fail("Invalid User");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.MonthlyOpIDMustSpecified));
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_Test()
        {
            try
            {
                //UpdateCurrentUserPersonId(ADOPerson1.ID);
                busGuncharSettings = new BGanttChartClientSettings(ADOUser1.UserName);
                grid_testObject = busGuncharSettings.GetGanttChartClientSettings();
                ClearSession();
                grid_testObject.DailyMission = true;
                grid_testObject.DailySickLeave = false;
                busGuncharSettings.SaveChanges(grid_testObject, UIActionType.EDIT);
                ClearSession();
                decimal id=grid_testObject.ID;
                grid_testObject = new GanttChartClientSettings();
                grid_testObject = busGuncharSettings.GetByID(id);
                Assert.AreEqual(true, grid_testObject.DailyMission);
                Assert.AreEqual(false, grid_testObject.DailySickLeave);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }     
    }
}
