using System;
using System.Globalization;
using System.Collections.Generic;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BooleanConverter : BaseTypeConverter
    {
        /// <summary>
        /// 将布尔值 true 表示为可转换的字符串。此字段为只读。 
        /// </summary>
        /// <remarks>该字段包含字符串“TRUE”，“YES”，“ON”，“1”。</remarks>
        public static readonly List<string> TrueStringList = new List<string>();

        /// <summary>
        /// 将布尔值 false 表示为可转换的字符串。此字段为只读。 
        /// </summary>
        /// <remarks>该字段包含字符串“FALSE”，“NO”，“OFF”，“0”。</remarks>
        public static readonly List<string> FalseStringList = new List<string>();

        static BooleanConverter()
		{
            TrueStringList.Add("T");
			TrueStringList.Add("TRUE");
			TrueStringList.Add("YES");
			TrueStringList.Add("ON");
			TrueStringList.Add("1");

            FalseStringList.Add("F");
			FalseStringList.Add("FALSE");
			FalseStringList.Add("NO");
			FalseStringList.Add("OFF");
			FalseStringList.Add("0");
			FalseStringList.Add("");
		}


        public override bool CanConvertFrom(Type sourceType)
        {
            return (sourceType == typeof(bool)) 
                || IsIntType(sourceType) 
                || base.CanConvertFrom(sourceType);
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source == null)
            {
                return false;
            }

            Type sourceType = source.GetType();

            if (sourceType == typeof(bool))
            {
                return source;
            }

            if (sourceType == typeof(string))
            {
                string upperedValue = source.ToString().Trim().ToUpper();
                if (TrueStringList.Contains(upperedValue))
                {
                    return true;
                }
                else if (FalseStringList.Contains(upperedValue))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }

            if (IsIntType(sourceType))
            {
                string sourceValue = source.ToString();
                if (sourceValue == "0")
                {
                    return false;
                }

                if (sourceValue == "1")
                {
                    return true;
                }
            }

            return false; 
        }

        private bool IsIntType(Type type)
        {
            return type == typeof (short) || type == typeof (int) || type == typeof (long) || type == typeof (UInt64);
        }
    }
}