using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Converter
    {
        public static object ToType(object source, Type targetType)
        {
            ITypeConverter typeConverter = TypeConverterRegistry.GetConverter(targetType);
            if (typeConverter == null)
            {
                return Convert.ChangeType(source, targetType);
            }

            return typeConverter.ConvertFrom(source);
        }
    }
}
