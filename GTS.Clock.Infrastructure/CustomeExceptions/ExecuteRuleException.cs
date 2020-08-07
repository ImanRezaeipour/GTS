using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Infrastructure.Exceptions
{
    public class ExecuteRuleException : BaseException
    {
        public ExecuteRuleException(string msg, 
                                        ExceptionType exType, 
                                        string exceptionSource,
                                        Exception innerException)
            : base("GTS.Clock.Infrastructure.Exceptions.ExecuteRuleException: " + msg, 
                        exceptionSource, 
                        innerException)
        {
            base.ExceptionTypeActivity = exType;
        }

    }
}
