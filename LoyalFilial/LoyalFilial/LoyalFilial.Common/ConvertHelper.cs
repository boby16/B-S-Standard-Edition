using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Common
{
    /// <summary>
    /// DateHelper
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// 将日期类型转换成字符串(2014-03-08)
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="type">日期类型</param>
        /// <returns>日期（文本）</returns>
        public static string ConvertToString(DateTime date, DateType type)
        {
            switch (type)
            {
                case DateType.Day:
                    return string.Format("{0:0000}-{1:00}-{2:00}", date.Year, date.Month, date.Day);
                    break;
                case DateType.Hour:
                    return string.Format("{0:0000}-{1:00}-{2:00} {3:00}:00:00", date.Year, date.Month, date.Day, date.Hour);
                    break;
                case DateType.Minute:
                    return string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:00", date.Year, date.Month, date.Day, date.Hour, date.Minute);
                    break;
                case DateType.Second:
                    return string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
                    break;
                case DateType.HourMinute:
                    return string.Format("{0:00}:{1:00}", date.Hour, date.Minute);
                case DateType.HourMinuteSecond:
                    return string.Format("{0:00}:{1:00}:{2:00}", date.Hour, date.Minute, date.Second);
                    break;
                default:
                    return string.Empty;
            }
        }
    }

    /// <summary>
    /// 日期类型
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// 天
        /// </summary>
        Day = 1,
        /// <summary>
        /// 小时
        /// </summary>
        Hour = 2,
        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 3,
        /// <summary>
        /// 秒
        /// </summary>
        Second = 4,
        /// <summary>
        /// 小时分钟
        /// </summary>
        HourMinute = 5,
        /// <summary>
        /// 时分秒
        /// </summary>
        HourMinuteSecond = 6
    }
}
