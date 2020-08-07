using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class DataAccessProxy
    {
        public decimal ID { get; set; }
        public string CustomCode { get; set; }
        public string Name { get; set; }
        public bool DeleteEnable { get; set; }
        public bool All { get; set; }
        
        /// <summary>
        /// جهت استفاده در گزارشات
        /// </summary>
        public bool IsReport { get; set; }
    }
}
