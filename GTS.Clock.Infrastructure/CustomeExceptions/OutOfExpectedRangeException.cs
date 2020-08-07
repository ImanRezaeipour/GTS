using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public class OutOfExpectedRangeException:BaseException 
    {
        #region Property
        /// <summary>
        /// ابتدای بازه
        /// </summary>
        public string StartOfExpectedRange
        {
            get;
            set;
        }

        /// <summary>
        /// انتهای بازه
        /// </summary>
        public string EndOfExpectedRange
        {
            get;
            set;
        }

        /// <summary>
        /// مقداری که خارج از بازه مد نظر بوده است و باعث این خطا شده است
        /// </summary>
        public string ProvidedValue
        {
            get;
            set;
        } 
        #endregion

        public OutOfExpectedRangeException(string start, string end, string providedValue, string ExceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.OutOfExpectedRangeException", ExceptionSource) 
        {
            StartOfExpectedRange = start;
            EndOfExpectedRange = end;
            ProvidedValue = providedValue;
        }

        public OutOfExpectedRangeException(string start, string end, string providedValue, string ExceptionSource,ExceptionType expType)
            : base("GTS.Clock.Infrastructure.Exceptions.OutOfExpectedRangeException", ExceptionSource)
        {
            base.ExceptionTypeActivity = expType;
            StartOfExpectedRange = start;
            EndOfExpectedRange = end;
            ProvidedValue = providedValue;
        }

        public OutOfExpectedRangeException(string start, string end, string providedValue, string ExceptionSource, string msg)
            : base("GTS.Clock.Infrastructure.Exceptions.OutOfExpectedRangeException" + msg, ExceptionSource)
        {
            StartOfExpectedRange = start;
            EndOfExpectedRange = end;
            ProvidedValue = providedValue;
        }

        public OutOfExpectedRangeException(string start, string end, string providedValue, string ExceptionSource, string msg,ExceptionType exType)
            : base("GTS.Clock.Infrastructure.Exceptions.OutOfExpectedRangeException" + msg, ExceptionSource)
        {
            base.ExceptionTypeActivity = exType;
            StartOfExpectedRange = start;
            EndOfExpectedRange = end;
            ProvidedValue = providedValue;
        }


        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendFormat("\n Expected Range : {0} - {1} \n And Provided Value : {2}", StartOfExpectedRange, EndOfExpectedRange, ProvidedValue);
            return msg.ToString();
        }
    }
}
