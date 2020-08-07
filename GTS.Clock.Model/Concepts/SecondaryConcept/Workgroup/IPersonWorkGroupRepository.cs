using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;

namespace GTS.Clock.Model
{
    public interface IPersonWorkGroupRepository : IRepository<PersonWorkGroup>
    {
        IList<AssignedWGDShift> GetAssignedWGDShift(decimal WorkGroupDetailID);

        //IList<AssignedWGDShift> GetAssignedWGDShiftByFilter(decimal WorkGroupDetailID);
    }
}
