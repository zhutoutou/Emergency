using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.Utility;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.LOG;
using ZIT.EMERGENCY.fnDataAccess;

namespace ZIT.EMERGENCY.Controller.DataAnalysis
{
    public class SendNewInfo
    {
        private Thread td;

        private IGetDataAccess getData;

        public static int n_ALAEM = SysParameters.InsertInterval+1;

        public static int n_Veh = SysParameters.InsertInterval / 5 +1;

        public SendNewInfo()
        {
            td = new Thread(new ThreadStart(SyncLcoalInfo));
            getData = DataAccess.GetDataAccess();
        }

        public void Start()
        {
            td.Start();
        }

        /// <summary>
        ///  操作线程
        /// </summary>
        private void SyncLcoalInfo()
        {
            while (true)
            {
                try
                {
                    if (n_ALAEM > SysParameters.InsertInterval)
                    {
                        List<ALARM_EVENT_INFO> aci = getData.getNewEventInfo();
                        if (aci.Count > 0)
                        {
                            IDataExChangeDataAccess Data = DataAccess.GetDataExChangeDataAccess();
                            Data.insertNewEventInfo(aci);
                        }
                        n_ALAEM = 0;
                    }
                    if (n_Veh > (SysParameters.InsertInterval/5))
                    {
                        List<VEHICLEREALSTATUS> aci = getData.getNewSSVehInfo();
                        if (aci.Count > 0)
                        {
                            IDataExChangeDataAccess Data = DataAccess.GetDataExChangeDataAccess();
                            Data.insertNewSSVehInfo(aci);
                        }
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
