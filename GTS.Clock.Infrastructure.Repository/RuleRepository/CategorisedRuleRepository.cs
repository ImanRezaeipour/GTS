using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;
using GTS.Clock.Model;


namespace GTS.Clock.Infrastructure.Repository
{
    public class CategorisedRuleRepository : RepositoryBase<AssignedRule>, IAssignedRuleRepository
    {
        public override string TableName
        {
            get { return "AssignedRule_view"; }
        }
        public CategorisedRuleRepository()
            : base()
        { }

        public CategorisedRuleRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
    }
}
