namespace LoyalFilial.Framework.Core.Cache
{
    /// <summary>
    /// 缓存器接口
    /// </summary>
    public interface ICache
    {
        ///  <summary>
        ///  以健值方式获取/设置缓存项（值）
        ///  </summary>
        ///  <param name="key">缓存项的健值</param>
        /// <param name="isExpiration">是否会过期，默认true,读配置的默认过期时间，设置户不过期传false</param>
        /// <remarks>
        /// 	如果是设置值，则使用配置默认过期策略缓存
        ///  </remarks>
        object this[string key, bool isExpiration = true] { get; set; }

        /// <summary>
        /// 按指定健值和过期策略来设置缓存项（值）
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="cacheDependency">缓存项的过期策略</param>
        object this[string key, ICacheDependency cacheDependency] { set; }

        /// <summary>
        /// 获取此缓存器所缓存项的个数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 添加一项到缓存器中
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="cacheDependency">缓存项的过期策略</param>
        void Add(string key, object value, ICacheDependency cacheDependency);

        /// <summary>
        /// 添加一项到缓存器中
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="isExpiration">>是否会过期，默认true,读配置的默认过期时间，设置户不过期传false</param>
        /// <remarks>
        /// 没有指定过期策略，则使用配置默认过期策略缓存
        /// </remarks>
        void Add(string key, object value, bool isExpiration = true);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <returns>缓存的对象，如果缓存中没有命中，则返回<c>null</c></returns>
        object Get(string key);

        /// <summary>
        /// 尝试获取缓存项，如果存在则返回<c>true</c>
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="value">>缓存的对象，如果缓存中没有命中，则返回<c>null</c></param>
        /// <returns>如果存在则返回<c>true</c></returns>
        bool TryGet(string key, out object value);

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        void Remove(string key);

        /// <summary>
        /// 判断缓存器中是否包含指定健值的缓存项
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <returns>是/否</returns>
        bool Contains(string key);

        /// <summary>
        /// 清除此缓存器中所有的项
        /// </summary>
        /// <remarks>
        /// 不影响配置文件中其他缓存器
        /// </remarks>
        void Clear();
    }
}
