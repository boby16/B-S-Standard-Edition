using System;
using LoyalFilial.Framework.Core.Cache;

namespace LoyalFilial.Framework.Cache
{
    public class CacheDependency : ICacheDependency
    {
        public CacheDependency()
        {
            IsExpired = false;
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsExpired { get; set; }

        /// <summary>
        /// 重置缓存策略（相当于重新开始缓存）
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// 是否为相对过期
        /// </summary>
        public bool Sliding { get; set; }

        /// <summary>
        /// 绝对过期时间
        /// </summary>
        public DateTime AbsoluteExpiration { get; private set; }

        /// <summary>
        /// 相对过期时间
        /// </summary>
        public TimeSpan SlidingExpiration { get; private set; }


        /// <summary>
        /// 创建永不过期的缓存策略
        /// </summary>
        /// <returns><see cref="ICacheDependency"/></returns>
        public static ICacheDependency Create()
        {
            return new CacheDependency();
        }

        /// <summary>
        /// 创建绝对时间过期的缓存策略
        /// </summary>
        /// <param name="absoluteExpiration">绝对时间</param>
        /// <returns><see cref="ICacheDependency"/></returns>
        public static ICacheDependency Create(DateTime absoluteExpiration)
        {
            var cd = new CacheDependency
                {
                    AbsoluteExpiration = absoluteExpiration,
                    SlidingExpiration = TimeSpan.MaxValue,
                    Sliding = false,
                    IsExpired = true
                };
            return cd;
        }

        /// <summary>
        /// 创建相对时间过期的缓存策略
        /// </summary>
        /// <param name="slidingExpiration">相对时间</param>
        /// <returns><see cref="ICacheDependency"/></returns>
        public static ICacheDependency Create(TimeSpan slidingExpiration)
        {
            var cd = new CacheDependency
                {
                    SlidingExpiration = slidingExpiration,
                    AbsoluteExpiration = DateTime.Now.Add(slidingExpiration),
                    Sliding = true,
                    IsExpired = true
                };
            return cd;
        }
    }
}
