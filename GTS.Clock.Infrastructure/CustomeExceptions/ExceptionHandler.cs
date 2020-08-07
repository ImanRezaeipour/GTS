using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public static class ExceptionHandler
    {
        public static void HandleException(LogLevel logLevel, string logMessage,BaseException  ex) 
        {
            GTSEngineLogger logger = new GTSEngineLogger();
            string msgLog = "";
            msgLog = ex.GetLogMessage();
            //msgLog += GetCustomeExceptionMessage(ex);

            switch (logLevel)
            {
                case LogLevel.Error:
                    logger.Logger.Error(logMessage + " ** " + msgLog, ex);
                    break;
                case LogLevel.Fatal:
                    logger.Logger.Fatal(logMessage + " ** " + msgLog, ex);
                    break;
                case LogLevel.Info:
                    logger.Logger.Info(logMessage + " ** " + msgLog, ex);
                    break;
                case LogLevel.Warn:
                    logger.Logger.Warn(logMessage + " ** " + msgLog, ex);
                    break;
                case LogLevel.Debug:
                    logger.Logger.Debug(logMessage + " ** " + msgLog, ex);
                    break;
            }
            if (ex.ExceptionTypeActivity == ExceptionType.FATAL) 
            {
                throw ex;
            }
        }

        

        

        public static void HandleException(LogLevel logLevel, BaseException ex)
        {
            GTSEngineLogger logger = new GTSEngineLogger();
            string msgLog = "";
            msgLog = ex.GetLogMessage();            

            switch (logLevel)
            {
                case LogLevel.Error:
                    logger.Logger.Error(msgLog, ex);
                    break;
                case LogLevel.Fatal:
                    logger.Logger.Fatal(msgLog, ex);
                    break;
                case LogLevel.Info:
                    logger.Logger.Info(msgLog, ex);
                    break;
                case LogLevel.Warn:
                    logger.Logger.Warn(msgLog, ex);
                    break;
                case LogLevel.Debug:
                    logger.Logger.Debug(msgLog, ex);
                    break;
            }
            if (ex.ExceptionTypeActivity == ExceptionType.FATAL)
            {
                throw ex;
            }
        }

        public static void HandleException(LogLevel logLevel, string logMessage)
        {
            GTSEngineLogger logger = new GTSEngineLogger();

            switch (logLevel)
            {
                case LogLevel.Error:
                    logger.Logger.Error(logMessage);
                    break;
                case LogLevel.Fatal:
                    logger.Logger.Fatal(logMessage);
                    break;
                case LogLevel.Info:
                    logger.Logger.Info(logMessage);
                    break;
                case LogLevel.Warn:
                    logger.Logger.Warn(logMessage);
                    break;
                case LogLevel.Debug:
                    logger.Logger.Debug(logMessage);
                    break;
            }
        }

        /// <summary>
        /// بصورت پیش فرض Info Log
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ex"></param>
        public static void HandleException(string logMessage, BaseException ex)
        {
            string msgLog = "";
            GTSEngineLogger logger = new GTSEngineLogger();
            msgLog = ex.GetLogMessage();            
            logger.Logger.Info(logMessage + " ** " + msgLog, ex);
            if (ex.ExceptionTypeActivity == ExceptionType.FATAL)
            {
                throw ex;
            }
        }

        /// <summary>
        /// بصورت پیش فرض Info Log
        /// </summary>
        /// <param name="logMessage"></param>       
        public static void HandleException(string logMessage)
        {
            GTSEngineLogger logger = new GTSEngineLogger();
            logger.Logger.Info(logMessage);

        }
        
    }
}
