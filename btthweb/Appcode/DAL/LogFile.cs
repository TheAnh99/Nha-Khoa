using btthweb.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btthweb.Appcode.DAL
{
    public class LogFile
    {
        private static readonly LogFile _instance = new LogFile();
        protected ILog monitoringLogger;
        protected static ILog debugLogger;

        private LogFile()
        {
            monitoringLogger = LogManager.GetLogger("MonitoringLogger");
            debugLogger = LogManager.GetLogger("DebugLogger");
        }

        /// <summary>
        /// Used to log Debug messages in an explicit Debug Logger
        /// </summary>
        /// <param name="message">The object message to log</param>
        public static void Debug(string message)
        {
            debugLogger.Debug(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public static void Debug(string message, System.Exception exception)
        {
            debugLogger.Debug(message, exception);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        public static void Info(string message)
        {
            _instance.monitoringLogger.Info(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public static void Info(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Info(message, exception);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
       /* public static void Info(NewsCBTT message, string action)
        {
            string loi = action + " - Stock_Code:" + message.Stock_Code + ";UserName:" + message.UserName + ";Title:" + message.Title + ";ID:" + message.ID + ";NewsID:" + message.NewsID + ";SID:" + message.SID + ";DatePub:" + message.DatePub + ";DocTypeID:" + message.DocTypeID + ";Language:" + message.Language + ";URL:" + message.URL + ";ReportYear:" + message.ReportYear;
            _instance.monitoringLogger.Info(loi);
        }*/

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        public static void Warn(string message)
        {
            _instance.monitoringLogger.Warn(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public static void Warn(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Warn(message, exception);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        public static void Error(string message)
        {
            _instance.monitoringLogger.Error(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public static void Error(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Error(message, exception);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        public static void Fatal(string message)
        {
            _instance.monitoringLogger.Fatal(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public static void Fatal(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Fatal(message, exception);
        }
    }
}