using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    public class ValidationException : UIBaseException
    {
        public ExceptionResourceKeys ResourceKey { get; set; }
        public ValidationException(ExceptionResourceKeys key, string msg, string execptionSrc)
            : base(UIExceptionTypes.ShowMessage, msg, execptionSrc)
        {
            ResourceKey = key;
        }

        public override string GetLogMessage()
        {
            return base.GetLogMessage();
        }
    }
}
