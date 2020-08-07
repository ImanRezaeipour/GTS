using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business.Proxy
{
    public class ShiftProxy 
    {
        /// <summary>
        /// یک شناسه یکتا جهت راحتی کار واسط کاربر
        /// </summary>
        public decimal ID { get; set; }

        /// <summary>
        /// شناسه شیفت
        /// </summary>
        public decimal ShiftID { get; set; }

        /// <summary>
        /// تاریخ
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// نام روز هفته
        /// </summary>
        public string DayName { get; set; }

        /// <summary>
        /// نام شیفت
        /// </summary>
        public string ShiftName { get; set; }
    }

    public class ShiftPairProxy 
    {
        public string From { get; set; }
        
        public string To { get; set; }

        public string Description { get; set; }
    }
}
