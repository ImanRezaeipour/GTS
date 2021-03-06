using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;

//18-1-90 Farz:
//1-daily precard does not exists in basic traffic(clock hardwares does not accept daily precards)
//
//
//
//
namespace GTS.Clock.Business.TrafficMapping
{
    public class TrafficMapper : IDisposable
    {
        #region variables
        Dictionary<string, Precard> precardDic = new Dictionary<string, Precard>();
        const int DayMinutes = 1440;
        const int MidDay = 720;
        const int Telorance = 60;
        const int NotPaired = -1000;
        const int EmpyButPaired = 0;
        const int EmptyDailyTraffic = -1000;
        private bool Reset = false;//اگر قانوني بخواهد بقيه قانونها اجرا نشوند اين را ست ميکند
        bool forceToSaveProceedTraffic = false;
        private Dictionary<decimal, int> virtualMidNight = new Dictionary<decimal, int>();
        private bool? endOfDayIsForce = null;
        VirtualMidNightList virtualMidNightList;
        VirtualMidNight mainVmn = new VirtualMidNight();
        private Person Person;
        GTSEngineLogger logger2 = new GTSEngineLogger();
        IList<AssignedRuleParameter> ruleParameterList;
        IList<decimal> acceptablePrecard = new List<decimal>();
        
        #endregion

        #region Properties


        public int OneEnterOneExit
        {
            get
            {
                DateTime date = DateTime.Now;
                if (!this.Person.BasicTrafficController.Finished)
                    date = this.Person.BasicTrafficController.CurrentBasicItem.Date;

                return this.GetOneEnterOneExit(date);
            }
        }

        public bool AutomaticTraffic
        {
            get
            {
                DateTime date = DateTime.Now;
                if (!this.Person.BasicTrafficController.Finished)
                    date = this.Person.BasicTrafficController.CurrentBasicItem.Date;

                return this.GetAutomaticTraffic(date);
            }
        }

        /// <summary>
        /// اولویت به ترتیب شیفت , قانون , پیشفرض
        /// </summary>
        public int VirtualMidNight
        {
            get
            {
                DateTime date = DateTime.Now;
                if (!this.Person.BasicTrafficController.Finished)
                    date = this.Person.BasicTrafficController.CurrentBasicItem.Date;
                BaseShift shift = this.Person.GetShiftByDate(date, "EndOfDay");

                int midNight = DayMinutes - 1;
                if (shift != null && shift.PairCount > 0)
                {
                    midNight = shift.Pairs.First().From;
                }
                else
                {
                    midNight = this.EndOfDay(date);
                }
                return midNight;

            }
        }

        public bool EndOfDayIsForce
        {
            get
            {
                if (endOfDayIsForce == null)
                {
                    DateTime date = DateTime.Now;
                    if (!this.Person.BasicTrafficController.Finished)
                        date = this.Person.BasicTrafficController.CurrentBasicItem.Date;

                    endOfDayIsForce = this.GetEndOfDayIsForce(date);
                }
                return endOfDayIsForce == null ? false : (bool)endOfDayIsForce;
            }
        }

        /// <summary>
        /// باید از پارامتر قوانین خوانده شود
        /// </summary>
        public int TrafficMinLength
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// باید از پارامتر قوانین خوانده شود
        /// فواصل خروج تا ورود کارکرد شود
        /// </summary>
        public int OutInAllow
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// باید از پارامتر قوانین خوانده شود
        /// پیشکارت 28 و 29
        /// </summary>
        public bool Precard28_29IsAllowed
        {
            get
            {
                return false;
            }
        }

        public DateTime CalculationFromDate { get; set; }
        public DateTime CalculationToDate { get; set; }

        #endregion

        #region Constructor

        public TrafficMapper(Person person, DateTime fromDate, DateTime toDate)
        {
            this.Person = person;
            this.CalculationFromDate = fromDate;
            this.CalculationToDate = toDate;
            CheckDbTrafics(person);
            InitPishcads();
            IRuleRepository ruleRep = Rule.GetRuleRepository(false);
            ruleParameterList = ruleRep.GetAssginedRuleParamList(CalculationFromDate, CalculationToDate);
        }

        /// <summary>
        /// برای اطمینان بیشتر نشانگر محاسبات بروزرسانی میشود
        /// مشکل در دو تردد پردازش دشه در یک روز
        /// </summary>
        /// <param name="person"></param>
        private void CheckDbTrafics(Person person)
        {
            BasicTrafficRepository rep = new BasicTrafficRepository();
            DateTime minDate = rep.GetMinInvalidDate(person.ID);
            if (minDate != Utility.GTSMinStandardDateTime)
            {
                CalculationDateRangeRepository cfpRep = new CalculationDateRangeRepository(false);
                cfpRep.InvalidTraficAndPermit(person.ID, minDate);
            }
        }

        #endregion

        /// <summary>
        /// نقطه شروع کار جفت کننده ترددها
        /// </summary>
        public void DoMap()
        {
            #region Synchronize

            TrafficSync trafficSync = new TrafficSync(this, Person);
            trafficSync.Synchronize();

            #endregion

            ///دریافت تنظیمات ترددها و مقداردهی نیمه شبهای مجازی
            ///همچنین مشخص کردن ابتدای محاسبات با توجه به شیفت جاری
            #region local variables & initilize

            BasicTrafficController basicController = Person.BasicTrafficController;
            basicController.Reset();

            virtualMidNightList = new VirtualMidNightList(Person, this.EndOfDayIsForce, CalculationFromDate, CalculationToDate, ruleParameterList);
            ProceedTraffic proceedTraffic = new ProceedTraffic();
            List<ProceedTraffic> proceedList = new List<ProceedTraffic>();
            basicController.SortByTime(SortOrder.asc);

            acceptablePrecard.Add(this.GetPrecardId(Precards.Enter));
            acceptablePrecard.Add(this.GetPrecardId(Precards.Usual));
            acceptablePrecard.Add(this.GetPrecardId(Precards.Exit));

            if (!basicController.Finished)
            {
                mainVmn = GetNearShiftDate(basicController.CurrentBasicItem, Person, true, true);


            #endregion


                #region last proccedd traffic
                this.Person.ProceedTrafficList = this.Person.ProceedTrafficList
                                    .OrderBy(x => x.FromDate)
                                    .ThenBy(x => x.ID).ToList();
                ProceedTraffic lastPt = this.Person.ProceedTrafficList
                                    .LastOrDefault();

                this.DeleteLastTraffic(lastPt, basicController, trafficSync);
                
                #endregion

                while (basicController.CanFindPair || (!basicController.Finished && !basicController.CanFindPair && !basicController.CurrentBasicItem.Used))
                {
                    VirtualMidNight tmpVmn = GetNearShiftDate(basicController.CurrentBasicItem, Person, true, true);
                    bool shouldSave = tmpVmn != mainVmn;
                    shouldSave = proceedTraffic.Pairs != null && proceedTraffic.Pairs.Count > 0 && !proceedTraffic.IsFilled ? true : shouldSave;

                    if (shouldSave || forceToSaveProceedTraffic) //شيفت عوض شده پس بايد تردد پردازش شده جديد شود
                    {
                        if (proceedTraffic.ProceedTrafficPaireController.IsInited)
                        {
                            forceToSaveProceedTraffic = false;
                            /************/
                            RuleOnProceedTrafficRunner(Person, ref proceedTraffic);
                            /************/
                            SaveProceedTraffic(Person, proceedTraffic);
                            mainVmn = GetNearShiftDate(basicController.CurrentBasicItem, Person, true, true);
                        }
                        proceedTraffic = new ProceedTraffic();
                    }

                    proceedTraffic.ProceedTrafficPaireController.ImportBasicTraffic(basicController.CurrentBasicItem, basicController.NextItem, basicController.HasNextItem);


                    if (!basicController.CurrentBasicItem.Used)
                    {
                        Reset = false;
                        basicController.CurrentBasicItem.Used = true;
                        /*******************/
                        RuleOnBasicTrafficRunner(Person, ref proceedTraffic);
                        /*******************/
                        if (OneEnterOneExit <= 0)
                        {
                            tmpVmn = GetNearShiftDate(basicController.CurrentBasicItem, Person, true, true);
                            ImportBaseTraffic(Person, proceedTraffic, tmpVmn.Date);
                        }
                        else
                        {
                            BasicTraffic basic = new BasicTraffic() { Time = proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From, Date = proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdFromDate };

                            VirtualMidNight vmn = GetNearShiftDate(basic, Person, true, true);
                            ImportBaseTraffic(Person, proceedTraffic, vmn.Date);
                        }
                        basicController.MoveCurrentToNextUnusedItem();
                    }
                }
            }
            if (proceedTraffic.Pairs.Count > 0)
            {
                RuleOnProceedTrafficRunner(Person, ref proceedTraffic);
                SaveProceedTraffic(Person, proceedTraffic);
            }
            //در حالت بالا آخرین آیتم که مثلا ورود است را در خروجی نشان نمیدهد
            if (!basicController.Finished && !basicController.CanFindPair)
            {
                proceedTraffic = new ProceedTraffic();
                basicController.CurrentBasicItem.Used = true;
                proceedTraffic.ProceedTrafficPaireController.ImportBasicTraffic(basicController.CurrentBasicItem, new BasicTraffic(), basicController.CurrentBasicItem.Precard, false);
                proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = NotPaired;
                proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = false;
                proceedTraffic.IsNotDaily = true;
                proceedTraffic.HasDailyItem = false;
                proceedTraffic.HasHourlyItem = true;
                ImportBaseTraffic(Person, proceedTraffic, basicController.CurrentBasicItem.Date);
                SaveProceedTraffic(Person, proceedTraffic);
                proceedTraffic = new ProceedTraffic();
            }

            /*******************/
            //Auto Enter Auto Exit
            if (this.AutomaticTraffic)
            {
                foreach (DateTime CalcDate in Person.CalcDateZone)
                {
                    AssignedWGDShift shift = (AssignedWGDShift)Person.GetShiftByDate(CalcDate);
                    if (shift.Pairs.Count == 0)
                        continue;
                    if (Person.ProceedTrafficList.Where(x => x.FromDate.Date == shift.Date.Date).Count() == 0)
                    {
                        proceedTraffic = new ProceedTraffic();
                        proceedTraffic.ProceedTrafficPaireController
                            .ImportBasicTraffic(shift.Date, shift.Pairs.First().From, shift.Date, shift.Pairs.Last().To, GetPrecard(Precards.Usual));
                        ImportBaseTraffic(Person, proceedTraffic, shift.Date);
                        RuleOnProceedTrafficRunner(Person, ref proceedTraffic);
                        SaveProceedTraffic(Person, proceedTraffic);
                    }
                }
            }
            /*******************/
            ProceedTraffic pt = new ProceedTraffic();
            RuleOnProceedTrafficList(Person, ref pt);
            /*******************/
        }


        #region T Methods

        /// <summary>
        ///در صورتی که ورود بزرگتر از خروج بود و تاریخ خروج جلو تر از ورود بود
        ///با مقدار شبانه روز جمع شود
        ///قبل از قانون 24
        ///مثلا گاهی ورود 23:59 و خروج 00:00 است
        /// </summary>        
        private void T1(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From >

                    _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To 
                    && _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To!=NotPaired)
                {
                    if (_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdFromDate <

                    _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdToDate)
                    {

                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To += DayMinutes;
                    }
                }
                //برای وقتی که جفت دوم داخل روز بعد باشد
                if (!Utility.IsEmpty(_proceedTraffic.Pairs))
                {
                    if (_proceedTraffic.Pairs.Any(x => x.From > DayMinutes || x.To > DayMinutes))
                    {
                        if (_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From > 0 &&
                            _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From < DayMinutes)
                            _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From += DayMinutes;

                        if (_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To > 0 &&
                            _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To < DayMinutes)
                            _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To += DayMinutes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T1", _person.BasicTrafficController.CurrentBasicItem.Date, ex);
            }
        }

        /// <summary>
        ///اگر تردد خروج برابر نیمه شب واقعی باشد یک دقیقه زیاد شود
        /// </summary>        
        private void T2(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To == DayMinutes)
                {
                    _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To++;
                }

                if (_person.BasicTrafficController.CurrentBasicItem.Time == DayMinutes)
                {
                    _person.BasicTrafficController.CurrentBasicItem.Time++;
                }
                if (_person.BasicTrafficController.NextItem.Time == DayMinutes)
                {
                    _person.BasicTrafficController.NextItem.Time++;
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T2", _person.BasicTrafficController.CurrentBasicItem.Date, ex);
            }
        }

        /// <summary>
        /// اگر هردو تردد پايه در يک تردد پردازش شده نيگنجند بايد تردد ناقص ثبت شود
        /// در دو حالت زير براي شخص تا انتهاي شيفت و از انتهاي شيفت به بعد تردد مجازي ثبت ميگردد
        /// 1:شيفت فعلي به شيفت بعدي چسبيده باشد
        /// 2:تردد بعدي با پيشکارت خروج ثبت شده باشد
        /// قانون يکي در ميان ورود و خروج ثبت شود هم بررسي ميشود        
        /// </summary>
        private void T3(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (Reset) return;

                if (_person.BasicTrafficController.HasNextItem && OneEnterOneExit <= 0)
                {
                    if (!AreInSameProceedTraffic(_person.BasicTrafficController.CurrentBasicItem, _person.BasicTrafficController.NextItem, true))
                    {
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = NotPaired;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = false;
                        _person.BasicTrafficController.NextItem.Used = false;

                    }

                    else if (_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From > _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To
                        && _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdFromDate.Date >= _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdToDate.Date)
                    {
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = NotPaired;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = false;
                        _person.BasicTrafficController.NextItem.Used = false;
                    }
                    else
                    {
                        _person.BasicTrafficController.CurrentBasicItem.Used = true;
                        _person.BasicTrafficController.NextItem.Used = true;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T3", _person.BasicTrafficController.CurrentBasicItem.Date, ex);
            }
        }

        /// <summary>
        ///تردد ها یک در میان ورود و خروج لحاظ گردد
        /// </summary>
        private void T4(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (Reset) return;

                if (OneEnterOneExit > 0 && _person.BasicTrafficController.HasNextItem)
                {
                    if ((_person.BasicTrafficController.NextItem.DateTime - _person.BasicTrafficController.CurrentBasicItem.DateTime).TotalMinutes <= OneEnterOneExit)
                    {
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = _person.BasicTrafficController.NextItem.Time;
                        _person.BasicTrafficController.NextItem.Used = true;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = true;
                    }
                    else
                    {
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = NotPaired;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = false;
                        _person.BasicTrafficController.NextItem.Used = false;
                    }

                    //check 2 after traffic
                    if (_person.BasicTrafficController.AfterNextItem.Date > _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdFromDate)
                    {
                        forceToSaveProceedTraffic = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T4", _person.BasicTrafficController.CurrentBasicItem.Date, ex);
            }
        }

        /// <summary>
        ///اعمال تلورانس پایان شبانه روز
        ///بعد از قانون 3 اجرا میشود
        /// </summary>
        private void T5(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                return;
                if (Reset) return;

                if (OneEnterOneExit <= 0 && _person.BasicTrafficController.HasNextItem)
                {
                    if (!_proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled 
                        &&
                        AreInSameProceedTraffic(_person.BasicTrafficController.CurrentBasicItem, _person.BasicTrafficController.NextItem, true))
                    {
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.From = NotPaired;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = NotPaired;
                        _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = false;
                        _person.BasicTrafficController.NextItem.Used = false;
                        _person.BasicTrafficController.CurrentBasicItem.Used = false;
                        forceToSaveProceedTraffic = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T5", _person.BasicTrafficController.CurrentBasicItem.Date, ex);
            }
        }

        /// <summary>
        /// تردد مجازی
        /// نیمه شب مجازی تنها وقتی اعمال میشود که شخص در روز تردد اول و در روز تردد دوم شیفت داشته باشد
        /// نیمه شب مجازی تنها وقتی اعمال میشود که تردد ها با پیشکارت ورود و خروج ثبت شده باشند
        /// </summary>
        /// <param name="_person"></param>
        /// <param name="_proceedTraffic"></param>
        private void T6(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (Reset) return;
                if (_person.BasicTrafficController.CurrentBasicItem.ID > 0 && GetInsertVirtualTraffic(_person.BasicTrafficController.CurrentBasicItem.Date) && _person.BasicTrafficController.HasNextItem)
                {
                    if (_person.BasicTrafficController.CurrentBasicItem.PrecardID == this.GetPrecardId(Precards.Enter)
                        &&
                        _person.BasicTrafficController.NextItem.PrecardID == this.GetPrecardId(Precards.Exit))
                    {
                        VirtualMidNight vmn1 = this.GetNearShiftDate(_person.BasicTrafficController.CurrentBasicItem, _person, true, true);
                        VirtualMidNight vmn2 = this.GetNearShiftDate(_person.BasicTrafficController.NextItem, _person, false, true);
                        if (vmn1 != vmn2 && _person.BasicTrafficController.CurrentBasicItem.Date < _person.BasicTrafficController.NextItem.Date)
                        {
                            if ((_person.BasicTrafficController.NextItem.Date - _person.BasicTrafficController.CurrentBasicItem.Date).Days==1) 
                            {
                                _person.BasicTrafficController.NextItem.Used = false;

                                BasicTraffic exitVirtualTraffic = new BasicTraffic(vmn1.SecondMid.Date, this.GetPrecardId(Precards.Exit), vmn1.Time);
                                BasicTraffic enterVirtualTraffic = new BasicTraffic(vmn1.SecondMid.Date, this.GetPrecardId(Precards.Enter), vmn1.Time + 1);

                                _person.BasicTrafficController.InsertTrafficAfterCurentItem(exitVirtualTraffic, enterVirtualTraffic);


                                _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.To = exitVirtualTraffic.Time;
                                _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.BasicTrafficIdToDate = exitVirtualTraffic.Date;
                                _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = true;
                                _person.BasicTrafficController.NextItem.Used = true;
                            }                                   
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T6", _person.BasicTrafficController.CurrentBasicItem.Date, ex);
            }
        }


        /// <summary>
        /// فواصل خروج تا ورود  تا سقف --- کارکرد محسوب شود        
        /// </summary>        
        private void T10(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                //if (Reset) return;
                int trafficMinimumLength = this.OutInAllow;
                if (trafficMinimumLength > 0)
                {
                    _proceedTraffic.ProceedTrafficPaireController.Reset();
                    while (_proceedTraffic.ProceedTrafficPaireController.HasNextItem)
                    {
                        int gap = _proceedTraffic.ProceedTrafficPaireController.GapBetweenCurrentAndNextItem;
                        if (gap <= trafficMinimumLength && gap > 0
                           && _proceedTraffic.ProceedTrafficPaireController.CurrentPaireItem.PishCardID
                                            == _proceedTraffic.ProceedTrafficPaireController.NextPaireItem.PishCardID)
                        {
                            if (_proceedTraffic.ProceedTrafficPaireController.NextPaireItem.To == NotPaired)
                            {
                                _proceedTraffic.ProceedTrafficPaireController.CurrentPaireItem.To =
                                    _proceedTraffic.ProceedTrafficPaireController.NextPaireItem.From;
                                _proceedTraffic.ProceedTrafficPaireController.RemoveNextItem();
                            }
                            else
                            {
                                _proceedTraffic.ProceedTrafficPaireController.MergeCurrentItemWithNextItem();
                                gap = _proceedTraffic.ProceedTrafficPaireController.GapBetweenCurrentAndNextItem;
                                if (gap > trafficMinimumLength)
                                {
                                    _proceedTraffic.ProceedTrafficPaireController.MoveToNextPairItem();
                                }
                            }
                        }
                        else
                        {
                            _proceedTraffic.ProceedTrafficPaireController.MoveToNextPairItem();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T14", _proceedTraffic.FromDate, ex);
            }
        }    

        /// <summary>
        /// init has hourly item
        /// </summary>        
        private void T11(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (_proceedTraffic.Pairs.Count > 0)
                {
                    for (int i = 0; i < _proceedTraffic.Pairs.Count; i++)
                    {
                        if (_proceedTraffic.Pairs[i].IsFilled)
                        {
                            _proceedTraffic.HasHourlyItem = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T18", _proceedTraffic.FromDate, ex);
            }
        }

        /// <summary>
        /// زوجهایی که یکی از جفتهای آنها در روز بعد افتاده باشد را با 1440 جمع میکند
        /// </summary>        
        private void T12(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (_proceedTraffic.Pairs.Count > 0)
                {
                    for (int i = 0; i < _proceedTraffic.Pairs.Count; i++)
                    {
                        if ((_proceedTraffic.Pairs[i].To < _proceedTraffic.Pairs[i].From
                            ||
                            _proceedTraffic.Pairs[i].BasicTrafficIdFromDate.Date < _proceedTraffic.Pairs[i].BasicTrafficIdToDate.Date)
                            && _proceedTraffic.Pairs[i].To > 0
                            && _proceedTraffic.Pairs[i].To < DayMinutes)
                        {
                            _proceedTraffic.Pairs[i].To += DayMinutes;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T18", _proceedTraffic.FromDate, ex);
            }
        }

        /// <summary>
        ///اگر آخرین قسمت ترددی ناقص بود و ابتدای آن خارج از شیفت بود آنرا حذف کن
        ///برای وقتی که شخص به اشتباه دوبار خروج زده باشد کاربرد دارد
        /// </summary>        
        private void T13(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (_proceedTraffic.Pairs.Count > 1)
                {
                    if (_proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].IsFilled == false
                        && (
                           (_proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].From - _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 2].To < 10
                            && _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].From - _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 2].To > 0)
                        || (_proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].From - _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 2].To < 10
                            && _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].From - _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 2].To > 0))
                        )
                    {
                        _proceedTraffic.SetFromToDate();
                        AssignedWGDShift wgd = GetShift(_person, _proceedTraffic.FromDate, _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].From);
                        if (wgd.ID == 0)
                        {
                            //تردد خروج  به اشتباه دوبار زده شده است
                            _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 2].To =
                                _proceedTraffic.Pairs[_proceedTraffic.Pairs.Count - 1].From;
                            _proceedTraffic.Pairs.RemoveAt(_proceedTraffic.Pairs.Count - 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T13", _proceedTraffic.FromDate, ex);
            }
        }

        /// <summary>
        /// ورود و خروج اتوماتیک
        /// </summary>
        private void T20(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (this.AutomaticTraffic)
                {
                    foreach (DateTime CalcDate in Person.CalcDateZone)
                    {
                        AssignedWGDShift awgd = (AssignedWGDShift)Person.GetShiftByDate(CalcDate);
                        if (awgd.Pairs.Count == 0)
                            continue;

                        if (_person.ProceedTrafficList.Where(x => x.FromDate.Date == awgd.Date).Count() == 0)
                        {
                            BasicTraffic b1 = new BasicTraffic(awgd.Date, GetPrecardId(Precards.Usual), awgd.Pairs.First().From);
                            BasicTraffic b2 = new BasicTraffic(awgd.Date, GetPrecardId(Precards.Usual), awgd.Pairs.Last().To);

                            _proceedTraffic.ProceedTrafficPaireController.ImportBasicTraffic(b1, b2, true);
                            _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.IsFilled = true;
                            _proceedTraffic.ProceedTrafficPaireController.CurrentEdingItem.Precard = GetPrecard(Precards.Usual);
                            ImportBaseTraffic(_person, _proceedTraffic, b1.Date);
                            _proceedTraffic.IsNotDaily = true;
                            SaveProceedTraffic(_person, _proceedTraffic);
                            _proceedTraffic = new ProceedTraffic();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T16", ex);
            }

        }

        /// <summary>
        /// اعمال مجوزهای تردد بر روی کلیه ترددهای پردازش شده
        /// این قانون در انتها اجرا میگردد
        /// </summary>
        /// <param name="_person"></param>
        /// <param name="_proceedTraffic"></param>
        private void T21(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (OneEnterOneExit > 0 && _person.PermitList != null && _person.PermitList.Count > 0)
                {
                    IList<Permit> allPermitList = _person.PermitList.Where(x => x.Pairs.Where(y => !y.IsApplyedOnTraffic && y.PreCardID == GetPrecardId(Precards.Usual)).Count() > 0)
                        .OrderBy(x => x.FromDate).ToList();
                    if (allPermitList != null && allPermitList.Count > 0)
                    {
                        #region Crate Info List
                        DateTime startDate = allPermitList.First().FromDate;
                        _person.ProceedTrafficList = _person.ProceedTrafficList.Where(x => x.FromDate < startDate).OrderBy(x => x.FromDate).ToList();
                        decimal lastExitId = 0;
                        if (_person.ProceedTrafficList != null && _person.ProceedTrafficList.Count > 0)
                            if (_person.ProceedTrafficList.Last().Pairs != null && _person.ProceedTrafficList.Last().Pairs.Count > 0)
                                lastExitId = _person.ProceedTrafficList.Last().Pairs.Last().BasicTrafficIdTo;

                        var baseicTraffics = _person.BasicTrafficList.Where(x => x.Date >= startDate && x.Active && x.ID != lastExitId).ToList();
                        var permitList = allPermitList.Where(x => x.FromDate >= startDate).ToList();
                        List<PairInfo> infoList = new List<PairInfo>();

                        var btafics = from o in baseicTraffics
                                      where o.Time != NotPaired
                                      select new PairInfo { Time = o.Time, BasicId = o.ID, Date = o.Date, PrecardID = o.PrecardID };
                        infoList.AddRange(btafics);

                        foreach (Permit permit in permitList)
                        {
                            var permitFroms = from o in permit.Pairs
                                              where o.From != NotPaired && o.PreCardID == GetPrecardId(Precards.Usual)
                                              select new PairInfo { Time = o.From, PermitId = o.ID, Date = o.Permit.FromDate, PrecardID = o.PreCardID };

                            var permitTos = from o in permit.Pairs
                                            where o.To != NotPaired && o.PreCardID == GetPrecardId(Precards.Usual)
                                            select new PairInfo { Time = o.To, PermitId = o.ID, Date = o.Permit.FromDate, PrecardID = o.PreCardID };

                            infoList.AddRange(permitFroms);
                            infoList.AddRange(permitTos);


                            permit.Pairs.All(x => x.IsApplyedOnTraffic = true);
                        }
                        #endregion



                        infoList = infoList.Distinct().ToList();

                        #region حذف آیتم تکراری
                        var unique = from o in infoList
                                     group o by new { o.PrecardID, o.Date, o.Time };

                        infoList = new List<PairInfo>();
                        foreach (var group in unique)
                        {
                            foreach (var info in group)
                            {
                                infoList.Add(info);
                                break;
                            }
                        }
                        #endregion

                        infoList = infoList.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();

                        DateTime lastDate = infoList.First().Date;
                        DateTime curentDate = infoList.First().Date;
                        ProceedTraffic pt = new ProceedTraffic();
                        pt.FromDate=Utility.GTSMinStandardDateTime;
                        pt.Pairs = new List<ProceedTrafficPair>();
                        bool filed = true; int itemUsed = 0;
                        for (int i = 0; i < infoList.Count - 1; i = filed ? i + 2 : i + 1)
                        {
                            lastDate = infoList[i].Date;
                            if (curentDate < lastDate)
                            {
                                #region insert ProceedTraffic
                                pt.Person = _person;
                                pt.HasHourlyItem = true;
                                Person.ProceedTrafficList.Add(pt);

                                pt = new ProceedTraffic();
                                pt.FromDate=Utility.GTSMinStandardDateTime;
                                pt.Pairs = new List<ProceedTrafficPair>();

                                curentDate = lastDate;
                                #endregion
                            }
                            if (infoList[i].Date == infoList[i + 1].Date
                                            ||
                                infoList[i].Date.AddDays(1) == infoList[i + 1].Date)
                            {
                                #region Pair
                                ProceedTrafficPair pair = new ProceedTrafficPair();
                                pair.BasicTrafficIdFrom = infoList[i].BasicId;
                                pair.PermitIdFrom = infoList[i].PermitId;
                                pair.BasicTrafficIdFromDate = infoList[i].Date;
                                pair.From = infoList[i].Time;

                                pair.BasicTrafficIdTo = infoList[i + 1].BasicId;
                                pair.PermitIdTo = infoList[i + 1].PermitId;
                                pair.BasicTrafficIdToDate = infoList[i + 1].Date;
                                pair.To = infoList[i + 1].Time;
                                if (infoList[i].Date.AddDays(1) == infoList[i + 1].Date)
                                {
                                    pair.To += DayMinutes;
                                }

                                pair.IsFilled = true;
                                pair.Precard = new Precard() { ID = infoList[i].PrecardID };

                                pair.ProceedTraffic = pt;
                                pt.Pairs.Add(pair);
                                if (pt.FromDate == Utility.GTSMinStandardDateTime)
                                {
                                    BasicTraffic basic = new BasicTraffic() { Time = pair.From, Date = pair.BasicTrafficIdFromDate };
                                    VirtualMidNight vmn = GetNearShiftDate(basic, Person, true, true);
                                    //pt.FromDate = pt.ToDate = infoList[i].Date;
                                    pt.FromDate = pt.ToDate = vmn.Date;
                                }
                                filed = true; itemUsed += 2;
                                #endregion
                            }
                            else
                            {
                                #region not pair
                                ProceedTrafficPair pair = new ProceedTrafficPair();
                                pair.BasicTrafficIdFrom = infoList[i].BasicId;
                                pair.PermitIdFrom = infoList[i].PermitId;
                                pair.BasicTrafficIdFromDate = infoList[i].Date;
                                pair.From = infoList[i].Time;
                                pair.To = NotPaired;

                                pair.IsFilled = false;
                                pair.Precard = new Precard() { ID = infoList[i].PrecardID };

                                pair.ProceedTraffic = pt;
                                pt.Pairs.Add(pair);
                                pt.FromDate = pt.ToDate = infoList[i].Date;
                                filed = false; itemUsed++;
                                #endregion
                            }
                        }

                        pt.HasHourlyItem = true;
                        pt.Person = _person;

                        this.T12(_person, ref pt);

                        Person.ProceedTrafficList.Add(pt);
                        pt = new ProceedTraffic();
                        pt.Pairs = new List<ProceedTrafficPair>();

                        #region پردازش آیتم های باقی مانده از لیست
                        if (itemUsed < infoList.Count - 1)
                        {
                            ProceedTrafficPair pair = new ProceedTrafficPair();
                            pair.BasicTrafficIdFrom = infoList[infoList.Count - 2].BasicId;
                            pair.PermitIdFrom = infoList[infoList.Count - 2].PermitId;
                            pair.BasicTrafficIdFromDate = infoList[infoList.Count - 2].Date;
                            pair.From = infoList[infoList.Count - 2].Time;

                            pair.BasicTrafficIdTo = infoList[infoList.Count - 1].BasicId;
                            pair.PermitIdTo = infoList[infoList.Count - 1].PermitId;
                            pair.BasicTrafficIdToDate = infoList[infoList.Count - 1].Date;
                            pair.To = infoList[infoList.Count - 1].Time;
                            if (infoList[infoList.Count - 2].Date.AddDays(1) == infoList[infoList.Count - 1].Date)
                            {
                                pair.To += DayMinutes;
                            }

                            pair.IsFilled = true;
                            pair.Precard = new Precard() { ID = infoList[infoList.Count - 2].PrecardID };

                            pair.ProceedTraffic = pt;
                            pt.Pairs.Add(pair);
                            pt.FromDate = pt.ToDate = infoList[infoList.Count - 2].Date;
                            pt.HasHourlyItem = true;

                            pt.Person = _person;

                            this.T12(_person, ref pt);

                            Person.ProceedTrafficList.Add(pt);
                        }
                        else if (itemUsed < infoList.Count)
                        {
                            ProceedTrafficPair pair = new ProceedTrafficPair();
                            pair.BasicTrafficIdFrom = infoList[infoList.Count - 1].BasicId;
                            pair.PermitIdFrom = infoList[infoList.Count - 1].PermitId;
                            pair.BasicTrafficIdFromDate = infoList[infoList.Count - 1].Date;
                            pair.From = infoList[infoList.Count - 1].Time;
                            pair.To = NotPaired;
                            pair.Precard = new Precard() { ID = infoList[infoList.Count - 1].PrecardID };


                            pair.ProceedTraffic = pt;
                            pt.Pairs.Add(pair);
                            pt.FromDate = pt.ToDate = infoList[infoList.Count - 1].Date;
                            pt.HasHourlyItem = true;

                            pt.Person = _person;

                            this.T12(_person, ref pt);

                            Person.ProceedTrafficList.Add(pt);
                        }
                        #endregion
                    }
                }
            }

            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T36", ex);
            }
        }

        /// <summary>
        /// اعمال مجوزهای تردد بر روی کلیه ترددهای پردازش شده
        /// این قانون در انتها اجرا میگردد
        /// </summary>
        /// <param name="_person"></param>
        /// <param name="_proceedTraffic"></param>
        private void T22(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                if (OneEnterOneExit == 0)
                {
                    DateTime date = DateTime.Now;
                    if (!_person.BasicTrafficController.Finished)
                        date = _person.BasicTrafficController.CurrentBasicItem.Date;

                    if (_person.PermitList != null && _person.PermitList.Count > 0)
                    {
                        IList<Permit> permitList = _person.PermitList.Where(x => x.Pairs.Where(y => !y.IsApplyedOnTraffic && y.PreCardID == GetPrecardId(Precards.Usual)).Count() > 0)
                            .OrderBy(x => x.FromDate).ToList();

                        foreach (Permit permit in permitList)
                        {
                            List<PairInfo> infoList = new List<PairInfo>();
                            ProceedTraffic pt = new ProceedTraffic();

                            #region Has Proceed Traffic
                            if (_person.ProceedTrafficList.Where(x => x.FromDate == permit.FromDate).Count() > 0)
                            {
                                pt = _person.ProceedTrafficList.Where(x => x.FromDate == permit.FromDate).First();
                                IList<int> tileList = new List<int>();
                                var ptFroms = from o in pt.Pairs
                                              where o.From != NotPaired
                                              select new PairInfo { Time = o.From, BasicId = o.BasicTrafficIdFrom, Date = o.BasicTrafficIdFromDate, PrecardID = o.PishCardID };
                                var ptTos = from o in pt.Pairs
                                            where o.To != NotPaired
                                            select new PairInfo { Time = o.To, BasicId = o.BasicTrafficIdTo, Date = o.BasicTrafficIdToDate, PrecardID = o.PishCardID };

                                var permitFroms = from o in permit.Pairs
                                                  where o.From != NotPaired && ptFroms.Where(x => x.Date == o.Permit.FromDate && x.PrecardID == o.PreCardID && x.Time == o.From).Count() == 0
                                                  select new PairInfo { Time = o.From, PermitId = o.ID, Date = o.Permit.FromDate, PrecardID = o.PreCardID };

                                var permitTos = from o in permit.Pairs
                                                where o.To != NotPaired && ptTos.Where(x => x.Date == o.Permit.FromDate && x.PrecardID == o.PreCardID && x.Time == o.To).Count() == 0
                                                select new PairInfo { Time = o.To, PermitId = o.ID, Date = o.Permit.FromDate, PrecardID = o.PreCardID };


                                infoList.AddRange(ptFroms);
                                infoList.AddRange(ptTos);
                                infoList.AddRange(permitFroms);
                                infoList.AddRange(permitTos);

                            }
                            #endregion

                            #region Has Not Proceed Traffic
                            else
                            {
                                pt = new ProceedTraffic();
                                pt.Person = _person;
                                pt.FromDate = permit.FromDate;
                                pt.ToDate = permit.ToDate;
                                pt.HasHourlyItem = true;
                                _person.ProceedTrafficList.Add(pt);

                                IList<int> tileList = new List<int>();

                                var permitFroms = from o in permit.Pairs
                                                  where o.From != NotPaired
                                                  select new PairInfo { Time = o.From, PermitId = o.ID, Date = o.Permit.FromDate, PrecardID = o.PreCardID };

                                var permitTos = from o in permit.Pairs
                                                where o.To != NotPaired
                                                select new PairInfo { Time = o.To, PermitId = o.ID, Date = o.Permit.FromDate, PrecardID = o.PreCardID };


                                infoList.AddRange(permitFroms);
                                infoList.AddRange(permitTos);

                            }
                            #endregion

                            foreach (PairInfo pairInfo in infoList)
                            {
                                if (permit.FromDate < pairInfo.Date && pairInfo.Time > 0 && pairInfo.Time < DayMinutes)
                                {
                                    pairInfo.Date = permit.FromDate.Date;
                                    pairInfo.Time += DayMinutes;
                                }
                            }


                            infoList = infoList.Distinct().ToList();
                            infoList = infoList.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();

                            infoList = infoList.Where(x => acceptablePrecard.Contains(x.PrecardID)).ToList();
                            #region حذف آیتم تکراری
                            var unique = from o in infoList
                                         group o by new { /*o.PrecardID,*/ o.Time };

                            infoList = new List<PairInfo>();
                            foreach (var group in unique)
                            {
                                foreach (var info in group)
                                {
                                    infoList.Add(info);
                                    break;
                                }
                            }
                            #endregion


                            pt.Pairs = new List<ProceedTrafficPair>();
                            pt.HasHourlyItem = true;
                            #region Create ProceedTrafficPair
                            for (int i = 0; i < infoList.Count - 1; i = i + 2)
                            {
                                ProceedTrafficPair pair = new ProceedTrafficPair();
                                pair.BasicTrafficIdFrom = infoList[i].BasicId;
                                pair.PermitIdFrom = infoList[i].PermitId;
                                pair.BasicTrafficIdFromDate = infoList[i].Date;
                                pair.From = infoList[i].Time;

                                pair.BasicTrafficIdTo = infoList[i + 1].BasicId;
                                pair.PermitIdTo = infoList[i + 1].PermitId;
                                pair.BasicTrafficIdToDate = infoList[i + 1].Date;
                                pair.To = infoList[i + 1].Time;

                                pair.IsFilled = true;
                                pair.Precard = new Precard() { ID = infoList[i].PrecardID };

                                pair.ProceedTraffic = pt;
                                pt.Pairs.Add(pair);
                            }
                            if (infoList.Count % 2 == 1)
                            {
                                ProceedTrafficPair pair = new ProceedTrafficPair();
                                pair.BasicTrafficIdFrom = infoList.Last().BasicId;
                                pair.PermitIdFrom = infoList.Last().PermitId;
                                pair.BasicTrafficIdFromDate = infoList.Last().Date;
                                pair.From = infoList.Last().Time;
                                pair.To = NotPaired;

                                pair.IsFilled = false;
                                pair.Precard = new Precard() { ID = infoList.Last().PrecardID };
                                pair.ProceedTraffic = pt;
                                pt.Pairs.Add(pair);
                            }
                            #endregion
                            this.T12(_person, ref pt);
                            permit.Pairs.All(x => x.IsApplyedOnTraffic = true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T35", ex);
            }
        }

        /// <summary>
        /// زوجهایی که یکی از جفتهای آنها در روز بعد افتاده باشد را با 1440 جمع میکند
        /// برای زوج هایی که در روز بعد هستند
        /// </summary>        
        private void T23(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                foreach (ProceedTraffic pt in _person.ProceedTrafficList)
                {
                    if (pt.ID == 0 && pt.PairCount > 0)
                    {
                        DateTime ptDate = pt.FromDate;
                        foreach (ProceedTrafficPair pair in pt.Pairs)
                        {
                            if (pair.BasicTrafficIdFromDate > ptDate && pair.From < DayMinutes && pair.From != NotPaired)
                            {
                                pair.From += (pair.BasicTrafficIdFromDate - ptDate).Days * DayMinutes;
                            }
                            if (pair.BasicTrafficIdToDate > ptDate && (pair.To < DayMinutes || pair.To < pair.From) && pair.To != NotPaired)
                            {
                                pair.To += (pair.BasicTrafficIdToDate - ptDate).Days * DayMinutes;
                            }

                            if (pair.BasicTrafficIdFromDate < ptDate && pair.From > 0 && pair.From != NotPaired)
                            {
                                pair.From -= DayMinutes;
                            }
                            if (pair.BasicTrafficIdToDate < ptDate && pair.To >0 && pair.To != NotPaired)
                            {
                                pair.To -= DayMinutes;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TrafficMapperRuleException(_person.PersonCode, "T23", _proceedTraffic.FromDate, ex);
            }
        }

  
      
     

      
        #endregion

        #region  private setvices


        /// <summary>
        ///  نزديک ترين شيفت را به يک تردد برميگرداند
        /// </summary>                         
        /// <param name="enter">اگر ورد باشد مقدار "درست" و اگر خروج مقدار "نادرست" , براي اضافه کار قبل و بعد از وقت استفاده ميشود</param>     
        public VirtualMidNight GetNearShiftDate(BasicTraffic basicTraffic, Person person, bool enter, bool applyMidNightTelorance)
        {
            if (basicTraffic.Precard != null)
            {
                if (basicTraffic.PrecardID == this.GetPrecardId(Precards.Enter))
                    enter = true;
                if (basicTraffic.PrecardID == this.GetPrecardId(Precards.Exit))
                    enter = false;
            }
            VirtualMidNight v = virtualMidNightList.GetMidNight(basicTraffic.Date, basicTraffic.Time, enter, applyMidNightTelorance);
            return v;
        }

        /// <summary>
        ///  نزديک ترين شيفت را به يک تردد برميگرداند
        /// </summary>                         
        /// <param name="enter">اگر ورد باشد مقدار "درست" و اگر خروج مقدار "نادرست" , براي اضافه کار قبل و بعد از وقت استفاده ميشود</param>     
        public VirtualMidNight GetNearShiftDate(BasicTraffic basicTraffic, Person person, bool enter)
        {
            if (basicTraffic.PrecardID == this.GetPrecardId(Precards.Enter))
                enter = true;
            if (basicTraffic.PrecardID == this.GetPrecardId(Precards.Exit))
                enter = false;
            VirtualMidNight v = virtualMidNightList.GetMidNight(basicTraffic.Date, basicTraffic.Time, enter, false);
            return v;
        }

        /// <summary>
        /// آيا يک تردد در يک شيفت ميگنجد
        /// با اين فرض که شيفتها قبلا بر اساس تاريخ و زمان شروع مرتب سازي شده اند
        /// </summary>
        /// <param name="enter">اگر ورد باشد مقدار "درست" و اگر خروج مقدار "نادرست" , براي اضافه کار قبل و بعد از وقت استفاده ميشود</param>     

        /// <returns></returns>
        private bool ContainsTraffic(AssignedWGDShift assgnWGD, BasicTraffic basicTraffic, bool enter)
        {
            int telorance = 60;
            if (assgnWGD.Date.Date == basicTraffic.Date.Date)
            {
                List<ShiftPair> list = assgnWGD.Pairs.OrderBy(x => x.From).ToList();
                //اعمال اضافه کار قبل و بعد از وقت بسته به ورود يا خوج بودن متفاوت است
                if (enter)
                {
                    if (list[0].From - list[0].BeforeTolerance - telorance <= basicTraffic.Time && list[list.Count - 1].To >= basicTraffic.Time)
                    {
                        return true;
                    }
                }
                else
                {
                    if (list[0].From <= basicTraffic.Time && list[list.Count - 1].To + list[list.Count - 1].AfterTolerance + telorance >= basicTraffic.Time)
                    {
                        return true;
                    }
                }
                //تا اينجا يعني تردد دقيقا در بازه شيفت واقع نشده , حال بايد بررسي کنيم که آيا در اضافه کار همان تاريخ شيفت واقع ميشود يا نه
                //اگر شيفت 11 شب تا 10 صبح بود و شخص براي اضافه کار تردد 10:30 تا 11 داشت تاريخ تردد کجا لحاظ شود             
            }
            //بررسي حالتي که طول شيفت از 24 ساعت بيشتر باشد
            else if (assgnWGD.Date < basicTraffic.Date)
            {
                foreach (ShiftPair sp in assgnWGD.Pairs)
                {
                    if (sp.To >= DayMinutes)
                    {
                        TimeSpan dateDiff = basicTraffic.Date.Date - assgnWGD.Date.Date;
                        int days = dateDiff.Days;// -1;//اختلاف روز کامل
                        int daysTime = days * DayMinutes;
                        daysTime += basicTraffic.Time;
                        //اعمال اضافه کار قبل و بعد از وقت بسته به ورود يا خوج بودن متفاوت است
                        if (enter)
                        {
                            if (sp.From - sp.BeforeTolerance <= daysTime && sp.To >= daysTime)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (sp.From <= daysTime && sp.To + sp.AfterTolerance >= daysTime)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// آيا يک تردد در يک شيفت ميگنجد
        /// با اين فرض که شيفتها قبلا بر اساس تاريخ و زمان شروع مرتب سازي شده اند
        /// </summary>
        /// <param name="assgnWGD"></param>
        /// <param name="basicTraffic"></param>
        /// <returns></returns>
        private bool ContainsTraffic(AssignedWGDShift assgnWGD, DateTime _dateTime, int _time)
        {
            int telorance = 60;
            if (assgnWGD.Date.Date == _dateTime.Date)
            {
                List<ShiftPair> list = assgnWGD.Pairs.OrderBy(x => x.From).ToList();
                if (list[0].From - list[0].BeforeTolerance - telorance <= _time && list[list.Count - 1].To + list[list.Count - 1].AfterTolerance + telorance >= _time)
                {
                    return true;
                }
            }
            //بررسي حالتي که طول شيفت از 24 ساعت بيشتر باشد
            else if (assgnWGD.Date < _dateTime)
            {
                foreach (ShiftPair sp in assgnWGD.Pairs)
                {
                    if (sp.To >= DayMinutes)
                    {
                        TimeSpan dateDiff = _dateTime.Date - assgnWGD.Date.Date;
                        int days = dateDiff.Days;// -1;//اختلاف روز کامل
                        int daysTime = days * DayMinutes;
                        daysTime += _time;
                        if (sp.From - sp.BeforeTolerance <= daysTime && sp.To + sp.AfterTolerance >= daysTime)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// اعمال نتایج در تردد پردازش شده
        /// </summary>
        /// <param name="_person"></param>
        /// <param name="_proceedTraffic"></param>
        /// <param name="date">تاریخی که تردد پردازش شده مربوط به آن روز است</param>
        private void ImportBaseTraffic(Person _person, ProceedTraffic _proceedTraffic, DateTime date)
        {
            _proceedTraffic.SaveDate(date);        

            _proceedTraffic.ProceedTrafficPaireController.EditFinish();

        }

        /// <summary>
        /// آيا دو تردد پايه در بازه يک تردد پردازش شده قرار ميگيرد
        /// </summary>
        /// <param name="nextBasicTraffic">تردد پايه</param>
        /// <param name="proceedTraffic">تردد پردازش شده</param>        
        private bool AreInSameProceedTraffic(BasicTraffic currentBasicTraffic, BasicTraffic nextBasicTraffic,bool applyMidnightTelorance)
        {
            if (currentBasicTraffic.PrecardID == GetPrecardId(Precards.Enter) &&
               nextBasicTraffic.PrecardID == GetPrecardId(Precards.Exit))
            {
                return true;
            }
            else if ((currentBasicTraffic.PrecardID == GetPrecardId(Precards.Enter) &&
               nextBasicTraffic.PrecardID == GetPrecardId(Precards.Enter)) 
                ||
                (currentBasicTraffic.PrecardID == GetPrecardId(Precards.Exit) &&
               nextBasicTraffic.PrecardID == GetPrecardId(Precards.Exit))
                 ||
                (currentBasicTraffic.PrecardID == GetPrecardId(Precards.Exit) &&
               nextBasicTraffic.PrecardID == GetPrecardId(Precards.Enter)))
            {
                return false;
            }           

            VirtualMidNight x = GetNearShiftDate(currentBasicTraffic, this.Person, true, applyMidnightTelorance);
                       
            VirtualMidNight y = GetNearShiftDate(nextBasicTraffic, this.Person, false, applyMidnightTelorance);
            if (x == y)
            {
                return true;
            }
            if (x.Date > y.Date) //در این حالت به علت اعمال تلورانس تردد ورود در امروز و خروج در دیروز افتاده
            {
                y = GetNearShiftDate(nextBasicTraffic, this.Person, true, applyMidnightTelorance);
                if (x == y)
                {
                    return true;
                }
            }
            if (EndOfDayIsForce == false)
            {
                if (x.Date == y.Date &&
                    ((x.Time <= this.VirtualMidNight && y.Time <= this.VirtualMidNight)
                            ||
                     (x.Time >= this.VirtualMidNight && y.Time >= this.VirtualMidNight))
                    )
                {
                    return true;
                }
            }
            //if(applyMidnightTelorance)
            //{             
            //    y = GetNearShiftDate(nextBasicTraffic, this.Person, false, false);
            //    if (x == y)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
      
        /// <summary>
        /// آيا يک تردد در يک شيفت واقع است
        /// </summary>        
        private bool IsInShift(BasicTraffic basicTraff, AssignedWGDShift assgnWGD)
        {
            if (assgnWGD.Date == basicTraff.Date)
            {
                List<ShiftPair> list = assgnWGD.Pairs.OrderBy(x => x.From).ToList();
                if (list.First().From - (list.First().BeforeTolerance + Telorance) <= basicTraff.Time && list.Last().To + (list.Last().AfterTolerance + Telorance) >= basicTraff.Time)
                {
                    return true;
                }
            }
            //انتهاي شيفت بزرگتر از 1440 بود
            else if (assgnWGD.Date < basicTraff.Date)
            {
                foreach (ShiftPair sp in assgnWGD.Pairs)
                {
                    if (sp.To >= DayMinutes)
                    {
                        TimeSpan dateDiff = basicTraff.Date - assgnWGD.Date;
                        int days = dateDiff.Days;// -1;//اختلاف روز کامل
                        int daysTime = days * DayMinutes;
                        daysTime += basicTraff.Time;
                        if (sp.From - sp.BeforeTolerance <= daysTime && sp.To + sp.AfterTolerance >= daysTime)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// يک تردد پردازش شده راذخيره ميکند
        /// </summary>      
        /// <param name="nearShiftDate">نزديکترين تاريخ شيفت بازيابي ميشود</param>        
        private void SaveProceedTraffic(Person _person, ProceedTraffic _proceedTraffic)
        {
            if (!_person.ProceedTrafficList.Contains(_proceedTraffic))
            {
                _proceedTraffic.SetFromToDate();
                foreach (ProceedTrafficPair ptp in _proceedTraffic.Pairs)
                {
                    if (!ptp.IsFilled)
                    {
                        if (ptp.From == NotPaired) ptp.BasicTrafficIdFrom = 0;
                        if (ptp.To == NotPaired) ptp.BasicTrafficIdTo = 0;
                    }
                    //این مورد در قانون 23 لحاظ میگردد
                    //if (ptp.To < ptp.From && ptp.To != NotPaired)
                    //{
                    //    throw new BaseException(String.Format("مقدار انتها بزرگتر از مقدار ابتدا در تردد روز {0} بیشتر است", _proceedTraffic.FromDate.ToShortDateString()), "ProceedTraffic.SaveProceedTraffic");
                    //}
                }
                _proceedTraffic.Person = Person;
                _person.ProceedTrafficList.Add(_proceedTraffic);
            }
        }

        /// <summary>
        /// قوانيني که بطور مستقيم با تردد پردازش نشده سروکار دارن را اجرا ميکند
        /// </summary>
        private void RuleOnBasicTrafficRunner(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                T6(_person, ref _proceedTraffic);  
                T2(_person, ref _proceedTraffic);
                T3(_person, ref _proceedTraffic);
                T4(_person, ref _proceedTraffic);
                T5(_person, ref _proceedTraffic);
            }
            catch (Exception ex)
            {
                logger2.Info(_person.BarCode, String.Format("TrafficMapper Error On RuleOnBasicTrafficRunner:{0}->{1}", _person.BasicTrafficController.CurrentBasicItem.Date.ToString(), ex.Message));
                logger2.Flush();
            }
        }

        /// <summary>
        /// قوانيني که براي اجرا به تردد پردازش شده بهمراه زوج مرتب هاي آن احتياج دارند را اجرا ميکند
        /// </summary>
        private void RuleOnProceedTrafficRunner(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            try
            {
                T10(_person, ref _proceedTraffic);
                T11(_person, ref _proceedTraffic);
                //T12(_person, ref _proceedTraffic);
                T13(_person, ref _proceedTraffic);
            }
            catch (Exception ex)
            {

                logger2.Info(_person.BarCode, String.Format("TrafficMapper Error On RuleOnProceedTrafficRunner:{0}->{1}", _person.BasicTrafficController.CurrentBasicItem.Date.ToString(), ex.Message));
                logger2.Flush();
            }
        }

        /// <summary>
        /// قوانيني که براي اجرا نياز به ليست حاصل ترددهاي پردازش شده دارد را اجرا ميکند 
        /// </summary>
        private void RuleOnProceedTrafficList(Person _person, ref ProceedTraffic _proceedTraffic)
        {
            T20(_person, ref _proceedTraffic);
            T21(_person, ref _proceedTraffic);
            T22(_person, ref _proceedTraffic);
            T23(_person, ref _proceedTraffic);
        }

        /// <summary>
        /// يک تردد پردازش نشده را از ورئدي ميگيرد و اگر اين تردد در سيفتي واقع شده باشد آن شيفت را برميگرداند
        /// </summary>
        /// <param name="_person"></param>
        /// <param name="_basicTraffic"></param>
        /// <returns></returns>
        private AssignedWGDShift GetShift(Person _person, BasicTraffic _basicTraffic, bool enter)
        {

            foreach (DateTime CalcDate in _person.CalcDateZone)
            {
                AssignedWGDShift assgnWGD = (AssignedWGDShift)_person.GetShiftByDate(CalcDate);
                if (assgnWGD.Pairs.Count == 0)
                    continue;
                if (ContainsTraffic(assgnWGD, _basicTraffic, enter))
                {
                    return assgnWGD;
                }
            }

            return new AssignedWGDShift();
        }

        /// <summary>
        /// يک تردد پردازش نشده را از ورئدي ميگيرد و اگر اين تردد در سيفتي واقع شده باشد آن شيفت را برميگرداند
        /// </summary>
        /// <param name="_person"></param>
        /// <param name="_basicTraffic"></param>
        /// <returns></returns>
        private AssignedWGDShift GetShift(Person _person, DateTime _date, int _time)
        {

            foreach (DateTime CalcDate in _person.CalcDateZone)
            {
                AssignedWGDShift assgnWGD = (AssignedWGDShift)_person.GetShiftByDate(CalcDate);
                if (assgnWGD.Pairs.Count == 0)
                    continue;
                if (ContainsTraffic(assgnWGD, _date, _time))
                {
                    return assgnWGD;
                }
            }

            return new AssignedWGDShift();
        }

        private AssignedWGDShift GetShift(Person _person, DateTime _date)
        {

            foreach (DateTime CalcDate in _person.CalcDateZone)
            {
                AssignedWGDShift assgnWGD = (AssignedWGDShift)_person.GetShiftByDate(CalcDate);
                if (assgnWGD.Pairs.Count == 0)
                    continue;
                if (assgnWGD.Date == _date.Date)
                {
                    return assgnWGD;
                }
            }
            return new AssignedWGDShift();
        }

        /// <summary>
        /// پیشکارتها را استخراج و باتوجه به نام آنها در دیکشنری قرار میدهد
        /// </summary>
        private void InitPishcads()
        {
            BPrecard bprecard = new BPrecard();
            IList<Precard> list = bprecard.GetAllWithoutAthoriZe();
            foreach (string pishcardName in Enum.GetNames(typeof(Precards)))
            {
                Precards pishcard = (Precards)Enum.Parse(typeof(Precards), pishcardName);
                if ((int)pishcard >= 0)
                {
                    Precard p = list.Where(x => x.Code == ((int)pishcard).ToString()).FirstOrDefault();
                    if (p != null)
                    {
                        precardDic.Add(pishcardName, p);
                    }
                    //else
                    //{
                    //    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ExpectedPrecardDoesNotExists, String.Format("پیشکارت مورد نظر در پردازش ترددها در دیتابیس یافت نشد,کد پیشکارت مورد نظر {0} میباشد", ((int)pishcard).ToString()), "GTS.Clock.Business.TrafficMapping");
                    //}
                }
            }
        }

        /// <summary>
        ///  پیشکارت را از دیکشنری استخراج میکند
        /// </summary>
        /// <param name="precard"></param>
        /// <returns></returns>
        private Precard GetPrecard(Precards precards)
        {
            Precard precard = precardDic[Utility.ToString(precards)];
            return precard;
        }

        /// <summary>
        /// شناسه پیشکارت را از دیکشنری استخراج میکند
        /// </summary>
        /// <param name="precard"></param>
        /// <returns></returns>
        private decimal GetPrecardId(Precards precards)
        {
            Precard precard = precardDic[Utility.ToString(precards)];
            return precard.ID;
        }

        /// <summary>
        /// شناسه دیتابیسی یک پیشکارت را میگیرد و نام آنرا پس میدهد
        /// </summary>
        /// <param name="precardId"></param>
        /// <returns></returns>
        private Precards GetPrecardKey(decimal precardId)
        {
            KeyValuePair<string, Precard> item = precardDic.Where(x => x.Value.ID == precardId).FirstOrDefault();
            string key = item.Key;
            if (key == null)
                return Precards.UnKnown;
            Precards p = (Precards)Enum.Parse(typeof(Precards), key);
            return p;
        }

        /// <summary>
        /// به قوانین یک شخص در تاریخ مشخص شده نگاه میکند
        /// اگر قانون تردد ها یکدر میان فعال باشد ، درست بر میگرداند
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private int GetOneEnterOneExit(DateTime currentDate)
        {
            //return false;
            if (this.Person.AssignedRuleList != null)
            {
                AssignedRule ar = this.Person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == 1002).FirstOrDefault();
                if (ar != null)
                {
                    IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= currentDate && x.ToDate >= currentDate).ToList();
                    if (asp != null)
                    {
                        AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

                        if (firstParam != null)
                            return Utility.ToInteger(firstParam.Value);
                    }
                }
            }
            return 0;

        }

        /// <summary>
        /// به قوانین یک شخص در تاریخ مشخص شده نگاه میکند
        /// اگر قانون درج تردد مجازی فعال باشد ، درست بر میگرداند 
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private bool GetInsertVirtualTraffic(DateTime currentDate)
        {
            if (this.Person.AssignedRuleList != null)
            {
                AssignedRule ar = this.Person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == 1019).FirstOrDefault();
                if (ar != null)
                {
                    IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= currentDate && x.ToDate >= currentDate).ToList();
                    if (asp != null)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        /// <summary>
        /// به قوانین یک شخص در تاریخ مشخص شده نگاه میکند
        /// اگر قانون تردد ها ترددها اتوماتیک وارد شود ، درست بر میگرداند
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private bool GetAutomaticTraffic(DateTime currentDate)
        {
            //return false;
            int ruleUsed = this.Person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == 1021).Count();
            return ruleUsed > 0;

        }

        /// <summary>
        /// به قوانین یک شخص در تاریخ مشخص شده نگاه میکند
        /// اگر قانون پایان شبانه روز فعال باشد ، مقدار را بر میگرداند
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private int EndOfDay(DateTime currentDate)
        {
            //return 1439;
            ////////////////////

            BaseShift shift = this.Person.GetShiftByDate(currentDate, "EndOfDay");
            currentDate = currentDate.Date;
            int midNight = DayMinutes - 1;
            if (shift != null && shift.PairCount > 0)
            {
                midNight = shift.Pairs.First().From;
            }
            else
            {
                if (this.Person.AssignedRuleList != null)
                {
                    AssignedRule ar = this.Person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == 1015).FirstOrDefault();
                    if (ar != null)
                    {
                        IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= currentDate && x.ToDate >= currentDate).ToList();
                        //IList<AssignedRuleParameter> asp = ar.RuleParameterList.Where(x => x.FromDate <= date && x.ToDate >= date).ToList();                       
                        if (asp != null)
                        {
                            AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

                            if (firstParam != null)
                                return Utility.ToInteger(firstParam.Value);
                        }
                    }
                }
            }
            return DayMinutes - 1;
        }

        /// <summary>
        ///پایان شبانه روز قطعی است
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private bool GetEndOfDayIsForce(DateTime currentDate)
        {
            //return false;
            if (this.Person.AssignedRuleList != null)
            {
                AssignedRule ar = this.Person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == 1015).FirstOrDefault();
                if (ar != null)
                {
                    EntityRepository<AssignRuleParameter> paramRep = new EntityRepository<AssignRuleParameter>();
                    IList<AssignRuleParameter> paramList = paramRep.Find(x => x.Rule.ID == ar.RuleId).ToList();

                    AssignRuleParameter asp = paramList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate).FirstOrDefault();
                    if (asp != null)
                    {
                        RuleParameter secondParam = asp.RuleParameterList.Where(x => x.Name.ToLower().Equals("second")).FirstOrDefault();

                        //AssignRuleParameter secondParam = asp.Where(x => x.Name.ToLower().Equals("second")).FirstOrDefault();

                        if (secondParam != null)
                            return Utility.ToBoolean(secondParam.Value);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// در صورت نیاز تردد پردازش شده آخر را حذف میکند
        /// </summary>
        /// <param name="lastPt"></param>
        /// <param name="basicController"></param>
        /// <param name="trafficSync"></param>
        private void DeleteLastTraffic(ProceedTraffic lastPt, BasicTrafficController basicController, TrafficSync trafficSync) 
        {
            if (lastPt != null)
            {
                ProceedTrafficPair pair1 = lastPt.Pairs.LastOrDefault();
                if (pair1 != null)
                {//آیا تردد ثبت شده در تردد پردازش شده با تردد جاری در یک روز قرار میگیرد
                    BasicTraffic lastPairTraffic = null;
                    if (pair1.BasicTrafficIdFrom > 0)
                    {
                        lastPairTraffic = this.Person.BasicTrafficList.Where(x => x.ID == pair1.BasicTrafficIdFrom).FirstOrDefault();
                    }
                    else if (pair1.PermitIdFrom > 0)
                    {
                        //not implemented yet
                    }
                    if (lastPairTraffic != null)
                    {
                        VirtualMidNight tmpVmn1 = GetNearShiftDate(lastPairTraffic, Person, true, true);

                        if (tmpVmn1 == mainVmn
                            ||
                            (lastPairTraffic.PrecardID == this.GetPrecardId(Precards.Enter) &&
                             basicController.CurrentBasicItem.PrecardID == this.GetPrecardId(Precards.Exit))
                            )
                        {
                            foreach (ProceedTrafficPair pair in lastPt.Pairs)
                            {
                                if (this.Person.PermitList != null)
                                {
                                    Permit fromPermit = this.Person.PermitList.Where(x => x.Pairs.Any(yield => yield.ID == pair.PermitIdFrom) && x.FromDate == pair.BasicTrafficIdFromDate).FirstOrDefault();
                                    Permit toPermit = this.Person.PermitList.Where(x => x.Pairs.Any(yield => yield.ID == pair.PermitIdTo) && x.FromDate == pair.BasicTrafficIdToDate).FirstOrDefault();

                                    if (fromPermit != null)
                                    {
                                        PermitPair permitPair = fromPermit.Pairs.Where(x => x.ID == pair.PermitIdFrom).First();
                                        permitPair.IsApplyedOnTraffic = false;
                                    }
                                    if (toPermit != null)
                                    {
                                        PermitPair permitPair = toPermit.Pairs.Where(x => x.ID == pair.PermitIdTo).First();
                                        permitPair.IsApplyedOnTraffic = false;
                                    }
                                }
                                if (this.Person.BasicTrafficList != null)
                                {
                                    BasicTraffic frombase = this.Person.BasicTrafficList.Where(x => x.ID == pair.BasicTrafficIdFrom).FirstOrDefault();
                                    BasicTraffic tobase = this.Person.BasicTrafficList.Where(x => x.ID == pair.BasicTrafficIdTo).FirstOrDefault();
                                    if (frombase != null)
                                    {
                                        frombase.Used = false;
                                    }
                                    if (tobase != null)
                                    {
                                        tobase.Used = false;
                                    }
                                }
                            }
                            this.Person.ProceedTrafficList.Remove(lastPt);

                            EntityRepository<ProceedTraffic> traficRep = new EntityRepository<ProceedTraffic>(false);

                            traficRep.Delete(lastPt);
                            //lastPt.Person = null;

                            trafficSync.Synchronize();
                            basicController.Reset();
                            if (!basicController.Finished)
                            {
                                mainVmn = GetNearShiftDate(basicController.CurrentBasicItem, Person, true, true);
                            }
                        }                       
                    }
                }
            }
        }
       

        #endregion

        /// <summary>
        /// جهت استفاده در قانون اعمال مجوزها
        /// </summary>
        private class PairInfo : IComparable
        {
            //public decimal ID { get; set; }

            public decimal BasicId { get; set; }

            public decimal PermitId { get; set; }

            public int Time { get; set; }

            public DateTime Date { get; set; }

            public decimal PrecardID { get; set; }


            #region IComparable Members

            /// <summary>
            /// جهت استفاده در حذف تکراری ها
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                PairInfo pair = (PairInfo)obj;
                if (this.Date.Date == pair.Date.Date.Date &&
                    this.PrecardID == pair.PrecardID &&
                    this.Time == pair.Time)
                    return 0;
                //else if (
                //    (this.Date.Date > pair.Date.Date.Date)
                //    ||
                //    (this.Date.Date == pair.Date.Date.Date && this.Time > pair.Time))
                //{
                //    return 1;
                //}
                return 1;
            }

            #endregion

            public static void Distinct(IList<PairInfo> list)
            {
                IList<PairInfo> result = new List<PairInfo>();
                foreach (PairInfo pair in list)
                {
                    //if(pair
                }
            }

            public override string ToString()
            {
                return String.Format("PID:{0} , Date:{1} , Time:{2}", PrecardID, Date, Time);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.virtualMidNightList != null)
            {
                this.virtualMidNightList.Dispose();
            }
        }

        #endregion
    }
}