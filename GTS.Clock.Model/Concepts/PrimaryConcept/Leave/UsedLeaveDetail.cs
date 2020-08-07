using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Concepts
{
    public class UsedLeaveDetail : IEntity
    {
        public UsedLeaveDetail()
        { }

        public UsedLeaveDetail(int Value, int LeaveMinuteInDay)
        {
            this.Value = Value;
            if (LeaveMinuteInDay > 0)
            {
                this.Day = Value / LeaveMinuteInDay;
                this.Minute = Value % LeaveMinuteInDay;
            }            
        }

        #region Properties

        public virtual decimal ID
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
            get;
            set;
        }

        public virtual int Day
        {
            get;
            set;
        }

        public virtual int Minute
        {
            get;
            set;
        }

        public virtual Person Person
        {
            get;
            set;
        }

        public virtual Permit Permit
        {
            get;
            set;
        }

        #endregion
    }
}
