using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Security
{
    public class Domains : IEntity 
    {
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string UserName
        {
            get;
            set;
        }

        public virtual string Password
        {
            get;
            set;
        }

        public virtual bool Active
        {
            get;
            set;
        }

        public virtual string Domain
        {
            get;
            set;
        }

       #endregion
    }
}