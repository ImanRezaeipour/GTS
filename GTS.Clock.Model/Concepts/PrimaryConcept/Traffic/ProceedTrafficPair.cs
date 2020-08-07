using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class ProceedTrafficPair : BasePair, IEntity
    {

        #region Constructor

        public ProceedTrafficPair()
            :this(0, 0)
        { }

        public ProceedTrafficPair(int from, int to)
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

        public bool IsFilled
        {
            get;
            set;
        }

        /// <summary>
        /// اگر برابر "درست" باشد بدین معناست که این زوج مرتب بصورت
        /// صوری درج گردیده است و ابتدا وانتها ندارد
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (this.From == -1000 && this.To == -1000)
                {
                    return true;
                }
                return false;
            }   
        }


        /// <summary>
        /// پیشکارت را برمیگرداند
        /// </summary>
        public decimal PishCardID
        {
            get 
            {
                if (this.Precard == null)
                    return -1;
                return (int)Precard.ID;
            }            
        }

        public int PishcardCode 
        {
            get 
            {
                if (this.Precard == null)
                    return -1;
                return Infrastructure.Utility.Utility.ToInteger(this.Precard.Code);
            }
        }

        public Precard Precard
        {
            get;
            set;
        }

        public decimal BasicTrafficIdFrom
        {
            get;
            set;
        }

        public decimal BasicTrafficIdTo
        {
            get;
            set;
        }

        public decimal PermitIdFrom
        {
            get;
            set;
        }

        public decimal PermitIdTo
        {
            get;
            set;
        }

        /// <summary>
        /// در دیتابیس ذخیره نمیشود
        /// </summary>
        public DateTime BasicTrafficIdFromDate
        {
            get;
            set;
        }

        /// <summary>
        /// در دیتابیس ذخیره نمیشود
        /// </summary>
        public DateTime BasicTrafficIdToDate
        {
            get;
            set;
        }

        public override int Value
        {
            get
            {
                if (this.IsFilled)
                    return base.Value;
                return 0;
            }
        }

        public virtual ProceedTraffic ProceedTraffic { get; set; }

        #endregion

        public override object Clone()
        {
            ProceedTrafficPair pair = new ProceedTrafficPair(this.From, this.To);
            pair.Precard = new Precard() { ID = this.Precard.ID };
            pair.BasicTrafficIdFrom = this.BasicTrafficIdFrom;
            pair.BasicTrafficIdFromDate = this.BasicTrafficIdFromDate;
            pair.BasicTrafficIdTo = this.BasicTrafficIdTo;
            pair.BasicTrafficIdToDate = this.BasicTrafficIdToDate;
            pair.ID = this.ID;
            pair.IsFilled = this.IsFilled;
            pair.ProceedTraffic = null;
            return pair;
        }

        public override string ToString()
        {
            return  string.Format(" {0}->{1}: [{2}]", this.ExFrom, this.ExTo, this.ExValue);
        }

    }
}
