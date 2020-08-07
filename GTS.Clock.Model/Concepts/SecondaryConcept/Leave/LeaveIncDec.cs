using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveIncDec : IEntity
    {
        #region Properties
        public virtual decimal ID
        {
            get;
            set;
        }
        public virtual LeaveBudgetYear LeaveBudgetYear
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
            get { return DayValue * (int)LeaveBudgetYear.MinutesInDay + TimeValue; }           
        }
        public virtual decimal Minutes
        {
            get { return Value % LeaveBudgetYear.MinutesInDay; }
        }
        public virtual int DayValue
        {
            get;
            set;
        }
        public virtual int TimeValue
        {
            get;
            set;
        }
        public virtual bool Applyed
        {
            get;
            set;
        }
        public virtual string Description
        {
            get;
            set;
        }
        #endregion

        #region Static Methods

        //public static ILeaveIncDecRepository GetLeaveIncDecRepository(bool Disconnectedly)
        //{
        //    return RepositoryFactory.GetRepository<ILeaveIncDecRepository, LeaveIncDec>(Disconnectedly);
        //}

        #endregion
    }
}
