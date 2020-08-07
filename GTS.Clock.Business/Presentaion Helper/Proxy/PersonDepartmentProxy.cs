using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class PersonDepartmentProxy
    {
        public decimal DepartmentId { get; set; }

        public decimal PersonId { get; set; }

        public bool ContainsInnerchilds { get; set; }
    }
}
