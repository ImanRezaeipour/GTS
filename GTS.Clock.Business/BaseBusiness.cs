using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using System.Reflection;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Validation.Configuration;

namespace GTS.Clock.Business
{
    public abstract class BaseBusiness<T> : MarshalByRefObject
        where T : GTS.Clock.Model.IEntity, new()
    {
        public string CurrentUsername
        {
            get { return Security.BUser.CurrentUser.UserName; }
        }

        public decimal CurrentUserId
        {
            get { return Security.BUser.CurrentUser.ID; }
        }

        #region variables
        static BusinessServiceLogger businessErrorlogger = new BusinessServiceLogger();
        static BusinessActivityLogger acctivityLogger = new BusinessActivityLogger();
        private EntityRepository<T> objRepository = new EntityRepository<T>(false);
        protected CFPRepository cfpRepository = new CFPRepository(false);
        #endregion

        public BaseBusiness() 
        {
            try
            {
                DoAuthorize(this.GetType().FullName);
            }
            catch (AthorizeServiceException ex) 
            {
                LogException(ex, typeof(T).Name, "Cunstructor");
                throw ex;
            }
        }

        #region Get Services

        /// <summary>
        /// یک آیتم را بوسیله کلید اصلی جستجو میکند
        /// اگر آیتم موجود نباشد خطا پرتاب میکند
        /// </summary>
        /// <param name="emplID"></param>
        /// <returns></returns>
        public virtual T GetByID(decimal objID)
        {
            try
            {
                Type t = this.GetType();
                T obj = objRepository.GetById(objID, false);
                if (obj != null)
                    return obj;

                throw new ItemNotExists(String.Format("{0} با این شناسه موجود نمیباشد", typeof(T).Name), typeof(T).FullName);
            }
            catch (Exception ex)
            {
                LogException(ex, typeof(T).Name, "GetByID");
                throw ex;
            }
        }

        /// <summary>
        /// لیست همه را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAll()
        {
            IList<T> list = objRepository.GetAll();
            return list;
        }

        /// <summary>
        /// همه را بصورت صفحه بندی برمیگرداند
        /// </summary>
        /// <param name="pageSize">سایز صفحه</param>
        /// <param name="pageIndex">ایندکس</param>
        /// <returns></returns>
        public virtual IList<T> GetAllByPage(int pageIndex, int pageSize)
        {
            try
            {
                EntityRepository<T> rep = new EntityRepository<T>(false);
                int count = this.GetRecordCount();
                if (pageSize * pageIndex < count)
                {
                    IList<T> result = rep.GetAllByPage(pageIndex, pageSize);
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), typeof(T).FullName + " -> GetAllByPage ");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, typeof(T).Name, "GetAllByPage");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد رکوردها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public virtual int GetRecordCount()
        {
            EntityRepository<T> rep = new EntityRepository<T>(false);
            int count = rep.GetCountByCriteria();
            return count;
        }        

        #endregion

        #region SaveChanges

        /// <summary>
        /// عملیات درج و بروزرسانی انجام میشود
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>شناسه آیتم درج یا بروزرسانی شده</returns>
        public virtual decimal SaveChanges(T obj, UIActionType action)
        {
            try
            {
                GetReadyBeforeSave(obj, action);

                if (action == UIActionType.ADD)
                {
                    DoAuthorize(this.GetType().FullName + ".Insert");
                    InsertValidate(obj);
                    UIValidate(obj, action);
                    Insert(obj);
                }
                else if (action == UIActionType.EDIT)
                {
                    DoAuthorize(this.GetType().FullName + ".Update");
                    UpdateValidate(obj);
                    UIValidate(obj, action);
                    Update(obj);
                }
                else if (action == UIActionType.DELETE)
                {
                    DoAuthorize(this.GetType().FullName + ".Delete");
                    DeleteValidate(obj);
                    UIValidate(obj, action);
                    Delete(obj);
                }
                OnSaveChangesSuccess(obj, action);
                UpdateCFP(obj, action);
                LogUserAction(obj, action.ToString());
                return obj.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, typeof(T).Name, "SaveChanges");
                throw ex;
            }
        }

        protected virtual void Insert(T obj)
        {
            try
            {
                objRepository.Save(obj);
            }
            catch (Exception ex)
            {

                LogException(ex, typeof(T).Name + " - " + "GTS.Clock.Business-Nhibernate Action");

                throw ex;
            }
        }

        protected virtual void Update(T obj)
        {
            try
            {
                objRepository.Update(obj);
            }
            catch (Exception ex)
            {

                LogException(ex, typeof(T).Name + " - " + "GTS.Clock.Business-Nhibernate Action");

                throw ex;
            }
        }

        protected virtual bool Delete(T obj)
        {
            try
            {
                objRepository.Delete(obj);
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, typeof(T).Name + " - " + "GTS.Clock.Business-Nhibernate Action");

                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Saving Events
        /// <summary>
        /// باید توسط بچه ها پیاده سازی شود
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void InsertValidate(T obj);

        /// <summary>
        /// باید توسط بچه ها پیاده سازی شود
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void UpdateValidate(T obj);

        /// <summary>
        /// باید توسط بچه ها پیاده سازی شود
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void DeleteValidate(T obj);

        /// <summary>
        /// اگر بروزرسانی و درج و حذف باموفقیت انجام گیرد این تابع صدا زده میشود
        /// </summary>
        /// <param name="action"></param>
        protected virtual void OnSaveChangesSuccess(T obj, UIActionType action)
        { }

        /// <summary>
        /// اگر شی نیاز به مقداردهی قبل از ذخیره دارد این تابع پیاده سازی دوباره شود
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void GetReadyBeforeSave(T obj, UIActionType action)
        { }

        /// <summary>
        /// بروزرسانی نشانه تغییرات
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void UpdateCFP(T obj, UIActionType action)
        {
        }

        protected virtual void UIValidate(T obj, UIActionType action)
        { }
        #endregion

        #region Log

        protected static bool IsBusinessErrorLogEnable
        {
            get
            {
                ApplicationSettings appSettings = BApplicationSettings.CurrentApplicationSettings;
                return appSettings.BusinessErrorLogEnable;

            }
        }

        protected static bool IsBusinessLogEnable
        {
            get
            {
                ApplicationSettings appSettings = BApplicationSettings.CurrentApplicationSettings;
                return appSettings.BusinessLogEnable;
            }
        }


        public static void LogException(UIValidationExceptions ex, string className, string methodName)
        {
            if (ex.InsertedLog) return;
            ex.InsertedLog = true;

            string curentUsername = Security.BUser.CurrentUser.UserName;
            if (curentUsername.ToLower().Equals("nunituser")) return;
            if (IsBusinessLogEnable)
            {
                businessErrorlogger.Info(curentUsername, className, methodName, ex.Source, ex.Message, ex);
            }
        }

        public static void LogException(BaseException ex, string className, string methodName)
        {
            if (ex.InsertedLog) return;
            ex.InsertedLog = true;
            string curentUsername = Security.BUser.CurrentUser.UserName;

            if (curentUsername.ToLower().Equals("nunituser")) return;

            if (IsBusinessErrorLogEnable)
            {
                if (ex is InvalidPersianDateException)
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "GTS.Clock.Business", " InvalidPersianDateException --> " + ((InvalidPersianDateException)ex).GetLogMessage(), ex);
                }
                else if (ex is UIBaseException)
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "GTS.Clock.Business", String.Format("{0} --> {1}", ex.GetType().Name, ((UIBaseException)ex).GetLogMessage()), ex);
                }
                else
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "GTS.Clock.Business", String.Format("{0} --> {1}", ex.GetType().Name, ((BaseException)ex).GetLogMessage()), ex);
                }
            }
        }

        public static void LogException(Exception ex, string className, string methodName)
        {
            if (ex is BaseException)
            {
                LogException((BaseException)ex, className, methodName);
            }
            else if (ex is UIValidationExceptions)
            {
                LogException((UIValidationExceptions)ex, className, methodName);
            }
            else
            {
                string curentUsername = Security.BUser.CurrentUser.UserName;
                if (curentUsername==null ||curentUsername.ToLower().Equals("nunituser")) return;
                if (IsBusinessLogEnable)
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "GTS.Clock.Business", Utility.GetExecptionMessage(ex), ex);
                }
            }
        }

        public static void LogException(Exception ex)
        {
            string className = Utility.CallerCalassName;
            string methodName = Utility.CallerMethodName;
            string exSource = Utility.CallerCalassFullName;
            if (IsBusinessErrorLogEnable)
            {
                string curentUsername = Security.BUser.CurrentUser.UserName;
                if (curentUsername.ToLower().Equals("nunituser")) return;
                businessErrorlogger.Error(curentUsername, className, methodName, exSource, Utility.GetExecptionMessage(ex), ex);
            }
        }

        public static void LogException(Exception ex,string execptionSrcDescription)
        {
            string className = Utility.CallerCalassName;
            string methodName = Utility.CallerMethodName;
            string exSource = execptionSrcDescription+ " -- " + Utility.CallerCalassFullName;
            if (IsBusinessErrorLogEnable)
            {
                string curentUsername = Security.BUser.CurrentUser.UserName;
                if (curentUsername.ToLower().Equals("nunituser")) return;
                businessErrorlogger.Error(curentUsername, className, methodName, exSource, Utility.GetExecptionMessage(ex), ex);
            }
        }

        public static void LogUserAction(string action)
        {
            try
            {
                string className = Utility.CallerCalassName;
                string methodName = Utility.CallerMethodName;
                string curentUsername = Security.BUser.CurrentUser.UserName;
                if (curentUsername.ToLower().Equals("nunituser")) return;

                string clientIPAddress = "";
                string pageId = "";

                if (System.Web.HttpContext.Current != null &&
                    System.Web.HttpContext.Current.Request != null)
                {
                    if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                    {
                        clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }
                    if (System.Web.HttpContext.Current.Request.UrlReferrer != null &&
                        System.Web.HttpContext.Current.Request.UrlReferrer.Segments != null &&
                        System.Web.HttpContext.Current.Request.UrlReferrer.Segments.Length > 2)
                    {
                        pageId = System.Web.HttpContext.Current.Request.UrlReferrer.Segments[2];
                    }
                }

                acctivityLogger.Info(curentUsername, className, methodName, action, pageId, clientIPAddress, "");
            }
            catch (Exception ex)
            {
                ///do nothing....
            }
        }

        public static void LogUserAction(T obj, string action)
        {
            try
            {
                string methodName = Utility.CallerMethodName;
                string curentUsername = Security.BUser.CurrentUser.UserName;
                if (curentUsername.ToLower().Equals("nunituser")) return;

                string clientIPAddress = "";
                string pageId = "";

                if (System.Web.HttpContext.Current != null &&
                    System.Web.HttpContext.Current.Request != null)
                {
                    if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                    {
                        clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }
                    if (System.Web.HttpContext.Current.Request.UrlReferrer != null &&
                        System.Web.HttpContext.Current.Request.UrlReferrer.Segments != null &&
                        System.Web.HttpContext.Current.Request.UrlReferrer.Segments.Length > 2)
                    {
                        pageId = System.Web.HttpContext.Current.Request.UrlReferrer.Segments[2];
                    }
                }

                acctivityLogger.Info(curentUsername, typeof(T).Name, methodName, action, pageId, clientIPAddress, obj.ToString());
            }
            catch (Exception ex)
            {
                ///do nothing....
            }
        }


        #endregion

      
        protected void DoAuthorize(string fullMethodName)
        {
            return;
            UserRepository repository=new UserRepository(false);

            if (CurrentUsername != null && CurrentUsername.Length > 0)
            {
                 IList<UserRepository.UserAuthorization> athorizeList =
                repository.GetUserAthorization(this.CurrentUsername);

                 IList<UserRepository.UserAuthorization> list= athorizeList.Where(x => x.Method.ToLower().Equals(fullMethodName.ToLower()))
                     .ToList<UserRepository.UserAuthorization>();
                 if (list.Count != 1)
                 {
                     throw new AthorizeServiceException("برای متد تعریف شده بیشتر یا کمتر از یک منبع پیدا شد",CurrentUsername,fullMethodName);
                 }
                 if (!list[0].Allow) 
                 {
                     throw new AthorizeServiceException("فراخوانی این سرویس برای این کاربر مجاز نمیباشد", CurrentUsername, fullMethodName);
                 }
            }
        }

        protected ILockCalculationUIValidation GetUIValidator() 
        {
            ILockCalculationUIValidation validator = UIValidationFactory.GetRepository<ILockCalculationUIValidation>();
            if (validator != null)
            {
                return validator;
            }
            else
                throw new Exception("Validator is null");        
        }

        protected ILockCalculationUIValidation UIValidator
        {
            get
            {
                ILockCalculationUIValidation validator = UIValidationFactory.GetRepository<ILockCalculationUIValidation>();
                if (validator != null)
                {
                    return validator;
                }
                else
                    throw new Exception("Validator is null");
            }
        }

        #region CFP

        /// <summary>
        /// بروزرسانی نشانه تغییرات
        /// </summary>
        /// <param name="obj"></param>
        private void InsertCFP(decimal personId, DateTime date)
        {
            CFP cfp = new CFP();
            cfp.PrsId = personId;
            cfp.Date = date;
            cfp.CalculationIsValid = false;
            cfpRepository.Save(cfp);
        }

        /// <summary>
        /// بروزرسانی نشانه محاسبات
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cfpDate"></param>
        protected void UpdateCFP(decimal personId, DateTime cfpDate)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    string className = Utility.CallerCalassName;

                    LogUserAction(String.Format("CFP {0} Prs:{1} cls:{2} ", Utility.ToPersianDate(cfpDate), personId, className));

                    CFP cfp = cfpRepository.GetByPersonID(personId);
                    if (cfp != null && cfp.ID > 0)
                    {
                        if (cfp.Date.Date >= cfpDate.Date)
                        {
                            cfp.Date = cfpDate.Date;
                            cfp.CalculationIsValid = false;
                            cfpRepository.WithoutTransactUpdate(cfp);
                        }
                    }
                    else
                    {
                        cfpRepository.InsertCFP(personId, cfpDate.Date);
                    }
                    PermitRepository permitRep = new PermitRepository();
                    permitRep.InvalidateTrafficCalculation(personId, cfpDate);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex) 
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// بروزرسانی نشانه محاسبات
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cfpDate"></param>
        protected void UpdateCFP(CFP cfp, decimal personId, DateTime cfpDate, bool invalidateTraffic)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    //string className = Utility.CallerCalassName;

                    //LogUserAction(String.Format("CFP {0} Prs:{1} cls:{2} ", Utility.ToPersianDate(cfpDate), personId, className));

                    if (cfp != null && cfp.ID > 0)
                    {
                        if (cfp.Date.Date >= cfpDate.Date)
                        {
                            cfp.Date = cfpDate.Date;
                            cfp.CalculationIsValid = false;
                            cfpRepository.WithoutTransactUpdate(cfp);
                        }
                    }
                    else
                    {
                        cfpRepository.InsertCFP(personId, cfpDate.Date);
                    }
                    if (invalidateTraffic)
                    {
                        PermitRepository permitRep = new PermitRepository();
                        permitRep.InvalidateTrafficCalculation(personId, cfpDate);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// بروزرسانی نشانه محاسبات
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cfpDate"></param>
        protected void UpdateCFP(IList<CFP> cfpList, bool invalidateTraffic)
        {
            LogUserAction(String.Format("CFP Update Count:{0} Started", cfpList.Count));

            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    foreach (CFP cfp in cfpList)
                    {
                        if (cfp != null && cfp.ID > 0)
                        {
                            cfp.CalculationIsValid = false;
                            cfpRepository.WithoutTransactUpdate(cfp);
                        }
                        else
                        {
                            cfpRepository.InsertCFP(cfp.PrsId, cfp.Date);
                        }
                        if (invalidateTraffic)
                        {
                            PermitRepository permitRep = new PermitRepository();
                            permitRep.InvalidateTrafficCalculation(cfp.PrsId, cfp.Date);
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    LogUserAction(String.Format("CFP Update Count:{0} Finished", cfpList.Count));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        protected CFP GetCFP(decimal personId) 
        {
             CFP cfp = cfpRepository.GetByPersonID(personId);
             if (cfp != null && cfp.ID > 0)
             {
                 return cfp;
             }
             return new CFP();
        }
        #endregion

   
    }
}
