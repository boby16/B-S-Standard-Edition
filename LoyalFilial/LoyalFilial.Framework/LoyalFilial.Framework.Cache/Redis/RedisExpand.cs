using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoyalFilial.Framework.Core.Cache;

namespace LoyalFilial.Framework.Cache
{
    public static class RedisExpand
    {


        public static T GetById<T>(this IRedis cache, object id)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetById(id);
            }
        }

        public static T GetById<T>(this IRedis cache, object id, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetById(id);
            }
        }

        public static IList<T> GetByIds<T>(this IRedis cache, IEnumerable ids)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetByIds(ids);
            }
        }

        public static IList<T> GetByIds<T>(this IRedis cache, IEnumerable ids, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetByIds(ids);

            }
        }


        public static IList<T> GetAll<T>(this IRedis cache)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetAll();
            }
        }

        public static IList<T> GetAll<T>(this IRedis cache, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetAll();
            }
        }

        public static IList<string> GetAllKeys<T>(this IRedis cache)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetAllKeys();
            }
        }

        public static IList<string> GetAllKeys<T>(this IRedis cache, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.GetAllKeys();
            }
        }

        public static T Add<T>(this IRedis cache, T entity)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.Store(entity);
            }
        }

        public static T Add<T>(this IRedis cache, T entity, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                return tClient.Store(entity);
            }
        }

        public static void AddAll<T>(this IRedis cache, IEnumerable<T> entities)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.StoreAll(entities);
            }
        }

        public static void AddAll<T>(this IRedis cache, IEnumerable<T> entities, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.StoreAll(entities);
            }
        }

        public static void Remove<T>(this IRedis cache, T entity)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.Delete(entity);
            }
        }

        public static void Remove<T>(this IRedis cache, T entity, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.Delete(entity);
            }
        }

        public static void RemoveById<T>(this IRedis cache, object id)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.DeleteById(id);
            }
        }

        public static void RemoveById<T>(this IRedis cache, object id, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.DeleteById(id);
            }
        }

        public static void RemoveByIds<T>(this IRedis cache, IEnumerable ids)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.DeleteByIds(ids);
            }
        }

        public static void RemoveByIds<T>(this IRedis cache, IEnumerable ids, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.DeleteByIds(ids);
            }
        }

        public static void RemoveAll<T>(this IRedis cache)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient().GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.DeleteAll();

            }
        }

        public static void RemoveAll<T>(this IRedis cache, long db)
        {
            using (var redisClient = Redis.RedisManager.GetPooledRedisClient(db).GetClient())
            {
                var tClient = redisClient.As<T>();
                tClient.DeleteAll();
            }
        }
    }
}
