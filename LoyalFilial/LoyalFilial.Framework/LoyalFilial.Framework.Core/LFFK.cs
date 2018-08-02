using System;
using System.Collections.Generic;
using LoyalFilial.Framework.Core.Cache;
using LoyalFilial.Framework.Core.Config;
using LoyalFilial.Framework.Core.Log;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Mail;

namespace LoyalFilial.Framework.Core
{
    public sealed class LFFK : CoreFramework
    {
        #region 初始化

        static LFFK()
        {
            Init(false);
        }

        /// <summary>
        /// 私有构造函数，防止外部实例化
        /// </summary>
        private LFFK() { }

        private static readonly object _initLockObject = new object();
        private static bool _initializing;
        private static LFFK _framework;

        public static void Init(bool isForce)
        {
            if ((_framework == null && !_initializing) || isForce)
            {
                lock (_initLockObject)
                {
                    if ((_framework == null && !_initializing) || isForce)
                    {
                        _framework = new LFFK();
                        _initializing = true;
                        if (_framework.ConfigManager == null)
                            _framework.ConfigManager = new ConfigManager();
                        _framework.Init(_framework.ConfigManager, isForce);
                    }
                }
            }
        }

        static void EnsureInit()
        {
            if (_framework == null || !_framework.Initialized)
                Init(true);
        }

        public bool Initialized
        {
            get
            {
                if (_framework == null) return false;
                return ((CoreFramework)_framework).ConfigInitialized;
            }
        }

        #endregion

        #region 框架初始化异常列表

        public static new List<Exception> ExceptionList
        {
            get { return ((IFramework)_framework).ExceptionList; }
        }

        #endregion

        #region 组件

        /// <summary>
        /// 获取指定的模块
        /// </summary>
        /// <param name="moduleName">模块名</param>
        /// <returns>IModule</returns>
        public new static IModule GetModule(string moduleName)
        {
            return ((IFramework)_framework).GetModule(moduleName);
        }

        /// <summary>
        /// 所有模块
        /// </summary>
        public new static List<IModule> ModuleList
        {
            get
            {
                EnsureInit();
                return ((IFramework)_framework).ModuleList;
            }
        }

        public static IConfigManager CurrentConfigManager
        {
            get { return ((IFramework)_framework).ConfigManager; }
            set
            {
                ((IFramework)_framework).ConfigManager = value;
            }
        }

        public static ILogManager LogManager
        {
            get
            {
                EnsureInit();
                return ((IFramework)_framework).ModuleList.Find(l => l is ILogManager) as ILogManager;
            }
        }

        public static IDataManager DataManager
        {
            get
            {
                EnsureInit();
                return ((IFramework)_framework).ModuleList.Find(l => l is IDataManager) as IDataManager;
            }
        }

        public static ICacheManager CacheManager
        {
            get
            {
                EnsureInit();
                return ((IFramework)_framework).ModuleList.Find(l => l is ICacheManager) as ICacheManager;
            }
        }
 
        public static IMailManager MailManager
        {
            get
            {
                EnsureInit();
                return ((IFramework)_framework).ModuleList.Find(l => l is IMailManager) as IMailManager;
            }
        }
        #endregion
    }
}
