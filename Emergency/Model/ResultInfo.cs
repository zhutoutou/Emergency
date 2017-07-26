using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.EMERGENCY.Model
{
    public class ResultInfo
    {
        public int ret { get; set; }
        public string msg { get; set; }

        public ResultInfo()
        { }

        public ResultInfo(int _ret, string _msg)
        {
            ret = _ret;
            msg = _msg;
        }
    }
}
