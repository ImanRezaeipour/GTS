using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Criterion;
using NHibernate;
using NHibernate.Type;

namespace GTS.Clock.Infrastructure.Repository.Leave
{
    public class LeaveYearRemainRepository : RepositoryBase<LeaveYearRemain>
    {
        public override string TableName
        {
            get { return "TA_LeaveYearRemain"; }
        }


        public IList<LeaveYearRemain> GetExtraLeaveYearRemains(decimal finalLeaveYearRemainID, DateTime targetDate, decimal personnelID)
        {
            IList<LeaveYearRemain> ExtraLeaveYearRemainsList = NHibernateSession.QueryOver<LeaveYearRemain>()
                                                                                               .Where(leaveYearRemain => leaveYearRemain.Date == targetDate && leaveYearRemain.ID != finalLeaveYearRemainID)
                                                                                               .JoinQueryOver(leaveYearRemain => leaveYearRemain.Person)
                                                                                               .Where(person => person.ID == personnelID)
                                                                                               .List<LeaveYearRemain>();
            return ExtraLeaveYearRemainsList;
        }

        public IList<LeaveYearRemain> GetAllLeaveYearRemain(decimal userId, DateTime fromDate, DateTime toDate,int pageIndex, int pageSize) 
        {
            IList<LeaveYearRemain> list = new List<LeaveYearRemain>();
            DetachedCriteria criteria = DetachedCriteria.For(this.persistanceType);
            criteria.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new LeaveYearRemain().Date),fromDate));
            criteria.Add(Restrictions.Le(Utility.Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate));
            criteria.Add(Expression.Sql("LeaveYearRemain_PersonId in (select * from fn_GetAccessiblePersons(0,?,?))", new object[] { userId, (int)PersonCategory.Public }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Int32 }));
            try
            {
                list = criteria.GetExecutableCriteria(NHibernateSession)
                    .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                    .List<LeaveYearRemain>() as IList<LeaveYearRemain>;
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return list;
        }

        public int GetLeaveYearRemainCount(decimal userId, DateTime fromDate, DateTime toDate) 
        {
            IList<LeaveYearRemain> list = new List<LeaveYearRemain>();
            DetachedCriteria criteria = DetachedCriteria.For(this.persistanceType);
            criteria.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate));
            criteria.Add(Restrictions.Le(Utility.Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate));
            criteria.Add(Expression.Sql("LeaveYearRemain_PersonId in (select * from fn_GetAccessiblePersons(0,?,?))", new object[] { userId, (int)PersonCategory.Public }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Int32 }));
            int count = 0;
            try
            {
                count = (int)criteria.GetExecutableCriteria(NHibernateSession)
                        .SetProjection(Projections.Count("ID")).UniqueResult();
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return count;
        }
    }
}
