using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    /// <summary>
    /// حالت پیش بینی نشده در توسعI سیستم
    /// </summary>
    public class UnforeseenState : BaseException
    {
        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public UnforeseenState(string msg, string exceptionSrc)
            : base(msg, exceptionSrc)
        { }


    }
}
