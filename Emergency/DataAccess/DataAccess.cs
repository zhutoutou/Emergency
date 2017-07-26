using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using ZIT.EMERGENCY.Utility;

namespace ZIT.EMERGENCY.DataAccess
{
    public class DataAccess
    {
        /// <summary>
        /// 定义根程序集
        /// </summary>
        private static readonly string strAssemblyName = "DataAccess";
        private static readonly string strNameSpaceName = "ZIT.EMERGENCY.DataAccess";
        /// <summary>
        /// 读取数据库类型
        /// </summary>
        private static readonly string db = SysParameters.DBType; 
        /// <summary>
        /// 数据访问类:DBConnTest
        /// </summary>
        /// <returns></returns>
        public static IDBConnTest GetDBConnTest()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBConnTest";
            return (IDBConnTest)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }



        public static IDBsendNewInfo GetDBsendNewInfo()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBsendNewInfo";
            return (IDBsendNewInfo)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }
    }
}
