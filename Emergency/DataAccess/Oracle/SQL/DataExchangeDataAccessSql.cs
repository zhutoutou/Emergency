using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ZIT.EMERGENCY.Model;
using System.Data.OracleClient;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle.SQL
{
    public class DataExchangeDataAccessSql
    {
        private static string ConvertDataTime(string Time)
        {
            string DataTime = "to_date('" + Time + "','yyyy-MM-dd hh24:mi:ss')";
            return DataTime;
        }

        public static ParameterSql GetDataExchangeDataAccessSql(ALARM_EVENT_INFO Data)
        {
            ParameterSql sqlpar = new ParameterSql();
            //20151210 修改人:朱星汉 修改内容:添加系统人员密码
            sqlpar.StrSql = "insert into ALARM_EVENT_INFO(DATATIME,CALLCOUNT,DEALCOUNT,DISPATCHCOUNT,PAIENTCOUNT,LASTTIME,READFLAG)values(:DATATIME,:CALLCOUNT,:DEALCOUNT,:DISPATCHCOUNT,:PAIENTCOUNT,:LASTTIME,:READFLAG)";
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
            sqlpar.StrSql = "insert into VEHICLEREALSTATUS(VEHICLECARD,VEHICLENAME,VEHICLEDEPARTMENT,STATUS,JD,WD,LASTTIME,READFLAG)values(:VEHICLECARD,:VEHICLENAME,:VEHICLEDEPARTMENT,:STATUS,:JD,:WD,:LASTTIME,:READFLAG)";
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
            sqlpar.StrSql = "insert into VEHICLEHISTROYSTATE(VEHICLECARD,VEHICLENAME,VEHICLEDEPARTMENT,LSH,CCXH,JD,WD,REPORTTIME,READFLAG)values(:VEHICLECARD,:VEHICLENAME,:VEHICLEDEPARTMENT,:LSH,:CCXH,:JD,:WD,:REPORTTIME,:READFLAG)";
            OracleParameter[] par ={new OracleParameter(":VEHICLECARD",GetString(Data.VEHICLECARD)),
                                    new OracleParameter(":VEHICLENAME",GetString(Data.VEHICLENAME)),
                                    new OracleParameter(":VEHICLEDEPARTMENT",GetString(Data.VEHICLEDEPARTMENT)),
                                    new OracleParameter(":LSH",GetString(Data.LSH)),
                                    new OracleParameter(":CCXH",GetNumber(Data.CCXH==0? "":Data.CCXH.ToString())),
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
            DateTime dt = new DateTime();
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
