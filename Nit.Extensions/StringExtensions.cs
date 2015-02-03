using System;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

namespace Nit.Extensions
{
    /// <summary>
    /// 摘要: string 的扩展方法.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 摘要: ToInt 方法.
        /// </summary>
        /// <param name="val">objectData</param>
        /// <returns>int</returns>
        public static int ToInt(this string val)
        {
            return int.Parse(val);
        }
        /// <summary>
        /// 摘要: ToInt 方法,并提供默认返还值.
        /// </summary>
        /// <param name="val">objectData</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <returns>int</returns>
        public static int ToInt(this string val, int defaultVal)
        {
            int result = defaultVal;
            int.TryParse(val, out result);
            return result;
        }
        /// <summary>
        /// 摘要: ToDouble 方法.
        /// </summary>
        /// <param name="val">objectData</param>
        /// <returns>double</returns>
        public static double ToDouble(this string val)
        {
            return double.Parse(val);
        }
        /// <summary>
        /// 摘要: ToDouble 方法,并提供默认返还值..
        /// </summary>
        /// <param name="val">objectData</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <returns>double</returns>
        public static double ToDouble(this string val, double defaultVal)
        {
            double result = defaultVal;
            double.TryParse(val, out result);
            return result;
        }
        /// <summary>
        /// 摘要: ToFloat方法.
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>float</returns>
        public static float ToFloat(this string val)
        {
            return float.Parse(val);
        }
        /// <summary>
        /// 摘要: ToFloat方法,如果执行失败返回指定float值.
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <returns></returns>
        public static float ToFloat(this string val, float defaultVal)
        {
            float result = defaultVal;
            float.TryParse(val, out result);
            return result;
        }
        /// <summary>
        /// 摘要: 判断 string 是否为空。
        /// </summary>
        /// <param name="val">val</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }
        /// <summary>
        /// 摘要:指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="val">value</param>
        /// <returns>如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。</returns>
        public static bool IsNullOrWhiteSpace(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }
        /// <summary>
        /// 摘要: 判断 string 是否为整数。
        /// </summary>
        /// <param name="val">objectData</param>
        /// <returns>bool</returns>
        public static bool IsInt(this string val)
        {
            int result = 0;
            return int.TryParse(val, out result);
        }
        /// <summary>
        /// 摘要: 判断 string 是否为double。
        /// </summary>
        /// <param name="val">objectData</param>
        /// <returns>bool</returns>
        public static bool IsDouble(this string val)
        {
            double result = 0;
            return double.TryParse(val, out result);
        }
        /// <summary>
        /// 摘要: 字符串转化为bool值.
        /// </summary>
        /// <param name="val">objectData</param>
        /// <returns>bool</returns>
        public static bool ToBool(this string val)
        {
            return bool.Parse(val.Replace("0", "false").Replace("1", "true"));
        }
        /// <summary>
        /// 摘要: 字符串转化为bool值.
        /// </summary>
        /// <param name="val">objectData</param>
        /// <param name="preval">preval</param>
        /// <returns>bool</returns>
        public static bool ToBool(this string val, bool preval)
        {
            bool result = preval;
            bool.TryParse(val, out result);
            return result;
        }

        /// <summary>
        /// 摘要：将字符串日期格式化
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="fomatString">fomatString</param>
        /// <returns>result</returns>
        public static string ToDate(this string val, string fomatString)
        {
            return Convert.ToDateTime(val).ToString(fomatString);
        }
        /// <summary>
        /// 摘要：将xml转换为DataTable
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DataTable XmlToDataTable(this string val)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(val);
            return dataSet.Tables[0];
        }
        /// <summary>
        /// 摘要：使用指定字符编码，将字符串转换为Byte数组(默认GBK编码)
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="encodeName">编码名称，例如：GBK，UTF8等</param>
        /// <returns></returns>
        public static byte[] ToByte(this string val, string encodeName = "GBK")
        {
            return Encoding.GetEncoding(encodeName).GetBytes(val);
        }
        /// <summary>
        /// 摘要：将字符串经过指定编码集，进行md5加密啊
        /// </summary>
        /// <param name="val">val</param>
        /// <param name="encodeName">encodeName</param>
        /// <returns></returns>
        public static string ToMD5(this string val, string encodeName = "GBK")
        {
            string result = string.Empty;
            MD5CryptoServiceProvider MD5CSP = new MD5CryptoServiceProvider();
            byte[] targets = MD5CSP.ComputeHash(Encoding.GetEncoding(encodeName).GetBytes(val));
            foreach (byte b in targets)
            {
                result += b.ToString("x2");
            }
            return result.ToUpper();
        }
        /// <summary>
        /// 摘要: 从右侧起获取字符串指定长度.
        /// </summary>
        /// <param name="val">默认字符串</param>
        /// <param name="length">长度</param>
        /// <returns>返回值</returns>
        public static string Right(this string val, int length)
        {
            return val.Substring(val.Length - length, length);
        }
        /// <summary>
        /// 摘要: 从左侧起获取字符串指定长度(等同于Substring(int length)方法).
        /// </summary>
        /// <param name="val">默认字符串</param>
        /// <param name="length">长度</param>
        /// <returns>返回值</returns>
        public static string Left(this string val, int length)
        {
            return val.Substring(length);
        }
        /// <summary>
        /// 摘要：json字符串反序列化为T对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="json">json字符串原数据</param>
        /// <returns>实例结果</returns>
        public static T DeserializeObject<T>(this string json)
        {
            return (T)JsonConvert.DeserializeObject(json, typeof(T));
        }
    }
}