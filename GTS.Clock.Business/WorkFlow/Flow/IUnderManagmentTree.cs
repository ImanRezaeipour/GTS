using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Business.RequestFlow
{
    public interface IUnderManagmentTree
    {
        IList<Person> GetDepartmentPerson(decimal departmentID, decimal flowId);
    }
}
