using System;
using System.Collections.Generic;

namespace LoyalFilial.Framework.Core.Util.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TypeConverterRegistry
    {
        // Fields
        private static IDictionary<Type, ITypeConverter> converters = new Dictionary<Type, ITypeConverter>();
        private static object syncObj = new object();

        // Methods
        static TypeConverterRegistry()
        {
            lock (syncObj)
            {
                converters.Add(typeof(string), new StringConverter());
                converters.Add(typeof(byte), new CustomNumberConverter(typeof(byte)));
                converters.Add(typeof(short), new CustomNumberConverter(typeof(short)));
                converters.Add(typeof(int), new CustomNumberConverter(typeof(int)));
                converters.Add(typeof(long), new CustomNumberConverter(typeof(long)));                
                converters.Add(typeof(decimal), new CustomNumberConverter(typeof(decimal)));
                converters.Add(typeof(double), new CustomNumberConverter(typeof(double)));
                converters.Add(typeof(bool), new BooleanConverter());
                converters.Add(typeof(DateTime), new DateTimeConverter());
                converters.Add(typeof(Guid), new GuidConverter());

                converters.Add(typeof(Nullable<byte>), new NullableConverter(typeof(Nullable<byte>)));
                converters.Add(typeof(Nullable<short>), new NullableConverter(typeof(Nullable<short>)));
                converters.Add(typeof(Nullable<int>), new NullableConverter(typeof(Nullable<int>)));
                converters.Add(typeof(Nullable<long>), new NullableConverter(typeof(Nullable<long>)));
                converters.Add(typeof(Nullable<decimal>), new NullableConverter(typeof(Nullable<decimal>)));
                converters.Add(typeof(Nullable<double>), new NullableConverter(typeof(Nullable<double>)));
                converters.Add(typeof(Nullable<Single>), new NullableConverter(typeof(Nullable<Single>)));
                converters.Add(typeof(Nullable<bool>), new NullableConverter(typeof(Nullable<bool>)));
                converters.Add(typeof(Nullable<DateTime>), new NullableConverter(typeof(Nullable<DateTime>)));
                converters.Add(typeof(Nullable<Guid>), new NullableConverter(typeof(Nullable<Guid>)));
            }
        }

        public static ITypeConverter GetConverter<TTarget>()
        {
            return GetConverter(typeof(TTarget));
        }

        public static ITypeConverter GetConverter(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "type cannnot be null.");
            }

            ITypeConverter converter = null;
            if (!converters.TryGetValue(type, out converter))
            {
                lock (syncObj)
                {
                    if (!converters.TryGetValue(type, out converter))
                    {
                        if (type.IsEnum)
                        {
                            converter = new SimpleEnumConverter(type);
                            converters.Add(type, converter);
                        }
                    }
                }
            }

            return converter;
        }

        public static void RegisterConverter(Type type, ITypeConverter converter)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "type cannnot be null.");
            }
            if (converter == null)
            {
                throw new ArgumentNullException("converter", "converter cannnot be null.");
            }
            lock (syncObj)
            {
                if (converters.ContainsKey(type))
                {
                    converters[type] = converter;
                }
                else
                {
                    converters.Add(type, converter);
                }
            }
        }
    }
}
