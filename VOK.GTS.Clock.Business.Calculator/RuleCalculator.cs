using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.ELE;

namespace GTS.Clock.Business.Calculator
{
    /// <summary>
    /// وزرات امور خارجه
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
      

        #endregion

        #region قوانين کارکرد

        #endregion

        #region قوانين مرخصي

        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري

        /// <summary>
        /// اعمال شیردهی برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public void R266(AssignedRule MyRule)
        {
            var cnpList = new[] { 2, 3020, 3028, 3501 };

            GetLog(MyRule, " Before Execute State:", cnpList);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                // مفدار غیبت مجاز برای شیردهی
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R4) &&
                Utility.ToInteger(this.Person.PersonTASpec.R4) > 0 &&
                // تاریخ شروع غیبت مجاز برای شیردهی
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R5) &&
                // تاریخ پایان غیبت مجاز برای شیردهی
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R6) &&

                this.DoConcept(3028).Value > 0
                )
            {

                if (
                    PersianDateTime.Parse(this.Person.PersonTASpec.R5).GregorianDate.Date <= this.RuleCalculateDate &&
                    this.RuleCalculateDate <= PersianDateTime.Parse(this.Person.PersonTASpec.R6).GregorianDate.Date
                    )
                {
                    var minutes = Utility.ToInteger(this.Person.PersonTASpec.R4);

                    foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From))
                    {
                        if (pair.Value > minutes)
                        {
                            IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                            this.DoConcept(3020).Value += tempPair.Value;

                            var hh = Operation.Differance(this.DoConcept(3028), tempPair);

                            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(hh.Pairs);

                            // غيبت ساعتي مجاز شيردهي
                            ((PairableScndCnpValue)this.DoConcept(3501)).AddPair(tempPair);

                            ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                            pair.To -= minutes;

                            break;
                        }

                        if (pair.Value == minutes)
                        {
                            this.DoConcept(3020).Value += pair.Value;

                            ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                            // غيبت ساعتي مجاز شيردهي
                            ((PairableScndCnpValue)this.DoConcept(3501)).AddPair(pair);

                            ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                            pair.From = pair.To = 0;
                            break;
                        }

                        this.DoConcept(3020).Value += pair.Value;

                        minutes -= pair.Value;

                        ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                        // غيبت ساعتي مجاز شيردهي
                        ((PairableScndCnpValue)this.DoConcept(3501)).AddPair(pair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                        pair.From = pair.To = 0;
                    }

                }
            }

            GetLog(MyRule, " After Execute State:", cnpList);
        }

        /// <summary>
        /// اعمال کسر مهد برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public void R267(AssignedRule MyRule)
        {
            var cnpList = new[] { 2, 3020, 3028 };

            GetLog(MyRule, " Before Execute State:", cnpList);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                // مفدار غیبت مجاز برای مهد
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R1) &&
                Utility.ToInteger(this.Person.PersonTASpec.R1) > 0 &&
                // تاریخ شروع غیبت مجاز برای مهد
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R2) &&
                // تاریخ پایان غیبت مجاز برای مهد
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R3) &&

                this.DoConcept(3028).Value > 0
                )
            {

                if (
                    PersianDateTime.Parse(this.Person.PersonTASpec.R2).GregorianDate.Date <= this.RuleCalculateDate &&
                    this.RuleCalculateDate <= PersianDateTime.Parse(this.Person.PersonTASpec.R3).GregorianDate.Date
                    )
                {
                    var minutes = Utility.ToInteger(this.Person.PersonTASpec.R1);

                    foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From))
                    {
                        if (pair.Value > minutes)
                        {
                            IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                            this.DoConcept(3020).Value += tempPair.Value;

                            var hh =
                                Operation.Differance(this.DoConcept(3028), tempPair);

                            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(hh.Pairs);

                            // غيبت ساعتي مجاز مهد
                            ((PairableScndCnpValue)this.DoConcept(3503)).AddPair(tempPair);

                            ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                            pair.To -= minutes;
                            break;
                        }

                        if (pair.Value == minutes)
                        {
                            this.DoConcept(3020).Value += pair.Value;

                            ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                            // غيبت ساعتي مجاز مهد
                            ((PairableScndCnpValue)this.DoConcept(3503)).AddPair(pair);

                            ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                            pair.From = pair.To = 0;
                            break;
                        }

                        this.DoConcept(3020).Value += pair.Value;

                        minutes -= pair.Value;

                        ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                        // غيبت ساعتي مجاز مهد
                        ((PairableScndCnpValue)this.DoConcept(3503)).AddPair(pair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                        pair.From = pair.To = 0;
                    }

                }
            }
            GetLog(MyRule, " After Execute State:", cnpList);
        }

        /// <summary>
        /// اعمال کسر تقلیل برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public void R268(AssignedRule MyRule)
        {
            var cnpList = new[] { 2, 3, 13, 3020, 3028, 3505, 4002, 4005, 4006, 4007 };

            GetLog(MyRule, " Before Execute State:", cnpList);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                // مفدار غیبت مجاز برای تقلیل
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R7) &&
                Utility.ToInteger(this.Person.PersonTASpec.R7) > 0 &&
                // تاریخ شروع غیبت مجاز برای تقلیل
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R8) &&
                // تاریخ پایان غیبت مجاز برای تقلیل
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R9)
                )
            {

                if (
                    PersianDateTime.Parse(this.Person.PersonTASpec.R8).GregorianDate.Date <= this.RuleCalculateDate &&
                    this.RuleCalculateDate <= PersianDateTime.Parse(this.Person.PersonTASpec.R9).GregorianDate.Date
                    )
                {
                    var minutes = Utility.ToInteger(this.Person.PersonTASpec.R7);

                    if (MyRule["First", this.RuleCalculateDate].ToInt() == 1)
                    {
                        #region اعمال روی غیبت
                        if (this.DoConcept(3028).Value > 0)
                        {
                            while (this.DoConcept(3028).Value > 0 && minutes > 0)
                            {
                                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From))
                                {
                                    if (pair.Value > minutes)
                                    {
                                        IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                                        this.DoConcept(3020).Value += tempPair.Value;

                                        var hh = Operation.Differance(this.DoConcept(3028), tempPair);

                                        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(hh.Pairs);

                                        // غيبت ساعتي مجاز تقليل
                                        ((PairableScndCnpValue)this.DoConcept(3505)).AddPair(tempPair);

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
                                        ((PairableScndCnpValue)this.DoConcept(3505)).AddPair(pair);

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
                                        ((PairableScndCnpValue)this.DoConcept(3505)).AddPair(pair);

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
                            ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(minutes);

                            var bb = new BasePair(
                                  ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To - minutes,
                                  ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To
                                  );

                            // غيبت ساعتي مجاز تقليل
                            ((PairableScndCnpValue)this.DoConcept(3505)).AddPair(bb);

                            this.ReCalculate(3, 13, 4005, 4006, 4007);
                        }
                        #endregion
                    }


                }
            }
            GetLog(MyRule, " After Execute State:", cnpList);
        }

        #endregion

        #region قوانين اضافه کاري

        #endregion

        #region قوانين عمومي

        /// <summary>
        /// وظیفه اجرای مفاهیم ماهانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R97(AssignedRule MyRule)
        {
            base.R97(MyRule);

            //this.DoConcept(5502);// ویژه جانبازی ماهانه
            this.DoConcept(3005);//غيبت روزانه ماهانه
            this.DoConcept(3502);//غیبت مجاز شیردهی ماهانه
            this.DoConcept(3504);//غیبت مجاز مهد ماهانه
            this.DoConcept(3506);//غیبت مجاز تقليل ماهانه
            //this.DoConcept(4504);//باجه عصر
            //this.DoConcept(4503);//باجه عصر
        }

        #endregion

        #region Concept Init


        #endregion


        #endregion
    }
}
