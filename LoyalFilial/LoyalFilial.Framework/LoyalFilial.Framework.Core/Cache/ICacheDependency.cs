using System;

namespace LoyalFilial.Framework.Core.Cache
{
    /// <summary>
    /// 缓存过期策略接口
    /// </summary>
    public interface ICacheDependency
    {
        /// <summary>
        /// 是否过期
        /// </summary>
        bool IsExpired { get; set; }

        /// <summary>
        /// 是否为相对过期
        /// </summary>
        bool Sliding { get; set; }

        /// <summary>
        /// 绝对过期时间
        /// </summary>
        DateTime AbsoluteExpiration { get; }

        /// <summary>
        /// 相对过期时间
        /// </summary>
        TimeSpan SlidingExpiration { get; }

        /// <summary>
        /// 重置缓存策略（相当于重新开始缓存）
        /// </summary>
        void Reset();
    }
}
