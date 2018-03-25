using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//【1】log4Net配置文件
//[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = @"\Config\Log4net.config", Watch = true)]
namespace WeChat.Common
{
    #region 日志类型
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogMessageType
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        Debug,
        /// <summary>
        /// 一般信息
        /// </summary>
        Info,
        /// <summary>
        /// 警告信息
        /// </summary>
        Warn,
        /// <summary>
        /// 一般错误
        /// </summary>
        Error,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal
    }

    #endregion

    #region MyRegion
    /// <summary>
    /// 日志公共帮助类
    /// </summary>
    public class LogHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static LogHelper()
        {
            // 在应用程序启动时运行的代码
            //【2】log4Net配置文件
            string configFile = @"Config\Log4net.config";
            string pathFile = string.Format("{0}{1}",AppDomain.CurrentDomain.BaseDirectory, configFile);
            var file = new FileInfo(pathFile);
            log4net.Config.XmlConfigurator.Configure(file);
        }


        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="messageType">日志类型</param>
        public static void WriteLog(string message, LogMessageType messageType)
        {
            switch (messageType)
            {
                case LogMessageType.Debug:
                    log.Debug(message);
                    break;
                case LogMessageType.Info:
                    log.Info(message);
                    break;
                case LogMessageType.Warn:
                    log.Warn(message);
                    break;
                case LogMessageType.Error:
                    log.Error(message);
                    break;
                case LogMessageType.Fatal:
                    log.Fatal(message);
                    break;
            }
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="exception">错误异常</param>
        /// <param name="messageType">日志类型（默认Error）</param>
        public static void WriteLog(string message, Exception exception, LogMessageType messageType = LogMessageType.Error)
        {
            switch (messageType)
            {
                case LogMessageType.Debug:
                    log.Debug(message, exception);
                    break;
                case LogMessageType.Info:
                    log.Info(message, exception);
                    break;
                case LogMessageType.Warn:
                    log.Warn(message, exception);
                    break;
                case LogMessageType.Error:
                    log.Error(message, exception);
                    break;
                case LogMessageType.Fatal:
                    log.Fatal(message, exception);
                    break;
            }
        }
    }
    #endregion
}
