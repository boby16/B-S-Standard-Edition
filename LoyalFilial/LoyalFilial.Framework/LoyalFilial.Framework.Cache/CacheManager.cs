using System;
using System.Xml;
using System.Xml.Linq;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Cache;
using LoyalFilial.Framework.Core.Util;

namespace LoyalFilial.Framework.Cache
{
    public class CacheManager : ICacheManager
    {
        public ICache MemCached { get; set; }
        public ICache Localcached { get; set; }
        public IRedis Redis { get; set; }

        #region IConfigurable 成员
        public bool ConfigInitialized { get; private set; }

        public bool Config(IConfigElement configElement)
        {
            return this.Config(this.Framework, configElement, false);
        }

        public bool Config(IConfigElement configElement, bool isForce)
        {
            return this.Config(this.Framework, configElement, isForce);
        }
        #endregion

        #region IModule 成员
        public string Name { get; set; }
        public IFramework Framework
        {
            get;
            private set;
        }

        public bool Config(IFramework framework, IConfigElement configElement)
        {
            return this.Config(framework, configElement, true);
        }

        public bool RefreshCache()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Config



        protected bool Config(IFramework framework, IConfigElement configElement, bool isForce)
        {
            this.Framework = framework;
            if (!this.ConfigInitialized || isForce)
            {
                Init(configElement.XmlElement);
                this.ConfigInitialized = true;
            }
            return true;
        }

        private bool Init(XmlElement xmlElement)
        {
            if (xmlElement == null)
                throw new Exception(string.Format(Constants.Error_NoConfigForModule, Constants.Module_Log_Name));
            try
            {
                XElement xElement = XmlHelper.ToXElement(xmlElement);
                if (xElement != null)
                {
                    foreach (XElement m in xElement.Elements(CacheConstants.Root))
                    {
                        if (m != null)
                        {
                            var cacheType = m.Attribute(CacheConstants.Element_Type).Value;
                            if (cacheType.ToUpper() == CacheConstants.T_MemCached.ToUpper())
                            {
                                var cacheName = CacheConstants.Config_CachedName;
                                var cacheNameConfig = m.Element(CacheConstants.CacheName);
                                if (cacheNameConfig != null)
                                    cacheName = cacheNameConfig.Attribute(CacheConstants.Value).Value;

                                var servers = CacheConstants.Config_CachedServers;
                                var serversConfig = m.Element(CacheConstants.Servers);
                                if (serversConfig != null)
                                    servers = serversConfig.Attribute(CacheConstants.Value).Value;

                                var sendReceiveTimeout = CacheConstants.Config_SendReceiveTimeout;
                                var sendReceiveTimeoutConfig = m.Element(CacheConstants.SendReceiveTimeout);
                                if (sendReceiveTimeoutConfig != null)
                                    sendReceiveTimeout = Convert.ToInt32(sendReceiveTimeoutConfig.Attribute(CacheConstants.Value).Value);

                                var connectTimeout = CacheConstants.Config_ConnectTimeout;
                                var connectTimeoutConfig = m.Element(CacheConstants.SendReceiveTimeout);
                                if (connectTimeoutConfig != null)
                                    connectTimeout = Convert.ToInt32(connectTimeoutConfig.Attribute(CacheConstants.Value).Value);

                                var minPoolSize = CacheConstants.Config_MinPoolSize;
                                var minPoolSizeConfig = m.Element(CacheConstants.MinPoolSize);
                                if (minPoolSizeConfig != null)
                                    minPoolSize = Convert.ToUInt32(minPoolSizeConfig.Attribute(CacheConstants.Value).Value);

                                var maxPoolSize = CacheConstants.Config_MaxPoolSize;
                                var maxPoolSizeConfig = m.Element(CacheConstants.MaxPoolSize);
                                if (maxPoolSizeConfig != null)
                                    maxPoolSize = Convert.ToUInt32(maxPoolSizeConfig.Attribute(CacheConstants.Value).Value);

                                var region = CacheConstants.Config_Region;
                                var regionConfig = m.Element(CacheConstants.Region);
                                if (regionConfig != null)
                                    region = regionConfig.Attribute(CacheConstants.Value).Value;

                                var time = CacheConstants.Config_ExpirationTime;
                                var timeConfig = m.Element(CacheConstants.Expiration);
                                if (timeConfig != null)
                                    time = timeConfig.Attribute(CacheConstants.Value).Value;

                                var expiration = new TimeSpan(0, 0, 30, 0);
                                if (!string.IsNullOrEmpty(time))
                                    expiration = new TimeSpan(0, 0, Convert.ToInt32(time), 0);
                                this.MemCached = new MemCached(cacheName, servers, sendReceiveTimeout, connectTimeout, minPoolSize, maxPoolSize, region, expiration);
                            }
                            if (cacheType.ToUpper() == CacheConstants.T_LocalCached.ToUpper())
                            {
                                var region = CacheConstants.Config_Region;
                                var regionConfig = m.Element(CacheConstants.Region);
                                if (regionConfig != null)
                                    region = regionConfig.Attribute(CacheConstants.Value).Value;

                                var time = CacheConstants.Config_ExpirationTime;
                                var timeConfig = m.Element(CacheConstants.Expiration);
                                if (timeConfig != null)
                                    time = timeConfig.Attribute(CacheConstants.Value).Value;

                                var expiration = new TimeSpan(0, 0, 30, 0);
                                if (!string.IsNullOrEmpty(time))
                                    expiration = new TimeSpan(0, 0, Convert.ToInt32(time), 0);

                                this.Localcached = new LocalCached(region, expiration);
                            }

                            if (cacheType.ToUpper() == CacheConstants.T_Redis.ToUpper())
                            {
                                var maxWritePoolSize = CacheConstants.R_V_MaxWritePoolSize;
                                var maxWritePoolSizeConfig = m.Element(CacheConstants.R_MaxWritePoolSize);
                                if (maxWritePoolSizeConfig != null)
                                    maxWritePoolSize = maxWritePoolSizeConfig.Attribute(CacheConstants.Value).Value;

                                var maxReadPoolSize = CacheConstants.R_V_MaxReadPoolSize;
                                var maxReadPoolSizeConfig = m.Element(CacheConstants.R_MaxReadPoolSize);
                                if (maxReadPoolSizeConfig != null)
                                    maxReadPoolSize = maxReadPoolSizeConfig.Attribute(CacheConstants.Value).Value;

                                var readWriteHosts = CacheConstants.R_V_ReadWriteHosts;
                                var readWriteHostsConfig = m.Element(CacheConstants.R_ReadWriteHosts);
                                if (readWriteHostsConfig != null)
                                    readWriteHosts = readWriteHostsConfig.Attribute(CacheConstants.Value).Value;

                                var readOnlyHosts = CacheConstants.R_V_ReadOnlyHosts;
                                var readOnlyHostsConfig = m.Element(CacheConstants.R_ReadOnlyHosts);
                                if (readOnlyHostsConfig != null)
                                    readOnlyHosts = readOnlyHostsConfig.Attribute(CacheConstants.Value).Value;

                                var initalDb = CacheConstants.R_V_InitalDb;
                                var initalDbConfig = m.Element(CacheConstants.R_InitalDb);
                                if (initalDbConfig != null)
                                    initalDb = initalDbConfig.Attribute(CacheConstants.Value).Value;

                                var time = CacheConstants.Config_ExpirationTime;
                                var timeConfig = m.Element(CacheConstants.Expiration);
                                if (timeConfig != null)
                                    time = timeConfig.Attribute(CacheConstants.Value).Value;

                                var expiration = new TimeSpan(0, 0, 30, 0);
                                if (!string.IsNullOrEmpty(time))
                                    expiration = new TimeSpan(0, 0, Convert.ToInt32(time), 0);

                                this.Redis = new Redis(Convert.ToInt32(maxWritePoolSize), Convert.ToInt32(maxReadPoolSize), readWriteHosts.Split(','), readOnlyHosts.Split(','), Convert.ToInt64(initalDb), expiration);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Error_Data_InitConfigFailed, ex);
            }
        }

        #endregion
    }
}
