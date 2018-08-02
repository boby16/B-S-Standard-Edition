using System;
using BeIT.MemCached;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Cache;

namespace LoyalFilial.Framework.Cache
{
    public class MemCached : ICache
    {
        #region 构造函数
        public MemCached() { }

        public MemCached(string cacheName, string servers, int sendReceiveTimeout, int connectTimeout, uint minPoolSize, uint maxPoolSize, string region, TimeSpan defaultExpirationTime)
        {
            try
            {
                try
                {
                    MemcachedClient.Setup(cacheName, servers.Split(','));
                }
                catch (Exception e)
                {
                    //LFFK.LogManager.Error(CacheConstants.Error_Init, e);
                }
                mc = MemcachedClient.GetInstance(cacheName);
                mc.SendReceiveTimeout = sendReceiveTimeout;
                mc.ConnectTimeout = connectTimeout;
                mc.MinPoolSize = minPoolSize;
                mc.MaxPoolSize = maxPoolSize;
                mc.KeyPrefix = region;
                CacheName = cacheName;
                DefaultExpirationTime = defaultExpirationTime;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Error_Data_InitConfigFailed, ex);
            }
        }
        #endregion

        #region Ctors
        // 获得客户端实例  
        private MemcachedClient mc;
        private static string CacheName { get; set; }

        /// <summary>
        /// 默认过期时间
        /// </summary>
        private static TimeSpan DefaultExpirationTime { get; set; }
        #endregion

        #region cache
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
            get { return 0; }
        }

        public void Add(string key, object value, ICacheDependency cacheDependency)
        {
            mc = MemcachedClient.GetInstance(CacheName);
            if (cacheDependency.IsExpired)
            {
                if (cacheDependency.Sliding)
                    mc.Set(key, value, cacheDependency.SlidingExpiration);
                else
                    mc.Set(key, value, cacheDependency.AbsoluteExpiration);
            }
            else
            {
                mc.Set(key, value);
            }
        }

        public void Add(string key, object value, bool isExpiration = true)
        {
            mc = MemcachedClient.GetInstance(CacheName);
            if (isExpiration)
                mc.Set(key, value, DefaultExpirationTime);
            else
                mc.Set(key, value);
        }

        public object Get(string key)
        {
            mc = MemcachedClient.GetInstance(CacheName);
            return mc.Get(key);
        }

        public bool TryGet(string key, out object value)
        {
            value = this.Get(key);
            return value != null;
        }

        public void Remove(string key)
        {
            mc = MemcachedClient.GetInstance(CacheName);
            mc.Delete(key);
        }

        public bool Contains(string key)
        {
            return default(bool);
        }

        public void Clear()
        {

        }
        #endregion

    }
}
