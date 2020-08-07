using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    public abstract class UIBaseException : BaseException
    {
        public UIExceptionTypes ExceptionType { get; set; }
        public UIFatalExceptionIdentifiers FatalExceptionIdentifier { get; set; }

        public UIBaseException(UIExceptionTypes type, string msg, string execptionSrc)
            : base(msg, execptionSrc)
        {
            ExceptionType = type;          
        }
        
        public UIBaseException(UIExceptionTypes type,UIFatalExceptionIdentifiers id, string msg, string execptionSrc)
            : base(msg, execptionSrc) 
        {
            ExceptionType = type;
            FatalExceptionIdentifier = id;
        }
    }
}
