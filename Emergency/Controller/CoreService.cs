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
        public event EventHandler<StatusEventArgs> DBConnectStatusChanged;

        public GServer gs;

        internal ConnectTest CT;

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
            CT = new ConnectTest();
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
            CT.ConnectionStatusChanged += DBConnect_StatusChanged;
            CT.Start();
            LogHelper.WriteLog("数据库连接检测线程已启动");

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
        private void DBConnect_StatusChanged(object sender, StatusEventArgs e)
        {
            OnDBConnectStatusChanged(e.Status);
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
        protected virtual void OnDBConnectStatusChanged(NetStatus status)
        {
            var handler = DBConnectStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }
        //
        // 摘要:
        //     卡类型的值对关系
        //
        // 参数:
        //   XB:
        //     卡类型
        //
        //   type:
        //     类型为文字转数字，则为 0；否则为 1。
        //
        public static string GetKLX(string KLX, int type)
        {
            KLX = KLX.Trim();
            if (type == 0)
            {
                switch (KLX)
                {
                    case "0":
                        KLX = "社保卡";
                        break;
                    case "1":
                        KLX = "农保卡";
                        break;
                    case "2":
                        KLX = "通用就诊卡";
                        break;
                    case "8":
                        KLX = "身份证";
                        break;
                    case "9":
                        KLX = "其他";
                        break;
                    case "A":
                        KLX = "健康档案卡";
                        break;
                    default:
                        KLX = "其他";
                        break;
                }
                return KLX;
            }
            else
            {
                switch (KLX)
                {
                    case "社保卡":
                        KLX = "0";
                        break;
                    case "农保卡":
                        KLX = "1";
                        break;
                    case "通用就诊卡":
                        KLX = "2";
                        break;
                    case "身份证":
                        KLX = "8";
                        break;
                    case "其他":
                        KLX = "9";
                        break;
                    case "健康档案卡":
                        KLX = "A";
                        break;
                    default:
                        KLX = "9";
                        break;
                }
                return KLX;
            }
        }

        //
        // 摘要:
        //     性别的值对关系
        //
        // 参数:
        //   XB:
        //     性别
        //
        //   type:
        //     类型为文字转数字，则为 0；否则为 1。
        //
        public static string GetXB(string XB, int type)
        {
            XB = XB.Trim();
            if (type == 0)
            {
                switch (XB)
                {
                    case "0":
                        XB = "未知";
                        break;
                    case "1":
                        XB = "男";
                        break;
                    case "2":
                        XB = "女";
                        break;
                    default:
                        XB = "未知";
                        break;
                }
                return XB;
            }
            else
            {
                switch (XB)
                {
                    case "未知":
                        XB = "0";
                        break;
                    case "男":
                        XB = "1";
                        break;
                    case "女":
                        XB = "2";
                        break;
                    default:
                        XB = "0";
                        break;
                }
                return XB;
            }
        }
 
    }
}
