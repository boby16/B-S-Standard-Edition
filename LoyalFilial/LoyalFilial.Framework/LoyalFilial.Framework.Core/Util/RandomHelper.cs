/***********************************************************************
 *  Author:  felix
 *  Date:    2014-01-09
 *  Purpose: 随机数实用类
 * /***********************************************************************
 *  Date        Changer         Description                                                            
 *  2014-01-09  felix           add
 * ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LoyalFilial.Framework.Core.Util
{
    /// <summary>
    /// 随机数实用类
    /// </summary>
    public static class RandomHelper
    {

        /// <summary>
        /// 缺省的字符串取值范围
        /// </summary>
        private const string DEFAULT_CHARLIST = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        /// <summary>
        /// 随机的数字
        /// </summary>
        private static readonly char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };


        /// <summary>
        /// 获取随机字节序列
        /// </summary>
        /// <param name="length">字节序列的长度</param>
        /// <returns>字节序列</returns>
        public static byte[] RandomBytes(int length)
        {
            return RandomBytes(length, false);
        }

        /// <summary>
        /// 获取随机字节序列
        /// </summary>
        /// <param name="length">字节序列的长度</param>
        /// <param name="nonZero">生成的数字是否可为0</param>
        /// <returns>字节序列</returns>
        public static byte[] RandomBytes(int length, bool nonZero)
        {
            if (length <= 0)
            {
                return new byte[0];
            }
            var rng = new RNGCryptoServiceProvider();
            var ret = new byte[length];
            if (nonZero)
            {
                rng.GetNonZeroBytes(ret);
            }
            else
            {
                rng.GetBytes(ret);
            }
            return ret;
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="charList">字符串取值范围（如果为Null或为空，则返回空字符串）</param>
        /// <returns>随机字符串</returns>
        private static string GetRandomString(int length, string charList)
        {
            if (length <= 0 || string.IsNullOrEmpty(charList))
            {
                return string.Empty;
            }
            var num = charList.Length;
            var ret = new char[length];
            var rnd = RandomBytes(length);
            for (var i = 0; i < rnd.Length; i++)
            {
                ret[i] = charList[rnd[i] % num];
            }
            return new string(ret);
        }




        /// <summary>
        /// 获取随机的编码
        /// </summary>
        /// <param name="length">长度：最低不少于7，长度越低，重复概率越高，反之</param>
        /// <param name="isUnique">是否需要保证唯一</param>
        /// <param name="charList">字符串取值范围（如果为Null或为空，则从默认的字符串中随机）</param>
        /// <returns>随机的字符串</returns>
        private static string RandomCode(int length, bool isUnique = false, string charList = null)
        {
            char[] source = arrChar;
            if (!string.IsNullOrEmpty(charList))
            {
                source = charList.ToCharArray();
                var tempChar = charList.ToCharArray().ToList();
                foreach (var c in source)
                {
                    int tempNum = 0;
                    if (!int.TryParse(c.ToString(), out tempNum))
                    {
                        tempChar.Remove(c);
                    }
                }
                source = tempChar.ToArray();
            }

            StringBuilder num = new StringBuilder();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            string millisecond = string.Empty;
            if (isUnique)
            {
                millisecond = DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture);
                length = length - millisecond.Length;
            }

            for (int i = 0; i < length; i++)
            {
                num.Append(source[rnd.Next(0, source.Length)].ToString(CultureInfo.InvariantCulture));
            }

            if (!isUnique)
                return num.ToString();
            else
                return millisecond + num;
        }

        /// <summary>
        /// 获取随机的编码
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="charList">字符串取值范围（如果为Null或为空，则从默认的字符串中随机）</param>
        /// <param name="isPureNumbers">是否需要返回纯数字(此参为true时，会过虑掉传入字符串中非数字的字符)</param>
        /// <param name="isUnique">是否需要保证唯一(只有在isPureNumbers参数为true时起作用)</param>
        /// <returns>随机的字符串</returns>
        public static string RandomCode(int length, string charList = null, bool isPureNumbers = false, bool isUnique = false)
        {
            if (isPureNumbers)
                return RandomCode(length, isUnique, charList);
            if (!string.IsNullOrEmpty(charList))
                return GetRandomString(length, charList);
            else
                return GetRandomString(length, DEFAULT_CHARLIST);
        }

        /// <summary>
        /// 获取随机的编码
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="quantity">数量</param>
        /// <param name="charList">字符串取值范围（如果为Null或为空，则从默认的字符串中随机）</param>
        /// <param name="isPureNumbers">是否需要返回纯数字(此参为true时，会过虑掉传入字符串中非数字的字符)</param>
        /// <param name="isUnique">是否需要保证唯一(只有在isPureNumbers参数为true时起作用)</param>
        /// <returns>随机的字符串</returns>
        public static Dictionary<string, string> RandomCodeList(int length, int quantity = 1, string charList = null, bool isPureNumbers = false, bool isUnique = false)
        {
            if (quantity > 1)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                int i = 1;
                while (i <= quantity)
                {
                    var str = RandomCode(length, charList, isPureNumbers, isUnique);
                    try
                    {
                        result.Add(str, str);
                        i++;
                    }
                    catch (Exception ex)
                    {

                    }

                }
                return result;
            }
            var s = RandomCode(length, charList, isPureNumbers, isUnique);
            return new Dictionary<string, string> { { s, s } };
        }
    }
}
