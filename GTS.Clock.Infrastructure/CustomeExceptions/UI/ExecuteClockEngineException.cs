using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    /// <summary>
    /// اگر موتور محاسبات در اجرای قوانین با مشکلی روبرو شود این خطا را ایجاد می نماید
    /// </summary>
    public class ExecuteGTSEngineException : UIBaseException
    {
        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public ExecuteGTSEngineException(string msg, string exceptionSrc)
            : base(UIExceptionTypes.ShowMessage, UIFatalExceptionIdentifiers.NONE, msg, exceptionSrc)
        { }


    }
}
