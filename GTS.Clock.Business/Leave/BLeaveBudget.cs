using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Leave
{
    /// <summary>
    /// created at: 2012-01-16 2:49:26 PM
    /// by        : 
    /// write your name here
    /// </summary>
    public class BLeaveBudget : BaseBusiness<Budget>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Leave.BLeave";
        private BudgetRepository objectRep = new BudgetRepository();

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(Budget obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(Budget obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            throw new IllegalServiceAccess("دسترسی یه این سرویس مجاز نمیباشد", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Budget obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            throw new IllegalServiceAccess("دسترسی یه این سرویس مجاز نمیباشد", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// درج بدون ترانزاکشن
        /// </summary>
        /// <param name="obj"></param>
        protected override void Insert(GTS.Clock.Model.Concepts.Budget obj)
        {
            try
            {
                objectRep.WithoutTransactSave(obj);
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business-Nhibernate Action");

                throw ex;
            }
        }

        protected void UpdateCFP1(Budget obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                RuleCategory ruleCat = new BRuleCategory().GetByID(obj.RuleCategory.ID);
                foreach (PersonRuleCatAssignment assgn in ruleCat.PersonRuleCatAssignList)
                {
                    decimal personId = assgn.Person.ID;
                    CFP cfp = base.GetCFP(personId);
                    DateTime newCfpDate = obj.Date;
                    if (cfp.ID == 0 || cfp.Date > newCfpDate)
                    {
                        DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(personId);

                        //بسته بودن محاسبات 
                        if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                        {
                            newCfpDate = calculationLockDate.AddDays(1);
                        }

                        base.UpdateCFP(personId, newCfpDate);
                    }
                }
            }
        }

        protected override void UpdateCFP(Budget obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                IList<CFP> cfpList = new List<CFP>();                
                Dictionary<decimal, DateTime> lockDates = new Dictionary<decimal, DateTime>();
                RuleCategory ruleCat = new BRuleCategory().GetByID(obj.RuleCategory.ID);
                IList<CFP> personCfpList = new CFPRepository().GetCFPListByRuleCategory(ruleCat.ID);
                foreach (PersonRuleCatAssignment assign in ruleCat.PersonRuleCatAssignList)
                {
                    DateTime calculationLockDate = DateTime.Now;
                    decimal personId = assign.Person.ID;
                    if (assign.Person.PersonTASpec.UIValidationGroup != null)
                    {
                        decimal uiValidateionGrpId = assign.Person.PersonTASpec.UIValidationGroup.ID;
                        if (!lockDates.ContainsKey(uiValidateionGrpId))
                        {
                            calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(assign.Person.PersonTASpec.UIValidationGroup.ID);
                            lockDates.Add(uiValidateionGrpId, calculationLockDate);
                        }
                        else
                        {
                            calculationLockDate = lockDates[uiValidateionGrpId];
                        }
                    }
                    //CFP cfp = base.GetCFP(personId);
                    CFP cfp = personCfpList.Where(x => x.PrsId == personId).FirstOrDefault();
                    DateTime newCfpDate = Utility.ToMildiDateTime(assign.FromDate);


                    //بسته بودن محاسبات 
                    if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                    {
                        newCfpDate = calculationLockDate.AddDays(1);
                    }
                    if (cfp == null || cfp.ID == 0 || cfp.Date > newCfpDate)
                    {
                        cfp.Date = newCfpDate.Date;
                        cfp.PrsId = personId;
                        cfpList.Add(cfp);
                    }
                }
                base.UpdateCFP(cfpList, false);
            }
        }
        #endregion

        /// <summary>
        /// سهمیه مرخصی یک دسته قانون برای یک سال مشخص را رمیگرداند
        /// </summary>
        /// <param name="ruleCategoryId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public LeaveBudgetProxy GetRuleBudget(decimal ruleCategoryId, int year)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", year));
                    toDate = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    fromDate = new DateTime(year, 1, 1);
                    toDate = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }
                LeaveBudgetProxy proxy = new LeaveBudgetProxy();

                IList<Budget> list = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Budget().Date), fromDate,CriteriaOperation.GreaterEqThan),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new Budget().Date), toDate, CriteriaOperation.LessEqThan),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new Budget().RuleCategory), new RuleCategory() { ID = ruleCategoryId }));               

                if (Utility.IsEmpty(list))
                {
                    proxy.BudgetType = BudgetType.Usual;

                }
                else if (list.Count == 1 && list.First().Type == BudgetType.Usual)
                {
                    Budget bud = list.First();
                    proxy.ID = bud.ID;
                    proxy.Description = bud.Description;
                    proxy.BudgetType = bud.Type;

                    proxy.TotoalDay = bud.Day.ToString();
                    proxy.TotoalTime = Utility.IntTimeToTime(bud.Minute, true);
                }
                else if (list.Count == 12)
                {
                    Budget bud = list.First();
                    proxy.ID = bud.ID;
                    proxy.Description = bud.Description;
                    proxy.BudgetType = BudgetType.PerMonth;

                    proxy.Day1 = list[0].Day.ToString();
                    proxy.Time1 = Utility.IntTimeToTime(list[0].Minute, true);

                    proxy.Day2 = list[1].Day.ToString();
                    proxy.Time2 = Utility.IntTimeToTime(list[1].Minute, true);

                    proxy.Day3 = list[2].Day.ToString();
                    proxy.Time3 = Utility.IntTimeToTime(list[2].Minute, true);

                    proxy.Day4 = list[3].Day.ToString();
                    proxy.Time4 = Utility.IntTimeToTime(list[3].Minute, true);

                    proxy.Day5 = list[4].Day.ToString();
                    proxy.Time5 = Utility.IntTimeToTime(list[4].Minute, true);

                    proxy.Day6 = list[5].Day.ToString();
                    proxy.Time6 = Utility.IntTimeToTime(list[5].Minute, true);

                    proxy.Day7 = list[6].Day.ToString();
                    proxy.Time7 = Utility.IntTimeToTime(list[6].Minute, true);

                    proxy.Day8 = list[7].Day.ToString();
                    proxy.Time8 = Utility.IntTimeToTime(list[7].Minute, true);

                    proxy.Day9 = list[8].Day.ToString();
                    proxy.Time9 = Utility.IntTimeToTime(list[8].Minute, true);

                    proxy.Day10 = list[9].Day.ToString();
                    proxy.Time10 = Utility.IntTimeToTime(list[9].Minute, true);

                    proxy.Day11 = list[10].Day.ToString();
                    proxy.Time11 = Utility.IntTimeToTime(list[10].Minute, true);

                    proxy.Day12 = list[11].Day.ToString();
                    proxy.Time12 = Utility.IntTimeToTime(list[11].Minute, true);
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.LeaveBudgetRecordsCountInDatabaseIsNotValid, String.Format("تعداد رکورد های بودجه مرخصی در دیتابیس برای گروه قانون {0} و  سال {1} نامعتبر است", ruleCategoryId, year), ExceptionSrc);
                }
                return proxy;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BLeave", "GetRuleBudget");
                throw ex;
            }
        }

        /// <summary>
        /// سهممیه سالیانه یک دسته قانون را ذخیره میکند
        /// اگر برای سال مشخص شده در 
        /// دیتابیس رکورد موجود باشد آنرا حذف میکنبیم و دوباره درج میکنیم
        /// شکل عادی را به شکل خاص ذخیره میکند(کارکس) ا
        /// </summary>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool SaveBudget(decimal ruleCategoryId, int year, LeaveBudgetProxy proxy)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", year));
                        toDate = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                    }
                    else
                    {
                        fromDate = new DateTime(year, 1, 1);
                        toDate = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                    }

                    objectRep.DeleteExistingBudget(ruleCategoryId, fromDate, toDate);

                 
                    if (proxy.BudgetType == BudgetType.Usual)
                    {
                        Budget budget = new Budget();
                        budget.Date = fromDate;
                        budget.RuleCategory = new RuleCategory() { ID = ruleCategoryId };
                        budget.Type = BudgetType.Usual;
                        budget.Description = proxy.Description;
                        budget.Day = Utility.ToInteger(proxy.TotoalDay);
                        budget.Minute = Utility.RealTimeToIntTime(proxy.TotoalTime);
                        this.SaveChanges(budget, UIActionType.ADD);
                       //
                       /* int day = Utility.ToInteger(proxy.TotoalDay) / 12;
                        int minutes = ((Utility.ToInteger(proxy.TotoalDay) - day) * 480) / 12;
                        for (int i = 0; i < 12; i++)
                        {
                            Budget budget = new Budget();
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                budget.Date = Utility.ToMildiDate(String.Format("{0}/{1}/01", year, i + 1));
                            }
                            else
                            {
                                budget.Date = fromDate.AddMonths(i);
                            }
                            //budget.Date = fromDate.AddMonths(i);
                            budget.RuleCategory = new RuleCategory() { ID = ruleCategoryId };
                            budget.Type = BudgetType.PerMonth;
                            budget.Description = Utility.IsEmpty(proxy.Description) ? String.Empty : proxy.Description;
                            budget.Day = day;
                            budget.Minute = minutes;
                            this.SaveChanges(budget, UIActionType.ADD);
                        }*/                      
                    }
                    else if(proxy.BudgetType == BudgetType.PerMonth)
                    {
                        for (int i = 0; i < 12; i++) 
                        {
                            Budget budget = new Budget();
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                budget.Date = Utility.ToMildiDate(String.Format("{0}/{1}/01", year, i + 1));
                            }
                            else
                            {
                                budget.Date = fromDate.AddMonths(i);
                            }
                            //budget.Date = fromDate.AddMonths(i);
                            budget.RuleCategory = new RuleCategory() { ID = ruleCategoryId };
                            budget.Type = BudgetType.PerMonth;
                            budget.Description = Utility.IsEmpty(proxy.Description) ? String.Empty : proxy.Description;
                            budget.Day =  proxy[i].Day;
                            budget.Minute = proxy[i].Minute;
                            this.SaveChanges(budget, UIActionType.ADD);
                        }
                    }                  

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BLeave", "SaveBudget");
                    throw ex;
                }
            }
        }
    }
}
