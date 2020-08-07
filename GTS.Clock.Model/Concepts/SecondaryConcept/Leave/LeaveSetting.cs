using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveSettings : IEntity
    {
        #region Properties
        public virtual decimal ID
        {
            get;
            set;
        }
        public virtual Person Person
        {
            get;
            set;
        }
        public virtual DateTime From
        {
            get;
            set;
        }          
        public virtual bool DoNotUseFutureMounthLeave
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
