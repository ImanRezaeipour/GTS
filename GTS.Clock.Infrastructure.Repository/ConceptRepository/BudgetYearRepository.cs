using System;
using System.Linq;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Criterion;
using System.Collections.Generic;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Infrastructure.Repository
{
    public class BudgetRepository : RepositoryBase<Budget> 
    {
        public override string TableName
        {
            get { return "TA_BudgetYear"; }
        }       


        public BudgetRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }
/*
        /// <summary>
        /// باقیمانده مرخصی استحقاقی را تا پایان سال برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="currentYear"></param>
        /// <param name="currentMonth"></param>
        /// <returns></returns>
        public int GetRemainLeaveToEndOfYear(decimal personId, int currentYear) 
        {
            int result= NHibernateSession.GetNamedQuery("GetRemainLeaveToEndOfYear")
                                    .SetParameter("personId", personId)
                                    .SetParameter("curentYear", currentYear)
                                    .UniqueResult<int>();
            return result;
        }

        /// <summary>
        /// باقیمانده مرخصی استحقاقی را تا پایان ماه برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="currentYear"></param>
        /// <param name="currentMonth"></param>
        /// <returns></returns>
        public int GetRemainLeaveToEndOfMonth(decimal personId, int currentYear, int currentMonth)
        {
            int result = NHibernateSession.GetNamedQuery("GetRemainLeaveToEndOfMonth")
                                    .SetParameter("personId", personId)
                                    .SetParameter("curentYear", currentYear)
                                    .SetParameter("curentMonth", currentMonth)
                                    .UniqueResult<int>();
            return result;
        }
      */
    }
}
