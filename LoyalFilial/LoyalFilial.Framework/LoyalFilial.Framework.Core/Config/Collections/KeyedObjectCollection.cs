using System;

namespace LoyalFilial.Framework.Core.Config.Collections
{
    /// <summary>
    /// 提供集合键嵌入在实现 <see cref="IKeyedObject"/> 集合子项中的<b>Key</b>属性的集合类。 
    /// </summary>
    /// <typeparam name="T">带有集合中的键属性的集合子项。</typeparam>
    public class KeyedObjectCollection<T> : KeyedObjectCollection<string, T>
        where T : IKeyedObject
    {
        /// <summary>
        /// 初始化使用默认相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        /// <remarks>字符串键值不区分大小写。</remarks>
        public KeyedObjectCollection()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// 初始化使用指定字符串相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        /// <param name="comparer">比较键时要使用的 <see cref="StringComparer"/>。</param>
        public KeyedObjectCollection(StringComparer comparer)
            : base(comparer ?? StringComparer.InvariantCultureIgnoreCase)
        {
        }
    }
}