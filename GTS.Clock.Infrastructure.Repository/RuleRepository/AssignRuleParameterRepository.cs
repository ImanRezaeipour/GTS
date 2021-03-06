using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;


namespace GTS.Clock.Infrastructure.Repository
{
    public class AssignRuleParameterRepository : RepositoryBase<AssignRuleParameter>, IAssignRuleParameterRepository
    {
        public override string TableName
        {
            get { return "TA_AssignRuleParameter"; }
        }

        public AssignRuleParameterRepository(bool disconectly) 
            : base(disconectly) 
        { }

        /// <summary>
        /// بعد از درج انتساب قانون به پارامتر باید پارامترها نیز درچ شوند که 
        /// این سرویس این کار را انجام میدهد
        /// </summary>
        /// <param name="assignRuleParameterID">شناسه انتساب قانون به پارامتر</param>
        /// <param name="ruleID">شناسه قانون</param>
        public void InitilizeParameters(decimal assignRuleParameterID, decimal ruleID)
        {
            decimal[] a = new decimal[1] { ruleID };

            IList<RuleTemplateParameter> list = base.NHibernateSession.QueryOver<RuleTemplateParameter>()
                                                        .WhereRestrictionOn(x => x.RuleTemplateId)
                                                        .IsInG<Object>(NHibernateSession.QueryOver<Rule>()
                                                                                        .Select(Projections.ProjectionList()
                                                                                                .Add(Projections.Property<Rule>(r => r.TemplateId)))
                                                                                        .WhereRestrictionOn(x => x.ID)
                                                                                        .IsIn(a)
                                                                                        .List<object>())
                                                        .List<RuleTemplateParameter>();

            EntityRepository<RuleParameter> paramRep = new EntityRepository<RuleParameter>(false);

            foreach (RuleTemplateParameter param in list)
            {
                RuleParameter ruleParam = new RuleParameter();
                ruleParam.Name = param.Name;
                ruleParam.Title = param.Title;
                ruleParam.Type = param.Type;
                ruleParam.AssignRuleParameter = new AssignRuleParameter() { ID = assignRuleParameterID };
                if (param.Type == RuleParamType.numeric || param.Type == RuleParamType.Time)
                {
                    ruleParam.Value = "0";
                }
                else
                    ruleParam.Value = String.Format("{0:yyyy/mm/dd}", Utility.Utility.GTSMinStandardDateTime);

                paramRep.Save(ruleParam);
            }
        }

        /// <summary>
        /// با توجه به شناسه قانون پارامترهای قالب را برمیگرداند
        /// </summary>
        /// <param name="ruleID"></param>
        /// <returns></returns>
        public IList<RuleTemplateParameter> GetRuleTemplateParameters(decimal ruleID) 
        {
            decimal[] a = new decimal[1] { ruleID };

            IList<RuleTemplateParameter> list = base.NHibernateSession.QueryOver<RuleTemplateParameter>()
                                                        .WhereRestrictionOn(x => x.RuleTemplateId)
                                                        .IsInG<Object>(NHibernateSession.QueryOver<Rule>()                                                                
                                                                                        .Select(Projections.ProjectionList()
                                                                                        .Add(Projections.Property<Rule>(r => r.TemplateId)))
                                                                                        .WhereRestrictionOn(x => x.ID)
                                                                                        .IsIn(a)
                                                                                        .List<object>())
                                                        .List<RuleTemplateParameter>();

            return list;
        }

        public IList<AssignRuleParameter> GetAssigneRuleParametersListByRuleID(decimal ruleID)
        {
            IList<AssignRuleParameter> AssignRuleParametersList = NHibernateSession.QueryOver<AssignRuleParameter>()
                                                                    .JoinQueryOver<Rule>(assignRuleParameter => assignRuleParameter.Rule)
                                                                    .Where(rule => rule.ID == ruleID)
                                                                    .List<AssignRuleParameter>();
            return AssignRuleParametersList;                                         
        }

       
    }
}
