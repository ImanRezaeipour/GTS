using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Business.Rules
{
    public class BRuleTemp : BaseBusiness<RuleTemplate>
    {
        private readonly EntityRepository<RuleTemplate> _ruleRep;//= new EntityRepository<RuleTemplate>();
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleTemp";

        private BRuleType bRuleType;// = new BRuleType();
        private Dictionary<decimal, string> RuleTypeList;// = new Dictionary<int, string>();

        public BRuleTemp()
        {
            _ruleRep = new EntityRepository<RuleTemplate>();

            bRuleType = new BRuleType();
            RuleTypeList = new Dictionary<decimal, string>();
            foreach (var ruleType in bRuleType.GetAll())
                RuleTypeList.Add(ruleType.ID, ruleType.Name);
        }


        public decimal InsertRule(RuleTemplate ruleRecived)
        {
            try
            {
                return this.SaveChanges(ruleRecived, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal UpdateRule(RuleTemplate ruleRecived)
        {
            try
            {
                return this.SaveChanges(ruleRecived, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal DeleteRule(RuleTemplate ruleRecived)
        {
            try
            {
                return this.SaveChanges(ruleRecived, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void InsertValidate(RuleTemplate obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (GetAll().Any(x => x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "نام تكراري است", ExceptionSrc);

            if (GetAll().Any(x => x.IdentifierCode.Equals(obj.IdentifierCode)))
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "كد تكراري است", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }

        }
        protected override void UpdateValidate(RuleTemplate obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (_ruleRep.GetAll().Any(x => x.ID != obj.ID && x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "نام تكراري است", ExceptionSrc);

            if (_ruleRep.GetAll().Any(x => x.ID != obj.ID && x.IdentifierCode.Equals(obj.IdentifierCode)))
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "كد تكراري است", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void DeleteValidate(RuleTemplate obj)
        {

        }

        private void GeneralValidation(RuleTemplate obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (string.IsNullOrEmpty(obj.Name))
                exception.Add(ExceptionResourceKeys.BRuleNameRepeated, "نام اجباري است", ExceptionSrc);

            if (obj.IdentifierCode < 1)
                exception.Add(ExceptionResourceKeys.BRuleCodeRequierd, "كد اجباري است", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        public int GetAllByPageBySearchCount(string searchTerm)
        {
            var count = 0;

            IEnumerable<RuleTemplate> queryOnRule = null;

            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnRule =
                        _ruleRep.Find(rule =>
                        rule.UserDefined);
                }
                else
                {
                    var allRule =
                         _ruleRep.GetAll();

                    queryOnRule = allRule.Where(rule =>
                             rule.UserDefined &&
                             (
                               rule.Name.Contains(searchTerm) ||
                               rule.IdentifierCode.ToString(CultureInfo.InvariantCulture).Contains(searchTerm)
                             )
                             );
                }

                count = 0;
                if (queryOnRule.FirstOrDefault() != null) count = queryOnRule.Count();
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.Rules.BRule", "GetAllByPageBySearchCount(ruleSearchKeys searchKey, string searchTerm)");
                throw ex;
            }
            return count;
        }

        public IList<RuleTemplateProxy> GetAllByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<RuleTemplate> queryOnRule = null;
            try
            {

                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnRule =
                        _ruleRep.Find(ruleTmp =>
                        ruleTmp.UserDefined);
                }
                else
                {
                    var allRule =
                         _ruleRep.GetAll();

                    queryOnRule = allRule.Where(rule =>
                             rule.UserDefined &&
                             (
                               rule.Name.Contains(searchTerm) ||
                               rule.IdentifierCode.ToString(CultureInfo.InvariantCulture).Contains(searchTerm)
                             )
                             );
                }

                queryOnRule =
                    queryOnRule
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.Rules.BRule", "GetAllByPageBySearch");
                throw ex;
            }


            var RuleTemplateProxyList = queryOnRule.Select(ruleTemplate => new RuleTemplateProxy(ruleTemplate) { Type = RuleTypeList[ruleTemplate.TypeId] }).ToList();

            return RuleTemplateProxyList;
        }

        public void Copy(RuleTemplate RuleTemplateFrom, ref RuleTemplate RuleTemplateTo)
        {
            RuleTemplateTo.IdentifierCode = RuleTemplateFrom.IdentifierCode;
            RuleTemplateTo.Name = RuleTemplateFrom.Name;
            RuleTemplateTo.CustomCategoryCode = RuleTemplateFrom.CustomCategoryCode;
            RuleTemplateTo.TypeId = RuleTemplateFrom.TypeId;
            RuleTemplateTo.UserDefined = RuleTemplateFrom.UserDefined;
            RuleTemplateTo.Script = RuleTemplateFrom.Script;
            RuleTemplateTo.CSharpCode = RuleTemplateFrom.CSharpCode;
            RuleTemplateTo.JsonObject = RuleTemplateFrom.JsonObject;
        }

    }
}
