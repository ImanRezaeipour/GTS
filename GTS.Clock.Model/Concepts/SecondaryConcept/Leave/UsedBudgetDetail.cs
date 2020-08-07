using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Concepts
{
    public class UsedLeaveDetail : IEntity
    {       
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual DateTime Date
        {
            get;
            set;
        }

        public virtual int Value
        {
            get;
            set;
        }

        public virtual UsedBudget UsedBudget
        {
            get;
            set;
        }

        #endregion
    }
}
