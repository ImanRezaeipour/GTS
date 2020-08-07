using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.Charts
{
    public interface IDepartmentRepository
    {
        IList<Department> GetDepartmentTree();

        decimal GetParentID(decimal departmentID);
        
        bool IsRoot(decimal departmentID);

        //IList<Department> GetChilds(decimal parentId);
        
    }
}

