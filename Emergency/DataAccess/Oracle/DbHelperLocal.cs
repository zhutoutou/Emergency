using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using ZIT.EMERGENCY.Model;
using ZIT.LOG;

namespace ZIT.EMERGENCY.DataAccess.Oracle
{
    public class DbHelperLocal:DbHelperOra
    {
        public ResultInfo RunProcedure(string connstr, string storedProcName, IDataParameter[] parameters)
        {
            ResultInfo ri = new ResultInfo();
            try
            {
                using (OracleConnection connection = new OracleConnection(connstr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcName;
                    foreach (OracleParameter or in parameters)
                    {
                        command.Parameters.Add(or);
                    }
                    int i = command.ExecuteNonQuery();
                    ri.ret = int.Parse(command.Parameters["ret"].Value.ToString());
                    ri.msg = command.Parameters["msg"].Value.ToString();
                    connection.Close();


                }
            }
            catch(Exception ex)
            {
                ri = new ResultInfo();
                LogHelper.WriteLog("", ex);
            }
            return ri;
        }
    }
}
