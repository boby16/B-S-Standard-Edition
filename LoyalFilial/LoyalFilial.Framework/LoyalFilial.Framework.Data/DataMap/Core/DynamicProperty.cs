using System.Reflection;

namespace LoyalFilial.Framework.Data.DataMap.Core
{
    public class DynamicProperty
    {
        /// <summary>
        /// Creates dynamic property instance for the specified <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="property">Property info to create dynamic property for.</param>
        /// <returns>Dynamic property for the specified <see cref="PropertyInfo"/>.</returns>
        public static IDynamicProperty Create(PropertyInfo property)
        {
            return new SafeProperty(property);
        }
    }
}
