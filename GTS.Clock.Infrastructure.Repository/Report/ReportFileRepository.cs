using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Transform;


namespace GTS.Clock.Infrastructure.Repository
{
    public class ReportFileRepository : RepositoryBase<ReportFile>
    {
        public override string TableName
        {
            get { return "TA_Flow"; }
        }

        public ReportFileRepository(bool disconectly) 
            : base(disconectly) 
        { }
            


    }
}
