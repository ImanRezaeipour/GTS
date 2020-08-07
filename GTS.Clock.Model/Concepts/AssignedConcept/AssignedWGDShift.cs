using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using System.Globalization;

namespace GTS.Clock.Model
{
    public class AssignedWGDShift : BaseShift
    {
        #region Properties

        public virtual decimal PersonId
        {
            get;
            set;
        }

        public virtual DateTime Date
        {
            get;
            set;
        }

        public virtual decimal ShiftId
        {
            get;
            set;
        }

        public int MinNobatKari
        {
            get;
            set;
        }

        #endregion

        #region Methods

        ///// <summary>
        ///// مقدار کارکردلازم ماهانه را برمیگرداند
        ///// </summary>
        ///// <returns></returns>
        //public int Total()
        //{
        //    PersianDateTime shiftDate = new PersianDateTime(this.Date);
        //    return this.Pairs.Where(x => shiftDate.PersianCalendar.GetYear(x.Date) == shiftDate.Year && 
        //                                shiftDate.PersianCalendar.GetMonth(x.Date) == shiftDate.Month)
        //                        .Sum(x => x.Value);
        //}

        //public int TotalDay()
        //{
        //    PersianDateTime shiftDate = new PersianDateTime(this.Date);
        //    return this.Pairs.Where(x => shiftDate.PersianCalendar.GetYear(x.Date) == shiftDate.Year &&
        //                                shiftDate.PersianCalendar.GetMonth(x.Date) == shiftDate.Month)
        //                        .Count(); 
        //}

        #endregion

        public override string ToString()
        {
            PersianDateTime p = new PersianDateTime(Date);
            string msg = p.PersianDate;
            foreach (ShiftPair paire in Pairs)
            {
                msg += String.Format(" ({0}-{1}) ", Utility.IntTimeToRealTime(paire.From), Utility.IntTimeToRealTime(paire.To));
            }
            return msg;
        }
    }
}
