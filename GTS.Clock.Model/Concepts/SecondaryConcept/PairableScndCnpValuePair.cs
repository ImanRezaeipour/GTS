using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class PairableScndCnpValuePair : BasePair, IEntity
    {

        #region Constructors

        public PairableScndCnpValuePair()
            : this(0, 0, null)
        { }

        public PairableScndCnpValuePair(int from, int to)
            : base(from, to)
        {
        }

        public PairableScndCnpValuePair(int from, int to, IPairableConceptValue<IPair> ScndCnpValue)
            : base(from, to)
        {
            this.ScndCnpValue = (PairableScndCnpValue)ScndCnpValue;
        }
        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual PairableScndCnpValue ScndCnpValue { get; set; }

        #endregion



    }
}
