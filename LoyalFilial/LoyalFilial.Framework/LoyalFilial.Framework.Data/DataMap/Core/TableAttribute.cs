using System;

namespace LoyalFilial.Framework.Data.DataMap.Core
{
    public class TableAttribute : Attribute
    {
        public string DatabaseName { get; private set; }
        public string TableName { get; private set; }

        public TableAttribute(string databaseName, string tableName)
        {
            this.DatabaseName = databaseName;
            this.TableName = tableName;
        }
    }
}
