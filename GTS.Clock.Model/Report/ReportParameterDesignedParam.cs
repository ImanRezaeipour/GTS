using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class ReportParameterDesignedParam :IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual ReportParameterDesigned ReportParameterDesigned { get; set; }
        public virtual String Parameter { get; set; }
    }
}
