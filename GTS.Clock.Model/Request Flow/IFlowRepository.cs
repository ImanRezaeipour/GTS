using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.RequestFlow
{
    public interface IFlowRepository : IRepository<Flow>
    {
        IList<UnderManagment> GetAllUnderManagments(decimal flowId);
    }
}
