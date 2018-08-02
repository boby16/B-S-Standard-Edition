using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITypeConverter
    {
        // Methods
        bool CanConvertFrom(Type sourceType);
        bool CanConvertTo(Type destinationType);
        object ConvertFrom(object source);
        object ConvertFrom(object source, CultureInfo culture);
        object ConvertTo(object vlaue, Type destinationType);
        object ConvertTo(object value, Type destinationType, CultureInfo culture);
    }
}
