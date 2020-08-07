using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.RequestFlow
{
    public class RegisteredRequestsFlowLevel
    {
        public decimal ManagerFlowID { get; set; }
        public decimal ManagerID { get; set; }
        public decimal FlowID { get; set; }
        public string ManagerName { get; set; }
        public int ManagerLevel { get; set; }
    }
}
