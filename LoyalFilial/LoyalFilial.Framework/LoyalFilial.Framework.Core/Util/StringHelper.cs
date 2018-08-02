using System;

namespace LoyalFilial.Framework.Core.Util
{
    public class StringHelper
    {
        public static bool IsTrue(string input)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(input))
                return false;
            return input.ToLower().Trim() == "true";
        }

        public static string GetString(object o)
        {
            return o == null ? "" : o.ToString();
        }

        public static string BlankNullString(string input)
        {
            if (input == null || input == string.Empty)
                return "";
            else
                return input;
        }

        public static string HtmlBlankNullString(string input)
        {
            if (input == null || input == string.Empty)
                return "&nbsp;";
            else
                return input;
        }

        public static bool IsHtmlBlank(string input)
        {
            return input == "&nbsp;";
        }

        public static bool IsNullOrEmptyOrBlankString(string input)
        {
            if (input == null) return true;
            return input.Equals(string.Empty) || input.Trim() == "";
        }

        public static string ShortString(string input, int length)
        {
            if (IsNullOrEmptyOrBlankString(input)) return "";
            if (input.Length <= length)
                return input;
            return input.Substring(0, length) + "...";
        }

        public static string ShortStringCharacter(string input, int length)
        {
            if (IsNullOrEmptyOrBlankString(input)) return "";
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(input);// 单字节字符长度
            int charLen = input.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > length)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(input.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > length)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == length)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }
                if (pos >= 0)
                    result = input.Substring(0, pos) + "...";
            }
            else
                result = input;
            return result;
        }

        public static string DataTimeLongString(DateTime time)
        {
            if (time == null) return "";
            return time.ToString("yyyy-MM-dd HH:mm:sss");
        }

        public static string DataTimeString(DateTime time)
        {
            if (time == null) return "";
            return time.ToString("yyyy-MM-dd HH:mm");
        }

        public static string DataTimeString(DateTime? time)
        {
            if (time == null) return "";
            return Convert.ToDateTime(time).ToString("yyyy-MM-dd HH:mm");
        }

        public static string DataTimeShortString(DateTime time)
        {
            if (time == null) return "";
            return time.ToString("yyyy-MM-dd");
        }

        public static string DataTimeShortString(DateTime? time)
        {
            if (time == null) return "";
            return Convert.ToDateTime(time).ToString("yyyy-MM-dd");
        }

        public static string HtmlCode(string input)
        {
            if (input == null) return "&nbsp;";
            return input.Replace("\r\n", "<br>").Replace(" ", "&nbsp;");
        }

        public static bool IsForbiddenSql(string input)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(input))
                return false;
            string[] arr = input.Split('\'');
            int count = 0, index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                var tmpIndex = arr[i].IndexOf(";");
                if (tmpIndex >= 0 && i%2 == 0)
                {
                    count++;
                    index += tmpIndex;
                    if (count > 1)
                    {
                        if (arr[i].Trim() == "")
                            continue;
                        return true;
                    }
                    var secPart = input.Substring(index + 1 + i);
                    if (IsNullOrEmptyOrBlankString(secPart))
                        continue;
                    var isSelect = secPart.Trim().ToLower().StartsWith("select");
                    if (!isSelect) return true;
                    continue;
                }
                index += arr[i].Length;
            }
            return false;
        }

        public static bool HasIdenticallyEqual(string input)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"(?:\s*[\d]+\s*=\s*[\d]+\s*|\s*'[\w|\s|\S]*'\s*=\s*'[\w|\s|\S]*'\s*)");
            System.Text.RegularExpressions.MatchCollection mc = reg.Matches(input);
            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                string[] tempArray = m.Value.Split('=');
                if (tempArray[0].Trim() == tempArray[1].Trim()) return true;
            }
            return false;
        }
    }
}