using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util
{
    public static class DateTimeHelper
    {
        private static readonly DateTime MaxValueMinusOneDay = DateTime.MaxValue.AddDays(-1.0);
        private static readonly DateTime MinValuePlusOneDay = DateTime.MinValue.AddDays(1.0);
        public static readonly DateTime MinValue = new DateTime(1900, 1, 1);
        public static readonly DateTime MaxValue = DateTime.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dateTime, string format)
        {
            return dateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToLocalTime(DateTime utcTime)
        {
            if (utcTime < MinValuePlusOneDay)
            {
                return DateTime.MinValue;
            }
            if (utcTime > MaxValueMinusOneDay)
            {
                return DateTime.MaxValue;
            }
            return utcTime.ToLocalTime();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToUniversalTime(DateTime localTime)
        {
            if (localTime < MinValuePlusOneDay)
            {
                return DateTime.MinValue;
            }
            if (localTime > MaxValueMinusOneDay)
            {
                return DateTime.MaxValue;
            }
            return localTime.ToUniversalTime();
        }
    }
}
