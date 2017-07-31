using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.fnDataAccess.Oracle.SQL;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.Utility;
using ZIT.LOG;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle
{
    public class DataExChangeDataAccess:IDataExChangeDataAccess
    {
        public void insertNewEventInfo(List<ALARM_EVENT_INFO> aci)
        {
            if (aci.Count > 0)
            {
                foreach (ALARM_EVENT_INFO item in aci)
                {
                    try
                    {
                        DataExchangeDataAccessSelectSql SelSql = new DataExchangeDataAccessSelectSql();

                        bool Have_ALARM_EVENT = SelSql.GetBoolHave_ALARM_EVENT( item.DATATIME);
                        LogHelper.WriteLog("ALARM_EVENT_INFO开始" + Have_ALARM_EVENT + "," + item.DATATIME);
                        if (Have_ALARM_EVENT == true)
                        {
                            ParameterSql parSql = DataExchangeDataAccessUpdateSql.GetDataExchangeDataAccessSql(item);
                            int i = DB120Helpcle.ExecuteSql( parSql.StrSql, parSql.OrclPar);
                            if (i > 0)
                            {
                                LogHelper.WriteLog("ALARM_EVENT_INFO更新数据成功。");
                            }
                            else
                            {
                                LogHelper.WriteLog("ALARM_EVENT_INFO更新数据失败。");
                            }
                        }
                        else
                        {
                            ParameterSql parSql = DataExchangeDataAccessSql.GetDataExchangeDataAccessSql(item);
                            int i = DB120Helpcle.ExecuteSql( parSql.StrSql, parSql.OrclPar);
                            if (i > 0)
                            {
                                LogHelper.WriteLog("ALARM_EVENT_INFO插入数据成功。");
                            }
                            else
                            {
                                LogHelper.WriteLog("ALARM_EVENT_INFO插入数据成功。");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                }
            }
        }

        public void insertNewSSVehInfo(List<VEHICLEREALSTATUS> aci)
        {
            if (aci.Count > 0)
            {
                foreach (VEHICLEREALSTATUS item in aci)
                {
                    try
                    {
                        DataExchangeDataAccessSelectSql SelSql = new DataExchangeDataAccessSelectSql();

                        bool HaveVeh = SelSql.GetBoolHaveVeh(item.VEHICLECARD);
                        LogHelper.WriteLog("VEHICLEREALSTATUS开始" + HaveVeh + "," + item.VEHICLECARD);
                        if (HaveVeh == true)
                        {
                            ParameterSql parSql = DataExchangeDataAccessUpdateSql.GetDataExchangeDataAccessSql(item);
                            int i = DB120Helpcle.ExecuteSql( parSql.StrSql, parSql.OrclPar);
                            if (i > 0)
                            {
                                LogHelper.WriteLog("VEHICLEREALSTATUS更新数据成功。");
                            }
                            else
                            {
                                LogHelper.WriteLog("VEHICLEREALSTATUS更新数据失败。");
                            }
                        }
                        else
                        {
                            ParameterSql parSql = DataExchangeDataAccessSql.GetDataExchangeDataAccessSql(item);
                            int i = DB120Helpcle.ExecuteSql( parSql.StrSql, parSql.OrclPar);
                            if (i > 0)
                            {
                                LogHelper.WriteLog("VEHICLEREALSTATUS插入数据成功。");
                            }
                            else
                            {
                                LogHelper.WriteLog("VEHICLEREALSTATUS插入数据成功。");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                }
            }
        }

        public void insertNewLSVehInfo(List<VEHICLEHISTROYSTATE> aci)
        {
            if (aci.Count > 0)
            {
                foreach (VEHICLEHISTROYSTATE item in aci)
                {
                    try
                    {
                        DataExchangeDataAccessSelectSql SelSql = new DataExchangeDataAccessSelectSql();

                        ParameterSql parSql = DataExchangeDataAccessSql.GetDataExchangeDataAccessSql(item);
                        int i = DB120Helpcle.ExecuteSql( parSql.StrSql, parSql.OrclPar);
                        if (i > 0)
                        {
                            LogHelper.WriteLog("VEHICLEREALSTATUS插入数据成功。");
                        }
                        else
                        {
                            LogHelper.WriteLog("VEHICLEREALSTATUS插入数据成功。");
                        }

                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                }
            }
        }
    }
}
