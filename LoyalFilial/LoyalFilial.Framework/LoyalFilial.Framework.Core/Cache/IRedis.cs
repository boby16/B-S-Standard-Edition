using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Framework.Core.Cache
{
    public interface IRedis : ICache
    {
        ///  <summary>
        ///  以健值方式获取/设置缓存项（值）
        ///  </summary>
        ///  <param name="key">缓存项的健值</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <param name="isExpiration">是否会过期，默认true,读配置的默认过期时间，设置户不过期传false</param>
        /// <remarks>
        /// 	如果是设置值，则使用配置默认过期策略缓存
        ///  </remarks>
        object this[string key, long db, bool isExpiration = true] { get; set; }

        /// <summary>
        /// 按指定健值和过期策略来设置缓存项（值）
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <param name="cacheDependency">缓存项的过期策略</param>
        object this[string key, long db, ICacheDependency cacheDependency] { set; }

        /// <summary>
        /// 获取此缓存器所缓存项的个数
        ///  <param name="db">存储的库（缓存中）</param>
        /// </summary>
        int CountForDb(long db);

        /// <summary>
        /// 添加一项到缓存器中
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <param name="cacheDependency">缓存项的过期策略</param>
        void Add(string key, object value, long db, ICacheDependency cacheDependency);

        /// <summary>
        /// 添加一项到缓存器中
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <param name="isExpiration">>是否会过期，默认true,读配置的默认过期时间，设置户不过期传false</param>
        /// <remarks>
        /// 没有指定过期策略，则使用配置默认过期策略缓存
        /// </remarks>
        void Add(string key, object value, long db, bool isExpiration = true);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <returns>缓存的对象，如果缓存中没有命中，则返回<c>null</c></returns>
        object Get(string key, long db);

        /// <summary>
        /// 尝试获取缓存项，如果存在则返回<c>true</c>
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <param name="value">>缓存的对象，如果缓存中没有命中，则返回<c>null</c></param>
        /// <returns>如果存在则返回<c>true</c></returns>
        bool TryGet(string key, long db, out object value);

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="db">存储的库（缓存中）</param>
        void Remove(string key, long db);

        /// <summary>
        /// 判断缓存器中是否包含指定健值的缓存项
        /// </summary>
        /// <param name="key">缓存项的健值</param>
        /// <param name="db">存储的库（缓存中）</param>
        /// <returns>是/否</returns>
        bool Contains(string key, long db);

        /// <summary>
        /// 清除此缓存器中所有的项 不影响配置文件中其他缓存器
        /// </summary>
        /// <param name="db">存储的库（缓存中）</param>
        void Clear(long db);

        /// <summary>
        /// 将内在数据持久化
        /// </summary>
        void Save();

        /// <summary>
        /// 将内在数据持久化
        /// </summary>
        /// <param name="db"></param>
        void Save(long db);

        /// <summary>
        /// 获取默认库下的所有Key
        /// </summary>
        /// <returns></returns>
        List<string> GetAllKeys();

        /// <summary>
        /// 获取指定库下的所有Key
        /// </summary>
        /// <param name="db">指定的库（下标）</param>
        /// <returns></returns>
        List<string> GetAllKeys(long db);

        long LPush(string listId, object value);
        long LPush(string listId, object value, long db);

        object LPop(string listId);
        object LPop(string listId, long db);

        long RPush(string listId, object value);
        long RPush(string listId, object value, long db);

        object RPop(string listId);
        object RPop(string listId, long db);
    }
}
