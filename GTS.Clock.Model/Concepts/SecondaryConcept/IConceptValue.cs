using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Model.Concepts
{
    public interface IConceptValue
    {
        decimal ID { get; set; }
        int Value { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        bool IsValid { get; set; }
        string ExValue { get; }
        Person Person { get; set; }        
    }
}
