using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.IO;
using System.Configuration;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Infrastructure.Log
{
    /// <summary>
    /// نوع ثبت لاگ
    /// </summary>
    public enum LogLevel { Error, Fatal, Debug, Info, Warn };

    /// <summary>
    /// منبع ذخیره و ثبت لاگ
    /// </summary>
    public enum LogSource
    {
        File,
        RuleLoggerToDB,
        WinSvcLogToDB,
        BusinessServiceErrorLog,
        UserActivityLog
    };

    public abstract class BaseLog
    {
        #region Variables
        const string PersonID = "PersonBarcode";
        static string configurationPath = "";
        static log4net.ILog rule_Log = null;
        static log4net.ILog db_Log = null;
        static log4net.ILog service_Log = null;
        static log4net.ILog businessServiceError_Log = null;
        string logAppender = "";
        LogSource _logSource;
        #endregion

        public bool IsFirstTime 
        {
            get 
            {
                return logAppender.Length == 0;
            }
        }

        #region Constructor
        public BaseLog()
        {
            if (IsFirstTime)
            {
                Init(LogSource.RuleLoggerToDB);
            }
        }

        public BaseLog(LogSource source)
        {
            Init(source);
        }
        #endregion

        #region Config
        private void Init(LogSource source)
        {
            _logSource = source;
            switch (source)
            {
                case LogSource.File:
                    logAppender = "LogToFile";
                    break;
                case LogSource.RuleLoggerToDB:
                    logAppender = "RuleLogToDB";
                    break;
                case LogSource.WinSvcLogToDB:
                    logAppender = "WinSvcLogToDB";
                    break;
                case LogSource.BusinessServiceErrorLog:
                    logAppender = "BusinessServiceErrors";
                    break;
                case LogSource.UserActivityLog:
                    logAppender = "ActivityLogToDB";
                    break;
                default:
                    logAppender = "LogToDB";
                    break;
            }
            try
            {
                ILog m_Log = GetLogFactory();
                //قسمت دوم شرط برای برنامه تستر کاربرد دارد
                if (m_Log == null || !(m_Log.IsDebugEnabled | m_Log.IsErrorEnabled | m_Log.IsFatalEnabled | m_Log.IsInfoEnabled | m_Log.IsWarnEnabled))
                {
                    Config();
                }
            }

            catch (NullReferenceException ex)
            {
                Exception s = new Exception();
                throw new ResourceNotFoundException("configuration file not found ", "LogConfigurationPath", ExceptionType.ALARM, "GTS.Clock.Infrastructure.Log.GTSRuleLogger.Constuctor", ex);                

            }
        }

        private void Config()
        {
            if (configurationPath.Length == 0)
            {
                configurationPath = ConfigurationSettings.AppSettings["Log4NetConfig"];
            }

            if (configurationPath.Equals(""))
            {
                throw new GTS.Clock.Infrastructure.Exceptions.ConfigNotProvided("Log4NetConfig key in AppSetting is not defined", "Log4NetConfig", ExceptionType.ALARM, "GTS.Clock.Infrastructure.Log.GTSRuleLogger Class Config");
            }
            LoadConfiguration(configurationPath);
        }

        /// <summary>
        /// بارگذاري فايل تنظيمات, در صورت عدم وجود فايل تنظيمات از فايل تنظيمات اصلي بارگذاري ميشود
        /// </summary>
        /// <param name="configPath">مسير فايل تنظيمات</param>
        private void LoadConfiguration(string configPath)
        {

            FileInfo file = new FileInfo(configPath);

            if (!file.Exists)
            {
                throw new ResourceNotFoundException("net4log config file was not found", configPath, ExceptionType.ALARM, "GTS.Clock.Infrastructure.Log.LoadConfiguration()");
            }
          
            if (log4net.LogManager.GetRepository().Configured == false)
            {
                log4net.Config.XmlConfigurator.Configure(file);
            }
            
            file = null;

            ILog m_Log = GetLogFactory();
            m_Log = log4net.LogManager.GetLogger(logAppender);
            SetLogFactory(m_Log);
        }

        #endregion

        public ILog Logger
        {
            get
            {
                return GetLogFactory();
            }
        }

        /// <summary>
        /// مسئول خالي سازي لاگ هاي موجود در انباره مي باشد
        /// </summary>
        public void Flush()
        {
            try
            {
                log4net.Appender.IAppender[] appenders = log4net.LogManager
                                                                .GetRepository()
                                                                .GetLogger(this.logAppender)
                                                                .Repository
                                                                .GetAppenders();
                foreach (log4net.Appender.IAppender appender in appenders)
                {
                    log4net.Appender.BufferingAppenderSkeleton buffer = appender as log4net.Appender.BufferingAppenderSkeleton;
                    if (buffer != null)
                    {
                        buffer.Flush();
                        // buffer.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                /// do noting!
            }
        }

        public void Info(string barcode, object message)
        {
            ILog m_Log = GetLogFactory();
            log4net.GlobalContext.Properties[PersonID] = barcode;
            m_Log.Info(message);
            log4net.GlobalContext.Properties[PersonID] = "";
        }

        public void Info(string barcode, object message, Exception exception)
        {
            ILog m_Log = GetLogFactory();
            log4net.GlobalContext.Properties[PersonID] = barcode;
            m_Log.Info(message, exception);
            log4net.GlobalContext.Properties[PersonID] = "";
        }

        public void Error(string barcode, object message)
        {
            ILog m_Log = GetLogFactory();
            log4net.GlobalContext.Properties[PersonID] = barcode;
            m_Log.Error(message);
            this.Flush();
            log4net.GlobalContext.Properties[PersonID] = "";
        }

        public void Error(string barcode, object message, Exception exception)
        {
            ILog m_Log = GetLogFactory();
            log4net.GlobalContext.Properties[PersonID] = barcode;
            m_Log.Error(message, exception);
            this.Flush();
            log4net.GlobalContext.Properties[PersonID] = "";
        }

        protected ILog GetLogFactory()
        {
            switch (_logSource)
            {
                case LogSource.RuleLoggerToDB:
                    return rule_Log;                  
                case LogSource.WinSvcLogToDB:
                    return service_Log;
                case LogSource.BusinessServiceErrorLog:
                    return businessServiceError_Log;
                default:
                    return db_Log;
            }
        }
        
        private void SetLogFactory(ILog m_log)
        {
            switch (_logSource)
            {
                case LogSource.RuleLoggerToDB:
                    rule_Log = m_log;
                    break;
                case LogSource.WinSvcLogToDB:
                    service_Log = m_log;
                    break;
                case LogSource.BusinessServiceErrorLog:
                    businessServiceError_Log = m_log;
                    break;
                default:
                    db_Log = m_log;
                    break;
            }
        }

        /// <summary>
        /// اگر سیستم تشخیص نداد که باید پیکربندی شود میتوان بصورت دستی این کار را انجام داد
        /// </summary>
        public static void ResetConfiguaraion() 
        {
            log4net.LogManager.GetRepository().ResetConfiguration();            
        }

    }
}
