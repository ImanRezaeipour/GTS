using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Business;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Business
{
    public abstract class BaseGTSBusiness<T> : MarshalByRefObject
        where T : GTS.Clock.Model.IEntity, new()
    {
        public string CurrentUsername
        {
            get;
            set;
        }

        public decimal CurrentUserId
        {
            get;
            set;
        }

        public bool IsBusinessLogEnable
        { get 
        {
            return true;
        }
            set 
            {
                bool x = true;
            }
        }
       
        #region variables
        static BusinessServiceLogger businessErrorlogger = new BusinessServiceLogger();
        static BusinessActivityLogger acctivityLogger = new BusinessActivityLogger();
        //private EntityRepository<T> objRepository = new EntityRepository<T>(false);
        private IRepository<T> objRepository = null;
       
        #endregion

        public BaseGTSBusiness(string curentUsername, decimal currentUserId, IRepository<T> repository, bool isBusinessLogEnable) 
        {
            try
            {
                this.CurrentUsername = curentUsername;
                this.CurrentUserId = currentUserId;
                this.IsBusinessLogEnable = isBusinessLogEnable;
                this.objRepository = repository;
            }
            catch (AthorizeServiceException ex) 
            {
                LogException(ex, typeof(T).Name, "Cunstructor", IsBusinessLogEnable);
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
                LogException(ex, typeof(T).Name, "GetByID", IsBusinessLogEnable);
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
            if (list == null)
                list = new List<T>();
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
                int count = this.GetRecordCount();
                if (pageSize * pageIndex < count)
                {
                    IList<T> result = objRepository.GetAllByPage(pageIndex, pageSize);
                    if (result == null)
                        result = new List<T>();
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), typeof(T).FullName + " -> GetAllByPage ");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, typeof(T).Name, "GetAllByPage", IsBusinessLogEnable);
                throw ex;
            }
        }

        /// <summary>
        /// تعداد رکوردها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public virtual int GetRecordCount()
        {           
            int count = objRepository.GetCountByCriteria();
            return count;
        }        

        #endregion

        #region SaveChanges

        /// <summary>
        /// عملیات درج و بروزرسانی انجام میشود
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>شناسه آیتم درج یا بروزرسانی شده</returns>
        public virtual decimal BaseSaveChanges(T obj, UIActionType action)
        {
            try
            {
                BaseGetReadyBeforeSave(obj, action);

                if (action == UIActionType.ADD)
                {
                    InsertValidate(obj);
                    BaseUIValidate(obj, action);
                    Insert(obj);
                }
                else if (action == UIActionType.EDIT)
                {
                    UpdateValidate(obj);
                    BaseUIValidate(obj, action);
                    Update(obj);
                }
                else if (action == UIActionType.DELETE)
                {
                    DeleteValidate(obj);
                    BaseUIValidate(obj, action);
                    Delete(obj);
                }
                BaseOnSaveChangesSuccess(obj, action);
                BaseUpdateCFP(obj, action);
                LogUserAction(obj, action.ToString(), this.CurrentUsername, IsBusinessLogEnable);
                return obj.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, typeof(T).Name, "SaveChanges", IsBusinessLogEnable);
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

                LogException(ex, typeof(T).Name + " - " + "BaseGTSBusiness-Nhibernate Action", IsBusinessLogEnable);

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

                LogException(ex, typeof(T).Name + " - " + "BaseGTSBusiness-Nhibernate Action", IsBusinessLogEnable);

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
                LogException(ex, typeof(T).Name + " - " + "BaseGTSBusiness-Nhibernate Action", IsBusinessLogEnable);

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
        /// <param name="clCar"></param>
        protected abstract void InsertValidate(T clCar);

        /// <summary>
        /// باید توسط بچه ها پیاده سازی شود
        /// </summary>
        /// <param name="clCar"></param>
        protected abstract void UpdateValidate(T clCar);

        /// <summary>
        /// باید توسط بچه ها پیاده سازی شود
        /// </summary>
        /// <param name="clCar"></param>
        protected abstract void DeleteValidate(T clCar);

        /// <summary>
        /// اگر بروزرسانی و درج و حذف باموفقیت انجام گیرد این تابع صدا زده میشود
        /// </summary>
        /// <param name="action"></param>
        protected virtual void BaseOnSaveChangesSuccess(T obj, UIActionType action)
        { }

        /// <summary>
        /// اگر شی نیاز به مقداردهی قبل از ذخیره دارد این تابع پیاده سازی دوباره شود
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void BaseGetReadyBeforeSave(T obj, UIActionType action)
        { }

        /// <summary>
        /// بروزرسانی نشانه تغییرات
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void BaseUpdateCFP(T obj, UIActionType action)
        {
        }

        protected virtual void BaseUIValidate(T obj, UIActionType action)
        { }
        #endregion

        #region Log

        protected void LogException(UIValidationExceptions ex, string className, string methodName,string currentUserName, bool isBusinessLogEnable)
        {
            if (ex.InsertedLog) return;
            ex.InsertedLog = true;

            string curentUsername = currentUserName;
            if (curentUsername.ToLower().Equals("nunituser")) return;
            if (isBusinessLogEnable)
            {
                businessErrorlogger.Info(curentUsername, className, methodName, ex.Source, ex.Message, ex);
            }
        }

        protected void LogException(BaseException ex, string className, string methodName, string currentUserName, bool isBusinessLogEnable)
        {
            if (ex.InsertedLog) return;
            ex.InsertedLog = true;
            string curentUsername = currentUserName;

            if (curentUsername.ToLower().Equals("nunituser")) return;

            if (isBusinessLogEnable)
            {
                if (ex is InvalidPersianDateException)
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "BaseGTSBusiness", " InvalidPersianDateException --> " + ((InvalidPersianDateException)ex).GetLogMessage(), ex);
                }
                else if (ex is UIBaseException)
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "BaseGTSBusiness", String.Format("{0} --> {1}", ex.GetType().Name, ((UIBaseException)ex).GetLogMessage()), ex);
                }
                else
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "BaseGTSBusiness", String.Format("{0} --> {1}", ex.GetType().Name, ((BaseException)ex).GetLogMessage()), ex);
                }
            }
        }

        protected void LogException(Exception ex, string className, string methodName, string currentUserName, bool isBusinessLogEnable)
        {
            if (ex is BaseException)
            {
                LogException((BaseException)ex, className, methodName, currentUserName, isBusinessLogEnable);
            }
            else if (ex is UIValidationExceptions)
            {
                LogException((UIValidationExceptions)ex, className, methodName, currentUserName, isBusinessLogEnable);
            }
            else
            {
                string curentUsername = currentUserName;
                if (curentUsername==null ||curentUsername.ToLower().Equals("nunituser")) return;
                if (isBusinessLogEnable)
                {
                    businessErrorlogger.Error(curentUsername, className, methodName, "BaseGTSBusiness", Utility.GetExecptionMessage(ex), ex);
                }
            }
        }

        protected void LogException(Exception ex, string currentUserName, bool isBusinessLogEnable)
        {
            string className = Utility.CallerCalassName;
            string methodName = Utility.CallerMethodName;
            string exSource = Utility.CallerCalassFullName;
            if (isBusinessLogEnable)
            {
                string curentUsername = currentUserName;
                if (curentUsername.ToLower().Equals("nunituser")) return;
                businessErrorlogger.Error(curentUsername, className, methodName, exSource, Utility.GetExecptionMessage(ex), ex);
            }
        }

        protected void LogException(Exception ex, string execptionSrcDescription, string currentUserName, bool isBusinessLogEnable)
        {
            string className = Utility.CallerCalassName;
            string methodName = Utility.CallerMethodName;
            string exSource = execptionSrcDescription+ " -- " + Utility.CallerCalassFullName;
            if (isBusinessLogEnable)
            {
                string curentUsername = currentUserName;
                if (curentUsername.ToLower().Equals("nunituser")) return;
                businessErrorlogger.Error(curentUsername, className, methodName, exSource, Utility.GetExecptionMessage(ex), ex);
            }
        }

        protected void LogUserAction(string action, string currentUserName, bool isBusinessLogEnable)
        {
            try
            {
                string className = Utility.CallerCalassName;
                string methodName = Utility.CallerMethodName;
                string curentUsername = currentUserName;
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

        protected void LogUserAction(T obj, string action, string currentUserName, bool isBusinessLogEnable)
        {
            try
            {
                string methodName = Utility.CallerMethodName;
                string curentUsername = currentUserName;
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


   
    }
}
