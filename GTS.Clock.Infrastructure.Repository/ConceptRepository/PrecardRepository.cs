using System;
using System.Linq;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Criterion;
using System.Collections.Generic;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Infrastructure.Repository
{
    public class PrecardRepository : RepositoryBase<Precard>, GTS.Clock.Model.Concepts.IPrecardRepository 
    {
        public override string TableName
        {
            get { return "TA_Precard"; }
        }       

        public PrecardRepository() 
        {

        }

        public PrecardRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }


        #region IPrecardRepository Members

        public bool DoesUsedBySubestitute(decimal precardId)
        {
            string sqlCommand = @"select count(*) from TA_SubstitutePrecardAccess
                                where subaccess_PrecardId=:precardId";
            object count = base.NHibernateSession.CreateSQLQuery(sqlCommand)
                 .SetParameter("precardId", precardId)
                 .List<object>().FirstOrDefault();
            return count == null || (int)count == 0 ? false : true;
        }

        public void DeletePrecardAccessGroupDetail(decimal precardAccessGroupID) 
        {
            string sqlCommand = @"DELETE FROM TA_PrecardAccessGroupDetail
                                WHERE accessGrpDtl_AccessGrpId=:precardAccessGroupID";
            object count = base.NHibernateSession.CreateSQLQuery(sqlCommand)
                 .SetParameter("precardAccessGroupID", precardAccessGroupID)
                 .ExecuteUpdate();
        }

        public PrecardGroups GetPrecardGroup(decimal precardID) 
        {
            string HQLCommand = @"SELECT pish.PrecardGroup FROM Precard as pish                               
                                WHERE pish.ID=:precardID";
            IList<PrecardGroups> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .SetParameter("precardID", precardID)
                               .List<PrecardGroups>();
            return list.FirstOrDefault();
        }

        /// <summary>
        /// تردد عادی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Precard GetUsualPrecard() 
        {
            string HQLCommand = @"SELECT pish FROM Precard as pish
                                WHERE pish.Code='0'";
            IList<Precard> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .List<Precard>();
            return list.FirstOrDefault();
        }

        #endregion
    }
}
