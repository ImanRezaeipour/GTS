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
    public class GridMonthlyOperationGridMasterSettingsTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_GridMonthlyOperationGridMasterSettingsTableAdapter gridTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_GridMonthlyOperationGridMasterSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter userSettingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_LanguagesTableAdapter langTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LanguagesTableAdapter();
        #endregion

        #region ADOObjects
        BGridMonthlyOperationMasterSettings busGridSettings;
        Languages ADOLAnguage1 = new Languages();
        Languages ADOLAnguage2 = new Languages();
        UserSettings ADOUserSet1 = new UserSettings();
        UserSettings ADOUserSet2 = new UserSettings();
        MonthlyOperationGridMasterSettings ADOgrid1 = new MonthlyOperationGridMasterSettings();
        MonthlyOperationGridMasterSettings grid_testObject = new MonthlyOperationGridMasterSettings();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            grid_testObject = new MonthlyOperationGridMasterSettings();

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

            gridTA.Insert(ADOUserSet1.ID, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);

            busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser1.UserName);
            DatasetGatewayWorkFlow.TA_GridMonthlyOperationGridMasterSettingsDataTable table = new DatasetGatewayWorkFlow.TA_GridMonthlyOperationGridMasterSettingsDataTable();
            table = gridTA.GetDataByUsername(ADOUser1.UserName);
            ADOgrid1.ID = Convert.ToInt32(table.Rows[0][0]);
        }

        [TearDown]
        public void TreatDown()
        {
        }

        [Test]
        public void GetGridSetting_Test()
        {
            busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser1.UserName);
            MonthlyOperationGridMasterSettings gridSet= busGridSettings.GetMonthlyOperationGridMasterSettings();
            Assert.AreEqual(ADOgrid1.ID, gridSet.ID);
        }

        [Test]
        public void GetGridSetting_Lang1Test()
        {
            busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser1.UserName);
            MonthlyOperationGridMasterGeneralSettings gridSet = busGridSettings.GetMonthlyOperationGridGeneralMasterSettings();
            Assert.AreEqual(ADOLAnguage1.ID, gridSet.Language.ID);
        }

        [Test]
        public void GetGridGeneralSetting_Lang2Test()
        {
            busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser2.UserName);
            MonthlyOperationGridMasterGeneralSettings gridSet = busGridSettings.GetMonthlyOperationGridGeneralMasterSettings();
            Assert.AreEqual(ADOLAnguage2.ID, gridSet.Language.ID);
        }

        [Test]
        public void GetGridSetting_InsertRecordTest()
        {
            try
            {
                busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser2.UserName);
                MonthlyOperationGridMasterSettings gridSet = busGridSettings.GetMonthlyOperationGridMasterSettings();
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
                busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser2.UserName);
                busGridSettings.SaveChanges(grid_testObject, UIActionType.EDIT);
                Assert.Fail("Invalid User");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.MonthlyOpCurentUserIsNotValid));
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
                busGridSettings = new BGridMonthlyOperationMasterSettings(ADOUser1.UserName);
                grid_testObject = busGridSettings.GetMonthlyOperationGridMasterSettings();
                ClearSession();
                grid_testObject.DailyMission = true;
                grid_testObject.DailyPureOperation = false;
                busGridSettings.SaveChanges(grid_testObject, UIActionType.EDIT);
                ClearSession();
                decimal id=grid_testObject.ID;
                grid_testObject = new MonthlyOperationGridMasterSettings();
                grid_testObject = busGridSettings.GetByID(id);
                Assert.AreEqual(true, grid_testObject.DailyMission);
                Assert.AreEqual(false, grid_testObject.DailyPureOperation);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetGridSetting_InsertRecordTest22222()
        {
            try
            {
                busGridSettings = new BGridMonthlyOperationMasterSettings();
                MonthlyOperationGridMasterSettings gridSet = busGridSettings.GetMonthlyOperationGridMasterSettings();
              
               
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
