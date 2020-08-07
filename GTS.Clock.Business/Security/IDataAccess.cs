using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Security
{
    public interface IDataAccess
    {
        IList<decimal> GetAccessibleDeparments();
        IList<decimal> GetAccessibleOrgans();
        IList<decimal> GetAccessibleWorkGroups();
        IList<decimal> GetAccessibleRuleGroups();
        IList<decimal> GetAccessibleShifts();
        IList<decimal> GetAccessiblePrecards();
        IList<decimal> GetAccessibleControlStations();
        IList<decimal> GetAccessibleDoctors();
        IList<decimal> GetAccessibleManagers();
        IList<decimal> GetAccessibleFlows();
        IList<decimal> GetAccessibleReports();

        /// <summary>
        /// تمام پرسنل منتسب به بخشهای یک کاربر را بصورت سلسله مراتبی استخراج میکند
        /// </summary>
        /// <returns></returns>
        IList<decimal> GetAccessiblePersonByDepartment();

        /// <summary>
        /// تمام پرسنل منتسب به بخشهای یک کاربر و ایستگاه کنترل را بصورت سلسله مراتبی استخراج میکند
        /// </summary>
        /// <returns></returns>
        IList<decimal> GetAccessiblePersonByDepartmentAndControlSatation();
    }
}
