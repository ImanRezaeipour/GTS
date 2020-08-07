using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Proxy
{
    public class UserRequestFilterProxy
    {
        public UserRequestFilterProxy() 
        {
            this.FromDate = null;
            this.ToDate = null;
            this.RequestType = null;
            this.RequestSubmiter = null;
            this.UnderManagmentPersonId = 0;
        }

        /// <summary>
        /// تاریخ ابتدا
        /// </summary>
        public string FromDate { get; set; }
        
        /// <summary>
        /// تاریخ انتها
        /// </summary>
        public string ToDate { get; set; }

        /// <summary>
        /// نوع درخواست
        /// </summary>
        public RequestType? RequestType { get; set; }

        /// <summary>
        /// صادر کننده
        /// </summary>
        public RequestSubmiter? RequestSubmiter { get; set; }

        /// <summary>
        /// فیلتر اپراتور - افراد تحت مدیریت
        /// </summary>
        public decimal UnderManagmentPersonId { get; set; }
    }
}
