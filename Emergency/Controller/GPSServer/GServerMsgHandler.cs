using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Utility;
using ZIT.EMERGENCY.Model;
using System.Text.RegularExpressions;
using ZIT.LOG;
using ZIT.EMERGENCY.fnDataAccess;

namespace ZIT.EMERGENCY.Controller.BusinessServer
{
    public class GServerMsgHandler
    {
        /// <summary>
        /// WebService代理类
        /// </summary>
        private IGetDataAccess getData;

        Dictionary<string, DateTime> _VehMap = new Dictionary<string, DateTime>();
        public GServerMsgHandler()
        {
            getData = DataAccess.GetDataAccess();
        }

        public void HandleMsg(string strMsg)
        {
            try
            {
                int m;
                string strMessageId = strMsg.Substring(1, 2);
                if (int.TryParse(strMessageId,out m))
                {
                    LogHelper.WriteNetMsgLog("Recieve GServer message:" + strMsg);
                }
                switch (strMessageId)
                {
                    //case "70":
                    //    Handle70Message(strMsg);
                    //    break;
                    case "40":
                        Handle40Message(strMsg);
                        break;
                    default:
                    break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
        #region
        //private void Handle70Message(string strMsg)
        //{
        //    string strcarID, strLSH, strCCXH;
        //    if (strMsg.Substring(0, 1) == "(" && strMsg.Substring(strMsg.Length - 1, 1) == ")")
        //    {
        //        strcarID = GetValueByKey(strMsg, "ID");
        //        strLSH = GetValueByKey(strMsg, "LSH");
        //        if (strcarID != "")
        //        {
        //            if (strLSH.Length == 21)
        //            {
        //                strCCXH = strLSH.Substring(strLSH.Length - 2, 2);
        //                strLSH = strLSH.Substring(0, strLSH.Length - 2);
        //                VehInfo vi = new VehInfo(strLSH, strCCXH);
        //                if (_VehMap.ContainsKey(strcarID))
        //                {
        //                    _VehMap[strcarID] = vi;
        //                }
        //                else
        //                {
        //                    _VehMap.Add(strcarID, vi);
        //                }
        //            }
        //            else if (strLSH.Length == 19)
        //            {
        //                if (_VehMap.ContainsKey(strcarID))
        //                {
        //                    if (_VehMap[strcarID].LSH != strLSH)
        //                    {
        //                        VehInfo vi = new VehInfo(strLSH, "");
        //                        _VehMap[strcarID] = vi;
        //                    }
        //                }
        //                else
        //                {
        //                    VehInfo vi = new VehInfo(strLSH, "");
        //                    _VehMap.Add(strcarID, vi);
        //                }
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //    }
        //}

        private void Handle40Message(string strMsg)
        {
            string strcarID, strLSH, strCCXH;
            try
            {
                strcarID = GetValueByKey(strMsg, "ID");
                strLSH = GetValueByKey(strMsg, "LSH");
                if (_VehMap.ContainsKey(strcarID))
                {
                    TimeSpan ts = DateTime.Now - _VehMap[strcarID];
                    if (ts.TotalMinutes < SysParameters.VehInfoInterval)
                    {
                        return;
                    }
                }
                if (strMsg.Substring(0, 1) == "(" && strMsg.Substring(strMsg.Length - 1, 1) == ")")
                {
                    
                    switch (strLSH.Length)
                    {
                        case 12:
                        case 19:
                            strCCXH = "";
                            break;
                        case 14:
                        case 21:
                                strCCXH = strLSH.Substring(strLSH.Length - 2, 2);
                                strLSH = strLSH.Substring(0, strLSH.Length - 2);
                            break;
                        default :
                            strLSH = "";
                            strCCXH = "";
                            break;
                    }
                    List<VEHICLEHISTROYSTATE> aci=getData.getNewLSVehInfo(strcarID,strLSH,strCCXH);
                    if (aci.Count > 0)
                    {
                        IDataExChangeDataAccess Data = DataAccess.GetDataExChangeDataAccess();
                        Data.insertNewLSVehInfo(aci);
                        if (_VehMap.ContainsKey(strcarID))
                        {
                            _VehMap[strcarID] = DateTime.Now;
                        }
                        else
                        {
                            _VehMap.Add(strcarID, DateTime.Now);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog("",ex);
            }
        }
        #endregion







        private string GetValueByKey(string message, string key)
        {
            string strReturn = "";

            Regex reg = new Regex(key + ":(.*?)\\%");
            if (reg.IsMatch(message))
            {
                Match match = reg.Match(message);
                strReturn = match.Groups[1].Value.Trim();
            }
            return strReturn;
        }


       
    }

}
