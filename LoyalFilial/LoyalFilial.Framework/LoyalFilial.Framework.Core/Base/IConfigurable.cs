namespace LoyalFilial.Framework.Core
{
    public interface IConfigurable
    {
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        bool ConfigInitialized { get; }

        /// <summary>
        /// 装载配置项
        /// </summary>
        /// <param name="configElement">配置内容</param>
        /// <returns>装载是否成功</returns>
        bool Config(IConfigElement configElement);

        /// <summary>
        /// 装载配置项
        /// </summary>
        /// <param name="configElement">配置内容</param>
        /// <param name="isForce">是否强制更新配置项内容</param>
        /// <returns>装载是否成功</returns>
        bool Config(IConfigElement configElement, bool isForce);
    }
}
