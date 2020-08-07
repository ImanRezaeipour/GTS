using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Concepts
{
    public class UsedBudget : IEntity
    {
        private decimal _minutesInDay = 0;

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual decimal MinutesInDay
        {
            get { return _minutesInDay; }
            set { _minutesInDay = value; }
        }

        #region Time Properties
        public virtual int MinutesInFarvardin
        {
            get;
            set;
        }
        public virtual int MinutesInOrdibehesht
        {
            get;
            set;
        }
        public virtual int MinutesInKhordad
        {
            get;
            set;
        }
        public virtual int MinutesInTir
        {
            get;
            set;
        }
        public virtual int MinutesInMordad
        {
            get;
            set;
        }
        public virtual int MinutesInShahrivar
        {
            get;
            set;
        }
        public virtual int MinutesInMehr
        {
            get;
            set;
        }
        public virtual int MinutesInAban
        {
            get;
            set;
        }
        public virtual int MinutesInAzar
        {
            get;
            set;
        }
        public virtual int MinutesInDey
        {
            get;
            set;
        }
        public virtual int MinutesInBahman
        {
            get;
            set;
        }
        public virtual int MinutesInEsfand
        {
            get;
            set;
        }
        #endregion 

        public virtual int Length
        {
            get { return 12; }
        }

        public virtual int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return MinutesInFarvardin;

                    case 1:
                        return MinutesInOrdibehesht;

                    case 2:
                        return MinutesInKhordad;

                    case 3:
                        return MinutesInTir;

                    case 4:
                        return MinutesInMordad;

                    case 5:
                        return MinutesInShahrivar;

                    case 6:
                        return MinutesInMehr;

                    case 7:
                        return MinutesInAban;

                    case 8:
                        return MinutesInAzar;

                    case 9:
                        return MinutesInDey;

                    case 10:
                        return MinutesInBahman;
                    case 11:
                        return MinutesInEsfand;
                }
                throw new OutOfExpectedRangeException("0", "11", index.ToString(),
                            "GTS.Clock.Model.Concepts.UsedBudget.Index.Get", ExceptionType.CRASH);                
            }
            set
            {
                switch (index)
                {
                    case 0:

                        MinutesInFarvardin = value;
                        break;

                    case 1:

                        MinutesInOrdibehesht = value;
                        break;

                    case 2:

                        MinutesInKhordad = value;
                        break;

                    case 3:

                        MinutesInTir = value;
                        break;

                    case 4:

                        MinutesInMordad = value;
                        break;

                    case 5:

                        MinutesInShahrivar = value;
                        break;

                    case 6:

                        MinutesInMehr = value;
                        break;

                    case 7:

                        MinutesInAban = value;
                        break;

                    case 8:

                        MinutesInAzar = value;
                        break;

                    case 9:

                        MinutesInDey = value;
                        break;

                    case 10:

                        MinutesInBahman = value;
                        break;
                    case 11:

                        MinutesInEsfand = value;
                        break;
                    default:
                        throw new OutOfExpectedRangeException("0", "11", index.ToString(),
                         "GTS.Clock.Model.Concepts.UsedBudget.Index.Set", ExceptionType.CRASH);   

                }
            }
        }

        public virtual IList<UsedLeaveDetail> DetailList
        {
            get;
            set;
        }
        #endregion
    }
}
