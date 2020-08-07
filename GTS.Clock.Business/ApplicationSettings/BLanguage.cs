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

namespace GTS.Clock.Business.AppSettings
{
    public class DateObj
    {
        public int Month { get; set; }
        public int Day { get; set; }
    }

    public enum SupportedLangs
    {
        faIR,
        enUS  
    }

    public class BLanguage
    {
        private string currentUser;
        public string CurrentUser
        {
            get
            {
                this.currentUser = Security.BUser.CurrentUser.UserName;
                return this.currentUser;
            }
        }
        //public const string BussinessSystemLanguageSessionName1 = "BussinessSystemLanguageSessionName1";
        //public const string BussinessLocalLanguageSessionName1 = "BussinessLocalLanguageSession1";
        //public const string BussinessLocalLanguageSessionName2 = "BussinessLocalLanguageSession2";
        //public const string BussinessSystemLanguageSessionName = "BussinessSystemLanguageSession1";


        public ApplicationLanguageSettings CurrentApplicationSetting 
        {
            get
            {
                EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
                ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();

                return appLangSet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LangID"></param>
        public void SetCurrentLanguage(string LangID)
        {
            EntityRepository<Languages> langRep = new EntityRepository<Languages>(false);
            EntityRepository<UserSettings> userSettingRep = new EntityRepository<UserSettings>(false);
            UserRepository rep = new UserRepository(false);
            User user = rep.GetByUserName(this.CurrentUser);
            Languages language = langRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Languages().LangID), LangID)).FirstOrDefault();
            if (language != null && user != null)
            {
                UserSettings userSetings = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user)).FirstOrDefault();
                if (userSetings != null)
                {
                    userSetings.Language = language;
                    userSettingRep.Update(userSetings);
                }
                else
                {
                    userSetings = new UserSettings();
                    userSetings.Language = language;
                    userSetings.User = user;
                    userSettingRep.Save(userSetings);
                }
                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalLanguageSessionName1, LangID);
                SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalLanguageSessionName2);
            }
            else
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsLanguageOrUserNotExsists, "زبان یا کاربر در دیتابیس موجود نمیباشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentLanguage");
            }        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LangID"></param>
        public void SetCurrentLanguage(string LangID,string username)
        {
            EntityRepository<Languages> langRep = new EntityRepository<Languages>(false);
            EntityRepository<UserSettings> userSettingRep = new EntityRepository<UserSettings>(false);
            UserRepository rep = new UserRepository(false);
            User user = rep.GetByUserName(username);
            Languages language = langRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Languages().LangID), LangID)).FirstOrDefault();
            if (language != null && user != null)
            {
                UserSettings userSetings = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user)).FirstOrDefault();
                if (userSetings != null)
                {
                    userSetings.Language = language;
                    userSettingRep.Update(userSetings);
                }
                else
                {
                    userSetings = new UserSettings();
                    userSetings.Language = language;
                    userSetings.User = user;
                    userSettingRep.Save(userSetings);
                }
                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalLanguageSessionName1, LangID);
                SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalLanguageSessionName2);
            }
            else
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsLanguageOrUserNotExsists, "زبان یا کاربر در دیتابیس موجود نمیباشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentLanguage");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLanguage()
        {
            if (!SessionHelper.HasSessionValue(SessionHelper.BussinessLocalLanguageSessionName1))
            {
                UserRepository rep=new UserRepository(false);
                User user= rep.GetByUserName(this.CurrentUser);
                if (user != null)
                {
                    EntityRepository<UserSettings> appRep = new EntityRepository<UserSettings>(false);
                    IList<UserSettings> list = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user));
                    if (list.Count > 0)
                    {
                        SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalLanguageSessionName1, list[0].Language.LangID);
                        return list[0].Language.LangID;
                    }
                }
                else
                {
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalLanguageSessionName1);
                }
                return "";               
            }
            return Utility.ToString(SessionHelper.GetSessionValue(SessionHelper.BussinessLocalLanguageSessionName1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Languages GetLanguageByUsername(string username)
        {         
            try
            {
                if (!SessionHelper.HasSessionValue(SessionHelper.BussinessLocalLanguageIDSessionName))
                {
                    UserRepository userRep = new UserRepository(false);
                    Languages lang = userRep.GetLanguageByUsername(username);
                    lang = lang == null ? new Languages() : lang;

                    SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalLanguageIDSessionName, lang);

                    return lang;
                }
                object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessLocalLanguageIDSessionName);
                if (obj != null)
                {
                    return (Languages)obj;
                }
                else
                {
                    return new Languages();
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BLanguage", "GetLanguageByUsername");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentSysLanguage()
        {
            /*
            EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
            ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();
            if (appLangSet != null)
            {
                return appLangSet.Language.LangID;
            }
            return "";
            */
            /*                 */

            if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSystemLanguageSessionName1))
            {
                string syslang = "";
                EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
                ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();
                if (appLangSet != null)
                {
                    syslang = appLangSet.Language.LangID;
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessSystemLanguageSessionName1, syslang);
                    return syslang;
                }
                return "";
            }
            return Utility.ToString(SessionHelper.GetSessionValue(SessionHelper.BussinessSystemLanguageSessionName1));
        }

        public string GetSysDateString(DateTime dateTime)
        {
            string SysDateString = string.Empty;
            switch (this.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    SysDateString = pCal.GetYear(dateTime).ToString() + "/" + pCal.GetMonth(dateTime).ToString() + "/" + pCal.GetDayOfMonth(dateTime);
                    break;
                case "en-US":
                    SysDateString = dateTime.ToShortDateString();
                    break;
            }
            return SysDateString;
        }

        public DateTime GetSysDateTime(string strDateTime)
        {
            DateTime SysDateTime = DateTime.Now;
            switch (this.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    string[] strDateTimeParts = strDateTime.Split(new char[]{'/'});
                    PersianCalendar pCal = new PersianCalendar();
                    SysDateTime = pCal.ToDateTime(int.Parse(strDateTimeParts[0]), int.Parse(strDateTimeParts[1]), int.Parse(strDateTimeParts[2]), 0, 0, 0, 0);
                    break;
                case "en-US":
                    SysDateTime = DateTime.Parse(strDateTime);
                    break;
            }
            return SysDateTime;
        }

        public DateObj GetDBDateTime(string strDBDateTime)
        {
            DateObj dateObj = new DateObj();
            DateTime dbDateTime = DateTime.Parse(strDBDateTime);
            switch (this.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    dateObj.Month = pCal.GetMonth(dbDateTime);
                    dateObj.Day = pCal.GetDayOfMonth(dbDateTime);
                    break;
                case "en-US":
                    dateObj.Month = dbDateTime.Month;
                    dateObj.Day = dbDateTime.Day;
                    break;
            }
            return dateObj;
        }

        public static LanguagesName CurrentSystemLanguage
        {
            get
            {
                try
                {
                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSystemLanguageSessionName))
                    {
                        LanguagesName language = LanguagesName.Unknown;
                        EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
                        ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();
                        if (appLangSet != null)
                        {
                            language = appLangSet.Language.Name;
                            SessionHelper.SaveSessionValue(SessionHelper.BussinessSystemLanguageSessionName, language);
                        }

                        return language;
                    }
                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessSystemLanguageSessionName);
                    if (obj != null)
                    {
                        return (LanguagesName)obj;
                    }
                    else
                    {
                        return LanguagesName.Unknown;
                    }
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BLanguage", "CurrentSystemLanguage");
                    throw ex;
                }
            }
        }

        public static LanguagesName CurrentLocalLanguage
        {
            get
            {
                try
                {              
                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessLocalLanguageSessionName2))
                    {
                        LanguagesName language = LanguagesName.Unknown;                        
                        User user = Security.BUser.CurrentUser;
                        if (user != null && user.ID > 0)
                        {
                            EntityRepository<UserSettings> appRep = new EntityRepository<UserSettings>(false);
                            IList<UserSettings> list = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user));
                            if (list.Count > 0)
                            {
                                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalLanguageSessionName2, list[0].Language.Name);
                                language = list[0].Language.Name;
                            }
                        }
                        else
                        {
                            SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalLanguageSessionName2);
                        }
                        return language;
                    }
                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessLocalLanguageSessionName2);
                    if (obj != null)
                    {
                        return (LanguagesName)obj;
                    }
                    else
                    {
                        return LanguagesName.Unknown;
                    }
                }
                catch (Exception ex) 
                {
                    BaseBusiness<Entity>.LogException(ex, "BLanguage", "CurrentLocalLanguage");
                    throw ex;
                }
                    
            }
        }

        public static Languages GetCurrentSystemLanguage()
        {
            try
            {
                if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSystemLanguageIDSessionName))
                {
                    EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
                    ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();
                    Languages lang = new Languages();
                    if (appLangSet != null)
                    {
                        lang = appLangSet.Language;
                    }

                    SessionHelper.SaveSessionValue(SessionHelper.BussinessSystemLanguageIDSessionName, lang);

                    return lang;
                }
                object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessSystemLanguageIDSessionName);
                if (obj != null)
                {
                    return (Languages)obj;
                }
                else
                {
                    return new Languages();
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BLanguage", "GetCurrentSystemLanguage()");
                throw ex;
            }
        }
    }
}
