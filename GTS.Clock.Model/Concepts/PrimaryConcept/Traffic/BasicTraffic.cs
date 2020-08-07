using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts
{
    public class BasicTraffic : ICloneable, IEntity
    {
        #region variables
        private DateTime date = new DateTime();
        private SortOrder sortOrder = SortOrder.asc;
        #endregion


        #region Constructor

        public BasicTraffic()
        {
            //init
            //date = new DateTime(1390, 1, 1);
        }
        public BasicTraffic(DateTime _date, decimal _precard, int _time)
        {
            Precard precard = new Precard();
            precard.ID = _precard;
            precard.Active = true;
            this.Date = _date;
            this.Time = _time;
            this.Precard = precard;
            Used = false;
        }
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Person Person
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Person OperatorPerson
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Precard Precard
        {
            get;
            set;
        }

        /// <summary>
        /// شناسه پیشکارت
        /// </summary>
        public virtual decimal PrecardID
        {
            get { return Precard.ID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Date
        {
            get
            {
                return date.Date;
            }
            set
            {
                date = value.Date;
            }
        }

        /// <summary>
        /// تاریخ و ساعت را برمیگرداند
        /// </summary>
        public virtual DateTime DateTime
        {
            get
            {
                int hours = ((int)(Time / 60));
                int minutes = Time % 60;
                System.DateTime _date = new DateTime(date.Year, date.Month, date.Day, hours, minutes, 0); ;
                return _date;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Time
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Used
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// اگر بوسیله اپراتور وارد سیستم شده باشد برابر "درست"خواهد بود
        /// </summary>
        public virtual bool Manual
        {
            get;
            set;
        }

        /// <summary>
        /// آگر شخص کارت بزند برابر صفر 
        /// اگر بوسیله اپراتور وارد سیستم شده باشد و ساعتی باشد یک و
        ///اگر بوسیله اپراتور وارد سیستم شده باشد و روزانه باشد دو میباشد
        ///در صورت مشاهده آیتم ساعتی و روزانه در یک روز اگر این فیلد صفر باشد قسمت آن روز ساعتی و روز بعد 
        ///روزانه حساب میشود   
        ///
        /// </summary>
        public virtual int EntryState
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual GTS.Clock.Model.BaseInformation.Clock Clock { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual SortOrder SorittingOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public virtual bool Transferred { get; set; }

        #endregion

        #region IClonable Members
        public virtual object Clone()
        {
            BasicTraffic basicTraffic = new BasicTraffic(this.date, this.PrecardID, this.Time);
            basicTraffic.Active = this.Active;
            basicTraffic.EntryState = this.EntryState;
            basicTraffic.ID = this.ID;
            basicTraffic.Manual = this.Manual;
            basicTraffic.Person = this.Person;
            basicTraffic.Precard = this.Precard;
            basicTraffic.sortOrder = this.sortOrder;
            basicTraffic.Used = this.Used;

            return basicTraffic;
        }
        #endregion

        public override string ToString()
        {
            return String.Format("{0} , {1}", Utility.ToPersianDate(Date), this.DateTime.TimeOfDay.ToString());
        }

        #region Static Method

        public static IRepository<BasicTraffic> GetEntityRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IRepository<BasicTraffic>, BasicTraffic>(Disconnectedly);
        }

        #endregion

    }
}
