using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.Clientele
{
    public class CL_PermitPair : BasePair, IEntity
    {
        #region Variable

        public int value = 0;

        #endregion

        #region Constructor

        public CL_PermitPair()
            :this(0, 0)
        { }

        public CL_PermitPair(int from, int to)
            : base(from, to)
        {
        }
        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual bool IsFilled { get; set; }

        public virtual decimal OffishTypeID { get; set; }

        public virtual decimal OffishID { get; set; }

        public override int Value { get; set; }

        public virtual CL_Permit Permit { get; set; }

        public virtual CL_OffishType OffishType { get; set; }

        #endregion

    }
}
