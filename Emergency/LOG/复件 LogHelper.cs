using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZIT.LOG
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public sealed class LogHelper
    {
        /// <summary>
        /// 指示是否设置配置文件。
        /// </summary>
        private static bool IsSetConfig = false;
        /// <summary>
        /// 网络收发消息日志文件操作
        /// </summary>
        public static readonly log4net.ILog LogNetMsgInfo = log4net.LogManager.GetLogger("LogNetMsgInfo");
        /// <summary>
        /// 运行消息日志文件操作
        /// </summary>
        public static readonly log4net.ILog LogRunningInfo = log4net.LogManager.GetLogger("LogRunningInfo");
        /// <summary>
        /// 异常日志文件操作
        /// </summary>
        public static readonly log4net.ILog LogError = log4net.LogManager.GetLogger("LogError");
        /// <summary>
        /// 设置默认配置文件
        /// </summary>
        public static void SetConfig()
        {
            FileInfo ConfigFile = new FileInfo("log4net.config");
            log4net.Config.XmlConfigurator.Configure(ConfigFile);
        }
        /// <summary>
        /// 写网络首发消息日志函数
        /// </summary>
        /// <param name="strMsg">要写的信息</param>
        public static void WriteNetMsgLog(string strNetMsg)
        {
            if (!IsSetConfig)
            {
                SetConfig();
                IsSetConfig = true;
            }
            LogNetMsgInfo.Info(strNetMsg);

        }
        /// <summary>
        /// 写运行信息日志函数
        /// </summary>
        /// <param name="strMsg">要写的信息</param>
        public static void WriteLog(string strMsg)
        {
            if (!IsSetConfig)
            {
                SetConfig();
                IsSetConfig = true;
            }

            LogRunningInfo.Info(strMsg);

        }
        /// <summary>
        /// 写异常日志函数
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="ex"></param>
        public static void WriteLogError(string strMsg, Exception ex)
        {
            if (!IsSetConfig)
            {
                SetConfig();
                IsSetConfig = true;
            }


            LogError.Error(strMsg, ex);

        }
    }
}
