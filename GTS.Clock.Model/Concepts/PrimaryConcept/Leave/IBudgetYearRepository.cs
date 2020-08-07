using System;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Collections.Generic;

namespace GTS.Clock.Model.Concepts
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        IList<LeaveCalcResult> GetLCR(decimal personId, DateTime fromDate, DateTime toDate);
    }
}
