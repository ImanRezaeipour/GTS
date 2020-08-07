using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Infrastructure.Exceptions
{
    public class XMLReadingException : BaseException
    {
        string xmlSourceName = "";
        public string XmlSourceName { get { return xmlSourceName; } }
        public override string Message
        {
            get
            {
                return base.Message + (xmlSourceName.Length > 0 ? "XML Cused Exception:" + xmlSourceName : "");
            }
        }

        public XMLReadingException(ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.XMLReadingException", exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }
        public XMLReadingException(string msg, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.XMLReadingException: " + msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }
        public XMLReadingException(string msg, string xmlName, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.XMLReadingException: " + msg, exceptionSource)
        {
            xmlSourceName = xmlName;
            base.ExceptionTypeActivity = exType;
        }
       
        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendLine("XML Cused Exception: " + xmlSourceName);
            return msg.ToString();  
        }

    }
}
