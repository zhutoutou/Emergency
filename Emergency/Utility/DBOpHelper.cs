using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.EMERGENCY.Utility
{
    public class DBOpHelper
    {

        /// <summary>
        /// 获取时间时若为空时返回DBnull
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取字符串时若为空时返回DBnull
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
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

        /// <summary>
        /// 获取数字时若为空时返回DBnull
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
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
