using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.ArchiveCalculations;

namespace GTS.Clock.Business.UIValidation
{
    public class RequestValidator : IRequestUIValidation, ILockCalculationUIValidation, IArchiveCalculationUIValidation
    {
        const string ExceptionSrc = "GTS.Clock.Business.UIValidation.RequestValidator";
        UIValidationExceptions exception = new UIValidationExceptions();
        UIValidationGroupingRepository validateRep = new UIValidationGroupingRepository();
        BRequest businessRequest = new BRequest();
        BPerson businessPerson = new BPerson();
        BDateRange businessDateRange = new BDateRange();

        RequestRepository requestRepository = new RequestRepository(false);


        /// <summary>
        /// تاریخ بسته شدن محاسبات را برای یک شخص برمیگرداند
        /// بستن محاسبات تنها از طریق قوانین 4 و 5 صورت میگیرد
        /// دقت شود که همیشه حداکثر تنها یکی از این دو فعال هستند
        /// لازم به ذکر است اگر 4 و 5 فعال نباشد بصورت پیشفرض قانون 6 درنظر گرفته میشود
        /// پس همیشه باید قانون 6 پارامتر داشته باشد
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public DateTime GetCalculationLockDate(decimal personId)
        {

            DateTime lockDate = Utility.GTSMinStandardDateTime;

            IList<UIValidationGrouping> groupingList = new List<UIValidationGrouping>();
            groupingList = validateRep.GetByPersonId(personId);
            IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.Active && x.SubSystemId == 1).ToList();

            UIValidationGrouping grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 4.ToString()).ToList().FirstOrDefault();
            //در حال تعریف پرسنل هستیم و هنوز گروه واسط کاربر انتساب ندادیم
            if (grouping == null)
            {
                return DateTime.Now;
            }

            //R4
            #region R4

            if (grouping != null && grouping.Active)
            {
                IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                #region Checking
                if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 4 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                }
                UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromCurrentMonth")).FirstOrDefault();
                if (param == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromCurrentMonth  نظر برای قانون 4 یافت نشد ", ExceptionSrc);
                }
                #endregion

                int lockCalculationFromCurrentMonth = Utility.ToInteger(param.Value);

                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                //تاریخ بستن ماه قبل
                if (lockCalculationFromCurrentMonth > pd.Day)
                {
                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month != 1 ? pd.Month - 1 : 12, lockCalculationFromCurrentMonth));
                }
                //تاریخ بستن ماه جاری
                else
                {
                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, lockCalculationFromCurrentMonth));
                }
            }
            #endregion

            //R5 اگر روز جاری به پارامتر نرسیده بود باید دو ماه قبل را بدهد ولی اگر بزرگتر از 
            //پارامتر بود باید ماه قبل را بدهد
            else
            {
                #region R5
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 5.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                    #region cheking
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 5 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromBeforeMonth")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromBeforeMonth  نظر برای قانون 5 یافت نشد ", ExceptionSrc);
                    }
                    #endregion

                    int LockCalculationFromBeforeMonth = Utility.ToInteger(param.Value);
                    PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                    if (LockCalculationFromBeforeMonth >= pd.Day)
                    {
                        if (pd.Month > 2)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 2, LockCalculationFromBeforeMonth));
                        }
                        else if (pd.Month == 2)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
                        }
                        else if (pd.Month == 1)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 11, LockCalculationFromBeforeMonth));
                        }
                    }
                    else
                    {
                        if (pd.Month > 1)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, LockCalculationFromBeforeMonth));
                        }
                        else
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
                        }
                    }
                }
                #endregion

                #region R6
                else //R6
                {
                    grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 6.ToString()).ToList().FirstOrDefault();
                    if (grouping != null && grouping.Active)
                    {
                        IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                        #region cheking
                        if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 6 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                        }
                        UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
                        if (param == null)
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, String.Format("پارامتر با کلید LockCalculationFromDate    برای قانون 6 یافت نشد، "), ExceptionSrc);
                        }
                        #endregion

                        DateTime LockCalculationFromDate = new DateTime();
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            LockCalculationFromDate = Utility.ToMildiDate(param.Value);
                        }
                        else
                        {
                            LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                        }
                        lockDate = LockCalculationFromDate;
                    }
                }
                #endregion
            }
            if (lockDate == Utility.GTSMinStandardDateTime)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationMinOneMustBeActive, "حداقل یکی از قوانین بستن محاسبات باید فعال باشد", ExceptionSrc);
            }
            return lockDate;
        }

        /// <summary>
        /// تاریخ بسته شدن محاسبات را برای یک شخص برمیگرداند
        /// بستن محاسبات تنها از طریق قوانین 4 و 5 صورت میگیرد
        /// دقت شود که همیشه حداکثر تنها یکی از این دو فعال هستند
        /// لازم به ذکر است اگر 4 و 5 فعال نباشد بصورت پیشفرض قانون 6 درنظر گرفته میشود
        /// پس همیشه باید قانون 6 پارامتر داشته باشد
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public DateTime GetCalculationLockDateByGroup(decimal uiGroupId)
        {

            DateTime lockDate = Utility.GTSMinStandardDateTime;

            IList<UIValidationGrouping> groupingList = new List<UIValidationGrouping>();
            groupingList = validateRep.GetByGroupId(uiGroupId);
            IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.Active && x.SubSystemId == 1).ToList();

            UIValidationGrouping grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 4.ToString()).ToList().FirstOrDefault();
            //در حال تعریف پرسنل هستیم و هنوز گروه واسط کاربر انتساب ندادیم
            if (grouping == null)
            {
                return DateTime.Now;
            }

            //R4
            #region R4

            if (grouping != null && grouping.Active)
            {
                IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                #region Checking
                if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 4 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                }
                UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromCurrentMonth")).FirstOrDefault();
                if (param == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromCurrentMonth  نظر برای قانون 4 یافت نشد ", ExceptionSrc);
                }
                #endregion

                int lockCalculationFromCurrentMonth = Utility.ToInteger(param.Value);

                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                //تاریخ بستن ماه قبل
                if (lockCalculationFromCurrentMonth > pd.Day)
                {
                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month != 1 ? pd.Month - 1 : 12, lockCalculationFromCurrentMonth));
                }
                //تاریخ بستن ماه جاری
                else
                {
                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, lockCalculationFromCurrentMonth));
                }
            }
            #endregion

            //R5 اگر روز جاری به پارامتر نرسیده بود باید دو ماه قبل را بدهد ولی اگر بزرگتر از 
            //پارامتر بود باید ماه قبل را بدهد
            else
            {
                #region R5
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 5.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                    #region cheking
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 5 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromBeforeMonth")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromBeforeMonth  نظر برای قانون 5 یافت نشد ", ExceptionSrc);
                    }
                    #endregion

                    int LockCalculationFromBeforeMonth = Utility.ToInteger(param.Value);
                    PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                    if (LockCalculationFromBeforeMonth >= pd.Day)
                    {
                        if (pd.Month > 2)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 2, LockCalculationFromBeforeMonth));
                        }
                        else if (pd.Month == 2)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
                        }
                        else if (pd.Month == 1)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 11, LockCalculationFromBeforeMonth));
                        }
                    }
                    else
                    {
                        if (pd.Month > 1)
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, LockCalculationFromBeforeMonth));
                        }
                        else
                        {
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
                        }
                    }
                }
                #endregion

                #region R6
                else //R6
                {
                    grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 6.ToString()).ToList().FirstOrDefault();
                    if (grouping != null && grouping.Active)
                    {
                        IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                        #region cheking
                        if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 6 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                        }
                        UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
                        if (param == null)
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, String.Format("پارامتر با کلید LockCalculationFromDate    برای قانون 6 یافت نشد، "), ExceptionSrc);
                        }
                        #endregion

                        DateTime LockCalculationFromDate = new DateTime();
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            LockCalculationFromDate = Utility.ToMildiDate(param.Value);
                        }
                        else
                        {
                            LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                        }
                        lockDate = LockCalculationFromDate;
                    }
                }
                #endregion
            }
            if (lockDate == Utility.GTSMinStandardDateTime)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationMinOneMustBeActive, "حداقل یکی از قوانین بستن محاسبات باید فعال باشد", ExceptionSrc);
            }
            return lockDate;
        }

        public void DoValidate(Request request)
        {
            try
            {
                decimal personId = request.Person.ID;
                IList<UIValidationGrouping> groupingList = new List<UIValidationGrouping>();
                groupingList = validateRep.GetByPersonId(personId);
                EntityRepository<UIValidationRule> rulerep = new EntityRepository<UIValidationRule>(false);
                IList<UIValidationRule> ruleList = rulerep.Find(x => x.Active && x.SubSystemId == 1).ToList();

                #region R4
                UIValidationGrouping grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 4.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    R4_LockCalculationFromCurrentMonth(request, grouping);
                }
                #endregion

                #region R5
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 5.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    R5_LockCalculationFromBeforeMonth(request, grouping);
                }
                #endregion

                #region R6
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 6.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    R6_LockCalculationFromDate(request, grouping);
                }
                #endregion

                #region R7
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 7.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R7_MaxTarfficRequest(request, grouping);
                    }
                }
                #endregion

                #region R8
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 8.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R8_TrafficRequest(request, grouping);
                    }
                }

                #endregion

                #region R9
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 9.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R9_HourlyLeaveDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R10
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 10.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R10_DailyLeaveDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R11
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 11.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R11_HourlyWithoutPayLeaveDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R12
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 12.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R12_DailyWithoutPayLeaveDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R13
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 13.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R13_HourlyEstelajiLeaveDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R14
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 14.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R14_DailyEstelajiLeaveDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R15
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 15.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R15_OverTimeDayOfset(request, grouping);
                    }
                }
                #endregion

                #region R16
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 16.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R16_MaxHourlyLeaveCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R17
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 17.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R17_MaxDailyLeaveCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R18
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 18.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R18_MaxHourlyWithoutPayCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R19
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 19.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R19_MaxDailyWithoutPayCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R20
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 20.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R20_MaxHourlyEstelajiCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R21
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 21.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R21_MaxDailyEstelajiCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R22
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 22.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R22_MaxOverWorkRequest(request, grouping);
                    }
                }
                #endregion

                #region R23
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 23.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R23_MaxDailyEstelajiYearCountRequest(request, grouping);
                    }
                }
                #endregion

                #region R24
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 24.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R24_AllRequestOffset(request, grouping);
                    }
                }
                #endregion

                #region R26
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 26.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R26_RequestMaxAvvalVaght(request, grouping);
                    }
                }
                #endregion

                #region R27
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 27.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R27_MaxCountInYearRequest(request, grouping);
                    }
                }
                #endregion

                #region R28
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 28.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R28_MaxValueOfRequest(request, grouping);
                    }
                }
                #endregion

                #region R29
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 29.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R29_DasturyOverworkRequest(request, grouping);
                    }
                }
                #endregion

                #region R30
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 30.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R30_DutyPlaceRequest(request, grouping);
                    }
                }
                #endregion

                #region R31
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 31.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R31_DoctorRequest(request, grouping);
                    }
                }
                #endregion

                #region R32
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 32.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R32_IllenssRequest(request, grouping);
                    }
                }
                #endregion

                #region R33
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 33.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R33_LeaveRemain(request, grouping);
                    }
                }
                #endregion

                #region R34
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 34.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R34_RequestDescriptionRequierd(request, grouping);
                    }
                }
                #endregion

                #region R35
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 35.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R35_RequestMaxCountInDay(request, grouping);
                    }
                }
                #endregion

                #region R36
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 36.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R36_RequestMinLength(request, grouping);
                    }
                }
                #endregion

                #region R37
                if ((new BOperator()).GetOperator(BUser.CurrentUser.Person.ID).ToList<Operator>().Count() > 0)
                {
                    grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 37.ToString()).ToList().FirstOrDefault();
                    if (grouping != null && grouping.Active && personId != BUser.CurrentUser.Person.ID)
                    {
                        R37_OperatorRequestMaxCount(request, grouping);
                    }
                }
                #endregion

                #region R38
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 38.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    if (grouping.OperatorRestriction || personId == BUser.CurrentUser.Person.ID)
                    {
                        R38_RequestOverTimeMaxAvvalVaght(request, grouping);
                    }
                }
                #endregion

                if (exception.Count > 0)
                    throw exception;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "DoValidate");
                throw ex;
            }
        }

        public void DoValidate(decimal personId)
        {

            
            IList<UIValidationGrouping> groupingList = new List<UIValidationGrouping>();
            groupingList = validateRep.GetByPersonId(personId);
            EntityRepository<UIValidationRule> rulerep = new EntityRepository<UIValidationRule>(false);
            IList<UIValidationRule> ruleList = rulerep.Find(x => x.Active && x.SubSystemId == 1).ToList();

            #region R39
            UIValidationGrouping grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 39.ToString()).ToList().FirstOrDefault();
            if (grouping != null)
            {
                R39_ArchiveCalculationKaheshi(grouping.Active);
            }
            if (exception.Count > 0)
                throw exception;
            #endregion
        }
        public void DoValidate(object obj)
        {
            try
            {
                decimal personId = 0;

                Type classtype = obj.GetType();
                /*1*/
                if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
                {
                    personId = ((GTS.Clock.Model.Concepts.Permit)obj).Person.ID;
                }
                /*2*/else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
                {
                    personId = (((GTS.Clock.Model.Concepts.ShiftException)obj).Person.ID);
                }
                /*3*/else if (classtype == typeof(GTS.Clock.Model.Concepts.Budget))
                {
                    personId = 0;//(((GTS.Clock.Model.Concepts.Budget)obj).RuleCategory.);
                }
                /*4*/else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
                {
                    personId = (((GTS.Clock.Model.Concepts.LeaveIncDec)obj).Person.ID);
                }
                /*5*/else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
                {
                    personId = (((GTS.Clock.Model.Concepts.LeaveYearRemain)obj).Person.ID);
                }
                /*6*/else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
                {
                    personId = (((GTS.Clock.Model.Concepts.BasicTraffic)obj).Person.ID);
                }
                else
                {
                    personId = BUser.CurrentUser.Person.ID;
                }

                IList<UIValidationGrouping> groupingList = new List<UIValidationGrouping>();
                groupingList = validateRep.GetByPersonId(personId);
                IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.Active && x.SubSystemId == 1).ToList();
                bool r6MustRun = true;

                #region R4
                UIValidationGrouping grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 4.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    r6MustRun = false;
                    R4_LockCalculationFromCurrentMonth(obj, grouping);
                }
                #endregion

                #region R5
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 5.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    r6MustRun = false;
                    R5_LockCalculationFromBeforeMonth(obj, grouping);
                }
                #endregion

                #region R6
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.Active && x.ValidationRule.CustomCode == 6.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    R6_LockCalculationFromDate(obj, grouping);
                }
                else if (!r6MustRun)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationMinOneMustBeActive, "حداقل یکی از قوانین بستن محاسبات باید فعال باشد", ExceptionSrc);
                }
                #endregion

                if (exception.Count > 0)
                    throw exception;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "DoValidate");
                throw ex;
            }
        }


        /// <summary>
        /// محاسبات در روز ___ ماه جاری بسته شود 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="grouping"></param>
        private void R4_LockCalculationFromCurrentMonth(object obj, UIValidationGrouping grouping)
        {
            IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

            #region Checking
            if (Utility.IsEmpty(parameters) || parameters.Count != 2)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 4 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            }
            UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromCurrentMonth")).FirstOrDefault();
            UIValidationRuleParameter paramMonth = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromMonth")).FirstOrDefault();
            if (param == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromCurrentMonth  نظر برای قانون 4 یافت نشد ", ExceptionSrc);
            }
            if (paramMonth == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromMonth  نظر برای قانون 4 یافت نشد ", ExceptionSrc);
            }
            #endregion

            int lockCalculationFromCurrentMonth = Utility.ToInteger(param.Value);
            int lockCalculationFromMonth = Utility.ToInteger(paramMonth.Value);
            PersianDateTime pd = null;
            DateTime ed = new DateTime();

            

            DateTime lockDate = new DateTime();
            

            DateTime changeDate = Utility.GTSMinStandardDateTime;
            Type classtype = obj.GetType();
            Person personObj = null;
            /*if (classtype == typeof(GTS.Clock.Model.Concepts.AssignWorkGroup))
            {
                GTS.Clock.Model.Concepts.AssignWorkGroup assgnWrkGrp = (GTS.Clock.Model.Concepts.AssignWorkGroup)obj;
                changeDate = assgnWrkGrp.FromDate;
            }*/
            if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
            {
                GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;
                changeDate = request.FromDate;
                personObj = request.Person;


            }
            if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
            {
                GTS.Clock.Model.Concepts.Permit permit = (GTS.Clock.Model.Concepts.Permit)obj;
                changeDate = permit.FromDate;
                personObj = permit.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
            {
                GTS.Clock.Model.Concepts.ShiftException shift = (GTS.Clock.Model.Concepts.ShiftException)obj;
                changeDate = shift.Date;
                personObj = shift.Person;

            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
            {
                GTS.Clock.Model.Concepts.LeaveIncDec leaveIncDec = (GTS.Clock.Model.Concepts.LeaveIncDec)obj;
                changeDate = leaveIncDec.Date;
                personObj = leaveIncDec.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
            {
                GTS.Clock.Model.Concepts.LeaveYearRemain leaveYearRemain = (GTS.Clock.Model.Concepts.LeaveYearRemain)obj;
                changeDate = leaveYearRemain.Date;
                personObj = leaveYearRemain.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
            {
                GTS.Clock.Model.Concepts.BasicTraffic traffic = (GTS.Clock.Model.Concepts.BasicTraffic)obj;

                changeDate = traffic.Date;
                personObj = traffic.Person;
            }
            Person person = new PersonRepository().GetById(personObj.ID, false);
            DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, changeDate);
            DateTime requestDate = DateTime.Now;
            if (lockCalculationFromMonth == 0)
            {
                pd = Utility.ToPersianDateTime(dateRangePersonInRequestDate.ToDate);
                ed = dateRangePersonInRequestDate.ToDate;
            }
            else
            {
                pd = Utility.ToPersianDateTime(dateRangePersonInRequestDate.ToDate.AddMonths(1));
                ed = dateRangePersonInRequestDate.ToDate.AddMonths(1);
            }
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, lockCalculationFromCurrentMonth));
            }
            else
            {
                lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year, ed.Month, lockCalculationFromCurrentMonth));
            }
            

            if (requestDate>lockDate && changeDate > Utility.GTSMinStandardDateTime && changeDate <= dateRangePersonInRequestDate.ToDate)
            {
                AddException(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth, " محاسبات بسته شده است");
            }
        }

        /// <summary>
        /// محاسبات در روز ___ ماه بعد بسته شود  
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="grouping"></param>
        private void R5_LockCalculationFromBeforeMonth(object obj, UIValidationGrouping grouping)
        {
            IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

            #region cheking
            if (Utility.IsEmpty(parameters) || parameters.Count != 1)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 5 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            }
            UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromBeforeMonth")).FirstOrDefault();
            if (param == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromBeforeMonth  نظر برای قانون 5 یافت نشد ", ExceptionSrc);
            }
            #endregion

            int LockCalculationFromBeforeMonth = Utility.ToInteger(param.Value);
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime lockDate;
            if (pd.Month > 1)
            {
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, LockCalculationFromBeforeMonth));
            }
            else
            {
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
            }
            DateTime changeDate = Utility.GTSMinStandardDateTime;
            Type classtype = obj.GetType();

            /*if (classtype == typeof(GTS.Clock.Model.Concepts.AssignWorkGroup))
            {
                GTS.Clock.Model.Concepts.AssignWorkGroup assgnWrkGrp = (GTS.Clock.Model.Concepts.AssignWorkGroup)obj;
                changeDate = assgnWrkGrp.FromDate;
            }*/
            if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
            {
                GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;
                changeDate = request.FromDate;
            }
            if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
            {
                GTS.Clock.Model.Concepts.Permit permit = (GTS.Clock.Model.Concepts.Permit)obj;
                changeDate = permit.FromDate;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
            {
                GTS.Clock.Model.Concepts.ShiftException shift = (GTS.Clock.Model.Concepts.ShiftException)obj;
                changeDate = shift.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
            {
                GTS.Clock.Model.Concepts.LeaveIncDec leaveIncDec = (GTS.Clock.Model.Concepts.LeaveIncDec)obj;
                changeDate = leaveIncDec.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
            {
                GTS.Clock.Model.Concepts.LeaveYearRemain leaveYearRemain = (GTS.Clock.Model.Concepts.LeaveYearRemain)obj;
                changeDate = leaveYearRemain.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
            {
                GTS.Clock.Model.Concepts.BasicTraffic traffic = (GTS.Clock.Model.Concepts.BasicTraffic)obj;
                changeDate = traffic.Date;
            }

            if (changeDate > Utility.GTSMinStandardDateTime && changeDate <= lockDate)
            {
                AddException(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth, " محاسبات بسته شده است");
            }
        }

        /// <summary>
        /// محاسبات از تاریخ ______ بسته شود 
        /// اگر قوانین 6و5اجرا نشود این قانون حتما اجرا میشود
        /// پس همیشه باید پارامتر داشته باشد
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="grouping"></param>
        private void R6_LockCalculationFromDate(object obj, UIValidationGrouping grouping)
        {
            IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

            #region cheking
            if (Utility.IsEmpty(parameters) || parameters.Count != 1)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 6 با مقدار مورد انتظار نابرابر است - تاریخ بستن محاسبات ", ExceptionSrc);
            }
            UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
            if (param == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید  - تاریخ بستن محاسبات LockCalculationFromDate  نظر برای قانون 6 یافت نشد ", ExceptionSrc);
            }
            #endregion

            DateTime LockCalculationFromDate = new DateTime();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                LockCalculationFromDate = Utility.ToMildiDate(param.Value);
            }
            else
            {
                LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
            }
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            DateTime changeDate = Utility.GTSMinStandardDateTime;
            Type classtype = obj.GetType();

            /*if (classtype == typeof(GTS.Clock.Model.Concepts.AssignWorkGroup))
            {
                GTS.Clock.Model.Concepts.AssignWorkGroup assgnWrkGrp = (GTS.Clock.Model.Concepts.AssignWorkGroup)obj;
                changeDate = assgnWrkGrp.FromDate;
            }*/
            if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
            {
                GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;
                changeDate = request.FromDate;
            }
            if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
            {
                GTS.Clock.Model.Concepts.Permit permit = (GTS.Clock.Model.Concepts.Permit)obj;
                changeDate = permit.FromDate;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
            {
                GTS.Clock.Model.Concepts.ShiftException shift = (GTS.Clock.Model.Concepts.ShiftException)obj;
                changeDate = shift.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
            {
                GTS.Clock.Model.Concepts.LeaveIncDec leaveIncDec = (GTS.Clock.Model.Concepts.LeaveIncDec)obj;
                changeDate = leaveIncDec.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
            {
                GTS.Clock.Model.Concepts.LeaveYearRemain leaveYearRemain = (GTS.Clock.Model.Concepts.LeaveYearRemain)obj;
                changeDate = leaveYearRemain.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
            {
                GTS.Clock.Model.Concepts.BasicTraffic traffic = (GTS.Clock.Model.Concepts.BasicTraffic)obj;
                changeDate = traffic.Date;
            }

            if (changeDate > Utility.GTSMinStandardDateTime && changeDate <= LockCalculationFromDate)
            {
                AddException(ExceptionResourceKeys.UIValidation_R6_LockCalculationFromDate, " محاسبات بسته شده است");
            }
        }

        /// <summary>
        /// تعداد درخواستهای تردد عادی پرسنل در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R7_MaxTarfficRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 7 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxTrafficRequestInMonth")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxTrafficRequestInMonth  نظر برای قانون 7 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R7TrafficRequestMaxCount, " تعداد درخواست تردد از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R8_TrafficRequest");
                throw ex;
            }
        }

        /// <summary>
        /// درخواست ترددهای عادی تا ___ روز بعد از روز درخواست قابل ثبت باشد
        /// </summary>
        private void R8_TrafficRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 8 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AllowedTrafficRequestAfterTime")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AllowedTrafficRequestAfterTime  نظر برای قانون 8 یافت نشد ", ExceptionSrc);
                    }
                    int afterTelorance = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AllowedTrafficRequestBeforeTime")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AllowedTrafficRequestBeforeTime  نظر برای قانون 8 یافت نشد ", ExceptionSrc);
                    }
                    int beforeTelorance = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.FromDate.Date;
                    if ((DateTime.Now.Date - requestDate).Days > afterTelorance)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R8TrafficRequestDayTimeFinished, "مهلت دادن درخواست تردد به پایان رسیده است");
                    }
                    if ((requestDate - DateTime.Now.Date).Days > beforeTelorance)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R8TrafficRequestDayTimeFinished, "ثبت درخواست برای تاریخ مورد نظر مجاز نمیباشد");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R8_TrafficRequest");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهای مرخصی ساعتی استحقاقی ساعتی از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R9_HourlyLeaveDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 9 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 9 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 9 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R9_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R9_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R9_HourlyLeaveDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهای مرخصی ساعتی استحقاقی روزانه از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R10_DailyLeaveDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 10 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 10 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 10 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R10_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R10_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R10_DailyLeaveDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهای مرخصی بدون حقوق ساعتی از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد  
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R11_HourlyWithoutPayLeaveDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 11 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 11 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 11 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R11_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R11_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R11_HourlyWithoutPayLeaveDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهاي مرخصي بدون حقوق روزانه از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد  
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R12_DailyWithoutPayLeaveDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 12 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 12 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 12 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R12_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R12_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R12_DailyWithoutPayLeaveDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهاي مرخصي استعلاجي ساعتي از___ روز قبل تا ___روز بعد از روز درخواست قابل ثبت باشد   
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R13_HourlyEstelajiLeaveDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 13 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 13 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 13 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R13_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R13_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R13_HourlyEstelajiLeaveDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهاي مرخصي استعلاجي روزانه از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R14_DailyEstelajiLeaveDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 14 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 14 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 14 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R14_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R14_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R14_DailyEstelajiLeaveDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهاي مجوز اضافه کاري  از___ روز قبل تا ___روز بعد از روز درخواست قابل ثبت باشد     
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R15_OverTimeDayOfset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 15 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 15 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 15 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.ToDate.Date;
                    if ((requestDate - DateTime.Now.Date).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R15_Before, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((DateTime.Now.Date - requestDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R15_After, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R15_OverTimeDayOfset");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي استحقاقي ساعتي در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R16_MaxHourlyLeaveCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 16 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 16 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R16RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R16_MaxHourlyLeaveCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي استحقاقي روزانه در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R17_MaxDailyLeaveCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 17 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 17 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R17RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R17_MaxDailyLeaveCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي بي حقوق ساعتي در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R18_MaxHourlyWithoutPayCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 18 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 18 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R18RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R18_MaxHourlyWithoutPayCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي بي حقوق روزانه در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R19_MaxDailyWithoutPayCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 19 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 19 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R19RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R19_MaxDailyWithoutPayCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي استعلاجي ساعتي در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R20_MaxHourlyEstelajiCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 20 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 20 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R20RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R20_MaxHourlyEstelajiCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي استعلاجي روزانه در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R21_MaxDailyEstelajiCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 21 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 21 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R21RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R21_MaxDailyEstelajiCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد ساعات درخواست مجوز اضافه کاري ___ ساعت در ماه مي باشد 
        /// </summary>
        private void R22_MaxOverWorkRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 20 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxHrours")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxHrours  نظر برای قانون 22 یافت نشد ", ExceptionSrc);
                    }
                    int maxHour = Utility.ToInteger(param.Value) * 60;
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    IList<Request> list = requestRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => request.Person), request.Person),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => request.Precard), request.Precard),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => request.FromDate), startMonth, CriteriaOperation.GreaterEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => request.FromDate), endMonth, CriteriaOperation.LessEqThan));
                    var sum1 = from o in list
                               where o.TimeDuration > 0
                               select o.TimeDuration;

                    var sum2 = from o in list
                               where o.TimeDuration == -1000
                               select o.ToTime - o.FromTime;

                    int newDuration = request.TimeDuration > 0 ? request.TimeDuration : request.ToTime - request.FromTime;

                    if (sum1.Sum() + sum2.Sum() + newDuration > maxHour)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R22RequestMaxHrour, " مقدار ساعت درخواست اضافه کاری از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R20_MaxHourlyEstelajiCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستهاي مرخصي استعلاجي روزانه در سال حداکثر ___ عدد باشد
        /// </summary>
        private void R23_MaxDailyEstelajiYearCountRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 23 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 23 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startYear, endYear;


                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startYear = Utility.GetDateOfBeginYear(requestDate, LanguagesName.Parsi);
                        endYear = Utility.GetDateOfEndYear(requestDate, LanguagesName.Parsi);
                    }
                    else
                    {
                        startYear = Utility.GetDateOfBeginYear(requestDate, LanguagesName.English);
                        endYear = Utility.GetDateOfEndYear(requestDate, LanguagesName.English);
                    }
                    int count = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, startYear, endYear);

                    if (count + (request.ToDate - request.FromDate).Days + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R21RequestMaxCount, " مقدار درخواست استعلاجی از حد مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R23_MaxDailyEstelajiCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// کلیه درخواست ها از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد
        /// </summary>
        private void R24_AllRequestOffset(Request request, UIValidationGrouping grouping)
        {
            try
            {
                IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 24 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                }
                UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                if (param == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                }
                int afterTelorance = Utility.ToInteger(param.Value);

                param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                if (param == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 8 یافت نشد ", ExceptionSrc);
                }
                int beforeTelorance = Utility.ToInteger(param.Value);

                DateTime requestDate = request.ToDate.Date;
                if ((DateTime.Now.Date - requestDate).Days > afterTelorance)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R8TrafficRequestDayTimeFinished, "مهلت دادن درخواست به پایان رسیده است");
                }
                if ((requestDate - DateTime.Now.Date).Days > beforeTelorance)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R8TrafficRequestDayTimeFinished, "ثبت درخواست برای تاریخ مورد نظر مجاز نمیباشد");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R24_AllRequestOffset");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد ساعات درخواست مجوز اضافه کاري ___ ساعت در ماه مي باشد 
        /// </summary>
        private void R25_MaxWithoutPayLeaveInMonthRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 20 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxHrours")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxHrours  نظر برای قانون 22 یافت نشد ", ExceptionSrc);
                    }
                    int maxHour = Utility.ToInteger(param.Value) * 60;
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    IList<Request> list = requestRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => request.Person), request.Person),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => request.Precard), request.Precard),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => request.FromDate), startMonth, CriteriaOperation.GreaterEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => request.FromDate), endMonth, CriteriaOperation.LessEqThan));
                    var sum1 = from o in list
                               where o.TimeDuration > 0
                               select o.TimeDuration;

                    var sum2 = from o in list
                               where o.TimeDuration == -1000
                               select o.ToTime - o.FromTime;

                    int newDuration = request.TimeDuration > 0 ? request.TimeDuration : request.ToTime - request.FromTime;

                    if (sum1.Sum() + sum2.Sum() + newDuration > maxHour)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R22RequestMaxHrour, " مقدار ساعت درخواست اضافه کاری از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R20_MaxHourlyEstelajiCountRequest");
                throw ex;
            }
        }

        /// <summary>
        /// حداکثر تعداد درخواست مرخصی ساعت استحقاقی  اول وقت 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R26_RequestMaxAvvalVaght(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 26 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter maxCount = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (maxCount == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 26 یافت نشد ", ExceptionSrc);
                    }
                    UIValidationRuleParameter PeriodTime = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("PeriodAvalVaght")).FirstOrDefault();
                    if (PeriodTime == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید PeriodAvalVaght  نظر برای قانون 26 یافت نشد ", ExceptionSrc);
                    }


                    int maxCountNum = Utility.ToInteger(maxCount.Value);


                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);

                    DateRange dateRange = new BDateRange().GetDateRangePerson(prs, 1011, request.FromDate);
                    startMonth = dateRange.FromDate;
                    endMonth = dateRange.ToDate;


                    prs.InitializeForAccessRules(startMonth, endMonth);
                    BaseShift shift = prs.GetShiftByDate(request.FromDate);
                    if (shift != null && shift.PairCount > 0)
                    {
                        if (shift.Pairs.OrderBy(x => x.From).First().From + Utility.ToInteger(PeriodTime.Value) >= request.FromTime)
                        {
                            int overCount = 1;
                            for (DateTime date = startMonth; date <= endMonth; date = date.AddDays(1))
                            {
                                shift = prs.GetShiftByDate(date);
                                if (shift != null && shift.PairCount > 0)
                                {
                                    int shiftFromTime = shift.Pairs.OrderBy(y => y.From).First().From + Utility.ToInteger(PeriodTime.Value);
                                    int count = requestRepository.Find(x => x.FromDate == date && x.Precard.ID == request.Precard.ID && x.Person.ID == request.Person.ID &&
                                         (x.RequestStatusList.Count == 0 || !x.RequestStatusList.Any(y => y.IsDeleted) || !x.RequestStatusList.Any(y => !y.Confirm)) && x.FromTime <= shiftFromTime).Count();
                                    if (count != 0)
                                    {
                                        overCount++;
                                    }

                                    if (overCount > maxCountNum)
                                    {
                                        AddException(ExceptionResourceKeys.UIValidation_R19RequestMaxCount, " تعداد درخواست مرخصی ساعتی اول وقت از حد مجاز تجاوز کرده است");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R26_RequestMaxAvvalVaght");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواست روزامه در سال ___ عدد مي باشد 
        /// </summary>
        private void R27_MaxCountInYearRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 27 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 27 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startYear, endYear;

                    startYear = Utility.GetDateOfBeginYear(requestDate, BLanguage.CurrentSystemLanguage);
                    endYear = Utility.GetDateOfEndYear(requestDate, BLanguage.CurrentSystemLanguage);

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startYear, endYear);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R21RequestMaxCount, " تعداد درخواست از حداکثر مجاز در سال تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R27_MaxCountInYearRequest");
                throw ex;
            }
        }

        /// <summary>
        /// سقف مقدار درخواست روزانه در هر نوبت ___ روز مي باشد 
        /// </summary>
        private void R28_MaxValueOfRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 28 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("DayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید DayCount  نظر برای قانون 28 یافت نشد ", ExceptionSrc);
                    }
                    int maxDay = Utility.ToInteger(param.Value);

                    if ((request.ToDate - request.FromDate).Days + 1 > maxDay)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R28RequestMaxValue, " مقدار مجموع روز درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R28_MaxValueOfRequest");
                throw ex;
            }
        }

        /// <summary>
        /// اضافه کار دستوری تنها توسط مدیران قابل ثبت باشد و حداکثر آن در ماه ___ ساعت میباشد 
        /// </summary>
        private void R29_DasturyOverworkRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 29 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxDasturyOverwork")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxDasturyOverwork  نظر برای قانون 29 یافت نشد ", ExceptionSrc);
                    }
                    BOperator busOperator = new BOperator();
                    if (!busOperator.IsOperator())
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R27RequestMustBeOperator, " تنها اپراتورها و مدیران مجاز به ثبت این درخواست میباشند");
                    }
                    else
                    {
                        int maxHour = Utility.ToInteger(param.Value) * 60;

                        if (request.TimeDuration > maxHour)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R27RequestMaxHrourDasturyOverwork, " مقدار ساعت درخواست اضافه کاری دستوری از حداکثر مجاز تجاوز کرده است");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R29_DasturyOverworkRequest");
                throw ex;
            }
        }

        /// <summary>
        /// مشخص نمودن محل ماموریت اجباری است 
        /// </summary>
        private void R30_DutyPlaceRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    if (request.DutyPositionID == 0)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R30DutyPlace, " محل ماموریت مشخص نشده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R30_DutyPlaceRequest");
                throw ex;
            }
        }

        /// <summary>
        /// مشخص نمودن نام پزشک اجباری است 
        /// </summary>
        private void R31_DoctorRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    if (request.DoctorID == 0)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R31Doctor, " نام پزشک مشخص نشده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R31_DoctorRequest");
                throw ex;
            }
        }

        /// <summary>
        /// مشخص نمودن نام بیماری اجباری است 
        /// </summary>
        private void R32_IllenssRequest(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    if (request.IllnessID == 0)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R32Illenss, " نام بیماری مشخص نشده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R32_IllenssRequest");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت مرخصي استحقاقي در صورت داشتن مانده مرخصي مجاز است 
        /// </summary>
        private void R33_LeaveRemain(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    int requestValue = 0;
                    int day, min;
                    GTS.Clock.Business.Leave.BRemainLeave BusRemainLeave = new Leave.BRemainLeave();
                    //در صورتی که به خظهای پایین تر منتقل شود منجر به تکرار شدن انتقال مانده مرخصی میشود
                    BusRemainLeave.GetRemainLeaveToEndOfYear(request.Person.ID, Utility.ToPersianDateTime(DateTime.Now).Year, Utility.ToPersianDateTime(DateTime.Now).Month, out day, out min);

                    Person person = new PersonRepository().GetById(request.Person.ID, false);

                    person.InitializeForAccessRules(request.FromDate.AddMonths(-2), request.ToDate.AddMonths(2));

                    Precard precard = new PrecardRepository().GetById(request.Precard.ID, false);
                    object leaveInDay = GetRuleParameter(request.FromDate, person, 3017, "first");
                    int leaveRemainValue = 0;
                    if (leaveInDay != null)
                    {
                        leaveRemainValue = day * Utility.ToInteger(leaveInDay) + min;
                    }
                    if (precard.Code == "41")
                    {
                        if (leaveInDay != null)
                        {
                            int days = (request.ToDate - request.FromDate).Days + 1;
                            requestValue = days * Utility.ToInteger(leaveInDay);
                        }
                    }
                    else if (precard.Code == "21")
                    {
                        requestValue = request.ToTime - request.FromTime;
                    }
                    if (leaveRemainValue < requestValue)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R33LeaveRemain, " ذخیره مرخصی کمتر از مقدار درخواستی است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R33_LeaveRemain");
                throw ex;
            }
        }

        /// <summary>
        /// حداکثر مرخصی ساعتی در ماه
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R34_RequestDescriptionRequierd(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    if (Utility.IsEmpty(request.Description))
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R33LeaveRemain, " توضیح درخواست اجباری است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R34_RequestDescriptionRequierd");
                throw ex;
            }
        }

        /// <summary>
        /// حداکثر تعداد درخواست در روز
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R35_RequestMaxCountInDay(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 35 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter param = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 35 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;

                    int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, requestDate, requestDate);
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R19RequestMaxCount, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R35_RequestMaxCountInDay");
                throw ex;
            }
        }

        /// <summary>
        /// حداقل مدت زمان بازه درخواست ساعتی 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R36_RequestMinLength(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 36 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter minLen = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MinLength")).FirstOrDefault();
                    if (minLen == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MinLength  نظر برای قانون 36 یافت نشد ", ExceptionSrc);
                    }

                    UIValidationRuleParameter avalVaght = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("avalVaght")).FirstOrDefault();
                    if (minLen == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید avalVaght  نظر برای قانون 36 یافت نشد ", ExceptionSrc);
                    }
                    int minLength = Utility.ToInteger(minLen.Value);
                    bool isAvvalVaght = Utility.ToBoolean(avalVaght.Value);
                    if (isAvvalVaght)
                    {
                        Person prs = new PersonRepository(false).GetById(request.Person.ID, false);
                        prs.InitializeForAccessRules(request.FromDate.AddDays(-1), request.ToDate.AddDays(1));
                        BaseShift shift = prs.GetShiftByDate(request.FromDate);
                        if (shift != null && shift.PairCount > 0)
                        {
                            if (shift.Pairs.OrderBy(x => x.From).First().From >= request.FromTime)
                            {
                                if (request.ToTime - request.FromTime < minLength)
                                {
                                    AddException(ExceptionResourceKeys.UIValidation_R19RequestMaxCount, " مدت زمان بازه درخواست از حد مجاز کمتر است");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (request.ToTime - request.FromTime < minLength)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R19RequestMaxCount, " مدت زمان بازه درخواست از حد مجاز کمتر است");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R36_RequestMinLength");
                throw ex;
            }
        }

        private void R37_OperatorRequestMaxCount(Request request, UIValidationGrouping grouping)
        {
            if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
            {
                try
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 37 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter operatorRequestMaxCount = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("OperatorRequestMaxCount")).FirstOrDefault();
                    if (operatorRequestMaxCount == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید OperatorRequestMaxCount برای قانون 37 یافت نشد", ExceptionSrc);
                    }
                    int oprRequestMaxCount = Utility.ToInteger(operatorRequestMaxCount.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startMonth = Utility.GetStartOfPersianMonth(requestDate);
                        endMonth = Utility.GetEndOfPersianMonth(requestDate);
                    }
                    else
                    {
                        startMonth = Utility.GetStartOfMiladiMonth(requestDate);
                        endMonth = Utility.GetEndOfMiladiMonth(requestDate);
                    }

                    int operatorIssuedRequestsCount = this.requestRepository.GetOperatorActiveRequestCount(BUser.CurrentUser.ID, grouping.ValidationGroup, request.Precard.ID, startMonth, endMonth);
                    if (operatorIssuedRequestsCount >= oprRequestMaxCount)
                        AddException(ExceptionResourceKeys.UIValidation_R19RequestMaxCount, "تعداد درخواست اپراتور از حد مجاز تجاوز کرده است");
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R37_OperatorRequestMaxCount");
                    throw ex;
                }
            }

        }


        /// <summary>
        /// حداکثر تعداد درخواست اول وقت 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R38_RequestOverTimeMaxAvvalVaght(Request request, UIValidationGrouping grouping)
        {
            try
            {
                if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParameter> parameters = grouping.RuleParameters;

                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 38 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParameter maxCount = parameters.Where(x => !Utility.IsEmpty(x.KeyName) && x.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (maxCount == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 38 یافت نشد ", ExceptionSrc);
                    }



                    int maxCountNum = Utility.ToInteger(maxCount.Value);


                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;


                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);

                    DateRange dateRange = new BDateRange().GetDateRangePerson(prs, 4005, request.FromDate);
                    startMonth = dateRange.FromDate;
                    endMonth = dateRange.ToDate;

                    prs.InitializeForAccessRules(startMonth, endMonth);
                    BaseShift shift = prs.GetShiftByDate(request.FromDate);
                    if (shift != null && shift.PairCount > 0)
                    {
                        if (shift.Pairs.OrderBy(x => x.From).First().From >= request.FromTime)
                        {
                            int overCount = 1;
                            for (DateTime date = startMonth; date <= endMonth; date = date.AddDays(1))
                            {
                                shift = prs.GetShiftByDate(date);
                                if (shift != null && shift.PairCount > 0)
                                {
                                    int shiftFromTime = shift.Pairs.OrderBy(y => y.From).First().From;
                                    int count = requestRepository.Find(x => x.FromDate == date && x.Precard.ID == request.Precard.ID && x.Person.ID == request.Person.ID &&
                                         (x.RequestStatusList.Count == 0 || !x.RequestStatusList.Any(y => y.IsDeleted) || !x.RequestStatusList.Any(y => !y.Confirm)) && x.FromTime <= shiftFromTime).Count();
                                    if (count != 0)
                                    {
                                        overCount++;
                                    }

                                    if (overCount > maxCountNum)
                                    {
                                        AddException(ExceptionResourceKeys.UIValidation_R38RequestMaxCount, " تعداد درخواست اضافه کار قبل وقت از حد مجاز تجاوز کرده است");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R38_RequestOverTimeMaxAvvalVaght");
                throw ex;
            }
        }

        private void R39_ArchiveCalculationKaheshi(bool active)
        {
            try
            {
                if (active==false)
                {

                    AddException(ExceptionResourceKeys.R39_ArchiveCalculationKaheshi, "قانون آرشیو نتایج محاسبات - کاهشی سپه فعال نیست");

                }
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R39_ArchiveCalculationKaheshi");
                throw ex;
            }

        }
        private void AddException(ExceptionResourceKeys key, string msg)
        {
            exception.Add(new ValidationException(key, msg, ExceptionSrc));
        }

        /// <summary>
        /// پارامتر قانون را از دسته قوانین بر میگرداند
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        private object GetRuleParameter(DateTime currentDate, Person person, int ruleIdentifier, string parameterName)
        {
            if (person.AssignedRuleList != null)
            {
                AssignedRule ar = person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == ruleIdentifier).FirstOrDefault();
                if (ar != null)
                {
                    EntityRepository<AssignRuleParameter> paramRep = new EntityRepository<AssignRuleParameter>();
                    IList<AssignRuleParameter> paramList = paramRep.Find(x => x.Rule.ID == ar.RuleId).ToList();

                    AssignRuleParameter asp = paramList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate).FirstOrDefault();
                    if (asp != null)
                    {
                        RuleParameter parameter = asp.RuleParameterList.Where(x => x.Name.ToLower().Equals(parameterName.ToLower())).FirstOrDefault();

                        if (parameter != null)
                            return parameter.Value;
                    }
                }
            }
            return null;
        }



    }

}
