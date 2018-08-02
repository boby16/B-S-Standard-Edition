namespace LoyalFilial.Framework.Cache
{
    internal class CacheConstants
    {

        #region mc

        public const string CacheName = "CacheName";
        public const string Servers = "Servers";
        public const string SendReceiveTimeout = "SendReceiveTimeout";
        public const string ConnectTimeout = "ConnectTimeout";
        public const string MinPoolSize = "MinPoolSize";
        public const string MaxPoolSize = "MaxPoolSize";
        public const string Region = "Region";
        public const string Expiration = "ExpirationTime";
        public const string Value = "value";

        public const string Error_Init = "缓存初始化失败";

        public const string Root = "Cache";
        public const string Element_Type = "type";

        public const string T_MemCached = "MemCached";
        public const string T_LocalCached = "LocalCached";
        public const string T_Redis = "Redis";


        public const string Config_CachedName = "RosetteStoneCache";
        public const string Config_CachedServers = "192.168.2.169:11211,192.168.2.169:11211";
        public const int Config_SendReceiveTimeout = 5000;
        public const int Config_ConnectTimeout = 5000;
        public const uint Config_MinPoolSize = 10;
        public const uint Config_MaxPoolSize = 50;
        public const string Config_ExpirationTime = "30";
        public const string Config_Region = "Rs_";


        #endregion

        #region redis
        public const string R_V_MaxWritePoolSize = "1000";
        public const string R_V_MaxReadPoolSize = "1000";
        public const string R_V_ReadWriteHosts = "192.168.2.169:6379";
        public const string R_V_ReadOnlyHosts = "192.168.2.169:6379";
        public const string R_V_InitalDb = "0";


        public const string R_MaxWritePoolSize = "MaxWritePoolSize";
        public const string R_MaxReadPoolSize = "MaxReadPoolSize";
        public const string R_ReadWriteHosts = "ReadWriteHosts";
        public const string R_ReadOnlyHosts = "ReadOnlyHosts";
        public const string R_InitalDb = "InitalDb";

        #endregion
    }
}
