using System;
using System.Reflection;
using System.ComponentModel;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Framework.Data.DataMap.Map
{
    public class PropertyMap
    {
        public PropertyMap(string key, PropertyInfo property,ColumnAttribute attribute)
        {
            this.Key = key;
            this.Property = DynamicProperty.Create(property);
            this.Converter = TypeDescriptor.GetConverter(this.Property.PropertyType);
            this.Column = attribute;
            if (property != null && property.DeclaringType != null)
                this.TypeName = property.DeclaringType.Name;
        }

        public TypeConverter Converter { get; set; }
        public IDynamicProperty Property { get; private set; }

        public string Key { get; private set; }
        public string TypeName { get; private set; }
        public ColumnAttribute Column { get; private set; }
    }
}
