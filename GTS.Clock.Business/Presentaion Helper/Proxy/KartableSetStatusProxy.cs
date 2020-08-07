using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// جهت ارسال بصورت پارامتر هنگام تغییر وضعیت درخواست توسط مدیر
    /// </summary>
    public class KartableSetStatusProxy
    {
        public KartableSetStatusProxy() { }

        public KartableSetStatusProxy(decimal requestId, decimal managerFlowId) 
        {
            RequestID = requestId;
            ManagerFlowID = managerFlowId;
        }
        public decimal RequestID { get; set; }

        public decimal ManagerFlowID { get; set; }
    }
}
