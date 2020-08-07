using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Security;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Linq;

namespace GTS.Clock.Infrastructure.Repository
{
    public class ResourceRepository : RepositoryBase<Resource>, IResourceRepository
    {
        public override string TableName
        {
            get { return "TA_SecurityResource"; }
        }

        public ServiceAuthorizeType CheckServiceAuthorize(decimal RoleID, IMethodInvocation input)
        {
            ServiceAuthorizeType SAT = ServiceAuthorizeType.Illegal;
            IList<Authorize> AuthorizeList = NHibernateSession.QueryOver<Authorize>()
                                                  .Where(authorize => authorize.Role.ID == RoleID && authorize.Allow)
                                                  .JoinQueryOver(authorize => authorize.Resource)
                                                  .Where(resource => resource.MethodPath == input.Target.ToString() && resource.MethodFullName == input.MethodBase.ToString())
                                                  .List<Authorize>();
            if (AuthorizeList.Count == 1)
                SAT = ServiceAuthorizeType.Legal;
            return SAT;
        }
    }

}
