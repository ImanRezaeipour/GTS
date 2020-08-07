using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Concepts
{
    public class ShiftPair : BasePair, IEntity
    {
        #region Variables

        private string afterTeloranceTime, beforeTeloranceTime, fromTime, toTime;

        #endregion

        #region Constructors

        public ShiftPair()
            : this(0, 0)
        { }

        public ShiftPair(int from, int to)
            : base(from, to)
        {
        }

        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public int AfterTolerance
        {
            get;
            set;
        }

        public int BeforeTolerance
        {
            get;
            set;
        }

        public decimal ShiftId
        {
            get;
            set;
        }

        /// <summary>
        /// تلورانس را برای نمایش در واسط کاربر به زمان تبدیل میکند
        /// </summary>
        public virtual string AfterToleranceTime
        {
            get
            {
                if (Utility.IsEmpty(afterTeloranceTime))
                    afterTeloranceTime = Utility.IntTimeToRealTime((int)AfterTolerance);
                return afterTeloranceTime;
            }
            set
            {
                afterTeloranceTime = value;
                AfterTolerance = Utility.RealTimeToIntTime(afterTeloranceTime);
            }
        }

        /// <summary>
        /// تلورانس را برای نمایش در واسط کاربر به زمان تبدیل میکند
        /// </summary>
        public virtual string BeforeToleranceTime
        {
            get
            {
                if (Utility.IsEmpty(beforeTeloranceTime))
                    beforeTeloranceTime = Utility.IntTimeToRealTime((int)BeforeTolerance);
                return beforeTeloranceTime;
            }
            set
            {
                beforeTeloranceTime = value;
                BeforeTolerance = Utility.RealTimeToIntTime(beforeTeloranceTime);
            }
        }

        /// <summary>
        /// دقیقه را برای نمایش در واسط کاربر به زمان تبدیل میکند
        /// </summary>
        public virtual string FromTime
        {
            get
            {
                if (Utility.IsEmpty(fromTime))
                    fromTime = Utility.IntTimeToRealTime(From);
                return fromTime;
            }
            set
            {
                fromTime = value;
                From = Utility.RealTimeToIntTime(fromTime);
            }
        }

        /// <summary>
        /// دقیقه را برای نمایش در واسط کاربر به زمان تبدیل میکند
        /// </summary>
        public virtual string ToTime
        {
            get
            {
                if (Utility.IsEmpty(toTime))
                    toTime = Utility.IntTimeToRealTime(To);
                if (this.To > 1440) 
                {
                    toTime = "+" + Utility.IntTimeToRealTime(To - 1440);
                    this.NextDayContinual = true;
                }
                return toTime;
            }
            set
            {
                toTime = value;
                To = Utility.RealTimeToIntTime(toTime);
                if (NextDayContinual) 
                {
                    this.To += 1440;
                }
            }
        }

        /// <summary>
        /// اگر واسط کاربر این عبارت را برابر درست بفرستد
        /// بدین معنی است که انتهای بازه زمان در روزبعد واقع شده است
        /// و باید با 1440 جمع شود
        /// </summary>
        //public virtual bool TimePlusFlag { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// ادامه در روز بعد
        /// </summary>
        public virtual bool NextDayContinual { get; set; }

        public virtual Shift Shift { get; set; }

        public virtual ShiftPairType ShiftPairType { get; set; }

        #endregion
    }
}
