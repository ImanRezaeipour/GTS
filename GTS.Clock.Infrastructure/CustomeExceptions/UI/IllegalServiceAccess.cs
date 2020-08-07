using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    /// <summary>
    /// اگر سیستم سمت کلاینت هک شود و متدی فراخوانی شود که اجازه آن صادر نشده باشد این خطا رخ میدهد
    /// </summary>
    public class IllegalServiceAccess : UIBaseException
    {
        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public IllegalServiceAccess(string msg, string exceptionSrc)
            : base(UIExceptionTypes.Fatal, UIFatalExceptionIdentifiers.IllegalServiceAccess, msg, exceptionSrc)
        {
        }
    }
}
