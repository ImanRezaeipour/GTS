using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.RequestFlow
{
    public interface IRequestRepository : IRepository<Request>
    {
        IList<Request> GetAllHourlyRequestByType(decimal personId, DateTime date, PrecardGroupsName key);

        IList<Request> GetAllHourlyRequestByType(decimal personId, DateTime date, PrecardGroupsName key1, PrecardGroupsName key2, PrecardGroupsName key3);

        IList<Request> GetAllDailyRequestByType(decimal personId, DateTime date, PrecardGroupsName key);

        IList<Request> GetAllDailyRequestByType(decimal personId, DateTime date, PrecardGroupsName key1, PrecardGroupsName key2, PrecardGroupsName key3);
    }
}
