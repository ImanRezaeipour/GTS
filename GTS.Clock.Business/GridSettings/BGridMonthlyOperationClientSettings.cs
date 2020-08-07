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
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.GridSettings
{
    /// <summary>
    /// تنظیمات کاربر جهت نمایش تعداد ستون در گزارش کارکرد ماهیانه
    /// </summary>
    public class BGridMonthlyOperationClientSettings : BaseBusiness<MonthlyOperationGridClientSettings>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.GridSettings.BGridMonthlyOperationClientSettings";
        private decimal workingUserSettingsId = 0;
        private decimal workingLanguageIdId = 0;
        private EntityRepository<MonthlyOperationGridClientSettings> rep = new EntityRepository<MonthlyOperationGridClientSettings>(false);
        private UserRepository userRep = new UserRepository(false);

        public string UserName
        {
            get;
            set;
        }

        public BGridMonthlyOperationClientSettings()
        {
        }

        public BGridMonthlyOperationClientSettings(string username)
        {
            this.UserName = username;
        }

        /// <summary>
        /// تنظیمات را برمیگرداند
        /// اگر موجود نباشد ایجاد میکند
        /// </summary>
        /// <returns></returns>
        public MonthlyOperationGridClientSettings GetMonthlyOperationGridClientSettings()
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                MonthlyOperationGridClientSettings Result = null;
                try
                {
                    if (ValidateUser())
                    {
                        EntityRepository<MonthlyOperationGridClientSettings> rep = new EntityRepository<MonthlyOperationGridClientSettings>(false);
                        IList<MonthlyOperationGridClientSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new MonthlyOperationGridClientSettings().UserSetting), new UserSettings() { ID = workingUserSettingsId }));
                        if (list != null && list.Count > 0)
                        {
                            Result = list[0];
                        }
                        else//insert record
                        {
                            MonthlyOperationGridClientSettings settings = new MonthlyOperationGridClientSettings() { AllowableOverTime = true, DailyAbsence = true, DailyMeritoriouslyLeave = true, DailyMission = true, DailyPureOperation = true, DailySickLeave = true, DailyWithoutPayLeave = true, DailyWithPayLeave = true, TheDate = true, DayName = true, FifthEntrance = false, FifthExit = false, FirstEntrance = true, FirstExit = true, FourthEntrance = false, FourthExit = false, HostelryMission = true, HourlyAllowableAbsence = true, HourlyMeritoriouslyLeave = true, HourlyMission = true, HourlyPureOperation = true, HourlySickLeave = true, HourlyUnallowableAbsence = true, HourlyWithoutPayLeave = true, HourlyWithPayLeave = true, ImpureOperation = true, LastExit = true, NecessaryOperation = true, PresenceDuration = true, ReserveField1 = false, ReserveField10 = false, ReserveField2 = false, ReserveField3 = false, ReserveField4 = false, ReserveField5 = false, ReserveField6 = false, ReserveField7 = false, ReserveField8 = false, ReserveField9 = false, SecondEntrance = true, SecondExit = true, Shift = true, ThirdEntrance = false, ThirdExit = false, UnallowableOverTime = true };
                            settings.UserSetting = new UserSettings() { ID = workingUserSettingsId };
                            base.Insert(settings);
                            Result = settings;
                        }
                    }
                    else
                    {
                        throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return Result;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BGridMonthlyOperationClientSettings", "GetMonthlyOperationGridClientSettings");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// تنظیمات مربوط به اندازه ستونهای گرید را برمیگرداند        
        /// </summary>
        /// <returns></returns>
        public MonthlyOperationGridClientGeneralSettings GetMonthlyOperationGridGeneralClientSettings()
        {
            try
            {
                if (ValidateLanguage())
                {
                    EntityRepository<MonthlyOperationGridClientGeneralSettings> rep = new EntityRepository<MonthlyOperationGridClientGeneralSettings>(false);
                    IList<MonthlyOperationGridClientGeneralSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new MonthlyOperationGridClientGeneralSettings().Language), new Languages() { ID = workingLanguageIdId }));
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
                LogException(ex, "BGridMonthlyOperationClientSettings", "GetMonthlyOperationGridGeneralClientSettings");
                throw ex;
            }
        }


        protected override void InsertValidate(MonthlyOperationGridClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        protected override void UpdateValidate(MonthlyOperationGridClientSettings clientSettings)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (!ValidateUser())
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpCurentUserIsNotValid, " کاربر فعلی سیستم نامعتبر است", ExceptionSrc));
            }
            if (clientSettings.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpIDMustSpecified, " شناسه تنظیمات باید مشخص شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(MonthlyOperationGridClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        protected override void GetReadyBeforeSave(MonthlyOperationGridClientSettings clientSettings, UIActionType action)
        {
            if (ValidateUser() && action == UIActionType.EDIT)
            {
                clientSettings.UserSetting = new UserSettings() { ID = this.workingUserSettingsId };
            }
        }

        protected override void Insert(MonthlyOperationGridClientSettings obj)
        {
            rep.WithoutTransactSave(obj);
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
        public decimal SaveChanges_onPersonnelState(MonthlyOperationGridClientSettings monthlyOperationGridClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(monthlyOperationGridClientSettings, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal SaveChanges_onManagerState(MonthlyOperationGridClientSettings monthlyOperationGridClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(monthlyOperationGridClientSettings, UAT);
        }

    }
}
