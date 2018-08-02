using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class BaseTypeConverter : ITypeConverter
    {
        // Methods
        protected BaseTypeConverter()
        {
        }

        public virtual bool CanConvertFrom(Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public virtual bool CanConvertTo(Type destinationType)
        {
            return (destinationType == typeof(string));
        }

        public object ConvertFrom(object source)
        {
            return this.ConvertFrom(source, CultureInfo.CurrentCulture);
        }

        public abstract object ConvertFrom(object source, CultureInfo culture);

        public object ConvertTo(object value, Type destinationType)
        {
            return this.ConvertTo(value, destinationType, CultureInfo.CurrentCulture);
        }

        public virtual object ConvertTo(object value, Type destinationType, CultureInfo culture)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (!(destinationType == typeof(string)))
            {
                throw this.GetConvertToException(value, destinationType);
            }
            if (value == null)
            {
                return string.Empty;
            }
            if ((culture != null) && (culture != CultureInfo.CurrentCulture))
            {
                IFormattable formattable = value as IFormattable;
                if (formattable != null)
                {
                    return formattable.ToString(null, culture);
                }
            }
            return value.ToString();
        }

        protected Exception GetConvertFromException(object value)
        {
            string fullName;
            if (value == null)
            {
                fullName = TC.ToStringNull;
            }
            else
            {
                fullName = value.GetType().FullName;
            }
            throw new NotSupportedException(string.Format(TC.ConvertFromException, base.GetType().Name, fullName));
        }

        protected Exception GetConvertToException(object value, Type destinationType)
        {
            string fullName;
            if (value == null)
            {
                fullName = TC.ToStringNull;
            }
            else
            {
                fullName = value.GetType().FullName;
            }
            throw new NotSupportedException(string.Format(TC.ConvertToException, base.GetType().Name, fullName, destinationType.FullName));
        }
    }
}
