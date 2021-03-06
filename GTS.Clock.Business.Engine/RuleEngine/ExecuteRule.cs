using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Diagnostics;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Business.Calculator;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Threading;
using GTS.Clock.Model.ELE;
using GTS.Clock.Business.AppSettings;
using NHibernate;
using System.Reflection;
using System.Globalization;
using GTS.Clock.Business.DesignedCalculator;

namespace GTS.Clock.Business.Engine
{
    public class ExecuteRule
    {
        #region Variable

        //private GTSLogger wathchLooger = new GTSLogger();
        private GTSEngineLogger gtsRuleLogger = new GTSEngineLogger();
        private IEngineEnvironment engineEnvironment;

        private static Mutex mut = new Mutex();
        private static ApplicationSettings appSet;

        private DateTime DateOfBeginYear;
        private DateTime DateOfEndYear;

        const string SettingsLoaded = "EngineSettingsLoaded";

        #region AppSettings
        private bool ruleDebug = false;
        private bool ruleDurationDebug = false;
        #endregion

        #endregion

        #region Constructor

        public ExecuteRule(IEngineEnvironment engineEnvironment)
        {
            this.engineEnvironment = engineEnvironment;
            this.ruleDebug = GTSAppSettings.RuleDebug;
            this.ruleDurationDebug = GTSAppSettings.RuleDurationDebug;
        }

        #endregion

        #region Properties
        public static ApplicationSettings GTSAppSettings
        {
            get
            {
                if (appSet == null || System.Runtime.Remoting.Messaging.CallContext.GetData(SettingsLoaded) == null)
                {
                    appSet = AppSettings.BApplicationSettings.CurrentApplicationSettings;
                    System.Runtime.Remoting.Messaging.CallContext.SetData(SettingsLoaded, true);
                }
                return appSet;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// يک نمونه از کلاس "محاسبه گر قانون" برمي گرداند. اين نمونه براي تمامي
        /// فراخواني هاي تابع يکسان است و تنها خصوصيات آن با مقادير ورودي
        /// مقداردهي مي گردند
        /// همچنین اگر تاریخ روزی که در حال محاسبه آن هستیم با محدوده تاریخی که قانون در آن تعریف شده است انطباق نداشته
        /// باشد با برگرداندن "تهی" قانون نباید اجرا شود
        /// </summary>
        private void GetRuleCalculator(IEngineEnvironment engineEnvironment,
                                                 ref RuleCalculator RuleCalc,
                                                 Person person,
                                                 DateTime calculationDate,
                                                 AssignedRule categorisedRule,
                                                 DateRange calcDateZone)
        {
            ///اگر تاریخی که در حال محاسبه ی آن هستیم از  محدوده ی تعریف شده ی قانون برای شخص خارج شده قانون را اجرا نمیکنیم
            if (calculationDate >= categorisedRule.FromDate
                && calculationDate <= categorisedRule.ToDate)
            {
                if (RuleCalc != null)
                {
                    //RuleCalc.Person = person;
                    RuleCalc.AssignedRule = categorisedRule;
                    if (!RuleCalc.RuleCalculateDate.Equals(calculationDate))
                    {
                        RuleCalc.RuleCalculateDate = calculationDate;
                        RuleCalc.ConceptCalculateDate = calculationDate;
                    }
                }
                else
                {
                    engineEnvironment.Person = person;
                    engineEnvironment.AssignedRule = categorisedRule;
                    engineEnvironment.ConceptCalculateDate = calculationDate;
                    engineEnvironment.RuleCalculateDate = calculationDate;
                    engineEnvironment.CalcDateZone = calcDateZone;
                    RuleCalc = new RuleCalculator(engineEnvironment);
                }
            }
            else 
            {
                RuleCalc = null;
            }
        }

        /// <summary>
        /// جدوال(های) پی متناظر با محدوده محاسبات را پر می نماید
        /// </summary>
        /// <param name="PersonBarcode"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        private void UpadtePTable(string PersonBarcode, DateTime FromDate, DateTime ToDate)
        {
            return;
            IPersonRepository PrsRep = Person.GetPersonRepository(false);
            PersianDateTime pFromdate = new PersianDateTime(FromDate);
            PersianDateTime pTodate = new PersianDateTime(ToDate);
            do
            {
                PrsRep.UpdatePTable(PersonBarcode, pFromdate);
                //مقدار تاريخ را برابر روز اول ماه بعد قرار ميدهيم
                pFromdate = pFromdate.NextMonthStart();
            } while (pFromdate.Year <= pTodate.Year &&
                      pFromdate.Month <= pTodate.Month &&
                      pFromdate.Day <= pTodate.Day);
        }

        /// <summary>
        /// تاریخ آخرین زمان محاسبات پرسنلی که محاسبه برای وی انجام شده است را بروزرسانی می نماید 
        /// </summary>
        /// <param name="InvalidCalcResult"></param>
        private void UpdateExecutablePersonCalculation(decimal ExecutablePrsCalcId, decimal PersonId, DateTime ToDate)
        {
            ExecutablePersonCalculation ExecPrsCalc = ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).GetById(ExecutablePrsCalcId, false);
            if (ExecPrsCalc != null)
            {
                ExecPrsCalc.FromDate = ToDate;
                ExecPrsCalc.CalculationIsValid = true;
                ExecPrsCalc.MidNightCalculate = true;
                ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).WithoutTransactUpdate(ExecPrsCalc);
            }
            else
            {
                gtsRuleLogger.Error(PersonId.ToString(), "This is a test log", new Exception());
                gtsRuleLogger.Flush();
            }
        }

        /// <summary>
        /// وظیفه این تابع حصول اطمینان از انتساب حداکثر یک "محدوده محاسبات" به شخص در محدوده تاریخ درخواست شده برای محاسبه است
        /// در تغییرات بعدی این تابع دیگر اجرا نمی شود
        /// </summary>
        /// <param name="person"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        private void RangeGroupAssignmentValidate(Person person, DateTime FromDate, DateTime ToDate)
        {
            PersonRangeAssignment From = person.PersonRangeAssignList
                                                .Where(x => x.FromDate <= FromDate)
                                                .OrderByDescending(x => x.FromDate)
                                                .FirstOrDefault();

            PersonRangeAssignment To = person.PersonRangeAssignList
                                                .Where(x => x.FromDate <= ToDate)
                                                .OrderByDescending(x => x.FromDate)
                                                .FirstOrDefault();
            if (From.ID != To.ID)
            {
                throw new BaseException("به شخص در محدوده ی درخواست شده برای محاسبات دو یا چند 'محدوده محاسبات مفاهیم ماهانه' انتساب داده شده است", "ExecuteRule.RangeGroupAssignmentValidate");
            }

        }

        /// <summary>
        /// این تابع به عنوان تابع مجری برای نخ ها استفاده می شود.
        /// برای ارسال پارمتر به این تابع حتما باید از کلاس "کمک کننده" استفاده شود
        /// </summary>
        /// <param name="state">حاوی "پرسنلی" است که محاسبات برای آن باید انجام شود</param>
        public void Execute(decimal ExecutablePrsCalcId, decimal PersonId, DateTime FromDate, DateTime ToDate)
        {
            RuleCalculator RuleCalc = null;
            Stopwatch MainStopWatch = new Stopwatch();
            Stopwatch RuleStopWatch = new Stopwatch();
            MainStopWatch.Start();

            Person prs = Person.GetPersonRepository(false).GetById(PersonId, false);
            IRuleRepository ruleRep = Rule.GetRuleRepository(false);

            if (prs.Active)
            {
                try
                {
                    DateRange CalcDateZone = null;
                    //در اينجا "تراکنش" نيازي نيست تنها به دليل اينکه در بازيابي داده ها
                    //اتصال" مدام باز و بسته نشود از "تراکنش" استفاده شده است"
                    using (NHibernateSessionManager.Instance.BeginTransactionOn(FlushMode.Never))
                    {
                        this.DateOfBeginYear = Utility.GetDateOfBeginYear(FromDate, BLanguage.CurrentSystemLanguage);
                        this.DateOfEndYear = Utility.GetDateOfEndYear(FromDate, BLanguage.CurrentSystemLanguage);
                        CalcDateZone = prs.InitializeForExecuteRule(FromDate, ToDate, this.DateOfBeginYear, this.DateOfEndYear);
                        IList<AssignedRuleParameter> ruleParameterList = ruleRep.GetAssginedRuleParamList(FromDate, ToDate);

                        var ruleUserDefinedList = ruleRep.GetRuleUserDefined();

                        foreach (DateTime dt in CalcDateZone)
                        {
                            #region Execute person's rules
                            foreach (AssignedRule AsgRule in prs.AssignedRuleList)
                            {
                                AsgRule.RuleParameterList = ruleParameterList;

                                this.GetRuleCalculator(this.engineEnvironment, ref RuleCalc, prs, dt, AsgRule, CalcDateZone);
                                if (RuleCalc != null)
                                {
                                    try
                                    {
                                        if (ruleDurationDebug)
                                        {
                                            RuleStopWatch.Reset();
                                            RuleStopWatch.Start();
                                        }

                                        if (ruleUserDefinedList.Any(x => x.IdentifierCode.Equals(AsgRule.IdentifierCode)))
                                        {
                                            try
                                            {
                                                string ruleName = "R" + AsgRule.IdentifierCode.ToString(CultureInfo.InvariantCulture);
                                                //DesignedCalculator.DesignedRuleCalculator a = new DesignedCalculator.DesignedRuleCalculator();
                                                Assembly asm = typeof(DesignedRuleCalculator).Assembly;
                                                Type type = typeof(RuleCalculator);
                                                MethodInfo info = type.GetExtensionMethod(asm, ruleName);
                                                if (info != null)
                                                {
                                                    info.Invoke(RuleCalc, new object[] { RuleCalc, AsgRule });
                                                }
                                                else
                                                {
                                                    throw new BaseException(String.Format("قانون شماره {0} در فايل اسمبلي کاربر يافت نشد", AsgRule.IdentifierCode), "ExecuteRule.UserDefiendRule");
                                                }
                                            }
                                            catch (ExecuteRuleException ex)
                                            {
                                                gtsRuleLogger.Error(prs.PersonCode, "UserDefiend ExecuteRule: " + ex.StackTrace, ex);
                                                gtsRuleLogger.Flush();
                                                //توسط صلواتي در خطا بايد محاسبات خاتمه پيدا کند
                                                throw;
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                RuleCalc.ExecuteRule();
                                            }
                                            catch (ExecuteRuleException ex)
                                            {
                                                gtsRuleLogger.Error(prs.PersonCode, "ExecuteRule: " + ex.StackTrace, ex);
                                                gtsRuleLogger.Flush();
                                                //توسط صلواتي در خطا بايد محاسبات خاتمه پيدا کند
                                                throw;
                                            }
                                        }

                                        if (ruleDurationDebug)
                                        {
                                            gtsRuleLogger.Info(prs.BarCode, String.Format("Rule:R{0}, Duration:{1}, Date:{2}", RuleCalc.AssignedRule.IdentifierCode, RuleStopWatch.Elapsed.ToString(), dt));
                                        }
                                    }
                                    catch (BaseException ex)
                                    {
                                        if (ex.InnerException != null && ex.InnerException is BaseException && ((BaseException)ex.InnerException).InsertedLog)
                                        {
                                            ex.InsertedLog = true;
                                        }
                                        BaseException.GetLog(gtsRuleLogger, prs.PersonCode, ex);
                                        //gtsRuleLogger.Error(prs.PersonCode, "ExecuteRule: " + ex.StackTrace, ex);
                                        //gtsRuleLogger.Flush();
                                        throw;
                                    }
                                    finally
                                    {
                                        RuleCalc.Dispose();
                                    }
                                }

                            }
                            #endregion
                        }
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                    }

                    using (NHibernateSessionManager.Instance.BeginTransactionOn(FlushMode.Auto))
                    {
                        try
                        {
                            mut.WaitOne();
                            IPersonRepository PrsRepository = Person.GetPersonRepository(false);
                            PrsRepository.DeleteScndCnpValue(prs.ID, CalcDateZone.FromDate, CalcDateZone.ToDate);

                            prs.ScndCnpValueList.DoInsert(NHibernateSessionManager.Instance.GetSession().Connection, NHibernateSessionManager.Instance.GetTransaction().GetDbTransaction);
                            PrsRepository.RunSQL(prs.ScndCnpValueList.UpdateQuery);
                            UpdateExecutablePersonCalculation(ExecutablePrsCalcId, PersonId, ToDate);
                            //throw new Exception("اين خطا براي تست عقب گرد تراکنش ايجاد شده است");
                            NHibernateSessionManager.Instance.CommitTransactionOn();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                BaseException.GetLog(gtsRuleLogger, prs.BarCode, ex, "Rollback -- ExecuteRule(Insert Results): " + ex.StackTrace);
                                //gtsRuleLogger.Error(prs.PersonCode, "Rollback -- ExecuteRule(Insert Results): " + ex.StackTrace, ex);
                                //gtsRuleLogger.Flush();

                                NHibernateSessionManager.Instance.RollbackTransactionOn();
                            }
                            catch (Exception except)
                            {
                                BaseException.GetLog(gtsRuleLogger, prs.BarCode, except, ex.StackTrace);
                                //gtsRuleLogger.Error(prs.BarCode, except.StackTrace, except);
                                //gtsRuleLogger.Flush();
                            }
                            throw;
                        }
                        finally
                        {
                            mut.ReleaseMutex();
                        }

                    }
                }
                catch (Exception ex)
                {
                    BaseException.GetLog(gtsRuleLogger, prs.BarCode, ex);
                    //gtsRuleLogger.Error(prs.BarCode, ex.StackTrace, ex);
                    //gtsRuleLogger.Flush();
                    throw;
                }
            }

            MainStopWatch.Stop();
            gtsRuleLogger.Info(prs.BarCode, String.Format("Person {0} Duration : {1}", prs.BarCode, MainStopWatch.Elapsed.ToString()));
            gtsRuleLogger.Flush();
        }

        /// <summary>
        /// این تابع به عنوان تابع مجری برای نخ ها استفاده می شود.
        /// برای ارسال پارمتر به این تابع حتما باید از کلاس "کمک کننده" استفاده شود
        /// </summary>
        /// <param name="state">حاوی "پرسنلی" است که محاسبات برای آن باید انجام شود</param>
        //public void Execute(ExecutablePersonCalculation mustExecute, Person prs2)
        //{
        //    if (mustExecute == null) return;

        //    RuleCalculator RuleCalc = null;
        //    Stopwatch MainStopWatch = new Stopwatch();
        //    Stopwatch RuleStopWatch = new Stopwatch();
        //    MainStopWatch.Start();

        //    Person prs = Person.GetPersonRepository(false).GetById(mustExecute.PersonId, false);
        //    try
        //    {
        //        DateRange CalcDateZone = null;
        //        //در اینجا "تراکنش" نیازی نیست تنها به دلیل اینکه در بازیابی داده ها
        //        //اتصال" مدام باز و بسته نشود از "تراکنش" استفاده شده است"
        //        using (NHibernateSessionManager.Instance.BeginTransactionOn(FlushMode.Never))
        //        {
        //            this.DateOfBeginYear = Utility.GetDateOfBeginYear(mustExecute.FromDate, BLanguage.CurrentSystemLanguage);
        //            this.DateOfEndYear = Utility.GetDateOfEndYear(mustExecute.FromDate, BLanguage.CurrentSystemLanguage);
        //            CalcDateZone = prs.InitializeForExecuteRule(mustExecute.FromDate, mustExecute.ToDate, this.DateOfBeginYear, this.DateOfEndYear);

        //            foreach (DateTime dt in CalcDateZone)
        //            {
        //                #region Execute person's rules
        //                foreach (AssignedRule AsgRule in prs.AssignedRuleList)
        //                {
        //                    this.GetRuleCalculator(this.engineEnvironment, ref RuleCalc, prs, dt, AsgRule, CalcDateZone);
        //                    if (RuleCalc != null)
        //                    {
        //                        try
        //                        {
        //                            RuleStopWatch.Reset();
        //                            RuleStopWatch.Start();

        //                            RuleCalc.ExecuteRule();

        //                            if (ruleDurationDebug)
        //                            {
        //                                gtsRuleLogger.Info(prs.BarCode, String.Format("Rule:M{0}, Duration:{1}, Date:{2}", RuleCalc.AssignedRule.IdentifierCode, RuleStopWatch.Elapsed.ToString(), dt));
        //                            }
        //                        }
        //                        catch (ExecuteRuleException ex)
        //                        {
        //                            string msg = String.Format("خطا در محاسبات آقا/خانم {0} با شناسه {1} در تاريخ {2}، پیغام: {3}", prs.Name, prs.BarCode, PersianDateTime.ToPersianDateTime(dt).ToString(), ex.Message);

        //                            gtsRuleLogger.Error(prs.PersonCode, msg, ex);
        //                            gtsRuleLogger.Flush();
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }
        //            NHibernateSessionManager.Instance.CommitTransactionOn();
        //        }

        //        using (NHibernateSessionManager.Instance.BeginTransactionOn(FlushMode.Auto))
        //        {
        //            try
        //            {
        //                mut.WaitOne();
        //                IPersonRepository PrsRepository = Person.GetPersonRepository(false);
        //                PrsRepository.DeleteScndCnpValue(prs.ID, CalcDateZone.FromDate, CalcDateZone.ToDate);

        //                prs.ScndCnpValueList.DoInsert(NHibernateSessionManager.Instance.GetSession().Connection, NHibernateSessionManager.Instance.GetTransaction().GetDbTransaction);
        //                PrsRepository.RunSQL(prs.ScndCnpValueList.UpdateQuery);
        //                UpadtePTable(prs.BarCode, CalcDateZone.FromDate, CalcDateZone.ToDate);
        //                UpdateExecutablePersonCalculation(mustExecute);
        //                NHibernateSessionManager.Instance.CommitTransactionOn();
        //            }
        //            catch
        //            {
        //                NHibernateSessionManager.Instance.RollbackTransactionOn();
        //                throw;
        //            }
        //            finally
        //            {
        //                mut.ReleaseMutex();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gtsRuleLogger.Error(prs.BarCode, ex.Message, ex);
        //        gtsRuleLogger.Flush();
        //        //NHibernateSessionManager.Instance.RollbackTransactionOn();
        //    }

        //    MainStopWatch.Stop();
        //    gtsRuleLogger.Info(prs.BarCode, String.Format("Person {0} Duration : {1}", prs.BarCode, MainStopWatch.Elapsed.ToString()));
        //    gtsRuleLogger.Flush();
        //}

        #endregion
    }
}