namespace LoyalFilial.Framework.Core.Config.Collections
{
    /// <summary>
    /// 表示带有集合中的键属性的集合子项。
    /// </summary>
    public interface IKeyedObject : IKeyedObject<string>
    {
    }

    /// <summary>
    /// 表示带有集合中的键属性的集合子项。
    /// </summary>
    public interface IKeyedObject<T>
    {
        /// <summary>
        /// 获取集合的键值。
        /// </summary>
        /// <value>集合的键值。</value>        
        T Key { get; }
    }
}
