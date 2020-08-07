using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Concepts
{
    public class BudgetYear : IEntity
    {
        #region Variables

        private decimal _minutesInDay = 0;

        #endregion     
     

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual decimal MinutesInDay 
        {
            get { return this._minutesInDay; }
            set { _minutesInDay = value; }
        }
       
        public virtual int CalcMinutesInFarvardin
        {
            get;
            set;
        }
        public virtual int CalcMinutesInOrdibehesht
        {
            get;
            set;
        }
        public virtual int CalcMinutesInKhordad
        {
            get;
            set;
        }
        public virtual int CalcMinutesInTir
        {
            get;
            set;
        }
        public virtual int CalcMinutesInMordad
        {
            get;
            set;
        }
        public virtual int CalcMinutesInShahrivar
        {
            get;
            set;
        }
        public virtual int CalcMinutesInMehr
        {
            get;
            set;
        }
        public virtual int CalcMinutesInAban
        {
            get;
            set;
        }
        public virtual int CalcMinutesInAzar
        {
            get;
            set;
        }
        public virtual int CalcMinutesInDey
        {
            get;
            set;
        }
        public virtual int CalcMinutesInBahman
        {
            get;
            set;
        }
        public virtual int CalcMinutesInEsfand
        {
            get;
            set;
        }

        /// <summary>
        /// مقدار سهمیه بندی مرخصی
        /// </summary>
        public virtual int Totoal { get; set; }

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
                        return CalcMinutesInFarvardin;

                    case 1:
                        return CalcMinutesInOrdibehesht;

                    case 2:
                        return CalcMinutesInKhordad;

                    case 3:
                        return CalcMinutesInTir;

                    case 4:
                        return CalcMinutesInMordad;

                    case 5:
                        return CalcMinutesInShahrivar;

                    case 6:
                        return CalcMinutesInMehr;

                    case 7:
                        return CalcMinutesInAban;

                    case 8:
                        return CalcMinutesInAzar;

                    case 9:
                        return CalcMinutesInDey;

                    case 10:
                        return CalcMinutesInBahman;
                    case 11:
                        return CalcMinutesInEsfand;
                }
                throw new OutOfExpectedRangeException("0", "11", index.ToString(),
                         "GTS.Clock.Model.Concepts.Budget.Index.Get", ExceptionType.CRASH);   
            }
            set
            {
                switch (index)
                {
                    case 0:

                        CalcMinutesInFarvardin = value;
                        break;

                    case 1:

                        CalcMinutesInOrdibehesht = value;
                        break;

                    case 2:

                        CalcMinutesInKhordad = value;
                        break;

                    case 3:

                        CalcMinutesInTir = value;
                        break;

                    case 4:

                        CalcMinutesInMordad = value;
                        break;

                    case 5:

                        CalcMinutesInShahrivar = value;
                        break;

                    case 6:

                        CalcMinutesInMehr = value;
                        break;

                    case 7:

                        CalcMinutesInAban = value;
                        break;

                    case 8:

                        CalcMinutesInAzar = value;
                        break;

                    case 9:

                        CalcMinutesInDey = value;
                        break;

                    case 10:

                        CalcMinutesInBahman = value;
                        break;
                    case 11:

                        CalcMinutesInEsfand = value;
                        break;
                    default:
                        throw new OutOfExpectedRangeException("0", "11", index.ToString(),
                        "GTS.Clock.Model.Concepts.Budget.Index.Set", ExceptionType.CRASH);   

                }
            }
        }

        #endregion
    }
}
