using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Concepts
{
    public class NonePairableScndCnpValue : BaseScndCnpValue
    {

        public override string ExValue
        {
            get { return Utility.IntTimeToTime(this.Value); }
        }

    }
}
