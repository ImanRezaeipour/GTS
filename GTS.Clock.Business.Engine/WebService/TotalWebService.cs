using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Diagnostics;
using System.Threading;
using GTS.Clock.Business.Engine;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;

namespace GTS.Clock.Business.Engine.WebServices
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode =
        AspNetCompatibilityRequirementsMode.Required)]
    public class TotalWebService : ITotalWebService
    {
        GTSEngineLogger logger = new GTSEngineLogger();

        #region GTS

        /// <summary> 
        /// اجرای قوانین برای پرسنل مشخص شده
        /// </summary>
        public void GTS_ExecuteByPersonID(string CallerIdentity, decimal PersonId)
        {
            try
            {
                Executer engine = new Executer();
                engine.Execute(CallerIdentity, PersonId, DateTime.Now.Date);
            }
            catch (BaseException ex)
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice PersonID:{0},Message:{1}", PersonId, ex.GetLogMessage()));
                logger.Flush();
                throw new GTSWebserviceException(ex.GetLogMessage(), String.Format("TotalWebService.GTS_ExecuteByPersonID({0})", PersonId));
            }

        }      

        /// <summary>
        /// مسئول پرسازی جداول "محاسبات" برای پرسنل مشخص شده می باشد  همچنین تاریخ انتها مشخص میشود
        /// برای تست موتور محاسبات استفاده می گردد
        /// </summary>
        public bool GTS_ExecuteByPersonIdAndToDate(string CallerIdentity, decimal PersonId, DateTime Date)
        {
            try
            {
                //تاریخ انتها به دلیل مسائل برگشت به عقب در مرخصی و ... باید همیشه برابر تاریخ امروز قرار بگیرید
                //در اینجا برای تست به عنوان پارامتر تاریخ انتها را می گیریم

                Executer engine = new Executer();
                engine.Execute(CallerIdentity, PersonId, Date.Date);
                return true;
            }
            catch (BaseException ex)
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice PrsonId:{0}, Message:{1}", PersonId, ex.GetLogMessage()));
                logger.Flush();
                throw ex;
            }
            //------------------------------------------            
        }

        /// <summary>
        /// اجرای قوانین برای تمامی پرسنل تا زمان حال
        /// بوسیله ویندوز سرویس
        /// </summary>
        public void GTS_ExecuteAll(string CallerIdentity)
        {
            try
            {
                //------------------------------------------           
                Executer engine = new Executer();
                engine.ExecuteByRobot(CallerIdentity, DateTime.Now.Date);
                //------------------------------------------
            }
            catch (BaseException ex)
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice Message:{0}", ex.GetLogMessage()));
                logger.Flush();
                throw ex;
            }

        }

        /// <summary>
        /// مسئول اجرای قوانین برای تمامی پرسنل تا زمان حال می باشد
        /// برای تست موتور محاسبات استفاده می گردد
        /// </summary>
        public void GTS_ExecuteAllByToDate(string CallerIdentity, DateTime Date)
        {
            try
            {
                ///TODO:Remove Date parameter
                //تاریخ انتها به دلیل مسائل برگشت به عقب در مرخصی و ... باید همیشه برابر تاریخ امروز قرار بگیرید
                //در اینجا برای تست به عنوان پارامتر تاریخ انتها را می گیریم
                //------------------------------------------           
                Executer engine = new Executer();
                engine.Execute(CallerIdentity, Date.Date);
                //------------------------------------------                
            }
            catch (BaseException ex)
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice Message:{0}", Utility.GetExecptionMessage(ex)));
                logger.Flush();
                throw ex;
            }

        }

        public bool GTS_ExecutePersonsByToDate(string CallerIdentity, IList<decimal> Persons, DateTime Date)
        {
            try
            {
                ///TODO:Remove Date parameter
                //تاریخ انتها به دلیل مسائل برگشت به عقب در مرخصی و ... باید همیشه برابر تاریخ امروز قرار بگیرید
                //در اینجا برای تست به عنوان پارامتر تاریخ انتها را می گیریم
                //------------------------------------------           
                Executer engine = new Executer();
                engine.Execute(CallerIdentity, Persons, Date.Date);
                //------------------------------------------        
                return true;
            }
            catch (BaseException ex)
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice Message:{0}", Utility.GetExecptionMessage(ex)));
                logger.Flush();
                throw ex;
            }
        }

        public int GTS_GETTotalExecuting(string CallerIdentity)
        {
            try
            {
                int count= ThreadHelper.TotalThreadCountByCaller(CallerIdentity);
                return count;
            }
            catch (Exception ex) 
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice Message:{0}", Utility.GetExecptionMessage(ex)));
                logger.Flush();
                throw ex;
            }
        }

        public int GTS_GETRemainExecuting(string CallerIdentity)
        {
            try
            {
                int count = ThreadHelper.TotalRemainThreadCountByCaller(CallerIdentity);
                return count;
            }
            catch (Exception ex)
            {
                logger.Logger.Error(String.Format("Error On TotalWebservice Message:{0}", Utility.GetExecptionMessage(ex)));
                logger.Flush();
                throw ex;
            }
        }

        public bool GTS_LockCalculation() 
        {
            Executer engine = new Executer();
            engine.LockCalculation();
            return true;
        }

        public bool GTS_UnLockCalculation() 
        {
            Executer engine = new Executer();
            engine.UnLockCalculation();
            return true;
        }
        #endregion

     
    }
}
