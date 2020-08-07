using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Security;
using System.Linq;


namespace GTS.Clock.Infrastructure.Repository
{
    public class DomainsRepository : RepositoryBase<Domains>
    {
        public override string TableName
        {
            get { return "TA_ActiveDirectoryDomains"; }
        }
        public IList<Domains> GetAllActive()
        {

            return base.GetAll().Where(x => x.Active == true).ToList();
        }

    }
}
