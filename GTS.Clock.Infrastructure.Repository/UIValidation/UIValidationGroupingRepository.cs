using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.UIValidation;
using NHibernate.Transform;
using GTS.Clock.Model;

namespace GTS.Clock.Infrastructure.Repository
{
    public class UIValidationGroupingRepository : RepositoryBase<UIValidationGrouping>
    {

        public override string TableName
        {
            get { return "TA_UIValidationGrouping"; }
        }

        /// <summary>
        /// بعد از بروزرسانی یگ گروه اعتبارسنجی و تخصیص قوانین جدید لازم است قوانین قبلی حذف و قوانین جدید
        /// جایگزین قبلی شود
        /// </summary>
        /// <param name="validationGroupId"></param>
        /// <param name="newRuleIds"></param>
        public void DeleteAfterUpdate(decimal validationGroupId, IList<decimal> newRuleIds)
        {
            string SQLCommand = @"Delete From TA_UIValidationGrouping where UIGrp_GroupID=:groupId
                                  and UIGrp_RuleID not in (:newRuleIds)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("groupId", validationGroupId)
                .SetParameterList("newRuleIds", base.CheckListParameter(newRuleIds))
                .ExecuteUpdate();
        }

        public decimal GetByPersonIdAndRuleCode(decimal personId, int ruleCode)
        {
            IList<decimal> kk = new List<decimal>();
            UIValidationGrouping grouping = null;
            UIValidationGroup group = null;
            UIValidationRule rule = null;
            PersonTASpec person = null;

            IList<UIValidationGrouping> list = base.NHibernateSession.QueryOver<UIValidationGrouping>(() => grouping)
                                          .JoinAlias(() => grouping.ValidationGroup, () => group)
                                          .JoinAlias(() => group.PersonTAList, () => person)
                                          .JoinAlias(() => grouping.ValidationRule, () => rule)
                                          .Where(() => person.ID == personId)
                                          .And(() => rule.CustomCode == ruleCode.ToString())
                                          .And(() => group.SubSystemId == (int)SubSystemIdentifier.TimeAtendance)
                                          .List<UIValidationGrouping>();

            decimal result = list != null && list.Count > 0 ? list.First().ID : 0;

            return result;
        }

        public IList<UIValidationGrouping> GetByPersonId(decimal personId)
        {

            UIValidationGrouping grouping = null;
            UIValidationGroup group = null;
            PersonTASpec person = null;

            IList<UIValidationGrouping> list = base.NHibernateSession.QueryOver<UIValidationGrouping>(() => grouping)
                                          .JoinAlias(() => grouping.ValidationGroup, () => group)
                                          .JoinAlias(() => group.PersonTAList, () => person)
                                          .Where(() => person.ID == personId)
                                          .And(() => group.SubSystemId == (int)SubSystemIdentifier.TimeAtendance)
                                          .List<UIValidationGrouping>();

            IList<UIValidationGrouping> result = list != null && list.Count > 0 ? list : new List<UIValidationGrouping>();

            return result;
        }

        public IList<UIValidationGrouping> GetByGroupId(decimal groupId)
        {
            UIValidationGrouping grouping = null;
            UIValidationGroup group = null;
            IList<UIValidationGrouping> list = NHibernateSession.QueryOver<UIValidationGrouping>(() => grouping)
                .Where(() => grouping.GroupID == groupId)
                .List();

            IList<UIValidationGrouping> result = list != null && list.Count > 0 ? list : new List<UIValidationGrouping>();

            return result;
        }

        /// <summary>
        /// پیشکاتهای متصل به یک قانون را برمیگرداند
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public IList<Precard> GetPrecard(string uiRuleCustomCode)
        {

            string SQLCommand = @"select * from TA_Precard where 
                                  Precrd_Code in (select UIRlePrecard_PrecardCustomCode from TA_UIValidationRulePrecard 
                                  where UIRlePrecard_RuleCustomCode=:ruleCode)";

            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(Precard))
                                     .SetParameter("ruleCode", uiRuleCustomCode)
                                     .List<Precard>();
            IList<Precard> result = list != null && list.Count > 0 ? list : new List<Precard>();
            return result;
        }

    }
}
