using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Calculator;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using NHibernate;
using System.Threading;
using System.Diagnostics;
using GTS.Clock.Model.ELE;
using System.Threading.Tasks;

namespace GTS.Clock.Business.Engine
{
    public class Executer
    {
        private static int MaxCPUUsage = 80;
        private static int MaxThread = 10;
        private static string MaxCPUUsageAppSetting = "EngineMaxCPUUsage";
        private static string MaxThreadCount = "EngineMaxParallelCount";
        private static bool stopEngine = false;
        GTSEngineLogger logger = new GTSEngineLogger();


        public Executer()
        {
            GTS.Clock.Business.AppSettings.BApplicationSettings.CheckGTSLicense();

            if (!Utility.IsEmpty(Utility.ReadAppSetting(MaxCPUUsageAppSetting)))
            {
                MaxCPUUsage = Utility.ToInteger(Utility.ReadAppSetting(MaxCPUUsageAppSetting));
            }
            if (!Utility.IsEmpty(Utility.ReadAppSetting(MaxThreadCount)))
            {
                MaxThread = Utility.ToInteger(Utility.ReadAppSetting(MaxThreadCount));
            }
        }

        /// <summary>
        /// اجرای محاسبات برای یک نفر 
        /// </summary>
        /// <param name="barcode">بارکد</param>
        /// <param name="toDate">انتهای بازه ی محاسبات</param>
        public void Execute(string CallerIdentity, string barcode, DateTime toDate)
        {
            ExecutablePersonCalculation MustExecuted = ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).GetByBarcode(barcode, toDate);
            ThreadHelper.CleanupThreads();
            logger.Logger.Info(
                string.Format("Execute for person '{0}' start at '{1}', Total Thread Count: '{2}', Runing Thread Count: '{3}'", barcode
                                                                                                                                   , DateTime.Now.ToShortDateString()
                                                                                                                                   , ThreadHelper.TotalThreadCount
                                                                                                                                   , ThreadHelper.TotalExecutingThreadCount));
            logger.Flush();
            if (MustExecuted != null /*&& !MustExecuted.CalculationIsValid*/)
            {
                MustExecuted.ToDate = toDate;
                ExecutableThread.ThreadParam param = ThreadHelper.ForceToExecute(CallerIdentity, MustExecuted, this.FinishedCallback);
                if (param != null)
                {
                    this.Execute(param);
                }
            }
        }

        /// <summary>
        /// اجرای محاسبات برای یک نفر
        /// </summary>
        /// <param name="personId">شناسه شخص</param>
        /// <param name="todate">انتهای بازه ی محاسبات</param>
        public void Execute(string CallerIdentity, decimal personId, DateTime toDate)
        {
            try
            {
                ExecutablePersonCalculation MustExecuted = ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).GetByPrsId(personId, toDate);
                ThreadHelper.CleanupThreads();
                logger.Logger.Info(
                    string.Format("Execute for person '{0}' start at '{1}', Total Thread Count: '{2}', Runing Thread Count: '{3}'", personId.ToString()
                                                                                                                                       , DateTime.Now.ToShortDateString()
                                                                                                                                       , ThreadHelper.TotalThreadCount
                                                                                                                                       , ThreadHelper.TotalExecutingThreadCount));
                logger.Flush();
                if (MustExecuted != null /*&& !MustExecuted.CalculationIsValid*/)
                {
                    MustExecuted.ToDate = toDate;
                    ExecutableThread.ThreadParam param = ThreadHelper.ForceToExecute(CallerIdentity, MustExecuted, this.FinishedCallback);
                    if (param != null)
                    {
                        this.Execute(param);
                    }
                }
            }
            catch (BaseException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// این تابع وظیفه اجرای قوانین برای تماممی اشخاصی که محاسباتشان نامعتبر است را برعهده دارد
        /// </summary>
        /// <param name="toDate"></param>
        public void Execute(string CallerIdentity, DateTime toDate)
        {
            try
            {
                IList<ExecutablePersonCalculation> MustExecuted = ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).GetAll(toDate);
                ThreadHelper.CleanupThreads();
                IList<ExecutablePersonCalculation> Threads = ThreadHelper.AddThreads(CallerIdentity, MustExecuted, this.FinishedCallback);
                logger.Logger.Info(
                    string.Format("Execute all start at '{0}', Total Thread Count: '{1}', Executable Thread Count: '{2}', Runing Thread Count: '{3}'", DateTime.Now.ToShortDateString()
                                                                                                                                               , ThreadHelper.TotalThreadCount
                                                                                                                                               , ThreadHelper.TotalThreadCount - ThreadHelper.TotalExecutingThreadCount
                                                                                                                                               , ThreadHelper.TotalExecutingThreadCount));
                logger.Flush();

                IList<Action> actions = new List<Action>();

                foreach (ExecutablePersonCalculation item in Threads /*.Where(x => x.CalculationIsValid == false)*/)
                {
                    item.ToDate = toDate;
                    ExecutableThread.ThreadParam param = ThreadHelper.PrepareToExecute(CallerIdentity, item);
                    if (param != null)
                    {
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(this.Execute), param);
                        actions.Add(() => this.Execute(param));
                    }
                }
                Task.Factory.StartNew(() => Parallel.Invoke(
                                               new ParallelOptions() { MaxDegreeOfParallelism = MaxThread },
                                               actions.ToArray()));
            }
            catch (Exception ex)
            {
                logger.Error("Execute(string CallerIdentity, DateTime toDate)->", "" + ex.Message, ex);
                logger.Flush();
                throw ex;
            }
        }

        /// <summary>
        /// این تابع وظیفه اجرای قوانین برای تماممی اشخاصی که محاسباتشان نامعتبر است را برعهده دارد
        /// همجنین تنها توسط سرویس ویندوزی اجرا میگردد
        /// </summary>
        /// <param name="toDate"></param>
        public void ExecuteByRobot(string CallerIdentity, DateTime toDate)
        {
            try
            {
                IList<ExecutablePersonCalculation> MustExecuted = ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).GetAll(toDate);
                ThreadHelper.CleanupThreads();
                IList<ExecutablePersonCalculation> Threads = ThreadHelper.AddThreads(CallerIdentity, MustExecuted, this.FinishedCallback);
                logger.Logger.Info(
                    string.Format("ExecuteByRobot start at '{0}', Total Thread Count: '{1}', Executable Thread Count: '{2}', Runing Thread Count: '{3}'", DateTime.Now.ToShortDateString()
                                    , ThreadHelper.TotalThreadCount
                                    , ThreadHelper.TotalThreadCount - ThreadHelper.TotalExecutingThreadCount
                                    , ThreadHelper.TotalExecutingThreadCount));
                logger.Flush();


                IList<Action> actions = new List<Action>();
                int threadGroupSize = Threads.Count / MaxThread;
                if (Threads.Count % MaxThread > 0)
                    MaxThread++;
                var groupThreadParams = new ExecutableThread.GroupThreadParam[MaxThread];
                for (int i = 0; i < MaxThread; i++)
                {
                    //if (Utility.CpuUsage < MaxCPUUsage)
                    //{
                    groupThreadParams[i] = new ExecutableThread.GroupThreadParam();
                    Threads.Skip(threadGroupSize * i).Take(threadGroupSize).ToList().ForEach((item) =>
                    {
                        item.ToDate = toDate;
                        var param = ThreadHelper.PrepareToExecute(CallerIdentity, item);
                        if (param != null)
                        {
                            param.ExecuteByRobot = true;
                            groupThreadParams[i].ThreadParams.Add(param);
                        }
                    });
                    //}
                    //else
                    //{
                    //    cpuUsageExtraCount++;
                    //    ThreadHelper.RemoveThread(CallerIdentity, item.PersonId);
                    //}
                }

                groupThreadParams.ToList().ForEach((threadParams) => {
                                                                      Task.Factory.StartNew(() => { ExecuteGroup(threadParams); });
                                                                     });
                //Task.Factory.StartNew(() => Parallel.Invoke(
                //                           new ParallelOptions() { MaxDegreeOfParallelism = MaxThread },
                //                           actions.ToArray()));

                //if (cpuUsageExtraCount > 0)
                //{
                //    logger.Info("ExecuteByRobot", String.Format("عدم اجرا تعداد {0} نفر بدلیل محدودیت پردازنده", cpuUsageExtraCount.ToString()));
                //    logger.Flush();
                //}
            }
            catch (Exception ex)
            {
                logger.Error("ExecuteByRobot", "Eception on ExecuteByRobot : " + Utility.GetExecptionMessage(ex), ex);
                logger.Flush();
            }
        }

        /// <summary>
        /// این تابع وظیفه اجرای قوانین برای تماممی اشخاص شده در پارامتر تا تاریخ ورودی را برعهده دارد
        /// </summary>
        /// <param name="Persons"></param>
        /// <param name="Date"></param>
        public void Execute(string CallerIdentity, IList<decimal> Persons, DateTime toDate)
        {
            try
            {
                IList<ExecutablePersonCalculation> MustExecuted = ExecutablePersonCalculation.GetExecutablePersonCalcRepositoy(false).GetAllByPrsIds(Persons, toDate);
                ThreadHelper.CleanupThreads();
                IList<ExecutablePersonCalculation> Threads = ThreadHelper.AddThreads(CallerIdentity, MustExecuted, this.FinishedCallback);
                logger.Logger.Info(
                                string.Format("Execute all start at '{0}', Total Thread Count: '{1}', Executable Thread Count: '{2}', Runing Thread Count: '{3}'", DateTime.Now.ToShortDateString()
                                                                                                                                                           , ThreadHelper.TotalThreadCount
                                                                                                                                                           , ThreadHelper.TotalThreadCount - ThreadHelper.TotalExecutingThreadCount
                                                                                                                                                           , ThreadHelper.TotalExecutingThreadCount));
                logger.Flush();

                IList<Action> actions = new List<Action>();

                foreach (ExecutablePersonCalculation item in Threads/*.Where(x => x.CalculationIsValid == false)*/)
                {
                    item.ToDate = toDate;
                    ExecutableThread.ThreadParam param = ThreadHelper.PrepareToExecute(CallerIdentity, item);
                    if (param != null)
                    {
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(this.Execute), param);
                        actions.Add(() => this.Execute(param));
                    }
                }
                Task.Factory.StartNew(() => Parallel.Invoke(
                                              new ParallelOptions() { MaxDegreeOfParallelism = MaxThread },
                                              actions.ToArray()));
            }
            catch (Exception ex)
            {
                logger.Error("Execute(string CallerIdentity, IList<decimal> Persons, DateTime toDate)->", "" + ex.Message, ex);
                logger.Flush();
                throw ex;
            }
        }

        /// <summary>
        /// این تابع توسط نخ فراخوانی میشود و نگاشت ترددها و اجرای قوانین را انجام میدهد
        /// </summary>
        /// <param name="ExeutableThread"></param>
        private void Execute(object state)
        {
            if (stopEngine)
                return;
            ExecutableThread.ThreadParam param = (ExecutableThread.ThreadParam)state;
            if (param == null)
                return;
            ThreadHelper.SetThreadContext(param.CallerIdentity, param.PersonId, Thread.CurrentThread);

            try
            {
                IEngineEnvironment engEnvironment = new EngineEnvironment();

                ExecuteTrafficMapper exTrafficMapper = new ExecuteTrafficMapper();
                exTrafficMapper.Execute(param.PersonId, param.FromDate, param.ToDate);
                //NHibernateSessionManager.Instance.ClearSession();
                ExecuteRule exRule = new ExecuteRule(engEnvironment);
                exRule.Execute(param.ExecutablePrsCalcId, param.PersonId, param.FromDate, param.ToDate);

                //تست باید گردد
                NHibernateSessionManager.Instance.ClearSession();
            }
            catch (Exception ex)
            {
                ThreadHelper.NotExecute(param.CallerIdentity, param.PersonId);
                logger.Logger.Error(String.Format("خطا در اجرای محاسبات PrsonId:{0}, Message:{1}", param.PersonId, ex.StackTrace), ex);
                logger.Flush();
                if (!param.ExecuteByRobot)
                {
                    throw ex;
                }
            }
            finally
            {
                if (param.FinishedCallback != null)
                {
                    this.FinishedCallback(param.CallerIdentity, param.PersonId);
                }
                GC.Collect();
            }
        }

        private void ExecuteGroup(object state)
        {
            if (stopEngine)
                return;
            ExecutableThread.GroupThreadParam param = state as ExecutableThread.GroupThreadParam;
            if (param == null)
                return;
            foreach (ExecutableThread.ThreadParam threadParam in param.ThreadParams)
            {
                this.Execute(threadParam);
            }
        }

        public void LockCalculation()
        {
            stopEngine = true;
        }

        public void UnLockCalculation()
        {
            stopEngine = false;
        }

        /// <summary>
        /// وظیفه ی خارج نمودن نخ به اتمام رسیده از لیست نخ ها را برعهده دارد
        /// </summary>
        /// <param name="PersonId"></param>
        public void FinishedCallback(String CallerIdentity, decimal PersonId)
        {
            try
            {
                ThreadHelper.RemoveThread(CallerIdentity, PersonId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
