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
    public class ItemNotExists : UIBaseException
    {
        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public ItemNotExists(string msg, string exceptionSrc)
            : base(UIExceptionTypes.Reload, UIFatalExceptionIdentifiers.NONE, msg, exceptionSrc)
        { }


    }
}
