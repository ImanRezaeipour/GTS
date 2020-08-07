using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Business.GridSettings
{
    /// <summary>
    ///  تنظیمات مدیر جهت نمایش تعداد ستون در گزارش کارکرد ماهیانه افراد تحت مدیریت 
    /// </summary>
    public class BGridMonthlyOperationMasterSettings : BaseBusiness<MonthlyOperationGridMasterSettings>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.GridSettings.BGridMonthlyOperationMasterSettings";
        private decimal workingUserSettingsId = 0;
        private decimal workingLanguageIdId = 0;
        private UserRepository userRep = new UserRepository(false);

        public string UserName
        {
            get;
            set;
        }

        public BGridMonthlyOperationMasterSettings()
        {            
        }

        public BGridMonthlyOperationMasterSettings(string username)
        {
            this.UserName = username;
        }

        /// <summary>
        /// تنظیمات را برمیگرداند
        /// اگر موجود نباشد ایجاد میکند
        /// </summary>
        /// <returns></returns>
        public MonthlyOperationGridMasterSettings GetMonthlyOperationGridMasterSettings()
        {
            try
            {
                if (ValidateUser())
                {
                    EntityRepository<MonthlyOperationGridMasterSettings> rep = new EntityRepository<MonthlyOperationGridMasterSettings>(false);
                    IList<MonthlyOperationGridMasterSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new MonthlyOperationGridMasterSettings().UserSetting), new UserSettings() { ID = workingUserSettingsId }));
                    if (list != null && list.Count > 0)
                    {
                        return list[0];
                    }
                    else//insert record 
                    {
                        MonthlyOperationGridMasterSettings settings = new MonthlyOperationGridMasterSettings() { AllowableOverTime = true, BarCode = true, DailyAbsence = true, DailyMeritoriouslyLeave = true, DailyMission = true, DailyPureOperation = true, DailySickLeave = true, DailyWithoutPayLeave = true, DailyWithPayLeave = true, DepartmentName = true, FifthEntrance = true, FifthExit = true, FirstEntrance = true, FirstExit = true, FourthEntrance = true, FourthExit = true, HostelryMission = true, HourlyAllowableAbsence = true, HourlyMeritoriouslyLeave = true, HourlyMission = true, HourlyPureOperation = true, HourlySickLeave = true, HourlyUnallowableAbsence = true, HourlyWithoutPayLeave = true, HourlyWithPayLeave = true, ImpureOperation = true, LastExit = true, NecessaryOperation = true, PersonName = true, PresenceDuration = true, RemainLeaveToMonthEnd = true, RemainLeaveToYearEnd = true, ReserveField1 = true, ReserveField10 = true, ReserveField2 = true, ReserveField3 = true, ReserveField4 = true, ReserveField5 = true, ReserveField6 = true, ReserveField7 = true, ReserveField8 = true, ReserveField9 = true, SecondEntrance = true, SecondExit = true, ThirdEntrance = true, ThirdExit = true, UnallowableOverTime = true };
                        settings.UserSetting = new UserSettings() { ID = workingUserSettingsId };
                        base.Insert(settings);
                        return settings;
                    }
                }
                else
                {
                    throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "MonthlyOperationGridMasterSettings", "GetMonthlyOperationGridMasterSettings");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات را برمیگرداند
        /// اگر موجود نباشد ایجاد میکند
        /// </summary>
        /// <returns></returns>
        public MonthlyOperationGridMasterGeneralSettings GetMonthlyOperationGridGeneralMasterSettings()
        {
            try
            {
                if (ValidateLanguage())
                {
                    EntityRepository<MonthlyOperationGridMasterGeneralSettings> rep = new EntityRepository<MonthlyOperationGridMasterGeneralSettings>(false);
                    IList<MonthlyOperationGridMasterGeneralSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new MonthlyOperationGridMasterGeneralSettings().Language), new Languages() { ID = workingLanguageIdId }));
                    if (list != null && list.Count > 0)
                    {
                        return list[0];
                    }
                }
                else
                {
                    throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogException(ex, "BGridMonthlyOperationMasterSettings", "GetMonthlyOperationGridGeneralMasterSettings");
                throw ex;
            }
        }


        protected override void InsertValidate(MonthlyOperationGridMasterSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        protected override void UpdateValidate(MonthlyOperationGridMasterSettings clientSettings)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (!ValidateUser() || clientSettings.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpCurentUserIsNotValid, " کاربر فعلی سیستم نامعتبر است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(MonthlyOperationGridMasterSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        protected override void GetReadyBeforeSave(MonthlyOperationGridMasterSettings clientSettings, UIActionType action)
        {
            if (ValidateUser() && action == UIActionType.EDIT)
            {
                clientSettings.UserSetting = new UserSettings() { ID = this.workingUserSettingsId };
            }
        }

        /// <summary>
        /// اگر نام کاربری وجود نداشته باشد یا رکورد تنظیمات کاربر در دیتابیس  موجود نباشد غلط برمیگرداند
        /// </summary>
        /// <returns></returns>
        private bool ValidateUser()
        {
            if (this.workingUserSettingsId > 0)
                return true;
            if (Utility.IsEmpty(this.UserName))
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
                
                if (user != null && user.UserSetting != null && user.UserSetting.ID > 0)
                {
                    this.UserName = user.UserName;
                    this.workingUserSettingsId = user.UserSetting.ID;
                }
            }
            else 
            {
                User user = userRep.GetByUserName(this.UserName);
                if (user != null && user.UserSetting != null && user.UserSetting.ID > 0)
                {
                    this.workingUserSettingsId = user.UserSetting.ID;
                }
            }
            if (this.workingUserSettingsId > 0)
            {
                NHibernateSessionManager.Instance.ClearSession();
                return true;
            }
            return false;
        }

        /// <summary>
        /// زبان انتخابی کاربر را اعتبارستجی میکند
        /// </summary>
        /// <returns></returns>
        private bool ValidateLanguage()
        {
            if (this.workingLanguageIdId > 0)
                return true;
            if (Utility.IsEmpty(this.UserName))
            {
                this.UserName = Security.BUser.CurrentUser.UserName;
                AppSettings.BLanguage blang = new GTS.Clock.Business.AppSettings.BLanguage();
                Languages lang = blang.GetLanguageByUsername(this.UserName);
                if (lang.ID > 0)
                {
                    this.workingLanguageIdId = lang.ID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                AppSettings.BLanguage blang = new GTS.Clock.Business.AppSettings.BLanguage();
                Languages lang = blang.GetLanguageByUsername(this.UserName);
                if (lang.ID > 0)
                {
                    this.workingLanguageIdId = lang.ID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public override decimal SaveChanges(MonthlyOperationGridMasterSettings obj, UIActionType action)
        {
            return base.SaveChanges(obj, action);
        }
    }
}
