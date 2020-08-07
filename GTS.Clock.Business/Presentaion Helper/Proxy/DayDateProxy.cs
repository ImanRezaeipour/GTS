using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class DayDateProxy
    {
        public int RowID { get; set; }
        public string DayName { get; set; }
        public string TheDate { get; set; }

        /// <summary>
        /// تاریخ میلادی
        /// </summary>
        public string Date { get; set; }
    }
}
