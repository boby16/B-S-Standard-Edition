namespace LoyalFilial.Framework.Core.Cache
{
    /// <summary>
    /// 缓存器接口
    /// </summary>
    public interface ICacheManager : IModule
    {
        ICache MemCached { get; set; }
        ICache Localcached { get; set; }
        IRedis Redis { get; set; }
    }
}
