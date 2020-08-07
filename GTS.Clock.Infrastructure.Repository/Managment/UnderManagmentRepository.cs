using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;


namespace GTS.Clock.Infrastructure.Repository
{
    public class UnderManagmentRepository : RepositoryBase<UnderManagment>, IUnderManagmentRepository
    {
        public override string TableName
        {
            get { return "TA_UnderManagment"; }
        }

        public UnderManagmentRepository(bool disconnectly)
            : base(disconnectly)
        { }

        #region IUnderManagmentRepository Members

        public IList<Department> GetAssignDepartments()
        {
            string HQLCommand = @"from Department where ID in
                        (select Department from UnderManagment)";
            IList<Department> list = base.NHibernateSession.CreateQuery(HQLCommand).List<Department>();
            return list;
        }

        public void DeleteUnderManagments(decimal flowId)
        {
            string HQLCommand = @"Delete from UnderManagment where Flow = :flow";
            base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("flow", new Flow() { ID = flowId })
                .ExecuteUpdate();
        }

        #endregion
       
    }
}
