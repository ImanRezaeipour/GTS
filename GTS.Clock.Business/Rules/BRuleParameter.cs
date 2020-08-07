using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Rules
{
    public class BRuleParameter : BaseBusiness<AssignRuleParameter>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleParameter";
        private Rule workingRule;
        AssignRuleParameterRepository assignRuleParameterRepository = new AssignRuleParameterRepository(false);

        /// <summary>
        /// ازروی شناسه قانون غالب و شناسه گروه قانون , قانون را استخراج میکند
        /// </summary>
        /// <param name="ruleTmpId">شناسه قانون قالب</param>
        /// <param name="ruleCategoryCode">شناسه گروه قانون</param>
        public BRuleParameter(decimal ruleTmpId,decimal ruleCategoryCode)
        {
            IList<Rule> list = new RuleRepository(false).GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Rule().TemplateId), ruleTmpId),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new Rule().Category), new RuleCategory() { ID = ruleCategoryCode }));
            if (list.Count == 1)
            {
                workingRule = list.First();
            }
            else if (list.Count == 0)
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.AssignParameterRuleIDInvalid, "پارامترهای ارسال شده معرف قانونی نمیباشد", ExceptionSrc);
                throw exception;
            }
            else
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.AssignParameterRuleIDInvalid, "پارامترهای ارسال شده معرف بیش از یک قانون میباشد", ExceptionSrc);
                throw exception;
            }
        }

        /// <summary>
        ///کلیه انتسابهای قانون به پارامتر را با توجه به شناسه قانون دریافت شده در سازنده برمیگرداند  
        ///اگر خالی باشد درج میگرد
        /// </summary>
        /// <returns></returns>
        public override IList<AssignRuleParameter> GetAll()
        {
            try
            {
                IList<AssignRuleParameter> list = assignRuleParameterRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignRuleParameter().Rule), workingRule));
                if (list == null || list.Count == 0) 
                {

                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleParameter", "GetAll");
                throw ex;
            }
        }

        /// <summary>
        /// ابتدا و انتها معتبر باشد
        /// ابتدا از انها کوچکتر باشد
        /// بررسی همپوشانی
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(AssignRuleParameter assignRuleParameter)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (assignRuleParameter.FromDate == null || assignRuleParameter.FromDate <= new DateTime(1921, 3, 21) ||
                assignRuleParameter.ToDate == null || assignRuleParameter.ToDate <= new DateTime(1921, 3, 21))
            {
                exception.Add(ExceptionResourceKeys.AssignParameterDateIsInvalid, "تاریخ ابتدا و انتها باید معتبر باشد", ExceptionSrc);
            }
            else if (assignRuleParameter.FromDate == assignRuleParameter.ToDate)
            {
                exception.Add(ExceptionResourceKeys.AssignParameterFromDateAndToDateAreEquals, "تاریخ ابتدا از انتها بزرگتر است", ExceptionSrc);
            }
            else if (assignRuleParameter.FromDate > assignRuleParameter.ToDate)
            {
                exception.Add(ExceptionResourceKeys.AssignParameterFromDateGreaterThanToDate, "تاریخ ابتدا از انتها بزرگتر است", ExceptionSrc);
            }
            else
            {
                Rule rule = workingRule;
                if (rule.AssignRuleParamList != null)
                {
                    IList<DateTime> fromList = rule.AssignRuleParamList.Select(x => x.FromDate).ToList<DateTime>();
                    IList<DateTime> toList = rule.AssignRuleParamList.Select(x => x.ToDate).ToList<DateTime>();

                    if (HasIntersect(fromList, toList, assignRuleParameter.FromDate, assignRuleParameter.ToDate))
                    {
                        exception.Add(ExceptionResourceKeys.AssignParameterDateHasIntersect, "محدوده انتسابها نباید با هم اشتراک داشته باشد", ExceptionSrc);
                    }
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignRuleParameter"></param>
        protected override void UpdateValidate(AssignRuleParameter assignRuleParameter)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (assignRuleParameter.FromDate == null || assignRuleParameter.FromDate <= new DateTime(1921, 3, 21) ||
                assignRuleParameter.ToDate == null || assignRuleParameter.ToDate <= new DateTime(1921, 3, 21))
            {
                exception.Add(ExceptionResourceKeys.AssignParameterDateIsInvalid, "تاریخ ابتدا و انتها باید معتبر باشد", ExceptionSrc);
            }
            else if (assignRuleParameter.FromDate >= assignRuleParameter.ToDate)
            {
                exception.Add(ExceptionResourceKeys.AssignParameterFromDateGreaterThanToDate, "تاریخ ابتدا از انتها بزرگتر است", ExceptionSrc);
            }
            else
            {
                IList<AssignRuleParameter> list = assignRuleParameterRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignRuleParameter().Rule), workingRule),
                                                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new AssignRuleParameter().ID), assignRuleParameter.ID, CriteriaOperation.NotEqual));              

                IList<DateTime> fromList = list.Select(x => x.FromDate).ToList<DateTime>();
                IList<DateTime> toList = list.Select(x => x.ToDate).ToList<DateTime>();

                if (HasIntersect(fromList, toList, assignRuleParameter.FromDate, assignRuleParameter.ToDate))
                {
                    exception.Add(ExceptionResourceKeys.AssignParameterDateHasIntersect, "محدوده انتسابها نباید با هم اشتراک داشته باشد", ExceptionSrc);
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// عملیات حذف نداریم
        /// </summary>
        /// <param name="assignRuleParameter"></param>
        protected override void DeleteValidate(AssignRuleParameter assignRuleParameter)
        {
            //throw new IllegalServiceAccess("عملیات حذف برای انتساب پارامترها تعریف نشده است", ExceptionSrc);
        }
      
        protected override void UpdateCFP(AssignRuleParameter obj, UIActionType action)
        {
            if (action != UIActionType.ADD)
            {
                IList<CFP> cfpList = new List<CFP>();
                Dictionary<decimal, DateTime> lockDates = new Dictionary<decimal, DateTime>();
                foreach (PersonRuleCatAssignment assgn in obj.Rule.Category.PersonRuleCatAssignList)
                {
                    DateTime calculationLockDate = DateTime.Now;
                    decimal personId = assgn.Person.ID;
                    if (assgn.Person.PersonTASpec.UIValidationGroup != null)
                    {
                        decimal uiValidateionGrpId = assgn.Person.PersonTASpec.UIValidationGroup.ID;
                        if (!lockDates.ContainsKey(uiValidateionGrpId))
                        {
                            calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(assgn.Person.PersonTASpec.UIValidationGroup.ID);
                            lockDates.Add(uiValidateionGrpId, calculationLockDate);
                        }
                        else
                        {
                            calculationLockDate = lockDates[uiValidateionGrpId];
                        }
                    }
                    CFP cfp = base.GetCFP(personId);
                    DateTime newCfpDate = Utility.ToMildiDateTime(assgn.FromDate);


                    //بسته بودن محاسبات 
                    if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                    {
                        newCfpDate = calculationLockDate.AddDays(1);
                    }
                    if (cfp.ID == 0 || cfp.Date > newCfpDate)
                    {
                        cfp.Date = newCfpDate.Date;
                        cfp.PrsId = personId;
                        cfpList.Add(cfp);
                    }
                }
                base.UpdateCFP(cfpList, false);
            }
        }

        /// <summary>
        /// رینج تاریخ پارامتر را بهمراه پارامتر دخیره میکند
        /// اگر تعداد پارامتر ارسالی کمتر از تعداد غالب بود آنگاه پارامترهای پیشفرض ذخیره میگردد
        /// بدلیل انجام عملیات جانبی تباید کلاینت مستقیما از متد 
        /// SaveChnages استفاده کند
        /// </summary>
        /// <param name="parameters">پارامترهای مقداردهی شده از طرف کلاینت</param>
        /// <param name="fromDate">تاریخ شروع رینج</param>
        /// <param name="toDate">تاریخ پایان رینج</param>
        /// <returns>شناسه رینج پارامتر</returns>
        public decimal InsertParameter(IList<RuleTemplateParameter> parameters, DateTime fromDate, DateTime toDate)
        {
            try
            {
                AssignRuleParameter assign = new AssignRuleParameter();
                assign.FromDate = fromDate;
                assign.ToDate = toDate;
                assign.Rule = workingRule;
                assign.RuleParameterList = new List<RuleParameter>();

                IList<RuleTemplateParameter> orginList = this.GetTemplateParameters();
                foreach (RuleTemplateParameter param in orginList)
                {                                       
                    if (parameters.Where(x => x.ID == param.ID).Count() > 0)
                    {
                        RuleTemplateParameter p = parameters.Where(x => x.ID == param.ID).First();
                        if (p.Type == RuleParamType.Time && p.ContinueOnTomorrow)
                            p.Value = Utility.ToString(Utility.ToInteger(p.Value) + 1440);
                        assign.RuleParameterList.Add(new RuleParameter() { Name = p.Name, Title = p.Title, Type = p.Type, Value = p.Value, AssignRuleParameter = assign });
                    }
                    else
                    {
                        assign.RuleParameterList.Add(new RuleParameter() { Name = param.Name, Title = param.Title, Type = param.Type, Value = param.Value, AssignRuleParameter = assign });
                    }
                }
                base.SaveChanges(assign, UIActionType.ADD);

                return assign.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleParameter", "InsertParameter");
                throw ex;
            }
        }

        /// <summary>
        /// رینج تاریخ پارامتر را بهمراه پارامتر بروزرسانی میکند
        /// اگر تعداد پارامتر ارسالی کمتر از تعداد ذخیره شده در دیتابیس کمتر بود آنگاه پارامترهای موجود در دیتابیس ذخیره میگردد
        /// بدلیل انجام عملیات جانبی تباید کلاینت مستقیما از متد 
        /// SaveChnages استفاده کند
        /// </summary>
        /// <param name="parameters">پارامترهای مقداردهی شده از طرف کلاینت - کلیه پارامتر ها باید دارای شناسه معتبر باشند</param>
        /// <param name="assignParamId">شناسه رینج تاریخ پارامترها</param>
        /// <param name="fromDate">تاریخ شروع رینج</param>
        /// <param name="toDate">تاریخ پایان رینج</param>
        /// <returns>شناسه رینج پارامتر</returns>
        public decimal UpdateParameter(IList<RuleParameter> parameters,decimal assignParamId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                AssignRuleParameter assign = new AssignRuleParameter();
                assign.ID = assignParamId;
                assign.FromDate = fromDate;
                assign.ToDate = toDate;
                assign.Rule = workingRule;
                assign.RuleParameterList = new List<RuleParameter>();

                IList<RuleParameter> orginList = this.GetRuleParameters(assignParamId);
                foreach (RuleParameter param in orginList)
                {
                    if (parameters.Where(x => x.ID == param.ID).Count() == 1)
                    {
                        RuleParameter p = parameters.Where(x => x.ID == param.ID).First();
                        if (p.Type == RuleParamType.Time && p.ContinueOnTomorrow)
                            p.Value = Utility.ToString(Utility.ToInteger(p.Value) + 1440);
                        param.Value  = p.Value;
                        param.ContinueOnTomorrow = p.ContinueOnTomorrow;
                        assign.RuleParameterList.Add(param);
                    }
                    else
                    {
                        assign.RuleParameterList.Add(param);
                    }
                }
               
                base.SaveChanges(assign, UIActionType.EDIT);

                return assign.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleParameter", "UpdateParameter");
                throw ex;
            }
        }

        /// <summary>
        /// وظیفه حذف را بر عهده دارد
        /// </summary>
        /// <param name="assignParamId"></param>
        public bool DeleteParameter(decimal assignParamId)
        {
            try
            {
                AssignRuleParameter assign = new AssignRuleParameter();
                assign.ID = assignParamId;
                base.SaveChanges(assign, UIActionType.DELETE);
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleParameter", "DeleteParameter");
                throw ex;
            }
        }

        private bool HasIntersect(IList<DateTime> fromList, IList<DateTime> toList, DateTime from, DateTime to)
        {
            for (int i = 0; i < fromList.Count; i++)
            {
                if ((fromList[i] <= from && toList[i] >= to) ||
                    (fromList[i] >= from && toList[i] <= to) ||
                    (fromList[i] >= from && fromList[i] <= to) ||
                    (toList[i] >= from && toList[i] <= to))
                {
                    return true;
                }
            }
            return false;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<AssignRuleParameter> GetAllAssignedRuleParameter_onRuleGroupInsert()
        {
            return this.GetAll();
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<AssignRuleParameter> GetAllAssignedRuleParameter_onRuleGroupUpdate()
        {
            return this.GetAll();
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertRuleParameter_onRuleGroupInsert(IList<RuleTemplateParameter> parametersList, DateTime fromDate, DateTime toDate)
        {
           return this.InsertParameter(parametersList, fromDate, toDate);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateRuleParameter_onRuleGroupInsert(IList<RuleParameter> parametersList, decimal ruleDateRangeID, DateTime fromDate, DateTime toDate)
        {
            return this.UpdateParameter(parametersList, ruleDateRangeID, fromDate, toDate);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteRuleParameter_onRuleGroupInsert(decimal ruleDateRangeID)
        {
            return this.DeleteParameter(ruleDateRangeID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertRuleParameter_onRuleGroupUpdate(IList<RuleTemplateParameter> parametersList, DateTime fromDate, DateTime toDate)
        {
            return this.InsertParameter(parametersList, fromDate, toDate);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateRuleParameter_onRuleGroupUpdate(IList<RuleParameter> parametersList, decimal ruleDateRangeID, DateTime fromDate, DateTime toDate)
        {
            return this.UpdateParameter(parametersList, ruleDateRangeID, fromDate, toDate);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteRuleParameter_onRuleGroupUpdate(decimal ruleDateRangeID)
        {
            return this.DeleteParameter(ruleDateRangeID);
        }


        #region Parameters

        /// <summary>
        ///  پارامترهای قالب را برای قانونی که در سازنده کلاس داده شده است برمیگرداند 
        /// </summary>
        /// <returns></returns>
        public IList<RuleTemplateParameter> GetTemplateParameters()
        {
            try
            {
                return assignRuleParameterRepository.GetRuleTemplateParameters(workingRule.ID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleParameter", "GetTemplateParameters");
                throw ex;
            }
        }

        /// <summary>
        /// پارامترهای اصلی را برمیگرداند
        /// </summary>
        /// <param name="assignRuleParameterID"></param>
        /// <returns></returns>
        public IList<RuleParameter> GetRuleParameters(decimal assignRuleParameterID)
        {
            try
            {
                EntityRepository<RuleParameter> rep = new EntityRepository<RuleParameter>(false);
                IList<RuleParameter> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleParameter().AssignRuleParameter), new AssignRuleParameter() { ID = assignRuleParameterID }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleParameter", "GetRuleParameters");
                throw ex;
            }
        }

        #endregion
    }
}
