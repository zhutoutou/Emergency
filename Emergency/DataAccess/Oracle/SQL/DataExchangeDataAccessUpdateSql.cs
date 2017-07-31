using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ZIT.EMERGENCY.Model;
using System.Data.OracleClient;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle.SQL
{
    /// <summary>
    /// 更新语句
    /// </summary>
    public class DataExchangeDataAccessUpdateSql
    {
       
        /// <summary>
        /// 车辆信息表
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ParameterSql GetDataExchangeDataAccessSql(ALARM_EVENT_INFO Data)
        {
            ParameterSql sqlpar = new ParameterSql();
            //20151210 修改人:朱星汉 修改内容:添加系统人员密码
            sqlpar.StrSql = "update ALARM_EVENT_INFO set CALLCOUNT=:CALLCOUNT,DEALCOUNT=:DEALCOUNT,DISPATCHCOUNT=:DISPATCHCOUNT,PAIENTCOUNT=:PAIENTCOUNT,LASTTIME=:LASTTIME,READFLAG=:READFLAG where DATATIME=:DATATIME";
            OracleParameter[] par ={new OracleParameter(":DATATIME",GetString(Data.DATATIME)),
                                    new OracleParameter(":CALLCOUNT",Data.CALLCOUNT),
                                    new OracleParameter(":DEALCOUNT",Data.DEALCOUNT),
                                    new OracleParameter(":DISPATCHCOUNT",Data.DISPATCHCOUNT),
                                    new OracleParameter(":PAIENTCOUNT",Data.PAIENTCOUNT),
                                    new OracleParameter(":LASTTIME",GetDateTime(Data.LASTTIME.ToString())),
                                    new OracleParameter(":READFLAG",Data.READFLAG), 
                                    };
            sqlpar.OrclPar = par;
            return sqlpar;

        }
        public static ParameterSql GetDataExchangeDataAccessSql(VEHICLEREALSTATUS Data)
        {
            ParameterSql sqlpar = new ParameterSql();
            //20151210 修改人:朱星汉 修改内容:添加系统人员密码
            sqlpar.StrSql = "update VEHICLEREALSTATUS set VEHICLECARD=:VEHICLECARD,VEHICLEDEPARTMENT=:VEHICLEDEPARTMENT,STATUS=:STATUS,JD=:JD,WD=:WD,LASTTIME=:LASTTIME,READFLAG=:READFLAG where VEHICLENAME=:VEHICLENAME";
            OracleParameter[] par ={new OracleParameter(":VEHICLECARD",GetString(Data.VEHICLECARD)),
                                    new OracleParameter(":VEHICLENAME",GetString(Data.VEHICLENAME)),
                                    new OracleParameter(":VEHICLEDEPARTMENT",GetString(Data.VEHICLEDEPARTMENT)),
                                    new OracleParameter(":STATUS",GetString(Data.STATUS)),
                                    new OracleParameter(":JD",Data.JD),
                                    new OracleParameter(":WD",Data.WD),
                                    new OracleParameter(":LASTTIME",GetDateTime(Data.LASTTIME.ToString())),
                                    new OracleParameter(":READFLAG",Data.READFLAG), 
                                    };
            sqlpar.OrclPar = par;
            return sqlpar;

        }

        public static ParameterSql GetDataExchangeDataAccessSql(VEHICLEHISTROYSTATE Data)
        {
            ParameterSql sqlpar = new ParameterSql();
            //20151210 修改人:朱星汉 修改内容:添加系统人员密码
            sqlpar.StrSql = "update VEHICLEHISTROYSTATE set VEHICLENAME:=VEHICLENAME,VEHICLEDEPARTMENT:=VEHICLEDEPARTMENT,LSH:=LSH,CCXH:=CCXH,JD:=JD,WD:=WD,REPORTTIME=:REPORTTIME,READFLAG=:READFLAG where VEHICLECARD=:VEHICLECARD";
            OracleParameter[] par ={new OracleParameter(":VEHICLECARD",GetString(Data.VEHICLECARD)),
                                    new OracleParameter(":VEHICLENAME",GetString(Data.VEHICLENAME)),
                                    new OracleParameter(":VEHICLEDEPARTMENT",GetString(Data.VEHICLEDEPARTMENT)),
                                    new OracleParameter(":LSH",GetString(Data.LSH)),
                                    new OracleParameter(":CCXH",Data.CCXH),
                                    new OracleParameter(":JD",Data.JD),
                                    new OracleParameter(":WD",Data.WD),
                                    new OracleParameter(":REPORTTIME",GetDateTime(Data.REPORTTIME.ToString())),
                                    new OracleParameter(":READFLAG",Data.READFLAG), 
                                    };
            sqlpar.OrclPar = par;
            return sqlpar;

        }
        //20151211 修改人:朱星汉 修改内容:获取时间时若为空时返回DBnull
        public static object GetDateTime(string Time)
        {
            DateTime? dt = null;
            object dtempty = DBNull.Value;
            try
            {
                if (string.IsNullOrEmpty(Time))
                {
                    return dtempty;

                }
                else
                {
                    dt = (DateTime)Convert.ToDateTime(Time);
                    return dt;
                }
            }
            catch
            {
                return dtempty;
            }
        }

        //20151211 修改人:朱星汉 修改内容:获取字符串时若为空时返回DBnull
        public static object GetString(string name)
        {
            object dtempty = DBNull.Value;
            string str = "";
            if (string.IsNullOrEmpty(name))
            {
                return dtempty;
            }
            else
            {
                str = name;
                return str;
            }
        }

        //20151211 修改人:朱星汉 修改内容:获取字符串时若为空时返回DBnull
        public static object GetNumber(string number)
        {
            object dtempty = DBNull.Value;
            double Double = 0;
            try
            {
                if (string.IsNullOrEmpty(number))
                {
                    return dtempty;
                }
                else
                {
                    Double = double.Parse(number);
                    return Double;              
                }
            }
            catch
            {
                return dtempty;
            }
        }

        
    }
}
