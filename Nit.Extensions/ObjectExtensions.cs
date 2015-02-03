using System;

namespace Nit.Extensions
{
    /// <summary>
    /// 摘要: object 的扩展方法类.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 摘要: 判断 object 是否为空。
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>bool</returns>
        public static bool IsNull(this object val)
        {
            return val == null;
        }
        /// <summary>
        /// 摘要: 判断 object 是否为整数。
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>bool</returns>
        public static bool IsInt(this object val)
        {
            int result = 0;
            return int.TryParse(val.ToString(), out result);
        }
        /// <summary>
        /// 摘要: 判断 object 是否为double。
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>bool</returns>
        public static bool IsDouble(this object val)
        {
            double result = 0;
            return double.TryParse(val.ToString(), out result);
        }
        /// <summary>
        /// 摘要: ToInt 方法.
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>int</returns>
        public static int ToInt(this object val)
        {
            int intData = 0;
            bool result = int.TryParse(val.ToString(), out intData);
            return intData;
        }
        /// <summary>
        /// 摘要: ToInt 方法,并提供默认返还值.
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <returns>int</returns>
        public static int ToInt(this object val, int defaultVal)
        {
            int result = defaultVal;
            int.TryParse(val.ToString(), out result);
            return result;
        }
        /// <summary>
        /// 摘要: ToDouble 方法.
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>double</returns>
        public static double ToDouble(this object val)
        {
            return double.Parse(val.ToString());
        }
        /// <summary>
        /// 摘要: ToDouble 方法,并提供默认返还值..
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <returns>double</returns>
        public static double ToDouble(this object val, double defaultVal)
        {
            double result = defaultVal;
            double.TryParse(val.ToString(), out result);
            return result;
        }
        /// <summary>
        /// 摘要: object转化为bool值.
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>bool</returns>
        public static bool ToBoolean(this object val)
        {
            return Convert.ToBoolean(val.ToString().Replace("0", "false").Replace("1", "true"));
        }
        /// <summary>
        /// 摘要: object转化为bool值.
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <returns>bool</returns>
        public static bool ToBoolean(this object val, bool defaultVal)
        {
            bool result = defaultVal;
            bool.TryParse(val.ToString(), out result);
            return result;
        }
        /// <summary>
        /// 摘要: 实例序列化为json字符串格式
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="val">实例</param>
        /// <returns>json</returns>
        public static string SerializeObject<T>(this T val)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(val);
        }
    }
}
