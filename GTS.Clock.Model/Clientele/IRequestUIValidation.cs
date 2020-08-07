using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
    public interface IRequestUIValidation
    {
        void DoValidate(CL_OffishRequest request);
    }
}
