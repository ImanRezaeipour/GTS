using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;


namespace GTS.Clock.Model
{   
    public interface IRuleCategoryRepository :  IRepository<RuleCategory>
    {
        IList<RuleCategory> GetRoot();

        IList<Rule> GetRulesByRuleCatID(decimal id);

    }
}
