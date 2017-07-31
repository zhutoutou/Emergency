using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ZIT.EMERGENCY.Model;
namespace ZIT.EMERGENCY.fnDataAccess.Oracle.SQL
{
    internal class GetDataSql
    {
        /// <summary>
        /// 获取急救数据信息
        /// </summary>
        /// <returns></returns>
        public static string GetALARMDataStr()
        {
            string strSql = "select * from v_alarm_event";
            return strSql;
        }
        /// <summary>
        /// 获取实时的车辆信息
        /// </summary>
        /// <returns></returns>
        public static string GetSSVehDataStr()
        {
            string strSql = "select * from v_alarm_vehiclerealstatus";
            return strSql;
        }

        /// <summary>
        /// 获取历史的车辆信息
        /// </summary>
        /// <returns></returns>
        public static string GetLSVehDataStr(string strID)
        {
            string strSql = @"select vehiclename,vehiclecard,vehicledepartment,status,jd,
                wd from v_alarm_vehiclerealstatus
                where vehiclename = '" + strID + "' and rownum=1";
            return strSql;
        }

        public static string GetCCXH(string strLSH,string strID)
        {
            string strSql = "select max(cs) from ccxxb where lsh='" + strLSH + "' and clid = '" + strID + "'";
            return strSql;
        }
        
    }
}
