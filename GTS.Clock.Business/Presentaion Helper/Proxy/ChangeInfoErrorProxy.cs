using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class ChangeInfoErrorProxy
    {
        public string PersonName { get; set; }
        public string PersonCode { get; set; }
        public string ErrorMessage { get; set; }
        public override string ToString()
        {
            return String.Format("نام:{0} شماره پرسنلی:{1} پیام خطا:{2}", PersonName, PersonCode, ErrorMessage);
        }
    }
}
