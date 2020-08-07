using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using System.Globalization;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model
{
    public class PersonWorkGroup : IEntity
    {

        #region Variables

        //private IList<WGDShift> _WGDShift = new List<WGDShift>();

        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string WorkGroupName
        { 
            get; 
            set; 
        }

        public virtual DateTime FromDate
        {
            get;
            set;
        }
        /*
        public virtual DateTime ToDate
        {
            get;
            set;
        } */

        public virtual decimal WorkGroupID
        {
            get;
            set;
        }

        //public IList<WGDShift> WGDShiftList
        //{
        //    get
        //    {
        //        if (this._WGDShift == null || this._WGDShift.Count == 0)
        //            this._WGDShift = PersonWorkGroup.GetPersonWorkGroupRepository(false)
        //                                            // .GetWGDShift(this.WorkGroupID);
        //                                             .GetWGDShiftByFilter(this.WorkGroupID);
        //        return this._WGDShift;
        //    }
        //}

        /*
        /// <summary>
        /// „ﬁœ«— ò«—ò—œ·«“„ „«Â«‰Â —« »—„Ìê—œ«‰œ
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            PersianDateTime shiftDate = new PersianDateTime(this.ToDate);
            return this.WGDShiftList.Where(x => shiftDate.PersianCalendar.GetYear(x.Date) == shiftDate.Year &&
                                        shiftDate.PersianCalendar.GetMonth(x.Date) == shiftDate.Month)
                                .Sum(x => x.Value);
        }

        public int TotalDay()
        {
            PersianDateTime shiftDate = new PersianDateTime(this.ToDate);
            return this.WGDShiftList.Where(x => shiftDate.PersianCalendar.GetYear(x.Date) == shiftDate.Year &&
                                        shiftDate.PersianCalendar.GetMonth(x.Date) == shiftDate.Month)
                                .Count();
        }*/

        #endregion

        #region Static Methods

        public static IPersonWorkGroupRepository GetPersonWorkGroupRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IPersonWorkGroupRepository, PersonWorkGroup>(Disconnectedly);
        }


        #endregion
    }
}
