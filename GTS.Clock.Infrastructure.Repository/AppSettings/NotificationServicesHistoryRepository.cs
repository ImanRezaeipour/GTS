using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.AppSetting;
using System.Linq;


namespace GTS.Clock.Infrastructure.Repository
{
    public class NotificationServicesHistoryRepository : RepositoryBase<NotificationServicesHistory>
    {
        public override string TableName
        {
            get { return "TA_NotificationServicesHistory"; }
        }
        public NotificationServicesHistoryRepository()
            : base()
        { }

        public NotificationServicesHistoryRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        public IList<NotificationServicesHistory> GetHistory(NotificationsServices serviceId, DateTime date) 
        {
            string HQLCommand = @"FROM NotificationServicesHistory 
                                  WHERE NotificationServiceID = :serviceId AND
                                  Date <= :date";

            IQuery query = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("serviceId", (int)serviceId)
                .SetParameter("date", date.Date);

            IList<NotificationServicesHistory> list = query.List<NotificationServicesHistory>();
            return list;

        }

        public void InsertHistory(NotificationsServices serviceId, DateTime date, IList<decimal> items)
        {
            string insertItems = "";
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (i == 0)
                    {
                        insertItems += String.Format(" values ({0},'{1}','{2}') ", (int)serviceId, String.Format("{0}_{1}", items[i], serviceId), date.Date);
                    }
                    else
                    {
                        insertItems += String.Format(" ,({0},'{1}','{2}') ", (int)serviceId, String.Format("{0}_{1}", items[i], serviceId), date.Date);

                    }
                }
                string sqlCommand = @" insert into TA_NotificationServicesHistory
                                (ns_NotificationServiceID,nt_ItemID,nt_Date)
                                " + insertItems;

                base.NHibernateSession.CreateSQLQuery(sqlCommand)
                    //.SetParameter("serviceId", (int)serviceId)
                    //.SetParameter("date", date.Date)
                      .ExecuteUpdate();
            }
        }

    }
}
