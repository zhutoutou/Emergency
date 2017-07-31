using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Utility;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle
{
    class DBConnTestRemote : IDBConnTest
    {
        public bool DBIsConnected()
        {
            bool bIsConnected = DB120Helpcle.IsConnected();
            return bIsConnected;
        }

    }
}
