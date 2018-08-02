namespace LoyalFilial.Framework.Core
{
    public interface IModule : IConfigurable
    {
        /// <summary>
        /// 模块名
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 框架主体实例
        /// </summary>
        IFramework Framework { get; }

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="framework">IFramework</param>
        /// <param name="setting">对应的配置节</param>
        bool Config(IFramework framework, IConfigElement configElement);

        /// <summary>
        /// 刷新模块内置缓存
        /// </summary>
        bool RefreshCache();
    }
}
