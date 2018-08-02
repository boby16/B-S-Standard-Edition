using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnumConverter : BaseTypeConverter
    {
        // Methods
        public EnumConverter(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override bool CanConvertFrom(Type sourceType)
        {
            return ((sourceType == typeof(System.Enum[])) || (sourceType == typeof(string)));
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source != null)
            {
                Array enumValues = System.Enum.GetValues(this.EnumType);
                Type sourceType = source.GetType();
                if (((sourceType == typeof(short)) || (sourceType == typeof(int))) || (sourceType == typeof(long)))
                {
                    return System.Enum.ToObject(this.EnumType, source);
                }
                if (source is string)
                {
                    try
                    {
                        string str = (string)source;
                        if (str.IndexOf(',') != -1)
                        {
                            long num = 0L;
                            foreach (string str2 in str.Split(new char[] { ',' }))
                            {
                                num |= Convert.ToInt64((System.Enum)System.Enum.Parse(this.EnumType, str2, true), culture);
                            }
                            return System.Enum.ToObject(this.EnumType, num);
                        }
                        return System.Enum.Parse(this.EnumType, str, true);
                    }
                    catch (Exception exception)
                    {
                        throw new FormatException(string.Format(TC.ConvertInvalidPrimitive, (string)source, this.EnumType.Name), exception);
                    }
                }
                if (source is System.Enum[])
                {
                    long num2 = 0L;
                    foreach (System.Enum enum2 in (System.Enum[])source)
                    {
                        num2 |= Convert.ToInt64(enum2, culture);
                    }
                    return System.Enum.ToObject(this.EnumType, num2);
                }
            }
            return System.Enum.ToObject(this.EnumType, 0);
        }

        // Properties
        public Type EnumType { get; private set; }
    }
}