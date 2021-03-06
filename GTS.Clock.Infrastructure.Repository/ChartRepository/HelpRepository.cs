using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Infrastructure.Repository
{
    public class HelpRepository : RepositoryBase<GTS.Clock.Model.Help>
    {
        public override string TableName
        {
            get { return "TA_Help"; }
        }

        public HelpRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
      
        #region Model Interface

        public IList<Help> GetHelpTree()
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(Help));
            crit.Add(Expression.Or(
                Expression.IsNull("Parent"),
                Expression.Eq("Parent.ID", Convert.ToDecimal(0))));

            IList<Help> parents = crit.List<Help>();            

            return parents;
        }

      
        #endregion      
        
    }
}
