using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public interface IPairContainer<TPair> 
         where TPair: IPair
    {
        IList<TPair> Pairs { get; set; }
    }
}
