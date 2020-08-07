using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    /// <summary>
    /// معمولا وقتی اتفاق می افند که کلاینت مشغول ویرایش یا مشاهده آیتمی باشد و در همان زمان
    /// شخص دیگری در سیستم آن  آیتم را از دیتابیس حذف کرده باشد
    /// </summary>
    public class AthorizeServiceException : UIBaseException
    {
        public string Username
        {
            get;
            set;
        }

        public string ServiceInfo
        {
            get;
            set;
        }


        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public AthorizeServiceException(string msg,string username,string serviceInfo)
            : base(UIExceptionTypes.Fatal, UIFatalExceptionIdentifiers.NONE, msg, serviceInfo)
        { }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());
            builder.AppendLine(String.Format("Service:{0} isIlegal to access by User: Username:{0} ", ServiceInfo, Username));
            return builder.ToString();
        }
    }
}
