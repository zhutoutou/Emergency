using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Model;

namespace ZIT.EMERGENCY.DataAccess
{
    public interface IDBConnTest
    {
        bool DBIsConnected();
    }
}
