using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using ZIT.LOG;
using ZIT.EMERGENCY.Utility;
using ZIT.EMERGENCY.Model;
using ZIT.EMERGENCY.fnDataAccess.Oracle.SQL;

namespace ZIT.EMERGENCY.fnDataAccess.Oracle
{
    public class GetDataAccess : IGetDataAccess
    {
        public string strConnLocalDB = SysParameters.DBConnectStringLocal;

        /// <summary>
        /// 获取急救数据信息
        /// </summary>
        /// <returns></returns>
        public List<ALARM_EVENT_INFO> getNewEventInfo()
        {
            try
            {
                List<ALARM_EVENT_INFO> list = new List<ALARM_EVENT_INFO>();
                DataTable dt = DB120Help.GetRecord(GetDataSql.GetALARMDataStr());
                foreach (DataRow r in dt.Rows)
                {
                    try
                    {
                        ALARM_EVENT_INFO aci = new ALARM_EVENT_INFO();

                        aci.CALLCOUNT = int.Parse(r["CALLCOUNT"].ToString());
                        aci.DEALCOUNT = int.Parse(r["DEALCOUNT"].ToString());
                        aci.DISPATCHCOUNT = int.Parse(r["DISPATCHCOUNT"].ToString());
                        aci.PAIENTCOUNT = int.Parse(r["PAIENTCOUNT"].ToString());
                        aci.LASTTIME = DateTime.Now;
                        aci.DATATIME = r["DATATIME"].ToString();
                        aci.READFLAG = 1;
                        list.Add(aci);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return list;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取实时的车辆信息
        /// </summary>
        /// <returns></returns>
        public List<VEHICLEREALSTATUS> getNewSSVehInfo()
        { 
            try
            {
            List<VEHICLEREALSTATUS> list = new List<VEHICLEREALSTATUS>();
                DataTable dt = DB120Help.GetRecord(GetDataSql.GetSSVehDataStr());
                foreach (DataRow r in dt.Rows)
                {
                    try
                    {
                        VEHICLEREALSTATUS aci = new VEHICLEREALSTATUS();

                        aci.VEHICLENAME = r["VEHICLENAME"].ToString();
                        aci.VEHICLECARD = r["VEHICLECARD"].ToString();
                        aci.VEHICLEDEPARTMENT = r["VEHICLEDEPARTMENT"].ToString();
                        aci.STATUS = r["STATUS"].ToString();
                        aci.JD = float.Parse(r["JD"].ToString());
                        aci.WD = float.Parse(r["WD"].ToString());
                        aci.LASTTIME = DateTime.Now;
                        aci.READFLAG = 1;
                        list.Add(aci);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return list;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取历史的车辆信息
        /// </summary>
        /// <returns></returns>
        public List<VEHICLEHISTROYSTATE> getNewLSVehInfo(string strID,string strLSH,string strCCXH)
        {
            int nCCXH=0;
            try
            {
                List<VEHICLEHISTROYSTATE> list = new List<VEHICLEHISTROYSTATE>();
                if (strID == "")
                {
                    return list;
                }
                if (strLSH != "")
                {
                    if (strCCXH != "")
                    {
                        int.TryParse(strCCXH, out nCCXH);
                    }
                    else
                    {
                        int.TryParse((DB120Help.GetSingle( GetDataSql.GetCCXH(strLSH, strID)) == null ? "0" : DB120Help.GetSingle( GetDataSql.GetCCXH(strLSH, strID)).ToString()), out nCCXH);
                    }
                }
                
                DataTable dt = DB120Help.GetRecord(GetDataSql.GetLSVehDataStr(strID));
                foreach (DataRow r in dt.Rows)
                {
                    try
                    {
                        VEHICLEHISTROYSTATE aci = new VEHICLEHISTROYSTATE();
                        aci.LSH = strLSH;
                        aci.CCXH = nCCXH;
                        aci.VEHICLENAME = r["VEHICLENAME"].ToString();
                        aci.VEHICLECARD = r["VEHICLECARD"].ToString();
                        aci.VEHICLEDEPARTMENT = r["VEHICLEDEPARTMENT"].ToString();

                        aci.JD = float.Parse(r["JD"].ToString());
                        aci.WD = float.Parse(r["WD"].ToString());
                        aci.REPORTTIME = DateTime.Now;
                        aci.READFLAG = 1;
                        list.Add(aci);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return list;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }
    }
}
