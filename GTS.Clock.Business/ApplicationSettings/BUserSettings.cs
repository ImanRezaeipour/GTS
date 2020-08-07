using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;


namespace GTS.Clock.Business.AppSettings
{
    public class BUserSettings : BaseBusiness<UserSettings>
    {
        const string ExceptionSrc = "GTS.Clock.Business.AppSettings.BUserSettings";
        private string currentUser;
        public string CurrentUser
        {
            get
            {
                this.currentUser = Security.BUser.CurrentUser.UserName;
                return this.currentUser;
            }
        }
        UserRepository userRep = new UserRepository(false);
        EntityRepository<UserSettings> userSettingRep = new EntityRepository<UserSettings>(false);
        EntityRepository<EmailSettings> emailSettingRep = new EntityRepository<EmailSettings>(false);
        EntityRepository<SMSSettings> smsSettingRep = new EntityRepository<SMSSettings>(false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinID"></param>
        public void SetCurrentSkin(decimal skinID)
        {
            try
            {
                EntityRepository<UISkin> skinRep = new EntityRepository<UISkin>(false);
                UserRepository rep = new UserRepository(false);
                User user = rep.GetByUserName(this.CurrentUser);
                UISkin skin = skinRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UISkin().ID), skinID)).FirstOrDefault();
                if (skin != null && user != null)
                {
                    UserSettings userSetings = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user)).FirstOrDefault();
                    if (userSetings != null)
                    {
                        userSetings.Skin = skin;
                        userSettingRep.Update(userSetings);
                    }
                    else
                    {
                        userSetings = new UserSettings();
                        userSetings.Skin = skin;
                        userSetings.User = user;
                        userSetings.Language = BLanguage.GetCurrentSystemLanguage();
                        userSettingRep.Save(userSetings);
                    }
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalSkinSessionName);
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsSkinOrUserNotExsists, "پوسته یا کاربر در دیتابیس موجود نمیباشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentSkin");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "SetCurrentSkin");
                throw ex;
            }
        }

        public static string CurrentSkin
        {
            get
            {
                try
                {
                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessLocalSkinSessionName))
                    {
                        string curentSkin = "";
                        User user = Security.BUser.CurrentUser;
                        if (user != null && user.ID > 0)
                        {
                            EntityRepository<UserSettings> appRep = new EntityRepository<UserSettings>(false);
                            IList<UserSettings> list = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user));
                            if (list.Count > 0 && list[0].Skin != null)
                            {
                                curentSkin = list[0].Skin.EnName;
                                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalSkinSessionName, curentSkin);
                            }
                        }
                        else
                        {
                            SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalSkinSessionName);
                        }
                        if (Utility.IsEmpty(curentSkin))
                        {
                            EntityRepository<UISkin> skinRep = new EntityRepository<UISkin>(false);
                            UISkin skin = skinRep.GetAll().FirstOrDefault();
                            if (skin != null)
                            {
                                curentSkin = skin.EnName;
                                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalSkinSessionName, curentSkin);
                            }
                        }
                        return curentSkin;
                    }
                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessLocalSkinSessionName);
                    if (obj != null)
                    {
                        return (string)obj;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BUserSettings", "CurrentSkin");
                    throw ex;
                }
            }
        }

        public IList<UISkin> GetAll()
        {
            try
            {
                IList<UISkin> skins = new List<UISkin>();
                if (!SessionHelper.HasSessionValue(SessionHelper.BussinessAllSkinSessionName))
                {
                    EntityRepository<UISkin> skinRep = new EntityRepository<UISkin>(false);
                    skins = skinRep.GetAll();
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessAllSkinSessionName, skins);
                    return skins;
                    foreach (UISkin skin in skins)
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            skin.Name = skin.FnName;
                        }
                        else
                        {
                            skin.Name = skin.EnName;
                        }
                    }
                    return skins;
                }
                object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessAllSkinSessionName);
                if (obj != null)
                {
                    skins = (IList<UISkin>)obj;
                    if (skins == null || skins.Count == 0)
                    {
                        SessionHelper.ClearSessionValue(SessionHelper.BussinessAllSkinSessionName);
                    }
                    foreach (UISkin skin in skins)
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            skin.Name = skin.FnName;
                        }
                        else
                        {
                            skin.Name = skin.EnName;
                        }
                    }
                    return skins;
                }
                else
                {
                    return new List<UISkin>();
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "GetAll");
                throw ex;
            }
        }


        #region Email

        public EmailSettings GetEmailSetting(decimal personID)
        {
            try
            {
                Person person = new BPerson().GetByID(personID);
                UserSettings userSetting = this.GetUserSettings(person.User);
                EmailSettings settings = this.GetEmailSettings(userSetting);

                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetEmailSetting");
                throw ex;
            }
        }

        public EmailSettings GetEmailSetting()
        {
            try
            {
                UserSettings userSetting = this.GetUserSettings(BUser.CurrentUser);
                EmailSettings settings = this.GetEmailSettings(userSetting); ;
                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetEmailSetting");
                throw ex;
            }
        }

        public void SaveEmailSetting(EmailSettings setting)
        {
            try
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
                this.SaveEmailSetting(setting, user);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSetting");
                throw ex;
            }
        }

        public void SaveEmailSetting(EmailSettings setting, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list;

                //don't select count 
                if (proxy.PersonId > 0)
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 1, PersonCategory.Public);
                }
                else
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy);
                }

                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveEmailSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSettingAdvanceSearch");
                throw ex;
            }
        }

        public void SaveEmailSetting(EmailSettings setting, string QuickSearch)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearch(QuickSearch, PersonCategory.Public);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveEmailSetting(setting, prs.User);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSettingQuickSearch");
                throw ex;
            }
        }

        public void SaveEmailSetting(EmailSettings setting, decimal personId)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonId = personId };
                this.SaveEmailSetting(setting, proxy);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSetting");
                throw ex;
            }
        }

        private void SaveEmailSetting(EmailSettings setting, User user)
        {
            try
            {

                /*if (user == null || user.ID == 0)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.CurrentUserIsNotValid, "کاربر جاری نا معتبر است", ExceptionSrc);
                }*/
                if (user.UserSetting == null || user.UserSetting.ID == 0)
                {
                    user.UserSetting = this.GetUserSettings(user);
                }
                if (user.UserSetting.EmailSettings == null)
                {
                    user.UserSetting.EmailSettings = this.GetEmailSettings(user.UserSetting);
                }

                UserSettings userSetting = base.GetByID(user.UserSetting.ID);
                userSetting.EmailSettings.Active = setting.Active;
                userSetting.EmailSettings.DayHour = Utility.RealTimeToIntTime(setting.TheDayHour);
                userSetting.EmailSettings.DayCount = setting.DayCount;
                userSetting.EmailSettings.Hour = Utility.RealTimeToIntTime(setting.TheHour);
                userSetting.EmailSettings.SendByDay = setting.SendByDay;

                #region validation
                UIValidationExceptions exceptions = new UIValidationExceptions();
                if (setting.Active)
                {
                    if (setting.SendByDay)
                    {
                        if (userSetting.EmailSettings.DayHour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_EmailTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                    }
                    else
                    {
                        if (userSetting.EmailSettings.Hour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_EmailTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                        else if (userSetting.EmailSettings.Hour < 5)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_EmailTimeLessThanMin, "تکرار زمان ارسال ایمیل حداقل 5 دقیقه میباشد", ExceptionSrc));
                        }
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                this.SaveChanges(userSetting, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات  ایمیل را برای کل سازمان برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<InfoServiceProxy> GetAllEmailSettings()
        {
            try
            {
                IList<InfoServiceProxy> proxyList = new List<InfoServiceProxy>();
                IList<UserSettings> settingList = base.GetAll();

                var result = from o in settingList
                             where o.EmailSettings != null && o.EmailSettings.Active
                             select o.EmailSettings;
                IList<EmailSettings> emailSettingList = result.ToList<EmailSettings>();

                foreach (EmailSettings setting in emailSettingList)
                {
                    InfoServiceProxy proxy = new InfoServiceProxy();

                    proxy.PersonId = setting.UserSetting.User.Person.ID;
                    proxy.PersonName = setting.UserSetting.User.Person.Name;
                    proxy.PersonCode = setting.UserSetting.User.Person.PersonCode;
                    proxy.Sex = setting.UserSetting.User.Person.Sex;
                    proxy.SendByDay = setting.SendByDay;
                    proxy.EmailAddress = setting.UserSetting.User.Person.PersonDetail.EmailAddress;
                    proxy.SmsNumber = setting.UserSetting.User.Person.PersonDetail.MobileNumber;
                    if (setting.SendByDay)
                    {
                        proxy.RepeatePeriod = new TimeSpan(setting.DayCount, ((int)setting.DayHour / 60), setting.DayHour % 60, 0);
                    }
                    else
                    {
                        proxy.RepeatePeriod = new TimeSpan(((int)setting.Hour / 60), setting.Hour % 60, 0);
                    }
                    proxyList.Add(proxy);
                }
                return proxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetAllEmailSettings");
                throw ex;
            }
        }
        #endregion

        #region SMS

        /// <summary>
        /// تنظیمات  اس ام اس را برای کل سازمان برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<InfoServiceProxy> GetAllSmsSettings()
        {
            try
            {
                IList<InfoServiceProxy> proxyList = new List<InfoServiceProxy>();
                IList<UserSettings> settingList = base.GetAll();

                var result = from o in settingList
                             where o.SMSSettings != null && o.SMSSettings.Active
                             select o.SMSSettings;
                IList<SMSSettings> smsSettingList = result.ToList<SMSSettings>();

                foreach (SMSSettings setting in smsSettingList)
                {
                    InfoServiceProxy proxy = new InfoServiceProxy();

                    proxy.PersonId = setting.UserSetting.User.Person.ID;
                    proxy.PersonName = setting.UserSetting.User.Person.Name;
                    proxy.PersonCode = setting.UserSetting.User.Person.PersonCode;
                    proxy.Sex = setting.UserSetting.User.Person.Sex;
                    proxy.SendByDay = setting.SendByDay;
                    proxy.EmailAddress = setting.UserSetting.User.Person.PersonDetail.EmailAddress;
                    proxy.SmsNumber = setting.UserSetting.User.Person.PersonDetail.MobileNumber;

                    if (setting.SendByDay)
                    {
                        proxy.RepeatePeriod = new TimeSpan(setting.DayCount, ((int)setting.DayHour / 60), setting.DayHour % 60, 0);
                    }
                    else
                    {
                        proxy.RepeatePeriod = new TimeSpan(((int)setting.Hour / 60), setting.Hour % 60, 0);
                    }
                    proxyList.Add(proxy);
                }
                return proxyList;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        public SMSSettings GetSMSSetting(decimal personID)
        {
            try
            {
                Person person = new BPerson().GetByID(personID);
                UserSettings userSetting = this.GetUserSettings(person.User);
                SMSSettings settings = this.GetSMSSettings(userSetting);

                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetSMSSetting");
                throw ex;
            }
        }

        public SMSSettings GetSMSSetting()
        {
            try
            {
                UserSettings userSetting = this.GetUserSettings(BUser.CurrentUser);
                SMSSettings settings = this.GetSMSSettings(userSetting);
                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetSMSSetting");
                throw ex;
            }
        }

        public void SaveSMSSetting(SMSSettings setting)
        {
            try
            {
                this.SaveSMSSetting(setting, BUser.CurrentUser);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettings");
                throw ex;
            }
        }

        public void SaveSMSSetting(SMSSettings setting, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list;

                //don't select count 
                if (proxy.PersonId > 0)
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 1, PersonCategory.Public);
                }
                else
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy);
                }
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveSMSSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettingsAdvanceSearch");
                throw ex;
            }
        }

        public void SaveSMSSetting(SMSSettings setting, string QuickSearch)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearch(QuickSearch, PersonCategory.Public);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveSMSSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettingsQuickSearch");
                throw ex;
            }
        }

        public void SaveSMSSetting(SMSSettings setting, decimal personId)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonId = personId };
                this.SaveSMSSetting(setting, proxy);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettings");
                throw ex;
            }
        }

        private void SaveSMSSetting(SMSSettings setting, User user)
        {
            try
            {
                /*if (user == null || user.ID == 0)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.CurrentUserIsNotValid, "کاربر جاری نا معتبر است", ExceptionSrc);
                }*/
                if (user.UserSetting == null || user.UserSetting.ID == 0)
                {
                    user.UserSetting = this.GetUserSettings(user);
                }
                if (user.UserSetting.SMSSettings == null)
                {
                    user.UserSetting.SMSSettings = this.GetSMSSettings(user.UserSetting);
                }
                if (user.UserSetting.EmailSettings == null)
                {
                    user.UserSetting.EmailSettings = this.GetEmailSettings(user.UserSetting);
                }

                UserSettings userSetting = base.GetByID(user.UserSetting.ID);
                userSetting.SMSSettings.Active = setting.Active;
                userSetting.SMSSettings.DayHour = Utility.RealTimeToIntTime(setting.TheDayHour);
                userSetting.SMSSettings.DayCount = setting.DayCount;
                userSetting.SMSSettings.Hour = Utility.RealTimeToIntTime(setting.TheHour);
                userSetting.SMSSettings.SendByDay = setting.SendByDay;

                #region validation
                UIValidationExceptions exceptions = new UIValidationExceptions();
                if (setting.Active)
                {
                    if (setting.SendByDay)
                    {
                        if (userSetting.SMSSettings.DayHour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_SMSTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                    }
                    else
                    {
                        if (userSetting.SMSSettings.Hour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_SMSTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                        else if (userSetting.SMSSettings.Hour < 5)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_SMSTimeLessThanMin, "تکرار زمان ارسال ایمیل حداقل 5 دقیقه میباشد", ExceptionSrc));
                        }
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                this.SaveChanges(userSetting, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettings");
                throw ex;
            }
        }

        #endregion

        #region Insert Update Delete
        protected override void InsertValidate(UserSettings obj)
        {

        }

        protected override void UpdateValidate(UserSettings obj)
        {

        }

        protected override void DeleteValidate(UserSettings obj)
        {
        }
        #endregion


        /// <summary>
        /// تنظیمات کاربر را برمیگرداند
        /// در صورد عدم وجود آنرا درج میکند
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private UserSettings GetUserSettings(User user)
        {
            if (user != null && user.ID > 0)
            {
                IList<UserSettings> userSettingList = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), new User() { ID = user.ID }));
                if (userSettingList != null && userSettingList.Count > 0)
                {
                    user.UserSetting = userSettingList.First();
                    return user.UserSetting;
                }
                else
                {
                    UserSettings userSetings = new UserSettings();
                    userSetings.User = user;
                    userSetings.Language = BLanguage.GetCurrentSystemLanguage();
                    userSettingRep.Save(userSetings);
                    user.UserSetting = userSetings;
                    return userSetings;
                }
            }
            return null;
        }

        private EmailSettings GetEmailSettings(UserSettings userSettings)
        {
            if (userSettings != null && userSettings.ID > 0)
            {
                if (userSettings.EmailSettings != null && userSettings.EmailSettings.ID > 0)
                {
                    return userSettings.EmailSettings;
                }
                else
                {
                    IList<EmailSettings> emailSettingList = emailSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new EmailSettings().UserSetting), new UserSettings() { ID = userSettings.ID }));
                    if (emailSettingList != null && emailSettingList.Count > 0)
                    {
                        return emailSettingList.First();
                    }
                    else
                    {
                        EmailSettings obj = new EmailSettings();
                        obj.ID = userSettings.ID;
                        obj.UserSetting = userSettings;
                        obj.Active = false;
                        emailSettingRep.Save(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        private SMSSettings GetSMSSettings(UserSettings userSettings)
        {
            if (userSettings != null && userSettings.ID > 0)
            {
                if (userSettings.SMSSettings != null && userSettings.SMSSettings.ID > 0)
                {
                    return userSettings.SMSSettings;
                }
                else
                {
                    IList<SMSSettings> smsSettingList = smsSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new SMSSettings().UserSetting), new UserSettings() { ID = userSettings.ID }));
                    if (smsSettingList != null && smsSettingList.Count > 0)
                    {
                        return smsSettingList.First();
                    }
                    else
                    {
                        SMSSettings obj = new SMSSettings();
                        obj.ID = userSettings.ID;
                        obj.UserSetting = userSettings;
                        obj.Active = false;
                        smsSettingRep.Save(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelUserSettingsLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckManagementUserSettingsLoadAccess()
        {
        }

    }
}
