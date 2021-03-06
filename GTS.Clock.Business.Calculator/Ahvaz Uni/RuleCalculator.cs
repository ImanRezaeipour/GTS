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


        /// <summary>
        /// کد 17 :  ایاب و ذهاب
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R1008(AssignedRule MyRule)
        {
            //5019 ایاب ذهاب ماهانه
            //3 کارکرد ناخالص ماهانه
            //10 کارکرد لازم ماهانه
            //this.DoConcept(1);
            
            if (this.DoConcept(5021).Value > 0)
            {
                int maxMonay = MyRule["first", this.RuleCalculateDate].ToInt();
                GetLog(MyRule, DebugRuleState.Before,5019);
                if (this.DoConcept(3).Value >= this.DoConcept(10).Value)
                {
                    this.DoConcept(5019).Value = maxMonay;
                }
                else
                {
                    double m = ((double)this.DoConcept(3).Value * (double)maxMonay);
                    double result = m / this.DoConcept(10).Value;
                    this.DoConcept(5019).Value = Convert.ToInt32(result);
                }
            }
            GetLog(MyRule, DebugRuleState.After , 5019);
        }

        #endregion

        #region قوانين کارکرد

       /// <summary>
        /// کد 33 :  ایام کاری مهم نیست و مجموع کارکرد هر ماه طبق جدول حساب شود
       /// </summary>
       /// <param name="MyRule"></param>
        public override void R2006(AssignedRule MyRule)
        {
            //3 کارکردناخالص ماهانه
            //6 کارکرد لازم
            //کارکردخالص ساعتي ماهانه 8
            //9 حضور ماهانه
            //کارکردلازم ماهانه 10
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4010 اضافه کارساعتي تعطيل ماهانه
            //1001 مرخصی در روز

            //1006 مرخصي استحقاقي روزانه ماهانه
            //1011 مرخصي استحقاقي ساعتي ماهانه
            //1017 مرخصي استعلاجي روزانه ماهانه
            //1097 مرخصی با حقوق ماهانه
            //1043 مرخصی با حقوق ساعتی ماهانه
            //501 حق التدریس
            //2006 مجموع ماموریت روزانه
            //2007 مجموع ماموریت ساعتی ماهانه

            GetLog(MyRule, DebugRuleState.Before,13, 3020, 3028, 4002, 4003);
            if (this.DoConcept(13).Value == 0)
            {
                this.DoConcept(13).Value += this.DoConcept(1).Value;
            }          
            if (this.DoConcept(5021).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before, 3, 8, 10, 13, 3034, 4005, 4006, 4010, 501, 502);

                int lazem = this.DoConcept(1001).Value;//بعلت نداشتن کارکرد لازم ماهانه از این مفهوم استفاده میشود
                int karkerd = this.DoConcept(9).Value + this.DoConcept(1006).Value * lazem
                                                      + this.DoConcept(1017).Value * lazem
                                                      + this.DoConcept(1097).Value * lazem
                                                      + this.DoConcept(2006).Value * lazem
                                                      + this.DoConcept(1011).Value
                                                      + this.DoConcept(2007).Value;

                DateTime monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(this.RuleCalculateDate).Year, Utility.ToPersianDateTime(this.RuleCalculateDate).Month, 1));
                DateTime endOfMonth = Utility.GetEndOfPersianMonth(this.RuleCalculateDate);

              
                var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "hagholtadris", this.RuleCalculateDate);
                int haghOTadris = 0;
                if (personParam != null && !Utility.IsEmpty(personParam.Value))
                {
                    haghOTadris = Utility.ToInteger(personParam.Value) * 60;
                }
                int daterangeORder = this.GetDateRange(3, this.ConceptCalculateDate).DateRangeOrder;
                int monthlyLazem = MyRule[daterangeORder.ToString() + "th", this.RuleCalculateDate].ToInt();
                this.DoConcept(10).Value = monthlyLazem * HourMin;
                if (this.DoConcept(3).Value >= haghOTadris)//(karkerd >= haghOTadris)
                {
                    if (haghOTadris > 0)
                    {
                        this.DoConcept(3).Value -= haghOTadris;
                        //karkerd -= haghOTadris;
                        this.DoConcept(501).Value = 0;
                        this.DoConcept(502).Value = haghOTadris;
                    }
                }
                else 
                {
                    //this.DoConcept(501).Value = karkerd;
                    //karkerd = 0;
                    this.DoConcept(501).Value = this.DoConcept(3).Value;
                    this.DoConcept(3).Value = 0;
                }
                if (this.DoConcept(3).Value >=  this.DoConcept(10).Value)//(karkerd >= monthlyLazem * HourMin)
                {
                    this.DoConcept(8).Value = monthlyLazem * HourMin;
                    //this.DoConcept(4005).Value = karkerd - this.DoConcept(10).Value;
                    this.DoConcept(4005).Value = this.DoConcept(3).Value - this.DoConcept(10).Value;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(3034).Value = 0;
                    this.DoConcept(3).Value = this.DoConcept(8).Value + this.DoConcept(4005).Value;
                }
                else//کسر کار
                {
                    //this.DoConcept(1043).Value += (monthlyLazem * HourMin) - karkerd;
                    this.DoConcept(3034).Value = this.DoConcept(10).Value-this.DoConcept(3).Value;
                    this.DoConcept(4005).Value = 0;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(4010).Value = 0;

                    //this.DoConcept(8).Value = monthlyLazem * HourMin;
                    //this.DoConcept(3).Value = monthlyLazem * HourMin;
                }
                GetLog(MyRule, DebugRuleState.After , 3, 8, 10, 13, 3034, 4005, 4006, 4010, 501, 502);
            }

           
            this.DoConcept(3020).Value = 0;
            this.DoConcept(3028).Value = 0;
            this.DoConcept(4002).Value = 0;
            this.DoConcept(4003).Value = 0;
            GetLog(MyRule, DebugRuleState.After ,13, 3020, 3028, 4002, 4003);
        }

       /// <summary>
       /// اعمال کارکرد روز تعطیل رسمی بصورت روزانه
       /// </summary>
       /// <param name="MyRule"></param>
       public void R2501(AssignedRule MyRule)
       {
           //1 تعطیل رسمی
           //3 کارکرد ناخالص ماهانه
           //5 کارکرد خالص ماهانه
           GetLog(MyRule, DebugRuleState.Before, 13);
           if (this.EngEnvironment.HasCalendar(this.RuleCalculateDate, "2")) 
           {
               int karkerd = MyRule["first", this.RuleCalculateDate].ToInt();
               this.DoConcept(13).Value = karkerd;
           }
           if (this.DoConcept(1090).Value >= 1)
           {
               this.DoConcept(13).Value = MyRule["first", this.RuleCalculateDate].ToInt();             
           }
           GetLog(MyRule, DebugRuleState.After , 13);
       }

       /// <summary>
       /// اعمال حق التدریس برای برخی از پرسنل
       /// </summary>
       /// <param name="MyRule"></param>
       public void R2502(AssignedRule MyRule)
       {
           //1082 مجموع انواع مرخصی ساعتی
           //2023 مفهوم مجموع ماموريت ساعتي
           var conceptList = new[] { 2, 3, 13, 3020, 3028, 3044, 4002, 4005, 4006, 4007 };
           GetLog(MyRule, " Before Execute State:", conceptList);
           var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "hagholtadris", this.RuleCalculateDate);

           /*  if (personParam != null && !Utility.IsEmpty(personParam.Value) && this.DoConcept(9).Value > 0)
             {               
                 int value = Utility.ToInteger(personParam.Value) * 60;
                 this.DoConcept(501).Value = value;
                 if (this.DoConcept(9).Value >= value)
                 {
                     this.DoConcept(9).Value -= value;
                     this.DoConcept(3).Value -= value;
                 }
                 else
                 {
                     this.DoConcept(8).Value = 0;
                     this.DoConcept(3).Value = 0;
                 }
             }*/
           GetLog(MyRule, " After Execute State:", conceptList);
       }

       /// <summary>
       /// اعمال تعطیلات خاص بجز پنجشنبه و جمعه
       /// هیئت علمی
       /// </summary>
       /// <param name="MyRule"></param>
       public void R2503(AssignedRule MyRule)
       {
           //1 تعطیل رسمی
           //3 کارکرد ناخالص ماهانه

           if (this.RuleCalculateDate.DayOfWeek != DayOfWeek.Friday && this.EngEnvironment.HasCalendar(this.RuleCalculateDate, "1"))
           {
               GetLog(MyRule, DebugRuleState.Before, 13);
               int karkerdEzafi = MyRule["First", this.RuleCalculateDate].ToInt();
               this.DoConcept(13).Value += karkerdEzafi;
               GetLog(MyRule, DebugRuleState.After , 13);
           }
          /*
           if (this.DoConcept(9).Value > 0)
           {
               

               DateRange dateRange = this.GetDateRange(3, this.RuleCalculateDate);
               if (dateRange != null)
               {
                   DateTime monthStart = dateRange.FromDate;// Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(this.RuleCalculateDate).Year, Utility.ToPersianDateTime(this.RuleCalculateDate).Month, 1));
                   DateTime endOfMonth = dateRange.ToDate;// Utility.GetEndOfPersianMonth(this.RuleCalculateDate);

                   int holiday = 0;
                   for (DateTime date = monthStart; date <= endOfMonth; date = date.AddDays(1))
                   {
                       if (date.DayOfWeek != DayOfWeek.Friday && this.EngEnvironment.HasCalendar(date, "1"))
                       {
                           holiday++;
                       }
                   }
                   if (holiday > 0)
                   {
                       GetLog(MyRule, " Before Execute State:", 3);

                       this.DoConcept(3).Value += holiday * karkerdEzafi;

                       GetLog(MyRule, " After Execute State:", 3);
                   }
               }
           }*/
       }

       /// <summary>
       /// کارکرد روزانه ماهانه
       /// هیئت علمی
       /// </summary>
       /// <param name="MyRule"></param>
       public void R2504(AssignedRule MyRule)
       {
           //1 تعطیل رسمی
           //3 کارکرد ناخالص ماهانه
           //5 کارکرد خالص ماهانه
           
           if (this.DoConcept(5021).Value > 0)
           {
               DateRange dateRange = this.GetDateRange(3, this.RuleCalculateDate);
               if (dateRange != null)
               {
                   DateTime monthStart = dateRange.FromDate;// Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(this.RuleCalculateDate).Year, Utility.ToPersianDateTime(this.RuleCalculateDate).Month, 1));
                   DateTime endOfMonth = dateRange.ToDate;// Utility.GetEndOfPersianMonth(this.RuleCalculateDate);


                   int presence = 0;
                   for (DateTime date = monthStart; date <= endOfMonth; date = date.AddDays(1))
                   {
                       if (this.DoConcept(1, date).Value > 0)
                       {
                           presence++;
                       }
                   }
                   if (presence > 0)
                   {
                       GetLog(MyRule, DebugRuleState.Before, 5);

                       this.DoConcept(5).Value = presence;

                       GetLog(MyRule, DebugRuleState.After , 5);
                   }
               }
           }

       }

     
        #endregion

        #region قوانين مرخصي
     
        #endregion

        #region قوانين ماموريت
       public override void R4001(AssignedRule MyRule)
       {
           //2023 مجموع ماموريت ساعتي
           //4 کارکردخالص روزانه
           //13 کارکرد ناخالص ساعتی
           //ماموريت خالص روزانه 2003
           //ماموريت درروز 2001
           //4002 اضافه کار مجاز
           //4003 اضافه کار غیرمجاز

           GetLog(MyRule, DebugRuleState.Before, 13);

           #region کارکرد خالص
           GetLog(MyRule, " Before Execute State:", 2, 4, 13, 2001, 4002);
           if (MyRule["First", this.RuleCalculateDate].ToInt() == 1 &&
               !EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
           {
               if (this.DoConcept(2005).Value == 1 && MyRule["Third", this.RuleCalculateDate].ToInt() == 1)
               {
                   this.DoConcept(4).Value = 1;
                   if (this.DoConcept(6).Value > 0)
                   {
                       this.DoConcept(2).Value = this.DoConcept(6).Value;
                   }
                   if (this.DoConcept(2).Value == 0)
                   {
                       this.DoConcept(4).Value = 0;
                   }
               }
               else if (MyRule["Forth", this.RuleCalculateDate].ToInt() == 1)
               {
                   int value = 0;
                   value = Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(2023)).Value;
                   if (value > 0)
                   {
                       if (this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType == ShiftTypesEnum.WORK)
                       {
                           this.DoConcept(2).Value += value;
                       }
                       else if (this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType == ShiftTypesEnum.OVERTIME)
                       {
                           ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(value);
                       }
                   }
               }
               this.ReCalculate(13);
           }
           GetLog(MyRule, " After Execute State:", 2, 4, 13, 2001, 4002);
           #endregion

           //اگر قانون 123 استفاده نشده است انگاه این قانون اجرا شود
           //جمله بالا اشتباه است زیرا در قانون بالا فقط داخل شیفت محاسبه میشود
           if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1 &&
               MyRule["First", this.RuleCalculateDate].ToInt() == 0)
           {
               if (this.DoConcept(2005).Value == 1 && MyRule["Third", this.RuleCalculateDate].ToInt() == 1)
               {
                   if (this.DoConcept(6).Value > 0)
                   {
                       this.DoConcept(13).Value += this.DoConcept(6).Value;
                   }
                   else
                   {
                       this.DoConcept(13).Value += this.DoConcept(7).Value;
                   }
               }
               else if (MyRule["Forth", this.RuleCalculateDate].ToInt() == 1)
               {
                   int value = 0;

                   value = this.DoConcept(2023).Value;

                   if (value > 0)
                   {
                       this.DoConcept(13).Value += value;
                   }
               }
           }
           else if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1)
           {//داخل شبفت در 123 محاسبه شده است
               if (this.DoConcept(2005).Value == 0)
               {
                   int value = 0;
                   // خارج شیفت به شرطی که قبلا در اضافه کار محاسبه نشده باشد
                   PairableScndCnpValue pairableScndCnpValue1 = Operation.Differance(this.DoConcept(2023), this.Person.GetShiftByDate(this.RuleCalculateDate));
                   PairableScndCnpValue pairableScndCnpValue2 = Operation.Differance(pairableScndCnpValue1, this.DoConcept(4002));
                   value = Operation.Differance(pairableScndCnpValue2, this.DoConcept(4003)).Value;
                   if (value > 0)
                   {
                       this.DoConcept(13).Value += value;
                   }
               }
           }
           GetLog(MyRule, DebugRuleState.After , 13);
       }
        #endregion

        #region قوانين کم کاري

       /// <summary>
       /// محاسبه تعداد تاخیر و تعجیل
       /// </summary>
       /// <param name="MyRule"></param>
       public virtual void R5501(AssignedRule MyRule)
       {
           //تاخیر ساعتی غیرمجاز 3029  
           //تعجیل ساعتی غیرمجاز 3030
           //3501 تعداد تاخیر و تعجیل 
           //3502 تعداد تاخیر و تعجیل ماهانه 

           GetLog(MyRule, DebugRuleState.Before, 3501,3502);
           if (this.DoConcept(3004).Value == 0)
           {
               if (this.DoConcept(3029).Value > 0)
               {
                   this.DoConcept(3501).Value++;
               }
               if (this.DoConcept(3030).Value > 0)
               {
                   this.DoConcept(3501).Value++;
               }
               if (this.DoConcept(3501).Value > 0)
               {
                   this.ReCalculate(3502);
               }
           }
           GetLog(MyRule, DebugRuleState.After , 3501, 3502);
       }

        #endregion

        #region قوانين اضافه کاري

        #endregion

        #region قوانين عمومي

        #endregion

        #region Concept Init


        #endregion


        #endregion
     
    }
}
