using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public interface IPair
    {
        int From { get; set; }
        int To { get; set; }
        int Value { get; }
        string ExFrom { get; }
        string ExTo { get; }
        string ExValue { get; }
    }
}
