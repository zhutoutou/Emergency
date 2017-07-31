using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using ZIT.EMERGENCY.Utility;

namespace ZIT.EMERGENCY.fnDataAccess
{
    public class DataAccess
    {
        /// <summary>
        /// 定义根程序集
        /// </summary>
        private static readonly string strAssemblyName = "DataAccess";
        private static readonly string strNameSpaceName = "ZIT.EMERGENCY.fnDataAccess";
        /// <summary>
        /// 读取数据库类型
        /// </summary>
        private static readonly string db = SysParameters.DBType; 
        /// <summary>
        /// 数据访问类:DBConnTest
        /// </summary>
        /// <returns></returns>
        public static IDBConnTest GetDBConnTestLocal()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBConnTestLocal";
            return (IDBConnTest)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        public static IDBConnTest GetDBConnTestRemote()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBConnTestRemote";
            return (IDBConnTest)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        public static IGetDataAccess GetDataAccess()
        {
            string strClassName = strNameSpaceName + "." + db + ".GetDataAccess";
            return (IGetDataAccess)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        public static IDataExChangeDataAccess GetDataExChangeDataAccess()
        {
            string strClassName = strNameSpaceName + "." + db + ".DataExChangeDataAccess";
            return (IDataExChangeDataAccess)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }
    }
}
