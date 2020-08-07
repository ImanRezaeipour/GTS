using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveSetting
    {
        #region Properties

        public virtual bool UseFutureMounthLeave
        {
            get;
            set;
        }

        public virtual int MinutesInDay
        {
            get;
            set;
        }

        #endregion
   
    }
}
