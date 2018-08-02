using System;
using System.Reflection;
using LoyalFilial.Framework.Core.Util.TypeConversion;

namespace LoyalFilial.Framework.Data.DataMap.Core
{
    public class SafeProperty : IDynamicProperty
    {
        private readonly PropertyInfo propertyInfo;
        private readonly PropertyGetterDelegate getter;
        private readonly PropertySetterDelegate setter;

        /// <summary>
        /// Creates a new instance of the safe property wrapper.
        /// </summary>
        /// <param name="propertyInfo">Property to wrap.</param>
        public SafeProperty(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
            getter = DynamicReflectionManager.CreatePropertyGetter(propertyInfo);
            setter = DynamicReflectionManager.CreatePropertySetter(propertyInfo);
        }

        public string PropertyName
        {
            get { return propertyInfo.Name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type PropertyType
        {
            get { return propertyInfo.PropertyType; }
        }

        /// <summary>
        /// Gets the value of the dynamic property for the specified target object.
        /// </summary>
        /// <param name="target">
        /// Target object to get property value from.
        /// </param>
        /// <returns>
        /// A property value.
        /// </returns>
        public object GetValue(object target)
        {
            return getter(target);
        }

        /// <summary>
        /// Gets the value of the dynamic property for the specified target object.
        /// </summary>
        /// <param name="target">
        /// Target object to get property value from.
        /// </param>
        /// <param name="index">Optional index values for indexed properties. This value should be null reference for non-indexed properties.</param>
        /// <returns>
        /// A property value.
        /// </returns>
        public object GetValue(object target, params object[] index)
        {
            return getter(target, index);
        }

        /// <summary>
        /// Gets the value of the dynamic property for the specified target object.
        /// </summary>
        /// <param name="target">
        /// Target object to set property value on.
        /// </param>
        /// <param name="value">
        /// A new property value.
        /// </param>
        public object SetValue(object target, object value)
        {
            object realValue = Converter.ToType(value, this.PropertyType);
            setter(target, realValue);
            return realValue;
        }

        /// <summary>
        /// Gets the value of the dynamic property for the specified target object.
        /// </summary>
        /// <param name="target">
        /// Target object to set property value on.
        /// </param>
        /// <param name="value">
        /// A new property value.
        /// </param>
        /// <param name="index">Optional index values for indexed properties. This value should be null reference for non-indexed properties.</param>
        public object SetValue(object target, object value, params object[] index)
        {
            object realValue = Converter.ToType(value, this.PropertyType);
            setter(target, value, index);
            return realValue;
        }

        /// <summary>
        /// Internal PropertyInfo accessor.
        /// </summary>
        internal PropertyInfo PropertyInfo
        {
            get { return propertyInfo; }
        }
    }
}
