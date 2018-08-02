using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LoyalFilial.Common
{
    public class SerializeHelper
    {
        #region json
        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="t">实际对象</param>
        /// <returns>返回json字符串</returns>
        public static string ObjectToJson<T>(T t)
        {
            return DealJosnNum(Newtonsoft.Json.JsonConvert.SerializeObject(t));
        }

        /// <summary>
        /// 从一个Json串生成对象信息
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>实际对象</returns>
        public static T JsonToObject<T>(string jsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="t">实际对象</param>
        /// <returns>返回json字符串</returns>
        public static string ObjectToJsonForTimeFormat<T>(T t)
        {
            var timeFormat = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return DealJosnNum(Newtonsoft.Json.JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, timeFormat));
        }

        /// <summary>
        /// replace the keywords of null in the Json string with "null"
        /// </summary>
        /// <param name="TempString">the Josn parameter</param>
        /// <returns>return the json string</returns>
        public static string DealJosnNum(string TempString)
        {
            return System.Text.RegularExpressions.Regex.Replace(TempString, @":null", ":\"null\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        #endregion

        #region xml
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object XmlToObject(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static object XmlToObject(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ObjectToXml(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion
        #endregion

        /// <summary>
        /// Bind Page States Name
        /// </summary>
        /// <param name="source"></param>
        /// <param name="descStrings"></param>
        /// <returns></returns>
        public static string StateChanger(string source, string[] descStrings)
        {
            for (int i = 0; i < descStrings.Length; i++)
            {
                string condition = "\"State\":" + i;
                string target = "\"State\":" + "\"" + descStrings[i] + "\"";
                source = source.Replace(condition, target);
            }
            return source;
        }

        /// <summary>
        /// 改变显示名称
        /// </summary>
        /// <param name="source">原json</param>
        /// <param name="descStrings">要改变的内容</param>
        /// <param name="conditionName">字段名称</param>
        /// <param name="sequence">序列开始默认是0</param>
        /// <param name="blankValue">给空数据一个默认填充值</param>
        /// <returns></returns>
        public static string ColumnRename(string source, string[] descStrings, string conditionName, int sequence = 0, string blankValue = null)
        {
            int temp = 0;
            for (int i = sequence; i < descStrings.Length + sequence; i++)
            {

                string condition = "\"" + conditionName + "\":" + i;
                string target = "\"" + conditionName + "\":" + "\"" + descStrings[temp] + "\"";
                source = source.Replace(condition, target);
                temp++;
            }

            var c1 = "\"" + conditionName + "\":\"null\"";
            var t = "\"" + conditionName + "\":\" \"";

            if (!string.IsNullOrEmpty(blankValue))
            {
                t = "\"" + conditionName + "\":\"" + blankValue + " \"";
            }
            source = source.Replace(c1, t);
            return source;
        }

        /// <summary>
        /// 将一个object对象序列化，返回一个byte[]
        /// </summary>
        /// <param name="obj">能序列化的对象</param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /**/
        /// <summary>
        /// 将一个序列化后的byte[]数组还原
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        public static object Clone(object obj)
        {

            BinaryFormatter bf = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, obj);

            ms.Seek(0, SeekOrigin.Begin);

            return bf.Deserialize(ms);

        }
    }
}
