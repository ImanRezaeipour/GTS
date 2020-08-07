using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Collections;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class SubstituteRepository : RepositoryBase<Substitute>
    {
        public override string TableName
        {
            get { return "TA_Substitute"; }
        }

        public SubstituteRepository(bool disconnectly)
            : base(disconnectly)
        {
        }

        /// <summary>
        /// سمتهای جانشینی یک شخص را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<Substitute> GetSubstitute(decimal personId) 
        {
            string HQLCommand = @"select sub from Substitute sub
                                  WHERE sub.Person.ID=:personId AND sub.Active=1";

            IList<Substitute> list = base.NHibernateSession.CreateQuery(HQLCommand)
               .SetParameter("personId", personId)
               .List<Substitute>();
            if (list == null)
                list = new List<Substitute>();
            return list;
        }

        /// <summary>
        /// آیا این شخص جانشین است
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool IsSubstitute(decimal personId) 
        {
            string HQLCommand = @"select count(sub) from Substitute sub
                                  WHERE sub.Person.ID=:personId AND sub.Active=1";

            object count = base.NHibernateSession.CreateQuery(HQLCommand)
               .SetParameter("personId", personId)
               .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString()) == 0 ? false : true;
        }


    }
}
