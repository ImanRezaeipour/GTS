using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    public class UpdatPersonImageException : UIBaseException
    {
        public ExceptionResourceKeys ResourceKey { get; set; }
        public UpdatPersonImageException(UIFatalExceptionIdentifiers id, string msg, string execptionSrc)
            : base(UIExceptionTypes.Fatal, id, msg, execptionSrc)
        {
        }

        public override string GetLogMessage()
        {
            return base.GetLogMessage();
        }
    }
}
