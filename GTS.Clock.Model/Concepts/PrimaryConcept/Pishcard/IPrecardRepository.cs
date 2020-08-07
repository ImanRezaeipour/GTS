using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.Concepts
{
    public interface IPrecardRepository : IRepository<Precard>
    {
        bool DoesUsedBySubestitute(decimal precardId);

        void DeletePrecardAccessGroupDetail(decimal precardAccessGroupID);

        PrecardGroups GetPrecardGroup(decimal precardID);

        Precard GetUsualPrecard();
    }
}
