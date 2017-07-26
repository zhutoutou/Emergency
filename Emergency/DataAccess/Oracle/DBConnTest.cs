using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using ZIT.EMERGENCY.DataAccess;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.Utility;

namespace ZIT.EMERGENCY.DataAccess.Oracle
{
    class DBConnTest : IDBConnTest
    {
        public bool DBIsConnected()
        {
            bool bIsConnected = DbHelperOra.IsConnected(SysParameters.DBConnectString);
            return bIsConnected;
        }
        
    }
}
