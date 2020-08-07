using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class ManagerProxy
    {
        public decimal ID { get; set; }

        /// <summary>
        /// شناسه پرسنل یا پست سازمانی
        /// </summary>
        public decimal OwnerID { get; set; }

        public int Level { get; set; }

        public ManagerType ManagerType { get; set; }

        public string Name { get; set; }
    }
}
