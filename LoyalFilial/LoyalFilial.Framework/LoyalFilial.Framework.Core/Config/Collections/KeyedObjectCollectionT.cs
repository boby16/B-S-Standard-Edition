using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LoyalFilial.Framework.Core.Config.Collections
{
    /// <summary>
    /// 提供集合键嵌入在实现 <see cref="IKeyedObject"/> 集合子项中的<b>Key</b>属性的集合类。 
    /// </summary>
    /// <typeparam name="T">带有集合中的键属性的集合子项。</typeparam>
    [Serializable]
    public class KeyedObjectCollection<TKey, T> : KeyedCollection<TKey, T>
        where T : IKeyedObject<TKey>
    {
        /// <summary>
        /// 初始化使用默认相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        public KeyedObjectCollection()
            : base()
        {
        }

        /// <summary>
        /// 初始化使用指定字符串相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        /// <param name="comparer">比较键时要使用的 <see cref="comparer"/>。</param>
        public KeyedObjectCollection(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// 从指定元素提取键。
        /// </summary>
        /// <param name="item">从中提取键的元素。</param>
        /// <returns>指定元素的键。</returns>
        protected override TKey GetKeyForItem(T item)
        {
            return item.Key;
        }

        /// <summary>
        /// 将元素插入集合的指定索引处。
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 item。</param>
        /// <param name="item">要插入的对象。</param>
        protected override void InsertItem(int index, T item)
        {
            if (this.Contains(item.Key))
                throw new Exception(item.Key.ToString());

            base.InsertItem(index, item);
        }

        /// <summary>
        /// 获取具有指定键的元素。
        /// </summary>
        /// <param name="key">要获取的元素的键。</param>
        /// <returns>带有指定键的元素。如果未找到具有指定键的元素，则默认为键值类型初始默认值。</returns>
        public new T this[TKey key]
        {
            get
            {
                T item = default(T);
                if (Contains(key))
                    item = base[key];

                return item;
            }
        }
    }
}