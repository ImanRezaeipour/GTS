using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Engine
{
    /// <summary>
    /// این کلاس وظیفه ی تنظیم عملیات محاسبه در هنگام کار با "نخ ها" را برعهده دارد
    /// </summary>
    public static class ThreadHelper
    {

        #region Variables
        static int a = 0;
        private static IDictionary<string, IDictionary<decimal, ExecutableThread>> executableThreadsWithCaller = new Dictionary<string, IDictionary<decimal, ExecutableThread>>();
        private static IDictionary<string, int> executableThreadsCountWithCaller = new Dictionary<string, int>();
        private static Mutex mut = new Mutex();

        #endregion

        /// <summary>
        /// در اینجا موارد قابل اجر به لیست منحصر به فرد فراخواننده اضافه می گردند و درصورتیکه این موارد قبلا وجود داشته باشند به عنوان مورد جدید پذیرفته نمی شود
        /// </summary>
        /// <param name="ExectablePrsCalcList"></param>
        /// <returns>مواردی که برای اجر پذیرفته شده اند</returns>
        public static IList<ExecutablePersonCalculation> AddThreads(String CallerIdentity, IList<ExecutablePersonCalculation> ExectablePrsCalcList, FinishedCallback Callback)
        {
            try
            {
                mut.WaitOne();
                IDictionary<decimal, ExecutableThread> ExecThreads = null;
                int ExecThreadsCount = 0;

                executableThreadsWithCaller.TryGetValue(CallerIdentity, out ExecThreads);
                executableThreadsCountWithCaller.TryGetValue(CallerIdentity, out ExecThreadsCount);

                if (ExecThreads == null)
                {
                    ExecThreads = new Dictionary<decimal, ExecutableThread>();
                    executableThreadsWithCaller.Add(CallerIdentity, ExecThreads);
                    executableThreadsCountWithCaller.Add(CallerIdentity, 0);
                }

                IList<ExecutablePersonCalculation> ItemAdded = new List<ExecutablePersonCalculation>();
                for (int i = 0; i <= ExectablePrsCalcList.Count - 1; i++)
                {
                    decimal PrsId = ExectablePrsCalcList[i].PersonId;

                    ///این پرسنل در لیستی با شناسه فراخواننده متفاوت قبلا درج شده است
                    if (ExsitThreadNotInCaller(CallerIdentity, PrsId))
                    {
                        continue;
                    }

                    if (!ExecThreads.ContainsKey(PrsId))
                    {
                        ExecutableThread executableThread = new ExecutableThread(CallerIdentity,
                                                                                 ExectablePrsCalcList[i].ID,
                                                                                 ExectablePrsCalcList[i].PersonId,
                                                                                 ExectablePrsCalcList[i].FromDate.Date,
                                                                                 ExectablePrsCalcList[i].ToDate.Date,
                                                                                 Callback);
                        ExecThreads.Add(PrsId, executableThread);
                        executableThreadsCountWithCaller[CallerIdentity]++;

                        ItemAdded.Add(ExectablePrsCalcList[i]);
                    }
                    //else if (!ExecThreads[PrsId].Executing)
                    //{
                    //    ItemAdded.Add(ExectablePrsCalcList[i]);
                    //}
                }
                return ItemAdded;
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// اگر نخی بیش از 10 ثانیه در حالت اجرا مانده باشد آن را از لیست اجرا حذف می نماید
        /// </summary>
        /// <param name="ExectablePrsCalcList"></param>
        public static void CleanupThreads()
        {
            try
            {
                mut.WaitOne();
                IList<string> MustDeleteCallerIdentities = new List<string>();
                for (int n = 0; n < executableThreadsWithCaller.Count; n++)
                {
                    IDictionary<decimal, ExecutableThread> ExecutableThreads =
                        executableThreadsWithCaller.ElementAt(n).Value;
                    for (int i = 0; i < ExecutableThreads.Count; i++)
                    {
                        ExecutableThread Thread = ExecutableThreads.ElementAt(i).Value;
                        if (Thread.Duration.Elapsed.TotalSeconds > 11160)
                        {
                            if (Thread.Param.ThreadContext != null)
                            {
                                Thread.Param.ThreadContext.Abort();
                            }
                            ExecutableThreads.Remove(Thread.Param.PersonId);
                            executableThreadsCountWithCaller[executableThreadsWithCaller.ElementAt(n).Key]--;
                        }
                    }
                    if (ExecutableThreads.Count == 0)
                    {
                        MustDeleteCallerIdentities.Add(executableThreadsWithCaller.ElementAt(n).Key);
                    }
                }
                foreach (string CallerIdentity in MustDeleteCallerIdentities)
                {
                    executableThreadsWithCaller.Remove(CallerIdentity);
                    executableThreadsCountWithCaller.Remove(CallerIdentity);
                }
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// مورد ارسال شده را برای اجرا تست می کند، اگر قبلا برای اجرا ارسال نشده باشد یا اجرا نشده باشد آن را برای اجرا برمی گرداند  
        /// </summary>
        /// <param name="Threads"></param>
        /// <returns>موردی که برای اجر پذیرفته شده است</returns>
        public static ExecutableThread.ThreadParam ForceToExecute(string CallerIdentity, ExecutablePersonCalculation MustExecuted, FinishedCallback Callback)
        {
            try
            {
                mut.WaitOne();

                //if (IsThreadExecute(MustExecuted.PersonId))
                //{
                //    ///نخی که محاسبات را در حالت اجرا قرار داده با فراخواننده ی این تابع متفاوت است
                //    if (executableThreadsWithCaller.ContainsKey(CallerIdentity) &&
                //        executableThreadsWithCaller[CallerIdentity].ContainsKey(MustExecuted.PersonId) &&
                //        !executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Executing)
                //    {
                //        executableThreadsWithCaller[CallerIdentity].Remove(MustExecuted.PersonId);
                //    }
                //    return null;
                //}
                //else
                //{
                if (!executableThreadsWithCaller.ContainsKey(CallerIdentity))
                {
                    executableThreadsWithCaller.Add(CallerIdentity, new Dictionary<decimal, ExecutableThread>());
                    executableThreadsCountWithCaller.Add(CallerIdentity, 0);
                }

                if (!executableThreadsWithCaller[CallerIdentity].ContainsKey(MustExecuted.PersonId))
                {
                    ExecutableThread executableThread = new ExecutableThread(CallerIdentity,
                                                                             MustExecuted.ID,
                                                                             MustExecuted.PersonId,
                                                                             MustExecuted.FromDate.Date,
                                                                             MustExecuted.ToDate.Date,
                                                                             Callback);
                    executableThreadsWithCaller[CallerIdentity].Add(MustExecuted.PersonId, executableThread);
                    executableThreadsCountWithCaller[CallerIdentity]++;
                }
                executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Executing = true;
                executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Duration.Start();
                executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Param.ToDate = MustExecuted.ToDate;
                return executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Param;
                //}
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// وظیفه تایید اجرای محاسبات برای "شخص" ورودی رابعهده دارد،         
        /// </summary>
        /// <param name="MustExecuted">نگهدارانده "شخصی" که قصد اجرای محاسباتش را داریم</param>
        /// <returns>در صورت که "نخی" به ازای "شخص" ورودی وجود نداشته باشد یا هم اکنون محاسبات برای این "شخص" در حال انجام باشد مقدار "خالی" برمی گرداند. در غیراینصورت "شخص" ورودی را برمیگرداند</returns>
        public static ExecutableThread.ThreadParam PrepareToExecute(string CallerIdentity, ExecutablePersonCalculation MustExecuted)
        {
            try
            {
                mut.WaitOne();
                ///نخی ما بازای این پرسنل وجود ندارد
                if (!executableThreadsWithCaller.ContainsKey(CallerIdentity) ||
                    !executableThreadsWithCaller[CallerIdentity].ContainsKey(MustExecuted.PersonId))
                {
                    return null;
                }
                ///نخی محاسبات این پرسنل را قبلا در حالت اجرا قرار داده است
                else if (IsThreadExecute(MustExecuted.PersonId))
                {
                    ///نخی که محاسبات را در حالت اجرا قرار داده با فراخواننده ی این تابع متفاوت است
                    if (!executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Executing)
                    {
                        executableThreadsWithCaller[CallerIdentity].Remove(MustExecuted.PersonId);
                        executableThreadsCountWithCaller[CallerIdentity]--;
                    }
                    return null;
                }
                else
                {
                    executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Executing = true;
                    executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Duration.Start();
                    executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Param.ToDate = MustExecuted.ToDate.Date;
                    return executableThreadsWithCaller[CallerIdentity][MustExecuted.PersonId].Param;
                }
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// آیا ایتمی با شناسه ورودی در لیست با شناسه فراخواننده وجود دارد
        /// </summary>
        public static bool ExsitThreadInCaller(string CallerIdentity, decimal personId)
        {
            try
            {
                mut.WaitOne();
                IDictionary<decimal, ExecutableThread> ExecutableThreads = executableThreadsWithCaller[CallerIdentity];
                return ExecutableThreads == null ? false : ExecutableThreads.ContainsKey(personId);
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// آیا ایتمی با شناسه ورودی در لیست هایی بغیر از شناسه فراخواننده ورودی وجود دارد
        /// </summary>
        /// <param name="CallerIdentity"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static bool ExsitThreadNotInCaller(string CallerIdentity, decimal personId)
        {
            try
            {
                mut.WaitOne();
                for (int i = 0; i < executableThreadsWithCaller.Count; i++)
                {
                    if (executableThreadsWithCaller.ElementAt(i).Key != CallerIdentity &&
                        executableThreadsWithCaller.ElementAt(i).Value.ContainsKey(personId))
                        return true;
                }
                return false;
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// یک آیتم را در صورت وجود حذف میکند
        /// </summary>
        public static void RemoveThread(string CallerIdentity, decimal personId)
        {
            try
            {
                mut.WaitOne();
                if (executableThreadsWithCaller.ContainsKey(CallerIdentity) &&
                    executableThreadsWithCaller[CallerIdentity].ContainsKey(personId))
                {
                    executableThreadsWithCaller[CallerIdentity].Remove(personId);
                    if (executableThreadsWithCaller[CallerIdentity].Count == 0)
                    {
                        executableThreadsCountWithCaller[CallerIdentity] = 0;
                    }
                }
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// اگر در هنگام اجرای نخ به هر دلیلی تصمیم گرفته شد اجرا متوقف شود
        /// با فراخوانی این متد نخ را از حالت اجرا خارج می نماییم
        /// </summary>
        /// <param name="MustExecuted"></param>
        public static void NotExecute(string CallerIdentity, decimal PersonId)
        {
            try
            {
                mut.WaitOne();
                if (executableThreadsWithCaller.ContainsKey(CallerIdentity) &&
                    executableThreadsWithCaller[CallerIdentity].ContainsKey(PersonId))
                {
                    executableThreadsWithCaller[CallerIdentity][PersonId].Param.ThreadContext = null;
                    executableThreadsWithCaller[CallerIdentity][PersonId].Duration.Stop();
                    executableThreadsWithCaller[CallerIdentity][PersonId].Executing = false;
                }
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        /// <summary>
        /// نخ در حال اجرا را لیست نخ ها مقداردهی مینماید تا در صورت نیاز
        /// بتوان اجرای نخ را لغو نمود.
        /// همچنین وضعیت نخ را در حالت اجرا قرار می دهد
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="thread"></param>
        public static void SetThreadContext(string CallerIdentity, decimal PersonId, Thread thread)
        {
            if (executableThreadsWithCaller.ContainsKey(CallerIdentity) &&
                    executableThreadsWithCaller[CallerIdentity].ContainsKey(PersonId))
            {
                executableThreadsWithCaller[CallerIdentity][PersonId].Param.ThreadContext = thread;
            }
        }

        /// <summary>
        /// تعداد کل نخ های مرتبط با شناسه ورودی را برمی گرداند
        /// </summary>
        /// <param name="CallerIdentity"></param>
        /// <returns></returns>
        public static int TotalThreadCountByCaller(string CallerIdentity)
        {
            return executableThreadsCountWithCaller.ContainsKey(CallerIdentity) ? executableThreadsCountWithCaller[CallerIdentity] : 0;
        }

        public static int TotalRemainThreadCountByCaller(string CallerIdentity)
        {
            int Result = 0;
            if (executableThreadsWithCaller.ContainsKey(CallerIdentity))
            {
                Result = executableThreadsWithCaller[CallerIdentity].Values.Count();
            }
            return Result;
        }

        public static int TotalThreadCount
        {
            get
            {
                int Result = 0;
                for (int i = 0; i < executableThreadsWithCaller.Count; i++)
                {
                    Result += executableThreadsWithCaller.ElementAt(i).Value.Count;
                }
                return Result;
            }
        }

        public static int TotalExecutingThreadCount
        {
            get
            {
                int Result = 0;
                for (int i = 0; i < executableThreadsWithCaller.Count; i++)
                {
                    foreach (ExecutableThread item in executableThreadsWithCaller.ElementAt(i).Value.Values)
                    {
                        if (item.Executing)
                            Result++;
                    }
                }
                return Result;
            }
        }

        private static bool IsThreadExecute(decimal PersonId)
        {
            for (int i = 0; i < executableThreadsWithCaller.Count; i++)
            {
                ExecutableThread ExecThread;
                executableThreadsWithCaller[executableThreadsWithCaller.Keys.ElementAt(i)].TryGetValue(PersonId, out ExecThread);
                if (ExecThread != null && ExecThread.Executing)
                    return true;
            }
            return false;
        }
    }
}
