using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Model.MonthlyReport
{
    public class CurrentProceedTraffic : IEntity
    {

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual decimal PersonId
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

        public virtual string PrecrdName
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string msg = "";
            msg += String.Format(" {0}->{1} ", this.From, this.To);
            return msg;
        }

        #endregion



    }
}
