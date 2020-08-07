using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class MonthlyDetailReportProxy
    {
        public decimal ID { get; set; }

        /// <summary>
        /// نام مفهوم
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Color { get; set; }
    }
}
