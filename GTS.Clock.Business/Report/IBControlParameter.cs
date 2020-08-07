using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Reporting
{
    public interface IBControlParameter
    {
        IDictionary<string, object> ParsParameter(string parameters, string actionId);
    }
}
