using System;
using System.Linq;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Concepts
{
    /// <summary>
    /// .کلاس پایه برای نگهداری مقادیر مفهوم زوج مرتبی
    /// </summary>
    /// <typeparam name="TPair">نوع زوج مرتبی که مقدار مفهوم از آن استفاده می نماید</typeparam>
    public class BasePairableConceptValue<TPair> : IPairableConceptValue<TPair>
        where TPair : IPair
    {
        #region Variables

        private int value = 0;

        #endregion

        #region Constructors

        public BasePairableConceptValue()
            : this(null)
        { }

        public BasePairableConceptValue(IList<TPair> Pairs)
        {
            if (Pairs == null)
                Pairs = new List<TPair>();
            this.Pairs = Pairs;
        }

        #endregion

        #region IPairableConceptValue<TPair> Members

        public virtual IList<TPair> Pairs
        {
            get;
            set;
        }

        public virtual TPair First
        {
            get { return this.Pairs.FirstOrDefault(); }
        }

        public virtual TPair Last
        {
            get { return this.Pairs.LastOrDefault(); }
        }

        public virtual TPair Intermediate
        {
            get { return this.Pairs[this.PairCount / 2]; }
        }

        public virtual int PairCount
        {
            get { return this.Pairs == null ? 0 : this.Pairs.Count; }
        }

        public virtual TPair PairPart(int Part)
        {
            return this.Pairs[Part];
        }

        public virtual int PairValues
        {
            get { return this.Pairs.Sum(x => x.Value); }
        }

        public virtual string ExPairValues
        {
            get { return Utility.IntTimeToRealTime(this.PairValues); }
        }

        public virtual void AddPairs(IList<TPair> Source)
        {
            this.Pairs.Clear();
            foreach (TPair pair in Source)
                this.Pairs.Add(pair);
        }

        public virtual void AppendPairs(IList<TPair> Source)
        {
            foreach (TPair pair in Source)
                this.Pairs.Add(pair);
        }

        public virtual void RemovePair(TPair Pair)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// مجموع مقادیر زوج مرتب های موجود در قبل از زمان ارسالی را برمی گرداند 
        /// </summary>
        /// <param name="Time">زمانیکه باید مقادیر زوج مرتب های قبل از آن محاسبه شوند</param>
        /// <returns></returns>
        public virtual int TotalBeforeTime(int Time)
        {
            return this.Pairs
                        .Where(x => x.To <= Time)
                        .Sum(x => x.Value);
        }

        /// <summary>
        /// مجموع مقادیر زوج مرتب های موجود در بعد از زمان ارسالی را برمی گرداند 
        /// </summary>
        /// <param name="Time">زمانیکه باید مقادیر زوج مرتب های بعد از آن محاسبه شوند</param>
        /// <returns></returns>
        public virtual int TotalAfterTime(int Time)
        {
            return this.Pairs
                        .Where(x => x.To >= Time)
                        .Sum(x => x.Value);
        }

        /// <summary>
        /// این تابع زوج مرتب مشخص شده با شناسه ورودی را حذف می کند.
        /// در عین حال قبل از حذف مرتب سازی براساس "از" انجام می شود
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemovePairAt(int index)
        {
            ((IList<IPair>)this.Pairs.OrderBy(x => x.From)).RemoveAt(index);
        }

        #endregion

        #region IConceptValue Members

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual int Value
        {
            get
            {
                if (this.PairValues > 0)
                    return this.PairValues;
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public virtual DateTime FromDate
        {
            get;
            set;
        }

        public virtual DateTime ToDate
        {
            get;
            set;
        }

        public virtual bool IsValid
        {
            get;
            set;
        }

        public virtual string ExValue
        {
            get { return Utility.IntTimeToRealTime(this.Value); }
        }

        public virtual Person Person
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            string str = " ";
            str += "Value : " + this.Value.ToString();
            str += " [";
            foreach (IPair pv in this.Pairs)
            {
                str += "(" + Utility.IntTimeToRealTime(pv.From) + "," + Utility.IntTimeToRealTime(pv.To) + ")";
            }
            str += "] ";

            return str;
        }
    }
}
