using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using ZIT.EMERGENCY.fnDataAccess;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.Utility;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle
{
    class DBConnTestLocal : IDBConnTest
    {
        public bool DBIsConnected()
        {
            bool bIsConnected = DB120Help.IsConnected();
            return bIsConnected;
        }
        
    }
}
