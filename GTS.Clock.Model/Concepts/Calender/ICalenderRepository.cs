using System;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Collections.Generic;

namespace GTS.Clock.Model.Concepts
{
    public interface ICalendarRepository : IRepository<Calendar>
    {
        IList<Calendar> GetAllCalendarWithType();
    }
}
