using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Infrastructure.Repository
{
    public class CalendarRepository : RepositoryBase<Calendar>, ICalendarRepository
    {
        public override string TableName
        {
            get { return "TA_calendar"; }
        }

        public CalendarRepository()
            : base()
        { }

        public CalendarRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        public void DeleteCalendarsByType(decimal calendarTypeId,DateTime fromDate,DateTime toDate) 
        {
            string SQLCommand = @"DELETE from TA_Calendar where Calendar_CalendarTypeId=:typeId
                                                            AND Calendar_Date >= :fromDate
                                                            AND Calendar_Date <= :toDate";
            NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("typeId", calendarTypeId)
                .SetParameter("fromDate", fromDate)
                .SetParameter("toDate", toDate)
                .ExecuteUpdate();

        }

        public IList<Calendar> GetAllCalendarWithType()
        {
            Calendar calendar = null;
            CalendarType calendarType = null;

            return NHibernateSession.QueryOver<Calendar>(() => calendar)
                                    .Select(Projections.Property(() => calendar.ID).WithAlias(() => calendar.ID),
                                            Projections.Property(() => calendar.ActiveDayCount).WithAlias(() => calendar.ActiveDayCount),
                                            Projections.Property(() => calendar.CalendarType).WithAlias(() => calendar.CalendarType))                                            
                                    .JoinAlias(() => calendar.CalendarType, () => calendarType)
                                    .List<Calendar>();

        }

        /// <summary>
        /// تلریخ تقوین را برمیگرداند
        /// </summary>
        /// <param name="caledarTypeId"></param>
        /// <returns></returns>
        public IList<DateTime> GetAllCalendarDateByTypeId(decimal caledarTypeId)
        {
            string HQLCommand = @"select cal.Date from Calendar as cal
                                  where cal.CalendarType.ID=:calTypeId";

            IList<DateTime> dates = base.NHibernateSession.CreateQuery(HQLCommand)
               .SetParameter("calTypeId", caledarTypeId)
               .List<DateTime>();
            return dates;

        }
    }
}
