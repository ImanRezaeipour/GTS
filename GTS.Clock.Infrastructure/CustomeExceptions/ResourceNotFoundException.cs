using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    class ResourceNotFoundException : BaseException
    {
        string fileName = "";
        public string ResourceName
        {
            get { return fileName; }
        }

        public ResourceNotFoundException(ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.ResourceNotFound", exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }

        public ResourceNotFoundException(string msg, ExceptionType exType, string exceptionSource)
            : base(msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }

        public ResourceNotFoundException(string msg, string theResourceName, ExceptionType exType, string exceptionSource)
            : base(msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
            fileName = theResourceName;
        }

        public ResourceNotFoundException(string msg, string theResourceName, ExceptionType exType, string exceptionSource, Exception innerException)
            : base(msg, exceptionSource, innerException)
        {
            base.ExceptionTypeActivity = exType;
            fileName = theResourceName;
        }

        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendLine(" Resource Information : " + ResourceName);
            return msg.ToString();
        }
    }
}
