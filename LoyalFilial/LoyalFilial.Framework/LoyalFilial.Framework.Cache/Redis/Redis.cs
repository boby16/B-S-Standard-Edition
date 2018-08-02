using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Cache;
using ServiceStack.Redis;

namespace LoyalFilial.Framework.Cache
{
    public class Redis : IRedis
    {
        #region 构造函数

        private static Redis _redisManager = null;

        public static Redis RedisManager
        {
            get
            {
                return _redisManager = (_redisManager ?? new Redis());
            }
        }

        public Redis() { }

        public Redis(int maxWritePoolSize, int maxReadPoolSize, string[] readWriteHosts, string[] readOnlyHosts, long initalDb, TimeSpan defaultExpirationTime)
        {
            try
            {
                try
                {
                    var config = new RedisClientManagerConfig
                    {
                        MaxWritePoolSize = maxWritePoolSize, //“写”链接池链接数
                        MaxReadPoolSize = maxReadPoolSize, //“读”链接池链接数
                        AutoStart = true,
                    };
                    PooledRedisClientManager.Add(initalDb, new PooledRedisClientManager(readWriteHosts, readOnlyHosts, config, initalDb, null, null));
                }
                catch (Exception e)
                {
                    //LFFK.LogManager.Error(CacheConstants.Error_Init, e);
                }
                DefaultExpirationTime = defaultExpirationTime;
                InitalDb = initalDb;
                ReadWriteHost = readWriteHosts[0];
                MaxWritePoolSize = maxWritePoolSize;
                MaxReadPoolSize = maxReadPoolSize;
                ReadWriteHosts = readWriteHosts;
                ReadOnlyHosts = readWriteHosts;
                PersistentClient.Add(initalDb, new RedisClient(ReadWriteHost.Split(':')[0], Convert.ToInt32(ReadWriteHost.Split(':')[1]), db: initalDb));
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Error_Data_InitConfigFailed, ex);
            }
        }
        #endregion

        #region Ctors

        // 客户端实例管理池(连接池)
        public static Dictionary<long, PooledRedisClientManager> PooledRedisClientManager = new Dictionary<long, PooledRedisClientManager>();

        private static string CacheName { get; set; }
        /// <summary>
        /// 默认过期时间
        /// </summary>
        private static TimeSpan DefaultExpirationTime { get; set; }

        private static long InitalDb { get; set; }

        private static string ReadWriteHost { get; set; }

        private static int MaxWritePoolSize { get; set; }

        private static int MaxReadPoolSize { get; set; }

        private static string[] ReadWriteHosts { get; set; }

        private static string[] ReadOnlyHosts { get; set; }

        //客户端实例管理池(长连接)
        public static Dictionary<long, RedisClient> PersistentClient = new Dictionary<long, RedisClient>();
        #endregion

        #region cache
        object ICache.this[string key, bool isExpiration = true]
        {
            get
            {
                return this.Get(key, InitalDb);
            }
            set
            {
                if (isExpiration)
                    this.Add(key, value, InitalDb, CacheDependency.Create(DefaultExpirationTime));
                else
                    this.Add(key, value, InitalDb, CacheDependency.Create());
            }
        }

        object ICache.this[string key, ICacheDependency cacheDependency]
        {
            set { this.Add(key, value, InitalDb, cacheDependency); }
        }

        public int Count
        {
            get
            {
                using (var rc = this.GetPooledRedisClient(InitalDb).GetClient())
                {
                    return rc.GetAllKeys().Count;
                }
            }
        }

        public void Add(string key, object value, ICacheDependency cacheDependency)
        {
            this.Add(key, value, InitalDb, cacheDependency);
        }

        public void Add(string key, object value, bool isExpiration = true)
        {
            this.Add(key, value, InitalDb, isExpiration);
        }

        public object Get(string key)
        {
            return this.Get(key, InitalDb);
        }

        public bool TryGet(string key, out object value)
        {
            return this.TryGet(key, InitalDb, out  value);
        }

        public void Remove(string key)
        {
            this.Remove(key, InitalDb);
        }

        public bool Contains(string key)
        {
            return this.Contains(key, InitalDb);
        }

        public void Clear()
        {
            this.Clear(InitalDb);
        }


        public void Save()
        {
            this.Save(InitalDb);
        }

        object IRedis.this[string key, long db, bool isExpiration]
        {
            get
            {
                return this.Get(key, db);
            }
            set
            {
                if (isExpiration)
                    this.Add(key, value, db, CacheDependency.Create(DefaultExpirationTime));
                else
                    this.Add(key, value, db, CacheDependency.Create());
            }
        }

        object IRedis.this[string key, long db, ICacheDependency cacheDependency]
        {
            set { this.Add(key, value, db, cacheDependency); }
        }

        public int CountForDb(long db)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                return rc.GetAllKeys().Count;
            }
        }

        public void Add(string key, object value, long db, ICacheDependency cacheDependency)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                if (cacheDependency.IsExpired)
                {
                    if (cacheDependency.Sliding)
                        rc.Set(key, value, cacheDependency.SlidingExpiration);
                    else
                        rc.Set(key, value, cacheDependency.AbsoluteExpiration);
                }
                else
                {
                    rc.Set(key, value);
                }
            }
        }

        public void Add(string key, object value, long db, bool isExpiration = true)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                if (isExpiration)
                    rc.Set(key, value, DefaultExpirationTime);
                else
                    rc.Set(key, value);
            }
        }

        public object Get(string key, long db)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                return rc.Get<object>(key);
            }
        }

        public bool TryGet(string key, long db, out object value)
        {
            value = this.Get(key, db);
            return value != null;
        }

        public void Remove(string key, long db)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                rc.Remove(key);
            }
        }

        public bool Contains(string key, long db)
        {

            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                return rc.ContainsKey(key);
            }
        }

        public void Clear(long db)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                rc.FlushDb();
            }
        }

        public void Save(long db)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                rc.Save();
            }
        }

        public List<string> GetAllKeys()
        {
            return this.GetAllKeys(InitalDb);
        }

        public List<string> GetAllKeys(long db)
        {
            using (var rc = this.GetPooledRedisClient(db).GetClient())
            {
                return rc.GetAllKeys();
            }
        }

        #endregion

        #region queue

        public long LPush(string listId, object value)
        {
            return this.LPush(listId, value, InitalDb);
        }

        public long LPush(string listId, object value, long db)
        {
            using (var client = this.GetPersistentRedisClient(db))
            {
                var buffer = RedisHeler.ObjectToBytes(value);
                return client.LPush(listId, buffer);
            }
        }


        public object LPop(string listId)
        {
            return this.LPop(listId, InitalDb);
        }

        public object LPop(string listId, long db)
        {
            using (var client = this.GetPersistentRedisClient(db))
            {
                var buffer = client.LPop(listId);
                var value = RedisHeler.BytesToObject(buffer);
                return value;
            }
        }

        public long RPush(string listId, object value)
        {
            return this.RPush(listId, value, InitalDb);
        }

        public long RPush(string listId, object value, long db)
        {
            using (var client = this.GetPersistentRedisClient(db))
            {
                var buffer = RedisHeler.ObjectToBytes(value);
                return client.RPush(listId, buffer);
            }
        }

        public object RPop(string listId)
        {
            return this.RPop(listId, InitalDb);
        }

        public object RPop(string listId, long db)
        {
            using (var client = this.GetPersistentRedisClient(db))
            {
                var buffer = client.RPop(listId);
                object value = null;
                if (buffer != null && buffer.Count() > 0)
                    value = RedisHeler.BytesToObject(buffer);
                return value;
            }
        }

        #endregion

        #region get PooledRedisClient
        public PooledRedisClientManager GetPooledRedisClient()
        {
            return GetPooledRedisClient(InitalDb);
        }

        public PooledRedisClientManager GetPooledRedisClient(long db)
        {
            try
            {
                PooledRedisClientManager pooledRedisClientManager = null;
                if (PooledRedisClientManager.TryGetValue(db, out pooledRedisClientManager))
                    return pooledRedisClientManager;
                else
                {
                    var config = new RedisClientManagerConfig
                       {
                           MaxWritePoolSize = MaxWritePoolSize, //“写”链接池链接数
                           MaxReadPoolSize = MaxReadPoolSize, //“读”链接池链接数
                           AutoStart = true,
                       };
                    pooledRedisClientManager = new PooledRedisClientManager(ReadWriteHosts, ReadOnlyHosts, config, db, null, null);
                    PooledRedisClientManager.Add(db, pooledRedisClientManager);
                    return pooledRedisClientManager;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region get PersistentRedisClient
        public RedisClient GetPersistentRedisClient()
        {
            return GetPersistentRedisClient(InitalDb);
        }

        public RedisClient GetPersistentRedisClient(long db)
        {
            try
            {
                return RedisClientFactory.GetRedisClient(ReadWriteHost, db);

                //RedisClient redisClient = null;
                //if (PersistentClient.TryGetValue(db, out redisClient))
                //    return redisClient;
                //else
                //{
                //    redisClient = new RedisClient(ReadWriteHost.Split(':')[0], Convert.ToInt32(ReadWriteHost.Split(':')[1]), db: db);
                //    PersistentClient.Add(db, redisClient);
                //    return redisClient;
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
