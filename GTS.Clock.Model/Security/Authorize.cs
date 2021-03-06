using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Security
{
    public class Authorize : IEntity
    {       
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual bool Allow
        {
            get;
            set;
        }   

        public virtual Role Role
        {
            get;
            set;
        }

        public virtual Resource Resource
        {
            get;
            set;
        }         

        #endregion    
    }
}
