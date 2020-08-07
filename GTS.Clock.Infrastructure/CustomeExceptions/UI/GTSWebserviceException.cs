using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    [Serializable]
    public class GTSWebserviceException : UIBaseException
    {

        public GTSWebserviceException(string msg,string exceptionSrc)
            : base(UIExceptionTypes.ShowMessage, UIFatalExceptionIdentifiers.NONE, msg, exceptionSrc)
        { }
    }
}
