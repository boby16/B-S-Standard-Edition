using ServiceStack.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace LoyalFilial.Framework.Cache
{
    public abstract class RedisClientFactory
    {
        internal class RedisClientPool
        {
            internal class ServiceStatckRedisClient : RedisClient
            {
                private readonly RedisClientFactory.RedisClientPool clientPool;
                public string SerializerName
                {
                    get
                    {
                        return "application/json; iso";
                    }
                }
                public ServiceStatckRedisClient(RedisClientFactory.RedisClientPool clientPool)
                    : base(clientPool.host, clientPool.port, db: clientPool.db)
                {
                    this.clientPool = clientPool;
                }
                protected override void Dispose(bool disposing)
                {
                    if (!disposing)
                    {
                        base.Dispose(false);
                        return;
                    }
                    if (base.HadExceptions)
                    {
                        base.Dispose(true);
                        return;
                    }
                    this.clientPool.clients.Push(this);
                }
            }

            private readonly string host;
            private readonly int port;
            private readonly long db;
            private readonly Stack clients = Stack.Synchronized(new Stack());
            public RedisClientPool(string host, int port, long db)
            {
                this.host = host;
                this.port = port;
                this.db = db;
            }
            public RedisClient GetRedisClient()
            {
                if (this.clients.Count > 0)
                {
                    lock (this.clients)
                    {
                        if (this.clients.Count > 0)
                        {
                            return (ServiceStatckRedisClient)this.clients.Pop();
                        }
                    }
                }
                return new ServiceStatckRedisClient(this);
            }
        }
        private static readonly Dictionary<string, RedisClientPool> clientPools = new Dictionary<string, RedisClientPool>(StringComparer.InvariantCultureIgnoreCase);


        public static RedisClient GetRedisClient(string serverName, long db)
        {
            return GetRedisClientInternal(serverName, db);
        }

        internal static RedisClient GetRedisClientInternal(string server, long db)
        {
            if (string.IsNullOrEmpty(server))
            {
                throw new ArgumentNullException("server");
            }
            RedisClientPool redisClientPool;
            if (!clientPools.TryGetValue((server + db), out redisClientPool))
            {
                lock (clientPools)
                {
                    if (!clientPools.TryGetValue((server + db), out redisClientPool))
                    {
                        string host = server;
                        int port = 6379;
                        string[] array = server.Split(new char[]
						{
							':'
						});
                        if (array.Length > 1)
                        {
                            host = array[0];
                            if (!int.TryParse(array[1], out port))
                            {
                                port = 6379;
                            }
                        }
                        redisClientPool = new RedisClientPool(host, port, db);
                        clientPools.Add((server + db), redisClientPool);
                    }
                }
            }
            return redisClientPool.GetRedisClient();
        }
    }
}
