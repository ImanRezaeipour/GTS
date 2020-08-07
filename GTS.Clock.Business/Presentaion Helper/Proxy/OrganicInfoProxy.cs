using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// تغییر مشخصات سازمانی
    /// </summary>
    public class OrganicInfoProxy
    {
        public decimal DepartmentID { get; set; }
        public decimal EmploymentTypeID { get; set; }
        public decimal WorkGroupID { get; set; }
        public decimal RuleGroupID { get; set; }
        public decimal DateRangeID { get; set; }
        public string WorkGroupFromDate { get; set; }
        public string DateRangeFromDate { get; set; }
        public string RuleGroupFromDate { get; set; }
        public string RuleGroupToDate { get; set; }
    }
}
