using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.TrafficMapping;
using NHibernate;
using System.Threading;


namespace GTS.Clock.Business.Engine
{   
    public class ExecuteTrafficMapper
    {
        #region Variable

        //private GTSEngineLogger gtsRuleLogger = new GTSEngineLogger();
       
     
        #endregion

        #region Comment

        /*
        private Person GetPerson(decimal personId)
        {
            IPersonRepository PrsRep = Person.GetPersonRepository(false);
            return PrsRep.GetById(personId, false);
        }
        private Person GetPerson(string barcode)
        {
            IPersonRepository PrsRep = Person.GetPersonRepository(false);
            return PrsRep.GetByBarcode(barcode);
        }

        /// <summary>
        /// .این تابع مسئول اجرای قوانین تمامی پرسنل در تاریخ درخواست شده می باشد
        /// it's not Thread-Safe
        /// </summary>
        /// <param name="CalculationDate">تاریخ اعمال قوانین</param> 
        /// <returns>پرسنل مشخص شده را به منظور بارگذاری مجدد در دسترسی های بعدی برمی گرداند</returns>
        private Person Execute(decimal personId)
        {
            Person person = null;
            person = GetPerson((int)personId);

            ExecutableThread.ThreadExecuteParam param = new ExecutableThread.ThreadExecuteParam();
            param.person = person;

            Execute(param);

            return person ?? null;
        }

        /// <summary>
        /// قوانین تردد را برای پرسنل با بارکد مشخص شده اجرا می نماید
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns>پرسنل مشخص شده را به منظور بارگذاری مجدد در دسترسی های بعدی برمی گرداند</returns>
        private Person Execute(string barcode)
        {
            Person person = null;
            person = GetPerson(barcode);

            ExecutableThread.ThreadExecuteParam param = new ExecutableThread.ThreadExecuteParam();           
            param.person = person;
               

            Execute(param);
            return person ?? null;
        }

        private void Execute()
        {
            IList<InvalidCalculationResult> MustExecuted = InvalidCalculationResult.GetInvalidCalcResultRepositoy(false).GetAll();
            ThreadHelper.AddThreads(MustExecuted,ThreadType.TrafficMapperThread);
            PersonRepository prsnRep = new PersonRepository(false);
           
            foreach (InvalidCalculationResult invalidlCalculationResult in MustExecuted)
            {
                ExecutableThread.ThreadExecuteParam param = new ExecutableThread.ThreadExecuteParam();
                param.FinishedCallback = new ExecutableThread.FinishedMyJob(ThreadHelper.FinishedJob);
                param.person = ThreadHelper.NowExecute(invalidlCalculationResult.Person, ThreadType.TrafficMapperThread) != null ? invalidlCalculationResult.Person : null;

                ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), param);            
            }
        }
     */
        
        #endregion

        public void Execute1(ExecutablePersonCalculation mustExecute)
        {
            if (mustExecute == null) 
                return;
            //Person prs = Person.GetPersonRepository(false).GetById(mustExecute.PersonId, false);

            //if (prs != null)
            {
                //prs.InitializeForTrafficMapper(mustExecute.FromDate, mustExecute.ToDate);
                //using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    try
                    {
                        //TrafficMapper tm = new TrafficMapper(prs);
                        //tm.DoMap();
                        //NHibernateSessionManager.Instance.CommitTransactionOn();
                    }
                    catch (TrafficMapperRuleException ex)
                    {
                        //gtsRuleLogger.Error(prs.BarCode, Infrastructure.Utility.Utility.GetExecptionMessage(ex));
                        //gtsRuleLogger.Flush();
                        //NHibernateSessionManager.Instance.RollbackTransactionOn();
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        //gtsRuleLogger.Error(prs.BarCode, String.Format("خطا در هنگام نگاشت تردد های پرسنل با بارکد: {0} متن خطا: {1}", prs.BarCode, Infrastructure.Utility.Utility.GetExecptionMessage(ex)));
                        //gtsRuleLogger.Flush();
                        //NHibernateSessionManager.Instance.RollbackTransactionOn();
                        throw ex;
                    }
                }
            }            
        }

        public void Execute(decimal PersonId, DateTime FromDate, DateTime ToDate)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    Person prs = Person.GetPersonRepository(false).GetById(PersonId, false);
                    prs.InitializeForTrafficMapper(FromDate, ToDate);
                    TrafficMapper tm = new TrafficMapper(prs, FromDate, ToDate);
                    tm.DoMap(); 
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    tm.Dispose();

                }
                catch (Exception ex)
                {
                    //gtsRuleLogger.Flush();
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw;
                }
            }
        }
    }
}

