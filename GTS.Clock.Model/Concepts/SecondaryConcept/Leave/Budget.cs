using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Concepts
{
    public class Budget : IEntity
    {
        #region Variables

        private decimal _minutesInDay = 0;

        #endregion

        #region Constractors

        public Budget()
            : this(0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MinutesInDay">تعداد دقایق یک روز مرخصی</param>
        public Budget(int minutesInDay)
        {
            _minutesInDay = minutesInDay;
        }

        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual int Year { get; set; }

        /// <summary>
        /// مقدار سهمیه بندی مرخصی
        /// </summary>
        public virtual int Total { get; set; }

        public virtual decimal MinutesInDay 
        {
            get { return _minutesInDay; }
            set { _minutesInDay = value; }
        }

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

        public virtual RuleCategory RuleCategory { get; set; }

        /// <summary>
        ///  A total value or per each month differently
        /// </summary>
        public BudgetType BudgetType { get; set; }

        public string Description { get; set; }


        /// <summary>
        /// مقداری که برای سهمیه توسط کاربر تعیین میشود
        /// </summary>      
        public int GetStandardBudget(int _mounth) 
        {

            switch (_mounth)
            {
                case 1:
                    return MinutesInFarvardin;

                case 2:
                    return MinutesInOrdibehesht;
                case 3:
                    return MinutesInKhordad;
                case 4:
                    return MinutesInTir;
                case 5:
                    return MinutesInMordad;
                case 6:
                    return MinutesInShahrivar;
                case 7:
                    return MinutesInMehr;
                case 8:
                    return MinutesInAban;
                case 9:
                    return MinutesInAzar;
                case 10:
                    return MinutesInDey;
                case 11:
                    return MinutesInBahman;
                case 12:
                    return MinutesInEsfand;
                default:
                    throw new OutOfExpectedRangeException("1", "12", _mounth.ToString(), "Budget.GetStandardBudget(mounth)");

            }
        }

        #endregion
    }
}
