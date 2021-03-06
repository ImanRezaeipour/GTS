using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class ProceedTraffic : BasePairableConceptValue<ProceedTrafficPair>, IEntity
    {

        #region variables
        /// <summary>
        /// Imported BasicTraffics Date Memory List to set FromDate-ToDate of current proceedTraffic
        /// </summary>

        private List<DateTime> basicTrafficDateList = new List<DateTime>();
        private ProceedTrafficPaireController proceedTrafficPaireController;

        #endregion

        #region constructors

        public ProceedTraffic()
            : base(new List<ProceedTrafficPair>())
        {
            proceedTrafficPaireController = new ProceedTrafficPaireController(this);
        }

        public ProceedTraffic(IList<ProceedTrafficPair> pairs)
            : base(pairs)
        {
            proceedTrafficPaireController = new ProceedTrafficPaireController(this);
        }

        #endregion


        #region Properties

        /// <summary>      
        /// نشان دهنده غیر روزانه بودن تردد است
        /// </summary>
        public virtual bool IsNotDaily
        {
            get;
            set;
        }

        public virtual ProceedTrafficPaireController ProceedTrafficPaireController
        {
            get
            {
                return proceedTrafficPaireController;// new ProceedTrafficPaireController(this.Pairs.ToList());
            }
        }

        /// <summary>
        /// طبق صحبت هاي صورت گرفته تا اينجا اگر اخرين زوج مرتب يک تردد پردازش شده ناقص باشد آن تردد ناقص است
        /// </summary>
        public virtual bool IsFilled
        {
            get
            {
                ProceedTrafficPair tmp = this.Last;
                if (tmp == null)
                    return true;
                return this.Last.IsFilled;
            }
        }

        public virtual bool HasDailyItem
        {
            get;
            set;
        }

        public virtual bool HasHourlyItem
        {
            get;
            set;
        }
        #endregion

        #region Methods
     
        public virtual void SaveDate(DateTime _date)
        {
            basicTrafficDateList.Add(_date.Date);
        }

        /// <summary>
        /// تاريخ از و تاريخ تا با توجه به زوج هاي مرتب داخل ليست تايين ميشوند
        /// </summary>
        public virtual void SetFromToDate()
        {
            if (basicTrafficDateList.Count > 0)
            {
                basicTrafficDateList = basicTrafficDateList.OrderBy(x => x.Date).ToList();
                this.FromDate = basicTrafficDateList[0];
                this.ToDate = basicTrafficDateList[basicTrafficDateList.Count - 1];
            }
            else
            {
                throw new Exception("List is empty:GTS.Clock.Model.Concepts.ProceedTraffic.SetFromToDate");
            }
        }

        public override string ToString()
        {
            string msg = "";
            foreach (IPair paire in Pairs)
            {
                msg += String.Format(" {0}->{1} ", paire.ExFrom, paire.ExTo);
            }
            return msg;
        }

        #endregion



    }
}
