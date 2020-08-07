using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public class ValueNotProvidedException : BaseException
    {
        #region Property
     
        public string ExpectedValueDescription
        {
            get;
            set;
        }

        public string ExpectedValueType
        {
            get;
            set;
        }
       
        public string ExpectedValueName
        {
            get;
            set;
        }
        #endregion


        public ValueNotProvidedException(string expectedValueName, string expectedValueType, string expectedValueDescription, string ExceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.ValueNotProvidedException", ExceptionSource) 
        {
            ExpectedValueDescription = expectedValueDescription;
            ExpectedValueName = expectedValueName;
            ExpectedValueType = expectedValueType;
        }

        public ValueNotProvidedException(string expectedValueName, string expectedValueType, string expectedValueDescription, string ExceptionSource, ExceptionType expType)
            : base("GTS.Clock.Infrastructure.Exceptions.ValueNotProvidedException", ExceptionSource)
        {
            base.ExceptionTypeActivity = expType;
            ExpectedValueDescription = expectedValueDescription;
            ExpectedValueName = expectedValueName;
            ExpectedValueType = expectedValueType;
        }

        public ValueNotProvidedException(string expectedValueName, string expectedValueType, string expectedValueDescription, string ExceptionSource, string msg)
            : base("GTS.Clock.Infrastructure.Exceptions.ValueNotProvidedException" + msg, ExceptionSource)
        {
            ExpectedValueDescription = expectedValueDescription;
            ExpectedValueName = expectedValueName;
            ExpectedValueType = expectedValueType;
        }

        public ValueNotProvidedException(string expectedValueName, string expectedValueType, string expectedValueDescription, string ExceptionSource, string msg, ExceptionType exType)
            : base("GTS.Clock.Infrastructure.Exceptions.ValueNotProvidedException" + msg, ExceptionSource)
        {
            base.ExceptionTypeActivity = exType;
            ExpectedValueDescription = expectedValueDescription;
            ExpectedValueName = expectedValueName;
            ExpectedValueType = expectedValueType;
        }

        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendFormat("\n Expected Value Name : {0} Expected Value Type : {1} Expected Value Description  : {2}", ExpectedValueName, ExpectedValueType, ExpectedValueDescription);
            return msg.ToString();
        }
    }
}
