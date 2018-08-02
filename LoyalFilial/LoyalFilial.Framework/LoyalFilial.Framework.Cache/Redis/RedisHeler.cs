using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using LoyalFilial.Framework.Core.Cache;
using ServiceStack.Redis;

namespace LoyalFilial.Framework.Cache
{
    public static class RedisHeler
    {
        public static IRedisClient GetRedisClientFormPooled(this IRedis cache)
        {
            return Redis.RedisManager.GetPooledRedisClient().GetClient();
        }

        public static IRedisClient GetRedisClientFormPooled(this IRedis cache, long db)
        {
            return Redis.RedisManager.GetPooledRedisClient(db).GetClient();
        }

        public static RedisClient GetPersistentRedisClient(this IRedis cache)
        {
            return Redis.RedisManager.GetPersistentRedisClient();
        }

        public static RedisClient GetPersistentRedisClient(this IRedis cache, long db)
        {
            return Redis.RedisManager.GetPersistentRedisClient(db);
        }

        /// <summary>
        /// 将一个object对象序列化，返回一个byte[]
        /// </summary>
        /// <param name="obj">能序列化的对象</param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /**/
        /// <summary>
        /// 将一个序列化后的byte[]数组还原
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }
    }
}
