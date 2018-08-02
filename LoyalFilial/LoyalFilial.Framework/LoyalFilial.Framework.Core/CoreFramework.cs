using System;
using System.Collections.Generic;
using System.Linq;
using LoyalFilial.Framework.Core.Util;
using System.Xml.Linq;
using LoyalFilial.Framework.Core.Config;
using System.IO;

namespace LoyalFilial.Framework.Core
{
    public abstract class CoreFramework : IFramework
    {
        protected string _id;
        protected bool _initialized;
        protected List<IModule> _modules;
        protected IConfigManager _configManager;
        protected List<Exception> _exceptionList = null;

        public event OnFrameworkInitialized Initialized;

        protected CoreFramework()
        {
        }

        /// <summary>
        /// 根据配置信息，装载框架组件列表
        /// </summary>
        /// <param name="configElement">框架配置信息</param>
        /// <returns>框架组件对象列表</returns>
        protected virtual List<IModule> LoadModule(IConfigElement configElement)
        {
            _modules = new List<IModule>();
            XElement xElement = XmlHelper.ToXElement(configElement.XmlElement);
            _id = xElement.Attribute(Constants.Config_DevFxId).Value;
            foreach (XElement m in xElement.Elements(Constants.Config_Module))
            {
                try
                {
                    var typeConfig = m.Attribute(Constants.Config_Module_Type);
                    if (typeConfig == null || StringHelper.IsNullOrEmptyOrBlankString(typeConfig.Value))
                        break;
                    var typeAssembly = typeConfig.Value.Split(new char[] { ',' });
                    if (typeAssembly.Length != 2 || StringHelper.IsNullOrEmptyOrBlankString(typeAssembly[0]) || StringHelper.IsNullOrEmptyOrBlankString(typeAssembly[1]))
                        break;
                    var file = string.Format(Constants.Config_Module_Assembly,
                                             AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                             typeAssembly[1].Trim());
                    if (!File.Exists(file))
                    {
                        file = string.Format(Constants.Config_Module_Assembly_Web,
                                             AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                             typeAssembly[1].Trim());
                    }
                    IModule module = TypeHelper.CreateObject(file, typeAssembly[0].Trim(), false) as IModule;
                    if (module != null)
                    {
                        var xName = m.Attribute(Constants.Config_Module_Name);
                        module.Name = xName == null ? "" : xName.Value;
                        module.Config(this, new ConfigElement(null, XmlHelper.ToXmlElement(m)));
                        _modules.Add(module);
                    }
                }
                catch (Exception ex)
                {
                    if (_exceptionList == null)
                    {
                        _exceptionList = new List<Exception>();
                    }
                    _exceptionList.Add(ex);
                    continue;
                }
            }
            return _modules;
        }

        #region IFramework 成员

        public string Id
        {
            get { return _id; }
        }

        public IModule GetModule(string moduleName)
        {
            return this.ModuleList.First(im => im.Name == moduleName);
        }

        public List<IModule> ModuleList
        {
            get { return _modules; }
        }

        public IConfigManager ConfigManager
        {
            get
            {
                return _configManager;
            }
            set
            {
                _configManager = value;
            }
        }

        public bool Init(IConfigManager configManager, bool isForce)
        {
            if (configManager == null && _configManager == null)
                throw new Exception(Constants.Error_NeedConfigMananger);
            if (configManager != null)
                _configManager = configManager;
            return this.Config(_configManager.LoadConfig(), isForce);
        }

        public bool Init(bool isForce)
        {
            return Init(null, isForce);
        }

        public List<Exception> ExceptionList
        {
            get { return _exceptionList; }
        }

        #endregion

        #region IConfigurable 成员

        public bool ConfigInitialized
        {
            get { return _initialized; }
        }

        public bool Config(IConfigElement configElement)
        {
            return Config(configElement, false);
        }

        public bool Config(IConfigElement configElement, bool isForce)
        {
            if (!this._initialized || isForce)
            {
                this._modules = new List<IModule>();
                this._modules = this.LoadModule(configElement);
                this._initialized = true;
                if (Initialized != null)
                {
                    Initialized();
                }
            }
            return this._initialized;
        }

        #endregion
    }
}
