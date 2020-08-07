using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveCalcResult : IEntity
    {
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

        public virtual int DayRemain
        {
            get;
            set;
        }

        public virtual int MinuteRemain
        {
            get;
            set;
        }

        public virtual int DayUsed
        {
            get;
            set;
        }

        public virtual int MinuteUsed
        {
            get;
            set;
        }

        public virtual int LeaveMinuteInDay
        {
            get;
            set;
        }

        public virtual decimal RefrenceId
        {
            get;
            set;
        }

        public virtual string Type
        {
            get;
            set;
        }


        public virtual Person Person
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// بر اساس پارامتر ورودی دقیقه ها را به روز تبدیل می نماید
        /// </summary>
        /// <param name="MinuteLeaveInDay"></param>
        public virtual void DoAdequate(int MinuteLeaveInDay)
        {
            if (MinuteLeaveInDay > 0)
            {
                this.Day += this.Minute / MinuteLeaveInDay;
                this.Minute = this.Minute % MinuteLeaveInDay;
                this.DayRemain += this.MinuteRemain / MinuteLeaveInDay;
                this.MinuteRemain = this.MinuteRemain % MinuteLeaveInDay;
                this.DayUsed += this.MinuteUsed / MinuteLeaveInDay;
                this.MinuteUsed = this.MinuteUsed % MinuteLeaveInDay;

                if (this.MinuteRemain < 0 && this.DayRemain > 0)
                {
                    this.DayRemain--;
                    this.MinuteRemain += MinuteLeaveInDay;
                }
            }
        }

        #endregion
    }
}
