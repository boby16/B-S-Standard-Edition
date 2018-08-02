namespace LoyalFilial.Framework.Data.DataMap.Core
{
    /// <summary>
    /// Represents an Indexer Get method
    /// </summary>
    /// <param name="target">the target instance when calling an instance method</param>
    /// <param name="index"></param>
    /// <returns>the value return by the Get method</returns>
    public delegate object PropertyGetterDelegate(object target, params object[] index);

    /// <summary>
    /// Represents a Set method
    /// </summary>
    /// <param name="target">the target instance when calling an instance method</param>
    /// <param name="value">the value to be set</param>
    /// <param name="index"></param>
    public delegate void PropertySetterDelegate(object target, object value, params object[] index);
}
