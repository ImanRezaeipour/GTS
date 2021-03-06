using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.ELE;
using GTS.Clock.Business.AppSettings;

namespace GTS.Clock.Business.Calculator
{
    public abstract class GeneralRuleCalculator : ObjectCalculator, IDisposable
    {
        #region Constants
        /// <summary>
        /// هر جا در قوانين نياز بود که پارامتري استفاده شود از اينجا استفاده ميشود و در آينده جايگزين ميگردد
        /// </summary>
        public enum Parameters
        {
            FirstParameter = 1,
            SecondParameter = 2,
            ThirdParameter = 3,
            FourthParameter = 4,
            FifthParameter = 5,
            SixthParameter = 6,
            SeventhParameter = 7,
            EighthParameter = 8,
            NinthParameter = 9,
            TenthParameter = 10,
            EleventhParameter = 11,
            TwelfthParameter = 12
        };
        protected const int HourMin = 60;
        const string SettingsLoaded = "RuleSettingsLoaded";

        #endregion

        #region Vairables

        protected static ApplicationSettings appSet;
        protected GeneralConceptCalculator CnpCalculator;
        protected GTSEngineLogger gtsRuleLogger;
        protected bool logLock = true;

        #endregion

        #region Constructors
        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر اشياء
        /// </summary>
        /// <param name="Person">پرسنلي که محاسبات براي او در حال انجام است</param>
        /// <param name="CategorisedRule">قانوني که منجر به فراخواني مفاهيم از کلاس "محاسبه گر قانون" خواهد شد</param>
        /// <param name="CalculateDate">تاريخ انجام محاسبات</param>
        public GeneralRuleCalculator(IEngineEnvironment engineEnvironment, GeneralConceptCalculator conceptCalculator)
            : base(engineEnvironment)
        {
            this.CnpCalculator = conceptCalculator;
            this.logLock = !GTSAppSettings.RuleDebug;
        }
        #endregion

        #region Properties

        public bool LogLock
        {
            get { return logLock; }
            set { logLock = value; }
        }

        public static ApplicationSettings GTSAppSettings
        {
            get
            {
                if (appSet == null || System.Runtime.Remoting.Messaging.CallContext.GetData(SettingsLoaded) == null)
                {
                    appSet = AppSettings.BApplicationSettings.CurrentApplicationSettings;
                }
                return appSet;
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// فراخواني متد قانون مشخص شده در خصوصيت "قانون دسته بندي شده" را برعهده دارد
        /// </summary>
        public void ExecuteRule()
        {
            try
            {
                base.GetType().InvokeMember(this.MethodName(this.AssignedRule.IdentifierCode), BindingFlags.InvokeMethod, null, this, new object[1] { this.AssignedRule });
            }
            catch (MissingMethodException ex)
            {
                throw new ExecuteRuleException(String.Format("تابع {0} معرف قانون {1} موجود نيست", this.MethodName(this.AssignedRule.IdentifierCode), this.AssignedRule.Name),
                                         ExceptionType.CRASH,
                                         String.Format("ConceptCalculatro.ExecuteRule({0}.{1})", this.AssignedRule.Name, this.MethodName(this.AssignedRule.IdentifierCode)),
                                         ex);
            }
            catch (TargetInvocationException ex)
            {

                if (ex.InnerException != null)
                {
                    string msg = String.Format("Error On Rule {0} on {1} , Message : {2} ", this.MethodName(this.AssignedRule.IdentifierCode), Utility.ToPersianDate(this.RuleCalculateDate), Utility.GetExecptionMessage(ex.InnerException));
                    ExecuteRuleException exp = new ExecuteRuleException(msg, ExceptionType.CRASH, "ExecuteRule.InvokeMember()", ex);
                    if (ex.InnerException != null && ex.InnerException is BaseException && ((BaseException)ex.InnerException).InsertedLog)
                    {
                        exp.InsertedLog = true;
                    }
                    throw exp;
                }
                else
                {
                    throw new ExecuteRuleException(String.Format(".خطا در اجراي قانون {0}، پیغام: {1}", this.MethodName(this.AssignedRule.IdentifierCode), Utility.GetExecptionMessage(ex)),
                                                   ExceptionType.CRASH,
                                                    "ExecuteRule.InvokeMember()",
                                                    ex);
                }
            }
            catch (Exception ex)
            {
                throw new ExecuteRuleException(String.Format(".خطا در اجراي قانون {0}، پیغام: {1}", this.MethodName(this.AssignedRule.IdentifierCode), Utility.GetExecptionMessage(ex)),
                                                    ExceptionType.CRASH,
                                                     "ExecuteRule.InvokeMember()",
                                                     ex);
            }

        }

        public string MethodName(decimal IdentitierCode)
        {
            return String.Format("R{0}", IdentitierCode.ToString());
        }

        public virtual void ReCalculate(decimal IdentifierCode)
        {
            this.ReCalculate(IdentifierCode, this.RuleCalculateDate);
        }

        public virtual void ReCalculate(params decimal[] IdentifiersCode)
        {
            for (int i = 0; i < IdentifiersCode.Length; i++)
            {
                this.ReCalculate(IdentifiersCode[i], this.RuleCalculateDate);
            }
        }

        public virtual void ReCalculate(decimal IdentifierCode, DateTime CalculateDate)
        {
            if (CalculateDate >= this.MinAssgnRuleDate)
            {
                this.CnpCalculator.ReCalculate(IdentifierCode, CalculateDate);
            }
        }

        protected BaseScndCnpValue GetConcept(decimal IdentifierCode, DateTime CalculateDate)
        {
            if (CalculateDate >= this.MinAssgnRuleDate)
            {
                return this.CnpCalculator.GetConcept(IdentifierCode, CalculateDate);
            }
            return new PairableScndCnpValue();
        }

        public BaseScndCnpValue DoConcept(decimal IdentifierCode)
        {
            return this.CnpCalculator.DoConcept(IdentifierCode, this.RuleCalculateDate);
        }

        public BaseScndCnpValue DoConcept(decimal IdentifierCode, DateTime CalculateDate)
        {
            if (CalculateDate >= this.MinAssgnRuleDate)
            {
                return this.CnpCalculator.DoConcept(IdentifierCode, CalculateDate);
            }
            return new PairableScndCnpValue();
        }

        /// <summary>
        /// رینج یک مفهوم را برمیگرداند
        /// </summary>
        /// <param name="IdentifierCode"></param>
        /// <param name="CalculateDate"></param>
        /// <returns></returns>
        public DateRange GetDateRange(decimal IdentifierCode, DateTime CalculateDate)
        {
            SecondaryConcept ScndCnp = EngEnvironment.GetConcept(IdentifierCode);
            DateRange daterange = this.Person.GetPeriodicScndCnpRange(ScndCnp, CalculateDate);
            return daterange;
        }

        public void GetLog(AssignedRule rule, string Message, params int[] ConceptsId)
        {
            if (!logLock)
            {
                Array.Sort(ConceptsId);
                string msg = String.Format("Barcode:{0} - Calculation Date:{1} - Order:{2} - {3}-{4}", this.Person.BarCode, this.RuleCalculateDate.ToString(), rule.Order, this.MethodName(rule.IdentifierCode), Message);
                foreach (int cnpId in ConceptsId)
                {
                    msg += String.Format("  --- C{0} : {1} ---  ", cnpId.ToString(), this.DoConcept(cnpId).ExValue.ToString());
                }
                if (gtsRuleLogger == null)
                    gtsRuleLogger = new GTSEngineLogger();
                gtsRuleLogger.Logger.Info(msg);
                gtsRuleLogger.Flush();
            }
        }

        
        #endregion

        #region Defined Methods

        #region Absolout

        /// <summary>قانون متفرقه 1-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يک-1 درنظر گرفته شده است</remarks>
        /// متفرقه 1-1: کليه غيبتهاي ساعتي به مرخصي تبديل شود
        public virtual void R1(AssignedRule MyRule)
        {
            //1003 مرخصي استحقاقي ساعتي
            //110 مفهوم مرخصي بايد به کارکرد خالص اضافه شود
            //2 مفهوم کارکردخالص ساعتي
            //3028 غيبت ساعتي غيرمجاز
            //1088 مفهوم مرخصی به کارکرد خالص اضافه شود
            GetLog(MyRule, " Before Execute State:", 1003, 3028);
            int remain = this.Person.GetRemainLeave(this.RuleCalculateDate);
            if (remain > 0)
            {
                if (remain >= this.DoConcept(3028).Value)
                {
                    this.DoConcept(1003).Value += this.DoConcept(3028).Value;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, this.DoConcept(3028).Value, null);
                    this.DoConcept(3028).Value = 0;
                }
                else
                {
                    this.DoConcept(1003).Value += remain;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, remain, null);
                    this.DoConcept(3028).Value -= remain;
                }
                //اولویت فانون 79 قبل از این قانون است
                if (this.DoConcept(1088).Value == 1)
                {
                    this.DoConcept(2).Value += this.DoConcept(1003).Value;
                    this.DoConcept(13).Value += this.DoConcept(1003).Value;
                }
            }
            GetLog(MyRule, " After Execute State:", 1003, 3028);
        }

        /// <summary>قانون متفرقه 1-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دو-2 درنظر گرفته شده است</remarks>
        public virtual void R2(AssignedRule MyRule)
        {
            //1005 مرخصي استحقاقي روزانه
            //3004 غيبت روزانه
            //1001 مرخصي درروز

            GetLog(MyRule, " Before Execute State:", 1005, 3004);
            int remain = this.Person.GetRemainLeave(this.RuleCalculateDate);
            if (remain >= this.DoConcept(3004).Value * this.DoConcept(1001).Value)
            {
                if (this.DoConcept(3004).Value > 0)
                {
                    this.DoConcept(1005).Value = this.DoConcept(3004).Value;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, this.DoConcept(3004).Value * this.DoConcept(1001).Value, null);
                    this.DoConcept(3004).Value = 0;
                }
            }
            else if (this.DoConcept(1098).Value >0)
            {
                if (this.DoConcept(3004).Value > 0)
                {
                    this.DoConcept(1005).Value = this.DoConcept(3004).Value;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, this.DoConcept(3004).Value * this.DoConcept(1001).Value, null);
                    this.DoConcept(3004).Value = 0;
                }
            }
            GetLog(MyRule, " After Execute State:", 1005, 3004);
        }
     
        /// <summary>قانون متفرقه 2-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهار-4 درنظر گرفته شده است</remarks>
        public virtual void R4(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 2, 3028, 3004);
            PairableScndCnpValue.AppendPairsToScndCnpValue(this.DoConcept(3028),
                                                             this.DoConcept(2));
            this.DoConcept(3028).Value = 0;
            if (this.DoConcept(3004).Value > 0)
            {
                this.DoConcept(2).Value += this.DoConcept(6).Value;
                this.DoConcept(3004).Value = 0;
            }
            this.ReCalculate(13);

            GetLog(MyRule, " After Execute State:", 2, 3028, 3004);
        }

        /// <summary>قانون متفرقه 7-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي ده-10 درنظر گرفته شده است</remarks>
        public virtual void R10(AssignedRule MyRule)
        {
            //1003 مرخصی ساعتی
            //4013 اضافه کارساعتی جمعه
            GetLog(MyRule, " Before Execute State:", 1003);
            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday)
            {
                this.DoConcept(1003).Value -= this.DoConcept(4013).Value;
                this.Person.AddRemainLeave(this.RuleCalculateDate, this.DoConcept(4013).Value);
            }
            GetLog(MyRule, " After Execute State:", 1003);
        }

        /// <summary>قانون متفرقه 7-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يازده-11 درنظر گرفته شده است</remarks>
        public virtual void R11(AssignedRule MyRule)
        {
            //1003 مرخصی ساعتی
            //4002 اضافه کار ساعتی
            GetLog(MyRule, " Before Execute State:", 1003);
            if (!(this.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday) &&
                (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2")) &&
                this.DoConcept(4002).Value > 0)
            {
                this.DoConcept(1003).Value -= this.DoConcept(4002).Value;
                this.Person.AddRemainLeave(this.RuleCalculateDate, this.DoConcept(4002).Value);
            }
            GetLog(MyRule, " After Execute State:", 1003);
        }

        /// <summary>قانون متفرقه 8-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دوازده-12 درنظر گرفته شده است</remarks>
        /// <!--در صورتيکه منظور قانون غيبتهاي روزانه بوده بايد متن اين قانون تغيير کند-->
        public virtual void R12(AssignedRule MyRule)
        {
            throw new NotImplementedException("مشکل دارد");
            //غيبت روزانه ماهانه 3005
            //اضافه کار ساعتي ماهانه 4005
            //غیبت در روز 3019
            //8 کارکردخالص ساعتي ماهانه
            //5 کارکردخالص روزانه ماهانه
            GetLog(MyRule, " Before Execute State:", 3005, 5, 8, 4005);
            int t = this.DoConcept(3019).Value;
            if (this.DoConcept(4005).Value > 0 && this.DoConcept(3005).Value > 0 && t > 0)
            {
                int tmp = Operation.Minimum(this.DoConcept(4005).Value,
                                                this.DoConcept(3005).Value * t);
                this.DoConcept(3005).Value -= tmp;
                this.DoConcept(5).Value += tmp;
                this.DoConcept(8).Value += tmp;
                this.DoConcept(4005).Value -= tmp;
            }
            GetLog(MyRule, " After Execute State:", 3005, 5, 8, 4005);
        }

        /// <summary>متفرقه 10-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شانزده-16 درنظر گرفته شده است</remarks>
        public virtual void R16(AssignedRule MyRule)
        {
            //تعجيل ساعتي ماهانه 49
            //مرخصي استحقاقي ساعتي ماهانه 1011
            //غيبت ساعتي ماهانه 3006
            GetLog(MyRule, " Before Execute State:", 3034, 3010, 1011);
            int tmp = Operation.Minimum(this.DoConcept(3010).Value,
                                       MyRule["First", this.RuleCalculateDate].ToInt(), this.Person.GetRemainLeave(this.RuleCalculateDate));

            if (tmp > 0)
            {
                this.DoConcept(3034).Value -= tmp;
                this.DoConcept(3010).Value -= tmp;
                this.DoConcept(1011).Value += tmp;
                if (this.DoConcept(3034).Value < 0)
                {
                    this.DoConcept(3034).Value = 0;
                }
            }

            GetLog(MyRule, " After Execute State:", 3034, 3010, 1011);
        }

        /// <summary>متفرقه 15-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هيجده-18 درنظر گرفته شده است</remarks>
        public virtual void R18(AssignedRule MyRule)
        {
            //تقويم تعطيل رسمي 1
            //حق غذا 5004
            GetLog(MyRule, " Before Execute State:", 5004);
            this.DoConcept(5004).Value = 0;
            if (MyRule["First", this.RuleCalculateDate].ToInt() == 1 && this.DoConcept(1).Value > 0)
            {
                this.DoConcept(5004).Value = 1;
            }
            if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1 && EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
            {
                this.DoConcept(5004).Value = 1;
            }
            if (MyRule["Third", this.RuleCalculateDate].ToInt() == 1 && (this.DoConcept(2005).Value > 0 || this.DoConcept(2008).Value > 0))
            {
                this.DoConcept(5004).Value = 1;
            }
            else if (MyRule["Fourth", this.RuleCalculateDate].ToInt() == 1 && this.Person.GetProceedTraficByDate(this.RuleCalculateDate).PairCount > 0)
            {
                this.DoConcept(5004).Value = 1;
            }
            GetLog(MyRule, " After Execute State:", 5004);
        }

        /// <summary>اضافه کار بهمراه کارکرد ناخالص محاسبه نشود</summary>
        /// <remarks></remarks>
        public virtual void R20(AssignedRule MyRule)
        {
            //13  کارکرد ناخالص
            //اضافه کار 4002          

            GetLog(MyRule, " Before Execute State:", 4002, 13);
            this.DoConcept(13);
            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
            GetLog(MyRule, " After Execute State:", 4002, 13);
        }

        /// <summary>1-20 قانون متفرقه</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و دو-22 درنظر گرفته شده است</remarks>
        public virtual void R22(AssignedRule MyRule)
        {
            //21 حضور ویژه
            GetLog(MyRule, " Before Execute State:", 21);
            ((PairableScndCnpValue)this.DoConcept(21)).AddPairs(Operation.Intersect(this.DoConcept(1), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            GetLog(MyRule, " After Execute State:", 21);
        }

        /// <summary>1-21 قانون متفرقه</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي یکصدو ده-110 درنظر گرفته شده است</remarks>
        public virtual void R110(AssignedRule MyRule)
        {
            //4002 اضافه کارمجاز ساعتي
            //4003 اضافه کاری غیر مجاز
            GetLog(MyRule, " Before Execute State:", 4002, 4003);

            ((PairableScndCnpValue)this.DoConcept(4003))
                                            .AddPairs(Operation.Intersect(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            ((PairableScndCnpValue)this.DoConcept(4002))
                                            .AddPairs(Operation.Differance(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));

            GetLog(MyRule, " After Execute State:", 4002, 4003);
        }

        /// <summary>1-22 قانون متفرقه</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي یکصدو یازده -111 درنظر گرفته شده است</remarks>
        public virtual void R111(AssignedRule MyRule)
        {
            //4002 اضافه کارمجاز ساعتي
            //4003 اضافه کاری غیر مجاز
            GetLog(MyRule, " Before Execute State:", 4002, 4003);

            ((PairableScndCnpValue)this.DoConcept(4003))
                                            .AddPairs(Operation.Intersect(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            ((PairableScndCnpValue)this.DoConcept(4002))
                                            .AddPairs(Operation.Differance(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));

            GetLog(MyRule, " After Execute State:", 4002, 4003);

        }

        /// <summary>قانون مقداردهی C16</summary>        
        public virtual void R222(AssignedRule MyRule)
        {
            throw new Exception("بوسیله قانون 221 فعال می گردد");

            this.DoConcept(16).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون کارکرد 2-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و شش-26 درنظر گرفته شده است</remarks>
        public virtual void R26(AssignedRule MyRule)
        {
            throw new Exception("بوسیه پارامتر در قوانین 203 اعمال گردید");

            //25 روزهای غیر کاری جزو کارکرد حساب شود
            GetLog(MyRule, " After Execute State:", 25);
            this.DoConcept(25).Value = 1;
            GetLog(MyRule, " After Execute State:", 25);
        }

        /// <summary>قانون کارکرد 4-2روزهاي کاري بیشتر از (ساعت کارکرد در روز) به تناسب روز کاری حساب شود</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي-30 درنظر گرفته شده است</remarks>
        public virtual void R30(AssignedRule MyRule)
        {
            //6 کارکردلازم
            //7 کارکرددرروز
            //4 کارکردخالص روزانه
            //3004 غيبت روزانه

            GetLog(MyRule, " Before Execute State:", 4);
            if (this.DoConcept(7).Value == 0)
            {
                this.DoConcept(4).Value = 0;
            }
            else
            {
                this.DoConcept(4).Value = this.DoConcept(6).Value / this.DoConcept(7).Value;
            }
            GetLog(MyRule, " After Execute State:", 4);
        }

        /// <summary>قانون کارکرد 7-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و يک-31 درنظر گرفته شده است</remarks>
        public virtual void R31(AssignedRule MyRule)
        {
            throw new NotImplementedException("به فلوچارت مربوطه مراجعه شود-این قانون جز قوانین منسوخ میباشد");
        }

        /// <summary>قانون کارکرد 8-5</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و شش-36 درنظر گرفته شده است</remarks>
        public virtual void R36(AssignedRule MyRule)
        {
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018 مفهوم حداکثر اضافه کار مجاز ماهانه

            //4010 اضافه کارساعتي تعطيل ماهانه
            //4019 مفهوم اضافه کار قبل از وقت ماهانه
            //4020 مفهوم اضافه کار بین وقت ماهانه
            //4021 مفهوم اضافه کار بعد از وقت ماهانه
            //625 مفهوم اضافه کار روز غیر کاری ماهانه
            //626 مفهوم اضافه کارساعتی جمعه ماهانه
            //627 مفهوم اضافه کارساعتی غیر مجاز جمعه ماهانه
            //628 مفهوم اضافه کارساعتی مجازتعطیل غیرجمعه ماهانه
            //629 مفهوم اضافه کارساعتی غیر مجازتعطیل غیرجمعه ماهانه

            //استفاده از انباره در اینجا بررسی شود
            throw new NotImplementedException();
            /*
            GetLog(MyRule, " Before Execute State:", 4010, 3005, 8, 3034, 4005, 4006, 625, 626, 627, 628, 629);
            IPersonWorkGroupRepository workGroupRep = PersonWorkGroup.GetPersonWorkGroupRepository(false);
            IList<PersonWorkGroup> list = workGroupRep.GetAll();
            int lazem = 0;
            if (list.Count > 0 && list.Where(x => x.WorkGroupID == MyRule["Second", this.RuleCalculateDate].ToInt()).Count() > 0)
            {
                lazem = list.Where(x => x.WorkGroupID == MyRule["Second", this.RuleCalculateDate].ToInt()).First().BaseShiftList
                    .Where(x => x.Date >= this.CalcDateZone.FromDate && x.Date <= this.CalcDateZone.ToDate)
                    .Sum(x => x.PairValues);
            }
            //            if (lazem != null)
            {
                this.DoConcept(10).Value = lazem;
                if (this.DoConcept(3).Value > lazem)
                {
                    this.DoConcept(8).Value = lazem;
                    this.DoConcept(4005).Value = this.DoConcept(3).Value - lazem;
                    this.DoConcept(3034).Value = 0;
                    this.DoConcept(3005).Value = 0;
                    this.DoConcept(4006).Value = 0;
                }
                else
                {
                    this.DoConcept(3034).Value = lazem - this.DoConcept(10).Value;
                    this.DoConcept(3005).Value = 0;
                    this.DoConcept(4005).Value = 0;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(4010).Value = 0;

                    this.DoConcept(4006).Value = 0;

                }
                if (this.DoConcept(4005).Value > this.DoConcept(4018).Value * HourMin)
                {
                    this.DoConcept(3).Value -= (this.DoConcept(4005).Value + this.DoConcept(4018).Value * HourMin);
                    this.DoConcept(4006).Value += (this.DoConcept(4005).Value - this.DoConcept(4018).Value * HourMin);
                    this.DoConcept(4005).Value = this.DoConcept(4018).Value * HourMin;
                }

            }
            GetLog(MyRule, " After Execute State:", 4010, 3005, 8, 3034, 4005, 4006, 625, 626, 627, 628, 629);
             * */
        }

        /// <summary>قانون مرخصي 5-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و دو-72 درنظر گرفته شده است</remarks>
        public virtual void R72(AssignedRule MyRule)
        {
            throw new Exception("در مفهوم مرخصی خالص ساعتی اعمال گرددید");
            //3034 مفهوم غيبت ساعتي ماهانه
            //1011 مفهوم مرخصي استحقاقي ساعتي ماهانه            
            //8 مفهوم کارکردخالص ساعتي ماهانه
            //1074 مرخصی بی حقوق ساعتی ماهانه

            GetLog(MyRule, " Before Execute State:", 3034, 1011);
            if (this.DoConcept(1011).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                if (this.DoConcept(1014).Value > 0)
                {
                    this.DoConcept(1074).Value += this.DoConcept(1011).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                }
                else
                {
                    this.DoConcept(3034).Value += this.DoConcept(1011).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                }

                this.Person.AddRemainLeave(this.RuleCalculateDate, this.DoConcept(1011).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                this.DoConcept(1011).Value = MyRule["First", this.RuleCalculateDate].ToInt();

            }
            GetLog(MyRule, " After Execute State:", 3034, 1011);
        }

        /// <summary>قانون مرخصي 5-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و سه-73 درنظر گرفته شده است</remarks>
        public virtual void R73(AssignedRule MyRule)
        {
            throw new Exception("در مفهوم مرخصی خالص ساعتی اعمال گرددید");
            //3028 مفهوم غيبت ساعتي 
            //مرخصي استحقاقي ساعتي 1003
            //1011 مفهوم مرخصي استحقاقي ساعتي ماهانه            
            //8 مفهوم کارکردخالص ساعتي ماهانه
            //1092 مفهوم تعداد بازه های ماهانه ی مرخصی ساعتی
            //2 مفهوم کارکرد خالص ساعتی

            GetLog(MyRule, " Before Execute State:RemainLeave[" + this.Person.GetRemainLeave(this.RuleCalculateDate) + "]", 1003, 1011, 1092, 1093, 3028, 3001, 1002);

            this.DoConcept(1002);
            this.DoConcept(1056);
            if (this.DoConcept(1092).Value > MyRule["First", this.RuleCalculateDate].ToInt()
                && ((PairableScndCnpValue)this.DoConcept(1003)).PairCount > 0)
            {
                int count = this.DoConcept(1092).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                for (int i = 0; i < count; i++)
                {
                    int _tmp = ((PairableScndCnpValue)this.DoConcept(1003)).Pairs.OrderBy(x => x.From).Last().Value;
                    IPair pair = ((PairableScndCnpValue)this.DoConcept(1003)).Pairs.OrderBy(x => x.From).Last();

                    PairableScndCnpValue.AppendPairToScndCnpValue(pair, this.DoConcept(3001));

                    this.Person.AddRemainLeave(this.RuleCalculateDate, _tmp);
                    this.DoConcept(1011).Value -= _tmp;
                    this.DoConcept(1003).Value -= _tmp;
                    ((PairableScndCnpValue)this.DoConcept(1003)).RemovePairAt(((PairableScndCnpValue)this.DoConcept(1003)).Pairs.Count - 1);
                }
                if (count > 0)
                {
                    this.ReCalculate(3028, 3029, 3030, 3031, 3008, 3010, 3014);
                }
                this.DoConcept(1093).Value -= count;
                this.DoConcept(1092).Value -= count;
            }
            GetLog(MyRule, " After Execute State:RemainLeave[" + this.Person.GetRemainLeave(this.RuleCalculateDate) + "]", 1003, 1011, 1092, 1092, 3028, 3001, 1002);
        }

        /// <summary>قانون مرخصي 6-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و چهار-74 درنظر گرفته شده است</remarks>
        public virtual void R74(AssignedRule MyRule)
        {
            throw new Exception("در مفهوم مرخصی خالص ساعتی اعمال گرددید");
            //1012 مفهوم مرخصي استحقاقي ساعتي سالانه
            //3007 مفهوم غيبت ساعتي سالانه
            //19 مفهوم کارکردخالص ساعتي سالانه
            //8 مفهوم کارکردخالص ساعتي ماهانه

            int tmp = this.DoConcept(1012).Value - MyRule["First", this.RuleCalculateDate].ToInt();
            if (this.DoConcept(1012).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:RemainLeave[" + this.Person.GetRemainLeave(this.RuleCalculateDate) + "]", 19, 1012, 3007);
                this.DoConcept(3007).Value += tmp;
                this.DoConcept(19).Value -= tmp;
                this.Person.AddRemainLeave(this.RuleCalculateDate, tmp);
                this.DoConcept(1012).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                GetLog(MyRule, " After Execute State:RemainLeave[" + this.Person.GetRemainLeave(this.RuleCalculateDate) + "]", 19, 1012, 3007);
            }
        }

        /// <summary>قانون مرخصي 7-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و هفت-77 درنظر گرفته شده است</remarks>
        public virtual void R77(AssignedRule MyRule)
        {
            throw new Exception("در پارامتر اعمال شده , ار رده خارج");
            //1094 مفهوم اگر مرخصی ماهانه از حد بخشش گذشت شامل بخشش نشود
            this.DoConcept(1094).Value = 1;
        }

        /// <summary>قانون مرخصي 9-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و هفت-77 درنظر گرفته شده است</remarks>
        public virtual void R101(AssignedRule MyRule)
        {
            throw new Exception("در مفهوم مرخصی خالص ساعتی اعمال گرددید");
            //1015 مرخصی بی حقوق در صورت عدم طلب مرخصی استعلاجی
            //1010 مرخصی استعلاجی روزانه
            //1008 مرخصی استعلاجی ساعتی
            //1016 مرخصی استعلاجی ساعتی ماهانه
            //1017 مرخصی استعلاجی روزانه ماهانه
            //1018 مرخصی استعلاجی روزانه سالانه
            //1019 مرخصی استعلاجی ساعتی سالانه
            //1014 درصورت عدم طلب مرخصی به بی حقوق تبدیل شود
            //1056 مرخصی بی حقوق ساعتی_12
            //1066 مرخصی بی حقوق روزانه_32
            //3028 غیبت غیر مجاز ساعتی
            //3004 غیبت غیر مجاز روزانه

            GetLog(MyRule, " Before Execute State:", 1018, 1019, 1056, 1066, 3028, 3004, 1008, 1010);

            if (this.DoConcept(1018).Value * this.DoConcept(1001).Value + this.DoConcept(1019).Value
                >= MyRule["First", this.RuleCalculateDate].ToInt() * this.DoConcept(1001).Value)
            {
                if (this.DoConcept(1015).Value == 1)
                {
                    this.DoConcept(1056).Value = this.DoConcept(1008).Value;
                    this.DoConcept(1066).Value = this.DoConcept(1010).Value;
                }
                else
                {
                    this.DoConcept(3028).Value = this.DoConcept(1008).Value;
                    this.DoConcept(3004).Value = this.DoConcept(1010).Value;
                }
                this.DoConcept(1008).Value = 0;
                this.DoConcept(1010).Value = 0;
                this.ReCalculate(1018, 1019);
            }

            GetLog(MyRule, " After Execute State:", 1018, 1019, 1056, 1066, 3028, 3004, 1008, 1010);
        }

        /// <summary>قانون مرخصي 15-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و يک-81 درنظر گرفته شده است</remarks>
        /// مرخصي 15-1: تعطيلات بين مرخصي جزو مرخصي حساب شود
        public virtual void R81(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //325 تعطیلات غیر رسمی بین مرخصی,مرخصی محسوب شود
            GetLog(MyRule, " Before Execute State:", 325);
            this.DoConcept(325).Value = 1;
            GetLog(MyRule, " After Execute State:", 325);
        }

        /// <summary>قانون مرخصي 16-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و چهار-2009 درنظر گرفته شده است</remarks>
        public virtual void R84(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //1010 مفهوم مرخصي استعلاجي روزانه
            //تعطيل رسمي 1
            //114 مفهوم اعمال تعطيلات نوروز در مرخصي    
            //328 مفهوم تعطیلات غیر رسمی بین مرخصی استعلاجی,مرخصی استعلاجی محسوب شود

            GetLog(MyRule, " Before Execute State:", 328);
            this.DoConcept(328).Value = 1;
            GetLog(MyRule, " After Execute State:", 328);
        }

        /// <summary>قانون مرخصي 17-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و پنج-2010 درنظر گرفته شده است</remarks>
        public virtual void R85(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //326 مفهوم روزهای غیر کاری بین مرخصی,مرخصی محسوب شود
            GetLog(MyRule, " Before Execute State:", 326);
            this.DoConcept(326).Value = 1;
            GetLog(MyRule, " After Execute State:", 326);
        }


        /// <summary>قانون مرخصي 17-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و شش-2011 درنظر گرفته شده است</remarks>
        public virtual void R86(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //1010 مفهوم مرخصي استعلاجي روزانه
            //تعطيل نوروز 4
            //329 مفهوم روزهای غیر کاری بین مرخصی استعلاجی,مرخصی استعلاجی محسوب شود
            GetLog(MyRule, " Before Execute State:", 329);
            this.DoConcept(329).Value = 1;
            GetLog(MyRule, " After Execute State:", 329);

        }

        /// <summary>قانون مرخصي 20-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و هشت-89 درنظر گرفته شده است</remarks>
        public virtual void R88(AssignedRule MyRule)
        {
            //1022 مرخصي بي حقوق بيماري قطعي در روزغيرکاري
            //1054 مفهوم مرخصی بیماری بی حقوق ساعتی_11
            //1063 مرخصی بیماری بی حقوق خالص روزانه_31
            //1031 مفهوم مرخصی بیماری بی حقوق روزانه_31
            //1030 مفهوم مرخصی بیماری بی حقوق خالص روزانه_31
            GetLog(MyRule, " Before Execute State:", 1054, 1064);
            this.DoConcept(1022).Value = 1;
            if (this.DoConcept(1063).Value == 1)
            {
                this.DoConcept(1064).Value = 1;
                this.DoConcept(1054).Value = 0;
            }
            GetLog(MyRule, " After Execute State:", 1054, 1064);
        }

        /// <summary>قانون مرخصي 21-1</summary>
        /// <remarks>این قانون بصورت عکس فعال میشود یعنی ما در حالت عادی مرخصی اول وقت میدهیم
        /// و در صورتی که این قانون تیک نخورده بود مرخصی داده شده را پس میگیریم</remarks>
        public virtual void R104(AssignedRule MyRule)
        {
            //1003  مرخصی ساعتی استحقاقی
            //1008 مرخصی ساعتی استعلاجی
            //1038 مرخصی ساعتی باحقوق_23
            //1039 مرخصی ساعتی باحقوق_24
            //1040 مرخصی ساعتی باحقوق_25
            //1041 مرخصی ساعتی باحقوق_26
            //1042 مرخصی ساعتی باحقوق_27
            GetLog(MyRule, " Before Execute State:", 1003, 1008, 1038, 1039, 1040, 1041, 1042);
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                int startShift = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From;
                IPair pair;
                if (this.DoConcept(1003).Value > 0)
                {
                    pair = ((PairableScndCnpValue)this.DoConcept(1003)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                    if (pair != null)
                    {
                        this.Person.AddRemainLeave(this.RuleCalculateDate, pair.Value);
                    }
                    ((PairableScndCnpValue)this.DoConcept(1003)).RemovePair(pair);
                }

                pair = ((PairableScndCnpValue)this.DoConcept(1008)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                ((PairableScndCnpValue)this.DoConcept(1008)).RemovePair(pair);

                pair = ((PairableScndCnpValue)this.DoConcept(1038)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                ((PairableScndCnpValue)this.DoConcept(1038)).RemovePair(pair);

                pair = ((PairableScndCnpValue)this.DoConcept(1039)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                ((PairableScndCnpValue)this.DoConcept(1039)).RemovePair(pair);

                pair = ((PairableScndCnpValue)this.DoConcept(1040)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                ((PairableScndCnpValue)this.DoConcept(1040)).RemovePair(pair);

                pair = ((PairableScndCnpValue)this.DoConcept(1041)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                ((PairableScndCnpValue)this.DoConcept(1041)).RemovePair(pair);

                pair = ((PairableScndCnpValue)this.DoConcept(1042)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                ((PairableScndCnpValue)this.DoConcept(1042)).RemovePair(pair);

            }
            GetLog(MyRule, " After Execute State:", 1003, 1008, 1038, 1039, 1040, 1041, 1042);
        }

        /// <summary>قانون مرخصي 22-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و نه-89 درنظر گرفته شده است</remarks>
        public virtual void R89(AssignedRule MyRule)
        {
            //1093 تعداد بازهای مرخصی ساعتی
            //1003 مفهوم مرخصي استحقاقي ساعتی
            //1081 مفهوم مرخصي استحقاقي ساعتی خارج از شیفت
            //1083 حداکثر تعداد مرخصی ساعتی در ماه
            if (this.DoConcept(1081).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 1003, 4002, 1081, 1092, 1093);
                ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
                BaseShift shift = this.Person.GetShiftByDate(this.RuleCalculateDate);
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(1081)).Pairs)
                {
                    //سقف تعداد مرخصی ساعتی در ماه اعمال میگردد 
                    if (this.DoConcept(1092).Value + 1 > this.DoConcept(1083).Value)
                    {
                        break;
                    }
                    bool weSawHim = false;
                    //before shift
                    if (pair.To <= shift.PastedPairs.From)
                    {
                        if (ProceedTraffic.Pairs.Where(x => x.From <= pair.From).Count() > 0)
                        {
                            weSawHim = true;
                        }
                    }
                    //after shift
                    else if (pair.From >= shift.PastedPairs.To)
                    {
                        if (ProceedTraffic.Pairs.Where(x => x.From >= pair.To).Count() > 0)
                        {
                            weSawHim = true;
                        }
                    }
                    //between shift
                    else
                    {
                        if (ProceedTraffic.Pairs.Where(x => x.To <= pair.From).Count() > 0
                            &&
                            ProceedTraffic.Pairs.Where(x => x.From >= pair.From).Count() > 0)
                        {
                            weSawHim = true;
                        }
                    }
                    if (weSawHim)
                    {
                        int validLeave = pair.Value;
                        this.Person.AddUsedLeave(this.RuleCalculateDate, validLeave, null);
                        this.DoConcept(1093).Value += 1;
                        this.DoConcept(1092).Value += 1;
                        ((PairableScndCnpValue)this.DoConcept(1003)).AppendPair(pair);
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);
                    }
                }
                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", 1003, 4002, 1081, 1093);
            }
        }

        /// <summary>قانون مرخصي 23-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود-90 درنظر گرفته شده است</remarks>
        public virtual void R90(AssignedRule MyRule)
        {
            //1005 مفهوم مرخصي استحقاقي روزانه           
            //حد نصاب نوبت کاري 10 90
            //تعداد نوبت کاري 10 74
            //142 زمان نوبت کاري 10
            //143 تعداد نوبت کاري 15
            //144 زمان نوبت کاري 15
            //145 تعداد نوبت کاري 20
            //146 زمان نوبت کاري 20
            //147 تعداد نوبت کاري 25
            //148 زمان نوبت کاري 25
            //149 تعداد نوبت کاري 35
            //150 زمان نوبت کاري 35           
            //1090 انواع مرخصی روزانه که حقوق تعلق میگیرد

            //نحوه ی استفاده از نوبت کاری باید بررسی شود
            throw new NotImplementedException();
            /*

            GetLog(MyRule, " Before Execute State:", 74, 142, 143, 144, 145, 146, 147, 148, 149, 150);

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).NobatKari != ShiftNobatKari.NONE)
            {
                if (this.DoConcept(1090).Value > 0 &&
                    this.Person.GetShiftByDate(this.RuleCalculateDate).MinNobatKari <= this.DoConcept(2).Value)
                {
                    switch (this.Person.GetShiftByDate(this.RuleCalculateDate).NobatKari)
                    {
                        case ShiftNobatKari.A:
                            this.DoConcept(5005).Value = 1;
                            this.DoConcept(142).Value = this.DoConcept(2).Value;
                            break;
                        case ShiftNobatKari.B:
                            this.DoConcept(143).Value = 1;
                            this.DoConcept(144).Value = this.DoConcept(2).Value;
                            break;
                        case ShiftNobatKari.C:
                            this.DoConcept(145).Value = 1;
                            this.DoConcept(146).Value = this.DoConcept(2).Value;
                            break;
                        case ShiftNobatKari.D:
                            this.DoConcept(147).Value = 1;
                            this.DoConcept(148).Value = this.DoConcept(2).Value;
                            break;
                        case ShiftNobatKari.E:
                            this.DoConcept(149).Value = 1;
                            this.DoConcept(150).Value = this.DoConcept(2).Value;
                            break;
                    }
                }
            }
            GetLog(MyRule, " After Execute State:", 74, 142, 143, 144, 145, 146, 147, 148, 149, 150);
             */
        }
        /// <summary>قانون مرخصي 23-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و يک-91 درنظر گرفته شده است</remarks>
        public virtual void R91(AssignedRule MyRule)
        {
            //1020 مفهوم مرخصي بايد با مجوز باشد 
            GetLog(MyRule, " Before Execute State:", 1020);
            this.DoConcept(1020).Value = 1;
            GetLog(MyRule, " After Execute State:", 1020);
        }


        /// <summary>قانون مرخصي 24-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و يک-91 درنظر گرفته شده است</remarks>
        public virtual void R105(AssignedRule MyRule)
        {
            //1074 مفهوم مرخصی بی حقوق ساعتی ماهانه 
            GetLog(MyRule, " Before Execute State:", 1074);
            this.DoConcept(1074).Value += this.GetConcept(1074, this.RuleCalculateDate.AddMonths(-1)).Value;
            GetLog(MyRule, " After Execute State:", 1074);
        }

        /// <summary>قانون مرخصي 25-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و دو-3021 درنظر گرفته شده است</remarks>
        public virtual void R92(AssignedRule MyRule)
        {
            //4002 مفهوم اضافه کار ساعتي
            //1029 مرخصی روزانه بی حقوق_44
            //1031 مرخصی روزانه بی حقوق_43
            //1037 مرخصی روزانه بی حقوق_45
            GetLog(MyRule, " Before Execute State:", 4002);
            bool state = false;
            if (MyRule["First", this.RuleCalculateDate].ToInt() == 43 && this.DoConcept(1031).Value > 0)
            {
                state = true;
            }
            else if (MyRule["First", this.RuleCalculateDate].ToInt() == 44 && this.DoConcept(1029).Value > 0)
            {
                state = true;
            }
            else if (MyRule["First", this.RuleCalculateDate].ToInt() == 45 && this.DoConcept(1037).Value > 0)
            {
                state = true;
            }
            if (state)
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(MyRule["Second", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4002);
        }

        /// <summary>قانون مرخصي 26-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و سه-93 درنظر گرفته شده است</remarks>
        public virtual void R93(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 1003);
            if (this.DoConcept(1064).Value > 0 || this.DoConcept(1066).Value > 0 || this.DoConcept(1068).Value > 0
                || this.DoConcept(1070).Value > 0 || this.DoConcept(1072).Value > 0)
            {
                this.DoConcept(1003).Value += MyRule["First", this.RuleCalculateDate].ToInt();
                this.Person.AddUsedLeave(this.RuleCalculateDate, MyRule["First", this.RuleCalculateDate].ToInt(), null);
            }
            GetLog(MyRule, " After Execute State:", 1003);
        }

        /// <summary>قانون مرخصي 27-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و چهار-3023 درنظر گرفته شده است</remarks>
        public virtual void R94(AssignedRule MyRule)
        {
            //3004 مفهوم غيبت روزانه

            if (this.DoConcept(3004).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 1003);
                this.DoConcept(1003).Value += MyRule["First", this.RuleCalculateDate].ToInt();
                this.Person.AddUsedLeave(this.RuleCalculateDate, MyRule["First", this.RuleCalculateDate].ToInt(), null);
                GetLog(MyRule, " After Execute State:", 1003);
            }
        }

        /// <summary>ماموريت 1-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست و يک-3032 درنظر گرفته شده است</remarks>
        public virtual void R121(AssignedRule MyRule)
        {
            //ماموريت روزانه ماهانه 2006
            //ماموريت ساعتي ماهانه 2007
            //ماموريت درروز 2001
            //2006 ماموريت روزانه ماهانه
            GetLog(MyRule, " Before Execute State:", 2006, 2007);
            if (this.DoConcept(2007).Value > this.DoConcept(2001).Value)
            {
                this.DoConcept(2006).Value += this.DoConcept(2007).Value /
                                                            this.DoConcept(2001).Value;
                this.DoConcept(2007).Value = this.DoConcept(2007).Value %
                                                                        this.DoConcept(2001).Value;
            }
            GetLog(MyRule, " After Execute State:", 2006, 2007);
        }

        /// <summary>ماموريت 2-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست ودو-3033 درنظر گرفته شده است</remarks>
        public virtual void R122(AssignedRule MyRule)
        {
            //4 کارکردخالص روزانه
            //ماموريت روزانه 2005
            //2008 ماموريت خالص شبانه روزی
            //ماموريت شبانه روزي سالانه 2009
            //ماموريت روزانه سالانه 2010
            //سقف تعداد ماموريت روزانه و شبانه روزي در سال 2011
            //3004 غيبت روزانه

            if (this.DoConcept(2009).Value +
                this.DoConcept(2010).Value > MyRule["MaxMissionCount", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 2, 13, 4, 2008, 2009, 2005, 3004);
                this.DoConcept(2005).Value = 0;
                this.DoConcept(2008).Value = 0;
                this.DoConcept(2).Value = 0;
                this.DoConcept(4).Value = 0;
                this.ReCalculate(13);
                if (this.DoConcept(6).Value > 0 && this.DoConcept(1).Value > 0)
                {
                    this.DoConcept(3004).Value = 1;
                }
                this.ReCalculate(2009, 2010);
                GetLog(MyRule, " After Execute State:", 2, 13, 4, 2008, 2009, 2005, 3004);
            }
        }

        /// <summary>ماموريت 3-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست وسه-123 درنظر گرفته شده است</remarks>
        public virtual void R123(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //2023 مجموع ماموريت ساعتي
            //کارکردخالص ساعتي 2
            //4 کارکردخالص روزانه
            //13 کارکرد ناخالص ساعتی
            //ماموريت خالص روزانه 2003
            //348 ماموریت به کارکردخالص اضافه شود
            //ماموريت درروز 2001
            //7 کارکرد در روز
            GetLog(MyRule, " Before Execute State:", 2, 4, 13, 2001, 4002);
            if (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
            {
                if (this.DoConcept(2005).Value == 1)
                {
                    this.DoConcept(4).Value = 1;
                    if (this.DoConcept(6).Value > 0)
                    {
                        this.DoConcept(2).Value = this.DoConcept(6).Value;
                    }
                    //else //باید توسط قانون روز غیر کاری به کارکرد اضافه شود انجام گردد
                    //{
                    //    this.DoConcept(2).Value = this.DoConcept(7).Value;
                    //}
                    if (this.DoConcept(2).Value == 0)
                    {
                        this.DoConcept(4).Value = 0;
                    }
                }
                else
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
        }

        /// <summary>ماموريت 5-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست وشش-126 درنظر گرفته شده است</remarks>
        /// ماموريت 5-1: تعطيلات بين ماموريت روزانه ماموريت محسوب شود
        public virtual void R126(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //2048 تعطیلات غیر رسمی بین ماموریت,ماموریت محسوب شود

            GetLog(MyRule, " Before Execute State:", 2048);
            this.DoConcept(2048).Value = 1;
            GetLog(MyRule, " After Execute State:", 2048);
        }

        ///<summary>ماموريت 7-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست وهشت-128 درنظر گرفته شده است</remarks>
        public virtual void R128(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //2047 تعطیلات رسمی بین ماموریت,ماموریت محسوب شود

            GetLog(MyRule, " Before Execute State:", 2047);
            this.DoConcept(2047).Value = 1;
            GetLog(MyRule, " After Execute State:", 2047);
        }

        ///<summary>ماموريت 8-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست و نه-129 درنظر گرفته شده است</remarks>
        public virtual void R129(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //2050 تعطیلات رسمی بین ماموریت شبانه روزی,ماموریت شبانه روزی محسوب شود

            GetLog(MyRule, " Before Execute State:", 2050);
            this.DoConcept(2050).Value = 1;
            GetLog(MyRule, " After Execute State:", 2050);
        }

        /// <summary>ماموريت 9-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوسي-5 درنظر گرفته شده است</remarks>
        public virtual void R130(AssignedRule MyRule)
        {
            throw new Exception("در قانون مربوطه بصورت پارامتر ارسال شد  , از رده خارج");
            //2049 روزهای غیر کاری بین ماموریت,ماموریت محسوب شود
            GetLog(MyRule, " Before Execute State:", 2049);
            this.DoConcept(2049).Value = 1;
            GetLog(MyRule, " After Execute State:", 2049);
        }

        ///<summary>قانون ماموريت 12-1</summary>
        /// <remarks>ماموریت ساعتی اول وقت با پیشکارت مجاز است</remarks>
        /// /// <remarks>این قانون بصورت عکس فعال میشود یعنی ما در حالت عادی ماموریت اول وقت میدهیم
        /// و در صورتی که این قانون تیک نخورده بود مامورینپت داده شده را پس میگیریم
        public virtual void R270(AssignedRule MyRule)
        {
            //2004  ماموریت ساعتی51 
            //2019 ماموریت ساعتی52
            //2020 ماموریت ساعتی53
            //2021 ماموریت ساعتی54
            //2022 ماموریت ساعتی55
            //مجموع ماموريت ساعتي 2023

            GetLog(MyRule, " Before Execute State:", 2004, 2019, 2020, 2021, 2022, 2023);
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                int startShift = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From;
                IPair pair = null;
                if (this.DoConcept(2004).Value > 0)
                {
                    pair = ((PairableScndCnpValue)this.DoConcept(2004)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                    ((PairableScndCnpValue)this.DoConcept(2004)).RemovePair(pair);
                    this.ReCalculate(2023);
                }
                if (this.DoConcept(2019).Value > 0)
                {
                    pair = ((PairableScndCnpValue)this.DoConcept(2019)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                    ((PairableScndCnpValue)this.DoConcept(2019)).RemovePair(pair);
                    this.ReCalculate(2023);
                }
                if (this.DoConcept(2020).Value > 0)
                {
                    pair = ((PairableScndCnpValue)this.DoConcept(2020)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                    ((PairableScndCnpValue)this.DoConcept(2020)).RemovePair(pair);
                    this.ReCalculate(2023);
                }
                if (this.DoConcept(2021).Value > 0)
                {
                    pair = ((PairableScndCnpValue)this.DoConcept(2021)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                    ((PairableScndCnpValue)this.DoConcept(2021)).RemovePair(pair);
                    this.ReCalculate(2023);
                }
                if (this.DoConcept(2022).Value > 0)
                {
                    pair = ((PairableScndCnpValue)this.DoConcept(2022)).Pairs.Where(x => x.From == startShift).FirstOrDefault();
                    ((PairableScndCnpValue)this.DoConcept(2022)).RemovePair(pair);
                    this.ReCalculate(2023);
                }
                if (pair != null && pair.Value > 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(3028)).AppendPair(pair);
                }
            }
            GetLog(MyRule, " After Execute State:", 2004, 2019, 2020, 2021, 2022, 2023);

        }
        ///<summary>قانون ماموريت 15-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو سي و پنج-1022 درنظر گرفته شده است</remarks>
        public virtual void R135(AssignedRule MyRule)
        {
            //2005 مجموع ماموريت روزانه
            //2008 مجموع ماموریت شبانه روزی
            //حد نصاب نوبت کاري 10 90
            //تعداد نوبت کاري 10 74
            //142 زمان نوبت کاري 10
            //143 تعداد نوبت کاري 15
            //144 زمان نوبت کاري 15
            //145 تعداد نوبت کاري 20
            //146 زمان نوبت کاري 20
            //147 تعداد نوبت کاري 25
            //148 زمان نوبت کاري 25
            //149 تعداد نوبت کاري 35
            //150 زمان نوبت کاري 35 

            //نحوه ی استفاده از نوبت کاری باید بررسی شود
            throw new NotImplementedException();
            /*
            GetLog(MyRule, " Before Execute State:", 2, 74, 142, 143, 144, 145, 146, 147, 148, 149, 150);
            if (this.DoConcept(2005).Value > 0 || this.DoConcept(2008).Value > 0)
            {
                if (this.Person.GetShiftByDate(this.RuleCalculateDate).NobatKari != ShiftNobatKari.NONE)
                {
                    if (this.Person.GetShiftByDate(this.RuleCalculateDate).MinNobatKari <= this.DoConcept(2).Value)
                    {
                        switch (this.Person.GetShiftByDate(this.RuleCalculateDate).NobatKari)
                        {
                            case ShiftNobatKari.A:
                                this.DoConcept(5005).Value = 1;
                                this.DoConcept(142).Value = this.DoConcept(2).Value;
                                break;
                            case ShiftNobatKari.B:
                                this.DoConcept(143).Value = 1;
                                this.DoConcept(144).Value = this.DoConcept(2).Value;
                                break;
                            case ShiftNobatKari.C:
                                this.DoConcept(145).Value = 1;
                                this.DoConcept(146).Value = this.DoConcept(2).Value;
                                break;
                            case ShiftNobatKari.D:
                                this.DoConcept(147).Value = 1;
                                this.DoConcept(148).Value = this.DoConcept(2).Value;
                                break;
                            case ShiftNobatKari.E:
                                this.DoConcept(149).Value = 1;
                                this.DoConcept(150).Value = this.DoConcept(2).Value;
                                break;
                        }
                    }
                }
            }
            GetLog(MyRule, " After Execute State:", 2, 74, 142, 143, 144, 145, 146, 147, 148, 149, 150);
             * */
        }

        ///<summary>قانون ماموريت 17-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو سي و هفت-137 درنظر گرفته شده است</remarks>
        public virtual void R137(AssignedRule MyRule)
        {
            this.DoConcept(2027).Value = 1;
        }

        ///<summary>قانون ماموريت 18-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو سي و هشت-138 درنظر گرفته شده است</remarks>
        public virtual void R138(AssignedRule MyRule)
        {
            //مجموع ماموريت روزانه 2005
            //2008 مجموع ماموریت شبانه روزی
            //تقويم تعطيل رسمي 1
            //تعطيل غير رسمي 2
            //ماموريت درروز 2001
            //1005 مرخصی استحقاقی روزانه
            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
            {
                if (this.DoConcept(2005).Value > 0 || this.DoConcept(2008).Value > 0)
                {
                    GetLog(MyRule, " Before Execute State:", 1005);
                    this.DoConcept(1005).Value -= 1;
                    this.Person.AddRemainLeave(this.RuleCalculateDate, this.DoConcept(2001).Value);
                    GetLog(MyRule, " After Execute State:", 1005);
                }
            }
        }

        ///<summary>قانون ماموريت 19-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو سي و نه-139 درنظر گرفته شده است</remarks>
        public virtual void R139(AssignedRule MyRule)
        {
            throw new NotImplementedException("بررسی شود");
            GetLog(MyRule, " Before Execute State:", 2004, 2019, 2020, 2021, 2022, 2031, 2032, 2033, 2034, 2035);
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
            {
                if (this.DoConcept(2031).Value > 0)
                {
                    this.DoConcept(2004).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2031).Value = 0;
                }
                if (this.DoConcept(2032).Value > 0)
                {
                    this.DoConcept(2019).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2032).Value = 0;
                }
                if (this.DoConcept(2033).Value > 0)
                {
                    this.DoConcept(2020).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2033).Value = 0;
                }
                if (this.DoConcept(2034).Value > 0)
                {
                    this.DoConcept(2021).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2034).Value = 0;
                }
                if (this.DoConcept(2035).Value > 0)
                {
                    this.DoConcept(2022).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2035).Value = 0;
                }
                if (this.DoConcept(2041).Value > 0)
                {
                    this.DoConcept(2004).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2041).Value = 0;
                }
                if (this.DoConcept(2042).Value > 0)
                {
                    this.DoConcept(2019).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2042).Value = 0;
                }
                if (this.DoConcept(2043).Value > 0)
                {
                    this.DoConcept(2020).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2043).Value = 0;
                }
                if (this.DoConcept(2044).Value > 0)
                {
                    this.DoConcept(2021).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2044).Value = 0;
                }
                if (this.DoConcept(2045).Value > 0)
                {
                    this.DoConcept(2022).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(2045).Value = 0;
                }
                this.ReCalculate(2005, 2008, 2023);
            }
            GetLog(MyRule, " After Execute State:", 2004, 2019, 2020, 2021, 2022, 2031, 2032, 2033, 2034, 2035);
        }

        ///<summary>قانون ماموريت 20-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو چهل-141 درنظر گرفته شده است</remarks>
        /// اموريت 20-2: روش تبديل ساعت ماموريت به روزانه: هر .... ساعت معادل يک روز ماموريت
        public virtual void R141(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005
            //ماموريت ساعتي 2004
            //2031 مفهوم ماموریت روزانه_61

            GetLog(MyRule, " Before Execute State:", 2004, 2005, 2031, 3028, 2023);
            if (this.DoConcept(2004).Value > 0)
            {
                this.DoConcept(2031).Value += this.DoConcept(2004).Value / MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(2004).Value = this.DoConcept(2004).Value % MyRule["First", this.RuleCalculateDate].ToInt();
                PairableScndCnpValue.AddPairToScndCnpValue(new PairableScndCnpValuePair(0, this.DoConcept(2004).Value), this.DoConcept(2004));
                this.DoConcept(3028).Value = 0;
                this.ReCalculate(2023, 2005);
            }
            GetLog(MyRule, " After Execute State:", 2004, 2005, 2031, 3028, 2023);

        }


        ///<summary>قانون ماموريت 20-3</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو چهل-142 درنظر گرفته شده است</remarks>
        public virtual void R142(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005
            //اضافه کارساعتي 4002
            //ماموريت ساعتي 2004


            GetLog(MyRule, " Before Execute State:", 2005, 3028, 4002, 2023, 2031);
            if (this.DoConcept(2005).Value > 0)
            {
                this.DoConcept(2031).Value = 1;
                if (MyRule["First", this.RuleCalculateDate].ToInt() > this.DoConcept(6).Value)
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt() - this.DoConcept(6).Value);
                }
                this.DoConcept(3028).Value = 0;
                this.ReCalculate(2023, 2005);
            }
            GetLog(MyRule, " After Execute State:", 2005, 3028, 4002, 2023, 2031);

        }

        /// <summary>قانون کم کاري 2-3</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و سه-43 درنظر گرفته شده است</remarks>
        public virtual void R43(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي ماهانه 8

            //مدت غيبت بين وقت مجازماهانه 3018

            //غيبت بين وقت ساعتي مجاز ماهانه 3027
            //3035 غيبت بين وقت ساعتي غيرمجاز ماهانه

            //غیبت ساعتی مجاز ماهانه 3026 
            //3034 غیبت ساعتی غیرمجاز ماهانه

            if (this.DoConcept(3018).Value <=
                  this.DoConcept(3027).Value)
            {
                GetLog(MyRule, " Before Execute State:", 8, 3026, 3027, 3034, 3035);
                int tmp = this.DoConcept(3027).Value - this.DoConcept(3018).Value;
                this.DoConcept(8).Value -= tmp;
                this.DoConcept(3026).Value -= tmp;
                this.DoConcept(3027).Value -= tmp;

                this.DoConcept(3034).Value += tmp;
                this.DoConcept(3035).Value += tmp;

                GetLog(MyRule, " After Execute State:", 8, 3026, 3027, 3034, 3035);
            }
        }

        /// <summary>قانون کم کاري 4-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و شش-46 درنظر گرفته شده است</remarks>
        public virtual void R46(AssignedRule MyRule)
        {
            //غيبت ساعتي مجاز ماهانه 3026
            //غيبت ساعتي غيرمجاز ماهانه 3034
            //اضافه کارساعتي مجاز ماهانه 4005
            //3 کارکردناخالص ماهانه            


            GetLog(MyRule, " Before Execute State:", 3026, 4005, 3);

            int tmp = Operation.Minimum(this.DoConcept(3026).Value,
                                            MyRule["First", this.RuleCalculateDate].ToInt());
            this.DoConcept(3026).Value -= tmp;
            this.DoConcept(4005).Value -= tmp;
            this.DoConcept(3).Value -= tmp;

            GetLog(MyRule, " After Execute State:", 3026, 4005, 3);
        }

        /// <summary>قانون کم کاري 8-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه-50 درنظر گرفته شده است</remarks>
        public virtual void R50(AssignedRule MyRule)
        {
            //1066 مرخصی بی حقوق روزانه
            //3028 غيبت ساعتي غيرمجاز
            //کارکردخالص ساعتي 2
            //13 کارکردناخالص ساعتی
            GetLog(MyRule, " Before Execute State:", 2, 3028);
            int tmp = this.DoConcept(1066).Value * MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(3028).Value += tmp;
            this.DoConcept(2).Value += tmp;
            this.ReCalculate(13);
            GetLog(MyRule, " After Execute State:", 2, 3028);
        }

        /// <summary>قانون کم کاري 9-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و يک-51 درنظر گرفته شده است</remarks>
        public virtual void R51(AssignedRule MyRule)
        {
            throw new NotImplementedException();
        }

        /// <summary>قانون کم کاري 10-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و سه-53 درنظر گرفته شده است</remarks>
        public virtual void R53(AssignedRule MyRule)
        {
            //در قانون 10-2 لحاظ شده
            throw new NotImplementedException();
        }

        /// <summary>قانون کم کاري 10-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و پنج-55 درنظر گرفته شده است</remarks>
        public virtual void R55(AssignedRule MyRule)
        {
            //4002 اضافه کارساعتي
            //2 کارکردخالص ساعتي
            //کارکردلازم 6
            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز
            //3001 غيبت خالص ساعتي
            //5007 مفهوم جانباز

            GetLog(MyRule, " Before Execute State:", 2, 4002, 3020, 3028, 5007);
            this.DoConcept(2).Value += MyRule["First", this.RuleCalculateDate].ToInt();
            if (this.DoConcept(2).Value > this.Person.GetShiftByDate(this.RuleCalculateDate).Value)
            {
                int tmp = this.DoConcept(2).Value -
                            this.Person.GetShiftByDate(this.RuleCalculateDate).Value;
                if (MyRule["Second", this.RuleCalculateDate].ToInt() == 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(tmp);
                }
                else
                {
                    this.Person.AddRemainLeave(this.RuleCalculateDate, tmp);
                }
                this.DoConcept(2).Value -= tmp;
                this.DoConcept(5007).Value += tmp;
            }
            else
            {
                this.DoConcept(3020).Value -= Operation.Minimum(this.DoConcept(3028).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                this.DoConcept(3028).Value += Operation.Minimum(this.DoConcept(3028).Value, MyRule["First", this.RuleCalculateDate].ToInt());
            }
            GetLog(MyRule, " After Execute State:", 2, 4002, 3020, 3028, 5007);
        }

        /// <summary>قانون کم کاري 15-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و نه-59 درنظر گرفته شده است</remarks>
        public virtual void R59(AssignedRule MyRule)
        {
            //غیبت ساعتی غیرمجاز ماهانه 3034

            GetLog(MyRule, " Before Execute State:", 3034);
            this.DoConcept(3034).Value += this.GetConcept(3034, this.RuleCalculateDate.AddMonths(-1)).Value;
            GetLog(MyRule, " After Execute State:", 3034);
        }

        /// <summary>قانون کم کاري 14-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و نه-59 درنظر گرفته شده است</remarks>
        public virtual void R99(AssignedRule MyRule)
        {
            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي

            //3015 غيبت ساعتي در استراحت بين وقت

            //3028 غيبت ساعتی غیرمجاز
            //2 کارکردخالص ساعتی


            GetLog(MyRule, " Before Execute State:", 1002, 2002, 2, 3028);

            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2002);
            if (this.DoConcept(3015).Value > 0)
            {
                this.DoConcept(3028).Value += this.DoConcept(3015).Value;
                this.DoConcept(2).Value -= this.DoConcept(3015).Value;
                this.ReCalculate(13);
                if (this.DoConcept(2).Value < 0)
                {
                    this.DoConcept(2).Value = 0;
                }
            }

            GetLog(MyRule, " After Execute State:", 1002, 2002, 2, 3028);
        }

        /// <summary>قانون کم کاري 17-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و يک-61 درنظر گرفته شده است</remarks>
        public virtual void R61(AssignedRule MyRule)
        {
            //8 کارکردخالص ساعتي ماهانه
            //3 کارکردناخالص ماهانه

            //تاخيرساعتي ماهانه 3009
            //غيبت ساعتي ماهانه 3006
            //3039 تعداد تاخير غيرمجاز ماهانه
            //جريمه تاخير 3037  

            int tmp = this.DoConcept(3039).Value;
            if (tmp > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 3009, 8, 3034, 3, 3037);

                int tmp2 = (tmp - MyRule["First", this.RuleCalculateDate].ToInt()) * MyRule["Second", this.RuleCalculateDate].ToInt();
                this.DoConcept(8).Value -= tmp2;
                this.DoConcept(3).Value -= tmp2;

                this.DoConcept(3009).Value += tmp2;
                this.DoConcept(3034).Value += tmp2;
                this.DoConcept(3037).Value += tmp2;

                GetLog(MyRule, " After Execute State:", 3009, 8, 3034, 3, 3037);
            }
        }

        /// <summary>قانون اضافه کاري 14-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و هفت-167 درنظر گرفته شده است</remarks>
        public virtual void R167(AssignedRule MyRule)
        {
            //ماموريت خالص شبانه روزي 2008
            //4002 اضافه کار ساعتي مجاز

            //تعطيل رسمي 1
            //2 تعطيل غيررسمي

            ///باید برای انواع ماموریت شبانه روزی اجرا شود
            if (this.DoConcept(2008).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 13);
                if (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt());
                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["Second", this.RuleCalculateDate].ToInt());

                }
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 15-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و هشت-4029 درنظر گرفته شده است</remarks>
        public virtual void R168(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005
            //4002 اضافه کار ساعتي مجاز

            //تعطيل رسمي 1
            //2 تعطيل غيررسمي

            if (this.DoConcept(2005).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 13);
                if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["Second", this.RuleCalculateDate].ToInt());
                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt());
                }
                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 16-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و نه-169 درنظر گرفته شده است</remarks>
        public virtual void R169(AssignedRule MyRule)
        {
            //4002 اضافه کار ساعتي مجاز

            //تعطيل رسمي 1
            //2 تعطيل غيررسمي

            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2")
                && (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0))
            {
                GetLog(MyRule, " Before Execute State:", 4002, 13);
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 17-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد-170 درنظر گرفته شده است</remarks>
        public virtual void R170(AssignedRule MyRule)
        {
            //1 مفهوم حضور                         
            //4002 اضافه کار ساعتي مجاز

            if (this.DoConcept(1).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 13);
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 19-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هشتاد و چهار-184 درنظر گرفته شده است</remarks>
        public virtual void R184(AssignedRule MyRule)
        {
            //اضافه کارساعتي مجاز 4002
            //13 کارکرد ناخالص
            //4003 اضافه کارساعتي غيرمجاز


            GetLog(MyRule, " Before Execute State:", 4002, 13, 4003);

            ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(this.DoConcept(4003).Value);
            this.ReCalculate(13);
            ((PairableScndCnpValue)this.DoConcept(4003)).ClearPairs();

            GetLog(MyRule, " After Execute State:", 4002, 13, 4003);
        }

        /// <summary>قانون اضافه کاري 21-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هشتاد و پنج-185 درنظر گرفته شده است</remarks>
        public virtual void R185(AssignedRule MyRule)
        {
            //4005 اضافه کارساعتي مجاز ماهانه
            //3 کارکردناخالص ماهانه
            //اضافه کارساعتي غيرمجاز ماهانه 4006

            GetLog(MyRule, " Before Execute State:", 4005, 3);

            //this.DoConcept(4006).Value = this.DoConcept(4005).Value;

            this.DoConcept(3).Value -= this.DoConcept(4005).Value;
            this.DoConcept(4005).Value = 0;

            GetLog(MyRule, " After Execute State:", 4005, 3);
        }

        /// <summary>قانون اضافه کاري 24-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و پنج-175 درنظر گرفته شده است</remarks>
        public virtual void R175(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4002            
            //1 تعطیل رسمی
            //2 تعطیل غیر رسمی
            //4009 اضافه کارساعتي تعطيل
            //140 کشيک 

            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2")
                && this.DoConcept(4009).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 5006, 4009, 4002, 13);

                this.DoConcept(5006).Value = 1;
                this.DoConcept(4009).Value -= MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(4002).Value -= MyRule["First", this.RuleCalculateDate].ToInt();
                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(MyRule["First", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 5006, 4009, 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 25-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و شش-176 درنظر گرفته شده است</remarks>
        public virtual void R176(AssignedRule MyRule)
        {
            //3003 مفهوم غيبت روزانه
            //اضافه کار ساعتي 4001            
            //4022 مفهوم تعطيل کاري            


            if ((this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                && (this.DoConcept(4002).Value) >= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4022, 13);

                this.DoConcept(4022).Value = 1;
                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(MyRule["Second", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 4022, 13);
            }
        }

        /// <summary>قانون اضافه کاري 26-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و هفت-177 درنظر گرفته شده است</remarks>
        public virtual void R177(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4002            
            //4008 مفهوم اضافه کارساعتي قبل ازوقت

            if (this.DoConcept(4008).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 4008, 4002, 13);

                int tmp = MyRule["Second", this.RuleCalculateDate].ToInt() - MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(4008).Value += tmp;
                //از همین روش استفاده شود چون گاها این قانون باعث کم شدن اضافه کار میگردد
                this.DoConcept(4002).Value += tmp;
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4008, 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 27-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و هشت-178 درنظر گرفته شده است</remarks>
        public virtual void R178(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4001            
            //4007 مفهوم اضافه کارساعتي بعد ازوقت

            if (this.DoConcept(4007).Value >= MyRule["First", this.RuleCalculateDate].ToInt() && this.DoConcept(4007).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4007, 13);

                int tmp = MyRule["Second", this.RuleCalculateDate].ToInt() - this.DoConcept(4007).Value;
                ((PairableScndCnpValue)this.DoConcept(4007)).IncreaseValue(tmp);
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(tmp);
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 4007, 13);
            }
        }

        /// <summary>قانون اضافه کاري 28-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و نه-179 درنظر گرفته شده است</remarks>
        public virtual void R179(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4001            
            //4007 مفهوم اضافه کارساعتي بعد ازوقت

            if (this.DoConcept(4007).Value >= MyRule["First", this.RuleCalculateDate].ToInt() && this.DoConcept(4007).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4007, 13);

                int tmp = MyRule["Second", this.RuleCalculateDate].ToInt() - this.DoConcept(4007).Value;
                ((PairableScndCnpValue)this.DoConcept(4007)).IncreaseValue(tmp);
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(tmp);
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 4007, 13);
            }
        }

        /// <summary>قانون اضافه کاري 29-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هشتاد-180 درنظر گرفته شده است</remarks>
        public virtual void R180(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4001            
            //4007 مفهوم اضافه کارساعتي بعد ازوقت

            if (this.DoConcept(4007).Value >= MyRule["First", this.RuleCalculateDate].ToInt() && this.DoConcept(4007).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4007, 13);

                int tmp = MyRule["Second", this.RuleCalculateDate].ToInt() - this.DoConcept(4007).Value;
                ((PairableScndCnpValue)this.DoConcept(4007)).IncreaseValue(tmp);
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(tmp);
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 4007, 13);
            }
        }

        /// <summary>قانون اضافه کاري 31-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هشتاد و دو-182 درنظر گرفته شده است</remarks>
        public virtual void R182(AssignedRule MyRule)
        {
            //اضافه کار ساعتي ماهانه 4005
            //4010 مفهوم اضافه کارساعتي تعطيل ماهانه

            int x = this.DoConcept(4005).Value - this.DoConcept(4010).Value;
            if (x > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 4010);

                this.DoConcept(4010).Value += x - MyRule["First", this.RuleCalculateDate].ToInt();

                GetLog(MyRule, " After Execute State:", 4010);
            }
        }

        /// <summary>قانون مقداردهی C51</summary>        
        public virtual void R230(AssignedRule MyRule)
        {
            throw new Exception("از رده خارج - بصورت پارامتر ارسال گردید");
            this.DoConcept(3013).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }


        /// <summary>قانون مقداردهی C71</summary>        
        public virtual void R236(AssignedRule MyRule)
        {
            this.DoConcept(5003).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C86</summary>        
        public virtual void R237(AssignedRule MyRule)
        {
            throw new Exception("از رده خارج - بصورت پارامتر ارسال گردید");
            this.DoConcept(2011).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C102</summary>        
        public virtual void R242(AssignedRule MyRule)
        {
            this.DoConcept(1015).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C141</summary>        
        public virtual void R248(AssignedRule MyRule)
        {
            this.DoConcept(5007).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C163</summary>        
        public virtual void R249(AssignedRule MyRule)
        {
            this.DoConcept(5011).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C134</summary>        
        public virtual void R251(AssignedRule MyRule)
        {
            throw new Exception("از رده خارج - تکراری");
            this.DoConcept(1021).Value = 1;// MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C135</summary>        
        public virtual void R252(AssignedRule MyRule)
        {
            this.DoConcept(1022).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C341</summary>        
        public virtual void R253(AssignedRule MyRule)
        {
            this.DoConcept(341).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C346</summary>        
        public virtual void R254(AssignedRule MyRule)
        {
            ((PairableScndCnpValue)this.DoConcept(21)).AddPairs(Operation.Intersect(this.DoConcept(1),
                                                  new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
        }

        /// <summary>قانون مقداردهی C538</summary>        
        public virtual void R260(AssignedRule MyRule)
        {
            this.DoConcept(1085).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C540</summary>        
        public virtual void R262(AssignedRule MyRule)
        {
            this.DoConcept(1087).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>
        ///  این قانون آخرین قانون است که اجرا میشود و باید کارهای آخر را انجام دهد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R207(AssignedRule MyRule)
        {
            //2 کارکرد خالص ساعتی 
            //4 کارکرد خالص روزانه
            //3028 غيبت ساعتی غیرمجاز
            //3004 غیبت ساعتی روزانه

            GetLog(MyRule, " After Execute State:", 4, 3028, 3004);
            if (this.DoConcept(2).Value == 0)
            {
                this.DoConcept(4).Value = 0;
            }
            if (this.DoConcept(3028).Value > 0 && this.DoConcept(2).Value == 0 && this.DoConcept(3004).Value == 0)
            {
                this.DoConcept(3004).Value = 1;
                this.DoConcept(3028).Value = 0;
            }
            GetLog(MyRule, " After Execute State:", 4, 3028, 3004);
        }

        /// <summary>
        ///  اعمال اضافه کار کارتی بر روی اضافه کار مجاز ساعتی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R206(AssignedRule MyRule)
        {
            //4017 اضافه کار مجاز کارتی
            //4002 اضافه کارساعتي مجاز
            GetLog(MyRule, " Before Execute State:", 4002);
            ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(this.DoConcept(4017));
            GetLog(MyRule, " After Execute State:", 4002);
        }

        #endregion

        #region قوانين متفرقه


        /// <summary>قانون متفرقه 4-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنج-5 درنظر گرفته شده است</remarks>
        /// <!--اين قانون بايد بعد از تبديل غيبت هاي ساعتي به روزانه اجرا شود-->
        public virtual void R1001(AssignedRule MyRule)
        {
            //3034 غيبت ساعتي ماهانه
            //4005 اضافه کارساعتي ماهانه
            //8 کارکردخالص ساعتي ماهانه
            GetLog(MyRule, " Before Execute State:", 3034, 4005, 8, 3006);
            if (this.DoConcept(4005).Value >= this.DoConcept(3034).Value)
            {
                this.DoConcept(4005).Value -= this.DoConcept(3034).Value;
                this.DoConcept(8).Value += this.DoConcept(3034).Value;
                this.DoConcept(3034).Value = 1;
            }
            else
            {
                this.DoConcept(8).Value += this.DoConcept(4005).Value;
                this.DoConcept(3034).Value -= this.DoConcept(4005).Value;
                this.DoConcept(4005).Value = 1;
            }
            GetLog(MyRule, " Before Execute State:", 3034, 4005, 8, 3006);
        }

        /// <summary>
        /// ترددها یک در میان ورود و خروج شود
        /// در ترافیک مپر استفاده میشود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1002(AssignedRule MyRule)
        {
        }

        /// <summary>قانون متفرقه 5-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفت-7 درنظر گرفته شده است</remarks>
        public virtual void R1003(AssignedRule MyRule)
        {
            //کارکرد خالص 2
            //کارکرد ناخالص 13
            //3008  تاخير خالص ساعتي           
            //مدت تاخيرمجاز 44  
            //اضافه کارآخروقت 4007
            //3021 تاخیر ساعتی مجاز
            //3028 غيبت ساعتی غیرمجاز
            //80 اگر تاخیر یا تعجیل بیشتر از حد شد این قانون ها اجرا نشود
            //3029 تاخير  ساعتي غیر مجاز       
            //1082 مجموع انواع مرخصی ساعتی که حقوق تعلق میگیرد           


            GetLog(MyRule, " Before Execute State:", 4007, 3021, 2, 4002, 3028, 3029);
            this.DoConcept(1082);

            if (this.DoConcept(3029).Value <= MyRule["First", this.RuleCalculateDate].ToInt()
                || MyRule["NotApplyIfGreater", this.RuleCalculateDate].ToInt() == 0)
            {
                int tmp = Operation.Minimum(this.DoConcept(3029).Value,
                                            this.DoConcept(4007).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                ((PairableScndCnpValue)this.DoConcept(3029)).DecreasePairFromLast(tmp);
                ((PairableScndCnpValue)this.DoConcept(3028)).DecreasePairFromLast(tmp);
                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(tmp);
                this.DoConcept(2).Value += tmp;
                this.ReCalculate(4007, 3021);
            }
            GetLog(MyRule, " After Execute State:", 4007, 3021, 2, 4002, 3028, 3029);
        }

        /// <summary>قانون متفرقه 5-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشت-8 درنظر گرفته شده است</remarks>
        public virtual void R1004(AssignedRule MyRule)
        {
            //3010 تعجيل خالص ساعتي
            //اضافه کاراول وقت 4008
            //1082 مجموع انواع مرخصی ساعتی که حقوق تعلق میگیرد
            this.DoConcept(1082);
            GetLog(MyRule, " Before Execute State:", 3030, 4002, 2, 4008);

            if (this.DoConcept(3030).Value > this.DoConcept(3012).Value)
            {
                if (this.DoConcept(3030).Value <= MyRule["First", this.RuleCalculateDate].ToInt()
                    || MyRule["NotApplyIfGreater", this.RuleCalculateDate].ToInt() == 0)
                {
                    int tmp = Operation.Minimum(this.DoConcept(3030).Value,
                                                this.DoConcept(4008).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3030)).DecreasePairFromFirst(tmp);
                    ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromFirst(tmp);
                    this.DoConcept(2).Value += tmp;
                    this.ReCalculate(4008);
                }
            }

            GetLog(MyRule, " After Execute State:", 3030, 4002, 2, 4008);
        }

        /// <summary>قانون متفرقه 9-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سيزده-13 درنظر گرفته شده است</remarks>
        public virtual void R1005(AssignedRule MyRule)
        {
            //غيبت ساعتي 3001
            //اضافه کار ساعتي 56
            //3028 غيبت ساعتی غیرمجاز
            //4002 اضافه کارساعتي مجاز
            //2 کارکردخالص ساعتي
            //4017 اضافه کار مجاز کارتی
            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
            if (proceedTraffic.IsFilled)
            {
                GetLog(MyRule, " Before Execute State:", 3020, 3028, 4002, 4017, 2);
                if (this.DoConcept(4002).Value > 0 && this.DoConcept(3028).Value > 0)
                {
                    int tmp = Operation.Minimum(this.DoConcept(3028).Value,
                                                this.DoConcept(4002).Value,
                                                MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3028)).DecreasePairFromLast(tmp);
                    this.DoConcept(3020).Value += tmp;
                    ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(tmp);
                    this.DoConcept(2).Value += tmp;
                }

                if (this.DoConcept(4017).Value > 0 && this.DoConcept(3028).Value > 0)
                {
                    int tmp = Operation.Minimum(this.DoConcept(3028).Value,
                                                this.DoConcept(4017).Value,
                                                MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3028)).DecreasePairFromLast(tmp);
                    this.DoConcept(3020).Value += tmp;
                    ((PairableScndCnpValue)this.DoConcept(4017)).DecreasePairFromLast(tmp);
                    this.DoConcept(2).Value += tmp;
                }
                GetLog(MyRule, " After Execute State:", 3020, 3028, 4002, 4017, 2);
            }
        }

        /// <summary>قانون متفرقه 9-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهارده-14 درنظر گرفته شده است</remarks>
        public virtual void R1006(AssignedRule MyRule)
        {
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //4005 اضافه کارساعتي مجاز ماهانه
            //8 کارکردخالص ساعتي ماهانه
            if (this.DoConcept(4005).Value > 0 && this.DoConcept(3034).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 4005, 3034, 8);
                int tmp = Operation.Minimum(this.DoConcept(4005).Value, this.DoConcept(3034).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                this.DoConcept(3034).Value -= tmp;
                this.DoConcept(4005).Value -= tmp;
                this.DoConcept(8).Value += tmp;
                if (this.DoConcept(3034).Value <= 0)
                {
                    this.DoConcept(3034).Value = 1;
                }
                if (this.DoConcept(4005).Value <= 0)
                {
                    this.DoConcept(4005).Value = 1;
                }
                GetLog(MyRule, " After Execute State:", 4005, 3034, 8);
            }

        }

        /// <summary>متفرقه 10-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي تاخير-15 درنظر گرفته شده است</remarks>
        public virtual void R1007(AssignedRule MyRule)
        {
            //تاخير ساعتي ماهانه 3009
            //مرخصي استحقاقي ساعتي ماهانه 1011
            //غيبت ساعتي ماهانه 3034
            GetLog(MyRule, " Before Execute State:", 3034, 3009, 1011);
            int tmp = Operation.Minimum(this.DoConcept(3009).Value,
                                         MyRule["First", this.RuleCalculateDate].ToInt(), this.Person.GetRemainLeave(this.RuleCalculateDate));

            if (tmp > 0)
            {
                this.DoConcept(3034).Value -= tmp;
                this.DoConcept(3009).Value -= tmp;
                this.DoConcept(1011).Value += tmp;
                this.Person.AddUsedLeave(this.RuleCalculateDate, tmp, null);
                if (this.DoConcept(3034).Value < 0)
                {
                    this.DoConcept(3034).Value = 0;
                }
            }

            GetLog(MyRule, " After Execute State:", 3034, 3009, 1011);
        }

        /// <summary>متفرقه 14-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفده-17 درنظر گرفته شده است</remarks>
        public virtual void R1008(AssignedRule MyRule)
        {
            //تقويم تعطيل رسمي 1
            //ایاب و ذهاب 5001
            GetLog(MyRule, " Before Execute State:", 5001);
            this.DoConcept(5001).Value = 0;
            if (MyRule["First", this.RuleCalculateDate].ToInt() == 1 && this.DoConcept(1).Value > 0)
            {
                this.DoConcept(5001).Value = 1;
            }
            if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1 && EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
            {
                this.DoConcept(5001).Value = 1;
            }
            if (MyRule["Third", this.RuleCalculateDate].ToInt() == 1 && (this.DoConcept(2005).Value > 0 || this.DoConcept(2008).Value > 0))
            {
                this.DoConcept(5001).Value = 1;
            }
            else if (MyRule["Fourth", this.RuleCalculateDate].ToInt() == 1 && this.Person.GetProceedTraficByDate(this.RuleCalculateDate).PairCount > 0)
            {
                this.DoConcept(5001).Value = 1;
            }
            GetLog(MyRule, " After Execute State:", 5001);
        }

        /// <summary>قانون متفرقه- مقداردهی به روز ناهاری
        /// </summary>
        public virtual void R1009(AssignedRule MyRule)
        {
            //5014 کارکرد لازم برای حق غذا
            //5015 حق غذا ماهانه
            //2023 مجموع ماموريت ساعتي
            //1 حضور

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 5014);

                if (this.DoConcept(1).Value + this.DoConcept(2023).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    DoConcept(5014).Value = 1;
                    this.ReCalculate(5015);
                }

                GetLog(MyRule, " After Execute State:", 5014);
            }
        }

        /// <summary>متفرقه 16-3</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نوزده-19 درنظر گرفته شده است</remarks>
        public virtual void R1010(AssignedRule MyRule)
        {
            //16 شب کاري
            //کارکرد خالص شب 15
            //4012 اضافه کارساعتي مجازشب

            GetLog(MyRule, " Before Execute State:", 16);
            //if (this.DoConcept`.Value + this.DoConcept(4012).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
            //{
            //    this.DoConcept(16).Value = 1;
            //}
            //else
            //{
            //    this.DoConcept(16).Value = 0;
            //}
            GetLog(MyRule, " After Execute State:", 16);

        }

        /// <summary>قانون متفرقه 19-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و يک-21 درنظر گرفته شده است</remarks>
        public virtual void R1011(AssignedRule MyRule)
        {
            //3031 غیبت بین وقت ساعتی غیرمجاز
            //3028 غیبت غیر مجاز ساعتی
            //2 کارکردخالص ساعتي
            GetLog(MyRule, " Before Execute State:", 3031, 3028, 2);
            int t = Operation.Intersect(this.DoConcept(3031), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), (int)MyRule["Second", this.RuleCalculateDate].ToInt())).Value;
            int t4 = Operation.Minimum(t, MyRule["Third", this.RuleCalculateDate].ToInt());
            int t5 = t - t4;
            ((PairableScndCnpValue)this.DoConcept(3031)).DecreasePairFromLast(t4);
            this.DoConcept(2).Value += t4;
            this.DoConcept(3028).Value -= t4;
            GetLog(MyRule, " After Execute State:", 3031, 3028, 2);
        }

        /// <summary>1-23 قانون متفرقه</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي یکصدو یازده -112 درنظر گرفته شده است</remarks>
        public virtual void R1012(AssignedRule MyRule)
        {
            //4002 اضافه کارمجاز ساعتي
            //4003 اضافه کاری غیر مجاز
            //4023 زمان ناهار
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4023);

            ((PairableScndCnpValue)this.DoConcept(4023))
                                          .AddPair(new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt()));

            ((PairableScndCnpValue)this.DoConcept(4003))
                                            .AppendPairs(Operation.Intersect(this.DoConcept(4002), this.DoConcept(4023)));
            ((PairableScndCnpValue)this.DoConcept(4002))
                                            .AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4023)));



            GetLog(MyRule, " After Execute State:", 4002, 4003, 4023);
        }

        /// <summary>1-24 قانون متفرقه</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي یکصدو یازده -113 درنظر گرفته شده است</remarks>
        public virtual void R1013(AssignedRule MyRule)
        {
            //4002 اضافه کارمجاز ساعتي
            //4003 اضافه کاری غیر مجاز
            GetLog(MyRule, " Before Execute State:", 4002, 4003);

            ((PairableScndCnpValue)this.DoConcept(4023))
                                         .AddPair(new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt()));


            ((PairableScndCnpValue)this.DoConcept(4003))
                                            .AppendPairs(Operation.Intersect(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            ((PairableScndCnpValue)this.DoConcept(4002))
                                            .AddPairs(Operation.Differance(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));

            GetLog(MyRule, " After Execute State:", 4002, 4003);
        }

        /// <summary>متفرقه 25-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و سه-23 درنظر گرفته شده است</remarks>
        public virtual void R1014(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003);

            ((PairableScndCnpValue)this.DoConcept(4003))
                                            .AddPairs(Operation.Intersect(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            ((PairableScndCnpValue)this.DoConcept(4002))
                                            .AddPairs(Operation.Differance(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            GetLog(MyRule, " After Execute State:", 4002, 4003);
        }

        /// <summary>
        /// پایان شبانه روز
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1015(AssignedRule MyRule)
        {
            //5018 پایان شبانه روز
            GetLog(MyRule, " Before Execute State:", 5018);
            this.DoConcept(5018).Value = MyRule["first", this.RuleCalculateDate].ToInt();
            GetLog(MyRule, " After Execute State:", 5018);
        }

        /// <summary>
        /// وظیفه اجرای مفاهیم ماهانه
        /// R97
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1016(AssignedRule MyRule)
        {
            this.DoConcept(9);//حضورماهانه
            this.DoConcept(4005);//اضافه کارساعتي مجاز ماهانه
            this.DoConcept(4006);//اضافه کارساعتي غیرمجاز ماهانه
            this.DoConcept(4020);//اضافه کار بین وقت ماهانه            
            this.DoConcept(4010);//اضافه کار تعطیل ماهانه            
            //this.DoConcept(4031);//اضافه کار غیر تعطیل ماهانه    
            this.DoConcept(4019);//اضافه کار اول مجاز ماهانه            
            this.DoConcept(4021);//اضافه کار آخر مجاز ماهانه      
            this.DoConcept(4036);//اضافه کار اول وقت غیر مجاز ماهانه            
            this.DoConcept(4037);//اضافه کار آخر وقت غیر مجاز ماهانه      

            this.DoConcept(3026);//غيبت ساعتي مجاز ماهانه
            this.DoConcept(3034);//غیبت ساعتی غیرمجاز ماهانه
            this.DoConcept(1006);//مرخصي استحقاقي روزانه ماهانه
            this.DoConcept(1011);//مرخصي استحقاقي ساعتي ماهانه
            this.DoConcept(3032);//تاخیر ساعتی غیرمجاز ماهانه
            this.DoConcept(3033);//تعجیل ساعتی غیرمجاز ماهانه

            this.DoConcept(1101);// مجموع انواع مرخصی ساعتی با حقوق ماهانه
            this.DoConcept(1102);
            this.DoConcept(1103);
            this.DoConcept(1104);
            this.DoConcept(1111); //مرخصی بی حقوق روزانه ماهانه_34
            this.DoConcept(1112);  //مرخصی بی حقوق روزانه ماهانه_35
           

            this.DoConcept(1073);//مرخصی بی حقوق ساعتی ماهانه_11
            this.DoConcept(1105);//مرخصی بی حقوق ساعتی ماهانه_12
            this.DoConcept(1106);//مرخصی بی حقوق ساعتی ماهانه_13
            this.DoConcept(1107);//مرخصی بی حقوق ساعتی ماهانه_14
           this.DoConcept(1108);//مرخصی بی حقوق ساعتی ماهانه_15
                     
            this.DoConcept(1074);//مرخصی بی حقوق روزانه ماهانه_31
            this.DoConcept(1075);//مرخصی بی حقوق ساعتی ماهانه
            this.DoConcept(1076);//مرخصي بي حقوق روزانه ماهانه
            this.DoConcept(1016);//مرخصی استعلاجی ساعتی ماهانه
            this.DoConcept(3005);//غيبت روزانه ماهانه

            this.DoConcept(8);//کارکردخالص ساعتي ماهانه
            this.DoConcept(5);//کارکردخالص روزانه ماهانه
            this.DoConcept(2007);//ماموريت ساعتي ماهانه
            this.DoConcept(2006);//ماموريت روزانه ماهانه
            this.DoConcept(3);//کارکردناخالص ماهانه
            this.DoConcept(10);//کارکردلازم ماهانه
            this.DoConcept(1017);//مرخصی استعلاجی ماهانه
            this.DoConcept(5013);//کد وضعیت روز جهت رنگ ماهانه
            this.DoConcept(5015);//کارکرد لازم برای حق غذا ماهانه
            this.DoConcept(5016);//طول دوره محدوده محاسبات ماهانه
            this.DoConcept(5023);//وضعیت روز ماهانه
            

            this.DoConcept(22);//حضور در تعطیلات خاص ماهانه
            this.DoConcept(1097);// مجموع مرخصی با حقوق روزانه ماهانه

            this.DoConcept(26);//کارکرد خالص شب ماهانه

            this.DoConcept(3005);//غيبت روزانه ماهانه
            this.DoConcept(3041);//غیبت مجاز شیردهی ماهانه
            this.DoConcept(3043);//غیبت مجاز مهد ماهانه
            this.DoConcept(3045);//غیبت مجاز تقليل ماهانه

            this.DoConcept(5019);//ایاب ذهاب ماهانه
            this.DoConcept(5020);//نوبت کاری
            this.DoConcept(5025);//تعداد تعطیل کاری ماهانه

            this.DoConcept(2052);
            this.DoConcept(2053);
            this.DoConcept(2054);
            this.DoConcept(2055);
            this.DoConcept(2057);


        }

        /// <summary>
        /// وظیفه اجرای مفاهیم روزانه
        /// R98
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1017(AssignedRule MyRule)
        {
            this.DoConcept(5008);//نوع
            this.DoConcept(1);//مفهوم حضور
            this.DoConcept(2);//کارکردخالص ساعتي
            this.DoConcept(4);//کارکردخالص روزانه
            this.DoConcept(13);//کارکردناخالص
            this.DoConcept(6);//کارکردلازم
            this.DoConcept(1003);//مرخصي استحقاقي ساعتي
            this.DoConcept(1005);//مرخصي استحقاقي روزانه
            this.DoConcept(1056);//مرخصي بی حقوق ساعتی 12
            this.DoConcept(1058);//مرخصي بی حقوق ساعتی 13
            this.DoConcept(1060);//مرخصي بی حقوق ساعتی 14
            this.DoConcept(1062);//مرخصي بی حقوق ساعتی 15
            this.DoConcept(1064);//مرخصي بی حقوق روزانه 31
            this.DoConcept(1066);//مرخصي بی حقوق روزانه 32
            this.DoConcept(1068);//مرخصي بی حقوق روزانه 33
            this.DoConcept(1070);//مرخصي بی حقوق روزانه 34
            this.DoConcept(1072);//مرخصي بی حقوق روزانه 35
            this.DoConcept(1109);//مجموع انواع مرخصی ساعتی بی حقوق
            this.DoConcept(1110); //مجموع انواع مرخصی  بی حقوق روزانه 
            this.DoConcept(1100);//مجموع انواع مرخصی ساعتی با حقوق
            this.DoConcept(1054);//بیحقوق 11
            this.DoConcept(2023);//ماموريت ساعتي
            this.DoConcept(2005);//ماموريت روزانه
            this.DoConcept(2032);
            this.DoConcept(2033);
            this.DoConcept(2034);
            this.DoConcept(2035);
            this.DoConcept(2056);


            this.DoConcept(3004);//غيبت روزانه
            this.DoConcept(1008);//مرخصی استعلاجی ساعتی
            this.DoConcept(2008);//ماموريت خالص شبانه روزي
            this.DoConcept(1010);//مرخصي استعلاجي روزانه

            this.DoConcept(4007);//اضافه کارآخروقت
            this.DoConcept(4017);//اضافه کار مجاز کارتی
            this.DoConcept(4009);//اضافه کار مجاز تعطیل
            //this.DoConcept(4030);//اضافه کار مجاز غیر تعطیل
            this.DoConcept(4034);//اضافه کار اول وقت غیر مجاز             
            this.DoConcept(4035);//اضافه کار آخر وقت غیر مجاز  

            this.DoConcept(5010);//ثبت دستی تردد
            //this.DoConcept(164);//خنثی کردن غیبت توسط مرخصی
            //this.DoConcept(165);//تاخیر سرویس
            this.DoConcept(5009);//نوع روز
            this.DoConcept(5012);//کد وضعیت روز جهت رنگ
            this.DoConcept(5014);//کارکرد لازم برای حق غذا
            this.DoConcept(5017);//حضور منهای اضافه کار در گانت چارت استفاده می گردد
            this.DoConcept(5022);//وضعیت روز

            this.DoConcept(1096);// مجموعه مرخصی با حقوق روزانه

            this.DoConcept(3040);// تاخیر مجاز شیردهی
            this.DoConcept(3042);//  غیبت مجاز مهد
            this.DoConcept(3044);// غیبت مجاز تقليل رفاه
        }
        /// <summary>
        ///  این قانون وضعیت رنگ روز را مشخص میکمد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1018(AssignedRule MyRule)
        {
            //5012 رنگ روز
            //تقويم تعطيل رسمي 1

            //3029 کد رنگ تعطیل رسمی
            //1015 کد رنگ تردد ناقص
            //103 کد رنگ غیبت روزانه
            //104 کد رنگ غیبت ساعتی
            //اگر عدد بیشتر از 200 بود بدین معناست که در روز تعطیل رسمی
            //یکی از موارد فوق اتفاق افتاده است


            GetLog(MyRule, " After Execute State:", 5012);

            #region Show Colors
            bool holiday = false, traffics = false, hourlyAbsence = false, dailyAbsence = false, permit = false, manualTraffic = false;

            #region holiday

            if (MyRule.HasParameter("1th", this.RuleCalculateDate))
            {
                if (MyRule["1th", this.RuleCalculateDate].ToInt() == 1)
                {
                    holiday = true;
                }
            }
            else
            {
                holiday = true;
            }
            #endregion

            #region Tafics
            if (MyRule.HasParameter("2th", this.RuleCalculateDate))
            {
                if (MyRule["2th", this.RuleCalculateDate].ToInt() == 1)
                {
                    traffics = true;
                }
            }
            else
            {
                traffics = true;
            }
            #endregion

            #region hourly Absence
            if (MyRule.HasParameter("3th", this.RuleCalculateDate))
            {
                if (MyRule["3th", this.RuleCalculateDate].ToInt() == 1)
                {
                    hourlyAbsence = true;
                }
            }
            else
            {
                hourlyAbsence = true;
            }
            #endregion

            #region dailyAbsence
            if (MyRule.HasParameter("4th", this.RuleCalculateDate))
            {
                if (MyRule["4th", this.RuleCalculateDate].ToInt() == 1)
                {
                    dailyAbsence = true;
                }
            }
            else
            {
                dailyAbsence = true;
            }
            #endregion

            #region permits
            if (MyRule.HasParameter("5th", this.RuleCalculateDate))
            {
                if (MyRule["5th", this.RuleCalculateDate].ToInt() == 1)
                {
                    permit = true;
                }
            }
            else
            {
                permit = true;
            }
            #endregion

            #region manual Traffic
            if (MyRule.HasParameter("6th", this.RuleCalculateDate))
            {
                if (MyRule["6th", this.RuleCalculateDate].ToInt() == 1)
                {
                    manualTraffic = true;
                }
            }
            else
            {
                manualTraffic = true;
            }
            #endregion

            #endregion

            this.DoConcept(5012).Value = 1;
            this.DoConcept(5013).Value = 5;
            this.DoConcept(5012).FromPairs = "#Transparent;";
            this.DoConcept(5012).ToPairs = "";

            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            #region not fielld traffic
            if (ProceedTraffic.PairCount > 0 && !ProceedTraffic.IsFilled)
            {
                if (traffics)
                {
                    this.DoConcept(5012).FromPairs = "#FFFF40;";//Yellow
                    this.DoConcept(5012).ToPairs = String.Format("fa:{0};en:{1}", "تردد ناقص", "Uncomplete Traffic");
                }
            }
            #endregion

            #region daily absent
            else if (this.DoConcept(3004).Value > 0)
            {
                if (dailyAbsence)
                {
                    this.DoConcept(5012).FromPairs = this.DoConcept(3004).Concept.Color + ";";
                    this.DoConcept(5012).ToPairs = String.Format("fa:{0};en:{1}", this.DoConcept(3004).Concept.FnName, this.DoConcept(3004).Concept.EnName);
                }
            }
            #endregion

            #region hourly absent
            else if (this.DoConcept(3028).Value > 0)
            {
                if (hourlyAbsence)
                {
                    this.DoConcept(5012).FromPairs = this.DoConcept(3028).Concept.Color + ";";
                    this.DoConcept(5012).ToPairs = String.Format("fa:{0};en:{1}", this.DoConcept(3028).Concept.FnName, this.DoConcept(3028).Concept.EnName);
                }
            }
            #endregion

            #region permit
            else
            {
                IList<Permit> permits = this.Person.GetPermitByDate(this.ConceptCalculateDate);
                if (permits.Count > 0)
                {
                    if (permit)
                    {
                        string faTitle = "";
                        string enTitle = "en:";
                        foreach (Permit p in permits)
                        {
                            foreach (PermitPair pair in p.Pairs)
                            {
                                if (faTitle.Length > 0)
                                    faTitle += " - ";
                                faTitle += pair.Precard.Name;
                            }
                        }
                        faTitle = "fa:" + faTitle;
                        this.DoConcept(5012).FromPairs = "#009900;";
                        this.DoConcept(5012).ToPairs = faTitle + ";" + enTitle;
                    }
                }
            }
            #endregion

            //this.DoConcept(5012).Value keep xor of number like 11010 which second number is holiday(2) and other is manual
            //traffics (4,8,16 ,...)
            #region holiday
            if ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1")
                    || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                && holiday)
            {
                this.DoConcept(5012).Value = 2;
                this.DoConcept(5012).FromPairs += "#FF0000;";//Red
            }
            else
            {
                this.DoConcept(5012).FromPairs += "BLACK;";
            }
            #endregion

            #region Manual Traffic
            if (this.Person.BasicTrafficList != null
                //&&this.Person.BasicTrafficList.Any(x => x.Date == this.RuleCalculateDate && x.Manual)
                && ProceedTraffic.Pairs != null
                && manualTraffic)
            {
                string manaulTrafficColor = "#6666FF;";
                int binaryIndex = 2;//the number of 2,4,8,16 ...
                foreach (ProceedTrafficPair pair in ProceedTraffic.Pairs)
                {
                    binaryIndex *= 2;
                    if (this.Person.BasicTrafficList.Any(x => x.Date == this.RuleCalculateDate && x.Manual && x.ID == pair.BasicTrafficIdFrom))
                    {
                        this.DoConcept(5012).Value ^= binaryIndex;
                    }
                    else if (pair.PermitIdFrom > 0)
                    {
                        this.DoConcept(5012).Value ^= binaryIndex;
                    }
                    binaryIndex *= 2;
                    if (this.Person.BasicTrafficList.Any(x => x.Date == this.RuleCalculateDate && x.Manual && x.ID == pair.BasicTrafficIdTo))
                    {
                        this.DoConcept(5012).Value ^= binaryIndex;
                    }
                    else if (pair.PermitIdTo > 0)
                    {
                        this.DoConcept(5012).Value ^= binaryIndex;
                    }
                }
                if (this.DoConcept(5012).Value > 2)
                {
                    this.DoConcept(5012).FromPairs += manaulTrafficColor;
                }
                else
                {
                    this.DoConcept(5012).FromPairs += "Transparent;";
                }
            }
            #endregion

            GetLog(MyRule, " After Execute State:", 5012);

        }

        /// <summary>درج تردد مجازی در پایان شبانه روز</summary>
        /// 
        public virtual void R1019(AssignedRule MyRule)
        {
        }


        /// <summary>
       /// ماهانه تا .... کسر کار به مرخصی تبدیل شود
       /// </summary>
       /// <param name="MyRule"></param>
        public virtual void R1020(AssignedRule MyRule)
        {
            //1011 مرخصي استحقاقي ساعتي ماهانه
            //غيبت ساعتي غيرمجاز ماهانه 3034
            //3026 غيبت ساعتي مجاز ماهانه
            //8 کارکردخالص ساعتي ماهانه
            //110 مفهوم مرخصي بايد به کارکرد خالص اضافه شود
            //3 کارکردناخالص ماهانه
            if (this.DoConcept(3034).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 1011, 8, 3, 3034);
                int max = MyRule["first", this.RuleCalculateDate].ToInt();
                int remain = this.Person.GetRemainLeave(this.RuleCalculateDate);
                int absence = this.DoConcept(3034).Value;
                if (absence > max)
                {
                    absence = max;
                }
                if (absence <= remain)
                {
                    this.DoConcept(1011).Value += absence;
                    this.DoConcept(8).Value += absence;
                    this.DoConcept(3).Value += absence;
                    this.DoConcept(3034).Value -= absence;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, absence, null);
                }
                else
                {
                    this.DoConcept(1011).Value += remain;
                    this.DoConcept(8).Value += remain;
                    this.DoConcept(3).Value += remain;
                    this.DoConcept(3034).Value -= remain;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, remain, null);
                }
                if (this.DoConcept(3034).Value == 0) 
                {
                    this.DoConcept(3034).Value = 1;
                }
                GetLog(MyRule, " After Execute State:", 1011, 8, 3, 3034);
            }
        }


        /// <summary>
        /// تردد اتوماتیک
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1021(AssignedRule MyRule)
        {

        }


        #endregion

        #region قوانين کارکرد

        /// <summary>قانون مقداردهی C14</summary>        
        public virtual void R2001(AssignedRule MyRule)
        {
            PairableScndCnpValue.AppendPairToScndCnpValue(new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt()), this.DoConcept(14));

            this.DoConcept(16).Value = 1;

            this.DoConcept(15);

        }

        /// <summary>قانون کارکرد 3-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و شش-27 درنظر گرفته شده است</remarks>
        public virtual void R2002(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //7 کارکرددرروز
            //2 کارکردخالص ساعتي 
            //کارکردخالص روزانه 4
            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            if (this.DoConcept(3004).Value == 0 &&
                this.DoConcept(2).Value > 0 &&
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value == this.DoConcept(7).Value &&
                this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType != ShiftTypesEnum.OVERTIME
                && ProceedTraffic.IsFilled)
            {
                GetLog(MyRule, " Before Execute State:", 4);
                this.DoConcept(4).Value = 1;
                GetLog(MyRule, " After Execute State:", 4);
            }
        }

        /// <summary>قانون کارکرد 4-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و هشت-28 درنظر گرفته شده است</remarks>
        /// کارکرد 4-1: روزهاي کاري بيشتر از (ساعت کارکرد در روز) يک روز کاري حساب شود
        public virtual void R2003(AssignedRule MyRule)
        {
            //6 کارکردلازم
            //7 کارکرددرروز
            //4 کارکردخالص روزانه
            //3004 غيبت روزانه
            //2023 مجموع ماموریت ساعتی
            //1082 مجموع انواع مرخصی ساعتی
            GetLog(MyRule, " Before Execute State:", 4);

            // به مرخصی بی حقوق , کارکرد روزانه و بیمه تعلق نمیگیرد
            if (this.DoConcept(6).Value > this.DoConcept(7).Value && this.DoConcept(1091).Value == 0)
            {
                this.DoConcept(2023);
                this.DoConcept(1082);
                //غیبت روزانه نداشت و همه روز غیبت نداشت
                //قانونی که غیبت ساعتی را به روزانه تبدیل میکند اولویت پایین تری دارد
                if (this.DoConcept(3004).Value == 0
                    &&
                    Operation.Differance(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(3028)).Value > 0)
                {
                    int karkerd = this.DoConcept(13).Value > this.DoConcept(6).Value ? this.DoConcept(6).Value : this.DoConcept(13).Value;

                    if (this.DoConcept(1090).Value >= 1) 
                    {
                        this.DoConcept(4).Value = (int)((float)karkerd / (float)this.DoConcept(1001).Value);
                    }
                    else
                    {
                        this.DoConcept(4).Value = (int)((float)karkerd / (float)this.DoConcept(7).Value);
                    }
                }
            }
            GetLog(MyRule, " After Execute State:", 4);
        }

        /// <summary>قانون کارکرد 5-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و نه-29 درنظر گرفته شده است</remarks>
        /// کارکرد 5-1: روزهاي کاري کمتر از (ساعت کارکرد در روز) يک روز کاري حساب شود
        public virtual void R2004(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4);
            if (this.DoConcept(6).Value > 0)
            {
                if (this.DoConcept(1).Value > 0)
                {
                    if (this.DoConcept(6).Value <= this.DoConcept(7).Value)
                    {
                        this.DoConcept(4).Value = 0;
                        if (this.DoConcept(3004).Value == 0)
                        {
                            this.DoConcept(4).Value = 1;
                        }
                    }
                }
                else
                {
                    if (this.DoConcept(6).Value < this.DoConcept(7).Value && this.DoConcept(2).Value > 0)
                    {
                        this.DoConcept(4).Value = 1;
                    }
                }
            }
            GetLog(MyRule, " Before Execute State:", 4);
        }

        /// <summary>
        /// کد 32 :  مجموع کارکرد لازم هر ماه در داخل شیفت ها  طبق جدول محاسبه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2005(AssignedRule MyRule)
        {
            //11 مفهوم تعداد روز
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018  حداکثر اضافه کار مجاز ماهانه

            //9 حضورماهانه           

            GetLog(MyRule, " Before Execute State:", 3034, 4005, 8, 3);
            int daterangeORder = this.GetDateRange(3, this.ConceptCalculateDate).DateRangeOrder;

            int lazem = MyRule[daterangeORder.ToString() + "th", this.RuleCalculateDate].ToInt() * HourMin;
            this.DoConcept(10).Value = lazem;
            this.DoConcept(3034).Value = 0;
            this.DoConcept(4005).Value = 0;

            if (lazem > this.DoConcept(3).Value)
            {
                int tmp = lazem - this.DoConcept(3).Value;
                this.DoConcept(3034).Value = tmp;
            }
            else
            {
                int tmp = this.DoConcept(3).Value - lazem;
                this.DoConcept(4005).Value = tmp;      
            }
           
            GetLog(MyRule, " Before Execute State:", 3034, 4005, 8, 3);
        }

        /// <summary>
        /// فرض : پرسنل ساعتی که شیفت برای آنها معین نشده است و میزان کارکرد لازم آنها بر اساس پارامتر قانون مشخص شده است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2006(AssignedRule MyRule)
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
            if (this.DoConcept(9).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 8, 10, 3034, 4005, 4006, 4010);

                int lazem = this.DoConcept(1001).Value;//بعلت نداشتن کارکرد لازم ماهانه از این مفهوم استفاده میشود


                int karkerd = this.DoConcept(9).Value + this.DoConcept(1006).Value * lazem
                                                      + this.DoConcept(1017).Value * lazem
                                                      + this.DoConcept(1097).Value * lazem
                                                      + this.DoConcept(1011).Value;

                int daterangeORder = this.GetDateRange(3, this.ConceptCalculateDate).DateRangeOrder;
                int monthlyLazem = MyRule[daterangeORder.ToString() + "th", this.RuleCalculateDate].ToInt();
                this.DoConcept(10).Value = monthlyLazem * HourMin;
                if (karkerd >= monthlyLazem * HourMin)
                {
                    this.DoConcept(8).Value = monthlyLazem * HourMin;
                    this.DoConcept(4005).Value = karkerd - this.DoConcept(10).Value;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(3034).Value = 0;
                    this.DoConcept(3).Value = this.DoConcept(8).Value + this.DoConcept(4005).Value;
                }
                else
                {
                    this.DoConcept(3034).Value = this.DoConcept(10).Value - karkerd;
                    this.DoConcept(4005).Value = 0;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(4010).Value = 0;
                }
                GetLog(MyRule, " After Execute State:", 8, 10, 3034, 4005, 4006, 4010);
            }

            this.DoConcept(3020).Value = 0;
            this.DoConcept(3028).Value = 0;
            this.DoConcept(4002).Value = 0;
            this.DoConcept(4003).Value = 0;
        }

        /// <summary>قانون کارکرد 8-3</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و چهار-34 درنظر گرفته شده است</remarks>
        public virtual void R2007(AssignedRule MyRule)
        {
            //11 مفهوم تعداد روز
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018  حداکثر اضافه کار مجاز ماهانه

            //9 حضورماهانه

            GetLog(MyRule, " Before Execute State:", 8, 3034, 4005, 3);
            int t2 = MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;

            if (t2 > this.DoConcept(8).Value)
            {
                int tmp = t2 - this.DoConcept(10).Value;
                if (this.DoConcept(4005).Value > tmp)
                {
                    this.DoConcept(4005).Value -= tmp;
                    this.DoConcept(8).Value += tmp;
                }
                else
                {
                    this.DoConcept(8).Value += this.DoConcept(4005).Value;
                    this.DoConcept(3034).Value += tmp - this.DoConcept(4005).Value;
                    this.DoConcept(4005).Value = 0;
                }

            }
            else
            {
                if (this.DoConcept(8).Value > t2)
                {
                    this.DoConcept(3034).Value -= this.DoConcept(10).Value + this.DoConcept(8).Value;
                    if (this.DoConcept(3034).Value < 0)
                    {
                        this.DoConcept(3034).Value = 0;
                    }
                    this.DoConcept(4005).Value += this.DoConcept(8).Value - t2;
                    this.DoConcept(8).Value = t2;
                }
                else
                {
                    this.DoConcept(3034).Value -= this.DoConcept(10).Value + t2;
                    if (this.DoConcept(3034).Value < 0)
                    {
                        this.DoConcept(3034).Value = 0;
                    }
                    if (this.DoConcept(4005).Value > this.DoConcept(9).Value)
                    {
                        this.DoConcept(4005).Value = this.DoConcept(9).Value;
                    }
                }
            }
            if (this.DoConcept(4005).Value > this.DoConcept(4018).Value * HourMin)
            {
                this.DoConcept(3).Value -= this.DoConcept(4005).Value + this.DoConcept(4018).Value * HourMin;
                this.DoConcept(4006).Value += this.DoConcept(4005).Value - this.DoConcept(4018).Value * HourMin;
                this.DoConcept(4005).Value = this.DoConcept(4018).Value * HourMin;
            }
            GetLog(MyRule, " After Execute State:", 8, 3034, 4005, 3);
        }

        /// <summary>قانون کارکرد 8-4</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و پنج-35 درنظر گرفته شده است</remarks>
        public virtual void R2008(AssignedRule MyRule)
        {
            //11 مفهوم تعداد روز
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018  حداکثر اضافه کار مجاز ماهانه
            GetLog(MyRule, " Before Execute State:", 8, 3034, 4005, 3);
            int t2 = MyRule["First", this.RuleCalculateDate].ToInt() * this.DoConcept(11).Value;
            if (t2 > this.DoConcept(10).Value)
            {
                int temp = t2 - this.DoConcept(10).Value;
                if (this.DoConcept(4005).Value > temp)
                {
                    this.DoConcept(4005).Value -= temp;
                    this.DoConcept(8).Value += temp;
                }
                else
                {
                    this.DoConcept(8).Value += this.DoConcept(4005).Value;
                    this.DoConcept(3034).Value += temp - this.DoConcept(4005).Value;
                    this.DoConcept(4005).Value = 0;
                }
            }
            else
            {
                this.DoConcept(4005).Value += this.DoConcept(10).Value - t2;

                if (this.DoConcept(8).Value > t2)
                {
                    this.DoConcept(8).Value = t2;
                }
            }
            this.DoConcept(10).Value = t2;
            if (this.DoConcept(4005).Value > this.DoConcept(4018).Value * HourMin)
            {
                this.DoConcept(3).Value -= this.DoConcept(3).Value + this.DoConcept(4018).Value * HourMin;
                this.DoConcept(4006).Value += this.DoConcept(4005).Value - this.DoConcept(4018).Value * HourMin;
                this.DoConcept(4005).Value = this.DoConcept(4018).Value * HourMin;
            }
            GetLog(MyRule, " After Execute State:", 8, 3034, 4005, 3);
        }

        /// <summary>قانون کارکرد 8-6</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دویست و هشتاد-280 درنظر گرفته شده است</remarks>
        /// توسط این قانون تمامی قوانین محاسبه غیبت ماهانه، اضافه کار ماهانه نادیده
        /// گرفته شده و با توجه به پارامتر ورودی غیبت ماهانه و اضافه کارماهانه محاسبه می گردد
        public virtual void R2009(AssignedRule MyRule)
        {
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه

            //4010 اضافه کارساعتي تعطيل ماهانه
            //4019 مفهوم اضافه کار قبل از وقت ماهانه
            //4020 مفهوم اضافه کار بین وقت ماهانه
            //4021 مفهوم اضافه کار بعد از وقت ماهانه
            //625 مفهوم اضافه کار روز غیر کاری ماهانه
            //626 مفهوم اضافه کارساعتی جمعه ماهانه
            //627 مفهوم اضافه کارساعتی غیر مجاز جمعه ماهانه
            //628 مفهوم اضافه کارساعتی مجازتعطیل غیرجمعه ماهانه
            //629 مفهوم اضافه کارساعتی غیر مجازتعطیل غیرجمعه ماهانه
            GetLog(MyRule, " Before Execute State:", 3, 4010, 3005, 8, 3034, 4005, 4006);
            this.DoConcept(10).Value = MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
            this.DoConcept(13);
            this.ReCalculate(3);
            if (this.DoConcept(3).Value >= (MyRule["First", this.RuleCalculateDate].ToInt() * HourMin))
            {
                this.DoConcept(8).Value = MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
                this.DoConcept(4005).Value = this.DoConcept(3).Value - (MyRule["First", this.RuleCalculateDate].ToInt() * HourMin);
                this.DoConcept(4006).Value = 0;
                this.DoConcept(3034).Value = 0;
                this.DoConcept(3005).Value = 0;
            }
            else
            {
                this.DoConcept(8).Value = this.DoConcept(3).Value;
                this.DoConcept(3034).Value = (MyRule["First", this.RuleCalculateDate].ToInt() * HourMin) - this.DoConcept(3).Value;
                this.DoConcept(3005).Value = 0;
                this.DoConcept(4005).Value = 0;
                this.DoConcept(4006).Value = 0;
                this.DoConcept(4010).Value = 0;

            }

            GetLog(MyRule, " After Execute State:", 3, 4010, 3005, 8, 3034, 4005, 4006);
        }

        /// <summary>قانون کارکرد 8-7</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دویست و هشتاد و یک-280 درنظر گرفته شده است</remarks>
        public virtual void R2010(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي ماهانه 8
            //حضورماهانه 9
            //10 کارکردلازم ماهانه
            //3 کارکردناخالص ماهانه
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه
            //3020 غیبت ساعتی مجاز ماهانه
            //4005 اضافه کارساعتي مجاز ماهانه

            //4010 اضافه کارساعتي تعطيل ماهانه
            //4019 مفهوم اضافه کار قبل از وقت ماهانه
            //4020 مفهوم اضافه کار بین وقت ماهانه
            //4021 مفهوم اضافه کار بعد از وقت ماهانه
            //625 مفهوم اضافه کار روز غیر کاری ماهانه
            //626 مفهوم اضافه کارساعتی جمعه ماهانه
            //627 مفهوم اضافه کارساعتی غیر مجاز جمعه ماهانه
            //628 مفهوم اضافه کارساعتی مجازتعطیل غیرجمعه ماهانه
            //629 مفهوم اضافه کارساعتی غیر مجازتعطیل غیرجمعه ماهانه
            GetLog(MyRule, " Before Execute State:", 4010, 3005, 8, 3034, 4005, 10, 4019, 4020, 4021);
            this.DoConcept(8).Value = this.DoConcept(3).Value;
            this.DoConcept(10).Value = 0;
            this.DoConcept(3034).Value = 0;
            this.DoConcept(3005).Value = 0;
            this.DoConcept(4005).Value = 0;
            this.DoConcept(4019).Value = 0;
            this.DoConcept(4020).Value = 0;
            this.DoConcept(4021).Value = 0;
            this.DoConcept(4010).Value = 0;


            GetLog(MyRule, " After Execute State:", 4010, 3005, 8, 3034, 4005, 10, 4019, 4020, 4021);
        }

        /// <summary>
        /// حد اکثر سقف کارکرد در ماه - پرسنل ساعتی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2011(AssignedRule MyRule)
        {
            //3 کارکردناخالص ماهانه
            //4005 اضافه کار غیر مجاز ماهانه
            GetLog(MyRule, " Before Execute State:", 3, 4005);
            int maxTime = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(13);
            if (this.DoConcept(3).Value > maxTime)
            {
                this.DoConcept(4005).Value = this.DoConcept(3).Value - maxTime;
                this.DoConcept(3).Value = maxTime;
            }


            GetLog(MyRule, " After Execute State:", 3, 4005);
        }

        /// <summary>
        /// کارکرد در روز
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2012(AssignedRule MyRule)
        {
            this.DoConcept(7).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>
        /// اعمال کارکرد در تعطیلات
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2013R(AssignedRule MyRule)
        {
            //23 مفهوم تعطیلات جزو کارکرد حساب شود
            //24 مفهوم تعطیلات رسمی جزو کارکرد حساب شود 
            //25 مفهوم روزهای غیر کاری جزو کارکرد حساب شود   
            //4 کارکرد خالص روزانه
            //6 مفهوم کارکردلازم
            //3004 غيبت روزانه
            //7 کارکرددرروز    

            bool hourlyWork = true, dailyWork = true;
            if (MyRule.HasParameter("HourlyWork", this.RuleCalculateDate))
            {
                hourlyWork = MyRule["HourlyWork", this.RuleCalculateDate].ToInt() > 0;
            }
            if (MyRule.HasParameter("DailyWork", this.RuleCalculateDate))
            {
                dailyWork = MyRule["DailyWork", this.RuleCalculateDate].ToInt() > 0;
            }

            this.DoConcept(23).Value = MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt();
            this.DoConcept(24).Value = MyRule["TatilRasmi", this.RuleCalculateDate].ToInt();
            this.DoConcept(25).Value = MyRule["GheirKari", this.RuleCalculateDate].ToInt();
            bool lastDay = this.CalcDateZone.ToDate.Equals(this.RuleCalculateDate);
            if ((this.DoConcept(6).Value > 0
                && (EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1", "2")
                || this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0))
                || lastDay)//روزهای آخر رینج محاسبات
            {

                GetLog(MyRule, " Before Execute State:", 2, 4, 13, 6, 3004);

                bool x1 = this.DoConcept(23).Value > 0 ? true : false;
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool y1 = this.DoConcept(24).Value > 0 ? true : false;
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool z1 = this.DoConcept(25).Value > 0 ? true : false;
                bool z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false;

                int normWork = this.DoConcept(7).Value;
                int beforeDays = 0;
                int bearkCounter = 10;
                while ((x1 & x2) | (y1 & y2) | (z1 & z2))
                {

                    bearkCounter--;
                    if (bearkCounter == 0) { break; }

                    beforeDays++;

                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true : false;
                }
                if (x1 || y1 || z1)
                {
                    bool work = false, absent = false;
                    if (this.CalcDateZone.IsContain(this.RuleCalculateDate.AddDays(-beforeDays)))
                    {
                        //روز قبل یا روز بعد تعطیلات
                        work = this.DoConcept(4, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
                            ||
                            this.DoConcept(4).Value > 0;

                        absent = this.DoConcept(3004, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
                            &&
                            this.DoConcept(3004).Value > 0;
                    }
                    else   //تعطیلات ابتدای رینج محاسبات
                    {
                        work = true;
                    }
                    if (lastDay)
                    {
                        work = true; absent = false;
                        beforeDays++;
                    }
                    for (int i = lastDay ? 0 : 1; i < beforeDays && this.RuleCalculateDate.AddDays(-i) >= this.MinAssgnRuleDate; i++)
                    {
                        //تعطیل شیفت دار نباید حساب شود
                        if (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-i)).Value > 0
                            && !EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-i), "1", "2"))
                            continue;
                        //اگر شنبه محاسبه شود باید کارکرد جمعه داده شود
                        //if (!this.CalcDateZone.IsContain(this.RuleCalculateDate.AddDays(-i)))
                        //    continue;

                        if (work)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, this.RuleCalculateDate.AddDays(-i)).Value = normWork;
                            if (dailyWork)
                                this.DoConcept(4, this.RuleCalculateDate.AddDays(-i)).Value = 1;
                        }
                        else if (absent)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, this.RuleCalculateDate.AddDays(-i)).Value = 0;
                            if (dailyWork)
                                this.DoConcept(4, this.RuleCalculateDate.AddDays(-i)).Value = 0;
                            //this.DoConcept(3004, this.RuleCalculateDate.AddDays(-i)).Value = 1;
                        }
                        else//این روز احتمالا بدلیل نداشتن قانون محاسبه نشده است
                        {
                            if (hourlyWork)
                                this.DoConcept(2, this.RuleCalculateDate.AddDays(-i)).Value = normWork;
                            if (dailyWork)
                                this.DoConcept(4, this.RuleCalculateDate.AddDays(-i)).Value = 1;
                            //throw new Exception("حالت پیشبینی نشده در قانون 203");
                        }
                        //اگر شخص حضور و اضافه کار داشت نباید اعمال گردد
                        if (this.DoConcept(13, this.RuleCalculateDate.AddDays(-i)).Value == 0)
                        {
                            this.ReCalculate(13, this.RuleCalculateDate.AddDays(-i));
                        }
                        //this.DoConcept(6, this.RuleCalculateDate.AddDays(-i)).Value = normWork;
                        if (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-i)).Value > 0)
                            this.DoConcept(6, this.RuleCalculateDate.AddDays(-i)).Value
                                = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-i)).Value;
                    }
                    //محاسبه دوباره کارکرد ماهانه
                    for (int i = beforeDays - 1; i >= 0 && this.RuleCalculateDate.AddDays(-i) >= this.MinAssgnRuleDate; i--)
                    {

                        //کامنت شد زیرا مثلا شنبه آخر ماه باید دوباره کارکرد ماهانه را حساب کند
                        //if (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-i)).Value > 0)
                        //    continue;
                        if (!this.CalcDateZone.IsContain(this.RuleCalculateDate.AddDays(-i)))
                            continue;
                        this.ReCalculate(5, this.RuleCalculateDate.AddDays(-i));
                    }
                }

                GetLog(MyRule, " After Execute State:", 2, 4, 13, 6, 3004);
            }
        }

        /// <summary>
        /// اعمال کارکرد در تعطیلات
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2013(AssignedRule MyRule)
        {
            //23 مفهوم تعطیلات جزو کارکرد حساب شود
            //24 مفهوم تعطیلات رسمی جزو کارکرد حساب شود 
            //25 مفهوم روزهای غیر کاری جزو کارکرد حساب شود   
            //4 کارکرد خالص روزانه
            //6 مفهوم کارکردلازم
            //3004 غيبت روزانه
            //7 کارکرددرروز    
            //MyRule.Memory = 123;
            bool hourlyWork = true, dailyWork = true;
            if (MyRule.HasParameter("HourlyWork", this.RuleCalculateDate))
            {
                hourlyWork = MyRule["HourlyWork", this.RuleCalculateDate].ToInt() > 0;
            }
            if (MyRule.HasParameter("DailyWork", this.RuleCalculateDate))
            {
                dailyWork = MyRule["DailyWork", this.RuleCalculateDate].ToInt() > 0;
            }

            this.DoConcept(23).Value = MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt();
            this.DoConcept(24).Value = MyRule["TatilRasmi", this.RuleCalculateDate].ToInt();
            this.DoConcept(25).Value = MyRule["GheirKari", this.RuleCalculateDate].ToInt();
            if ((this.DoConcept(6).Value > 0
                && (EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1", "2")
                || this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0))
                )
            {

                GetLog(MyRule, " Before Execute State:", 2, 4, 13, 6, 3004);

                bool x1 = this.DoConcept(23).Value > 0 ? true : false;
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool y1 = this.DoConcept(24).Value > 0 ? true : false;
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool z1 = this.DoConcept(25).Value > 0 ? true : false;
                bool z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false;

                int normWork = this.DoConcept(7).Value;
                int beforeDays = 0;
                int bearkCounter = 10;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {

                    bearkCounter--;
                    if (bearkCounter == 0) { break; }

                    beforeDays++;

                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (dateList.Count > 0)
                {
                    bool work = false, absent = false;

                    //روز قبل یا روز بعد تعطیلات
                    work = this.DoConcept(4, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
                        &&
                        this.DoConcept(4).Value > 0;

                    absent = this.DoConcept(3004, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
                        &&
                        this.DoConcept(3004).Value > 0;


                    foreach (DateTime calcDate in dateList)
                    {
                        if (calcDate < this.MinAssgnRuleDate)
                        {
                            break;
                        }
                        //تعطیل شیفت دار نباید حساب شود
                        if (this.Person.GetShiftByDate(calcDate).Value > 0
                            && !EngEnvironment.HasCalendar(calcDate, "1", "2"))
                            continue;

                        if (work)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = normWork;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 1;
                        }
                        else if (absent)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = 0;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 0;
                        }
                        else//این روز احتمالا بدلیل نداشتن قانون محاسبه نشده است
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = normWork;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 1;
                        }
                        //اگر شخص حضور و اضافه کار داشت نباید اعمال گردد
                        if (this.DoConcept(13, calcDate).Value == 0)
                        {
                            this.ReCalculate(13, calcDate);
                        }
                        if (this.Person.GetShiftByDate(calcDate).Value > 0)
                            this.DoConcept(6, calcDate).Value
                                = this.Person.GetShiftByDate(calcDate).Value;
                    }
                    //محاسبه دوباره کارکرد ماهانه

                    for (int i = beforeDays - 1; i >= 0 && this.RuleCalculateDate.AddDays(-i) >= this.MinAssgnRuleDate; i--)
                    {
                        if (!this.CalcDateZone.IsContain(this.RuleCalculateDate.AddDays(-i)))
                            continue;
                        this.ReCalculate(5, this.RuleCalculateDate.AddDays(-i));
                    }
                }

                GetLog(MyRule, " After Execute State:", 2, 4, 13, 6, 3004);
            }
        }

        /// <summary>
        /// تنها تردد اول و آخر لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2014(AssignedRule MyRule)
        {
            GetLog(MyRule, " After Execute State:", 1);
            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.ConceptCalculateDate);
            if (ProceedTraffic != null && ProceedTraffic.HasHourlyItem)
            {
                IPair pair = new PairableScndCnpValuePair();
                pair.From = ProceedTraffic.Pairs.OrderBy(x => x.From).First().From;
                pair.To = ProceedTraffic.Pairs.OrderBy(x => x.From).Last().To;
                if (!ProceedTraffic.Pairs.OrderBy(x => x.From).Last().IsFilled) 
                {
                    pair.To = ProceedTraffic.Pairs.OrderBy(x => x.From).Last().From;
                }
                if (pair.From < pair.To && pair.From != -1000 && pair.To != -1000)
                {
                    ((PairableScndCnpValue)this.DoConcept(1)).AddPair(pair);
                    ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(Operation.Differance(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1))); 
                }
            }
            GetLog(MyRule, " After Execute State:", 1);

        }

        /// <summary>
        /// در صورت حداقل ----- حضور در روز تعطیل , کارکرد روزانه به شخص تعلق بگیرد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2015(AssignedRule MyRule)
        {
            GetLog(MyRule, " After Execute State:", 4);
            int gheireRasmi = MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt();
            int rasmi = MyRule["TatilRasmi", this.RuleCalculateDate].ToInt();
            int tatil = MyRule["GheirKari", this.RuleCalculateDate].ToInt();

            bool x1 = gheireRasmi > 0 ? true : false;
            bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate, "2");

            bool y1 = rasmi > 0 ? true : false;
            bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate, "1");

            bool z1 = tatil > 0 ? true : false;
            bool z2 = this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0 ? true : false;

            if ((x1 & x2) | (y1 & y2) | (z1 & z2))
            {
                if (this.DoConcept(1).Value > MyRule["minPresent", this.RuleCalculateDate].ToInt())
                {
                    this.DoConcept(4).Value = 1;
                }
            }

            GetLog(MyRule, " After Execute State:", 4);

        }

        #endregion

        #region قوانين مرخصي

        /// <summary>قانون مرخصي 1-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و هفت-3002 درنظر گرفته شده است</remarks>
        public virtual void R3001(AssignedRule MyRule)
        {
            //مرخصي درروز 1001

            //1006 مفهوم مرخصي استحقاقي روزانه ماهانه
            //1011 مفهوم مرخصي استحقاقي ساعتي ماهانه

            //1016 مرخصی استعلاجی ساعتی ماهانه
            //1017 مرخصی استعلاجی روزانه ماهانه

            //1073 مرخصی بی حقوق ساعتی ماهانه_11
            //1074 مرخصی بی حقوق ساعتی ماهانه_11
            //1074 مرخصی بی حقوق ساعتی ماهانه
            //1076 مرخصی بی حقوق روزانه ماهانه

            //1043,1044,1045,1046,1047 مرخصی باحقوق ساعتی ماهیانه
            //1048,1049,1050,1051,1052 مرخصی باحقوق روزانه ماهیانه
            GetLog(MyRule, " Before Execute State:", 1006, 1011, 1017, 1016, 1074, 1073, 1076, 1074, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1052);

            if (this.DoConcept(1001).Value > 0)
            {

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1011).Value > 0)
                {
                    //استحقاقی
                    this.DoConcept(1006).Value += this.DoConcept(1011).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1011).Value = this.DoConcept(1011).Value % this.DoConcept(1001).Value;
                    if (this.DoConcept(1011).Value == 0) this.DoConcept(1011).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1016).Value > 0)
                {
                    //استعلاجی
                    this.DoConcept(1017).Value += this.DoConcept(1016).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1016).Value = this.DoConcept(1016).Value % this.DoConcept(1001).Value;
                    if (this.DoConcept(1016).Value == 0) this.DoConcept(1016).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                //بی حقوق
                if (this.DoConcept(1073).Value > 0)
                {
                    this.DoConcept(1074).Value += this.DoConcept(1073).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1073).Value = this.DoConcept(1073).Value % this.DoConcept(1001).Value;
                    if (this.DoConcept(1073).Value == 0) this.DoConcept(1073).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1074).Value > 0)
                {
                    this.DoConcept(1076).Value += this.DoConcept(1074).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1074).Value = this.DoConcept(1074).Value % this.DoConcept(1001).Value;
                    if (this.DoConcept(1074).Value == 0) this.DoConcept(1074).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1043).Value > 0)
                {
                    // مرخصی با حقوق
                    this.DoConcept(1048).Value += this.DoConcept(1043).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1043).Value += this.DoConcept(1043).Value / this.DoConcept(1001).Value;
                    if (this.DoConcept(1043).Value == 0) this.DoConcept(1043).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1044).Value > 0)
                {
                    this.DoConcept(1049).Value += this.DoConcept(1044).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1044).Value += this.DoConcept(1044).Value / this.DoConcept(1001).Value;
                    if (this.DoConcept(1044).Value == 0) this.DoConcept(1044).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1045).Value > 0)
                {
                    this.DoConcept(1050).Value += this.DoConcept(1045).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1045).Value += this.DoConcept(1045).Value / this.DoConcept(1001).Value;
                    if (this.DoConcept(1045).Value == 0) this.DoConcept(1045).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1046).Value > 0)
                {
                    this.DoConcept(1051).Value += this.DoConcept(1046).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1046).Value += this.DoConcept(1046).Value / this.DoConcept(1001).Value;

                    if (this.DoConcept(1046).Value == 0) this.DoConcept(1046).Value = 1;
                }

                // شرط اضافه شد
                // در صورتیکه مفاهیم هر روز مقدار داشته باشد و در پایان دوره
                // با مفهوم ماهانه صفر شوند برای نمایش روزانه ها این شرط اضافه شده
                if (this.DoConcept(1047).Value > 0)
                {
                    this.DoConcept(1052).Value += this.DoConcept(1047).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1047).Value += this.DoConcept(1047).Value / this.DoConcept(1001).Value;

                    if (this.DoConcept(1047).Value == 0) this.DoConcept(1047).Value = 1;
                }
            }
            GetLog(MyRule, " After Execute State:", 1006, 1011, 1017, 1016, 1074, 1073, 1076, 1074, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1052);
        }

        /// <summary>
        /// حداکثر مرخصی ساعتی در روز ... میباشد و جریمه اعمال شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3002(AssignedRule MyRule)
        {
            var maxLeaveInDayParameter = MyRule["First", this.RuleCalculateDate].ToInt();
            var toLeave0Absence1Parameter = MyRule["Second", this.RuleCalculateDate].ToInt() == 1;
            var attendToOvertimeParameter = MyRule["Third", this.RuleCalculateDate].ToInt() == 1;

            // نگهداری شناسه های مفاهیم
            var conceptList = new List<int>();

            #region مرخصی استحقاقی ساعتی
            if (this.DoConcept(1003).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1003, 1005, 3004, 3020, 2, 4, 3004 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.Person.AddRemainLeave(this.RuleCalculateDate, this.DoConcept(1003).Value);
                    this.Person.AddRemainLeave(this.RuleCalculateDate, this.DoConcept(1005).Value * this.DoConcept(1001).Value);

                    this.DoConcept(3004).Value = 1;

                    this.DoConcept(1003).Value = 0;
                    this.DoConcept(1005).Value = 0;

                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;

                    this.DoConcept(3020).Value = 0;

                    if (attendToOvertimeParameter)
                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                    this.ReCalculate(13);

                }
                else
                {
                    conceptList.AddRange(new[] { 1003, 1005, 3001, 3008, 3010, 3014, 3029, 3030, 3031, 3028 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    int leaveHour = this.DoConcept(1003).Value;
                    int leaveInDay = this.DoConcept(1001).Value;
                    this.DoConcept(1005).Value = 1;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, leaveInDay - leaveHour, null);
                    this.DoConcept(1003).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(3001)).ClearPairs();
                    this.DoConcept(3020).Value = 0;
                    this.ReCalculate(3008, 3010, 3014, 3029, 3030, 3031, 3028);

                    if (attendToOvertimeParameter)
                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));
                }

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی استعلاجی ساعتی
            if (this.DoConcept(1008).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1008, 1010, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1008).Value = 0;
                    this.DoConcept(1010).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1010, 1008 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1010).Value = 1;
                    ((PairableScndCnpValue)this.DoConcept(1008)).ClearPairs();

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی ساعتی با حقوق_23

            if (this.DoConcept(1038).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1031, 1038, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1031).Value = 0;
                    this.DoConcept(1038).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1031, 1038 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1031).Value = 1;
                    this.DoConcept(1038).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }

            #endregion

            #region مرخصی ساعتی با حقوق_24
            if (this.DoConcept(1039).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1029, 1039, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1029).Value = 0;
                    this.DoConcept(1039).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1029, 1039 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1029).Value = 1;
                    this.DoConcept(1039).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی ساعتی با حقوق_25
            if (this.DoConcept(1040).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1037, 1040, 3004, 2, });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1037).Value = 0;
                    this.DoConcept(1040).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1037, 1040 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1037).Value = 1;
                    this.DoConcept(1040).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی ساعتی با حقوق_26
            if (this.DoConcept(1041).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1033, 1041, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1033).Value = 0;
                    this.DoConcept(1041).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1033, 1041 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1033).Value = 1;
                    this.DoConcept(1041).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی ساعتی با حقوق_27
            if (this.DoConcept(1042).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1035, 1042, 3004, 2, 4, 4002 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1035).Value = 0;
                    this.DoConcept(1042).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1035, 1042 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1035).Value = 1;
                    this.DoConcept(1042).Value = 0;
                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی بیماری بی حقوق ساعتی_11
            if (this.DoConcept(1054).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1064, 1054, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1064).Value = 0;
                    this.DoConcept(1054).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1066, 1054 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1066).Value = 1;
                    this.DoConcept(1054).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی بی حقوق  ساعتی_12
            if (this.DoConcept(1056).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1066, 1056, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1066).Value = 0;
                    this.DoConcept(1056).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1066, 1056 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1066).Value = 1;
                    this.DoConcept(1056).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی بیماری بی حقوق ساعتی_13
            if (this.DoConcept(1058).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1068, 1058, 3004, 2, 4 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1068).Value = 0;
                    this.DoConcept(1058).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1066, 1058 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1066).Value = 1;
                    this.DoConcept(1058).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی بی حقوق  ساعتی_14
            if (this.DoConcept(1060).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1060, 3004, 2, 4, 1070 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1070).Value = 0;
                    this.DoConcept(1060).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1060, 1066 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1066).Value = 1;
                    this.DoConcept(1060).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            #region مرخصی بی حقوق  ساعتی_15
            if (this.DoConcept(1062).Value >= maxLeaveInDayParameter)
            {
                conceptList.Clear();
                conceptList.Add(4002);
                if (toLeave0Absence1Parameter)
                {
                    conceptList.AddRange(new[] { 1062, 3004, 2, 4, 1072 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(3004).Value = 1;
                    this.DoConcept(2).Value = 0;
                    this.DoConcept(4).Value = 0;
                    this.DoConcept(1072).Value = 0;
                    this.DoConcept(1062).Value = 0;

                }
                else
                {
                    conceptList.AddRange(new[] { 1062, 1066 });
                    GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                    this.DoConcept(1066).Value = 1;
                    this.DoConcept(1062).Value = 0;

                }

                if (attendToOvertimeParameter)
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
            #endregion

            this.ReCalculate(1090);
        }

        /// <summary>
        /// حضور در مرخصی روزانه , اضافه کار شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3003(AssignedRule MyRule)
        {
            //1005 مرخصی استحقاقی روزانه

            // نگهداری شناسه های مفاهیم
            var conceptList = new List<int>();

            #region مرخصی استحقاقی ساعتی
            if (this.DoConcept(1005).Value == 1 && this.DoConcept(1).Value > 0)
            {
                conceptList.Clear();
                conceptList.Add(4002);

                GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));

                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());

            }

            #endregion

        }

        /// <summary>قانون مرخصي 6-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و پنج-3005 درنظر گرفته شده است</remarks>
        public virtual void R3004(AssignedRule MyRule)
        {
            //1003 مفهوم مرخصي استحقاقي ساعتي
            //3001 غيبت خالص ساعتي
            //2 کارکردخالص ساعتي
            GetLog(MyRule, " Before Execute State:", 1003, 3028);

            var execptList =
                ((PairableScndCnpValue)this.DoConcept(1003)).Pairs.Where(
                    x => x.Value < MyRule["First", this.RuleCalculateDate].ToInt()).ToList();

            if (execptList.Any())
            {
                ((PairableScndCnpValue)this.DoConcept(1003)).RemovePairs(execptList);
                ((PairableScndCnpValue)this.DoConcept(3028)).AppendPairs(execptList);

                this.Person.AddRemainLeave(this.RuleCalculateDate, execptList.Sum(x => x.Value));
            }




            GetLog(MyRule, " After Execute State:", 1003, 3028);

        }

        /// <summary>قانون مرخصي 7-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و شش-8 درنظر گرفته شده است</remarks>
        public virtual void R3005(AssignedRule MyRule)
        {
            //1011 مفهوم مرخصي استحقاقي ساعتي ماهانه
            //1094 مفهوم اگر مرخصی ماهانه از حد بخشش گذشت شامل بخشش نشود

            GetLog(MyRule, " Before Execute State:", 1011, 1094);
            if (this.DoConcept(1011).Value > 0 &&
                (this.DoConcept(1011).Value <= MyRule["First", this.RuleCalculateDate].ToInt()
                || MyRule["NotApplyIfGreater", this.RuleCalculateDate].ToInt() == 0))
            {
                int temp = Operation.Minimum(MyRule["First", this.RuleCalculateDate].ToInt(), this.DoConcept(1011).Value);
                this.DoConcept(1011).Value -= temp;
                //this.Person.AddRemainLeave(this.RuleCalculateDate, temp);
                this.Person.AddUsedLeave(this.ConceptCalculateDate,
                                           -1 * temp, null);
            }

            GetLog(MyRule, " After Execute State:", 1011, 1094);
        }

        /// <summary>قانون مرخصي 10-2</summary>
        ///<remarks>شناسه اين قانون در تعاريف بعدي هفتاد و هشت-9 درنظر گرفته شده است</remarks>
        public virtual void R3006(AssignedRule MyRule)
        {
            //1078 مرخصی استحقاقی اول وقت
            //1079 تعداد مرخصی استحقاقی اول وقت در روز
            //1080 تعداد مرخصی استحقاقی اول وقت در ماه
            GetLog(MyRule, " Before Execute State:", 1003, 3028, 1056, 1078);

            if (this.DoConcept(1080).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                if (this.DoConcept(1079).Value == 1)
                {
                    this.Person.AddRemainLeave(this.RuleCalculateDate, ((PairableScndCnpValue)this.DoConcept(1078)).Pairs[0].Value);
                    ((PairableScndCnpValue)this.DoConcept(1003)).RemovePair(((PairableScndCnpValue)this.DoConcept(1078)).Pairs[0]);
                    if (this.DoConcept(1014).Value == 1)
                    {
                        this.DoConcept(1056).Value += this.DoConcept(1078).Value;
                    }
                    else
                    {
                        this.DoConcept(3028).Value += this.DoConcept(1078).Value;
                    }
                    ((PairableScndCnpValue)this.DoConcept(1078)).ClearPairs();
                }
                else
                {
                    throw new UnforeseenState("اگر تعداد مرخصی اول وقت در ماه بیشتر از تعداد مجاز شد باید این مقدارغیر مجاز حتما مربوط به امروز شود", "قانون 104");
                }
            }

            GetLog(MyRule, " After Execute State:", 1003, 3028, 1056, 1078);
        }

        /// <summary>قانون مرخصي 14-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هفتاد و نه-79 درنظر گرفته شده است</remarks>
        public virtual void R3007(AssignedRule MyRule)
        {
            //تقويم تعطيل رسمي 1
            //تعطيل غير رسمي 2
            //تعطيل نوروز 4

            //2 مفهوم کارکردخالص ساعتي
            //4 مفهوم کارکردخالص روزانه
            //مرخصي درروز 1001
            //مرخصي استحقاقي روزانه 1005
            //مرخصي استحقاقي ساعتي 1003

            //1008 مرخصي استعلاجي ساعتي

            //1038,1039,1040,1041,1042 مرخصی باحقوق ساعتی
            //1043,1044,1045,1046,1047 مرخصی باحقوق روزانه

            //1090 مفهوم مجموع انواع مرخصی روزانه
            //1088 مفهوم مرخصی به کارکرد خالص اضافه شود
            GetLog(MyRule, " Before Execute State:", 4, 2);

            this.DoConcept(1088).Value = 1;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).PairCount > 0
                 && this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType != ShiftTypesEnum.OVERTIME)
            {
                if (this.DoConcept(1090).Value > 0)
                {
                    this.DoConcept(4).Value = 1;
                    this.DoConcept(2).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).Value;
                    this.ReCalculate(13);
                }
                else
                {
                    int value = 0;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1003)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1008)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1038)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1039)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1040)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1042)).Value;
                    if (value > 0)
                    {
                        this.DoConcept(4).Value = 1;
                        this.DoConcept(2).Value += value;
                        this.ReCalculate(13);
                    }
                }
            }
            GetLog(MyRule, " After Execute State:", 4, 2);
        }

        /// <summary>قانون مرخصي 14-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد-80 درنظر گرفته شده است</remarks>
        public virtual void R3008(AssignedRule MyRule)
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

            //1090 مفهوم مجموع انواع مرخصی روزانه

            GetLog(MyRule, " Before Execute State:", 13);
            if (MyRule["First", this.RuleCalculateDate].ToInt() == 0 &&
                (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4") ||
                this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.Count > 0))
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
            GetLog(MyRule, " Before Execute State:", 13);
        }

        /// <summary>
        /// سقف انتقال مانده مرخصی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3009(AssignedRule MyRule)
        {
            //325 تعطیلات غیر رسمی بین مرخصی,مرخصی محسوب شود
            var maxTransfer = MyRule["first", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مرخصي 19-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد و هفت-2012 درنظر گرفته شده است</remarks>
        public virtual void R3010(AssignedRule MyRule)
        {
            //1021 مرخصي قطعي در روزغيرکاري         
            GetLog(MyRule, " Before Execute State:", 1021);
            this.DoConcept(1021).Value = 1;
            GetLog(MyRule, " After Execute State:", 1021);
        }

        /// <summary>قانون مرخصي 28-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و پنج-3024 درنظر گرفته شده است</remarks>
        public virtual void R3011(AssignedRule MyRule)
        {
            //7 مفهوم کارکرددرروز
            //1003 مفهوم مرخصي استحقاقي ساعتي
            //1005 مفهوم مرخصي استحقاقي روزانه
            //308 جمع مرخصي استحقاقي ساعتي
            //6 کارکرد لازم
            this.DoConcept(1109);
           // this.DoConcept(1058);
            #region استحقاقی
            // در کد قبلی (کامنت شده) مقدار مرجع برای مقایسه پارامتر دوم بوده
            // در صورتیکه باید مرخصی در روز C20 در نظر گرفته میشده
            GetLog(MyRule, " Before Execute State:", 1003, 1005);
            if (this.DoConcept(6).Value > 0&&this.DoConcept(1003).Value >0 )
            {
                this.DoConcept(1003).Value = (this.DoConcept(1005).Value * this.DoConcept(6).Value) + this.DoConcept(1003).Value;
                this.DoConcept(1005).Value = 0;

                if (
                    this.DoConcept(1003).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1003).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1005).Value = 1;

                    if (this.DoConcept(1003).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1003).Value = this.DoConcept(1003).Value % this.DoConcept(1001).Value;
                    else ((PairableScndCnpValue)this.DoConcept(1003)).ClearPairs();// this.DoConcept(1003).Value = 0;

                    this.ReCalculate(1090, 1095);
                }
                else
                {
                    this.DoConcept(1005).Value = this.DoConcept(1003).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1003).Value = this.DoConcept(1003).Value % this.DoConcept(1001).Value;
                }
            }
            #endregion

            #region استعلاجی
            if (this.DoConcept(1010).Value >= 1)
            {
                GetLog(MyRule, " Before Execute State:", 1010, 1008);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1010).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1010).Value = 1;
                        this.DoConcept(1008).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1008).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, " After Execute State:", 1010, 1008);
            }
            #endregion

            #region با حقوق 43
            if (this.DoConcept(1031).Value >= 1)
            {
                GetLog(MyRule, " Before Execute State:", 1031, 1038);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1031).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1031).Value = 1;
                        this.DoConcept(1038).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1038).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, " After Execute State:", 1031, 1038);
            }
            #endregion

            #region با حقوق 44
            if (this.DoConcept(1029).Value >= 1)
            {
                GetLog(MyRule, " Before Execute State:", 1029, 1039);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1029).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1029).Value = 1;
                        this.DoConcept(1039).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1039).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, " After Execute State:", 1029, 1039);
            }
            #endregion

            #region با حقوق 45
            if (this.DoConcept(1037).Value >= 1)
            {
                GetLog(MyRule, " Before Execute State:", 1040, 3037);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1037).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1037).Value = 1;
                        this.DoConcept(1040).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1040).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, " After Execute State:", 1040, 1037);
            }
            #endregion

            #region با حقوق 46
            if (this.DoConcept(1033).Value >= 1)
            {
                GetLog(MyRule, " Before Execute State:", 1041, 1033);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1033).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1033).Value = 1;
                        this.DoConcept(1041).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1041).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, " After Execute State:", 1041, 1033);
            }
            #endregion

            #region با حقوق 47
            if (this.DoConcept(1035).Value >= 1)
            {
                GetLog(MyRule, " Before Execute State:", 1035, 1042);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1035).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1035).Value = 1;
                        this.DoConcept(1042).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1042).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, " After Execute State:", 1035, 1042);
            }
            #endregion

            #region بی حقوق 31
            GetLog(MyRule, " Before Execute State:", 1054, 1064);
            if (this.DoConcept(1054).Value > 0)
            {
                this.DoConcept(1054).Value = (this.DoConcept(1064).Value * this.DoConcept(6).Value) + this.DoConcept(1054).Value;
                this.DoConcept(1064).Value = 0;

                if (
                    this.DoConcept(1054).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1054).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1064).Value = 1;

                    if (this.DoConcept(1054).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1054).Value = this.DoConcept(1054).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1054).Value = 0;
                }
                else
                {
                    this.DoConcept(1064).Value = this.DoConcept(1054).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1054).Value = this.DoConcept(1054).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, " After Execute State:", 1054, 1064);
            
            #endregion

            #region بی حقوق 32
            // در کد قبلی (کامنت شده) مقدار مرجع برای مقایسه پارامتر دوم بوده
            // در صورتیکه باید مرخصی در روز C20 در نظر گرفته میشده
            GetLog(MyRule, " Before Execute State:", 1056, 1066);
            if (this.DoConcept(1056).Value > 0)
            {
                this.DoConcept(1056).Value = (this.DoConcept(1066).Value * this.DoConcept(6).Value) + this.DoConcept(1056).Value;
                this.DoConcept(1066).Value = 0;

                if (
                    this.DoConcept(1056).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1056).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1066).Value = 1;

                    if (this.DoConcept(1056).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1056).Value = this.DoConcept(1056).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1056).Value = 0;
                }
                else
                {
                    this.DoConcept(1066).Value = this.DoConcept(1056).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1056).Value = this.DoConcept(1056).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, " After Execute State:", 1056, 1066);
            #endregion

            #region بی حقوق 33
            GetLog(MyRule, " Before Execute State:", 1058, 1068);
            if (this.DoConcept(1058).Value > 0)
            {
                this.DoConcept(1058).Value = (this.DoConcept(1068).Value * this.DoConcept(6).Value) + this.DoConcept(1058).Value;
                this.DoConcept(1068).Value = 0;

                if (
                    this.DoConcept(1058).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1058).Value <= 
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1068).Value = 1;

                    if (this.DoConcept(1058).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1058).Value = this.DoConcept(1058).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1058).Value = 0;
                }
                else
                {
                    this.DoConcept(1068).Value = this.DoConcept(1058).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1058).Value = this.DoConcept(1058).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, " After Execute State:", 1058, 1068);
            #endregion

            #region بی حقوق 34
            GetLog(MyRule, " Before Execute State:", 1060, 1070);
            if (this.DoConcept(1060).Value > 0)
            {
                this.DoConcept(1060).Value = (this.DoConcept(1070).Value * this.DoConcept(6).Value) + this.DoConcept(1060).Value;
                this.DoConcept(1070).Value = 0;

                if (
                    this.DoConcept(1060).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1060).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1070).Value = 1;

                    if (this.DoConcept(1060).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1060).Value = this.DoConcept(1060).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1060).Value = 0;
                }
                else
                {
                    this.DoConcept(1070).Value = this.DoConcept(1060).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1060).Value = this.DoConcept(1060).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, " After Execute State:", 1060, 1070);                   
            #endregion

            #region بی حقوق 35
            GetLog(MyRule, " Before Execute State:", 1062, 1072);
            if (this.DoConcept(1062).Value > 0)
            {
                this.DoConcept(1062).Value = (this.DoConcept(1072).Value * this.DoConcept(6).Value) + this.DoConcept(1062).Value;
                this.DoConcept(1072).Value = 0;

                if (
                    this.DoConcept(1062).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1062).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1072).Value = 1;

                    if (this.DoConcept(1062).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1062).Value = this.DoConcept(1062).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1062).Value = 0;
                }
                else
                {
                    this.DoConcept(1072).Value = this.DoConcept(1062).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1062).Value = this.DoConcept(1062).Value % this.DoConcept(1001).Value;
                }
            }

            this.ReCalculate(1109,1110);
            GetLog(MyRule, " After Execute State:", 1062, 1072);
            #endregion


        }

        /// <summary>قانون مرخصي 29-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و شش-3025 درنظر گرفته شده است</remarks>
        /// مرخصي 28-2: روش تبديل ساعت مرخصي به روزانه:هر .... ساعت معادل يک روز مرخصي
        public virtual void R3012(AssignedRule MyRule)
        {
            int leaveInDay = MyRule["First", this.RuleCalculateDate].ToInt();
            if (this.DoConcept(1003).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1005, 1003);
                this.DoConcept(1005).Value += this.DoConcept(1003).Value / leaveInDay;
                this.DoConcept(1003).Value = this.DoConcept(1003).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1005, 1003);
            }

            if (this.DoConcept(1008).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1010, 1008);
                this.DoConcept(1010).Value += this.DoConcept(1008).Value / leaveInDay;
                this.DoConcept(1008).Value = this.DoConcept(1008).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1010, 1008);
            }

            if (this.DoConcept(1038).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1031, 1038);
                this.DoConcept(1031).Value += this.DoConcept(1038).Value / leaveInDay;
                this.DoConcept(1038).Value = this.DoConcept(1038).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1031, 1038);
            }

            if (this.DoConcept(1039).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1029, 1039);
                this.DoConcept(1029).Value += this.DoConcept(1039).Value / leaveInDay;
                this.DoConcept(1039).Value = this.DoConcept(1039).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1029, 1039);
            }

            if (this.DoConcept(1040).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1037, 1040);
                this.DoConcept(1037).Value += this.DoConcept(1040).Value / leaveInDay;
                this.DoConcept(1040).Value = this.DoConcept(1040).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1037, 1040);
            }

            if (this.DoConcept(1041).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1033, 1041);
                this.DoConcept(1033).Value += this.DoConcept(1041).Value / leaveInDay;
                this.DoConcept(1041).Value = this.DoConcept(1041).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1033, 1041);
            }

            if (this.DoConcept(1042).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1035, 1042);
                this.DoConcept(1035).Value += this.DoConcept(1042).Value / leaveInDay;
                this.DoConcept(1042).Value = this.DoConcept(1042).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1035, 1042);
            }

            if (this.DoConcept(1054).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1054, 1064);
                this.DoConcept(1064).Value += this.DoConcept(1054).Value / leaveInDay;
                this.DoConcept(1054).Value = this.DoConcept(1054).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1054, 1064);
            }

            if (this.DoConcept(1056).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1056, 1066);
                this.DoConcept(1066).Value += this.DoConcept(1056).Value / leaveInDay;
                this.DoConcept(1056).Value = this.DoConcept(1056).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1056, 1066);
            }

            if (this.DoConcept(1058).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1058, 1068);
                this.DoConcept(1068).Value += this.DoConcept(1058).Value / leaveInDay;
                this.DoConcept(1058).Value = this.DoConcept(1058).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1058, 1068);
            }

            if (this.DoConcept(1060).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1060, 1070);
                this.DoConcept(1070).Value += this.DoConcept(1060).Value / leaveInDay;
                this.DoConcept(1060).Value= this.DoConcept(1060).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1060, 1070);
            }

            if (this.DoConcept(1062).Value > leaveInDay)
            {
                GetLog(MyRule, " Before Execute State:", 1062, 1072);
                this.DoConcept(1072).Value += this.DoConcept(1062).Value / leaveInDay;
                this.DoConcept(1062).Value = this.DoConcept(1062).Value % leaveInDay;
                GetLog(MyRule, " After Execute State:", 1062, 1072);
            }
        }

        /// <summary>قانون فراخواننده ی اصلاح «نتیجه مرخصی محاسبه شده» در هر روز"</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدو شش-106 درنظر گرفته شده است</remarks>
        public virtual void R3013(AssignedRule MyRule)
        {
            this.Person.VerifyRemainLeave(this.RuleCalculateDate);
        }

        /// <summary>
        /// مانده مرخصی استحقاقی میتواند منفی شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3014(AssignedRule MyRule)
        {
            this.DoConcept(1098).Value = 1;
        }

        /// <summary>
        /// اعمال مرخصی در زمان حضور
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3015(AssignedRule MyRule)
        {
            this.DoConcept(1099).Value = 1;
        }

        /// <summary>
        /// اعمال بازخريد مرخصي ها
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3016(AssignedRule MyRule)
        {
            Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(Precards.DailyLeave6));
            if (permit != null && permit.Value == 1)
            {
                int leaveInDay = this.DoConcept(1001, this.RuleCalculateDate).Value;
                var remain = this.Person.GetRemainLeave(this.RuleCalculateDate);
                var souldDecrease = Operation.Minimum(remain, leaveInDay);

                if (souldDecrease > 0)
                    this.Person.AddUsedLeave(this.RuleCalculateDate, souldDecrease, permit);

            }
        }

        /// <summary>قانون مقداردهی C20</summary>        
        public virtual void R3017(AssignedRule MyRule)
        {
            this.DoConcept(1001).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.Person.LeaveSetting.MinutesInDay = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C101</summary>        
        public virtual void R3018(AssignedRule MyRule)
        {
            this.DoConcept(1014).Value = 1;// MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C536</summary>        
        public virtual void R3019(AssignedRule MyRule)
        {
            this.DoConcept(1083).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C537</summary>        
        public virtual void R3020(AssignedRule MyRule)
        {
            this.DoConcept(1084).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C539</summary>        
        public virtual void R3021(AssignedRule MyRule)
        {
            this.DoConcept(1086).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی LeaveSetting.UseFutureMounthLeave</summary>        
        public virtual void R3022(AssignedRule MyRule)
        {
            this.Person.LeaveSetting.UseFutureMounthLeave = true;
        }

        /// <summary>
        /// اعمال مرخصی استحقاقی در روزها تعطیل بین مرخصی استحقاقی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3023(AssignedRule MyRule)
        {
            //1005 مفهوم مرخصي استحقاقي روزانه
            //114 مفهوم اعمال تعطيلات نوروز در مرخصي 
            //324 تعطیلات رسمی بین مرخصی,مرخصی محسوب شود
            //325 تعطیلات غیر رسمی بین مرخصی,مرخصی محسوب شود
            //326 روزهای غیر کاری بین مرخصی,مرخصی محسوب شود


            GetLog(MyRule, " Before Execute State:", 1003, 1005);
            if (this.DoConcept(1005).Value > 0
                && this.DoConcept(1005, this.RuleCalculateDate.AddDays(-1)).Value ==0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
                bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

                int beforeDays = 0;
                int maxLoopcounter = 5;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {
                    beforeDays++;
                    maxLoopcounter--;
                    if (maxLoopcounter < 1)
                    {
                        break;
                    }
                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2)) 
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (this.DoConcept(1005, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {
                    int leaveInDay = this.DoConcept(1001, this.RuleCalculateDate).Value;
                    this.DoConcept(1003).Value = 0;

                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(1005, calcDate).Value = 1;
                        this.Person.AddUsedLeave(calcDate, leaveInDay, null);
                    }
                }
            }

            GetLog(MyRule, " After Execute State:", 1003, 1005);
        }

        /// <summary>
        ///  اعمال مرخصی استعلاجی در روزها تعطیل بین مرخصی استعلاجی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3024(AssignedRule MyRule)
        {
            //1010 مفهوم مرخصي استعلاجی روزانه
            //114 مفهوم اعمال تعطيلات نوروز در مرخصي 
            //327 تعطیلات رسمی بین مرخصی استعلاجی,مرخصی استعلاجی محسوب شود
            //328 تعطیلات غیر رسمی بین مرخصی استعلاجی,مرخصی استعلاجی محسوب شود
            //329 روزهای غیر کاری بین مرخصی استعلاجی,مرخصی استعلاجی محسوب شود

            GetLog(MyRule, " Before Execute State:", 1010);
            if (this.DoConcept(1010).Value > 0
                && this.DoConcept(1010, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
                bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

                int beforeDays = 0;
                int maxLoopcounter = 20;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {
                    beforeDays++;
                    maxLoopcounter--;
                    if (maxLoopcounter < 1)
                    {
                        break;
                    }
                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }
                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (this.DoConcept(1010, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {
                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(1010, calcDate).Value = 1;
                    }
                }

            }

            GetLog(MyRule, " After Execute State:", 1010);
        }

        /// <summary>
        /// سقف مرخصی ساعتی استحقاقی درماه --- میباشد و بیش از آن به مرخصی بی حقوق تبدیل شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3025(AssignedRule MyRule)
        {
            //1011 مرخصی ساعتی ماهانه
            //1075 مرخصی بی حقوق ساعتی ماهانه
            int maxLeave = MyRule["first", this.RuleCalculateDate].ToInt();
            if (this.DoConcept(1011).Value > 0 && this.DoConcept(1011).Value > maxLeave) 
            {
                GetLog(MyRule, " Before Execute State:", 1011, 1075);
                int overLeave = this.DoConcept(1011).Value - maxLeave;
                this.DoConcept(1011).Value = maxLeave;
                this.Person.AddUsedLeave(this.RuleCalculateDate, -1 * overLeave, null);
                this.DoConcept(1075).Value += overLeave;
                GetLog(MyRule, " After Execute State:", 1011, 1075);
            }
        }

        /// <summary>
        /// اگر شخص از شنبه تا چهار شنبه را مرخصی روزانه استحقاقی گرفت , پنج شنبه هم مرخصی منظور گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3026(AssignedRule MyRule)
        {
            //1011 مرخصی ساعتی ماهانه
            //1075 مرخصی بی حقوق ساعتی ماهانه
            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday) 
            {
                Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate.AddDays(-5), EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
                bool allOfWeekLeave = permit != null && permit.Value == 1 ? true : false;

                permit = this.Person.GetPermitByDate(this.RuleCalculateDate.AddDays(-4), EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
                allOfWeekLeave = allOfWeekLeave && permit != null && permit.Value == 1 ? true : false;

                permit = this.Person.GetPermitByDate(this.RuleCalculateDate.AddDays(-3), EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
                allOfWeekLeave = allOfWeekLeave && permit != null && permit.Value == 1 ? true : false;

                permit = this.Person.GetPermitByDate(this.RuleCalculateDate.AddDays(-2), EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
                allOfWeekLeave = allOfWeekLeave && permit != null && permit.Value == 1 ? true : false;

                permit = this.Person.GetPermitByDate(this.RuleCalculateDate.AddDays(-1), EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
                allOfWeekLeave = allOfWeekLeave && permit != null && permit.Value == 1 ? true : false;

                if (allOfWeekLeave) 
                {
                    GetLog(MyRule, " Before Execute State:", 1005, 3004);
                    int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
                    if (demandLeave >= this.DoConcept(1001).Value)
                    {
                        this.Person.AddUsedLeave(this.RuleCalculateDate, this.DoConcept(1001).Value, null);
                        this.DoConcept(1005).Value = 1;
                    }
                    else 
                    {
                        this.DoConcept(3004).Value = 1;
                    }
                    GetLog(MyRule, " After Execute State:", 1005, 3004);
                }
            }
        }
        #endregion

        #region قوانين ماموريت

        /// <summary>
        /// ماموريت به كاركرد اضافه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4001(AssignedRule MyRule)
        {
            //2023 مجموع ماموريت ساعتي
            //4 کارکردخالص روزانه
            //13 کارکرد ناخالص ساعتی
            //ماموريت خالص روزانه 2003
            //ماموريت درروز 2001
            //4002 اضافه کار مجاز
            //4003 اضافه کار غیرمجاز

            GetLog(MyRule, " Before Execute State:", 13);

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
                MyRule["First", this.RuleCalculateDate].ToInt() == 0 &&
                !EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
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
            else if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1
                && !EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
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
            GetLog(MyRule, " After Execute State:", 13);
        }

        /// <summary>
        /// ماموريت11-1: ماموريت روزانه و شبانه روزي در روزهاي غير كاري كاري اضافه كاري محسوب شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4002(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005
            //2008 ماموريت شبانه روزي
            //اضافه کارساعتي 56
            //ماموريت درروز 2001
            //7 کارکرد در روز
            if (this.DoConcept(2008).Value + this.DoConcept(2005).Value > 0 &&
                  this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002);
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(this.DoConcept(7).Value);
                GetLog(MyRule, " After Execute State:", 4002);
            }
        }

        /// <summary>
        /// ماموريت 13-1: ماموريت در ساعت غير كاري مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4003(AssignedRule MyRule)
        {
            //ماموريت ساعتي 2004
            //4007 مفهوم اضافه کارساعتي بعد ازوقت           
            //4008 مفهوم اضافه کارساعتي قبل ازوقت
            //2012 مجوز ماموريت در ساعات غيرکاري           
            //4001 اضافه خالص کارساعتي
            //4002 اضافه کارساعتي
            //2023 مفهوم مجموع ماموريت ساعتي           
            //2028 مفهوم ماموریت خارج از شیفت در روز کاری

            GetLog(MyRule, " Before Execute State:", 2004, 4007, 2012, 4002, 2023);
            bool mustPeresentEndOfShift = false;
            if (MyRule.HasParameter("first", this.RuleCalculateDate))
            {
                mustPeresentEndOfShift = MyRule["first", this.RuleCalculateDate].ToInt() > 0;
            }
            if (this.DoConcept(2028).Value > 0 && this.Person.GetShiftByDate(this.RuleCalculateDate).PairCount > 0)
            {
                ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(2028)).Pairs)
                {
                    int endOfShift = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).Last().To;

                    if (mustPeresentEndOfShift && pair.To > endOfShift)
                    {
                        int endOfPair = 0;
                        if (ProceedTraffic != null && ProceedTraffic.PairCount > 0 && ProceedTraffic.Pairs.Any(x => x.From > pair.To))
                        {
                            endOfPair = pair.To;
                        }
                        else if (ProceedTraffic != null && ProceedTraffic.PairCount > 0 && ProceedTraffic.Pairs.Any(x => x.From > endOfShift))
                        {
                            endOfPair = ProceedTraffic.Pairs.Where(x => x.From > endOfShift).OrderBy(x => x.From).First().From;
                        }
                        else
                        {
                            endOfPair = endOfShift;
                        }
                        if (pair.From < endOfPair)
                        {
                            PairableScndCnpValuePair allowPair = new PairableScndCnpValuePair(pair.From, endOfPair);

                            ((PairableScndCnpValue)this.DoConcept(4001))
                           .AppendPairs(Operation.Differance(allowPair, this.Person.GetShiftByDate(this.RuleCalculateDate)));

                            ((PairableScndCnpValue)this.DoConcept(4002))
                                .AppendPairs(Operation.Differance(allowPair, this.Person.GetShiftByDate(this.RuleCalculateDate)));

                        }
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4001))
                            .AppendPairs(Operation.Differance(pair, this.Person.GetShiftByDate(this.RuleCalculateDate)));

                        ((PairableScndCnpValue)this.DoConcept(4002))
                            .AppendPairs(Operation.Differance(pair, this.Person.GetShiftByDate(this.RuleCalculateDate)));

                    }

                    if (Operation.Intersect(pair, this.DoConcept(2004)).Value == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(2004)).AppendPair(pair);
                    }
                }
                this.ReCalculate(13, 2023, 4007, 4008);
            }

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
            {
                if (this.DoConcept(2023).Value > 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(4001))
                        .AppendPairs(((PairableScndCnpValue)this.DoConcept(2023)).Pairs);

                    ((PairableScndCnpValue)this.DoConcept(4002))
                       .AppendPairs(((PairableScndCnpValue)this.DoConcept(2023)).Pairs);

                    this.ReCalculate(13, 2023, 4007, 4008);
                }
            }

            GetLog(MyRule, " After Execute State:", 2004, 4007, 2012, 4002, 2023);
        }

        /// <summary>
        /// ماموريت 14-1: ماموريت در زمان استراحت بين وقت مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4004(AssignedRule MyRule)
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
                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(((PairableScndCnpValue)this.DoConcept(2013)).Pairs);
                this.ReCalculate(13, 4012, 2023);
            }
            GetLog(MyRule, " After Execute State:", 4002, 2004, 2019, 2020, 2021, 2022, 2023);
        }

        /// <summary>
        /// ماموريت 16-1: ماموريت در روز غير كاري در صورت حضور اضافه كار حساب شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4005(AssignedRule MyRule)
        {
            //ماموريت ساعتي 2004
            //مجوز ماموريت در ساعات غيرکاري 2012
            //اضافه کارساعتي 4002
            //حضور 1
            //ماموريت روزانه 2005
            GetLog(MyRule, " Before Execute State:", 4002);
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                if (this.DoConcept(2005).Value > 0 || this.DoConcept(2008).Value > 0)
                    if (this.DoConcept(1).Value > 0)
                    {

                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));
                        this.ReCalculate(13);
                        //نباید از اینجا به بعد کارکرد ناخالص دوباره محاسبه شود تا
                        //در قانون ماموریت به کارکرد ناخالص اضافه شود اختلال ایجاد نشود
                    }
            GetLog(MyRule, " After Execute State:", 4002);

        }

        /// <summary>
        /// ماموريت 20-1: ماموريت در روز بيشتر از .... و كمتر از .... يك روز ماموريت حساب شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4006(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005
            //ماموريت ساعتي 2004
            GetLog(MyRule, " Before Execute State:", 2004, 2005, 3028, 2023, 2031);
            if (this.DoConcept(2005).Value > 0)
            {
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    //this.DoConcept(2031).Value = 1;
                    if (this.DoConcept(6).Value > MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(2004).Value = this.DoConcept(6).Value - this.DoConcept(2001).Value;
                    }
                }
                else
                {
                    this.DoConcept(2004).Value = this.DoConcept(6).Value;
                }
                this.DoConcept(3028).Value = 0;
                this.ReCalculate(2023, 2005);
            }
            GetLog(MyRule, " After Execute State:", 2004, 2005, 3028, 2023, 2031);

        }

        /// <summary>
        /// ماموریت روزانه .... اضافه کار لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4007(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005
            //اضافه کارساعتي 4002
            //ماموريت ساعتي 2004


            GetLog(MyRule, " Before Execute State:", 4002);
            if (this.DoConcept(2005).Value > 0)
            {
                if (MyRule.HasParameter("second", this.RuleCalculateDate))
                {
                    if (MyRule["second", this.RuleCalculateDate].ToInt() == 1
                        &&
                        (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0))
                    {
                        return;
                    }
                }
                this.DoConcept(2031).Value = 1;
                if (MyRule["First", this.RuleCalculateDate].ToInt() > 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt());
                }
            }
            GetLog(MyRule, " After Execute State:", 4002);

        }

        /// <summary>
        /// قانون مقداردهي ماموريت در روز (مفهوم ماموريت در روز)
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4008(AssignedRule MyRule)
        {
            this.DoConcept(2001).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>
        /// ماموريت در زمان استراحت بين وقت مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4009(AssignedRule MyRule)
        {
            this.DoConcept(2012).Value = 1;// MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>
        /// ماموريت در ساعات غير كاري مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4010(AssignedRule MyRule)
        {
            this.DoConcept(2029).Value = 1;// MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>
        /// اعمال ماموريت روزانه در روزها تعطيل بين ماموريت روزانه
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4011(AssignedRule MyRule)
        {
            //2005 مفهوم مجموع ماموریت روزانه
            //2047 تعطیلات رسمی بین مرخصی,مرخصی محسوب شود
            //2048 تعطیلات غیر رسمی بین مرخصی,مرخصی محسوب شود
            //2049 روزهای غیر کاری بین مرخصی,مرخصی محسوب شود
            //23 مفهوم تعطیلات جزو کارکرد حساب شود
            //24 مفهوم تعطیلات رسمی جزو کارکرد حساب شود 
            //25 مفهوم روزهای غیر کاری جزو کارکرد حساب شود

            GetLog(MyRule, " Before Execute State:", 2, 4, 2005, 2031, 2032, 2033, 2034, 2035);
            if (this.DoConcept(2005).Value > 0
                && this.DoConcept(2005, this.RuleCalculateDate.AddDays(-1)).Value ==0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
                bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

                int beforeDays = 0;
                int maxLoopcounter = 5;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {
                    beforeDays++;
                    maxLoopcounter--;
                    if (maxLoopcounter < 1)
                    {
                        break;
                    }
                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 & !x2 & !y2 ? true : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (this.DoConcept(2005, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {
                    foreach (DateTime calcDate in dateList)
                    {
                        if (this.DoConcept(2031, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2031, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2032, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2032, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2033, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2033, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2034, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2034, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2035, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2035, calcDate).Value = 1;
                        }
                        this.ReCalculate(2005, calcDate);
                    }
                    //چون اولویت این قانون بعد از قانون 203 است
                    //برای روز جاری وظایف قانون 203 را اجرا میکنیم
                    if (this.DoConcept(2005).Value > 0)
                    {
                        if (x2 && this.DoConcept(24).Value > 0
                            || y2 && this.DoConcept(23).Value > 0
                            || z2 && this.DoConcept(25).Value > 0)
                        {
                            this.DoConcept(2).Value = this.DoConcept(6).Value;
                            this.DoConcept(4).Value = 1;
                        }
                    }
                }
            }
            GetLog(MyRule, " After Execute State:", 2, 4, 2005, 2031, 2032, 2033, 2034, 2035);
        }

        /// <summary>
        /// اعمال ماموريت شبانه روزي در روزها تعطيل بين ماموريت شبانه روزي
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4012(AssignedRule MyRule)
        {
            //2008 مفهوم مجموع ماموریت شبانه روزی 
            //2050 تعطیلات رسمی بین ماموریت شبانه روزی,ماموریت شبانه روزی محسوب شود
            //571 تعطیلات غیر رسمی بین ماموریت شبانه روزی,ماموریت شبانه روزی محسوب شود
            //572 روزهای غیر کاری بین ماموریت شبانه روزی,ماموریت شبانه روزی محسوب شود

            GetLog(MyRule, " Before Execute State:", 2008, 2041, 2042, 2043, 2044, 2045);
            if (this.DoConcept(2008).Value == 0
                && this.DoConcept(2008, this.RuleCalculateDate.AddDays(-1)).Value > 0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
                bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

                int beforeDays = 0;
                int maxLoopcounter = 5;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {
                    beforeDays++;
                    maxLoopcounter--;
                    if (maxLoopcounter < 1)
                    {
                        break;
                    }
                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (this.DoConcept(2008, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {
                    foreach (DateTime calcDate in dateList)
                    {
                        if (this.DoConcept(2041, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2041, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2042, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2042, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2043, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2043, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2044, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2044, calcDate).Value = 1;
                        }
                        if (this.DoConcept(2045, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                        {
                            this.DoConcept(2045, calcDate).Value = 1;
                        }
                        this.ReCalculate(2008, calcDate);
                    }
                }
            }
            GetLog(MyRule, " After Execute State:", 2008, 2041, 2042, 2043, 2044, 2045);
        }

        /// <summary>
        /// حداقل مدت ماموریت ساعتی قبل از وقت .... و حداکثر آن ... میباشد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4013(AssignedRule MyRule)
        {
            //2023 مجموع انواع ماموریت ساعتی
            if (this.DoConcept(2023).Value > 0 && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 2023, 4001, 4002);

                int min = MyRule["first", this.RuleCalculateDate].ToInt();
                int max = MyRule["second", this.RuleCalculateDate].ToInt();
                IPair shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First();

                PairableScndCnpValue beforeTimeCnp = new PairableScndCnpValue();
                beforeTimeCnp.AddPairs(((PairableScndCnpValue)this.DoConcept(2023)).Pairs.Where(x => x.From < shiftStart.From && x.To < shiftStart.From ).ToList());

                if (beforeTimeCnp.Value < min || beforeTimeCnp.Value > max)
                {
                    ((PairableScndCnpValue)this.DoConcept(4001)).AddPairs(Operation.Differance(this.DoConcept(4001), beforeTimeCnp));
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), beforeTimeCnp));
                    ((PairableScndCnpValue)this.DoConcept(2023)).AddPairs(Operation.Differance(this.DoConcept(2023), beforeTimeCnp));

                    this.ReCalculate(13);
                }

                GetLog(MyRule, " After Execute State:", 2023, 4001, 4002);
            }
        }

        /// <summary>
        /// حداقل مدت ماموریت ساعتی اول وقت .... و حداکثر آن ... میباشد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4014(AssignedRule MyRule)
        {
            //2023 مجموع انواع ماموریت ساعتی
            if (this.DoConcept(2023).Value > 0 && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                IPair shiftFirstStart = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First();
                GetLog(MyRule, " Before Execute State:", 2023, 4001, 4002);

                int min = MyRule["first", this.RuleCalculateDate].ToInt();
                int max = MyRule["second", this.RuleCalculateDate].ToInt();

                PairableScndCnpValue startTimeCnp = new PairableScndCnpValue();

                startTimeCnp.AddPairs(((PairableScndCnpValue)this.DoConcept(2023)).Pairs.Where(x => x.From <= shiftFirstStart.From).ToList());

                if (startTimeCnp.Value < min || startTimeCnp.Value > max)
                {
                    ((PairableScndCnpValue)this.DoConcept(4001)).AddPairs(Operation.Differance(this.DoConcept(4001), startTimeCnp));
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), startTimeCnp));
                    ((PairableScndCnpValue)this.DoConcept(2023)).AddPairs(Operation.Differance(this.DoConcept(2023), startTimeCnp));

                    this.ReCalculate(13);
                }

                GetLog(MyRule, " After Execute State:", 2023, 4001, 4002);
            }
        }


        /// <summary>
        /// حداقل مدت ماموریت ساعتی داخل شیفت .... و حداکثر آن ... میباشد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R4015(AssignedRule MyRule)
        {
            //2023 مجموع انواع ماموریت ساعتی
            if (this.DoConcept(2023).Value > 0 && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 2023, 4001, 4002);

                int min = MyRule["first", this.RuleCalculateDate].ToInt();
                int max = MyRule["second", this.RuleCalculateDate].ToInt();

                PairableScndCnpValue dutyInShiftCnp = new PairableScndCnpValue();
                dutyInShiftCnp = Operation.Intersect(this.DoConcept(2023), this.Person.GetShiftByDate(this.RuleCalculateDate));

                if (dutyInShiftCnp.Value < min || dutyInShiftCnp.Value > max)
                {
                    ((PairableScndCnpValue)this.DoConcept(3028)).AppendPairs(dutyInShiftCnp);
                    ((PairableScndCnpValue)this.DoConcept(2023)).AddPairs(Operation.Differance(this.DoConcept(2023), dutyInShiftCnp));
                    this.DoConcept(2).Value -= dutyInShiftCnp.Value;
                 
                    this.ReCalculate(13);
                }

                GetLog(MyRule, " After Execute State:", 2023, 4001, 4002);
            }
        }


       /// <summary>
       /// ماموریت روزانه جزو نوبت کار حساب شود
       /// </summary>
       /// <param name="MyRule"></param>
        public virtual void R4016(AssignedRule MyRule)
        {
            //2005 مجموع انواع ماموریت روزانه
            //5005 نوبت کاری
            if (this.DoConcept(2005).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 5005);
                this.DoConcept(5005).Value = 1;

                GetLog(MyRule, " After Execute State:", 5005);
            }
        }




        #endregion

        #region قوانين کم کاري

        /// <summary>قانون کم کاري 1-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و هشت-38 درنظر گرفته شده است</remarks>
        public virtual void R5001(AssignedRule MyRule)
        {

            //کارکردخالص ساعتي 2
            //13 کارکردناخالص

            //غيبت خالص ساعتي 3001
            //3020 غیبت مجاز ساعتی
            //تاخير ساعتي مجاز 3021
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

            GetLog(MyRule, " Before Execute State:", 2, 3008, 3028, 3020, 3021, 3029, 3038, 1002, 2002);
            this.DoConcept(1109);
            IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
            IPair tajil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).LastOrDefault();
            if ((this.DoConcept(3002).Value == 1 && this.DoConcept(3029).Value > MyRule["First", this.RuleCalculateDate].ToInt()) || this.DoConcept(3021).Value > 0)
            {
                return;
            }

            if (Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)).Value > 0 && this.DoConcept(1).Value > 0)
            {
                int StartShift = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                IPair StartMissionPair = ((PairableScndCnpValue)this.DoConcept(2002)).Pairs.OrderBy(x => x.From).FirstOrDefault();
                if (StartMissionPair != null && StartMissionPair.From - this.DoConcept(3029).Value == StartShift)
                {
                    this.DoConcept(3021).Value = Operation.Minimum(this.DoConcept(3029).Value, MyRule["Second", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3029)).DecreasePairFromFirst(this.DoConcept(3021).Value);
                    this.DoConcept(2).Value += this.DoConcept(3021).Value;

                    this.ReCalculate(13, 3020, 3028);
                }
                else
                {                    
                    #region چک کردن حضور قبل و بعد تاخیر و تعجیل و سقف مقدار

                    int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From;
                    int shiftEnd = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.To;

                    if (takhir.From != shiftStart || takhir.To != ((PairableScndCnpValue)this.DoConcept(1)).Pairs.OrderBy(x => x.From).First().From)
                    {
                       return;                
                    }
                    if (tajil.To != shiftEnd || tajil.From != ((PairableScndCnpValue)this.DoConcept(1)).Pairs.OrderBy(x => x.From).Last().To)
                    {
                        return;                      
                    }
                                    
                    #endregion
                    this.DoConcept(3021).Value = Operation.Minimum(this.DoConcept(3029).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3029)).DecreasePairFromFirst(this.DoConcept(3021).Value);
                    this.DoConcept(2).Value += this.DoConcept(3021).Value;

                    this.ReCalculate(13, 3020, 3028);

                    if (this.DoConcept(3029).Value > 0)
                        this.DoConcept(3038).Value = 1;
                }
            }
            GetLog(MyRule, " After Execute State:", 2, 3008, 3028, 3020, 3021, 3029, 3038, 2002);
        }

        /// <summary>قانون کم کاري 1-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي سي و نه-39 درنظر گرفته شده است</remarks>
        public virtual void R5002(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي 2
            //غيبت خالص ساعتي 3001

            //مدت تعجيل مجاز 3012

            //تعجيل ساعتي مجاز 3022
            //تعجيل ساعتي غيرمجاز 3030
            //3036 تعداد بازه های تعجیل

            //1002 مرخصي خالص استحقاقي ساعتي 
            //2002 ماموريت خالص ساعتي
            //1056 مرخصی بی حقوق ساعتی 12

            GetLog(MyRule, " Before Execute State:", 2, 3020, 3022, 3028, 3030, 3036, 1002, 2002);
            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2023);
            if ((this.DoConcept(3002).Value == 1 && this.DoConcept(3030).Value > this.DoConcept(3012).Value) || this.DoConcept(3022).Value > 0)
                return;
            if (Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)).Value > 0 && this.DoConcept(1).Value > 0)
            {

                this.DoConcept(3022).Value = Operation.Minimum(this.DoConcept(3030).Value,
                                                                                this.DoConcept(3012).Value);
                ((PairableScndCnpValue)this.DoConcept(3030)).DecreasePairFromLast(this.DoConcept(3022).Value);
                this.DoConcept(2).Value += this.DoConcept(3022).Value;

                this.ReCalculate(13, 3020, 3028);

                if (this.DoConcept(3030).Value > 0)
                    this.DoConcept(3036).Value = 1;

            }
            GetLog(MyRule, " After Execute State:", 2, 3020, 3022, 3030, 3028, 3036, 1002, 2002);
        }

        /// <summary>قانون کم کاري 1-3</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل-3006 درنظر گرفته شده است</remarks>
        public virtual void R5003(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي 2
            //غيبت خالص ساعتي 3001

            //مدت غيبت بين وقت مجاز 3017

            //غيبت بين وقت ساعتي مجاز 3023
            //غيبت بين وقت ساعتي غيرمجاز 3031

            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي

            GetLog(MyRule, " Before Execute State:", 2, 3023, 3031, 1002, 2002);
            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2023);

            if (Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)).Value > 0 && this.DoConcept(1).Value > 0)
            {
                this.DoConcept(3023).Value = Operation.Minimum(this.DoConcept(3031).Value,
                                                                                this.DoConcept(3017).Value);
                ((PairableScndCnpValue)this.DoConcept(3031)).DecreasePairFromFirst(this.DoConcept(3023).Value);

                this.DoConcept(2).Value += this.DoConcept(3023).Value;
                this.ReCalculate(13, 3020, 3028);
            }
            GetLog(MyRule, " After Execute State:", 2, 3023, 3031, 1002, 2002);
        }

        /// <summary>قانون کم کاري 2-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و يک-41 درنظر گرفته شده است</remarks>
        public virtual void R5004(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي ماهانه 8

            //تاخير ساعتي مجاز ماهانه 3024
            //3032 تاخير ساعتي غيرمجاز ماهانه

            //غیبت ساعتی مجاز ماهانه 3026 
            //3034 غیبت ساعتی غیرمجاز ماهانه


            if (MyRule["First", this.RuleCalculateDate].ToInt() <= this.DoConcept(3024).Value)
            {
                GetLog(MyRule, " Before Execute State:", 8, 3024, 3026, 3032, 3034);
                int tmp = this.DoConcept(3024).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(8).Value -= tmp;
                this.DoConcept(3026).Value -= tmp;
                this.DoConcept(3024).Value -= tmp;

                this.DoConcept(3034).Value += tmp;
                this.DoConcept(3032).Value += tmp;

                GetLog(MyRule, " After Execute State:", 8, 3024, 3026, 3032, 3034);
            }
        }

        /// <summary>قانون کم کاري 2-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و دو-42 درنظر گرفته شده است</remarks>
        public virtual void R5005(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي ماهانه 8

            //مدت تعجيل مجازماهانه 3013

            //تعجيل ساعتي مجاز ماهانه 3025
            //تعجيل ساعتي غيرمجاز ماهانه 3033

            //غیبت ساعتی مجاز ماهانه 3026 
            //3034 غیبت ساعتی غیرمجاز ماهانه

            if (/*this.DoConcept(3013).Value <=*/
               MyRule["First", this.RuleCalculateDate].ToInt() <=
               this.DoConcept(3025).Value)
            {
                GetLog(MyRule, " Before Execute State:", 8, 3025, 3026, 3033, 3034);

                int tmp = this.DoConcept(3025).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(8).Value -= tmp;
                this.DoConcept(3026).Value -= tmp;
                this.DoConcept(3025).Value -= tmp;

                this.DoConcept(3034).Value += tmp;
                this.DoConcept(3033).Value += tmp;

                GetLog(MyRule, " After Execute State:", 8, 3025, 3026, 3033, 3034);
            }
        }

        /// <summary>قانون کم کاري 3-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و چهار-44 درنظر گرفته شده است</remarks>
        public virtual void R5006(AssignedRule MyRule)
        {
            //3001 غيبت خالص ساعتي
            //کارکردخالص ساعتي 2

            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز

            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي

            GetLog(MyRule, " Before Execute State:", 2, 3020, 3028, 1002, 2002);
            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2002);
            if (this.DoConcept(3020).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                int tmp = this.DoConcept(3020).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(2).Value -= tmp;
                //this.DoConcept(3028).Value = this.DoConcept(3001).Value - this.DoConcept(3020).Value;
                this.DoConcept(3028).Value += tmp;
                this.DoConcept(3020).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                if (this.DoConcept(2).Value < 0)
                {
                    this.DoConcept(2).Value = 0;
                }
            }
            GetLog(MyRule, " After Execute State:", 2, 3020, 3028, 1002, 2002);
        }

        /// <summary>قانون کم کاري 3-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و پنج-45 درنظر گرفته شده است</remarks>
        public virtual void R5007(AssignedRule MyRule)
        {
            //کل تاخیر یا تعجیل بیش از حد مجاز ماهانه غیبت حساب شود 3006
            //کارکردخالص ساعتي ماهانه 8

            //غيبت ساعتي مجاز ماهانه 3026
            //غيبت ساعتي غيرمجاز ماهانه 3034
            GetLog(MyRule, " Before Execute State:", 8, 3026, 3034);
            if (this.DoConcept(3026).Value > MyRule["First", this.RuleCalculateDate].ToInt() * HourMin)
            {
                if (this.DoConcept(3006).Value == 1)
                {
                    this.DoConcept(3034).Value += this.DoConcept(3026).Value;
                    this.DoConcept(8).Value -= this.DoConcept(3026).Value;
                    this.DoConcept(3026).Value = 0;
                }
                else
                {
                    this.DoConcept(3034).Value += this.DoConcept(3026).Value - MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
                    this.DoConcept(8).Value -= this.DoConcept(3026).Value + MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
                    this.DoConcept(3026).Value = MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
                }
            }
            GetLog(MyRule, " After Execute State:", 8, 3026, 3034);
        }

        /// <summary>قانون کم کاري 5-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و هفت-47 درنظر گرفته شده است</remarks>
        public virtual void R5008(AssignedRule MyRule)
        {
            //2 کارکردخالص ساعتي
            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز

            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي

            GetLog(MyRule, " Before Execute State:", 2, 3020, 3028, 1002, 2002);

            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2002);
            this.DoConcept(3028);
            if (this.DoConcept(3028).Value >MyRule["First", this.RuleCalculateDate].ToInt())
            {
                //float coEfficient = (MyRule["Second", this.RuleCalculateDate].ToInt() / 100f) + 1;

                //int tmp = this.DoConcept(3028).Value;
                //int tmp2 = (int)Math.Round((tmp * coEfficient));

                //this.DoConcept(2).Value -= tmp2 - tmp;
                //this.DoConcept(3028).Value = tmp2;

                //طبق سند آقای نجاری
                float coEfficient = (MyRule["Second", this.RuleCalculateDate].ToInt() / 100f);
                int tmp = (int)Math.Round((this.DoConcept(3028).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * coEfficient);

                this.DoConcept(3028).Value += tmp;
                this.DoConcept(2).Value -= tmp;
                this.ReCalculate(13);

                if (this.DoConcept(2).Value < 0)
                {
                    this.DoConcept(2).Value = 0;
                }
            }
            GetLog(MyRule, " After Execute State:", 2, 3020, 3028, 1002, 2002);
        }

        /// <summary>قانون کم کاري 6-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و هشت-48 درنظر گرفته شده است</remarks>
        public virtual void R5009(AssignedRule MyRule)
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
            this.DoConcept(1109);
            this.DoConcept(2002);
            if (this.DoConcept(3028).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                this.DoConcept(3004).Value = 1;
                this.DoConcept(3020).Value = 0;
                //this.DoConcept(3028).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();
                
                if (MyRule.HasParameter("Second", this.RuleCalculateDate))
                {
                    if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(((PairableScndCnpValue)this.DoConcept(2)).Pairs);
                    }
                }
                else
                {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(((PairableScndCnpValue)this.DoConcept(2)).Pairs);
                }
                this.DoConcept(4).Value = 0;
                this.DoConcept(2).Value = 0;
                this.DoConcept(13).Value = 0;
            }
            GetLog(MyRule, " After Execute State:", 2, 4, 3020, 3028, 4002, 3004, 1002, 2002);
        }

        /// <summary>قانون کم کاري 7-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و نه-49 درنظر گرفته شده است</remarks>
        public virtual void R5010(AssignedRule MyRule)
        {
            //تعجيل ساعتي مجاز 3022
            //تعجيل ساعتي غيرمجاز 3030

            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز
            //3004 غيبت روزانه

            //کارکردخالص ساعتي 2
            //4 کارکردخالص روزانه
            //13 کارکردناخالص ساعتی


            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي

            GetLog(MyRule, " Before Execute State:", 3004, 3020, 3028, 3022, 3030, 4, 2, 13, 1002, 2002);
            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2002);
            if (this.DoConcept(3030).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                this.DoConcept(3004).Value = 1;
                this.DoConcept(3020).Value = 0;
                this.DoConcept(3028).Value = 0;

                this.DoConcept(3022).Value = 0;
                this.DoConcept(3030).Value = 0;

                ///طبق سند الگوریتم لازم نیست کارکرد به اضافه کار اضافه شود
                //((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(((PairableScndCnpValue)this.DoConcept(2)).Pairs);

                this.DoConcept(4).Value = 0;
                this.DoConcept(2).Value = 0;
                this.DoConcept(13).Value = 0;
            }
            GetLog(MyRule, " After Execute State:", 3004, 3020, 3028, 3022, 3030, 4, 2, 13, 1002, 2002);
        }

        /// <summary>قانون کم کاري 9-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و دو-52 درنظر گرفته شده است</remarks>
        public virtual void R5011(AssignedRule MyRule)
        {
            //غيبت ساعتي غيرمجاز ماهانه 3034
            //کارکردخالص ساعتي ماهانه 8   
            //کارکرد ناخالص ساعتي ماهانه 3

            GetLog(MyRule, " Before Execute State:", 3034, 8, 3);
            if (this.DoConcept(3034).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                if (MyRule["Second", this.RuleCalculateDate].ToInt() == 0)
                {
                    this.DoConcept(3034).Value -= MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(8).Value += MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(3).Value += MyRule["First", this.RuleCalculateDate].ToInt();
                }
            }
            else if (this.DoConcept(3034).Value > 0)
            {
                int tmp = this.DoConcept(3034).Value;
                this.DoConcept(8).Value += tmp;
                this.DoConcept(3).Value += tmp;
                this.DoConcept(3034).Value = 1;
            }
            GetLog(MyRule, " Before Execute State:", 3034, 8, 3);


            //if (this.DoConcept(3034).Value <= MyRule["First", this.RuleCalculateDate].ToInt())
            //{
            //    GetLog(MyRule, " Before Execute State:", 3, 8, 3034);
            //    //به نظر آقای نجاری لازم نیست با بخشش غیبت غیرمجاز به غیبت مجاز اضافه شود
            //    //this.DoConcept(3026).Value += MyRule["First", this.RuleCalculateDate].ToInt();
            //    int tmp = Operation.Minimum(this.DoConcept(3034).Value, MyRule["First", this.RuleCalculateDate].ToInt());
            //    this.DoConcept(8).Value += tmp;
            //    this.DoConcept(3).Value += tmp;
            //    this.DoConcept(3034).Value -= tmp;

            //    GetLog(MyRule, " After Execute State:", 3, 8, 3034);
            //}
        }

        /// <summary>قانون کم کاري 12-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و شش-56 درنظر گرفته شده است</remarks>
        public virtual void R5012(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي 2
            //غيبت ساعتی غیرمجاز 3028
            //3020 غيبت ساعتی مجاز

            //تعداد بازه های تاخیر 3038

            //مدت تاخيرمجاز 44

            //تاخير ساعتي مجاز 3021
            //3029 تاخير ساعتي غيرمجاز

            //3002 کل تاخیر یا تعجیل بیش از حد مجاز روزانه غیبت حساب شود

            this.DoConcept(3002).Value = 1;


            #region comment
            /*
            GetLog(MyRule, " Before Execute State:", 44, 3020, 3021, 2, 3029, 3022, 3030, 3023, 3031, 3028);
             * 
            if (this.DoConcept(1).Value > 0)
            {
                if (this.DoConcept(44).Value != 0 &&
                    this.DoConcept(3029).Value + this.DoConcept(3021).Value > this.DoConcept(44).Value)
                {
                    this.DoConcept(2).Value -= this.DoConcept(3021).Value;
                    this.DoConcept(3038).Value = 1;
                    //1002 ,2002 از خط پایین حذف شد
                    //زیرا مقدار قبلی آن قابل محاسبه نبود
                    this.ReCalculate(3021, 3029, 3020, 3028);
                }

                //تعجيل خالص ساعتي 3010

                //مدت تعجيل مجاز 3012

                //تعجيل ساعتي مجاز 3022
                //تعجيل ساعتي غيرمجاز 3030

                if (this.DoConcept(3012).Value != 0 &&
                    this.DoConcept(3030).Value + this.DoConcept(3022).Value > this.DoConcept(3012).Value)
                {
                    this.DoConcept(2).Value -= this.DoConcept(3022).Value;
                    this.DoConcept(3036).Value = 1;
                    //1002 ,2002 از خط پایین حذف شد
                    //زیرا مقدار قبلی آن قابل محاسبه نبود
                    this.ReCalculate(3022, 3020, 3030, 3028);
                }

                //3014 غيبت بين وقت خالص ساعتي

                //مدت غيبت بين وقت مجاز 3017

                //غيبت بين وقت ساعتي مجاز 3023
                //غيبت بين وقت ساعتي غيرمجاز 3031
                if (this.DoConcept(3017).Value != 0 &&
                    this.DoConcept(3031).Value + this.DoConcept(3023).Value > this.DoConcept(3017).Value)
                {
                    this.DoConcept(2).Value -= this.DoConcept(3023).Value;
                    //1002 ,2002 از خط پایین حذف شد
                    //زیرا مقدار قبلی آن قابل محاسبه نبود
                    this.ReCalculate(3023, 3020, 3031, 3028);
                }

                if (this.DoConcept(2).Value < 0)
                {
                    this.DoConcept(2).Value = 0;
                }
            }
            GetLog(MyRule, " After Execute State:", 44, 3020, 3021, 2, 3029, 3022, 3030, 3023, 3031, 3028);
             * 
           * */

            #endregion
        }

        /// <summary>قانون کم کاري 13-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي پنجاه و هشت-58 درنظر گرفته شده است</remarks>
        public virtual void R5013(AssignedRule MyRule)
        {
            //تاخيرخالص ساعتي ماهانه 3009
            //کارکردخالص ساعتي ماهانه 8
            //غيبت ساعتي ماهانه 3006

            //مدت تاخيرمجازماهانه 45

            //تاخير ساعتي مجاز ماهانه 3024
            //3032 تاخير ساعتي غيرمجاز ماهانه

            //3006  کل تاخیر یا تعجیل بیش از حد مجاز ماهانه غیبت حساب شود

            this.DoConcept(3006).Value = 1;


            #region comment
            /*
            GetLog(MyRule, " Before Execute State:", 8, 3024, 3032, 3033, 3027, 3035);
            if (this.DoConcept(45).Value <=
                    this.DoConcept(3024).Value)
            {
                this.DoConcept(8).Value -= this.DoConcept(3024).Value - this.DoConcept(45).Value;
                this.DoConcept(3024).Value = this.DoConcept(45).Value;
                this.DoConcept(3032).Value = this.DoConcept(3009).Value - this.DoConcept(3024).Value;
            }

            //تعجيل خالص ساعتي ماهانه 49
            //کارکردخالص ساعتي ماهانه 8
            //غيبت ساعتي ماهانه 3006

            //مدت تعجيل مجازماهانه 3013

            //تعجيل ساعتي مجاز ماهانه 3025
            //تعجيل ساعتي غيرمجاز ماهانه 3033

            if (this.DoConcept(3013).Value <=
                    this.DoConcept(3025).Value)
            {
                this.DoConcept(8).Value -= this.DoConcept(3013).Value;
                this.DoConcept(3025).Value = this.DoConcept(3013).Value;
                this.DoConcept(3033).Value = this.DoConcept(3010).Value - this.DoConcept(3025).Value;
            }

            //غيبت بين وقت خالص ساعتي ماهانه 3016
            //کارکردخالص ساعتي ماهانه 8
            //غيبت ساعتي ماهانه 3006

            //مدت غيبت بين وقت مجازماهانه 3018

            //غيبت بين وقت ساعتي مجاز ماهانه 3027
            //3035 غيبت بين وقت ساعتي غيرمجاز ماهانه

            if (this.DoConcept(3018).Value <=
                    this.DoConcept(3027).Value)
            {
                this.DoConcept(8).Value -= this.DoConcept(3018).Value;
                this.DoConcept(3027).Value = this.DoConcept(3018).Value;
                this.DoConcept(3035).Value = this.DoConcept(3016).Value - this.DoConcept(3027).Value;
            }
            GetLog(MyRule, " After Execute State:", 8, 3024, 3032, 3033, 3027, 3035);
             * */

            #endregion
        }

        /// <summary>قانون کم کاري 16-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت-60 درنظر گرفته شده است</remarks>
        public virtual void R5014(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //کارکردخالص ساعتي 2
            //4 کارکردخالص روزانه
            //7 کارکرددرروز
            if (this.DoConcept(3004).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 2, 4, 3004);
                int coefficient = 2;
                if (MyRule.HasParameter("first", this.RuleCalculateDate))
                {
                    coefficient = MyRule["first", this.RuleCalculateDate].ToInt();
                }
                this.DoConcept(2).Value -= this.DoConcept(3004).Value * this.DoConcept(7).Value;
                this.DoConcept(4).Value = -1 * this.DoConcept(3004).Value;
                this.DoConcept(3004).Value *= coefficient;
                if (this.DoConcept(2).Value < 0)
                {
                    this.DoConcept(2).Value = 0;
                }
                if (this.DoConcept(4).Value < 0)
                {
                    this.DoConcept(4).Value = 0;
                }
                GetLog(MyRule, " After Execute State:", 2, 4, 3004);
            }
        }

        /// <summary>قانون کم کاري 18-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و دو-62 درنظر گرفته شده است</remarks>
        public virtual void R5015(AssignedRule MyRule)
        {
            //کارکرد خالص ساعتي 2
            //تعطيل رسمي 1
            //تعطيل غير رسمي 2
            //تعطيل نوروز 4
            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز
            //13 کارکردناخالص
            //6 کارکرد لازم

            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            if ((proceedTraffic != null && !proceedTraffic.IsFilled) &&
                !proceedTraffic.HasDailyItem &&
                (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0) &&
                (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4")) 
        )// &&  this.DoConcept(3028).Value <= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, " Before Execute State:", 2, 13, 3028);

                
                this.DoConcept(3028).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                if (this.DoConcept(2).Value < this.DoConcept(6).Value - this.DoConcept(3028).Value && MyRule["First", this.RuleCalculateDate].ToInt() > 0)
                {
                    this.DoConcept(2).Value = this.DoConcept(6).Value - this.DoConcept(3028).Value;
                    this.ReCalculate(13);
                }


                GetLog(MyRule, " After Execute State:", 2, 13, 3028);
            }
        }

        /// <summary>قانون کم کاري 19-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و سه-63 درنظر گرفته شده است</remarks>
        public virtual void R5016(AssignedRule MyRule)
        {
            //3028 غيبت ساعتي غيرمجاز

            //کارکردخالص ساعتي 2
            //13 کارکردناخالص ساعتی
            GetLog(MyRule, " Before Execute State:", 2, 3028, 13);

            for (int i = 1; i <= 3; i++)
            {
                int tmp = Operation.Intersect(this.DoConcept(3028),
                                              new PairableScndCnpValuePair(MyRule["Row" + i.ToString() + "_1", this.RuleCalculateDate].ToInt(),
                                                                            MyRule["Row" + i.ToString() + "_2", this.RuleCalculateDate].ToInt())
                                              ).Value * (MyRule["Row" + i.ToString() + "_3", this.RuleCalculateDate].ToInt() / 100);
                this.DoConcept(2).Value -= tmp;
                this.DoConcept(3028).Value += tmp;
            }
            this.ReCalculate(13);
            if (this.DoConcept(2).Value < 0)
            {
                this.DoConcept(2).Value = 0;
            }

            GetLog(MyRule, " After Execute State:", 2, 3028, 13);
        }

        /// <summary>قانون کم کاري 20-1،2،3</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و چهار-64 درنظر گرفته شده است</remarks>
        public virtual void R5017(AssignedRule MyRule)
        {
            //غيبت ساعتي غيرمجاز ماهانه 3034
            //8 کارکردخالص ساعتي ماهانه
            //3 کارکردناخالص ماهانه

            GetLog(MyRule, " Before Execute State:", 8, 3034, 3);
            int tmp = this.DoConcept(3034).Value;
            if (tmp > 0)
            {

                if (tmp < MyRule["1th", this.RuleCalculateDate].ToInt())
                {
                    tmp = (int)(tmp * (MyRule["2th", this.RuleCalculateDate].ToInt() / 100f));
                }
                else if (tmp >= MyRule["3th", this.RuleCalculateDate].ToInt()
                    &&
                    tmp < MyRule["4th", this.RuleCalculateDate].ToInt())
                {
                    tmp = (int)(tmp * (MyRule["5th", this.RuleCalculateDate].ToInt() / 100f));
                }
                else if (tmp >= MyRule["6th", this.RuleCalculateDate].ToInt()
                    &&
                    tmp < MyRule["7th", this.RuleCalculateDate].ToInt())
                {
                    tmp = (int)(tmp * (MyRule["8th", this.RuleCalculateDate].ToInt() / 100f));
                }

                this.DoConcept(8).Value -= tmp;
                this.DoConcept(3).Value -= tmp;
                this.DoConcept(3034).Value += tmp;
            }
            GetLog(MyRule, " After Execute State:", 8, 3034, 3);
        }

        /// <summary>قانون کم کاري 21-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و پنج-65 درنظر گرفته شده است</remarks>
        public virtual void R5018(AssignedRule MyRule)
        {
            //کارکردخالص روزانه ماهانه 5
            //3034 غيبت ساعتي غيرمجاز ماهانه

            //3005 غيبت خالص روزانه ماهانه

            //غيبت درروز 3019

            GetLog(MyRule, " Before Execute State:", 3019,3005, 3034, 5);
            if (this.DoConcept(3019).Value > 0)
            {
                int tmp = this.DoConcept(3034).Value / this.DoConcept(3019).Value;
                int tmp2 = this.DoConcept(3034).Value % this.DoConcept(3019).Value;
                this.DoConcept(3005).Value += tmp;
                this.DoConcept(5).Value -= tmp;
                this.DoConcept(3034).Value = tmp2;
            }
            GetLog(MyRule, " After Execute State:",3019, 3005, 3034, 5);
        }

        /// <summary>قانون کم کاري 22-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و شش-66 درنظر گرفته شده است</remarks>
        public virtual void R5019(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //غيبت ساعتي غيرمجازي 3028
            //1 حضور
            //2005 ماموریت روزانه
            //1090 مرخصی روزانه
            //581 مجموع مرخصی بی حقوق روزانه

            //1003 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي

            GetLog(MyRule, " Before Execute State:", 3028, 3004, 1002, 2002);
            this.DoConcept(1002);
            this.DoConcept(1109);            
            this.DoConcept(2002);
            if (this.DoConcept(3028).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 3028, 3004, 1002, 2002);
                if (this.DoConcept(1).Value == 0 && this.DoConcept(2).Value == 0
                    && this.DoConcept(2023).Value == 0 && this.DoConcept(1082).Value == 0
                    && this.DoConcept(1090).Value == 0 && this.DoConcept(2005).Value == 0
                    && this.DoConcept(1091).Value == 0
                    && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
                {
                    if (this.DoConcept(3028).Value >= MyRule["First", this.RuleCalculateDate].ToInt() &&
                        this.DoConcept(3028).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(3004).Value = 1;
                        this.DoConcept(3028).Value = 0;
                    }
                    else if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > MyRule["Second", this.RuleCalculateDate].ToInt() && MyRule["Second", this.RuleCalculateDate].ToInt() != 0)
                    {
                        this.DoConcept(3004).Value += this.Person.GetShiftByDate(this.RuleCalculateDate).Value /
                                                    MyRule["Second", this.RuleCalculateDate].ToInt();
                        this.DoConcept(3028).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).Value %
                                                    MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                GetLog(MyRule, " After Execute State:", 3028, 3004, 1002, 2002);
            }

        }

        /// <summary>
        /// مقادیر تاخیر و تعجیل مختص افراد اعمال شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5020(AssignedRule MyRule)
        {
            var cnpList = new[] { 2, 13, 3020, 3021, 3022, 3026, 3024, 3025, 3028, 3029, 3030, 3034, 3032, 3033 };

            GetLog(MyRule, " Before Execute State:", cnpList);

            //if (this.RuleCalculateDate.Date == new DateTime(2013,1,22).Date)

            //2 	کارکردخالص ساعتي

            //3020	غيبت ساعتي مجاز
            //3021	تاخير ساعتي مجاز
            //3022	تعجيل ساعتي مجاز
            //3026    غیبت ساعتی مجاز ماهانه

            //3028    غیبت ساعتی غیر مجاز
            //3029	تاخير ساعتي غيرمجاز
            //3034   غيبت ساعتي غيرمجاز ماهانه
            //3030   تعجيل ساعتي غيرمجاز            
            //3032   تاخیر ساعتی غیرمجاز ماهانه 

            //4002   اضافه کارساعتي مجاز

            var kasre_takhir = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_takhir", this.RuleCalculateDate);
            var kasre_tajil = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_tajil", this.RuleCalculateDate);
            bool addToOverwork = false;
            if (MyRule.HasParameter("first", this.RuleCalculateDate)) 
            {
                addToOverwork = MyRule["first", this.RuleCalculateDate].ToInt() > 0;
            }
            PairableScndCnpValue PersonDetailAllowedTakhir = null;
            PairableScndCnpValue takhirMojaz = null;
            PairableScndCnpValue PersonDetailAllowedTajil = null;
            PairableScndCnpValue tajilMojaz = null;

            if (//    تاخیر مجاز فردی
                kasre_takhir != null)
            {
                int takhirMojazVal = Utility.ToInteger(kasre_takhir.Value);
                takhirMojaz = new PairableScndCnpValue();
                if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
                {
                    takhirMojaz.Pairs = new List<IPair>(){(new PairableScndCnpValuePair(
                             this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From,
                             this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From + takhirMojazVal
                             ))};
                }
                PersonDetailAllowedTakhir = Operation.Intersect(this.DoConcept(3029), takhirMojaz);


                if (Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)).Value > 0 &&
                    this.DoConcept(1).Value > 0)
                {
                    if (this.DoConcept(3029).Value > 0)
                    {                        
                        this.DoConcept(3021).Value = PersonDetailAllowedTakhir.Value;
                        ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(this.DoConcept(3029), PersonDetailAllowedTakhir));

                        this.DoConcept(2).Value += this.DoConcept(3021).Value;

                        if (this.DoConcept(3029).Value > 0)
                            this.DoConcept(3038).Value = 1;

                    }
                }
            }
            if (// تعجیل مجاز فردی
                kasre_tajil != null)
            {
                int tajilMojazVal = Utility.ToInteger(kasre_tajil.Value);
                tajilMojaz = new PairableScndCnpValue();
                if (this.DoConcept(3030).Value > 0)
                {
                    tajilMojaz.Pairs = new List<IPair>(){(new PairableScndCnpValuePair(
                           ((PairableScndCnpValue)this.DoConcept(3030)).Pairs.First().To - tajilMojazVal,
                           ((PairableScndCnpValue)this.DoConcept(3030)).Pairs.First().To
                           ))};
                }
                PersonDetailAllowedTajil = Operation.Intersect(this.DoConcept(3030), tajilMojaz);
                
                if (Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)).Value > 0 &&
                    this.DoConcept(1).Value > 0)
                {
                    if (this.DoConcept(3030).Value > 0)
                    {
                        this.DoConcept(3022).Value = PersonDetailAllowedTajil.Value;
                        ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Differance(this.DoConcept(3030), PersonDetailAllowedTajil));
                        this.DoConcept(2).Value += this.DoConcept(3022).Value;

                        if (this.DoConcept(3030).Value > 0)
                            this.DoConcept(3038).Value = 1;
                    }
                }
            }
            if (takhirMojaz != null && addToOverwork)
            {
                PairableScndCnpValue pair = Operation.Differance(takhirMojaz, PersonDetailAllowedTakhir);
                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(pair);
            }
            if (tajilMojaz != null && addToOverwork) 
            {
                PairableScndCnpValue pair = Operation.Differance(tajilMojaz, PersonDetailAllowedTajil);
                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(pair);
            }
            if (kasre_takhir != null
                ||
                kasre_tajil != null)
            {
                this.ReCalculate(13, 3020, 3028, 3026, 3034);
            }

            GetLog(MyRule, " After Execute State:", cnpList);

        }

        /// <summary>
        /// اعمال شیردهی برای برخی از پرسنل
        /// یا از ابتدا و یا از انتها
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5021(AssignedRule MyRule)
        {
            //3020 غیبت ساعتی مجاز
            //3029 تاخیر
            //3030 تعجیل
            //3028 غیبت ساعتی
            //3021 تاخیر مجاز
            //3022 تعجیل مجاز
            //3040 غیبت مجاز شیردهی
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            var conceptList = new[] { 2, 3020, 3028, 3040 };


            GetLog(MyRule, " Before Execute State:", conceptList);

            this.DoConcept(1082);
            this.DoConcept(2023);

            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi", this.RuleCalculateDate);
            var personParam_takhir = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi_takhir", this.RuleCalculateDate);
            var personParam_tajil = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi_tajil", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                (personParam_takhir != null || personParam_tajil != null || personParam!=null) &&
                this.DoConcept(3028).Value > 0
                )
            {

                var minutes = personParam != null ? Utility.ToInteger(personParam.Value) : 0;
                var minutes_takhir = personParam_takhir != null ? Utility.ToInteger(personParam_takhir.Value) : 0;
                var minutes_tajil = personParam_tajil != null ? Utility.ToInteger(personParam_tajil.Value) : 0;

                IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
                IPair tagil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).LastOrDefault();

                IPair tempPair = null;
                if (minutes > 0 && takhir != null && takhir.Value > 0)
                {
                    tempPair = takhir;
                    if (takhir.Value >= minutes)
                    {
                        tempPair = new BasePair(takhir.From, takhir.From + minutes);
                    }
                }
                else if (minutes > 0 && tagil != null && tagil.Value > 0)
                {
                    tempPair = tagil;
                    if (tagil.Value >= minutes)
                    {
                        tempPair = new BasePair(tagil.To - minutes, tagil.To);
                    }
                }
                if (minutes == 0 && minutes_takhir > 0 && takhir != null && takhir.Value > 0)
                {
                    tempPair = takhir;
                    if (takhir.Value >= minutes_takhir)
                    {
                        tempPair = new BasePair(takhir.From, takhir.From + minutes_takhir);
                    }
                }
                if (minutes == 0 && minutes_tajil > 0 && tagil != null && tagil.Value > 0)
                {
                    tempPair = tagil;
                    if (tagil.Value >= minutes_tajil)
                    {
                        tempPair = new BasePair(tagil.To - minutes_tajil, tagil.To);
                    }
                }
                if (tempPair != null && tempPair.Value > 0)
                {


                    this.DoConcept(3020).Value += tempPair.Value;

                    var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                    // غيبت ساعتي مجاز شيردهي
                    ((PairableScndCnpValue)this.DoConcept(3040)).AddPair(tempPair);

                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);
                }               
            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اعمال کسر مهد برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5022(AssignedRule MyRule)
        {
            //	2	کارکردخالص ساعتی
            //	3020	غیبت ساعتی مجاز
            //	3028	غیبت ساعتی غیرمجاز
            //	3042	غیبت مجاز مهد
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي


            var conceptList = new[] { 2, 3042, 3020, 3028 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            this.DoConcept(1082);
            this.DoConcept(2023);

            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_mahd", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                personParam != null &&
                this.DoConcept(3028).Value > 0
                )
            {

                var minutes = Utility.ToInteger(personParam.Value);
                IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
                IPair tagil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).LastOrDefault();

                IPair tempPair = null;
                if (takhir != null && takhir.Value > 0)
                {
                    if (takhir.Value >= minutes)
                    {
                        tempPair = new BasePair(takhir.From, takhir.From + minutes);
                    }
                    else
                    {
                        tempPair = takhir;
                    }
                }
                else if (tagil != null && tagil.Value > 0)
                {
                    if (tagil.Value >= minutes)
                    {
                        tempPair = new BasePair(tagil.To - minutes, tagil.To);
                    }
                    else
                    {
                        tempPair = tagil;
                    }
                }
                if (tempPair != null && tempPair.Value > 0)
                {
                    this.DoConcept(3020).Value += tempPair.Value;

                    var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                    // غيبت ساعتي مجاز مهد
                    ((PairableScndCnpValue)this.DoConcept(3042)).AddPair(tempPair);

                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);
                }
                //////
                /*  foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From))
                  {
                      if (pair.Value > minutes)
                      {
                          IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                          this.DoConcept(3020).Value += tempPair.Value;

                          var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                          ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                          // غيبت ساعتي مجاز مهد
                          ((PairableScndCnpValue)this.DoConcept(3042)).AddPair(tempPair);

                          ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                          pair.To -= minutes;
                          break;
                      }

                      if (pair.Value == minutes)
                      {
                          this.DoConcept(3020).Value += pair.Value;

                          ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                          // غيبت ساعتي مجاز مهد
                          ((PairableScndCnpValue)this.DoConcept(3042)).AddPair(pair);

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
  */
            }
            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>
        /// اعمال کسر تقلیل برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5023(AssignedRule MyRule)
        {
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            var conceptList = new[] { 2, 3, 13, 3020, 3028, 3044, 4002, 4005, 4006, 4007 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            this.DoConcept(1082);
            this.DoConcept(2023);


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
                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From))
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
            }
            GetLog(MyRule, " After Execute State:", conceptList);
        }

        /// <summary>قانون مقداردهی C50</summary>        
        public virtual void R5024(AssignedRule MyRule)
        {
            this.DoConcept(3012).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C53</summary>        
        public virtual void R5025(AssignedRule MyRule)
        {
            this.DoConcept(3017).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C54</summary>        
        public virtual void R5026(AssignedRule MyRule)
        {
            this.DoConcept(3018).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>قانون مقداردهی C55</summary>        
        public virtual void R5027(AssignedRule MyRule)
        {
            this.DoConcept(3019).Value = MyRule["First", this.RuleCalculateDate].ToInt();
        }

        /// <summary>
        /// تبدیل اتوماتیک تاخیر به مرخصی تا سقف ... ساعت
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5028(AssignedRule MyRule)
        {
            //کارکردخالص ساعتي 2
            //13 کارکردناخالص

            //غيبت خالص ساعتي 3001
            //3020 غیبت مجاز ساعتی
            //تاخير ساعتي مجاز 3021
            //3028 غیبت غیر مجاز ساعتی
            //3029 تاخير ساعتي غيرمجاز


            //تاخيرخالص ساعتي 3008


            //تعداد بازه های تاخیر 3038

            //1002 مرخصي خالص استحقاقي ساعتي 
            //2002 ماموريت خالص ساعتي
            //1056 مرخصی بی حقوق ساعتی 12

            //1025 مرخصی با حقوق ساعتی_دادگاه-ورزشی
            //1027 مرخصی با حقوق ساعتی
            //3046 تاخیر تبدیل شده به مرخصی
            //3047 تاخیر تبدیل شده به مرخصی ماهانه
         
            int max = MyRule["First", this.RuleCalculateDate].ToInt();
            int addToLeave = MyRule["Second", this.RuleCalculateDate].ToInt();
          
          
          
            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2023);

            if (this.DoConcept(3047).Value >= max)
            {
                return;
            }

            if (this.DoConcept(3029).Value > 0 && this.DoConcept(1).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 2, 3046, 3028, 1003, 3029, 3047);

                int takhirToLeave = this.DoConcept(3029).Value > addToLeave ? addToLeave : this.DoConcept(3029).Value;

                if (this.DoConcept(3047).Value + takhirToLeave > max)
                {
                    takhirToLeave = max - this.DoConcept(3047).Value;
                }
                PairableScndCnpValuePair pair = new PairableScndCnpValuePair(((PairableScndCnpValue)this.DoConcept(3029)).Pairs.First().From, ((PairableScndCnpValue)this.DoConcept(3029)).Pairs.First().From + takhirToLeave);

                int demandLeave = this.Person.GetRemainLeave(this.RuleCalculateDate);
                if (demandLeave >= pair.Value || this.DoConcept(1098).Value > 0)
                {
                    this.Person.AddUsedLeave(this.RuleCalculateDate, pair.Value, null);
                    ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(this.DoConcept(3029), pair));
                    ((PairableScndCnpValue)this.DoConcept(3046)).AppendPair(pair);
                    ((PairableScndCnpValue)this.DoConcept(1003)).AppendPair(pair);
                    int beforeVal = this.DoConcept(3047).Value;
                    this.ReCalculate(13, 3028, 3047);                              
                }
            }
            GetLog(MyRule, " After Execute State:", 2, 3046, 3028, 1003, 3029, 3047);
        }

        /// <summary>
        /// اعمال غیبت در تعطیلات
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5029(AssignedRule MyRule)
        {
            //3004 غیبت روزانه
            GetLog(MyRule, " Before Execute State:", 3004);
            if (this.DoConcept(3004).Value > 0
                && this.DoConcept(3004, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
               
                bool x1 = Utility.ToBoolean(MyRule["TatilRasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1");

                bool y1 = Utility.ToBoolean(MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2");

                bool z1 = Utility.ToBoolean(MyRule["GheirKari", this.RuleCalculateDate].ToInt());
                bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

                int beforeDays = 0;
                int maxLoopcounter = 7;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {
                    beforeDays++;
                    maxLoopcounter--;
                    if (maxLoopcounter < 1)
                    {
                        break;
                    }
                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1");

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2");

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (this.DoConcept(3004, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {
                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(3004, calcDate).Value = 1;
                        this.DoConcept(4).Value = 0;
                        this.DoConcept(2).Value = 0;
                        this.DoConcept(13).Value = 0;
                    }
                }
            }

            GetLog(MyRule, " After Execute State:", 3004);
        }

        /// <summary>
        ///  هر .... ساعت غیبت معادل یک روز غیبت می باشد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5030(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //غيبت ساعتي غيرمجاز 3028
            //1 حضور

            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي
            //581 مجموع مرخصی بی حقوق روزانه

            GetLog(MyRule, " Before Execute State:", 3028, 3004, 1002, 2002);
            this.DoConcept(1002);
            this.DoConcept(1056);
            this.DoConcept(2002);
            if (this.DoConcept(3028).Value > 0
                && this.DoConcept(1).Value == 0 && this.DoConcept(2).Value == 0
                && this.DoConcept(1090).Value == 0 && this.DoConcept(2005).Value == 0
                && this.DoConcept(1091).Value == 0
                && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                && MyRule["First", this.RuleCalculateDate].ToInt() != 0)
            {
                this.DoConcept(3004).Value =(int)((float)this.DoConcept(6).Value / (float)MyRule["First", this.RuleCalculateDate].ToInt());

                this.DoConcept(3028).Value = (int)((float)this.DoConcept(6).Value % (float)MyRule["First", this.RuleCalculateDate].ToInt());
            }
            GetLog(MyRule, " After Execute State:", 3028, 3004, 1002, 2002);
        }

        /// <summary>
        /// ماهانه تا سقف ----- کسر کار ساعتی به مرخصی تبدیل شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5031(AssignedRule MyRule)
        {
            //3032 غیبت ساعنی ماهانه

            GetLog(MyRule, " Before Execute State:", 3032);
            if (MyRule["First", this.RuleCalculateDate].ToInt() > 0 && this.DoConcept(3034).Value > 0) 
            {
                int abcentToLeave = this.DoConcept(3034).Value > MyRule["First", this.RuleCalculateDate].ToInt() ? MyRule["First", this.RuleCalculateDate].ToInt() : this.DoConcept(3034).Value;
                this.DoConcept(3034).Value -= abcentToLeave;
                this.Person.AddUsedLeave(this.RuleCalculateDate, abcentToLeave, null);
                if (this.DoConcept(3034).Value == 0) 
                {
                    this.DoConcept(3034).Value = 1;
                }
            }
            GetLog(MyRule, " After Execute State:", 3032);
        }

        /// <summary>قانون کم کاري 1-1</summary>
        /// <remarks>سقف تاخیر یا تعجیل مجاز روزانه</remarks>
        //public virtual void R5032(AssignedRule MyRule)
        //{
        //    //3020 غیبت ساعتی مجاز
        //    //3029 تاخیر
        //    //3030 تعجیل
        //    //3028 غیبت ساعتی
        //    //3021 تاخیر مجاز
        //    //3022 تعجیل مجاز
        //    //3031 غیبت بین وقت ساعتی غیرمجاز
        //    //3024 تاخیر ساعتی مجاز ماهانه
        //    //3026 غیبت ساعتی مجاز ماهانه
        //    //3027 غیبت بین وقت ساعتی مجاز ماهانه
        //    //1082 مجموع انواع مرخصی ساعتی
        //    //2023 مفهوم مجموع ماموريت ساعتي

        //    var conceptList = new[] { 2, 3020, 3022, 3024, 3026, 3031, 3020, 3027, 3028, 3040, 3026 };

        //    GetLog(MyRule, " Before Execute State:", conceptList);

        //    this.DoConcept(1082);
        //    this.DoConcept(2023);
        //    this.DoConcept(1002);
        //    this.DoConcept(1109);
        //    this.DoConcept(2023);

        //    int forje = 0;
        //    int forjeSum = MyRule["First", this.RuleCalculateDate].ToInt();
        //    int forjeTakhir = MyRule["Second", this.RuleCalculateDate].ToInt();
        //    int forjeTajil = MyRule["Third", this.RuleCalculateDate].ToInt();
        //    int forjeBetween = MyRule["Fourth", this.RuleCalculateDate].ToInt();
        //    int forjeMounth = MyRule["Fifth", this.RuleCalculateDate].ToInt();
        //    int forjeMounthTakhir = MyRule["Sixth", this.RuleCalculateDate].ToInt();
        //    int forjeMounthTajil = MyRule["Seventh", this.RuleCalculateDate].ToInt();
        //    int forjeMounthBetween = MyRule["Eighth", this.RuleCalculateDate].ToInt();

        //    int SumHourlyAbsent = this.DoConcept(3024).Value + this.DoConcept(3025).Value + this.DoConcept(3027).Value;

        //    if (
        //        this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
        //        this.DoConcept(1).Value > 0 &&
        //        (forjeSum > 0 || forjeTakhir > 0 || forjeTajil > 0 || forjeBetween > 0) &&
        //        this.DoConcept(3028).Value > 0
        //        )
        //    {

        //        IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
        //        IPair tajil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).LastOrDefault();

        //        #region چک کردن حضور قبل و بعد تاخیر و تعجیل و سقف مقدار

        //        int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From;
        //        int shiftEnd = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.To;

        //        if (takhir.From != shiftStart || takhir.To != ((PairableScndCnpValue)this.DoConcept(1)).Pairs.OrderBy(x => x.From).First().From)
        //        {
        //            takhir = new PairableScndCnpValuePair();
        //        }
        //        if (tajil.To != shiftEnd || tajil.From != ((PairableScndCnpValue)this.DoConcept(1)).Pairs.OrderBy(x => x.From).Last().To)
        //        {
        //            tajil = new PairableScndCnpValuePair();
        //        }

        //        bool notAllowMax = MyRule["Ninth", this.RuleCalculateDate].ToInt() > 0 ? true : false;
        //        if (notAllowMax && ((takhir.Value > forjeSum && forjeSum > 0) || (takhir.Value > forjeTakhir && forjeTakhir > 0 && forjeSum == 0)))
        //        {
        //            takhir = new PairableScndCnpValuePair();
        //        }
        //        if (notAllowMax && ((tajil.Value > forjeSum && forjeSum > 0) || (tajil.Value > forjeTajil && forjeTajil > 0 && forjeSum == 0)))
        //        {
        //            tajil = new PairableScndCnpValuePair();
        //        }

        //        #endregion

        //        PairableScndCnpValue tempPairs = new PairableScndCnpValue();
        //        if (forjeSum > 0)
        //        {
        //            forje = forjeSum;
        //        }
        //        else if (forjeTakhir > 0)
        //        {
        //            forje = forjeTakhir;
        //        }
        //        if (forje > 0 && takhir != null && takhir.Value > 0 && ((takhir.Value + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0
        //            || (((forje + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0))
        //            || ((forje + tempPairs.Value + this.DoConcept(3024).Value <= forjeMounthTakhir) && forjeMounth == 0 && forjeMounthTakhir > 0)
        //            || ((takhir.Value + tempPairs.Value + this.DoConcept(3024).Value <= forjeMounthTakhir) && forjeMounth == 0 && forjeMounthTakhir > 0)))
        //        {
        //            ((PairableScndCnpValue)tempPairs).AddPair(takhir);

        //            if (takhir.Value >= forje)
        //            {
        //                tempPairs.AddPair(new BasePair(takhir.From, takhir.From + forje));
        //                forje = 0;
        //            }
        //            else
        //            {
        //                forje -= takhir.Value;
        //            }
        //        }
        //        if (forjeSum == 0 && forjeTajil > 0)
        //        {

        //            forje = forjeTajil;
        //        }

        //        if (forje > 0 && tajil != null && tajil.Value > 0 && ((tajil.Value + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0
        //            || (((forje + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0))
        //            || ((forje + tempPairs.Value + this.DoConcept(3025).Value <= forjeMounthTajil) && forjeMounth == 0 && forjeMounthTajil > 0)
        //            || ((tajil.Value + tempPairs.Value + this.DoConcept(3025).Value <= forjeMounthTajil) && forjeMounth == 0 && forjeMounthTajil > 0)))
        //        {
        //            ((PairableScndCnpValue)tempPairs).AppendPair(tajil);

        //            if (tajil.Value > forje)
        //            {
        //                ((PairableScndCnpValue)tempPairs).AddPairs(Operation.Differance(tempPairs, tajil));
        //                tempPairs.AppendPair(new BasePair(tajil.To - forje, tajil.To));
        //                forje = 0;
        //            }
        //            else if (tajil.Value <= forje)
        //            {
        //                forje -= tajil.Value;
        //            }
        //        }

        //        if (Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)).Value > 0)
        //        {
        //            PairableScndCnpValue Between = ((PairableScndCnpValue)this.DoConcept(3031));
        //            if (notAllowMax && ((Between.Value > forjeSum && forjeSum > 0) || (Between.Value > forjeBetween && forjeBetween > 0 && forjeSum == 0)))
        //            {
        //                Between = new PairableScndCnpValue();
        //            }
        //            if (forjeSum == 0 && forjeBetween > 0)
        //            {
        //                forje = forjeBetween;
        //            }
        //            if (forje > 0 && Between != null && Between.Value > 0 && ((Between.Value + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0
        //            || (((forje + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0))
        //            || ((forje + tempPairs.Value + this.DoConcept(3027).Value <= forjeMounthBetween) && forjeMounth == 0 && forjeMounthBetween > 0)
        //            || ((Between.Value + tempPairs.Value + this.DoConcept(3027).Value <= forjeMounthBetween) && forjeMounth == 0 && forjeMounthBetween > 0)))
        //            {
        //                foreach (PairableScndCnpValuePair pair in Between.Pairs.OrderBy(x => x.From))
        //                {

        //                    PairableScndCnpValue tempBetween = new PairableScndCnpValue();
        //                    ((PairableScndCnpValue)tempBetween).AppendPair(pair);

        //                    if (tempBetween.Value <= forje && forje > 0)
        //                    {
        //                        tempPairs.AppendPair(pair);
        //                        forje -= tempBetween.Value;
        //                    }

        //                    else if (tempBetween.Value > forje && forje > 0)
        //                    {
        //                        tempPairs.AppendPair(new BasePair(pair.From, pair.From + forje));
        //                        forje = 0;
        //                    }

        //                }

        //            }
        //            this.DoConcept(3023).Value = Operation.Intersect(this.DoConcept(3031), tempPairs).Value;

        //            ((PairableScndCnpValue)this.DoConcept(3031)).DecreasePairFromFirst(this.DoConcept(3023).Value);

        //            this.DoConcept(2).Value += this.DoConcept(3023).Value;
        //            this.ReCalculate(13, 3020, 3028);
        //        }

        //        if (tempPairs != null && tempPairs.Value > 0)
        //        {
        //            if (!MyRule.HasParameter("Eleventh", this.RuleCalculateDate) || MyRule["Eleventh"].ToInt() == 0)
        //            {
        //                this.DoConcept(3020).Value = 0;
        //                this.DoConcept(3020).Value += tempPairs.Value;
        //                var AllowTakhir = tempPairs.Pairs.Where(x => x.From == shiftStart).FirstOrDefault();
        //                var Allowtajil = tempPairs.Pairs.Where(x => x.To == shiftEnd).FirstOrDefault();
        //                //  var AllowBetween = Operation.Differance((Operation.Differance(this.DoConcept(3020), AllowTakhir)),Allowtajil);
        //                if (AllowTakhir != null)
        //                {
        //                    this.DoConcept(3021).Value += AllowTakhir.Value;

        //                }

        //                if (Allowtajil != null)
        //                {
        //                    this.DoConcept(3022).Value += Allowtajil.Value;
        //                    // this.DoConcept(3023).Value += tempPairs.Value -( Allowtajil.Value+AllowTakhir.Value);
        //                }
        //                this.DoConcept(3020).Value = this.DoConcept(3021).Value + this.DoConcept(3022).Value + this.DoConcept(3023).Value;

        //            }

        //            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), tempPairs));
        //            ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(this.DoConcept(3029), tempPairs));
        //            ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Differance(this.DoConcept(3030), tempPairs));
        //            ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Differance(this.DoConcept(3031), tempPairs));


        //            ///

        //        }
        //    }

        //    GetLog(MyRule, " After Execute State:", conceptList);
        //}

        /// <summary>قانون کم کاري 1-1</summary>
        /// <remarks>سقف تاخیر یا تعجیل مجاز روزانه</remarks>
        public virtual void R5033(AssignedRule MyRule)
        {
            //3031 غیبت بین وقت ساعتی غیرمجاز
            //3028 غیبت غیر مجاز ساعتی
            //2 کارکردخالص ساعتي
            //(3020)غيبت ساعتي مجاز روزانه
            this.DoConcept(3020);
            this.DoConcept(3028);
            this.DoConcept(3031);
            GetLog(MyRule, " Before Execute State:", 3031, 3028, 2);
            int t = Operation.Intersect(this.DoConcept(3031), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), (int)MyRule["Second", this.RuleCalculateDate].ToInt())).Value;
            int t4 = Operation.Minimum(t, MyRule["Third", this.RuleCalculateDate].ToInt());
            int t5 = t - t4;
            int tmp=0;
            ((PairableScndCnpValue)this.DoConcept(3031)).DecreasePairFromLast(t4);
            this.DoConcept(2).Value += t4;
            this.DoConcept(3028).Value -= t4;
            this.DoConcept(3020).Value += t4;
            if (this.DoConcept(3028).Value > 0 && MyRule["Fourth", this.RuleCalculateDate].ToInt() > 0 && MyRule["Fifth", this.RuleCalculateDate].ToInt() > 0)
            {
                float coEfficient = (MyRule["Fourth", this.RuleCalculateDate].ToInt() / 100f);
                 tmp = (int)Math.Round(t5 * coEfficient);
           
             }
            else if (this.DoConcept(3028).Value > 0 && MyRule["Fourth", this.RuleCalculateDate].ToInt() > 0 && MyRule["Fifth", this.RuleCalculateDate].ToInt() == 0)
            {
                float coEfficient = (MyRule["Fourth", this.RuleCalculateDate].ToInt() / 100f);
                 tmp = (int)Math.Round((this.DoConcept(3028).Value) * coEfficient);
            }

            this.DoConcept(3028).Value += tmp;
            this.DoConcept(2).Value -= tmp;
            this.ReCalculate(13);

            if (this.DoConcept(2).Value < 0)
            {
                this.DoConcept(2).Value = 0;
            }
            GetLog(MyRule, " After Execute State:", 3031, 3028, 2);

        }
        #endregion

        #region قوانين اضافه کاري

        /// <summary>قانون اضافه کاري 1-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصد و پنجاه-150 درنظر گرفته شده است</remarks>
        public virtual void R6001(AssignedRule MyRule)
        {
            //4015 اضافه کار با مجوز باشد
            this.DoConcept(4015).Value = 1;

            ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4002));
            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
        }

        /// <summary>قانون اضافه کاري 1-2</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصد و پنجاه و يک-151 درنظر گرفته شده است</remarks>
        public virtual void R6002(AssignedRule MyRule)
        {
            //4016 مفهوم اضافه کار بعد از وقت مجوزی است
            this.DoConcept(4016).Value = 1;
        }

        /// <summary>قانون اضافه کاري 1-1</summary>
        /// <param name="Result"></param>
        /// <remarks>اضافه کار در تعطیلات غیر مجاز است</remarks>
        public virtual void R6003(AssignedRule MyRule)
        {
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4002, 4003, 13 });

            var rasmi = MyRule["first", this.RuleCalculateDate].ToInt() > 0 ? true : false;
            var nRasmi = MyRule["second", this.RuleCalculateDate].ToInt() > 0 ? true : false;

            if ((rasmi && EngEnvironment.HasCalendar(this.RuleCalculateDate, "1")) || (nRasmi && (EngEnvironment.HasCalendar(this.RuleCalculateDate, "2") || this.Person.GetShiftByDate(this.RuleCalculateDate).PairCount == 0))
                && this.DoConcept(4002).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4002));
                ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
        }

        /// <summary>قانون اضافه کاري 3-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و سه-153 درنظر گرفته شده است</remarks>        
        public virtual void R6004(AssignedRule MyRule)
        {
            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4008 اضافه کارساعتي قبل ازوقت

            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4008, 13);
            if (this.DoConcept(4008).Value < MyRule["First", this.RuleCalculateDate].ToInt())
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4008)));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4008));
                ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 4008, 13);
        }

        /// <summary>قانون اضافه کاري 3-2</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و چهار-154 درنظر گرفته شده است</remarks>       
        public virtual void R6005(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4007, 13);

            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4007 اضافه کارساعتي بعد ازوقت

            if (this.DoConcept(4007).Value < MyRule["First", this.RuleCalculateDate].ToInt())
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4007)));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4007));
                ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 4007, 13);
        }

        /// <summary>قانون اضافه کاري 4-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و پنج-10 درنظر گرفته شده است</remarks>       
        public virtual void R6006(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4008, 13);

            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4008 اضافه کارساعتي قبل ازوقت

            if (this.DoConcept(4008).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                int notAllowValue = this.DoConcept(4008).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                PairableScndCnpValue notAllow = new PairableScndCnpValue();
                foreach (PairableScndCnpValuePair pair in ((PairableScndCnpValue)this.DoConcept(4008)).Pairs.OrderBy(x => x.From))
                {
                    if (pair.Value >= notAllowValue)
                    {
                        IPair notAloowPair = new PairableScndCnpValuePair(pair.From, pair.From + notAllowValue);
                        notAllow.Pairs.Add(notAloowPair);
                        notAllowValue = 0;
                        break;
                    }
                    else
                    {
                        notAllowValue -= pair.Value;
                        notAllow.Pairs.Add(pair);
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(Operation.Intersect(this.DoConcept(4002), notAllow));
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), notAllow));
                //((PairableScndCnpValue)this.DoConcept(4008)).AddPairs(Operation.Differance(this.DoConcept(4008), notAllow));
                ////
                //((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromFirst(this.DoConcept(4008).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                //((PairableScndCnpValue)this.DoConcept(4003)).IncreaseValue(this.DoConcept(4008).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                //this.DoConcept(4008).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                this.ReCalculate(13,4008);
            }

            GetLog(MyRule, " After Execute State:", 4002, 4003, 4008, 13);
        }

        /// <summary>قانون اضافه کاري 4-2</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و شش-3 درنظر گرفته شده است</remarks>       
        public virtual void R6007(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4007, 13);

            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4007 اضافه کارساعتي بعد ازوقت

            if (this.DoConcept(4007).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                int notAllowValue = this.DoConcept(4007).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                PairableScndCnpValue notAllow = new PairableScndCnpValue();
                foreach (PairableScndCnpValuePair pair in ((PairableScndCnpValue)this.DoConcept(4007)).Pairs.OrderByDescending(x => x.From))
                {
                    if (pair.Value >= notAllowValue)
                    {
                        IPair notAloowPair = new PairableScndCnpValuePair(pair.To - notAllowValue, pair.To);
                        notAllow.Pairs.Add(notAloowPair);
                        notAllowValue = 0;
                        break;
                    }
                    else
                    {
                        notAllowValue -= pair.Value;
                        notAllow.Pairs.Add(pair);
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(Operation.Intersect(this.DoConcept(4002), notAllow));
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), notAllow));
                //((PairableScndCnpValue)this.DoConcept(4007)).AddPairs(Operation.Differance(this.DoConcept(4007), notAllow));
                //((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(this.DoConcept(4007).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                //((PairableScndCnpValue)this.DoConcept(4003)).IncreaseValue(this.DoConcept(4007).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                //this.DoConcept(4007).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                this.ReCalculate(13,4007);
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 4007, 13);
        }

        /// <summar
        /// 
        /// y>قانون اضافه کاري 5-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و هفت-1016 درنظر گرفته شده است</remarks>
        public virtual void R6008(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 13);
            //4015 اضافه کار با مجوز باشد
            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4017 اضافه کار مجاز کارتی

            if (this.DoConcept(4015).Value == 1)
                return;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                 && this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                int notAllowedValue = this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4002)).Pairs)
                {
                    if (pair.Value - notAllowedValue > 0)
                    {
                        IPair notAllowedPair = new PairableScndCnpValuePair(pair.To - notAllowedValue, pair.To);

                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(notAllowedPair);

                        pair.To -= notAllowedValue;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else if (pair.Value - notAllowedValue == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        notAllowedValue -= pair.Value;

                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                    }
                }
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 13);
        }

        /// <summary>قانون اضافه کاري 5-2</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و هشت-158 درنظر گرفته شده است</remarks>
        public virtual void R6009(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 13);

            //4015 اضافه کار با مجوز باشد
            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز

            if (this.DoConcept(4015).Value == 1)
                return;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0
                && this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                int notAllowedValue = this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4002)).Pairs)
                {
                    if (pair.Value - notAllowedValue > 0)
                    {
                        IPair notAllowedPair = new PairableScndCnpValuePair(pair.To - notAllowedValue, pair.To);

                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(notAllowedPair);

                        pair.To -= notAllowedValue;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else if (pair.Value - notAllowedValue == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        notAllowedValue -= pair.Value;

                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                    }
                }
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 13);
        }

        /// <summary>قانون اضافه کاري 6-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و نه-1018 درنظر گرفته شده است</remarks>
        public virtual void R6010(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 3, 4005, 4006, 4018);

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غيرمجاز ماهانه
            //3 کارکرد ناخالص ساعتی ماهانه
            //4018 مفهوم حداکثر اضافه کار مجاز ماهانه
            //if (this.DoConcept(4005).Value >= MyRule["First", this.RuleCalculateDate].ToInt() * HourMin)
            this.DoConcept(4018).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            if (this.DoConcept(4005).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                this.DoConcept(3).Value -= this.DoConcept(4005).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(4006).Value += this.DoConcept(4005).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(4005).Value = MyRule["First", this.RuleCalculateDate].ToInt();

                //if (this.DoConcept(3).Value < 0)
                //{
                //    this.DoConcept(3).Value = 0;
                //}
            }
            GetLog(MyRule, " After Execute State:", 3, 4005, 4006, 4018);
        }

        /// <summary>قانون اضافه کاري 6-2</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت-1019 درنظر گرفته شده است</remarks>
        public virtual void R6011(AssignedRule MyRule)
        {
            ///به دليل اينکه در مورد اضافه کار تعطيل نيازي به نگهداري مقادير مجاز
            ///و غير مجاز نيست در اينجا خود اضافه کار تعطيل را مقداردهي مي نماييم
            GetLog(MyRule, " Before Execute State:", 3, 4005, 4006, 4010);

            //4010 مفهوم اضافه کارساعتي تعطيل ماهانه
            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غيرمجاز ماهانه
            //3 کارکرد ناخالص ساعتی ماهانه

            if (MyRule["First", this.RuleCalculateDate].ToInt() > 0
                 && this.DoConcept(4010).Value > MyRule["First", this.RuleCalculateDate].ToInt() * HourMin)
            {
                int tmp = this.DoConcept(4010).Value - MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
                this.DoConcept(3).Value -= tmp;
                this.DoConcept(4005).Value -= tmp;
                this.DoConcept(4006).Value += tmp;
                this.DoConcept(4010).Value = MyRule["First", this.RuleCalculateDate].ToInt() * HourMin;
            }
            GetLog(MyRule, " After Execute State:", 3, 4005, 4006, 4010);
        }

        /// <summary>حد اکثر اضافه کاری روز تعطیل رسمی</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و هشت-158 درنظر گرفته شده است</remarks>
        public virtual void R6012(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 13);

            //4015 اضافه کار با مجوز باشد
            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز

            if (this.DoConcept(4015).Value == 1)
                return;
            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1")
                && this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                int notAllowedValue = this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt();
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4002)).Pairs)
                {
                    if (pair.Value - notAllowedValue > 0)
                    {
                        IPair notAllowedPair = new PairableScndCnpValuePair(pair.To - notAllowedValue, pair.To);

                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(notAllowedPair);

                        pair.To -= notAllowedValue;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else if (pair.Value - notAllowedValue == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        notAllowedValue -= pair.Value;

                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                    }
                }
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 13);
        }

        /// <summary>قانون اضافه کاري 7-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و دو-162 درنظر گرفته شده است</remarks>
        public virtual void R6013(AssignedRule MyRule)
        {
            //1095 مجموع انواع مرخصی روزانه
            //4002 اضافه کار ساعتي مجاز
            //کارکرد خالص ساعتي 2
            //13 کارکرد ناخالص ساعتی

            //3021 اضافه کار اجباري

            GetLog(MyRule, " Before Execute State:", 4002, 2, 13);

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0
                && this.DoConcept(1095).Value > 0)
                return;

            int tmp = Operation.Minimum(this.DoConcept(2).Value, MyRule["First", this.RuleCalculateDate].ToInt());
            ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(tmp);
            this.DoConcept(2).Value -= tmp;
            this.ReCalculate(13);

            GetLog(MyRule, " After Execute State:", 4002, 2, 13);

        }

        /// <summary>قانون اضافه کاري 8-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و سه-163 درنظر گرفته شده است</remarks>
        public virtual void R6014(AssignedRule MyRule)
        {
            //4002 اضافه کار ساعتي مجاز
            //13 کارکرد ناخالص
            //4008 اضافه کاراول وقت
            //4007 اضافه کارآخروقت
            float i = 0;

            //روز عادي(روزي که کاري باشد
            this.DoConcept(4026).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(4027).Value = MyRule["Second", this.RuleCalculateDate].ToInt();
            this.DoConcept(4028).Value = MyRule["Third", this.RuleCalculateDate].ToInt();
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4007, 4008, 13);

                float coEfficient = MyRule["Second", this.RuleCalculateDate].ToInt() / 100f;
                //coEfficient += 1;
                int min = Operation.Minimum(this.DoConcept(4002).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                i = min * coEfficient;
                if (this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    i += (this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Third", this.RuleCalculateDate].ToInt() / 100);
                }

                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(i));
                min = Operation.Minimum(this.DoConcept(4007).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                i = min * coEfficient;
                if (this.DoConcept(4007).Value > MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    i += (this.DoConcept(4007).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Third", this.RuleCalculateDate].ToInt() / 100);
                }
                ((PairableScndCnpValue)this.DoConcept(4007)).IncreaseValue((int)Math.Round(i));

                min = Operation.Minimum(this.DoConcept(4008).Value, MyRule["First", this.RuleCalculateDate].ToInt() - min);
                i = min * coEfficient;
                if (this.DoConcept(4008).Value > MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    i += (this.DoConcept(4008).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Third", this.RuleCalculateDate].ToInt() / 100);
                }
                ((PairableScndCnpValue)this.DoConcept(4008)).IncreaseValue((int)Math.Round(i));
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 4007, 4008, 13);
            }
        }

        /// <summary>قانون اضافه کاري 9-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و چهار-164 درنظر گرفته شده است</remarks>
        public virtual void R6015(AssignedRule MyRule)
        {
            //4002 اضافه کار ساعتي مجاز
            //اضافه کار ساعتي تعطيل 4009

            //1 تعطيل رسمي
            //2 تعطيل غيررسمي
            this.DoConcept(4026).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(4027).Value = MyRule["Second", this.RuleCalculateDate].ToInt();
            this.DoConcept(4028).Value = MyRule["Third", this.RuleCalculateDate].ToInt();
            float i = 0;
            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "2")
                && !EngEnvironment.HasCalendar(this.RuleCalculateDate, "1"))
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4009);
                float coEfficient = MyRule["Second", this.RuleCalculateDate].ToInt() / 100f;
                //coEfficient += 1;
                int min = Operation.Minimum(this.DoConcept(4002).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                i = min * coEfficient; ;
                if (this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    i += (this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Third", this.RuleCalculateDate].ToInt() / 100);
                }

                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(i));
                this.DoConcept(4009).Value += (int)Math.Round(i);

                GetLog(MyRule, " After Execute State:", 4002, 4009);
            }
        }

        /// <summary>قانون اضافه کاري 10-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و پنج-165 درنظر گرفته شده است</remarks>
        public virtual void R6016(AssignedRule MyRule)
        {
            //4002 اضافه کار ساعتي مجاز

            float i = 0;

            this.DoConcept(4026).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(4027).Value = MyRule["Second", this.RuleCalculateDate].ToInt();
            this.DoConcept(4028).Value = MyRule["Third", this.RuleCalculateDate].ToInt();
            //روز غيرکاري
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0
                && !EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
            {
                GetLog(MyRule, " Before Execute State:", 4002);
                float coEfficient = MyRule["Second", this.RuleCalculateDate].ToInt() / 100f;
                //coEfficient += 1;
                int min = Operation.Minimum(this.DoConcept(4002).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                i = min * coEfficient;
                if (this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    coEfficient = MyRule["Third", this.RuleCalculateDate].ToInt() / 100f;
                    // coEfficient += 1;
                    i += (this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * coEfficient;
                }
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(i));

                GetLog(MyRule, " After Execute State:", 4002);
            }
        }

        /// <summary>قانون اضافه کاري 11-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و سه-163 درنظر گرفته شده است</remarks>
        public virtual void R6017(AssignedRule MyRule)
        {
            //4002 اضافه کار ساعتي مجاز
            //اضافه کار ساعتي تعطيل 4009

            //1 تعطيل رسمي
            this.DoConcept(4026).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(4027).Value = MyRule["Second", this.RuleCalculateDate].ToInt();
            this.DoConcept(4028).Value = MyRule["Third", this.RuleCalculateDate].ToInt();
            float i = 0;
            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1"))
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4009);
                float coEfficient = MyRule["Second", this.RuleCalculateDate].ToInt() / 100f;
                //coEfficient += 1;
                int min = Operation.Minimum(this.DoConcept(4002).Value, MyRule["First", this.RuleCalculateDate].ToInt());
                i = min * coEfficient;
                if (this.DoConcept(4002).Value > MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    coEfficient = MyRule["Third", this.RuleCalculateDate].ToInt() / 100f;
                    //coEfficient += 1;
                    i += (this.DoConcept(4002).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * coEfficient;
                }
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(i));
                this.DoConcept(4009).Value += (int)Math.Round(i);
                GetLog(MyRule, " After Execute State:", 4002, 4009);
            }

        }

        /// <summary>قانون اضافه کاري 12-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و پنج-165 درنظر گرفته شده است</remarks>
        public virtual void R6018(AssignedRule MyRule)
        {
            //4002 اضافه کار ساعتي مجاز
            //4012 اضافه کارساعتي شب
            this.DoConcept(4026).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(4027).Value = MyRule["Second", this.RuleCalculateDate].ToInt();
            this.DoConcept(4028).Value = MyRule["Third", this.RuleCalculateDate].ToInt();
            GetLog(MyRule, " Before Execute State:", 4002, 4012);
            int i = Operation.Minimum(this.DoConcept(4012).Value, MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Second", this.RuleCalculateDate].ToInt() / 100);

            if (this.DoConcept(4012).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                i = (this.DoConcept(4012).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Third", this.RuleCalculateDate].ToInt() / 100);
            }
            ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(i);
            this.DoConcept(4012).Value += i;
            GetLog(MyRule, " After Execute State:", 4002, 4012);
        }

        /// <summary>قانون اضافه کاري 13-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و شصت و شش-166 درنظر گرفته شده است</remarks>
        public virtual void R6019(AssignedRule MyRule)
        {
            //اضافه کار ساعتي تعطيل 4009 
            //4012 اضافه کارساعتي شب
            //4002 اضافه کار ساعتي مجاز
            //4011 اضافه کارساعتي شب تعطيل
            this.DoConcept(4026).Value = MyRule["First", this.RuleCalculateDate].ToInt();
            this.DoConcept(4027).Value = MyRule["Second", this.RuleCalculateDate].ToInt();
            this.DoConcept(4028).Value = MyRule["Third", this.RuleCalculateDate].ToInt();
            GetLog(MyRule, " Before Execute State:", 4009, 4011, 4002, 4012);
            int i = Operation.Minimum(this.DoConcept(4011).Value, MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Second", this.RuleCalculateDate].ToInt() / 100);
            if (this.DoConcept(4011).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                i = (this.DoConcept(4011).Value - MyRule["First", this.RuleCalculateDate].ToInt()) * (MyRule["Third", this.RuleCalculateDate].ToInt() / 100);
            }
            this.DoConcept(4009).Value += i;
            this.DoConcept(4011).Value += i;
            ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(i);
            this.DoConcept(4012).Value += i;

            GetLog(MyRule, " After Execute State:", 4009, 4011, 4002, 4012);
        }

        /// <summary>
        /// اعمال سقف اضافه کار بصورت مجزا برای هر شخص
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6020(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 3, 13, 4005, 4006, 4018);

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غيرمجاز ماهانه
            //3 کارکرد ناخالص ساعتی ماهانه
            //4018 مفهوم حداکثر اضافه کار مجاز ماهانه

            if (
                !string.IsNullOrEmpty(this.Person.PersonTASpec.R10)
                && Utility.ToInteger(this.Person.PersonTASpec.R10) > 0
                && this.DoConcept(4005).Value > 0
                )
            {
                this.DoConcept(4018).Value = Utility.ToInteger(this.Person.PersonTASpec.R10) * 60;
                this.ReCalculate(3, 13, 4005, 4006);
                if (this.DoConcept(4005).Value > Utility.ToInteger(this.Person.PersonTASpec.R10) * 60)
                {
                    this.DoConcept(3).Value -= this.DoConcept(4005).Value - Utility.ToInteger(this.Person.PersonTASpec.R10) * 60;
                    this.DoConcept(4006).Value += this.DoConcept(4005).Value - Utility.ToInteger(this.Person.PersonTASpec.R10) * 60;
                    this.DoConcept(4005).Value = Utility.ToInteger(this.Person.PersonTASpec.R10) * 60;
                }
            }

            GetLog(MyRule, " After Execute State:", 3, 13, 4005, 4006, 4018);
        }

        /// <summary>قانون اضافه کاري 18-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و يک-171 درنظر گرفته شده است</remarks>
        public virtual void R6021(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4002
            //4014 اضافه کار مجاز بين وقت
            //4012 اضافه کارساعتي مجازشب
            //69 مفهوم استراحت بين وقت
            //14 مفهوم شب

            GetLog(MyRule, " Before Execute State:", 4002, 4014, 4012, 13);

            ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Union(this.DoConcept(4002), this.DoConcept(4014)));
            this.ReCalculate(13);


            GetLog(MyRule, " After Execute State:", 4002, 4014, 4012, 13);
        }

        /// <summary>قانون اضافه کاري 22-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و دو-172 درنظر گرفته شده است</remarks>
        public virtual void R6022(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005            
            //اضافه کار ساعتي 4002
            //1 مفهوم حضور

            if (this.DoConcept(2005).Value > 0 && this.DoConcept(1).Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 13);

                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(((PairableScndCnpValue)this.DoConcept(1)).Pairs);

                //float coEfficient = 1 + (MyRule["First", this.RuleCalculateDate].ToInt() / 100f);
                var coEfficient = (int)Math.Round((decimal)(this.DoConcept(4002).Value * (MyRule["First", this.RuleCalculateDate].ToInt() / 100)));

                //((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(this.DoConcept(4002).Value * coEfficient));
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(coEfficient);
                this.ReCalculate(13);

                GetLog(MyRule, " After Execute State:", 4002, 13);
            }
        }

        /// <summary>قانون اضافه کاري 23-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هفتاد و سه-173 درنظر گرفته شده است</remarks>
        public virtual void R6023(AssignedRule MyRule)
        {
            //3028 غیبت ساعتی
            //مجموع انواع مرخصي روزانه 1090            
            //اضافه کار ساعتي 4002
            //2030 کار خارج از اداره
            //2023 مجموع ماموريت ساعتي
            //1 مفهوم حضور
            //4025 تبدیل حضور به اضافه کار در روز مرخصی

            if (this.DoConcept(1090).Value > 0)// && this.DoConcept(4025).Value == 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 13);

                ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();

                foreach (var pair in ((PairableScndCnpValue)this.DoConcept(1)).Pairs)
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                    var coEfficient = (int)Math.Round((decimal)((pair.Value * MyRule["First", this.RuleCalculateDate].ToInt()) / 100));
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(coEfficient);
                }

                this.ReCalculate(13);
                if (this.DoConcept(9).Value > 0)
                {
                    this.ReCalculate(4005);
                }

                GetLog(MyRule, " After Execute State:", 4002, 13);
            }
        }

        /// <summary>
        /// سقف اضافه کار در ایام هفته
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6024(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 13);
            //4015 اضافه کار با مجوز باشد
            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4017 اضافه کار مجاز کارتی

            DayOfWeek dayName = this.RuleCalculateDate.DayOfWeek;
            int maxOverWork = -1;
            switch (dayName)
            {
                case DayOfWeek.Saturday:
                    maxOverWork = MyRule["1th", this.RuleCalculateDate].ToInt();
                    break;
                case DayOfWeek.Sunday:
                    maxOverWork = MyRule["2th", this.RuleCalculateDate].ToInt();
                    break;
                case DayOfWeek.Monday:
                    maxOverWork = MyRule["3th", this.RuleCalculateDate].ToInt();
                    break;
                case DayOfWeek.Tuesday:
                    maxOverWork = MyRule["4th", this.RuleCalculateDate].ToInt();
                    break;
                case DayOfWeek.Wednesday:
                    maxOverWork = MyRule["5th", this.RuleCalculateDate].ToInt();
                    break;
                case DayOfWeek.Thursday:
                    maxOverWork = MyRule["6th", this.RuleCalculateDate].ToInt();
                    break;
                case DayOfWeek.Friday:
                    maxOverWork = MyRule["7th", this.RuleCalculateDate].ToInt();
                    break;
            }
            int notAllowedValue = this.DoConcept(4002).Value - maxOverWork;
            if (maxOverWork > -1 && notAllowedValue > 0)
            {
                PairableScndCnpValue notAllowedCnp = new PairableScndCnpValue();
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4002)).Pairs)
                {
                    if (pair.Value - notAllowedValue > 0)
                    {
                        IPair notAllowedPair = new PairableScndCnpValuePair(pair.To - notAllowedValue, pair.To);

                        notAllowedCnp.AppendPair(notAllowedPair);

                        //pair.To -= notAllowedValue;
                        //this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else if (pair.Value - notAllowedValue == 0)
                    {
                        notAllowedCnp.AppendPair(pair);
                        //((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        //pair.From = pair.To = 0;
                        //this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;

                        break;
                    }
                    else
                    {
                        notAllowedCnp.AppendPair(pair);
                        //((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);

                        notAllowedValue -= pair.Value;

                        //pair.From = pair.To = 0;
                        //this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                    }
                }
                if (notAllowedCnp.PairCount > 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(notAllowedCnp);
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(((PairableScndCnpValue)this.DoConcept(4002)), notAllowedCnp));
                    this.ReCalculate(13);
                }
            }

            GetLog(MyRule, " After Execute State:", 4002, 4003, 13);
        }

        /// <summary>قانون اضافه کاري 30-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هشتاد و يک-181 درنظر گرفته شده است</remarks>
        public virtual void R6025(AssignedRule MyRule)
        {
            //اضافه کار ساعتي 4002
            //4003 اضافه کارساعتي غیرمجاز
            //13 کارکردناخالص
            //4023 زمان ناهار

            if (this.DoConcept(4002).Value > 0
                && this.DoConcept(1).Value >= MyRule["First", this.RuleCalculateDate].ToInt()
                && (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                && this.DoConcept(2005).Value == 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4003, 13, 4023);

                int tmp = Operation.Minimum(this.DoConcept(4002).Value, MyRule["Second", this.RuleCalculateDate].ToInt());

                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(tmp);
                ((PairableScndCnpValue)this.DoConcept(4003)).IncreaseValue(tmp);
                this.ReCalculate(13);
                this.DoConcept(4023).Value = tmp;

                GetLog(MyRule, " After Execute State:", 4002, 4003, 13, 4023);
            }
        }

        /// <summary>قانون اضافه کاري 32-1</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و هشتاد و سه-183 درنظر گرفته شده است</remarks>
        public virtual void R6026(AssignedRule MyRule)
        {
            //اضافه کار خاص ساعتي 4001            
            //4002 اضافه کار ساعتی
            //4003 اضافه کار ساعتی عیر مجاز

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
            {
                GetLog(MyRule, " Before Execute State:", 4002, 4003);

                ((PairableScndCnpValue)this.DoConcept(4003))
                                                .AddPairs(Operation.Differance(this.DoConcept(4002),
                                                            new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
                ((PairableScndCnpValue)this.DoConcept(4002))
                                                .AddPairs(Operation.Intersect(this.DoConcept(4002),
                                                            new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));

                GetLog(MyRule, " After Execute State:", 4002, 4003);
            }
        }

        /// <summary>
        /// اضافه کار قبل از ساعات ---- غیر مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6027(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4008, 13);

            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4008 اضافه کارساعتي قبل ازوقت
            if (((PairableScndCnpValue)this.DoConcept(4002)).PairCount > 0)
            {
                int fromLimit = MyRule["First", this.RuleCalculateDate].ToInt();
                IList<IPair> pairs = ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.From).Where(x => x.From < fromLimit).ToList();
                if (pairs != null && pairs.Count > 0)
                {
                    bool applyed = false;
                    foreach (IPair pair in pairs)
                    {
                        if (pair.To <= fromLimit)
                        {
                            PairableScndCnpValue diff = Operation.Differance(this.DoConcept(4002), pair);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(diff);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);
                            applyed = true;
                        }
                        else if (pair.To > fromLimit)
                        {
                            IPair p = new PairableScndCnpValuePair(pair.From, fromLimit);
                            PairableScndCnpValue diff = Operation.Differance(this.DoConcept(4002), p);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(diff);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(p);
                            applyed = true;
                        }
                    }
                    if (applyed)
                        this.ReCalculate(4008, 13);
                }
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 4008, 13);
        }

        /// <summary>
        /// اضافه کار بعد از ساعات ---- غیر مجاز است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6028(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4008, 13);

            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4008 اضافه کارساعتي قبل ازوقت
            if (((PairableScndCnpValue)this.DoConcept(4002)).PairCount > 0)
            {
                int toLimit = MyRule["First", this.RuleCalculateDate].ToInt();
                IList<IPair> pairs = ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.From).Where(x => x.To > toLimit).ToList();
                if (pairs != null && pairs.Count > 0)
                {
                    bool applyed = false;
                    foreach (IPair pair in pairs)
                    {
                        if (pair.From >= toLimit)
                        {
                            PairableScndCnpValue diff = Operation.Differance(this.DoConcept(4002), pair);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(diff);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);
                            applyed = true;
                        }
                        else if (pair.From < toLimit)
                        {
                            IPair p = new PairableScndCnpValuePair(toLimit, pair.To);
                            PairableScndCnpValue diff = Operation.Differance(this.DoConcept(4002), p);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(diff);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(p);
                            applyed = true;
                        }
                    }
                    if (applyed)
                        this.ReCalculate(4008, 13);
                }
            }
            GetLog(MyRule, " After Execute State:", 4002, 4003, 4008, 13);
        }

        /// <summary>
        /// روزانه از ساعت ------ تا ساعات ---------- اضافه کار بعنوان اضافه کار ویژه لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6029(AssignedRule MyRule)
        {
            //4002 اضافه کار مجاز
            //4005 اضافه کار مجاز ماهانه
            //4032 اضافه کار ویژه
            //4033 اضافه کار ویژه ماهانه
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4002, 4032 });


            int fromTime = MyRule["first", this.RuleCalculateDate].ToInt();
            int toTime = MyRule["second", this.RuleCalculateDate].ToInt();
            int diffrenceFromOverwork = MyRule["third", this.RuleCalculateDate].ToInt();
            PairableScndCnpValue overWork = (PairableScndCnpValue)this.DoConcept(4002);
            PairableScndCnpValuePair pair = new PairableScndCnpValuePair(fromTime, toTime);
            PairableScndCnpValue specOverwork = Operation.Intersect(overWork, pair);
            if (specOverwork != null && specOverwork.Value > 0)
            {
                GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());
                ((PairableScndCnpValue)this.DoConcept(4032)).AddPairs(specOverwork);
                if (diffrenceFromOverwork > 0)
                {
                    overWork.AddPairs(Operation.Differance(overWork, specOverwork));
                }
                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }

        }

        /// <summary>
        /// ماهانه بیش از ------ ساعت اضافه کار  تا سقف ---------- ساعات بعنوان اضافه کار ویژه لحاظ گردد 
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6030(AssignedRule MyRule)
        {
            //4002 اضافه کار مجاز
            //4005 اضافه کار مجاز ماهانه
            //4032 اضافه کار ویژه
            //4033 اضافه کار ویژه ماهانه
            int fromDuration = MyRule["first", this.RuleCalculateDate].ToInt();
            int maxDuration = MyRule["second", this.RuleCalculateDate].ToInt();
            int diffrenceFromOverwork = MyRule["third", this.RuleCalculateDate].ToInt();
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4005, 4033 });

            if (this.DoConcept(4005).Value > fromDuration)
            {
                GetLog(MyRule, " Before Execute State:", conceptList.Distinct().ToArray());

                this.DoConcept(4033).Value = this.DoConcept(4005).Value - fromDuration;
                this.DoConcept(4033).Value = this.DoConcept(4033).Value > maxDuration ? maxDuration : this.DoConcept(4033).Value;
                if (diffrenceFromOverwork > 0)
                {
                    this.DoConcept(4005).Value -= this.DoConcept(4033).Value;
                }

                GetLog(MyRule, " After Execute State:", conceptList.Distinct().ToArray());
            }
        }

        /// <summary>
        /// اضافه کار اجباری اعمال شود است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6031(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4002, 4003, 4008, 13);

            var obligatoryOvertimePairs =
                this.Person.GetShiftByDate(this.RuleCalculateDate, "1")
                .Pairs.Where(x =>
                    x.ShiftPairType != null &&
                    x.ShiftPairType.CustomCode.Equals("1")
                    );

            // شیفت اضافه کار اجباری وجود ندارد
            if (obligatoryOvertimePairs == null || !obligatoryOvertimePairs.Any()) return;

            // با این فرض که غیبت روزانه خورده است
            if (this.DoConcept(1).Value == 0) return;

            foreach (var obligatoryOvertimePair in obligatoryOvertimePairs.ToList())
            { 
                var absentOnMandatoryOvertime =
                    Operation.Differance(
                        obligatoryOvertimePair,
                        Operation.Intersect(obligatoryOvertimePair, this.DoConcept(1))
                        );

                PairableScndCnpValue difToAppend;

                if (((PairableScndCnpValue)this.DoConcept(3028)).Pairs.Any())
                {
                    difToAppend = Operation.Union(this.DoConcept(3028), absentOnMandatoryOvertime);
                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(difToAppend);                   
                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(absentOnMandatoryOvertime);
                }
                ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(this.DoConcept(3028));
                //PairableScndCnpValue absent = (PairableScndCnpValue)this.DoConcept(3028);
                //BaseShift shift = this.Person.GetShiftByDate(this.RuleCalculateDate);
                //if (absent.Pairs
                //            .Where(x => x.To >= shift.Last.To)
                //                    .FirstOrDefault() != null
                //          )
                //{
                //    ((PairableScndCnpValue)this.DoConcept(3001)).AddPair(absent.Pairs.OrderBy(x => x.To).ToList().Last());
                //    this.ReCalculate(3030);
                //}
            }
        }

        /// <summary>
        /// اعمال مجوز اضافه کار
        /// در اصل اضافه کار را به غیر مجاز تبدیل میکند
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6032(AssignedRule MyRule)
        {
            //4002 اضافه کارساعتي مجاز
            //4003 اضافه کارساعتي غیرمجاز       
            //4015 اضافه کار با مجوز باشد
            //4016 مفهوم اضافه کار بعد از وقت مجوزی است
            //4029 مفهوم مجوز اضافه کاری
            //4007 مفهوم اضافه کارآخر وقت            
            //4026 سقف اضافه کار که ضریب تعلق میگیرد

            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.OverTime));

            #region Apply Parameters
            int withoutPermitAlow = 0;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                withoutPermitAlow = MyRule["MojazTatil", this.RuleCalculateDate].ToInt();
            else
                withoutPermitAlow = MyRule["MojazAadi", this.RuleCalculateDate].ToInt();
            if (withoutPermitAlow > 0)
            {
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4003)).Pairs)
                {
                    if (pair.Value - withoutPermitAlow > 0)
                    {
                        IPair allowedPair = new PairableScndCnpValuePair(pair.From, pair.From + withoutPermitAlow);

                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(allowedPair);

                        pair.From += withoutPermitAlow;
                        this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                        break;
                    }
                    else if (pair.Value - withoutPermitAlow == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                        pair.From = pair.To = 0;
                        this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                        withoutPermitAlow -= pair.Value;

                        pair.From = pair.To = 0;
                        this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;
                    }
                }
            }
            #endregion

            if (permit != null && permit.Value > 0)
            {
                if (this.DoConcept(4003).Value > 0)
                {
                    foreach (PermitPair permitPair in permit.Pairs)
                    {
                        //مجوز مقداری و بازه ای - در حال حاضر نمیتوان روی خصیصه جفت بودن در مجوز حساب کرد
                        //لذا از روش زیر جهت شناسایی استفاده میگردد
                        #region Pairly Permit
                        if (permitPair.To - permitPair.From == permitPair.Value)
                        {
                            PairableScndCnpValue allowedOverWork = Operation.Intersect(permitPair, (PairableScndCnpValue)this.DoConcept(4003));
                            PairableScndCnpValue notAllowedOverWork = Operation.Differance(this.DoConcept(4003), allowedOverWork);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(allowedOverWork);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(notAllowedOverWork);

                            permitPair.IsApplyedOnTraffic = true;//اعمال شد
                        }
                        #endregion

                        #region Value Permit
                        else if (permitPair.From == 1439 && permitPair.To == 1439 && permitPair.Value > 0)
                        {
                            int permitOverWork = permitPair.Value;

                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4003)).Pairs)
                            {
                                if (pair.Value - permitOverWork > 0)
                                {
                                    IPair allowedPair = new PairableScndCnpValuePair(pair.From, pair.From + permitOverWork);

                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(allowedPair);

                                    pair.From += permitOverWork;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                                    break;
                                }
                                else if (pair.Value - permitOverWork == 0)
                                {
                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                                    break;
                                }
                                else
                                {
                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                                    permitOverWork -= pair.Value;

                                    pair.From = pair.To = 0;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;
                                }
                            }
                            permitPair.IsApplyedOnTraffic = true;//اعمال شد
                        }
                        #endregion
                    }
                    this.ReCalculate(13);
                }
            }

           
            GetLog(MyRule, " After Execute State:", 4007, 4002, 4003, 13);
        }




        #endregion


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //this.ConceptList.Clear();                    
        }

        #endregion
    }
}
