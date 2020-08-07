using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Permit : BasePairableConceptValue<CL_PermitPair>, IEntity
    {

        #region constructors

        public CL_Permit()
            : base()
        {

        }

        #endregion

        #region Properties

        public virtual bool IsPairly
        {

            get;
            set;
        }

        public override int Value
        {
            get
            {
                if (this.IsPairly)
                {
                    return this.PairValues;
                }
                else
                {
                    if (this.PairCount != 0)
                        return 1;
                }
                return 0;
            }

        }        

        #endregion



    }
}
