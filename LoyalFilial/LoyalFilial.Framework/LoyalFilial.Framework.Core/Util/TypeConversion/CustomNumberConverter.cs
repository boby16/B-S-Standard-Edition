using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustomNumberConverter : BaseTypeConverter
    {
        // Fields
        private Type targetType;

        // Methods
        public CustomNumberConverter(Type targetType)
        {
            if (!this.IsNumberType(targetType))
            {
                throw new ArgumentException("Property type must be a primitve type or decimal type.");
            }
            this.targetType = targetType;
        }

        public override bool CanConvertFrom(Type sourceType)
        {
            return (this.IsNumberType(sourceType) || base.CanConvertFrom(sourceType));
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source == null)
            {
                source = "0";
            }
            Type sourceType = source.GetType();
            if (sourceType == typeof(string))
            {
                string value = source as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "0";
                }
                try
                {
                    if (this.targetType == typeof(byte))
                    {
                        return byte.Parse(value, NumberStyles.Number, culture);
                    }
                    if (this.targetType == typeof(short))
                    {
                        return short.Parse(value, NumberStyles.Number, culture);
                    }
                    if (this.targetType == typeof(int))
                    {
                        return int.Parse(value, NumberStyles.Number, culture);
                    }
                    if (this.targetType == typeof(long))
                    {
                        return long.Parse(value, NumberStyles.Number, culture);
                    }
                    if (this.targetType == typeof(double))
                    {
                        return Convert.ToDouble(value);
                    }
                    if (this.targetType == typeof(decimal))
                    {
                        return Convert.ToDecimal(value);
                    }
                }
                catch (Exception exception)
                {
                    throw this.FromStringError(value, exception);
                }
            }
            else if (this.IsNumberType(sourceType))
            {
                return Convert.ChangeType(source, this.targetType);
            }
            throw base.GetConvertFromException(source);
        }

        protected Exception FromStringError(string failedText, Exception innerException)
        {
            string errorMessage = string.Format(TC.ConvertInvalidPrimitive, failedText, this.targetType.Name);
            if (innerException == null)
            {
                return new Exception(errorMessage);
            }
            if (innerException is OverflowException)
            {
                return new OverflowException(errorMessage, innerException);
            }
            if (innerException is FormatException)
            {
                return new FormatException(errorMessage, innerException);
            }
            return new Exception(errorMessage, innerException);
        }

        private bool IsNumberType(Type type)
        {
            return ((((type == typeof(byte)) || (type == typeof(short)) || (type == typeof(int))) || ((type == typeof(long)) || (type == typeof(double)))) || (type == typeof(decimal)));
        }
    }
}