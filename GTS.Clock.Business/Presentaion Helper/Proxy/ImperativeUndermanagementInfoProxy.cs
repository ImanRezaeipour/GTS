using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class ImperativeUndermanagementInfoProxy
    {
        public decimal PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonCode { get; set; }
        public string PersonImage { get; set; }
        public decimal ImperativeValue { get; set; }
        public bool IsLockedImperative { get; set; }
        public string ImperativeDescription { get; set; }
        public CalcInfoProxy CalcInfo { get; set; }
 

    }
}
