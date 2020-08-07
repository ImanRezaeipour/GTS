using System;
using System.Collections.Generic;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.CustomeXMLConvertor;
using GTS.Clock.Infrastructure.RepositoryFramework;


namespace GTS.Clock.Model
{
    [XMLConvertorRoot("Rules")]
    public class Rule : BaseRule<RuleParameter>
    {
        #region properties

        public virtual decimal TemplateId
        {
            get;
            set;
        }

        public virtual decimal CategoryId
        {
            get;
            set;
        }

        public virtual IList<AssignRuleParameter> AssignRuleParamList
        {
            get;
            set;
        }

        public virtual RuleCategory Category
        {
            get;
            set;
        }

        public virtual bool HasParameter
        {
            get
            {
                try
                {
                    IRuleRepository repository = GetRuleRepository(false);
                    if (repository.GetTemplateParameterCount(this.TemplateId) > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return false;
            }
        }

        public virtual decimal TypeId
        {
            get;
            set;
        }

        public virtual bool IsForcible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Script value.
        /// </summary>
        public virtual String Script { get; set; }

        /// <summary>
        /// Gets or sets the CSharpCode value.
        /// </summary>
        public virtual String CSharpCode { get; set; }

        /// <summary>
        /// مشخص میکند که از قانون طراحی شده
        /// توسط کاربر است یا خیر
        /// </summary>
        public virtual bool UserDefined { get; set; }

        /// <summary>
        /// Json
        /// </summary>
        public virtual string JsonObject { get; set; }
        
        #endregion

        #region Method

        /// <summary>
        /// برای پاس دادن به 
        /// XMLConvertor
        /// هنگامی که
        /// NHibernate
        /// لیست برای ما تولید میکند
        /// ProxyRule
        /// </summary>
        /// <param name="rule">ProxyRule</param>
        /// <returns></returns>        
        public static Rule CopyRule(Rule rule)
        {
            Rule r = new Rule();
            r.ID = rule.ID;
            r.Name = rule.Name;
            r.Script = rule.Script;
            return r;
        }

        public static List<Rule> CopyRule(List<Rule> ruleList)
        {
            List<Rule> list = new List<Rule>();
            foreach (Rule rule in ruleList)
            {
                Rule r = new Rule();
                r.ID = rule.ID;
                r.Name = rule.Name;
                r.Script = rule.Script;
                list.Add(r);
            }
            return list;
        }

        public static IRuleRepository GetRuleRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IRuleRepository, Rule>(Disconnectedly);
        }

        #endregion



    }
}