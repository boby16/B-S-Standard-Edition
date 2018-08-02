namespace LoyalFilial.Framework.Core
{
    public interface IConfigManager
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        IConfigElement LoadConfig();

        /// <summary>
        /// 获取给定模块的配置信息
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns>IConfigElement</returns>
        IConfigElement LoadConfig(string moduleName);
    }
}
