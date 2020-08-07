using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class DesignedReportType:IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual String Name { get; set; }
        public virtual String CustomCode { get; set; }
    }
}
