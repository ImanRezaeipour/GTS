using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public interface IWorkGroupRepository
    {
        IList<WorkGroup> SearchByName(string workgroupName);

        bool UsedByPerson(decimal workGroupId);

        void DeleteWorkGroupDetail(decimal workGroupId, DateTime minDate,DateTime maxDate);
    }
}
