using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Model;
namespace ZIT.EMERGENCY.DataAccess
{
    public interface IDBsendNewInfo
    {
        void sendNewEventInfo();

        void sendNewSSVehInfo();

        void sendNewLSVehInfo(string strcarID, string strLSH, string strCCXH);

        Dictionary<string,VehInfo> getInitVehMap();
    }
}
