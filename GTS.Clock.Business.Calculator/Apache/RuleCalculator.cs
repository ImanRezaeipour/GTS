using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.ModelEngine;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.ModelEngine.Concepts;
using GTS.Clock.ModelEngine.Concepts.Operations;
using GTS.Clock.ModelEngine.AppSetting;
using GTS.Clock.ModelEngine.ELE;

namespace GTS.Clock.Business.Calculator
{
    public class RuleCalculator : GeneralRuleCalculator
    {

        #region Constructors
        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر اشياء
        /// </summary>
        /// <param name="Person">پرسنلي که محاسبات براي او در حال انجام است</param>
        /// <param name="CategorisedRule">قانوني که منجر به فراخواني مفاهيم از کلاس "محاسبه گر قانون" خواهد شد</param>
        /// <param name="CalculateDate">تاريخ انجام محاسبات</param>
        public RuleCalculator(IEngineEnvironment engineEnvironment)
            : base(engineEnvironment, new ConceptCalculator(engineEnvironment))
        {
            this.logLock = !GTSAppSettings.RuleDebug;
        }
        #endregion

        #region Defined Methods

        #region قوانين متفرقه


        #endregion

        #region قوانين کارکرد

        /// <summary>
        /// •	در صورتی به شخص کارکرد روزانه داده میشود که شخص حداقل به میزان 7:30 حضور داشته باشد.
        //•	 در صورت حضور کمتر از 7:30 , میزان حضور اضافه کار لحاظ میگردد.
        //•	در صورت حضور بیش از 7:30 , علاوه بر اختصاص کارکرد روزانه , اضافه کار نیز به شخص تعلق میگیرد.
        //•	در صورت عدم حضور شخص در یک روز , به شخص غیبت روزانه تعلق میگیرد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2501_old(AssignedRule MyRule)
        {

            GetLog(MyRule, DebugRuleState.Before, 4, 4002, 3028, 3004, 13);
            if (this.DoConcept(2005).Value == 0 && this.DoConcept(1090).Value == 0)
            {
                this.DoConcept(4).Value = 0;
            }
            PairableScndCnpValue tmpCnp = new PairableScndCnpValue();
            PairableScndCnpValue orginTmpCnp = new PairableScndCnpValue();
            tmpCnp.AddPairs(this.DoConcept(1));
            tmpCnp.AppendPairs(this.DoConcept(1003));
            tmpCnp.AppendPairs(this.DoConcept(2023));

            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();

            int minHozour = MyRule["first", this.RuleCalculateDate].ToInt();
            int esterhat = MyRule["second", this.RuleCalculateDate].ToInt();
            int in2Shift = MyRule["third", this.RuleCalculateDate].ToInt();
            this.DoConcept(3012).Value = minHozour;//بعلت عدم تعریم مفهوم  از این استفاده میشود
            if (tmpCnp.Value > 0)
            {
                orginTmpCnp.AddPairs(tmpCnp);
                int start1 = MyRule["4th", this.RuleCalculateDate].ToInt();//شروع محدوده غیر مجاز اضافه کار قبل وقت صبح
                int start2 = MyRule["5th", this.RuleCalculateDate].ToInt();//شروع محدوده غیر مجاز اضافه کار بعد وقت صبح
                int end2 = MyRule["6th", this.RuleCalculateDate].ToInt();//پایان محدوده غیر مجاز اضافه کار بعد وقت صبح
                int start3 = MyRule["7th", this.RuleCalculateDate].ToInt();//شروع محدوده غیر مجاز اضافه کار قبل وقت عصر
                int end3 = MyRule["8th", this.RuleCalculateDate].ToInt();//پایان محدوده غیر مجاز اضافه کار قبل وقت عصر
                int end4 = MyRule["9th", this.RuleCalculateDate].ToInt();//پایان محدوده غیر مجاز اضافه کار بعد وقت عصر
                int start = tmpCnp.Pairs.First().From;
                int end = tmpCnp.Pairs.Last().To;
                PairableScndCnpValuePair notAllow = new PairableScndCnpValuePair();
                PairableScndCnpValue notAllowOverwork = new PairableScndCnpValue();

                if (start1 > 0 && end4 > 0)
                {
                    notAllow = new PairableScndCnpValuePair(start1, end4);
                    notAllowOverwork.AddPairs(Operation.Differance(tmpCnp, notAllow));
                    tmpCnp.AddPairs(Operation.Intersect(tmpCnp, notAllow));
                }
                if (end>0 && start2>0 && end2>0 && end > start2 && end < end2)
                {
                    notAllow = new PairableScndCnpValuePair(start2, end2);
                    notAllowOverwork.AppendPairs(Operation.Intersect(tmpCnp, notAllow));
                    tmpCnp.AddPairs(Operation.Differance(tmpCnp, notAllow));
                }

                if (start>0 && start3>0 && end3>0 && start > start3 && start < end3)
                {
                    notAllow = new PairableScndCnpValuePair(start3, end3);
                    notAllowOverwork.AppendPairs(Operation.Intersect(tmpCnp, notAllow));
                    tmpCnp.AddPairs(Operation.Differance(tmpCnp, notAllow));
                }
              
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(notAllowOverwork);
            }
            if (tmpCnp.Value > minHozour)
            {
                if (tmpCnp.Value >= in2Shift && in2Shift > 0)
                {
                    tmpCnp.DecreasePairFromFirst(esterhat * 2);
                }
                else
                {
                    tmpCnp.DecreasePairFromFirst(esterhat);
                }
            }
            if (tmpCnp.Value > 0)
            {
                this.DoConcept(13).Value = tmpCnp.Value;
           
                if (tmpCnp.Value >= this.DoConcept(7).Value)
                {
                    this.DoConcept(4).Value = 1;
                    tmpCnp.DecreasePairFromFirst(this.DoConcept(7).Value);
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(tmpCnp);
                }
                else
                {
                    //اگر حداقل حضور که معمولا 3:30 است را داشته باشد , این مفهوم مقدار دهس میشود
                    if (tmpCnp.Value >= minHozour)
                    {
                        this.DoConcept(503).Value = 1;
                        ((PairableScndCnpValue)this.DoConcept(3028)).IncreaseValue(this.DoConcept(7).Value);// - tmpCnp.Value);
                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(tmpCnp);
                    }
                    else 
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(tmpCnp);
                     //   this.DoConcept(13).Value = this.DoConcept(4002).Value;
                    }
                }
            }
            else if (this.DoConcept(2005).Value == 0 && this.DoConcept(1090).Value == 0 && this.DoConcept(1091).Value == 0)
            {
                this.DoConcept(3004).Value = 1;
            }
            this.DoConcept(5022).FromPairs = "";
            GetLog(MyRule, DebugRuleState.After, 4, 4002, 3028, 3004, 13);
        }

        /// <summary>
        /// •	در صورتی به شخص کارکرد روزانه داده میشود که شخص حداقل به میزان 7:30 حضور داشته باشد.
        //•	 در صورت حضور کمتر از 7:30 , میزان حضور اضافه کار لحاظ میگردد.
        //•	در صورت حضور بیش از 7:30 , علاوه بر اختصاص کارکرد روزانه , اضافه کار نیز به شخص تعلق میگیرد.
        //•	در صورت عدم حضور شخص در یک روز , به شخص غیبت روزانه تعلق میگیرد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2501(AssignedRule MyRule)
        {

            GetLog(MyRule, DebugRuleState.Before, 4, 4002, 3028, 3004, 13);
            this.DoConcept(501).Value = 1;//روز محاسبه شده را نگه میدارد.بوسیله این متوجه میشویم که حداکثر تعداد روز محاسباتی در ماه چقدر است-همیشه نمیتوان به تعداد رینج ماهانه اکتفا کرد
            if (this.DoConcept(2005).Value == 0 && this.DoConcept(1090).Value == 0)
            {
                this.DoConcept(4).Value = 0;
            }
            PairableScndCnpValue tmpCnp = new PairableScndCnpValue();
            PairableScndCnpValue orginTmpCnp = new PairableScndCnpValue();
            tmpCnp.AddPairs(this.DoConcept(1));
            tmpCnp.AppendPairs(this.DoConcept(1003));
            tmpCnp.AppendPairs(this.DoConcept(2023));

            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();

            int minHozour = MyRule["first", this.RuleCalculateDate].ToInt();
            int esterhat = MyRule["second", this.RuleCalculateDate].ToInt();
            int in2Shift = MyRule["third", this.RuleCalculateDate].ToInt();
            this.DoConcept(3012).Value = minHozour;//بعلت عدم تعریم مفهوم  از این استفاده میشود
            if (tmpCnp.Value > 0)//کم کردن کارکرد خارج از محدوده
            {
                orginTmpCnp.AddPairs(tmpCnp);
                int start1 = MyRule["4th", this.RuleCalculateDate].ToInt();//شروع محدوده غیر مجاز اضافه کار قبل وقت صبح
                int start2 = MyRule["5th", this.RuleCalculateDate].ToInt();//شروع محدوده غیر مجاز اضافه کار بعد وقت صبح
                int end2 = MyRule["6th", this.RuleCalculateDate].ToInt();//پایان محدوده غیر مجاز اضافه کار بعد وقت صبح
                int start3 = MyRule["7th", this.RuleCalculateDate].ToInt();//شروع محدوده غیر مجاز اضافه کار قبل وقت عصر
                int end3 = MyRule["8th", this.RuleCalculateDate].ToInt();//پایان محدوده غیر مجاز اضافه کار قبل وقت عصر
                int end4 = MyRule["9th", this.RuleCalculateDate].ToInt();//پایان محدوده غیر مجاز اضافه کار بعد وقت عصر
                int start = tmpCnp.Pairs.First().From;
                int end = tmpCnp.Pairs.Last().To;
                PairableScndCnpValuePair notAllow = new PairableScndCnpValuePair();
                PairableScndCnpValue notAllowOverwork = new PairableScndCnpValue();

                if (start1 > 0 && end4 > 0)
                {
                    notAllow = new PairableScndCnpValuePair(start1, end4);
                    notAllowOverwork.AddPairs(Operation.Differance(tmpCnp, notAllow));
                    tmpCnp.AddPairs(Operation.Intersect(tmpCnp, notAllow));
                }
                if (end > 0 && start2 > 0 && end2 > 0 && end > start2 && end < end2)
                {
                    notAllow = new PairableScndCnpValuePair(start2, end2);
                    notAllowOverwork.AppendPairs(Operation.Intersect(tmpCnp, notAllow));
                    tmpCnp.AddPairs(Operation.Differance(tmpCnp, notAllow));
                }

                if (start > 0 && start3 > 0 && end3 > 0 && start > start3 && start < end3)
                {
                    notAllow = new PairableScndCnpValuePair(start3, end3);
                    notAllowOverwork.AppendPairs(Operation.Intersect(tmpCnp, notAllow));
                    tmpCnp.AddPairs(Operation.Differance(tmpCnp, notAllow));
                }

                //((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(notAllowOverwork);
            }
            if (tmpCnp.Value > 0)//کم کردن مدت استراحت
            {
                if (tmpCnp.Value >= in2Shift && in2Shift > 0)
                {
                    tmpCnp.DecreasePairFromFirst(esterhat * 2);
                }
                else
                {
                    tmpCnp.DecreasePairFromFirst(esterhat);
                }
            }
            if (tmpCnp.Value > 0)
            {              
                this.DoConcept(13).Value = tmpCnp.Value;
            }
            else if (this.DoConcept(2005).Value == 0 && this.DoConcept(1090).Value == 0 && this.DoConcept(1091).Value == 0)
            {
                this.DoConcept(3004).Value = 1;
            }
            this.DoConcept(5022).FromPairs = "";
            GetLog(MyRule, DebugRuleState.After, 4, 4002, 3028, 3004, 13);
        }
     
        /// <summary>
        ///•	در صورتی که میزان  " کارکرد روزانه ماهانه" 
        ///کمتر از "تعداد روزی که شخص در آنها حضور داشته است" 
        ///باشد , آنگاه هر 7:30 از "اضافه کار  ماهانه" شخص در روزهایی که
        ///حداقل 3:30 حضور داشته باشد به میزان یک روز به "کارکرد روزانه ماهانه" شخص اضافه میکند.
        ///•	به ازای هر 6 روز کارکرد روزانه , یک روز off به شخص تعلق میگیرد.به عبارتی یکروز از غیبت ماهانه شخص کسر میگردد.
        public virtual void R2502_old(AssignedRule MyRule)
        {
            //502 تعداد حضور کاری
            //5 کارکرد روزانه ماهانه
            //504 تعداد روز که حدنساب حضور را داشته
            //4005 اضافه کار ماهانه
            //506 تعداد آف ماهانه
            //508 تعداد تبدیل ماهانه
            //3034 غیبت غیر مجاز ساعتی
            //3012 حدنساب کارکرد
            this.DoConcept(3034);
            this.DoConcept(3005);
            this.DoConcept(4005);
            GetLog(MyRule, DebugRuleState.Before, 3, 5, 3034, 4002, 4005, 3005, 13);
            this.DoConcept(501);
            this.DoConcept(505);
            this.DoConcept(507);
            int tabdilShodeBeKarkerd = 0;// با توجه به 504
            //this.DoConcept(509).Value = this.DoConcept(3028).Value;
            if (this.DoConcept(9).Value > 0)
            {
                this.DoConcept(510).Value = this.DoConcept(3034).Value;
                int karkerdInDay = 0;
                if (this.DoConcept(5).Value < this.DoConcept(502).Value && this.DoConcept(504).Value > 0)
                {
                    karkerdInDay = this.DoConcept(3034).Value / this.DoConcept(504).Value;
                    if (karkerdInDay > 0)
                    {
                        int tabdilKarkerd = this.DoConcept(4005).Value / karkerdInDay;
                        tabdilKarkerd = tabdilKarkerd <= this.DoConcept(504).Value ? tabdilKarkerd : this.DoConcept(504).Value;
                        this.DoConcept(4005).Value -= karkerdInDay * tabdilKarkerd;
                        this.DoConcept(3034).Value -= karkerdInDay * tabdilKarkerd;
                        this.DoConcept(508).Value = tabdilKarkerd;
                        this.DoConcept(5).Value += tabdilKarkerd;
                        tabdilShodeBeKarkerd = tabdilKarkerd;
                        if (this.DoConcept(3034).Value == 0 && this.DoConcept(510).Value > 0)
                        {
                            this.DoConcept(3034).Value = 1;
                        }
                    }
                }
                int off = this.DoConcept(5).Value / 6;
                off = off > 4 ? 4 : off;
                int morOff = 0;
                if (this.DoConcept(3005).Value < off)
                {
                    this.DoConcept(4005).Value += (off - this.DoConcept(3005).Value) * this.DoConcept(7).Value;
                    this.DoConcept(3).Value += (off - this.DoConcept(3005).Value) * this.DoConcept(7).Value;
                    morOff = off - this.DoConcept(3005).Value;
                    off = this.DoConcept(3005).Value;
                }
                this.DoConcept(506).Value = off + morOff;
                this.DoConcept(3005).Value -= off;
                this.DoConcept(5).Value += off;

                int minHozour = 3 * 60 + 30;
                this.DoConcept(3034).Value = 0;
                if (this.DoConcept(3005).Value > 0 && this.DoConcept(4005).Value > minHozour && tabdilShodeBeKarkerd < this.DoConcept(504).Value)
                {
                    this.DoConcept(3034).Value = this.DoConcept(7).Value - this.DoConcept(4005).Value;
                    if (this.DoConcept(3034).Value < 0)
                    {
                        this.DoConcept(4005).Value = -1 * this.DoConcept(3034).Value;
                        this.DoConcept(3034).Value = 0;
                    }
                    else
                    {
                        this.DoConcept(4005).Value = 1;
                    }

                    this.DoConcept(5).Value += 1;
                    this.DoConcept(3005).Value -= 1;
                }
                DateRange range = this.GetDateRange(5, this.RuleCalculateDate);
                int days = (range.ToDate - range.FromDate).Days + 1;
/*
 * //باید به 504 توجه شود
                //تبدیل دوباره اضافه کار به کارکرد
                if (this.DoConcept(3005).Value == 0 && this.DoConcept(4005).Value > 0)
                {
                    if (this.DoConcept(5).Value < days)
                    {
                        if (karkerdInDay > 0)
                        {
                            int tabdilKarkerd = this.DoConcept(4005).Value / karkerdInDay;
                            tabdilKarkerd = tabdilKarkerd <= (days - this.DoConcept(5).Value) ? tabdilKarkerd : (days - this.DoConcept(5).Value);
                            this.DoConcept(4005).Value -= karkerdInDay * tabdilKarkerd;
                           // this.DoConcept(3034).Value -= karkerdInDay * tabdilKarkerd;
                            this.DoConcept(508).Value = tabdilKarkerd;
                            this.DoConcept(5).Value += tabdilKarkerd;
                            if (this.DoConcept(3034).Value == 0 && this.DoConcept(510).Value > 0)
                            {
                                this.DoConcept(3034).Value = 1;
                            }
                        }
                    }
                    //تبدیل اضافه کار نیمه
                    if (this.DoConcept(5).Value < days)
                    {
                        if (karkerdInDay > 0)
                        {
                            int tabdilKarkerd = this.DoConcept(4005).Value / this.DoConcept(3012).Value;
                            tabdilKarkerd = tabdilKarkerd <= (days - this.DoConcept(5).Value) ? tabdilKarkerd : (days - this.DoConcept(5).Value);
                            this.DoConcept(4005).Value -= this.DoConcept(3012).Value * tabdilKarkerd;
                            // this.DoConcept(3034).Value -= karkerdInDay * tabdilKarkerd;
                            this.DoConcept(508).Value = tabdilKarkerd;
                            this.DoConcept(5).Value += tabdilKarkerd;
                            if (this.DoConcept(3034).Value == 0 && this.DoConcept(510).Value > 0)
                            {
                                this.DoConcept(3034).Value = 1;
                            }
                        }
                    }

                }*/
            }
           
            GetLog(MyRule, DebugRuleState.After, 3, 5, 3034, 4002, 4005, 3005, 13);
        }

        /// <summary>
        ///•	در صورتی که میزان  " کارکرد روزانه ماهانه" 
        ///کمتر از "تعداد روزی که شخص در آنها حضور داشته است" 
        ///باشد , آنگاه هر 7:30 از "اضافه کار  ماهانه" شخص در روزهایی که
        ///حداقل 3:30 حضور داشته باشد به میزان یک روز به "کارکرد روزانه ماهانه" شخص اضافه میکند.
        ///•	به ازای هر 6 روز کارکرد روزانه , یک روز off به شخص تعلق میگیرد.به عبارتی یکروز از غیبت ماهانه شخص کسر میگردد.
        public virtual void R2502_excloud(AssignedRule MyRule)
        {
            //502 تعداد حضور کاری
            //5 کارکرد روزانه ماهانه
            //504 تعداد روز که حدنساب حضور را داشته
            //4005 اضافه کار ماهانه
            //506 تعداد آف ماهانه
            //508 تعداد تبدیل ماهانه
            //3034 غیبت غیر مجاز ساعتی
            //3012 حدنساب کارکرد
            GetLog(MyRule, DebugRuleState.Before, 3, 5, 3034, 4002, 4005, 3005, 13);

            this.DoConcept(3034);
            this.DoConcept(3005);
            this.DoConcept(4005);
            this.DoConcept(501);
            this.DoConcept(505);
            this.DoConcept(507);
            if (this.DoConcept(3).Value > 0)
            {
                 DateRange daterange= this.GetDateRange(3, this.ConceptCalculateDate);
                int days=(daterange.ToDate-daterange.FromDate).Days+1;
                this.DoConcept(5).Value = this.DoConcept(3).Value / this.DoConcept(7).Value;
                if (this.DoConcept(5).Value > days - this.DoConcept(3005).Value)
                {
                    this.DoConcept(5).Value = (days - this.DoConcept(3005).Value);
                }
                this.DoConcept(510).Value = this.DoConcept(3034).Value;

                int off = this.DoConcept(5).Value / 6;
                off = off > 4 ? 4 : off;
                int morOff = 0;
                if (this.DoConcept(3005).Value < off)
                {
                    this.DoConcept(4005).Value += (off - this.DoConcept(3005).Value) * this.DoConcept(7).Value;
                    this.DoConcept(3).Value += (off - this.DoConcept(3005).Value) * this.DoConcept(7).Value;
                    morOff = off - this.DoConcept(3005).Value;
                    off = this.DoConcept(3005).Value;
                }
                this.DoConcept(506).Value = off + morOff;
                this.DoConcept(3005).Value -= off;
                this.DoConcept(5).Value += off;                                         
            }

            GetLog(MyRule, DebugRuleState.After, 3, 5, 3034, 4002, 4005, 3005, 13);
        }

        /// <summary>
        /// اعمال ضریب کارکد در تعطیلات رسمی غیر از جمعه
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2503(AssignedRule MyRule)
        {

            GetLog(MyRule, DebugRuleState.Before, 13);

            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.RuleCalculateDate.DayOfWeek!=DayOfWeek.Friday)
            {
                float coEfficient = MyRule["first", this.RuleCalculateDate].ToInt() / 100f;
                float zarib = (float)this.DoConcept(13).Value * coEfficient;

                this.DoConcept(13).Value = (int)zarib;
            }

            GetLog(MyRule, DebugRuleState.After, 13);
        }

        public override void R2006(AssignedRule MyRule)
        {
            this.DoConcept(3034);
            this.DoConcept(3005);
            this.DoConcept(4005);
            this.DoConcept(1110);
            this.DoConcept(501);
            this.DoConcept(505);
            this.DoConcept(507);
            if (this.DoConcept(3).Value > 0)
            {
                int overKarkerd = 0;
                DateRange daterange = this.GetDateRange(3, this.ConceptCalculateDate);
                int daysCount = this.DoConcept(502).Value;// (daterange.ToDate - daterange.FromDate).Days + 1;
                if (daysCount > (daterange.ToDate - daterange.FromDate).Days + 1)
                {
                    daysCount = (daterange.ToDate - daterange.FromDate).Days + 1;
                }
                
                this.DoConcept(5).Value = this.DoConcept(3).Value / this.DoConcept(7).Value;

                if (this.DoConcept(5).Value + this.DoConcept(3005).Value + this.DoConcept(1076).Value > daysCount)
                {
                    overKarkerd += (this.DoConcept(5).Value - ((this.DoConcept(5).Value + this.DoConcept(3005).Value + this.DoConcept(1076).Value) - daysCount)) * this.DoConcept(7).Value;
                    this.DoConcept(5).Value = daysCount - this.DoConcept(3005).Value - this.DoConcept(1076).Value;
                }
                overKarkerd = this.DoConcept(3).Value - (this.DoConcept(5).Value * this.DoConcept(7).Value);
               
                int off = this.DoConcept(5).Value / 6;
                off = off > 4 ? 4 : off;
                if (this.DoConcept(3005).Value < off)
                {
                    overKarkerd += (off - this.DoConcept(3005).Value) * this.DoConcept(7).Value;
                    off = this.DoConcept(3005).Value;
                }

                this.DoConcept(506).Value = off;
                this.DoConcept(3005).Value -= off;
                this.DoConcept(5).Value += off;

                if (this.DoConcept(5).Value + this.DoConcept(3005).Value + this.DoConcept(1076).Value > daysCount)
                {
                    overKarkerd += (this.DoConcept(5).Value - ((this.DoConcept(5).Value + this.DoConcept(3005).Value + this.DoConcept(1076).Value) - daysCount)) * this.DoConcept(7).Value;
                    this.DoConcept(506).Value -= ((this.DoConcept(5).Value + this.DoConcept(3005).Value + this.DoConcept(1076).Value) - daysCount);
                    this.DoConcept(5).Value = daysCount - this.DoConcept(3005).Value - this.DoConcept(1076).Value;
                }

                int kam = daysCount - this.DoConcept(5).Value - this.DoConcept(3005).Value - this.DoConcept(1076).Value;
                if (kam > 0)
                {
                    int k = overKarkerd / this.DoConcept(7).Value;
                    if (k > kam)
                    {
                        k = kam;
                    }
                    if (k > 0)
                    {
                        this.DoConcept(5).Value += k;
                        overKarkerd -= (k * this.DoConcept(7).Value);
                        this.DoConcept(506).Value += k;
                    }
                }

                int monthlyLazem = MyRule[daterange.DateRangeOrder.ToString() + "th", this.RuleCalculateDate].ToInt();
                this.DoConcept(10).Value = monthlyLazem * HourMin;
                this.DoConcept(4005).Value = overKarkerd;

            }
        }


        #endregion

        #region قوانين مرخصي

        public override void R3008(AssignedRule MyRule)
        {
            //13 کارکردناخالص
            //1005 مفهوم مرخصي استحقاقي روزانه
            //1003 مفهوم مرخصي استحقاقي ساعتي
            //110 مفهوم مرخصي بايد به کارکرد خالص اضافه شود
            //1001 مرخصي درروز

            //مرخصی ساعتی با حقوق-دادگاه 1025
            //مرخصی ساعتی با حقوق 1027

            //مرخصی با حقوق روزانه_فوت بستگان 1029
            //مرخصی با حقوق روزانه_جبرانی ماموریت 1031
            //مرخصی با حقوق روزانه_زایمان 1033
            //مرخصی با حقوق روزانه 1035
            //مرخصی با حقوق روزانه-دادگاه ورزشی 1037
            //1010  مرخصی استعلاجی روزانه
            //1090 مفهوم مجموع انواع مرخصی روزانه
            //1008مرخصی استعلاجی ساعتی
            GetLog(MyRule, DebugRuleState.Before, 13);
            if (MyRule["First", this.RuleCalculateDate].ToInt() == 0)
            {
                if (this.DoConcept(1090).Value >= 1)
                {
                    this.DoConcept(13).Value = this.DoConcept(1090).Value * this.DoConcept(1001).Value;
                    this.DoConcept(13).Value += this.DoConcept(4002).Value;
                }
                else
                {
                    //به دلیل اینکه در اضافه کار مرخصی های خارج از شیفت
                    //لحاظ شده است و در تعریف کارکردناخالص به آن اضافه گشته
                    //در این جا اشتراک مرخصی و شیفت را به کارکردناخالص اضافه میکنیم

                    int value = 0;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1003)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1008)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1038)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1025)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1027)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1010)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1008)).Value;


                    if (value > 0)
                    {
                        this.DoConcept(13).Value += value;
                    }
                }
            }
            else //مرخصی به کارکرد خالص اضافه شده و حالا کافی است کارکرد ناخالص دوباره محاسبه شود
            {
                this.ReCalculate(13);
            }
            if (this.DoConcept(1090).Value >= 1)
            {
                this.DoConcept(4).Value = 1;//مانا آرد
            }
            GetLog(MyRule, DebugRuleState.After, 13);
        }

        /// <summary>
        /// دادن بودجه در آخر هر ماه
        /// قانون بودجه ندارد
        /// مرخصی میتواند منفی شود
        /// آخر ماه به نسبت کارکرد شارژ میشود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3501(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 9);
             DateRange dateRange = this.GetDateRange(4039, this.RuleCalculateDate);
             if (this.RuleCalculateDate == dateRange.ToDate || this.Person.EndEmploymentDate == this.RuleCalculateDate)
             {
                 int maxKarkerd = this.DoConcept(10).Value;
                 //int maxKarkerd = MyRule["first", this.RuleCalculateDate].ToInt();
                 int maxLeave = MyRule["second", this.RuleCalculateDate].ToInt();
                 int hozour = this.DoConcept(9).Value;
                 if (hozour > 0 && maxKarkerd > 0)
                 {
                     int nesbat = (hozour * maxLeave) / maxKarkerd;
                     nesbat = nesbat > maxLeave ? maxLeave : nesbat;
                     if (nesbat > 0)
                     {
                         //this.Person.AddUsedLeave(this.RuleCalculateDate, -1 * (nesbat), null);
                         //LeaveCalcResult CurrentLCR = this.Person.LeaveCalcResultList.Where(x => x.Date == this.RuleCalculateDate).LastOrDefault();
                         //if (CurrentLCR != null)
                         //{
                         //    CurrentLCR.MinuteUsed -= nesbat;
                         //    if (CurrentLCR.MinuteUsed < 0)
                         //    {
                         //        CurrentLCR.DayUsed--;
                         //        CurrentLCR.MinuteUsed += CurrentLCR.LeaveMinuteInDay;
                         //    }
                         //    CurrentLCR.DoAdequate(CurrentLCR.LeaveMinuteInDay);
                         //}
                         this.Person.AddBudgetLeave(this.RuleCalculateDate, (nesbat), null);
                     }                    
                 }
                 this.DoConcept(10).Value = 0;//عدم نمایش
             }
             GetLog(MyRule, DebugRuleState.After, 9);
        }

    

        #endregion

        #region قوانين ماموريت
      
        #endregion

        #region قوانين کم کاري

        #endregion

        #region قوانين اضافه کاري

        #endregion


        #endregion        

    }
}