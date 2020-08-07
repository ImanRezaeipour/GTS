using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model
{
    public class UpdatePTable: IEntity                
    {

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string Result
        {
            get;
            set;
        }

        #endregion

    }
}
