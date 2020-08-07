using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.MonthlyReport
{
    public interface IPersonalMonthlyRptRepository : IRepository<PersonalMonthlyReport>
    {
        IList<CurrentProceedTraffic> LoadDailyProceedTrafficList(decimal personId, DateTime fromDate, DateTime toDate);
        IList<ScndCnpValue> LoadDailyScndCnpList(decimal PersonId, DateTime Date, int Order);
        IList<PersonalMonthlyReportRowDetail> LoadPairableScndcnpValue(decimal PersonId, DateTime Date);
        IList<PersonalMonthlyReportRowDetail> LoadPairableScndcnpValue(decimal PersonId, DateTime Date, ConceptsKeys key);
        IList<ScndCnpValue> LoadDailyScndCnpWithouthMonthlyList(decimal PersonId, DateTime fromDate, DateTime toDate);
    }
}
