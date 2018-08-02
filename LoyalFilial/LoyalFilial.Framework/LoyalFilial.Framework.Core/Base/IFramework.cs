using System;
using System.Collections.Generic;

namespace LoyalFilial.Framework.Core
{
    public delegate void OnFrameworkInitialized();

    public interface IFramework : IConfigurable
    {
        /// <summary>
        /// 框架标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 根据模块名称，获取模块实例
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>模块实例对象</returns>
        IModule GetModule(string moduleName);

        /// <summary>
        /// 框架装载的模块列表
        /// </summary>
        List<IModule> ModuleList { get; }

        /// <summary>
        /// 框架配置管理器
        /// </summary>
        IConfigManager ConfigManager { get; set; }

        /// <summary>
        /// 框架初始化
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        /// <param name="isForce">是否强制重新初始化</param>
        /// <returns>是否初始化完成</returns>
        bool Init(IConfigManager configManager, bool isForce);

        /// <summary>
        /// 框架初始化
        /// </summary>
        /// <param name="isForce">是否强制重新初始化</param>
        /// <returns>是否初始化完成</returns>
        bool Init(bool isForce);

        /// <summary>
        /// 框架初始化的错误信息
        /// </summary>
        List<Exception> ExceptionList { get; }

        event OnFrameworkInitialized Initialized;
    }
}
