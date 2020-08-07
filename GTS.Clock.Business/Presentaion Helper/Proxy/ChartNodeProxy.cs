using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// این کلاس بمنظور سریال کردن ایجاد شده است
    /// کلاس اصلی هنگام سریال کردن خطای حلقه را میدهد
    /// </summary>
    public class ChartNodeProxy
    {
        public ChartNodeProxy() 
        {
            HasChild = false;
            IsContinued = true;
        }

        public decimal ID { get; set; }

        public string Name { get; set; }

        public string customCode { get; set; }
      
        /// <summary>
        /// در جاوااسکریپت استفاده میشود ما همیشه مقدار آنرا درست بر میگردانیم
        /// </summary>
        public bool IsContinued { get; set; }

        public bool HasChild { get; set; }
    }
}
