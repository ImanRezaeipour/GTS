using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model
{
    public interface IRuleRepository : IRepository<Rule>
    {
        IList<RuleTemplate> GetRuleTemplates(decimal[] ruleTemplateIds);

        int GetTemplateParameterCount(decimal ruleTmpId);

        IList<AssignedRuleParameter> GetAssginedRuleParamList(DateTime fromDate, DateTime toDate);

        List<RuleTemplate> GetRuleUserDefined();
    }
}
