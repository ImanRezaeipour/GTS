using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Concepts
{
    public class Permit : BasePairableConceptValue<PermitPair>, IEntity
    {

        #region constructors

        public Permit()
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

        #region Methods

        public virtual IList<IPair> FilterByPrecard(params decimal[] precards)
        {
            List<IPair> results = new List<IPair>();
            List<IPair> tmpresults = new List<IPair>();
            tmpresults.Clear();
            foreach (IPair pair in this.Pairs.Where(x => precards.Contains(x.PreCardID))
                                                     .ToList())
            {
                tmpresults.AddRange(Operations.Operation.Union(pair, results).Pairs);
            }
            results.Clear();
            results.AddRange(tmpresults);
            if (!(precards.Contains(125) || precards.Contains(126)))
            {
                return Operations.Operation.Union(results);
            }
            else
            {
                return results;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} , {1}", Utility.ToPersianDate(FromDate), this.ExPairValues);
        }

        #endregion

    }
}
