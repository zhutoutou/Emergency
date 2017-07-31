using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.fnDataAccess.Oracle;
using ZIT.EMERGENCY.Utility;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle.SQL
{
    public class DataExchangeDataAccessSelectSql
    {
        
        /// <summary>
        /// 获取当日急救信息是否已经存在
        /// </summary>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public bool GetBoolHave_ALARM_EVENT(string Datatime)
        {
            bool Have_ALARM_EVENT = false;
            try
            {
                string sql = "select Datatime from ALARM_EVENT_INFO where DataTime ='" +  Datatime + "'";
                object obj = DB120Helpcle.GetSingle(sql);
                if (obj != null)
                {
                    Have_ALARM_EVENT = true;
                }
            }
            catch {}
            return Have_ALARM_EVENT;
        }

        /// <summary>
        /// 获取当日急救信息是否已经存在
        /// </summary>
        /// <param name="Datetime"></param>
        /// <returns></returns>
        public bool GetBoolHaveVeh(string VEHICLECARD)
        {
            bool HaveVeh = false;
            try
            {
                string sql = "select VEHICLECARD from VEHICLEREALSTATUS where VEHICLECARD ='" + VEHICLECARD + "'";
                object obj = DB120Helpcle.GetSingle(sql);
                if (obj != null)
                {
                    HaveVeh = true;
                }
            }
            catch {}
            return HaveVeh;
        }
    
    }
}
