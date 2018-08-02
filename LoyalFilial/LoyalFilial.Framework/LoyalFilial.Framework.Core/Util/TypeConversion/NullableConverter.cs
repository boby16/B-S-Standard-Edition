using System;
using System.Globalization;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    public class NullableConverter : BaseTypeConverter
    {
        private Type nullableType;
        private Type simpleType;
        private ITypeConverter simpleTypeConverter;

        public NullableConverter(Type type)
        {
            this.nullableType = type;
            this.simpleType = Nullable.GetUnderlyingType(type);
            if (this.simpleType == null)
            {
                throw new ArgumentException(TC.NullableConverterBadCtorArg, "type");
            }
            this.simpleTypeConverter = TypeConverterRegistry.GetConverter(this.simpleType);
        }

        public override bool CanConvertFrom(Type sourceType)
        {
            if (sourceType == this.simpleType)
            {
                return true;
            }
            if (this.simpleTypeConverter != null)
            {
                return this.simpleTypeConverter.CanConvertFrom(sourceType);
            }
            return base.CanConvertFrom(sourceType);
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if ((source == null) || (source.GetType() == this.simpleType))
            {
                return source;
            }
            if ((source is string) && string.IsNullOrEmpty(source as string))
            {
                return null;
            }
            if (this.simpleTypeConverter != null)
            {
                return this.simpleTypeConverter.ConvertFrom(source, culture);
            }
            return null;
        }
    }
}