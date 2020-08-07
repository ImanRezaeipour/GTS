using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;

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

        public virtual LeaveIncDecAction Type
        {
            get 
            {
                return (Day < 0 || Minute < 0) ? LeaveIncDecAction.Decrease : LeaveIncDecAction.Increase;
            }
        }

        //public virtual LeaveIncDecAction IncDecType
        //{
        //    get;
        //    set;
        //}

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

        public virtual Person Person
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("شخص:{0} تاریخ:{1} روز :{2} ساعت :{3} نوع:{4} میباشد ", this.Person.Name, Utility.ToPersianDate(this.Date), this.Day, Utility.IntTimeToRealTime(this.Minute), this.Type.ToString());
            return summery;
        }

    }
}
