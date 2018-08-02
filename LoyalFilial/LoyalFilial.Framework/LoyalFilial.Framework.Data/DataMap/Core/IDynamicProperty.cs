using System;

namespace LoyalFilial.Framework.Data.DataMap.Core
{
    /// <summary>
    /// Defines methods that dynamic property class has to implement.
    /// </summary>
    public interface IDynamicProperty
    {
        string PropertyName { get; }

        /// <summary>
        /// 
        /// </summary>
        Type PropertyType { get; }

        /// <summary>
        /// Gets the value of the dynamic property for the specified target object.
        /// </summary>
        /// <param name="target">
        /// Target object to get property value from.
        /// </param>
        /// <returns>
        /// A property value.
        /// </returns>
        object GetValue(object target);

        /// <summary>
        /// Gets the value of the dynamic property for the specified target object.
        /// </summary>
        /// <param name="target">
        /// Target object to set property value on.
        /// </param>
        /// <param name="value">
        /// A new property value.
        /// </param>
        object SetValue(object target, object value);

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
        object GetValue(object target, params object[] index);

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
        object SetValue(object target, object value, params object[] index);
    }
}
