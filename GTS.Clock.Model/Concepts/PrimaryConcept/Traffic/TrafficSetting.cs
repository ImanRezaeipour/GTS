using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts
{
    public class TrafficSettings: IEntity
    {
        #region Properties
        public virtual decimal ID
        {
            get;
            set;
        }
        public virtual Person Person
        {
            get;
            set;
        }
        public virtual int Precard28_29ExpireLength
        {
            get;
            set;
        }
        public virtual bool BeginTimeHourDutyByCart
        {
            get;
            set;
        }
        public virtual bool DutyByPermision
        {
            get;
            set;
        }
        public virtual bool DelayCartIsAllowed
        {
            get;
            set;
        }
        public virtual bool DelayCartServiceIsAllowd
        {
            get;
            set;
        }
        public virtual bool OneEnterOneExit
        {
            get;
            set;
        }
        public virtual bool AutoEnterTraffic
        {
            get;
            set;
        }
        public virtual bool AutoExitTraffic
        {
            get;
            set;
        }
        
        /// <summary>
        /// غیبت ها با طول کمتر از این مقدار را حذف کن
        /// </summary>
        public virtual int TrafficMinLength
        {
            get;
            set;
        }
        /// <summary>
        /// غیبت ها با طول کمتر از این مقدار را حذف کن
        /// </summary>
        public virtual int OutInWorkMax
        {
            get;
            set;
        }

        public virtual int VirtualMidNight
        {
            get;
            set;
        }
        public virtual bool FixVirtualMidNight
        {
            get;
            set;
        }
        
        #endregion
  
    }
}
