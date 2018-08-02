using LoyalFilial.Framework.Core.Data;

namespace LoyalFilial.Framework.Data.Database
{
    public class DatabaseConnectionString : IDatabaseConnectionString
    {
        public string Key { get; set; }

        public string ConnectionString { get; set; }

        public bool IsDefault { get; set; }

        public int MaxPools { get; set; }

        public int ActiveConnectionCount { get; set; }
    }
}