using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Common
{
    public class SecurityHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns>时间戳</returns>
        public static long GetUnixtime()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">加密源</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptionMD5(string source)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }
        /// <summary>
        /// get web client ip
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {

            string userIP = "未获取用户IP";

            try
            {
                if (System.Web.HttpContext.Current == null
                    || System.Web.HttpContext.Current.Request == null
                    || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                //CDN加速后取到的IP simone 090805
                string customerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(customerIP))
                {
                    return customerIP;
                }

                customerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!String.IsNullOrEmpty(customerIP))
                    return customerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    customerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (customerIP == null)
                        customerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    customerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.Compare(customerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return customerIP;
            }
            catch { }

            return userIP;
        }
    }
}
