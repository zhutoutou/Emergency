using System;
using ZIT.EMERGENCY.Utility;
using ZIT.EMERGENCY.Controller.BusinessServer;
using ZIT.EMERGENCY.Controller.DataAnalysis;
using ZIT.LOG;

namespace ZIT.EMERGENCY.Controller
{
    /// <summary>
    /// 核心服务类
    /// </summary>
    public class CoreService
    {
        /// <summary>
        /// 确保CoreService只有一个实例。
        /// </summary>
        private static CoreService instance = null;

        /// <summary>
        /// 与120业务服务器连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> BServerConnectionStatusChanged;
        /// <summary>
        /// 数据库连接状态变化事件
        /// </summary>
        public event EventHandler<StatusEventArgs> DBLConnectStatusChanged;


        public event EventHandler<StatusEventArgs> DBRConnectStatusChanged;

        public GServer gs;

        internal ConnectTestLocal CTL;

        internal ConnectTestRemote CTR;

        internal SendNewInfo SN;

        /// <summary>
        /// 获取当前类实例
        /// </summary>
        /// <returns></returns>
        public static CoreService GetInstance()
        {
            if (null == instance)
            {
                instance = new CoreService();
            }
            return instance;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private CoreService()
        {
            gs = new GServer();
            gs.strRemoteIP = SysParameters.GServerIP;
            gs.nLocalPort = SysParameters.GLocalPort;
            CTL = new ConnectTestLocal();
            CTR = new ConnectTestRemote();
            SN = new SendNewInfo();


        }

        /// <summary>
        /// 开始数据交换服务
        /// </summary>
        public void StartService()
        {
            try
            {
                //UDP连接120业务服务器
                gs.ConnectionStatusChanged += BusnessServer_StatusChanged;
                gs.Start();
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("UDP业务服务器启动失败", ex); }

            //启动数据库连接检测线程
            CTL.ConnectionStatusChanged += DBLConnect_StatusChanged;
            CTL.Start();
            LogHelper.WriteLog("本地数据库连接检测线程已启动");


            //启动数据库连接检测线程
            CTR.ConnectionStatusChanged += DBRConnect_StatusChanged;
            CTR.Start();
            LogHelper.WriteLog("对方数据库连接检测线程已启动");

            //启动SendNewInfo数据库析服务
            SN.Start();
            LogHelper.WriteLog("SendNewInfo数据库析服务已启动");
            
        }

        /// <summary>
        /// 停止数据交换服务
        /// </summary>
        public void StopService()
        {
            gs.Stop();
        }

        private void BusnessServer_StatusChanged(object sender, StatusEventArgs e)
        {
            OnBServerConnectionStatusChanged(e.Status);
        }
        private void DBLConnect_StatusChanged(object sender, StatusEventArgs e)
        {
            OnDBLConnectStatusChanged(e.Status);
        }

        private void DBRConnect_StatusChanged(object sender, StatusEventArgs e)
        {
            OnDBRConnectStatusChanged(e.Status);
        }
        /// <summary>
        /// Raises BServerConnectionStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnBServerConnectionStatusChanged(NetStatus status)
        {
            var handler = BServerConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }

        /// <summary>
        /// Raises DBConnectStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnDBLConnectStatusChanged(NetStatus status)
        {
            var handler = DBLConnectStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }

        protected virtual void OnDBRConnectStatusChanged(NetStatus status)
        {
            var handler = DBRConnectStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }
 
    }
}
