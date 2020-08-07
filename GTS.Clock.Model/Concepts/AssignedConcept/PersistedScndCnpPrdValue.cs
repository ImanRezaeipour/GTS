using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model
{
    public class PersistedScndCnpPrdValue : IEntity
    {
        public PersistedScndCnpPrdValue()
        {
            FromDate = new DateTime();
            ToDate = new DateTime();
        }

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual int Value
        {
            get;
            set;
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

        public virtual decimal ScndCnpId
        {
            get;
            set;
        }

        public virtual decimal CalcRangeGrpId { get; set; }

        /// <summary>
        /// ایندکس متناظر با داده های این شی را برای استفاده در دیکشنری تعریف شده در کلاس پرسنل، برمیگرداند
        /// </summary>
        public virtual string GetIndex()
        {
            return this.ScndCnpId.ToString() + this.FromDate.ToString("yyyy/MM/dd") + this.ToDate.ToString("yyyy/MM/dd");
        }


        public override bool Equals(object obj)
        {

            return (this.ID == ((PersistedScndCnpPrdValue)obj).ID && this.FromDate == ((PersistedScndCnpPrdValue)obj).FromDate && this.ToDate == ((PersistedScndCnpPrdValue)obj).ToDate);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode() + this.FromDate.GetHashCode() + this.ToDate.GetHashCode();
        }

        #endregion
    }
}
