using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Business.Proxy
{
    public class LeaveBudgetProxy
    {       
        private int _minutesInDay=0;
        public LeaveBudgetProxy(){}

        public int MinutesInDay
        {
            get { return this._minutesInDay; }
            set { this._minutesInDay = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minutesInDay">جهت استفاده در ایندکسر</param>
        public LeaveBudgetProxy(int minutesInDay)
        {
            this._minutesInDay=minutesInDay;
        }
       
        public virtual decimal ID { get; set; }

        public virtual int Year { get; set; }

        public virtual string TotoalDay { get; set; }
        public virtual string TotoalTime { get; set; }

        public virtual string Day1
        {
            get;
            set;
        }
        public virtual string Time1
        {
            get;
            set;
        }

        public virtual string Day2
        {
            get;
            set;
        }
        public virtual string Time2
        {
            get;
            set;
        }

        public virtual string Day3
        {
            get;
            set;
        }
        public virtual string Time3
        {
            get;
            set;
        }

        public virtual string Day4
        {
            get;
            set;
        }
        public virtual string Time4
        {
            get;
            set;
        }

        public virtual string Day5
        {
            get;
            set;
        }
        public virtual string Time5
        {
            get;
            set;
        }

        public virtual string Day6
        {
            get;
            set;
        }
        public virtual string Time6
        {
            get;
            set;
        }

        public virtual string Day7
        {
            get;
            set;
        }
        public virtual string Time7
        {
            get;
            set;
        }

        public virtual string Day8
        {
            get;
            set;
        }
        public virtual string Time8
        {
            get;
            set;
        }

        public virtual string Day9
        {
            get;
            set;
        }
        public virtual string Time9
        {
            get;
            set;
        }

        public virtual string Day10
        {
            get;
            set;
        }
        public virtual string Time10
        {
            get;
            set;
        }

        public virtual string Day11
        {
            get;
            set;
        }
        public virtual string Time11
        {
            get;
            set;
        }

        public virtual string Day12
        {
            get;
            set;
        }
        public virtual string Time12
        {
            get;
            set;
        }

        public BudgetType BudgetType { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// مقدار هر ماه را به دقیقه برمیگرداند
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual LeaveInfo this[int index]
        {
            get
            {
                LeaveInfo l = new LeaveInfo();
                switch (index)
                {
                    case 0:
                        l.Day = Utility.ToInteger(Day1);
                        l.Minute = Utility.RealTimeToIntTime(Time1);
                        break;

                    case 1:
                        l.Day = Utility.ToInteger(Day2);
                        l.Minute = Utility.RealTimeToIntTime(Time2);
                        break;

                    case 2:
                        l.Day = Utility.ToInteger(Day3);
                        l.Minute = Utility.RealTimeToIntTime(Time3);
                        break;

                    case 3:
                        l.Day = Utility.ToInteger(Day4);
                        l.Minute = Utility.RealTimeToIntTime(Time4);
                        break;

                    case 4:
                        l.Day = Utility.ToInteger(Day5);
                        l.Minute = Utility.RealTimeToIntTime(Time5);
                        break;

                    case 5:
                        l.Day = Utility.ToInteger(Day6);
                        l.Minute = Utility.RealTimeToIntTime(Time6);
                        break;

                    case 6:
                        l.Day = Utility.ToInteger(Day7);
                        l.Minute = Utility.RealTimeToIntTime(Time7);
                        break;

                    case 7:
                        l.Day = Utility.ToInteger(Day8);
                        l.Minute = Utility.RealTimeToIntTime(Time8);
                        break;

                    case 8:
                        l.Day = Utility.ToInteger(Day9);
                        l.Minute = Utility.RealTimeToIntTime(Time9);
                        break;

                    case 9:
                        l.Day = Utility.ToInteger(Day10);
                        l.Minute = Utility.RealTimeToIntTime(Time10);
                        break;

                    case 10:
                        l.Day = Utility.ToInteger(Day11);
                        l.Minute = Utility.RealTimeToIntTime(Time11);
                        break;
                    case 11:
                        l.Day = Utility.ToInteger(Day12);
                        l.Minute = Utility.RealTimeToIntTime(Time12);
                        break;
                }
                return l;
                throw new OutOfExpectedRangeException("0", "11", index.ToString(),
                            "GTS.Clock.Business.Proxy.LeaveBudgetProxy.Index.Get", ExceptionType.CRASH);
            }           
        }
    }

}

