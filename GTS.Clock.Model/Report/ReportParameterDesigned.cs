using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
  public class ReportParameterDesigned :IEntity
    {
      public virtual Decimal ID { get; set; }
      //public virtual ReportParameter ReportParameter { get; set; }
       
      public virtual String CustomCode { get; set; }
      public virtual String FnName { get; set; }
      public virtual String EnName { get; set; }
      public virtual String Title { get; set; }
      public virtual IList<ReportParameterDesignedParam> ReportParameterDesignedParam { get; set; }
      public virtual ReportUIParameter ReportUIParameter { get; set; }
    }
}
