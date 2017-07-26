using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Model;
using System.Data.OracleClient;
using System.Data;
using ZIT.EMERGENCY.Utility;
using ZIT.LOG;

namespace ZIT.EMERGENCY.DataAccess.Oracle
{
    public class DBsendNewInfo:IDBsendNewInfo
    {
        DbHelperLocal db = new DbHelperLocal();
        public string strConnDB = SysParameters.DBConnectString;
        
        public void sendNewEventInfo()
        {
            ResultInfo ri = new ResultInfo();   
            
            OracleParameter ret=new OracleParameter("ret",OracleType.Number);
            OracleParameter msg =new OracleParameter("msg",OracleType.VarChar);
            ret.Direction = ParameterDirection.Output;
            msg.Direction = ParameterDirection.Output;
            msg.Size = 100;
            OracleParameter[] paramters={
                                            ret,
                                            msg,
                                        };

            ri = db.RunProcedure(strConnDB, "PROCALARM_EVENT", paramters);
            if (ri == null)
            {
                LogHelper.WriteLog("ALARM_EVENT_INFO出现异常。");
            }
            else if (ri.ret == 1)
            {
                LogHelper.WriteLog("ALARM_EVENT_INFO成功。");
            }
            else 
            {
                LogHelper.WriteLog("ALARM_EVENT_INFO失败-" + ri.msg);
            }
        }

        public void sendNewSSVehInfo()
        {
            ResultInfo ri = new ResultInfo();

            OracleParameter ret = new OracleParameter("ret", OracleType.Number);
            OracleParameter msg = new OracleParameter("msg", OracleType.VarChar);
            ret.Direction = ParameterDirection.Output;
            msg.Direction = ParameterDirection.Output;
            msg.Size = 100;
            OracleParameter[] paramters ={
                                            ret,
                                            msg,
                                        };

            ri = db.RunProcedure(strConnDB, "PROCalarm_vehiclerealstatus", paramters);
            if (ri == null)
            {
                LogHelper.WriteLog("PROCalarm_vehiclerealstatus出现异常。");
            }
            else if (ri.ret == 1)
            {
                LogHelper.WriteLog("PROCalarm_vehiclerealstatus成功。");
            }
            else
            {
                LogHelper.WriteLog("PROCalarm_vehiclerealstatus失败-" + ri.msg);
            } 

        }

        public void sendNewLSVehInfo(string strcarID, string strLSH, string strCCXH)
        {
            ResultInfo ri = new ResultInfo();
            OracleParameter vc_clID = new OracleParameter("vc_clID", OracleType.VarChar, 100);
            OracleParameter vc_lsh = new OracleParameter("vc_lsh", OracleType.VarChar, 100);
            OracleParameter vc_ccxh = new OracleParameter("vc_ccxh", OracleType.VarChar, 100);
            OracleParameter ret = new OracleParameter("ret", OracleType.Number);
            OracleParameter msg = new OracleParameter("msg", OracleType.VarChar);
            vc_clID.Value = strcarID;
            vc_lsh.Value = strLSH;
            vc_ccxh.Value = strCCXH;
            ret.Direction = ParameterDirection.Output;
            msg.Direction = ParameterDirection.Output;
            vc_clID.Size = 100;
            vc_lsh.Size = 100;
            vc_ccxh.Size = 100;
            msg.Size = 100;
            OracleParameter[] paramters ={
                                            vc_clID,
                                            vc_lsh,
                                            vc_ccxh,
                                            ret,
                                            msg,
                                        };

            ri = db.RunProcedure(strConnDB, "PROCalarm_vehiclehistroystate", paramters);
            if (ri == null)
            {
                LogHelper.WriteLog("PROCalarm_vehiclehistroystate出现异常。");
            }
            else if (ri.ret == 1)
            {
                LogHelper.WriteLog("PROCalarm_vehiclehistroystate成功。");
            }
            else
            {
                LogHelper.WriteLog("PROCalarm_vehiclehistroystate失败-" + ri.msg);
            }
        }

        public Dictionary<string, VehInfo> getInitVehMap() 
        {
            Dictionary<string, VehInfo> vehMap = new Dictionary<string, VehInfo>();
            try 
            {
                string strcarid;
                string strSQL = "select cl.id as carid,dq.lsh,cc.cs as ccxh from clxxb cl,dqclztxxb dq,ccxxb cc where cl.id = dq.id and cc.lsh = dq.lsh and cc.clid = cl.id";
                DataTable dt = DbHelperOra.GetRecord(strConnDB,strSQL);
                foreach (DataRow r in dt.Rows)
                {
                    try
                    {
                        VehInfo vi = new VehInfo();
                        vi.LSH = r["LSH"].ToString();
                        vi.CCXH = r["CCXH"].ToString();
                        strcarid = r["CarID"].ToString();
                        vehMap.Add(strcarid, vi);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("", e);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
            return vehMap;
        }
    }
}
