using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class ProceedTrafficPaireController
    {
        #region variable

        private int curentIndex = -1;
        private IList<ProceedTrafficPair> proceedTrafficPairList = new List<ProceedTrafficPair>();
        ProceedTrafficPair edingTrafficPair = new ProceedTrafficPair();
        ProceedTraffic _proceedTraffic;

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trafficList">یک لیست از ترددهای زوج مرتبی که میخواهیم آنرا پردازش کنیم</param>
        public ProceedTrafficPaireController(ProceedTraffic proceedTraffic)
        {
            _proceedTraffic = proceedTraffic;
            proceedTrafficPairList = _proceedTraffic.Pairs;
            if (proceedTrafficPairList != null && proceedTrafficPairList.Count > 0)
            {
                curentIndex = 0;
            }
            IsInited = false;
        }
        #endregion

        #region Properties

        /// <summary>
        ///آیا کنترلر مقدار دهی اولیه شده است یا تردد پردازش شده بتازگی ایجاد شده است
        /// </summary>
        public bool IsInited
        {
            get;
            set;
        }

        /// <summary>
        /// ایندکس آیتم فعلی
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                if (this.Count == 0)
                {
                    throw new Exception("The List Is Empty: GTS.Clock.Model.Concepts.ProceedTrafficPaireController.CurrentIndex");
                }
                return curentIndex;
            }
        }

        /// <summary>
        /// خالی بودن
        /// </summary>
        public bool IsEmpty 
        {
            get { return this.Count == 0; }
        }

        /// <summary>
        /// آیتم فعلی
        /// </summary>
        public ProceedTrafficPair CurrentPaireItem
        {
            get
            {
                if (this.Count > 0)
                {
                    return proceedTrafficPairList[curentIndex];
                }
                else
                {
                    throw new Exception("The List Is Empty: GTS.Clock.Model.Concepts.ProceedTrafficPaireController.CurrentPaireItem");
                }
            }
        }

        /// <summary>
        /// آیتم بعدی
        /// </summary>
        public ProceedTrafficPair NextPaireItem
        {
            get
            {
                if (this.CurrentIndex < this.Count - 1)
                {
                    return proceedTrafficPairList[CurrentIndex + 1];
                }
                else
                {
                    throw new Exception("The List Is Empty: GTS.Clock.Model.Concepts.ProceedTrafficPaireController.NextPaireItem");
                }
            }
        }

        /// <summary>
        /// آیا آیتم بعدی وجود دارد
        /// </summary>
        public bool HasNextItem
        {
            get
            {
                if (curentIndex < this.Count - 1)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// آیتم آخر
        /// </summary>
        public ProceedTrafficPair LastItem
        {
            get
            {
                if (Count > 0)
                {
                    return proceedTrafficPairList[Count - 1];
                }
                throw new Exception("there is no item in the list:GTS.Clock.Model.Concepts.ProceedTrafficPaireController.LastItem");
            }
        }

        /// <summary>
        /// آیتم در حال ویرایش
        /// </summary>
        public ProceedTrafficPair CurrentEdingItem
        {
            get { return edingTrafficPair; }
            set { edingTrafficPair = value; }
        }

        /// <summary>
        /// تعداد جفتهای تردد پردازش شده کهکارمان با آنها تمام شده است
        /// </summary>
        public int Count
        {
            get
            {
                if (proceedTrafficPairList == null)
                {
                    return 0;
                }
                return proceedTrafficPairList.Count;
            }
        }

        /// <summary>
        /// فاصله بین خروج آیتم فعلی تا ورود آیتم بعدی
        /// </summary>
        public int GapBetweenCurrentAndNextItem
        {
            get
            {
                if (this.HasNextItem)
                {
                    return System.Math.Abs(this.NextPaireItem.From - this.CurrentPaireItem.To);
                }
                return -1;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// یک تردد 1 دقیقه ای بمنظور ایجاد تعادل ثبت میکند
        /// </summary>
        public void AddBallanceTraffic(ProceedTraffic _pf)
        {
            int time = CurrentEdingItem.From;
            ProceedTrafficPair p = new ProceedTrafficPair(time - 1, time);
            //p.PishCardID = 0;
            p.Precard = new Precard() { ID = 0 };
            p.IsFilled = true;
            p.ProceedTraffic = _pf;
            proceedTrafficPairList.Add(p);
        }
        /// <summary>
        /// فیلد در حال ویرایش را به لیست اضافه میکند
        /// </summary>
        public void EditFinish()
        {
            if (edingTrafficPair.From == 0 && edingTrafficPair.To == 0 && !edingTrafficPair.IsFilled)
            {
                return;
            }
            if (proceedTrafficPairList == null)
            {
                proceedTrafficPairList = new List<ProceedTrafficPair>();
            }
            edingTrafficPair.ProceedTraffic = _proceedTraffic;
            proceedTrafficPairList.Add(edingTrafficPair);
            edingTrafficPair = new ProceedTrafficPair();
        }

        public void ImportBasicTraffic(BasicTraffic basic1, BasicTraffic basic2, Precard precard, bool forceFilled)
        {
            if (basic1.Time == 0 && basic2.Time == 0)
            {
                return;
            }
            edingTrafficPair.From = basic1.Time;
            edingTrafficPair.BasicTrafficIdFrom = basic1.ID;
            edingTrafficPair.BasicTrafficIdFromDate = basic1.Date;

            edingTrafficPair.To = basic2.Time;
            edingTrafficPair.BasicTrafficIdTo = basic2.ID;
            edingTrafficPair.BasicTrafficIdToDate = basic2.Date;
            edingTrafficPair.IsFilled = true;
            if (basic2.ID == 0 && !forceFilled)
            {
                edingTrafficPair.IsFilled = false;
            }
            edingTrafficPair.Precard = precard;

        }
        /// <summary>
        /// Initilize the CurentEditingItem
        /// </summary>
        /// <param name="basic1"></param>
        /// <param name="basic2"></param>
        public void ImportBasicTraffic(BasicTraffic basic1, BasicTraffic basic2,bool hasNextItem)
        {
            IsInited = true;
            //  basic1.Used = true;

            edingTrafficPair.IsFilled = true;
            if (basic2.ID == 0)
            {
                edingTrafficPair.IsFilled = false;
            }
            edingTrafficPair.Precard = basic1.Precard;

            edingTrafficPair.From = basic1.Time;
            edingTrafficPair.BasicTrafficIdFrom = basic1.ID;
            edingTrafficPair.BasicTrafficIdFromDate = basic1.Date;
            if (hasNextItem)
            {
                edingTrafficPair.To = basic2.Time;
                edingTrafficPair.BasicTrafficIdTo = basic2.ID;
                edingTrafficPair.BasicTrafficIdToDate = basic2.Date;
            }
            else 
            {
                edingTrafficPair.To = -1000;
                edingTrafficPair.BasicTrafficIdTo = 0;
            }
        }
        public void ImportBasicTraffic(DateTime date1, int time1, DateTime date2, int time2, Precard precard)
        {
            IsInited = true;
            //  basic1.Used = true;
            edingTrafficPair.From = time1;
            edingTrafficPair.BasicTrafficIdFrom = 0;
            edingTrafficPair.BasicTrafficIdFromDate = date1.Date;

            edingTrafficPair.IsFilled = true;
            edingTrafficPair.Precard = precard;

            edingTrafficPair.To = time2;
            edingTrafficPair.BasicTrafficIdTo = 0;
            edingTrafficPair.BasicTrafficIdToDate = date2.Date;
        }
        public void Reset()
        {
            if (this.Count > 0)
            {
                curentIndex = 0;
            }
            else
            {
                curentIndex = -1;
            }
        }
        public void RemoveNextItem()
        {
            proceedTrafficPairList.RemoveAt(CurrentIndex + 1);
        }
        /// <summary>
        /// ابتدای آیتم جاری را به انتهای آیتم بعدی وصل میکند و آیتم بعدی را حذف میکند
        /// </summary>
        public void MergeCurrentItemWithNextItem()
        {
            if (this.HasNextItem)
            {
                this.CurrentPaireItem.To = this.NextPaireItem.To;
                proceedTrafficPairList.RemoveAt(CurrentIndex + 1);
            }
        }

        /// <summary>
        /// آیتم جاری را به جلو میبرد
        /// </summary>
        public void MoveToNextPairItem()
        {
            if (this.HasNextItem)
            {
                curentIndex++;
            }
        }
        #endregion
    }
}
