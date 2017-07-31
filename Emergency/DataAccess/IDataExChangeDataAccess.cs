using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Model;

namespace ZIT.EMERGENCY.fnDataAccess
{
    public interface IDataExChangeDataAccess
    {
        void insertNewEventInfo(List<ALARM_EVENT_INFO> aci);

        void insertNewSSVehInfo(List<VEHICLEREALSTATUS> aci);

        void insertNewLSVehInfo(List<VEHICLEHISTROYSTATE> aci);
    }
}
