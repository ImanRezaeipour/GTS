using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public class NHibernateException : BaseException
    {
        Hashtable attributes = new Hashtable();
        /// <summary>
        /// «‰Ê«⁄ Œ’Ì’Â«Ì Ìò ‰Ê⁄ «À À‰« —« œ— «Ì‰Ã« „Ì Ê«‰ «÷«›Â ò—œ òÂ  « œ— ¬Ì‰œÂ ·«ê ‘Ê‰œ
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public void AddAttribute(string propertyName, string propertyValue) 
        {
            if (attributes.ContainsKey(propertyName))
            {
                attributes[propertyName] = propertyValue;
            }
            else
            {
                attributes.Add(propertyName, propertyValue);
            }
        }
        /// <summary>
        /// NhibernateException.GetType().ToString();
        /// </summary>
        public string NhibernateExceptionType
        {
            get;
            set;
        }
        public NHibernateException(ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.NHibernateException", exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }

        public NHibernateException(string msg, ExceptionType exType, string exceptionSource, Exception innerException)
            : base("GTS.Clock.Infrastructure.Exceptions.NHibernateException: " + msg, exceptionSource, innerException)
        {
            base.ExceptionTypeActivity = exType;
        }

        public NHibernateException(string msg, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.NHibernateException: " + msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }
 
        public NHibernateException(string msg, string xmlName, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.NHibernateException: " + msg, exceptionSource)
        {           
            base.ExceptionTypeActivity = exType;
        }

        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendLine(NhibernateExceptionType);
            foreach (string key in attributes.Keys)
            {
                msg.AppendLine(key + " : " + attributes[key].ToString());
            }
            return msg.ToString();            
        }               
    }
}
