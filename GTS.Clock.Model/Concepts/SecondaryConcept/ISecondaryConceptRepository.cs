using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Collections;
using GTS.Clock.Model;

namespace GTS.Clock.Model.Concepts
{
    public interface ISecondaryConceptRepository : IRepository<SecondaryConcept>
    {
        IList<PersistedScndCnpPrdValue> GetPeriodicScndCnpValues(decimal PersonId, DateTime FromDate, DateTime ToDate, DateTime FromDateRange, DateTime ToDateRange, decimal CalcRangeGrpId);
    }
}
