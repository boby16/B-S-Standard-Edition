using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StringConverter : BaseTypeConverter
    {
        // Methods
        public override bool CanConvertFrom(Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source is string)
            {
                return (string)source;
            }
            if (source == null)
            {
                return string.Empty;
            }
            if ((culture != null) && (culture != CultureInfo.CurrentCulture))
            {
                IFormattable formattable = source as IFormattable;
                if (formattable != null)
                {
                    return formattable.ToString(null, culture);
                }
            }
            return source.ToString();
        }
    }
}
