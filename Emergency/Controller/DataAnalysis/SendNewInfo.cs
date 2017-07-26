using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.DataAccess;
using ZIT.EMERGENCY.Utility;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.LOG;

namespace ZIT.EMERGENCY.Controller.DataAnalysis
{
    public class SendNewInfo
    {
        private Thread td;

        private IDBsendNewInfo sendNewInfo;

        public static int n_ALAEM = 0;

        public static int n_Veh = 0;

        public SendNewInfo()
        {
            td = new Thread(new ThreadStart(Todo));
            sendNewInfo = DataAccess.DataAccess.GetDBsendNewInfo();
        }

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
                    if (n_ALAEM > SysParameters.InsertInterval)
                    {
                        sendNewInfo.sendNewEventInfo();
                        n_ALAEM = 0;
                    }
                    if (n_Veh > (SysParameters.InsertInterval/5))
                    {
                        sendNewInfo.sendNewSSVehInfo();
                        n_Veh = 0;
                    }
                    n_ALAEM++;
                    n_Veh++;

                }
                catch (Exception ex)
                {
                    LOG.LogHelper.WriteLog("程序异常!", ex);
                }
                Thread.Sleep(60 * 1000);
            }
        
        }


        public void Stop()
        {
            td.Abort();
        }
    }
}
