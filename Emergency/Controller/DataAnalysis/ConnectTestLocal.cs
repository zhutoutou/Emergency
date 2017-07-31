using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.EMERGENCY.fnDataAccess;

namespace ZIT.EMERGENCY.Controller.DataAnalysis
{
    public class ConnectTestLocal
    {
        private bool blConnected;

        private Thread td;

        public ConnectTestLocal()
        {
            td = new Thread(new ThreadStart(Todo));
            ConnTest = DataAccess.GetDBConnTestLocal();
            blConnected = false;
        }

        private IDBConnTest ConnTest;

        /// <summary>
        /// 与120业务服务器连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> ConnectionStatusChanged;

        public void Start()
        {
            td.Start();
        }

        /// <summary>
        ///  操作线程
        /// </summary>
        private void Todo()
        {
            while (true)
            {
                try
                {
                    if (ConnTest.DBIsConnected())
                    {
                        if (!blConnected)
                        {
                            blConnected = true;
                            //raise connect event
                            OnConnectionStatusChanged(NetStatus.Connected);
                        }
                    }
                    else
                    {
                        if (blConnected)
                        {
                            blConnected = false;
                            //raise disconnect event
                            OnConnectionStatusChanged(NetStatus.DisConnected);
                        }
                    }
                }
                catch
                {
                }
                Thread.Sleep(5 * 1000);
            }
        }

        public void Stop()
        {
            td.Abort();
        }

        private void OnConnectionStatusChanged(NetStatus status)
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }
    }
}
