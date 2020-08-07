using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public class InvalidPersianDateException : BaseException
    {
        public string InvalidDateValue
        {
            get;
            set;
        }
        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public InvalidPersianDateException(string msg, string exceptionSrc)
            : base(msg, exceptionSrc)
        { }

        public InvalidPersianDateException(string msg, string exceptionSrc,string invalidDateValue)
            : base(msg, exceptionSrc)
        {
            InvalidDateValue = invalidDateValue;
        }

        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendLine(" Invalid Date PArameter : " + InvalidDateValue);
            return msg.ToString();
        }
    }
}
