using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Business.TrafficMapping
{

    /// <summary>
    /// نیمه شبهای مجازی با اعمال نیمه شب و وضعیت شیفتها نسبت به یکدیگر
    /// </summary>
    public class VirtualMidNightList : IDisposable
    {
        #region Variables
        private const int DayMinutes = 1440;
        private const int HourTelorance = 2;
        private List<VirtualMidNight> _vmnList = new List<VirtualMidNight>();
        private IList<AssignedRuleParameter> RuleParameterList;
        Person person = null;
        #endregion

        #region Properties
        public int Length
        {
            get { return _vmnList.Count; }
        }

        /// <summary>
        /// شروع و پایان شبانه روز
        /// </summary>
        public class MidNightTimeSt : ICloneable
        {
            public int Start;
            public int End;
            public int BeforeTelorance;
            public int AfterTelorance;
            public MidNightTimeSt()
            {
                Start = 0;
                End = 0;
                BeforeTelorance = 0;
                AfterTelorance = 0;
            }

            public virtual object Clone()
            {
                MidNightTimeSt obj = new MidNightTimeSt();
                obj.AfterTelorance = this.AfterTelorance;
                obj.BeforeTelorance = this.BeforeTelorance;
                obj.End = this.End;
                obj.Start = this.Start;
                return obj;
            }

        }
        public bool EndOfDayIsForce
        {
            get;
            set;
        }

        /// <summary>
        /// در صورتی که شروع و پایان شبانه روز در شیفت مشخص شده باشد آنرا جداگانه 
        /// برمیگرداند در غیر این صورت شروع و پایان یکسان برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private MidNightTimeSt GetMidNightTime(DateTime date)
        {
            MidNightTimeSt st = new MidNightTimeSt();
            BaseShift shift = this.person.GetShiftByDate(date, "EndOfDay");
            date = date.Date;
            st.Start = st.End = DayMinutes - 1;
            if (shift != null && shift.PairCount > 0)
            {
                st.Start = shift.Pairs.First().From;
                st.End = shift.Pairs.First().To;
                st.BeforeTelorance = shift.Pairs.First().BeforeTolerance;
                st.AfterTelorance = shift.Pairs.First().AfterTolerance;
                if (st.End - st.Start < 2)
                {
                    st.End = st.Start;
                }
            }
            else
            {
                if (this.person.AssignedRuleList != null)
                {
                    AssignedRule ar = this.person.AssignedRuleList.Where(x => x.FromDate <= date && x.ToDate >= date && x.IdentifierCode == 1015).FirstOrDefault();
                    if (ar != null)
                    {
                        IList<AssignedRuleParameter> asp = RuleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= date && x.ToDate >= date).ToList();
                        if (asp != null)
                        {
                            AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

                            if (firstParam != null)
                                st.Start = st.End = Utility.ToInteger(firstParam.Value);
                        }
                    }
                }
            }
            return st;
        }

        public DateTime CalcFromDate { get; set; }
        public DateTime CalcToDate { get; set; }

        #endregion

        public VirtualMidNightList(Person _person, bool endOfDayIsForce,DateTime calcFromDate,DateTime calcToDate,IList<AssignedRuleParameter> ruleParameterList)
        {
            try
            {
                RuleParameterList = ruleParameterList;

                this.CalcFromDate = calcFromDate;
                this.CalcToDate = calcToDate;
                this.person = _person;
                EndOfDayIsForce = endOfDayIsForce;
                //{
                //    AssignedWGDShift assignWGD = (AssignedWGDShift)_person.GetShiftByDate(_person.CalcDateZone.FromDate);
                //    if (assignWGD.Pairs.Count > 0)
                //    {
                //        ShiftPair fSpwd = assignWGD.Pairs.OrderBy(x => x.From).First();
                //        ShiftPair lSpwd = assignWGD.Pairs.OrderBy(x => x.From).Last();
                //        decimal lAfterTelorance = 0;// lSpwd.AfterTolerance;
                //        int to = lSpwd.To + (int)lAfterTelorance;
                //        if (to > DayMinutes)
                //        {
                //            to -= DayMinutes;
                //        }
                //        VirtualMidNight vmn = new VirtualMidNight(assignWGD.Date, to, fSpwd.BeforeTolerance, lSpwd.AfterTolerance);
                //        _vmnList.Add(vmn);
                //    }
                //}
                MidNightTimeSt midNightTime = this.GetMidNightTime(_person.CalcDateZone.FromDate);
                MidNightTimeSt beforeMidNight = new MidNightTimeSt() { Start = -1 };
                MidNightTimeSt lastMidNight = null;
                foreach (DateTime CalcDate in _person.CalcDateZone)
                {
                    try
                    {

                        beforeMidNight = midNightTime;
                        midNightTime = this.GetMidNightTime(CalcDate);
                        AssignedWGDShift tomorrowShift = (AssignedWGDShift)_person.GetShiftByDate(CalcDate.AddDays(1));
                        AssignedWGDShift todayShift = (AssignedWGDShift)_person.GetShiftByDate(CalcDate);


                        if (todayShift.Pairs.Count > 0)
                        {
                            lastMidNight = (MidNightTimeSt)midNightTime.Clone();
                            //جلوگیری از تداخل نیمه شبهای مجازی شیفتهای متوالی
                            //در حالت اول شبانه روز اول محدود میشود و در حالت دوم شبانه روز دوم را محدود میکنیم
                            //مثال حالت اول مانند وقتی که روز اول دارای شیفت 23:50 تا +7:00 با پایان شبانه روز 20:00 باشد و روز دوم دارای شیفت 7:00 تا 17:00 با پایان شبانه روز 6:00 باشد
                            //مثال حالت دوم مانند وقتی که روز اول دارای شیفت 7:00 تا +7:00 با پایان شبانه روز 7:00 باشد و روز دوم دارای شیفت 7:00 تا 17:00 با پایان شبانه روز 6:00 باشد
                            // فرض بر این است که حالت دوم تنها در هنگام شیفت 24 ساعته بروز میکند و باید "تا" پایان شبانه روز شیفت مقدار بگیرد  - در مثال بالا +7:00 میباشد
                            if (beforeMidNight.Start > midNightTime.Start && (beforeMidNight.End - DayMinutes) < midNightTime.Start)
                            {
                                VirtualMidNight lastVmn = _vmnList.Last();
                                lastVmn.SecondMidLimit = midNightTime.Start - 1;
                            }
                            else if(beforeMidNight.Start > midNightTime.Start)
                            {
                                int end = beforeMidNight.End;
                                if (end > DayMinutes)
                                {
                                    end -= DayMinutes;
                                }
                                midNightTime.Start = end + 1;
                            }                           

                            VirtualMidNight vmn = GetMidNightOFShift(todayShift, midNightTime);
                            _vmnList.Add(vmn);
                        }
                        else
                        {
                            //این بلاک بجای قسمت مربوط به درج نیمه شب در تعطیلات اضافه گردید
                            VirtualMidNight vmn = new VirtualMidNight();
                            if (lastMidNight != null)//استفاده از روزهای قبل که شیفت داشته است و عدم استفاده از قانون پایان شبانه روز
                            {
                                vmn = new VirtualMidNight(CalcDate, lastMidNight.Start, lastMidNight.BeforeTelorance, lastMidNight.AfterTelorance);
                            }
                            else
                            {
                                vmn = new VirtualMidNight(CalcDate, midNightTime.Start, midNightTime.BeforeTelorance, midNightTime.AfterTelorance);
                            }
                            _vmnList.Add(vmn);
                            //فقط وقتی بکار میآید که در روزهای متوالی شیفت داشته باشیم
                            if (beforeMidNight.Start > 0)
                            {
                                midNightTime = (MidNightTimeSt)beforeMidNight.Clone();
                            }
                            beforeMidNight.Start = -1;

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                //افزودن نیمه شب مجازی برای روزهایی که شیفت نداشته است
                //از 10 روز قبل شروع میکنیم تا برای اولین ترددها مشکلی ایجاد نشود
                #region without shift days
                if (_vmnList.Count > 0)
                {
                    VirtualMidNight lastVmn = new VirtualMidNight();
                    _vmnList = _vmnList.OrderBy(x => x.DateTime).ToList();

                    DateTime startDate = _vmnList.First().Date.AddDays(-10);
                    DateTime endDate = _vmnList.OrderBy(x => x.Date).Last().Date;


                    for (DateTime dateCounter = startDate; dateCounter < endDate; dateCounter = dateCounter.AddDays(1))
                    {
                        try
                        {
                            if (!_vmnList.Any(x => x.Date == dateCounter.Date))
                            {
                                // VirtualMidNight vmn = new VirtualMidNight(dateCounter, midNightTime);
                                //اگر روزی شیفت نداشت ما باید نیمه شب مجازی اضافه کنیم که همان روز را پوشش دهد(نیمه دوم )در
                                //مورد مکان های مشترک اگر شخص در حال ورود بود باید نیمه شب مجازی دوم و اگر در حال خروج بود
                                //باید نیمه شب مجازی اول به ان تعلق گیرد

                                VirtualMidNight vmn = new VirtualMidNight();
                                if (_vmnList.Any(x => x.Date == dateCounter.Date.AddDays(1)) &&
                                    _vmnList.Any(x => x.Date == dateCounter.Date.AddDays(-1)) &&
                                    _vmnList.Where(x => x.Date == dateCounter.Date.AddDays(1)).First().Time
                                    == _vmnList.Where(x => x.Date == dateCounter.Date.AddDays(-1)).First().Time)
                                {
                                    lastVmn = lastVmn = _vmnList.Where(x => x.Date == dateCounter.Date.AddDays(1)).First();
                                    vmn = new VirtualMidNight(dateCounter, lastVmn.Time, lastVmn.BeforeTelorance, lastVmn.AfterTelorance);
                                }
                                if (_vmnList.Any(x => x.Date == dateCounter.Date.AddDays(-1)))
                                {
                                    lastVmn = _vmnList.Where(x => x.Date == dateCounter.Date.AddDays(-1)).First();
                                    vmn = new VirtualMidNight(dateCounter, lastVmn.Time, lastVmn.BeforeTelorance, lastVmn.AfterTelorance);
                                }
                                else
                                {
                                    vmn = new VirtualMidNight(dateCounter, midNightTime.Start, midNightTime.BeforeTelorance, midNightTime.AfterTelorance);
                                    lastVmn = vmn;
                                }
                                _vmnList.Add(vmn);
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    _vmnList = _vmnList.OrderBy(x => x.DateTime).ToList();
                } 
                #endregion
              
                foreach (VirtualMidNight vmn in _vmnList)
                {
                    vmn.DayPart = GetMainDayRegion(vmn, _person);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// دو شیفت را دریافت کرده و بررسی مسکند که آیا این دو بهم چسبیده اند یا خیر
        /// </summary>       
        /// <param name="telorance">مقدار تلورانسی که اگر این دو شیفت با هم فاصله داشته باشند 
        /// هم بهم چسبیده بحساب می آیند</param>     
        private bool AreShiftsConnected(AssignedWGDShift shift1, AssignedWGDShift shift2, int telorance)
        {
            try
            {
                ShiftPair fSpwd1 = shift1.Pairs.OrderBy(x => x.From).FirstOrDefault();
                decimal fBeforeTelorance1 = fSpwd1.BeforeTolerance;
                decimal fAfterTelorance1 = fSpwd1.AfterTolerance;
                ShiftPair lSpwd1 = shift1.Pairs.OrderBy(x => x.From).LastOrDefault();
                decimal lBeforeTelorance1 = lSpwd1.BeforeTolerance;
                decimal lAfterTelorance1 = lSpwd1.AfterTolerance;

                ShiftPair fSpwd2 = shift2.Pairs.OrderBy(x => x.From).FirstOrDefault();
                decimal fBeforeTelorance2 = fSpwd2.BeforeTolerance;
                decimal fAfterTelorance2 = fSpwd2.AfterTolerance;
                ShiftPair lSpwd2 = shift2.Pairs.OrderBy(x => x.From).LastOrDefault();
                decimal lBeforeTelorance2 = lSpwd2.BeforeTolerance;
                decimal lAfterTelorance2 = lSpwd2.AfterTolerance;


                if (shift1.Date == shift2.Date)
                {
                    if (lSpwd1.To + lAfterTelorance1 + telorance >= fSpwd2.From - fAfterTelorance2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //اگر تاریخ ها باهم تفاوت داشت باید بررسی کنیم که آیا 
                //شیفت در فردا ادامه دارد و اگر دارد تا شیفت بعدی ادامه دارد یا خیر
                else if (shift2.Date > shift1.Date && lSpwd1.To + lAfterTelorance1 > DayMinutes)
                {
                    int days = Convert.ToInt32((lSpwd1.To + lAfterTelorance1) / DayMinutes);
                    DateTime endDate = shift1.Date.AddDays(days);

                    int remainTime = lSpwd1.To + (int)lAfterTelorance1 - days * DayMinutes;
                    if (endDate >= shift2.Date && remainTime + telorance >= fSpwd2.From - fBeforeTelorance2)
                    {
                        return true;
                    }
                }
                else if (shift1.Date > shift2.Date && lSpwd2.To + lAfterTelorance2 > DayMinutes)
                {
                    int days = Convert.ToInt32((lSpwd2.To + lAfterTelorance2) / DayMinutes);
                    DateTime endDate = shift2.Date.AddDays(days);

                    int remainTime = lSpwd2.To + (int)lAfterTelorance2 - days * DayMinutes;
                    if (endDate >= shift1.Date && remainTime + telorance >= fSpwd1.From - fBeforeTelorance1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private VirtualMidNight GetMidNightOFShift(AssignedWGDShift shift, MidNightTimeSt midNight)
        {
            try
            {
                VirtualMidNight result = new VirtualMidNight();
                ShiftPair firstPair = shift.Pairs.OrderBy(x => x.From).First();

                ShiftPair lastPair = shift.Pairs.OrderBy(x => x.From).Last();

                int time = 0;
                DateTime date = new DateTime();

                //فرض شده است که شیفت بهم چسبیده نداریم
                //برای شیفتی که انتهای آن فردا است
                if (lastPair.To > DayMinutes &&
                     lastPair.To > midNight.Start && !EndOfDayIsForce)
                {
                    date = shift.Date;
                    time = lastPair.To;//+ (int)lAfterTelorance1;
                    result = new VirtualMidNight(date, time, firstPair.BeforeTolerance, lastPair.AfterTolerance);
                }

                result = new VirtualMidNight(shift.Date, midNight.Start, midNight.BeforeTelorance, midNight.AfterTelorance);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VirtualMidNight this[int index]
        {
            get
            {
                if (index < Length)
                {
                    return _vmnList[index];
                }
                else
                {
                    throw new Exception("Out of range:GTS.Clock.Business.TrafficMapping.VirtualMidNightList");
                }
            }
        }
      
        public VirtualMidNight GetMidNight(DateTime date, int time, bool enter,bool applyMidNightTelorance)
        {
            if (time < 0) 
            {
                time += DayMinutes;
            }
            List<VirtualMidNight> vmnList;
            VirtualMidNight tmpVmn = new VirtualMidNight(date, time);

            if (applyMidNightTelorance)
            {
                vmnList = _vmnList
                     .Where(x => (x.FirstMid.StartDateTime.AddMinutes(-1 * x.BeforeTelorance) <= tmpVmn.DateTime
                                       && x.FirstMid.EndDateTime >= tmpVmn.DateTime)
                     ||
                                 (x.SecondMid.IsNotEmpty && x.SecondMid.StartDateTime <= tmpVmn.DateTime
                                       && x.SecondMid.EndDateTime/*.AddMinutes(x.AfterTelorance)*/ >= tmpVmn.DateTime)
                     ).ToList();
            }
            else
            {
                vmnList = _vmnList
                     .Where(x => (x.FirstMid.StartDateTime <= tmpVmn.DateTime
                                       && x.FirstMid.EndDateTime >= tmpVmn.DateTime)
                     ||
                                 (x.SecondMid.IsNotEmpty && x.SecondMid.StartDateTime <= tmpVmn.DateTime
                                       && x.SecondMid.EndDateTime >= tmpVmn.DateTime)
                     ).ToList();
            }
            if (!EndOfDayIsForce)
            {
                //تکرار با اعمال تلورانس
                //falat 00086011 1388/2/11 عامل ایجاد این کار بود

                if (vmnList.Count == 0)
                {
                    int telorance = HourTelorance;
                    if (enter)
                    {
                        vmnList = _vmnList
                             .Where(x => (x.FirstMid.StartDateTime.AddHours(telorance) <= tmpVmn.DateTime
                                               && x.FirstMid.EndDateTime >= tmpVmn.DateTime)
                             ||
                                         (x.SecondMid.IsNotEmpty && x.SecondMid.StartDateTime <= tmpVmn.DateTime
                                               && x.SecondMid.EndDateTime >= tmpVmn.DateTime)
                             ).ToList();
                    }
                    else
                    {
                        vmnList = _vmnList
                             .Where(x => (x.FirstMid.StartDateTime <= tmpVmn.DateTime
                                               && x.FirstMid.EndDateTime >= tmpVmn.DateTime)
                             ||
                                         (x.SecondMid.IsNotEmpty && x.SecondMid.StartDateTime <= tmpVmn.DateTime
                                               && x.SecondMid.EndDateTime.AddHours(telorance) >= tmpVmn.DateTime)
                             ).ToList();
                    }
                }
            }
           
            vmnList = vmnList.OrderBy(x => x.FirstMid.StartDateTime).ToList();
                       
            if (vmnList.Count > 0)
            {              
                if (vmnList.Count == 0 && !enter)
                {
                    vmnList.Add(tmpVmn);
                }
                if (enter)
                    return vmnList.Last();
                else
                    return vmnList.First();
            }          

            int midNightTime = this.GetMidNightTime(date).Start;
            return new VirtualMidNight(date, midNightTime, 0, 0);

        }

        /// <summary>
        /// مشخص سازی قسمت اصلی بازه با توجه به شیفت
        /// </summary>
        /// <param name="vmn"></param>
        /// <param name="_person"></param>
        public FirstOrSecond GetMainDayRegion(VirtualMidNight vmn, Person _person)
        {
            if (vmn.SecondMid.Length == 0)
            {
                return FirstOrSecond.First;
            }
            if (vmn.FirstMid.Length == 0)
            {
                return FirstOrSecond.Second;
            }


            DateTime firstRegDate = vmn.FirstMid.Date;
            DateTime secondRegDate = vmn.SecondMid.Date;

            AssignedWGDShift shift1 = (AssignedWGDShift)_person.GetShiftByDate(firstRegDate);
            AssignedWGDShift shift2 = (AssignedWGDShift)_person.GetShiftByDate(secondRegDate);

            if (shift1.PairCount > 0 && shift2.PairCount > 0)
            {
                if (HasIntersect(shift1, vmn.FirstMid))
                {
                    return FirstOrSecond.First;
                }
                else if (HasIntersect(shift2, vmn.SecondMid))
                {
                    return FirstOrSecond.Second;
                }
            }
            else if (shift2.PairCount == 0)
            {
                return FirstOrSecond.First;
            }
            else if (shift1.PairCount == 0)
            {
                return FirstOrSecond.Second;
            }

            return FirstOrSecond.First;
        }

        private bool HasIntersect(AssignedWGDShift shift, GTS.Clock.Business.TrafficMapping.VirtualMidNight.VirtualDayRegion region) 
        {
            if(shift.Date != region.Date)
                return false;
            int fromTime=region.StartDateTime.Hour*60+region.StartDateTime.Minute;
            int toTime=region.EndDateTime.Hour*60+region.EndDateTime.Minute;
            if (shift != null && shift.PairCount > 0) 
            {
                foreach (ShiftPair pair in shift.Pairs)
                {
                    IList<IPair> tmp1 = new List<IPair>();

                    if (pair.To <= DayMinutes)
                    {
                        tmp1.Add(pair);
                    }
                    else
                    {
                        tmp1.Add(new ShiftPair() { From = pair.From, To = DayMinutes - 1 });
                    }


                    IList<IPair> tmp2 = new List<IPair>();
                    tmp2.Add(new ShiftPair() { From = fromTime, To = toTime });

                    var pairsResult = (from BB in
                                           (from A in tmp1
                                            from B in tmp2
                                            select new PairableScndCnpValuePair(Math.Max(A.From, B.From), Math.Min(A.To, B.To)))
                                       where BB.From < BB.To
                                       select BB);
                    return pairsResult.OfType<IPair>().ToList<IPair>().Count > 0;

                }
            }
            return false;
        }

        #region IDisposable Members

        public void Dispose()
        {
            this._vmnList.Clear();
        }

        #endregion
    }
}