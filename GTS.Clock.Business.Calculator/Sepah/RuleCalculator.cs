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
    /// <summary>
    /// بانک سپه
    /// </summary>
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

        /// <summary>
        /// قانون متفرقه- مقداردهی به روز ناهاری
        /// </summary>
        public override void R1009(AssignedRule MyRule)
        {
            // 
            // 5014 کارکرد لازم برای حق غذا
            // 5015 حق غذا ماهانه
            // 2023 مجموع ماموريت ساعتي
            // 4502 باجه 
            // 308  جمع مرخصی استحقاقی ساعتی
            // 1    حضور
            // 1005 مرخصی استحقاقی روزانه
            // 1010 مرخصی استعلاجی روزانه
            // 2005 ماموریت روزانه

            //this.Person.AddUsedLeave(this.RuleCalculateDate, 480, new Permit());
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                && this.DoConcept(1005).Value < 1
                && this.DoConcept(1010).Value < 1
                && this.DoConcept(2005).Value < 1
                )
            {
                GetLog(MyRule, DebugRuleState.Before, 5014,5015);
                var totalOfParameterVale = 0;

                if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += Operation.Intersect((this.DoConcept(1)), this.Person.GetShiftByDate(this.RuleCalculateDate)).Value;
                if (MyRule["Third", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(4512).Value > 0 ? this.DoConcept(4514).Value : this.DoConcept(3021).Value;
                if (MyRule["Fourth", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(4002).Value;
                if (MyRule["Fifth", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(1003).Value;
                if (MyRule["Sixth", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(2004).Value;
                if (MyRule["Seventh", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(2014).Value;
                if (MyRule["Eighth", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(4009).Value;
                if (MyRule["Ninth", this.RuleCalculateDate].ToInt() == 1) totalOfParameterVale += this.DoConcept(4502).Value;

                if (totalOfParameterVale >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    DoConcept(5014).Value = 1;
                    this.ReCalculate(5015);
                }

                GetLog(MyRule, DebugRuleState.After , 5014, 5015);
            }
        }

        /// <summary>
        /// وظیفه اجرای مفاهیم ماهانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R1016(AssignedRule MyRule)
        {
            base.R1016(MyRule);

            this.DoConcept(5502);// ویژه جانبازی ماهانه
            //this.DoConcept(3005);//غيبت روزانه ماهانه
            this.DoConcept(3042);//اضافه کار شب ماهانه
            //this.DoConcept(3041);//غیبت مجاز شیردهی ماهانه
            //this.DoConcept(3043);//غیبت مجاز مهد ماهانه
            //this.DoConcept(3045);//غیبت مجاز تقليل ماهانه
            this.DoConcept(4504);//باجه عصر خالص
            this.DoConcept(4505);//باجه عصر نا خالص
            this.DoConcept(4010);//اضافه کارساعتی تعطیل ماهانه
            this.DoConcept(4510);//اول وقت
            //this.DoConcept(4513);//کاهشی
            this.DoConcept(5504);//وضعیت روز
        }

        /// <summary>
        /// تشخیص وضعیت روز
        /// </summary>
        /// <param name="MyRule"></param>
        public void R1501(AssignedRule MyRule)
        {
            //1005	مرخصی استحقاقی روزانه
            //1010	مرخصی استعلاجی روزانه
            //2005	ماموریت روزانه
            //3004	غیبت روزانه
            //1091	مجموع مرخصی بی حقوق روزانه
            //1096	مجموع مرخصی با حقوق روزانه

            this.DoConcept(5504).Value = -1;//جهت نمایش در تایم شیت
            this.DoConcept(5504).FromPairs = " ";

            //1005	مرخصی استحقاقی روزانه
            if (this.DoConcept(1005).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "مرخصی";
                return;
            }

            //1010	مرخصی استعلاجی روزانه
            if (this.DoConcept(1010).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "استعلاجی";
                return;
            }


            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
            if (ProceedTraffic.PairCount > 0 && !ProceedTraffic.IsFilled)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "تردد ناقص";
                return;
            }

            //3004	غیبت روزانه
            if (this.DoConcept(3004).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "غیبت";
                return;
            }

            //2005	ماموریت روزانه
            if (this.DoConcept(2005).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "ماموریت";
                return;
            }

            //1096	مجموع مرخصی با حقوق روزانه
            if (this.DoConcept(1096).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "اعطایی";
                return;
            }


            //1091	مجموع مرخصی بی حقوق روزانه
            if (this.DoConcept(1091).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "بی حقوق";
                return;
            }


            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "تعطیل";
                return;
            }

            if (this.DoConcept(1).Value > 0)
            {
                this.DoConcept(5503).Value = -1;
                this.DoConcept(5503).FromPairs = "حاضر";
                return;
            }

        }

        #endregion

        #region قوانين کارکرد

        #endregion

        #region قوانين مرخصي

        /// <summary>
        /// مرخصي 3-1: جمع مرخصي در روز چنانچه به .... رسيد كل روز مرخصي تبديل شود
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R3002(AssignedRule MyRule)
        {
            //1 مفهوم حضور
            //1001 مفهوم مرخصي درروز
            //مرخصي استحقاقي ساعتي 1003  
            //مرخصي استحقاقي روزانه 1005        
            //3001 مفهوم غيبت خالص ساعتي           
            //1014 مفهوم مرخصي استحقاقي بدون حقوق  در صورت عدم طلب مرخصي استحقاقي
            //1056 مرخصي بی حقوق ساعتي
            //1066 مرخصي بی حقوق روزانه
            //2030 کار خارج از اداره
            //2023 مجموع ماموريت ساعتي

            //3004	غیبت روزانه

            //4025 تبدیل حضور به اضافه کار در روز مرخصی

            this.DoConcept(4512).Value = 0;

            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            if ((proceedTraffic != null && !proceedTraffic.IsFilled) &&
                !proceedTraffic.HasDailyItem &&
                (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0) &&
                (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4"))
               )// &&  this.DoConcept(3028).Value <= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                return;
            }

            if (this.DoConcept(1005).Value != 1 && this.DoConcept(3004).Value != 1 && this.DoConcept(1003).Value>0)
            {
                int leaveHour = this.DoConcept(1003).Value;
                if (
                    //leaveHour > 0 && // حتما در آنروز مرخصی ساعتی داشته است
                    leaveHour + this.DoConcept(3028).Value > MyRule["First", this.RuleCalculateDate].ToInt()
                    )
                {
                    var cnpList = new[] { 2, 4, 1003, 1005, 3004, 3020, 3028, 4512, 4514, 4515 };
                    GetLog(MyRule, " Before Execute State:", cnpList);

                    #region مقداردهي به مفاهيم مغايرت
                    this.DoConcept(4512).Value = 1;

                    this.DoConcept(4514).Value = this.DoConcept(1003).Value;
                    ((PairableScndCnpValue)this.DoConcept(1003)).ClearPairs();

                    this.DoConcept(4515).Value = this.DoConcept(3028).Value;
                    ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();

                    #endregion

                    #region تبديل به مرخصي روزانه
                    if (MyRule["Second", this.RuleCalculateDate].ToInt().Equals(0))
                    {
                        int leaveInDay = this.DoConcept(1001).Value;
                        this.DoConcept(1005).Value = 1;
                        this.Person.AddUsedLeave(this.RuleCalculateDate, leaveInDay - leaveHour, null);
                    }
                    #endregion

                    #region تبديل به غيبت روزانه
                    if (MyRule["Second", this.RuleCalculateDate].ToInt().Equals(1))
                    {
                        this.DoConcept(3004).Value = 1;
                        this.DoConcept(2).Value = 0;
                        this.DoConcept(4).Value = 0;
                    }
                    #endregion

                    this.DoConcept(3020).Value = 0;

                    GetLog(MyRule, " After Execute State:", cnpList);

                }
            }

            #region مرخصي استعلاجي
            //1008 مرخصي استعلاجي ساعتي
            //1010 مرخصي استعلاجي روزانه

            if (this.DoConcept(1008).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1010, 1008, 4002);

                this.DoConcept(1010).Value = 1;
                ((PairableScndCnpValue)this.DoConcept(1008)).ClearPairs();
                GetLog(MyRule, " After Execute ", 1010, 1008, 4002);

            }

            #endregion

            #region مرخصی با حقوق
            if (this.DoConcept(1038).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1031, 1038, 4002);

                this.DoConcept(1031).Value = 1;
                this.DoConcept(1038).Value = 0;

                GetLog(MyRule, " After Execute ", 1031, 1038, 4002);

            }
            if (this.DoConcept(1039).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1029, 1039, 4002);

                this.DoConcept(1029).Value = 1;
                this.DoConcept(1039).Value = 0;

                GetLog(MyRule, " After Execute ", 1029, 1039, 4002);

            }
            if (this.DoConcept(1040).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1037, 1040, 4002);

                this.DoConcept(1037).Value = 1;
                this.DoConcept(1040).Value = 0;

                GetLog(MyRule, " After Execute ", 1037, 1040, 4002);

            }
            if (this.DoConcept(1041).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1033, 1041, 4002);

                this.DoConcept(1033).Value = 1;
                this.DoConcept(1041).Value = 0;

                GetLog(MyRule, " After Execute ", 1033, 1041, 4002);

            }
            if (this.DoConcept(1042).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1035, 1042, 4002);

                this.DoConcept(1035).Value = 1;
                this.DoConcept(1042).Value = 0;

                GetLog(MyRule, " After Execute ", 1035, 1042, 4002);

            }
            #endregion

            #region مرخصی بی حقوق

            if (this.DoConcept(1054).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1066, 1054, 4002);

                this.DoConcept(1066).Value = 1;
                this.DoConcept(1054).Value = 0;

                GetLog(MyRule, " After Execute ", 1066, 1054, 4002);

            }
            if (this.DoConcept(1056).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1066, 1056, 4002);

                this.DoConcept(1066).Value = 1;
                this.DoConcept(1056).Value = 0;

                GetLog(MyRule, " After Execute ", 1066, 1056, 4002);

            }
            if (this.DoConcept(1058).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1066, 1058, 4002);

                this.DoConcept(1066).Value = 1;
                this.DoConcept(1058).Value = 0;

                GetLog(MyRule, " After Execute ", 1066, 1058, 4002);

            }
            if (this.DoConcept(1060).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1066, 1060, 4002);

                this.DoConcept(1066).Value = 1;
                this.DoConcept(1060).Value = 0;

                GetLog(MyRule, " After Execute ", 1066, 1060, 4002);

            }
            if (this.DoConcept(1062).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute ", 1066, 1062, 4002);

                this.DoConcept(1066).Value = 1;
                this.DoConcept(1062).Value = 0;

                GetLog(MyRule, " After Execute ", 1066, 1062, 4002);
            }
            #endregion

            this.ReCalculate(1090);
        }


        /// <summary>
        /// مرخصي سپه:غيبت ساعتي غير مجاز ماهانه در صورتيكه كمتر از ... بود با مرخصي استحقاقي جبران شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3016(AssignedRule MyRule)
        {
            // 1011	مرخصی استحقاقی ساعتی ماهانه

            // 3034 غیبت ساعتی غیرمجاز ماهانه

            var conceptList = new[] { 1011, 3034 };
            GetLog(MyRule, " Before Execute State:", conceptList);
            if (
                this.Person.GetRemainLeave(this.RuleCalculateDate) > 0 &&
                this.DoConcept(3034).Value > 0 &&
                this.DoConcept(3034).Value <= MyRule["First", this.RuleCalculateDate].ToInt()
                )
            {
                var shouldDecrease = Operation.Minimum(this.Person.GetRemainLeave(this.RuleCalculateDate), this.DoConcept(3034).Value);
                this.DoConcept(3034).Value -= shouldDecrease;
                this.DoConcept(1011).Value += shouldDecrease;
                this.Person.AddUsedLeave(this.RuleCalculateDate, shouldDecrease, null);
            }
            GetLog(MyRule, " After Execute State:", conceptList);
        }

        #endregion

        #region قوانين ماموريت



        /// <summary>
        /// ماموریت 14-1: ماموریت در زمان استراحت بین وقت مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R4004(AssignedRule MyRule)
        {
            //14 مفهوم شب
            //2012 ماموریت در استراحت بین وقت مجاز است
            //ماموريت دراستراحت بين وقت 2013
            //اضافه کارساعتي 4002
            //4012 اضافه کارساعتي مجازشب
            //2023 مفهوم مجموع ماموريت ساعتي
            GetLog(MyRule, " Before Execute State:", 4002, 2004, 2019, 2020, 2021, 2022, 2023);
            if (this.DoConcept(2012).Value == 0)
            {
                ((PairableScndCnpValue)this.DoConcept(2004)).AppendPairs(Operation.Intersect(this.DoConcept(2004), this.DoConcept(2013)));
                ((PairableScndCnpValue)this.DoConcept(2019)).AppendPairs(Operation.Intersect(this.DoConcept(2019), this.DoConcept(2013)));
                ((PairableScndCnpValue)this.DoConcept(2020)).AppendPairs(Operation.Intersect(this.DoConcept(2020), this.DoConcept(2013)));
                ((PairableScndCnpValue)this.DoConcept(2021)).AppendPairs(Operation.Intersect(this.DoConcept(2021), this.DoConcept(2013)));
                ((PairableScndCnpValue)this.DoConcept(2022)).AppendPairs(Operation.Intersect(this.DoConcept(2022), this.DoConcept(2013)));
                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairsEx(((PairableScndCnpValue)this.DoConcept(2013)).Pairs);
                this.ReCalculate(13, 4012, 2023);
            }
            GetLog(MyRule, " After Execute State:", 4002, 2004, 2019, 2020, 2021, 2022, 2023);
        }

        #endregion

        #region قوانين کم کاري

        /// <summary>قانون کم کاري 1-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و هشت-38 درنظر گرفته شده است</remarks>
        public override void R5001(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي 2
            //13 کارکردناخالص

            //غيبت خالص ساعتي 3001
            //3020 غیبت مجاز ساعتی
            //3023 غیبت بین وقت ساعتی مجاز
            //3028 غیبت غیر مجاز ساعتی
            //3029 تاخير ساعتي غيرمجاز

            //تاخيرخالص ساعتي 3008

            //تعداد بازه های تاخیر 3038

            //1002 مرخصي خالص استحقاقي ساعتي 
            //2002 ماموريت خالص ساعتي
            //1056 مرخصی بی حقوق ساعتی 12

            //1025 مرخصی با حقوق ساعتی_دادگاه-ورزشی
            //1027 مرخصی با حقوق ساعتی

            //3002 کل تاخیر یا تعجیل بیش از حد مجاز روزانه غیبت حساب شود

            var cnpList = new[] { 2, 3008, 3028, 3020, 3029, 3038, 1002, 2002 };

            GetLog(MyRule, " Before Execute State:", cnpList);
            //this.DoConcept(1002);
            //this.DoConcept(1056);
            this.DoConcept(2023);

            int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From;
            IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
            if (this.DoConcept(1).Value == 0 || takhir.From != shiftStart || takhir.To != ((PairableScndCnpValue)this.DoConcept(1)).Pairs.OrderBy(x => x.From).First().From)
            {
                return;
            }

            if (this.DoConcept(3002).Value == 1 && this.DoConcept(3029).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " After Execute State:", cnpList);
                return;
            }

            if (Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)).Value > 0 && this.DoConcept(1).Value > 0)
            {
                int StartShift = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                IPair StartMissionPair = ((PairableScndCnpValue)this.DoConcept(2002)).Pairs.OrderBy(x => x.From).FirstOrDefault();
                if (StartMissionPair != null && StartMissionPair.From - this.DoConcept(3029).Value == StartShift)
                {
                    var int3021 = Operation.Minimum(this.DoConcept(3029).Value, MyRule["Second", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3029)).DecreasePairFromFirst(int3021);
                    this.DoConcept(2).Value += this.DoConcept(3021).Value;

                    this.ReCalculate(13, 3020, 3028);
                }
                else
                {
                    #region اگر بخشی از غیبت اول وقت(تاخیر) را مرخصی گرفته است مابقی تاخیر نباید به مجاز تبدیل شود

                    int ShiftStartWithAllowedDelay =
                        this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).FirstOrDefault().From
                        + MyRule["First", this.RuleCalculateDate].ToInt();

                    if (((PairableScndCnpValue)this.DoConcept(1002)).Pairs.Where(x => x.From == ShiftStartWithAllowedDelay).Count() > 0)
                    {
                        GetLog(MyRule, " After Execute State:", cnpList);
                        return;
                    }

                    #endregion

                    var int3021 = Operation.Minimum(this.DoConcept(3029).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3029)).DecreasePairFromFirst(int3021);
                    this.DoConcept(2).Value += int3021;

                    this.ReCalculate(13, 3020, 3028);

                    if (this.DoConcept(3029).Value > 0)
                        this.DoConcept(3038).Value = 1;

                }
            }

            GetLog(MyRule, " After Execute State:", cnpList);
        }

        /// <summary>قانون کم کاري 5-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و هفت-47 درنظر گرفته شده است</remarks>
        public override void R5009(AssignedRule MyRule)
        {
            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز
            //3004 غيبت روزانه

            //کارکردخالص ساعتي 2
            //4 کارکردخالص روزانه
            //13 کارکردناخالص ساعتی
            //اضافه کار ساعتي 4002

            //1003 مرخصي استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي


            GetLog(MyRule, " Before Execute State:", 2, 4, 3020, 3028, 4002, 3004, 1002, 2002);

            this.DoConcept(1002);
            this.DoConcept(1056);
            this.DoConcept(2002);
             ProceedTraffic proceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
             if ((proceedTraffic != null && !proceedTraffic.IsFilled) &&
                 !proceedTraffic.HasDailyItem &&
                 (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0) &&
                 (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4"))
                )// &&  this.DoConcept(3028).Value <= MyRule["First", this.RuleCalculateDate].ToInt())
             {
                 return;
             }
            if (this.DoConcept(3028).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {

                this.DoConcept(3004).Value = 1;
                this.DoConcept(3020).Value = 0;
                //this.DoConcept(3028).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();

                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(
                     Operation.Differance(((PairableScndCnpValue)this.DoConcept(2)).Pairs, this.Person.GetShiftByDate(this.RuleCalculateDate))
                    );
                this.DoConcept(4).Value = 0;
                this.DoConcept(2).Value = 0;
                this.DoConcept(13).Value = 0;
            }
            GetLog(MyRule, " After Execute State:", 2, 4, 3020, 3028, 4002, 3004, 1002, 2002);
        }

     
        /// <summary>
        /// شیردهی از ابتدا و انتها
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5021(AssignedRule MyRule)
        {
            //3020 غیبت ساعتی مجاز
            //3028 غیبت ساعتی
            //3040 غیبت مجاز شیردهی
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            var conceptList = new[] { 2, 3020, 3028, 3040 };


            GetLog(MyRule, " Before Execute State:", conceptList);

            //this.DoConcept(1082);
            //this.DoConcept(2023);

            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                personParam != null &&
                this.DoConcept(3028).Value > 0
                )
            {

                var minutes = Utility.ToInteger(personParam.Value);
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderByDescending(x => x.From))
                {
                    if (pair.Value > minutes)
                    {
                        IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                        this.DoConcept(3020).Value += tempPair.Value;

                        var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                        // غيبت ساعتي مجاز شيردهي
                        ((PairableScndCnpValue)this.DoConcept(3040)).AppendPair(tempPair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                        pair.To -= minutes;

                        break;
                    }

                    if (pair.Value == minutes)
                    {
                        this.DoConcept(3020).Value += pair.Value;

                        ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                        // غيبت ساعتي مجاز شيردهي
                        ((PairableScndCnpValue)this.DoConcept(3040)).AppendPair(pair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                        pair.From = pair.To = 0;
                        break;
                    }

                    this.DoConcept(3020).Value += pair.Value;

                    minutes -= pair.Value;

                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                    // غيبت ساعتي مجاز شيردهي
                    ((PairableScndCnpValue)this.DoConcept(3040)).AppendPair(pair);

                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                    pair.From = pair.To = 0;
                }
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)));
            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اعمال مهد
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5022(AssignedRule MyRule)
        {
            //3020 غیبت ساعتی مجاز
            //3028 غیبت ساعتی
            //3042	غیبت مجاز مهد
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            var conceptList = new[] { 2, 3020, 3028, 3040 };


            GetLog(MyRule, " Before Execute State:", conceptList);

            //this.DoConcept(1082);
            //this.DoConcept(2023);

            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_mahd", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                personParam != null &&
                this.DoConcept(3028).Value > 0
                )
            {

                var minutes = Utility.ToInteger(personParam.Value);
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderByDescending(x => x.From))
                {
                    if (pair.Value > minutes)
                    {
                        IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                        this.DoConcept(3020).Value += tempPair.Value;

                        var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                        // غيبت ساعتي مجاز مهد
                        ((PairableScndCnpValue)this.DoConcept(3042)).AppendPair(tempPair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                        pair.To -= minutes;

                        break;
                    }

                    if (pair.Value == minutes)
                    {
                        this.DoConcept(3020).Value += pair.Value;

                        ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                        // غيبت ساعتي مجاز مهد
                        ((PairableScndCnpValue)this.DoConcept(3042)).AppendPair(pair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                        pair.From = pair.To = 0;
                        break;
                    }

                    this.DoConcept(3020).Value += pair.Value;

                    minutes -= pair.Value;

                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                    // غيبت ساعتي مجاز مهد
                    ((PairableScndCnpValue)this.DoConcept(3042)).AppendPair(pair);

                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                    pair.From = pair.To = 0;
                }
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)));
            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اعمال کسر تقلیل برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5023(AssignedRule MyRule)
        {
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            var conceptList = new[] { 2, 3, 13, 3020, 3028, 3044, 4002, 4005, 4006, 4007 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            //this.DoConcept(1082);
            //this.DoConcept(2023);


            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_taghlil", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                // مفدار غیبت مجاز برای تقلیل
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                personParam != null
                )
            {
                var minutes = Utility.ToInteger(personParam.Value);

                if (MyRule["First", this.RuleCalculateDate].ToInt() == 1)
                {
                    #region اعمال روی غیبت
                    if (this.DoConcept(3028).Value > 0)
                    {
                        while (this.DoConcept(3028).Value > 0 && minutes > 0)
                        {
                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderByDescending(x => x.From))
                            {
                                if (pair.Value > minutes)
                                {
                                    IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                                    this.DoConcept(3020).Value += tempPair.Value;

                                    var PairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(PairableScndCnpValue.Pairs);

                                    // غيبت ساعتي مجاز تقليل
                                    ((PairableScndCnpValue)this.DoConcept(3044)).AppendPair(tempPair);

                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                                    pair.To -= minutes;

                                    minutes = 0;

                                    break;
                                }

                                if (pair.Value == minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    minutes -= pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                                    // غيبت ساعتي مجاز تقليل
                                    ((PairableScndCnpValue)this.DoConcept(3044)).AppendPair(pair);

                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;

                                    break;
                                }

                                if (pair.Value < minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    minutes -= pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                                    // غيبت ساعتي مجاز تقليل
                                    ((PairableScndCnpValue)this.DoConcept(3044)).AddPair(pair);

                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                }
                            }
                        }
                    }
                    #endregion
                }

                if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1)
                {
                    #region اعمال روی اضافه کار
                    if (minutes > 0)
                    {
                        // اعمال روی اضافه کار
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValueEx(minutes);

                        var basePair = new BasePair(
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To - minutes,
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To
                              );

                        // غيبت ساعتي مجاز تقليل
                        ((PairableScndCnpValue)this.DoConcept(3044)).AddPair(basePair);

                        this.ReCalculate(3, 13, 4005, 4006, 4007);
                    }
                    #endregion
                }
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)));

            }
            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اعمال کسر جانبازی یا اضافه کار جانبازی
        /// بر اساس درصد جانبازی
        /// </summary>
        /// <param name="MyRule"></param>
        public void R5501(AssignedRule MyRule)
        {

            var conceptList = new[] { 2, 13, 3020, 3028, 4002, 4007, 3028, 5501 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            //4002   اضافه کارساعتي مجاز

            //3028    غیبت ساعتی غیر مجاز
            //3029    غیبت ساعتی مجاز

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                this.Person.PersonTASpec.R16 > 0 &&
                !Utility.IsEmpty(this.Person.PersonTASpec.R16Value.ComboValue) &&
                Utility.ToInteger(this.Person.PersonTASpec.R16Value.ComboValue) > 0 &&
                this.Person.PersonTASpec.R16Value.ComboValue.Length.Equals(3)
                )
            {

                var percentMinute = new Dictionary<string, int>
                    {
                        {"25", 45},
                        {"30", 60},
                        {"40", 90},
                        {"50", 120},
                        {"60", 150},
                        {"70", 210}
                    };

                var percent = this.Person.PersonTASpec.R16Value.ComboValue.Substring(0, 2);

                if (percentMinute.ContainsKey(percent))
                {
                    var minutes = percentMinute.FirstOrDefault(x => x.Key == percent).Value;

                    bool doOnOvertime = this.Person.PersonTASpec.R16Value.ComboValue.Substring(2, 1) == "1";

                    if (doOnOvertime)
                    {
                        #region OLD
                        // اعمال روی اضافه کار
                        //((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValueEx(minutes);

                        //var basePair = new BasePair(
                        //      ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To - minutes,
                        //      ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To
                        //      ); 
                        #endregion

                        BasePair basePair = null;

                        if (this.DoConcept(4002).Value > 0)
                        {
                            basePair = new BasePair(
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).First().To,
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).First().To + minutes
                              );
                        }
                        else
                        {
                            basePair = new BasePair(
                              this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).First().To,
                              this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).First().To + minutes
                              );
                        }

                        // قرار شد در غيبت نمايش داده نشود
                        //this.DoConcept(5501).Value += minutes;
                        ((PairableScndCnpValue)this.DoConcept(5501)).AddPair(basePair);

                        this.ReCalculate(3, 13, 4005, 4006, 4007);
                    }
                    else
                    {
                        // اعمال روی غیبت
                        if (this.DoConcept(3028).Value > 0)
                        {
                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderByDescending(x => x.From))
                            {
                                if (pair.Value > minutes)
                                {
                                    IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                                    this.DoConcept(3020).Value += tempPair.Value;

                                    var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);
                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                                    pair.To -= minutes;

                                    break;
                                }

                                if (pair.Value == minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);
                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                    break;
                                }

                                this.DoConcept(3020).Value += pair.Value;

                                minutes -= pair.Value;

                                ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);
                                ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                pair.From = pair.To = 0;
                            }
                        }
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)));

            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }


        /// <summary>
        /// کم کاری-سپه:کسر کار مجاز ماهانه با مانده مرخصی جبران شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R5502(AssignedRule MyRule)
        {
            // 1011	مرخصی استحقاقی ساعتی ماهانه

            // 3026 غیبت ساعتی مجاز ماهانه

            var conceptList = new[] { 1011, 3026 };
            GetLog(MyRule, " Before Execute State:", conceptList);
            if (this.Person.GetRemainLeave(this.RuleCalculateDate) > 0 && this.DoConcept(3026).Value > 0)
            {
                var shouldDecrease = Operation.Minimum(this.Person.GetRemainLeave(this.RuleCalculateDate), this.DoConcept(3026).Value);
                this.DoConcept(3026).Value -= shouldDecrease;
                this.DoConcept(1011).Value += shouldDecrease;
                this.Person.AddUsedLeave(this.RuleCalculateDate, shouldDecrease, null);
            }
            GetLog(MyRule, " After Execute State:", conceptList);
        }

        #endregion

        #region قوانين اضافه کاري

        /// <summary>
        /// بانک سپه: سقف مجموع اضافه کار مجاز و باجه
        /// براساس ترتیب از مرخصی مجاز و بعد از باجه کم شود
        /// 1. عادی 
        /// 2. کشیک
        /// 3. باجه
        /// 4. جانبازی
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6020(AssignedRule MyRule)
        {
            var conceptList = new[] { 3, 13, 4005, 4006, 4007, 4010, 4018, 4505, 5502 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غيرمجاز ماهانه
            //3 کارکرد ناخالص ساعتی ماهانه
            //4018 مفهوم حداکثر اضافه کار مجاز ماهانه
            //4513 کاهشی ماهانه
            if (
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R10)
                && Utility.ToInteger(this.Person.PersonTASpec.R10) > 0
                &&
                    (
                        this.DoConcept(4005).Value + this.DoConcept(4010).Value +
                        this.DoConcept(4505).Value + this.DoConcept(5502).Value
                    ) > (Utility.ToInteger(this.Person.PersonTASpec.R10) * 60)
                )
            {

                var maxTotal = Utility.ToInteger(this.Person.PersonTASpec.R10) * 60;

                if (this.DoConcept(9).Value > 0 &&
                    maxTotal > 0 &&
                    (this.DoConcept(4005).Value + this.DoConcept(4010).Value + this.DoConcept(4505).Value + this.DoConcept(5502).Value) >= maxTotal
                    )
                {
                    this.DoConcept(4513).Value = (this.DoConcept(4005).Value + this.DoConcept(4010).Value + this.DoConcept(4505).Value + this.DoConcept(5502).Value) - maxTotal;
                }

                int unAllowed = this.DoConcept(4513).Value;

                #region عادی
                if (this.DoConcept(4005).Value > 0 && unAllowed > 0)
                {
                    if (this.DoConcept(4005).Value >= unAllowed)
                    {
                        this.DoConcept(4005).Value -= unAllowed;
                        //this.DoConcept(4006).Value += unAllowed;
                        this.DoConcept(3).Value -= unAllowed;
                        unAllowed = 0;
                        return;
                    }
                    else
                    {
                        //this.DoConcept(4006).Value += this.DoConcept(4005).Value;
                        unAllowed -= this.DoConcept(4005).Value;
                        this.DoConcept(3).Value -= this.DoConcept(4005).Value;
                        this.DoConcept(4005).Value = 0;
                    }
                }
                #endregion

                #region کشیک
                if (this.DoConcept(4010).Value > 0 && unAllowed > 0)
                {
                    if (this.DoConcept(4010).Value >= unAllowed)
                    {
                        this.DoConcept(4010).Value -= unAllowed;
                        //this.DoConcept(4006).Value += unAllowed;
                        this.DoConcept(3).Value -= unAllowed;
                        unAllowed = 0;
                        return;
                    }
                    else
                    {
                        //this.DoConcept(4006).Value += this.DoConcept(4010).Value;
                        unAllowed -= this.DoConcept(4010).Value;
                        this.DoConcept(3).Value -= this.DoConcept(4010).Value;
                        this.DoConcept(4010).Value = 0;
                    }
                }

                #endregion

                #region باجه
                if (this.DoConcept(4505).Value > 0 && unAllowed > 0)
                {
                    if (this.DoConcept(4505).Value >= unAllowed)
                    {
                        this.DoConcept(4505).Value -= unAllowed;
                        //this.DoConcept(4006).Value += unAllowed;
                        this.DoConcept(3).Value -= unAllowed;
                        unAllowed = 0;
                        return;
                    }
                    else
                    {
                        //this.DoConcept(4006).Value += this.DoConcept(4505).Value;
                        unAllowed -= this.DoConcept(4505).Value;
                        this.DoConcept(3).Value -= this.DoConcept(4505).Value;
                        this.DoConcept(4505).Value = 0;
                    }
                }
                #endregion

                #region جانبازی
                if (this.DoConcept(5502).Value > 0 && unAllowed > 0)
                {
                    if (this.DoConcept(5502).Value >= unAllowed)
                    {
                        this.DoConcept(5502).Value -= unAllowed;
                        //this.DoConcept(4006).Value += unAllowed;
                        this.DoConcept(3).Value -= unAllowed;
                        unAllowed = 0;
                        return;
                    }
                    else
                    {
                        //this.DoConcept(4006).Value += this.DoConcept(5502).Value;
                        unAllowed -= this.DoConcept(5502).Value;
                        this.DoConcept(3).Value -= this.DoConcept(5502).Value;
                        this.DoConcept(5502).Value = 0;
                    }
                }
                #endregion

            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اضافه کاری باجه عصر
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6029(AssignedRule MyRule)
        {
            // بر اساس صحبتهای بانک سپه پنجشنبه ها با جه تعریف نشود
            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday) return;

            //4502 خالص بادجه
            //4504 خالص بادجه ماهانه
            //4505ناخالص بادجه ماهانه 
            var conceptList = new[] { 3, 13, 4002, 4003, 4005, 4006, 4007, 4018, 4502, 4504, 4505 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            var startTime = MyRule["First", this.RuleCalculateDate].ToInt();
            var finishTime = MyRule["Second", this.RuleCalculateDate].ToInt();

            // در صورتيكه چهار شنبه بود از دو پارامتر پنجم و ششم
            // براي تعيين ابتدا و انتهاي باجه استفاده شود
            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday &&
                MyRule["Fifth", this.RuleCalculateDate].ToInt() > 0 &&
                MyRule["Sixth", this.RuleCalculateDate].ToInt() > 0
                )
            {
                startTime = MyRule["Fifth", this.RuleCalculateDate].ToInt();
                finishTime = MyRule["Sixth", this.RuleCalculateDate].ToInt();
            }

            var shouldAddtoBaje = this.DoConcept(4002);

            #region اعمال حضور آخر وقت اجباری
            PairableScndCnpValue missionAfterShift = Operation.Differance(this.DoConcept(2023), this.Person.GetShiftByDate(this.RuleCalculateDate));
            if (missionAfterShift.Pairs.Any() && this.Person.GetProceedTraficByDate(this.RuleCalculateDate).Pairs.Any())
            {
                var missionUnionOverTime = Operation.Union(missionAfterShift, shouldAddtoBaje);

                var from = missionUnionOverTime.Pairs.OrderBy(x => x.From).First().From;
                var to = this.Person.GetProceedTraficByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).Last().To;

                var sshouldAddtoBaje = Operation.Intersect(missionUnionOverTime, new PairableScndCnpValuePair(from, to));

                if (sshouldAddtoBaje.Value > 0)
                {
                    ((PairableScndCnpValue)shouldAddtoBaje).AddPairs(sshouldAddtoBaje.Pairs);
                }
            }

            #endregion

            var baje = Operation.Intersect(shouldAddtoBaje,
                                              new PairableScndCnpValuePair(startTime, finishTime)
                                                  );

            if (baje != null && baje.Value >= MyRule["Forth", this.RuleCalculateDate].ToInt())
            {
                ((PairableScndCnpValue)this.DoConcept(4502)).AddPairs(baje);
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), baje));

                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(
                    Operation.Differance(this.DoConcept(4003), Operation.Intersect(baje, this.DoConcept(4003))));


                ((PairableScndCnpValue)this.DoConcept(4506)).AddPairs(baje);
                //this.DoConcept(4506).Value += (this.DoConcept(4502).Value * MyRule["Third", this.RuleCalculateDate].ToInt()) / 100;
                ((PairableScndCnpValue)this.DoConcept(4506)).IncreasePairsValueByPercent(MyRule["Third", this.RuleCalculateDate].ToInt());

            }
            //ناخالص ماهانه باید بر اساس خالص ماهانه محاسبه گردد
            if (this.DoConcept(4504).Value > 0 && MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
            {
                int monthlyValue = this.DoConcept(4504).Value;
                if (monthlyValue % 60 > 0)
                {
                    var temp = monthlyValue % 60;

                    monthlyValue -= temp;
                    temp = temp >= 30 ? 60 : 0;
                    monthlyValue += temp;
                }

                int percentValue = (monthlyValue * MyRule["Third", this.RuleCalculateDate].ToInt()) / 100;// +monthlyValue * MyRule["Third", this.RuleCalculateDate].ToInt() / 100;

                this.DoConcept(4505).Value = monthlyValue + percentValue;
            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اضافه كاري بعد از وقت بانك سپه
        /// دو قسمتي كه براي هر قسمت 5 پارامتر براي محاسبه لازم است
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6030(AssignedRule MyRule)
        {
            // 13   كاركرد ناخالص
            // 4002  اضافه كار ساعتي مجاز
            // 4003 اضافه كار غير مجاز
            // 4007 اضافه كار آخر وقت


            if (this.DoConcept(4007).Value > 0)
            {
                var conceptList = new[] { 13, 4002, 4003, 4007, };
                GetLog(MyRule, " Before Execute State:", conceptList);

                #region شنبه تا سه شنبه
                var startTime1 = MyRule["First", this.RuleCalculateDate].ToInt();
                var finishTime1 = MyRule["Second", this.RuleCalculateDate].ToInt();
                var minTime1 = MyRule["Third", this.RuleCalculateDate].ToInt();
                var maxTime1 = MyRule["Fourth", this.RuleCalculateDate].ToInt();
                var scale1 = MyRule["Fifth", this.RuleCalculateDate].ToInt();

                var startTime2 = MyRule["Sixth", this.RuleCalculateDate].ToInt();
                var finishTime2 = MyRule["Seventh", this.RuleCalculateDate].ToInt();
                var minTime2 = MyRule["Eighth", this.RuleCalculateDate].ToInt();
                var maxTime2 = MyRule["Ninth", this.RuleCalculateDate].ToInt();
                var scale2 = MyRule["Tenth", this.RuleCalculateDate].ToInt(); 
                #endregion

                #region چهارشنبه
                // در صورتيكه چهار شنبه بود از 4 پارامتر آخر قانون
                // براي تععين ابتدا و انتهاي دو قسمت اضافه كار بعد از وقت استفاده شود
                if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday)
                {
                    if (MyRule["Eleventh", this.RuleCalculateDate].ToInt() > 0 &&
                        MyRule["Twelfth", this.RuleCalculateDate].ToInt() > 0)
                    {
                        startTime1 = MyRule["Eleventh", this.RuleCalculateDate].ToInt();
                        finishTime1 = MyRule["Twelfth", this.RuleCalculateDate].ToInt();
                    }

                    if (MyRule["Thirteenth", this.RuleCalculateDate].ToInt() > 0 &&
                        MyRule["Fourteenth", this.RuleCalculateDate].ToInt() > 0)
                    {
                        startTime2 = MyRule["Thirteenth", this.RuleCalculateDate].ToInt();
                        finishTime2 = MyRule["Fourteenth", this.RuleCalculateDate].ToInt();
                    }
                } 
                #endregion

                #region پنجشنبه
                // در صورتيكه پنجشنبه بود از 4 پارامتر آخر قانون
                // براي تعين ابتدا و انتهاي دو قسمت اضافه كار بعد از وقت استفاده شود
                if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday)
                {
                    if (MyRule["Fifteenth", this.RuleCalculateDate].ToInt() > 0 &&
                        MyRule["Sixteenth", this.RuleCalculateDate].ToInt() > 0)
                    {
                        startTime1 = MyRule["Fifteenth", this.RuleCalculateDate].ToInt();
                        finishTime1 = MyRule["Sixteenth", this.RuleCalculateDate].ToInt();
                    }

                    if (MyRule["Seventeenth", this.RuleCalculateDate].ToInt() > 0 &&
                        MyRule["Eighteenth", this.RuleCalculateDate].ToInt() > 0)
                    {
                        startTime2 = MyRule["Seventeenth", this.RuleCalculateDate].ToInt();
                        finishTime2 = MyRule["Eighteenth", this.RuleCalculateDate].ToInt();
                    }
                } 
                #endregion

                var shouldAdd = this.DoConcept(4007);

                #region اعمال حضور آخر وقت اجباری
                PairableScndCnpValue missionAfterShift =
                    Operation.Differance(this.DoConcept(2023), this.Person.GetShiftByDate(this.RuleCalculateDate));

                if (missionAfterShift.Pairs.Any())
                {
                    var missionUnionOverTime = Operation.Union(Operation.Union(missionAfterShift, shouldAdd));

                    var from = missionUnionOverTime.Pairs.OrderBy(x => x.From).First().From;
                    var to = this.Person.GetProceedTraficByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).Last().To;

                    var sshouldAddtoBaje = Operation.Intersect(missionUnionOverTime, new PairableScndCnpValuePair(from, to));

                    if (sshouldAddtoBaje.Value > 0)
                    {
                        ((PairableScndCnpValue)shouldAdd).AddPairs(sshouldAddtoBaje.Pairs);
                    }
                }

                #endregion
                
                var partOne = Operation.Intersect(shouldAdd, new PairableScndCnpValuePair(startTime1, finishTime1));
                var partTwo = Operation.Intersect(shouldAdd, new PairableScndCnpValuePair(startTime2, finishTime2));
                
                var totalDiff = Operation.Intersect(
                                                Operation.Differance(Operation.Differance(this.DoConcept(4007), this.DoConcept(4502)), partOne),
                                                Operation.Differance(Operation.Differance(this.DoConcept(4007), this.DoConcept(4502)), partTwo)
                                                );

                if (partOne.Value > 0 || partTwo.Value > 0)
                {
                    #region اضافه كار مجاز
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4007)));
                    ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();

                    #region Part One
                    if (partOne.Value >= minTime1)
                    {
                        var temp = partOne.DecreasePairFromLast(Operation.Minimum(maxTime1, partOne.Value));

                        totalDiff.AppendPairs(Operation.Differance(partOne, temp));

                        ((PairableScndCnpValue)this.DoConcept(4007)).AppendPairs(temp.Pairs);
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairsEx(temp.Pairs);

                        var tmp = temp.PairValues * scale1 / 100;

                        this.DoConcept(4007).Value += tmp;
                        this.DoConcept(4002).Value += tmp;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(partOne.Pairs);
                    }
                    #endregion

                    #region Part Two
                    if (partTwo.Value >= minTime2)
                    {
                        var temp = partTwo.DecreasePairFromLast(Operation.Minimum(maxTime2, partTwo.Value));

                        totalDiff.AppendPairs(Operation.Differance(partTwo, temp));

                        ((PairableScndCnpValue)this.DoConcept(4007)).AppendPairs(temp.Pairs);
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairsEx(temp.Pairs);

                        var tmp = temp.PairValues * scale2 / 100;

                        this.DoConcept(4007).Value += tmp;
                        this.DoConcept(4002).Value += tmp;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(partTwo.Pairs);
                    }
                    #endregion

                    #endregion

                    #region اضافه كار غيرمجاز
                    if (totalDiff.Value > 0)
                        //((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(totalDiff.SortOrderPairs().Pairs);
                        ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(
                            Operation.Differance(this.DoConcept(4003), Operation.Intersect(totalDiff, this.DoConcept(4003))));
                    #endregion

                    this.ReCalculate(13);

                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4007)));
                    //((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(((PairableScndCnpValue)this.DoConcept(4007)).Pairs);
                    ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(
                        Operation.Differance(this.DoConcept(4003), Operation.Intersect(this.DoConcept(4007), this.DoConcept(4003))));
                    ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
                    this.ReCalculate(13);
                }

                GetLog(MyRule, " After Execute State:", conceptList);
            }

        }
     

        /// <summary>
        /// محاسبه مجموع اضافه کار
        /// </summary>
        /// <param name="MyRule"></param>
        public void R6501(AssignedRule MyRule)
        {
            //جمع اضافه کار در مفهوم اضافه کار ویژه نگهداری میشود
            //4032 اضافه کار ویژه
            //4033 اضافه کار ویژه ماهانه

            this.DoConcept(4032).Value = 0;
            if (this.DoConcept(9).Value > 0)
            {

                var conceptList = new[] { 4033 };
                GetLog(MyRule, " Before Execute State:", conceptList);
                this.DoConcept(4033).Value = this.DoConcept(4005).Value + this.DoConcept(5502).Value + this.DoConcept(4505).Value + this.DoConcept(4010).Value;
                GetLog(MyRule, " After Execute State:", conceptList);
            }
        }

        /// <summary>
        /// اضافه كاري قبل از وقت بانك سپه
        /// </summary>
        /// <param name="MyRule"></param>
        public void R6502(AssignedRule MyRule)
        {
            // 13   كاركرد ناخالص
            // 4002  اضافه كار ساعتي مجاز
            // 4003 اضافه كار غير مجاز
            // 4008 اضافه كار اول وقت

            if (this.DoConcept(4008).Value > 0)
            {
                var conceptList = new[] { 13, 4002, 4003, 4008 };
                GetLog(MyRule, " Before Execute State:", conceptList);

                var startTime = MyRule["First", this.RuleCalculateDate].ToInt();
                var finishTime = MyRule["Second", this.RuleCalculateDate].ToInt();
                var minTime = MyRule["Third", this.RuleCalculateDate].ToInt();
                var maxTime = MyRule["Fourth", this.RuleCalculateDate].ToInt();
                var scale = MyRule["Fifth", this.RuleCalculateDate].ToInt();

                var partOne = Operation.Intersect(this.DoConcept(4008),
                                                  new PairableScndCnpValuePair(startTime, finishTime));
                var partOneDiff = Operation.Differance(this.DoConcept(4008),
                                                       new PairableScndCnpValuePair(startTime, finishTime));

                #region اضافه كار مجاز

                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4008)));
                ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();

                #region Part One

                if (partOne.Value >= minTime)
                {
                    var temp = partOne.DecreasePairFromLast(Operation.Minimum(maxTime, partOne.Value));

                    //((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(Operation.Intersect(partOne, temp));

                    ((PairableScndCnpValue)this.DoConcept(4008)).AppendPairs(temp.Pairs);
                    //((PairableScndCnpValue)this.DoConcept(4002)).AppendPairsEx(temp.Pairs);

                    var tmp = temp.PairValues * scale / 100;

                    this.DoConcept(4008).Value += tmp;
                    //this.DoConcept(4002).Value += tmp;
                }
                else
                {
                    //((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(partOne.Pairs);
                }

                #endregion

                #endregion

                #region اضافه كار غيرمجاز
                // بنا بر درخواست تاریخ 07/05/1392
                //if (partOneDiff.Value > 0)
                //    ((PairableScndCnpValue)this.DoConcept(4003))
                //        .AppendPairs(Operation.Intersect(partOneDiff,((PairableScndCnpValue)this.DoConcept(4007))));

                #endregion

                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", conceptList);

            }
        }

        /// <summary>
        /// اضافه كار كشيك براي تعطيل رسمي و غير رسمي
        /// با حداقل ... و حداكثر ... با
        /// ضريب ... درصد محاسبه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R6503(AssignedRule MyRule)
        {
            //4009 اضافه کار ساعتی تعطیل

            if (
                this.DoConcept(4009).Value > 0
                )
            {

                var conceptList = new[] { 4002, 4009, 13 };

                GetLog(MyRule, " Before Execute State:", conceptList);

                //((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4009)));

                if (this.DoConcept(4009).Value > 0)
                {

                    if (this.DoConcept(4009).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                    {
                        PairableScndCnpValue temp;

                        if (this.DoConcept(4009).Value > MyRule["Second", this.RuleCalculateDate].ToInt())
                        {
                            temp = ((PairableScndCnpValue)this.DoConcept(4009)).DecreasePairFromLast(this.DoConcept(4009).Value - MyRule["Second", this.RuleCalculateDate].ToInt());

                            ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(temp);
                        }

                        var tmp = this.DoConcept(4009).Value * MyRule["Third", this.RuleCalculateDate].ToInt() / 100;

                        //((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs((PairableScndCnpValue)this.DoConcept(4009));

                        this.DoConcept(4009).Value += tmp;
                        //this.DoConcept(4002).Value += tmp;

                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(((PairableScndCnpValue)this.DoConcept(4009)).Pairs);
                        ((PairableScndCnpValue)this.DoConcept(4009)).ClearPairs();
                    }
                }

                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", conceptList);
            }

        }

        #endregion

  
        #endregion

    }
}
