using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Concepts
{
    /// <summary>
    /// .کلاس پایه برای نگهداری زوج مرتب
    /// </summary>
    public class BasePair : IPair,ICloneable 
    {
        #region Constructor

        public BasePair(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        #endregion

        #region IPair Members

        public virtual int From
        {
            get;
            set;
        }

        public virtual int To
        {
            get;
            set;
        }

        public virtual int Value
        {
            get
            {
                if (this.To == -1000 || this.To == 0)
                    return 0;
                int temp = this.To - this.From;
                if (temp < 0)
                    throw new BaseException("مقدار از بزرگتر از مقدار تا مي باشد", this.GetType().ToString() + ".Value");
                return temp;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public virtual string ExFrom
        {
            get { return Utility.IntTimeToRealTime(this.From); }
        }

        public virtual string ExTo
        {
            get { return Utility.IntTimeToRealTime(this.To); }
        }

        public virtual string ExValue
        {
            get { return Utility.IntTimeToRealTime(this.Value); }
        }

        #endregion

        #region IClonable Members
        public virtual object Clone()
        {
            BasePair pair = new BasePair(this.From, this.To);                    

            return pair;
        }
        #endregion
      
        public override string ToString()
        {
            return String.Format(" ({0}-{1}) ", this.ExFrom, this.ExTo);
        }
    }
}
