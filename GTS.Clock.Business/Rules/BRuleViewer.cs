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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Rules
{
    /// <summary>
    /// وظیفه:نمایش دسته قوانین - قوانین - دینج پارامترها - پارامترها
    /// این کلاس سرویسی بابت ویرایش - حذف و درج ارائه نمیدهد
    /// </summary>
    public class BRuleViewer : MarshalByRefObject
    {
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleViewer";

        private RuleCategory workingRuleCategory;

        /// <summary>
        /// شناسه گروه قانون بمنظور استفاده در عملیات دریافت میگردد
        /// </summary>
        /// <param name="ruleCategoryId"></param>
        public BRuleViewer(decimal ruleCategoryId)
        {
            RuleCategoryRepository repository = new RuleCategoryRepository(false);
            IList<RuleCategory> list = repository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleCategory().ID), ruleCategoryId));
            if (list.Count == 1)
            {
                workingRuleCategory = list[0];
            }
            else
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.RuleCategoryIdIsInValid, "پارامتر ارسال شده معرف دسته قانون نمیباشد", ExceptionSrc);
                throw exception;
            }
        }

        /// <summary>
        /// دسته بندی قوانین را از حیث اضافه کار - مرخصی و ... برمیگرداند
        /// </summary>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<RuleType> GetAllRuleTypes()
        {
            BRuleType businessRuleType = new BRuleType();
            return businessRuleType.GetAll();
        }

        /// <summary>
        /// قوانین یک دسته نوع قانون مثلا اضافه کار را برمیگرداند
        /// </summary>
        /// <param name="ruleTypeId"></param>
        /// <returns></returns>
        public IList<Rule> GetAllRules(decimal ruleTypeId)
        {
            RuleRepository repository = new RuleRepository(false);
            IList<Rule> list = repository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Rule().TypeId), ruleTypeId)
                                                            , new CriteriaStruct(Utility.GetPropertyName(() => new Rule().Category), workingRuleCategory));
            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(x => x.IdentifierCode).ToList();
            }
            return list;
        }

        /// <summary>
        /// رینج پارامترهای یک قانون را برمیگرداند
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public IList<AssignRuleParameter> GetAllRuleParametersRange(decimal ruleId)
        {
            BRule businessRule = new BRule();
            Rule rule = businessRule.GetByID(ruleId);
            IList<AssignRuleParameter> list = rule.AssignRuleParamList;
            foreach (AssignRuleParameter assign in list)
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    assign.TheFromDate = Utility.ToPersianDate(assign.FromDate);
                    assign.TheToDate = Utility.ToPersianDate(assign.ToDate);
                }
                else if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                {
                    assign.TheFromDate = Utility.ToString(assign.FromDate);
                    assign.TheToDate = Utility.ToString(assign.ToDate);
                }
            }
            return list;
        }

        /// <summary>
        /// پارامترهای قانون را به ازای رینج پارامتر مشخص شده برمیگرداند
        /// </summary>
        /// <param name="assignRuleParameter">شناسه رینج پارامتر</param>
        /// <returns></returns>
        public IList<RuleParameter> GetAllRuleParameters(decimal assignRuleParameterId) 
        {
            EntityRepository<RuleParameter> rep = new EntityRepository<RuleParameter>(false);
            IList<RuleParameter> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleParameter().AssignRuleParameter), new AssignRuleParameter() { ID = assignRuleParameterId }));
            list = list.Select(x => { x.Value = x.Type == RuleParamType.Date ? new BLanguage().GetSysDateString(DateTime.Parse(x.Value)) : x.Type == RuleParamType.Time ? this.CreateTimeString_RuleParameters(x.Value) : x.Value; return x; }).ToList();
            return list;
        }

        private string CreateTimeString_RuleParameters(string strTime)
        {
            string retStrTime = string.Empty;
            int hour = 0;
            int min = 0;
            string strHour = string.Empty;
            string strMin = string.Empty;
            hour = Math.DivRem(int.Parse(strTime), 60, out min);
            strHour = hour.ToString();
            if (strHour.Length < 2)
                strHour = "0" + strHour;
            strMin = min.ToString();
            if (strMin.Length < 2)
                strMin = "0" + strMin;
            retStrTime = strHour + ":" + strMin;
            return retStrTime;
        }

    }
}
