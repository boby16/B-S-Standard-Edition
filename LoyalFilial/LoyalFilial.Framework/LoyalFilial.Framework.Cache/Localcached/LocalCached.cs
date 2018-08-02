using System;
using System.Runtime.Caching;
using LoyalFilial.Framework.Core.Cache;

namespace LoyalFilial.Framework.Cache
{
    public class LocalCached : ICache
    {
        #region 构造函数
        public LocalCached() { }
        public LocalCached(string region, TimeSpan defaultExpirationTime)
        {
            DefaultExpirationTime = defaultExpirationTime;
            Region = null;// region;
            cache = MemoryCache.Default;
        }
        #endregion

        #region Ctors

        private static string Region { get; set; }
        private ObjectCache cache;
        /// <summary>
        /// 默认过期时间
        /// </summary>
        private static TimeSpan DefaultExpirationTime { get; set; }
        #endregion

        object ICache.this[string key, bool isExpiration = true]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                if (isExpiration)
                    this.Add(key, value, CacheDependency.Create(DefaultExpirationTime));
                else
                    this.Add(key, value, CacheDependency.Create());
            }
        }

        object ICache.this[string key, ICacheDependency cacheDependency]
        {
            set { this.Add(key, value, cacheDependency); }
        }

        public int Count
        {
            get
            {
                cache = MemoryCache.Default;
                return (int)cache.GetCount(Region);
            }
        }

        public void Add(string key, object value, ICacheDependency cacheDependency)
        {
            cache = MemoryCache.Default;
            var cacheItemPolicy = new CacheItemPolicy();
            if (cacheDependency.IsExpired)
            {
                if (cacheDependency.Sliding)
                {
                    cacheItemPolicy.SlidingExpiration = cacheDependency.SlidingExpiration;
                }
                else
                {
                    cacheItemPolicy.AbsoluteExpiration = cacheDependency.AbsoluteExpiration;
                }
            }
            cache.Set(key, value, cacheItemPolicy, Region);
        }

        public void Add(string key, object value, bool isExpiration = true)
        {
            cache = MemoryCache.Default;
            var cacheItemPolicy = new CacheItemPolicy();
            if (isExpiration)
                cacheItemPolicy.SlidingExpiration = DefaultExpirationTime;
            cache.Set(key, value, cacheItemPolicy, Region);
        }

        public object Get(string key)
        {
            cache = MemoryCache.Default;
            return cache.Get(key, Region);
        }

        public bool TryGet(string key, out object value)
        {
            value = this.Get(key);
            return value != null;
        }

        public void Remove(string key)
        {
            cache = MemoryCache.Default;
            cache.Remove(key, Region);
        }

        public bool Contains(string key)
        {
            cache = MemoryCache.Default;
            return cache.Contains(key, Region);
        }

        public void Clear()
        {

        }
    }
}
