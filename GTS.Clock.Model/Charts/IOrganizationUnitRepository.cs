using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.Charts
{
    public interface IOrganizationUnitRepository
    {
        IList<OrganizationUnit> GetOrganizationUnitTree();

        decimal GetParentID(decimal departmentID);

        bool IsRoot(decimal organID);

        bool HasPerson(decimal organID);
        OrganizationUnit GetByPersonId(decimal personID);
    }
}

