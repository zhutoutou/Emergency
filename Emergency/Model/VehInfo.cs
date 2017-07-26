using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.EMERGENCY.Model
{
    public class VehInfo
    {
        public string LSH { get; set; }

        public string CCXH { get; set; }


        public VehInfo()
        { }

        public VehInfo(string _LSH, string _CCXH)
        {
            LSH = _LSH;
            CCXH = _CCXH;
        }
    }
}
