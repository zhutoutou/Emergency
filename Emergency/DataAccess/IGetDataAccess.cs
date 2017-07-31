using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Model;

namespace ZIT.EMERGENCY.fnDataAccess
{
    public interface IGetDataAccess
    {
        /// <summary>
        /// 车辆基础信息
        /// </summary>
        /// <returns></returns>
        List<ALARM_EVENT_INFO> getNewEventInfo();

        List<VEHICLEREALSTATUS> getNewSSVehInfo();

        List<VEHICLEHISTROYSTATE> getNewLSVehInfo(string strcarID, string strLSH,string strCCXH);


    }
}
