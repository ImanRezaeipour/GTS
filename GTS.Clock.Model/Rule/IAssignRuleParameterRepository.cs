using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model
{
    public interface IAssignRuleParameterRepository : IRepository<AssignRuleParameter>
    {
        void InitilizeParameters(decimal assignRuleParameterID, decimal ruleID);
    }
}
