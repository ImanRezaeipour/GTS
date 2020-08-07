using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts.UI
{
    public interface IGridMonthlyOperationConceptValuesRepository : IRepository<GridMonthlyOperationConceptValues>
    {
        int UpdateResultTableByRanglyConceptValues(decimal managerId, int order, int year, int month, decimal departmentId, string personCode, string personName, int searchMode);
        int UpdateResultTableByConceptValues(decimal personId, DateTime fromDate, DateTime toDate);
    }
}
