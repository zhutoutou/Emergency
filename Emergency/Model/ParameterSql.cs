using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

namespace ZIT.EMERGENCY.Model
{
    public class ParameterSql
    {
        /// <summary>
        /// Sql语句
        /// </summary>
        public string StrSql { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public OracleParameter[] OrclPar { get; set; }
    }
}
