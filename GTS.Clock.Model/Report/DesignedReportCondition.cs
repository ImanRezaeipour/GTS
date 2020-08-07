using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class DesignedReportCondition :IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual String ConditionText { get; set; }
        public virtual String ConditionValue{ get; set; }
        public virtual String OrderText { get; set; }
        public virtual String OrderValue { get; set; }
       public virtual Report Report { get; set; }
       public virtual Person Person { get; set; }
    }
}
