using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Common
{
    /// <summary>
    /// 请求帮助类
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// web请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="buffer">参数</param>
        /// <param name="encode">编码</param>
        /// <param name="timeOut">请求超时时间（秒）</param>
        /// <returns>响应字符串</returns>
        public static string Request(string url, string method, string contentType, byte[] buffer, Encoding encode = null, int? timeOut = null)
        {
            return Request(url, method, contentType, new List<byte[]> { buffer }, encode);
        }


        /// <summary>
        /// web请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="buffers">参数</param>
        /// <param name="encode">编码</param>
        /// <param name="timeOut">请求超时时间（秒）</param>
        /// <returns>响应字符串</returns>
        public static string Request(string url, string method, string contentType, List<byte[]> buffers, Encoding encode = null, int? timeOut = null)
        {
            encode = encode ?? new UTF8Encoding();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = contentType;
            if (timeOut != null && timeOut.HasValue)
                request.Timeout = timeOut.Value * 1000;
            request.ContentLength = GetListByteLengh(buffers);
            Stream outStream = request.GetRequestStream();
            foreach (var buffer in buffers)
            {
                outStream.Write(buffer, 0, buffer.Length);
            }

            outStream.Close();
            //接收HTTP做出的响应
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, encode);
            Char[] read = new Char[256];
            int count = readStream.Read(read, 0, 256);
            string responseString = null;
            while (count > 0)
            {
                responseString += new String(read, 0, count);
                count = readStream.Read(read, 0, 256);
            }
            readStream.Close();
            response.Close();
            return responseString;
        }

        /// <summary>
        /// web请求(GET方式)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string RequestGet(string url, string contentType = "application/json")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = contentType;
            var response = request.GetResponse() as HttpWebResponse;
            var streamReader = new StreamReader(response.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            response.Close();
            streamReader.Close();
            return responseContent;
        }

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="buffers">字节数组</param>
        /// <returns>字节长度</returns>
        private static long GetListByteLengh(List<byte[]> buffers)
        {
            if (buffers != null && buffers.Count > 0)
            {
                return buffers.Sum(b => b.Length);
            }
            return default(long);
        }
    }
}
