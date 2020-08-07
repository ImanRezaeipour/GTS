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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;


namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    class UserSettingsTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_GridMonthlyOperationGridClientSettingsTableAdapter gridTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_GridMonthlyOperationGridClientSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter userTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter();
        DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter userSettingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter();
        DatabaseGatewayTableAdapters.TA_LanguagesTableAdapter langTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LanguagesTableAdapter();
        DatabaseGateway2TableAdapters.TA_EmailSettingsTableAdapter emailTA = new DatabaseGateway2TableAdapters.TA_EmailSettingsTableAdapter();
        DatabaseGateway2TableAdapters.TA_SMSSettingsTableAdapter smsTA = new DatabaseGateway2TableAdapters.TA_SMSSettingsTableAdapter();
        #endregion

        #region ADOObjects
        EntityRepository<UserSettings> userSettingRep = new EntityRepository<UserSettings>(false);
        BUserSettings busUserSettings;
        Languages ADOLAnguage1 = new Languages();
        Languages ADOLAnguage2 = new Languages();
        User ADOUser6 = new User();
        UserSettings ADOUserSet1 = new UserSettings();
        UserSettings ADOUserSet2 = new UserSettings();
        EmailSettings ADOEmailSetting1 = new EmailSettings();
        EmailSettings email_testObject = new EmailSettings();
        SMSSettings ADOSMSSetting1 = new SMSSettings();
        SMSSettings sms_testObject = new SMSSettings();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            busUserSettings = new BUserSettings();
            email_testObject = new EmailSettings();

            userTA.InsertQuery(ADOPerson1.ID, "TestADOPerson6");
            DatabaseGateway.TA_SecurityUserDataTable userTable = userTable = userTA.GetDataByUserName("TestADOPerson6");
            ADOUser6.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
            ADOUser6.UserName = "TestADOPerson6";

            DatabaseGateway.TA_LanguagesDataTable langTable = langTA.GetData();
            ADOLAnguage1.ID = Convert.ToInt32(langTable.Rows[0][0]);
            ADOLAnguage2.ID = Convert.ToInt32(langTable.Rows[1][0]);

            userSettingTA.Insert(ADOUser1.ID, ADOLAnguage1.ID, null);
            userSettingTA.Insert(ADOUser2.ID, ADOLAnguage2.ID,null);

            DatabaseGateway.TA_UserSettingsDataTable userSetTanle = userSettingTA.GetDataByUsername(ADOUser1.UserName);
            ADOUserSet1.ID = Convert.ToInt32(userSetTanle.Rows[0][0]);

            userSetTanle = userSettingTA.GetDataByUsername(ADOUser2.UserName);
            ADOUserSet2.ID = Convert.ToInt32(userSetTanle.Rows[0][0]);

            emailTA.Insert(ADOUserSet1.ID, true, true, 1, 420, 0, ADOUserSet1.ID);
          
            DatabaseGateway2.TA_EmailSettingsDataTable table = new DatabaseGateway2.TA_EmailSettingsDataTable();
            table = emailTA.GetDataByUserName(ADOUser1.UserName);
            ADOEmailSetting1.ID = Convert.ToInt32(table.Rows[0][0]);

            smsTA.Insert(ADOUserSet1.ID, true, true, 1, 420, 0, ADOUserSet1.ID);
            ADOSMSSetting1.ID = ADOUserSet1.ID;

            
        }

        [TearDown]
        public void TreatDown()
        {
            emailTA.DeleteById(ADOUserSet1.ID);
            smsTA.DeleteById(ADOUserSet1.ID);

            IList<UserSettings> list = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), BUser.CurrentUser));
            if (list != null && list.Count > 0)
            {
                emailTA.DeleteBySettingId(list.First().ID);
                smsTA.DeleteBySettingId(list.First().ID);
            }
                     
            userTA.DeleteByUsername("TestADOPerson6");

        }

        #region Email
        [Test]
        public void GetEmailSetting_NotExsit_ShouldInsert()
        {
            email_testObject = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.IsTrue(email_testObject.ID > 0);
        }

        [Test]
        public void GetEmailSetting_Current_NotExsit_ShouldInsert()
        {
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);
            email_testObject = busUserSettings.GetEmailSetting();
            Assert.IsTrue(email_testObject.ID > 0);
        }

        [Test]
        public void GetEmailSetting()
        {
            email_testObject = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.AreEqual(ADOEmailSetting1.ID, email_testObject.ID);
        }

        [Test]
        public void SaveEmailSettings_CurrentUSer()
        {
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);
            EmailSettings emailSet = new EmailSettings();
            emailSet = busUserSettings.GetEmailSetting();
            emailSet.Active = true;
            emailSet.SendByDay = true;
            emailSet.TheDayHour = "13:10";
            emailSet.TheHour = "10:10";
            emailSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveEmailSetting(emailSet);
            ClearSession();
            emailSet = busUserSettings.GetEmailSetting();
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);


        }

        [Test]
        public void SaveEmailSettings_ByPersonId()
        {
            EmailSettings emailSet = new EmailSettings();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            emailSet.Active = true; 
            emailSet.SendByDay = true;
            emailSet.TheDayHour = "13:10";
            emailSet.TheHour = "10:10";
            emailSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveEmailSetting(emailSet, ADOPerson1.ID);
            ClearSession();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);


        }

        [Test]
        public void SaveEmailSettings_ZeroValidation_Test()
        {
            try
            {
                EmailSettings emailSet = new EmailSettings();
                emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
                emailSet.Active = true;
                emailSet.SendByDay = false;
                emailSet.TheDayHour = "00:50";
                emailSet.TheHour = "00:0";
                emailSet.DayCount = 5;
                ClearSession();
                busUserSettings.SaveEmailSetting(emailSet, ADOPerson1.ID);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserSet_EmailTimeIsNotValid));
            }


        }


        [Test]
        public void SaveEmailSettings_ZeroValidationByDay_Test()
        {
            try
            {
                EmailSettings emailSet = new EmailSettings();
                emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
                emailSet.Active = true;
                emailSet.SendByDay = true;
                emailSet.TheDayHour = "00:00";
                emailSet.TheHour = "10:10";
                emailSet.DayCount = 5;
                ClearSession();
                busUserSettings.SaveEmailSetting(emailSet, ADOPerson1.ID);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserSet_EmailTimeIsNotValid));
            }


        }

        [Test]
        public void SaveEmailSettings_MinTimeValidation_Test()
        {
            try
            {
                EmailSettings emailSet = new EmailSettings();
                emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
                emailSet.Active = true;
                emailSet.SendByDay = false;
                emailSet.TheHour = "00:03";
                ClearSession();
                busUserSettings.SaveEmailSetting(emailSet, ADOPerson1.ID);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserSet_EmailTimeLessThanMin));
            }


        }


        [Test]
        public void SaveEmailSettings_ByQuickSearch()
        {
            EmailSettings emailSet = new EmailSettings();
            emailSet.Active = true;
            emailSet.SendByDay = true;
            emailSet.TheDayHour = "13:10";
            emailSet.TheHour = "10:10";
            emailSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveEmailSetting(emailSet, "TestAli");
            ClearSession();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);

            emailSet = busUserSettings.GetEmailSetting(ADOPerson2.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);

            emailSet = busUserSettings.GetEmailSetting(ADOPerson3.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);


        }

        [Test]
        public void SaveEmailSettings_ByAdvanceSearchSearch()
        {
            EmailSettings emailSet = new EmailSettings();
            emailSet.Active = true;
            emailSet.SendByDay = true;
            emailSet.TheDayHour = "13:10";
            emailSet.TheHour = "10:10";
            emailSet.DayCount = 5;
            ClearSession();
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.DepartmentId = ADORoot.ID;
            proxy.IncludeSubDepartments = true;
            busUserSettings.SaveEmailSetting(emailSet, proxy);
            ClearSession();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);

            emailSet = busUserSettings.GetEmailSetting(ADOPerson2.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);

            emailSet = busUserSettings.GetEmailSetting(ADOPerson3.ID);
            Assert.IsTrue(emailSet.SendByDay);
            Assert.IsTrue(emailSet.Active);
            Assert.AreEqual(790, emailSet.DayHour);
            Assert.AreEqual(5, emailSet.DayCount);


        }
        
        #endregion

        #region SMS
        [Test]
        public void GetSMSSetting_NotExsit_ShouldInsert()
        {
            sms_testObject = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.IsTrue(sms_testObject.ID > 0);
        }

        [Test]
        public void GetSMSSetting_Current_NotExsit_ShouldInsert()
        {
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);
            sms_testObject = busUserSettings.GetSMSSetting();
            Assert.IsTrue(sms_testObject.ID > 0);
        }

        [Test]
        public void GetSMSSetting()
        {
            sms_testObject = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.AreEqual(ADOSMSSetting1.ID, sms_testObject.ID);
        }

        [Test]
        public void SaveSMSSettings_CurrentUSer()
        {
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);
            SMSSettings smsSet = new SMSSettings();
            smsSet = busUserSettings.GetSMSSetting();
            smsSet.Active = true;
            smsSet.SendByDay = true;
            smsSet.TheDayHour = "13:10";
            smsSet.TheHour = "10:10";
            smsSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveSMSSetting(smsSet);
            ClearSession();
            smsSet = busUserSettings.GetSMSSetting();
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);


        }

        [Test]
        public void SaveSMSSettings_ByPersonId()
        {
            SMSSettings smsSet = new SMSSettings();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            smsSet.Active = true; 
            smsSet.SendByDay = true;
            smsSet.TheDayHour = "13:10";
            smsSet.TheHour = "10:10";
            smsSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveSMSSetting(smsSet, ADOPerson1.ID);
            ClearSession();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);


        }
     
        [Test]
        public void SaveSMSSettings_ZeroValidation_Test()
        {
            try
            {
                SMSSettings emailSet = new SMSSettings();
                emailSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
                emailSet.Active = true;
                emailSet.SendByDay = false;
                emailSet.TheDayHour = "00:50";
                emailSet.TheHour = "00:0";
                emailSet.DayCount = 5;
                ClearSession();
                busUserSettings.SaveSMSSetting(emailSet, ADOPerson1.ID);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserSet_SMSTimeIsNotValid));
            }


        }

        [Test]
        public void SaveSMSSettings_ZeroValidationByDay_Test()
        {
            try
            {
                SMSSettings emailSet = new SMSSettings();
                emailSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
                emailSet.Active = true;
                emailSet.SendByDay = true;
                emailSet.TheDayHour = "00:00";
                emailSet.TheHour = "10:10";
                emailSet.DayCount = 5;
                ClearSession();
                busUserSettings.SaveSMSSetting(emailSet, ADOPerson1.ID);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserSet_SMSTimeIsNotValid));
            }


        }

        [Test]
        public void SaveSMSSettings_MinTimeValidation_Test()
        {
            try
            {
                SMSSettings emailSet = new SMSSettings();
                emailSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
                emailSet.Active = true;
                emailSet.SendByDay = false;
                emailSet.TheHour = "00:03";
                ClearSession();
                busUserSettings.SaveSMSSetting(emailSet, ADOPerson1.ID);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserSet_SMSTimeLessThanMin));
            }


        }

        [Test]
        public void SaveSMSSettings_ByQuickSearch()
        {
            SMSSettings smsSet = new SMSSettings();
            smsSet.Active = true;
            smsSet.SendByDay = true;
            smsSet.TheDayHour = "13:10";
            smsSet.TheHour = "10:10";
            smsSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveSMSSetting(smsSet, "TestAli");
            ClearSession();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);

            smsSet = busUserSettings.GetSMSSetting(ADOPerson2.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);

            smsSet = busUserSettings.GetSMSSetting(ADOPerson3.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);


        }

        [Test]
        public void SaveSMSSettings_ByAdvanceSearchSearch()
        {
            SMSSettings smsSet = new SMSSettings();
            smsSet.Active = true;
            smsSet.SendByDay = true;
            smsSet.TheDayHour = "13:10";
            smsSet.TheHour = "10:10";
            smsSet.DayCount = 5;
            ClearSession();
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.DepartmentId = ADORoot.ID;
            proxy.IncludeSubDepartments = true;
            busUserSettings.SaveSMSSetting(smsSet, proxy);
            ClearSession();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);

            smsSet = busUserSettings.GetSMSSetting(ADOPerson2.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);

            smsSet = busUserSettings.GetSMSSetting(ADOPerson3.ID);
            Assert.IsTrue(smsSet.SendByDay);
            Assert.IsTrue(smsSet.Active);
            Assert.AreEqual(790, smsSet.DayHour);
            Assert.AreEqual(5, smsSet.DayCount);


        }

        #endregion

        [Test]
        public void SaveEmail_SMS_Settings_ByPersonId_checkConfilict()
        {
            EmailSettings emailSet = new EmailSettings();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            emailSet.Active = true; 
            emailSet.SendByDay = true;
            emailSet.TheDayHour = "13:10";
            emailSet.TheHour = "10:10";
            emailSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveEmailSetting(emailSet, ADOPerson1.ID);
            ClearSession();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.AreEqual(790, emailSet.DayHour);

            SMSSettings smsSet = new SMSSettings();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            smsSet.Active = true; 
            smsSet.SendByDay = true;
            smsSet.TheDayHour = "13:10";
            smsSet.TheHour = "10:10";
            smsSet.DayCount = 5;
            ClearSession();
            busUserSettings.SaveSMSSetting(smsSet, ADOPerson1.ID);
            ClearSession();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.AreEqual(790, smsSet.DayHour);

            ClearSession();
            emailSet = busUserSettings.GetEmailSetting(ADOPerson1.ID);
            Assert.AreEqual(790, emailSet.DayHour);
            ClearSession();
            smsSet = busUserSettings.GetSMSSetting(ADOPerson1.ID);
            Assert.AreEqual(790, smsSet.DayHour);


        }



    }
}
