using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class DesignedReportColumn:IEntity 
    {

       public virtual Decimal ID { get ;set; }
       public virtual String Title { get; set; }
       public virtual Boolean Active { get; set; }
       public virtual Report Report { get; set; }
       public virtual Concepts.SecondaryConcept Concept { get; set; }
       public virtual Int16 Order { get; set; }
    }
}
