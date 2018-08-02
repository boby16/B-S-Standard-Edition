using System;
using System.Text;
using System.Security.Cryptography;

namespace LoyalFilial.Framework.Core.Util
{
    /// <summary>
    /// 签名工具类
    /// </summary>
    public class SignatureHelper
    {
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="data">签名数据</param>
        /// <param name="algorithm">算法</param>
        /// <param name="key">密钥</param>
        /// <returns>签名</returns>
        public static string Generate(string data, string algorithm, string key)
        {
            if (string.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException("algorithm", "算法为空");
            }
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data", "数据为空");
            }
            if (key == null)
            {
                key = string.Empty;
            }
            byte[] hash = null;
            data = string.Format("{0}{1}", data, key);
            data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
            switch (algorithm.ToLower())
            {

                case "md5":
                    hash = MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(data));
                    break;
                case "sha256":
                    hash = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(data));
                    break;
                case "sha512":
                    hash = SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(data));
                    break;
                default:
                    throw new Exception("当前不支持以" + algorithm + "为算法的签名");
            }
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sig">原始签名</param>
        /// <param name="data">签名数据</param>
        /// <param name="algorithm">算法</param>
        /// <param name="key">密钥</param>
        /// <param name="rightSig">正确的签名</param>
        /// <returns>是否通过验证</returns>
        public static bool VerifySig(string sig, string data, string algorithm, string key, out string rightSig)
        {
            rightSig = Generate(data, algorithm, key);
            return string.Compare(sig, rightSig, true) == 0;
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="data">签名数据</param>
        /// <param name="algorithm">算法</param>
        /// <param name="key">密钥</param>
        /// <returns>签名</returns>
        public static string GenerateAPK(string data, string algorithm, string key)
        {
            if (string.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException("algorithm", "算法为空");
            }
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data", "数据为空");
            }
            if (key == null)
            {
                key = string.Empty;
            }
            byte[] hash = null;
            data = string.Format("{0}{1}", data, key);
            data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
            switch (algorithm.ToLower())
            {

                case "md5":
                    hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(data));
                    break;
                case "sha256":
                    hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(data));
                    break;
                case "sha512":
                    hash = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(data));
                    break;
                default:
                    throw new Exception("当前不支持以" + algorithm + "为算法的签名");
            }
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sig">原始签名</param>
        /// <param name="data">签名数据</param>
        /// <param name="algorithm">算法</param>
        /// <param name="key">密钥</param>
        /// <param name="rightSig">正确的签名</param>
        /// <returns>是否通过验证</returns>
        public static bool VerifySigAPK(string sig, string data, string algorithm, string key, out string rightSig)
        {
            rightSig = GenerateAPK(data, algorithm, key);
            return string.Compare(sig, rightSig, true) == 0;
        }
    }
}
