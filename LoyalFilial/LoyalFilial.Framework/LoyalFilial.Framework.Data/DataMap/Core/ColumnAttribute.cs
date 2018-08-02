using System;

namespace LoyalFilial.Framework.Data.DataMap.Core
{
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsIdAutoIncrease { get; private set; }
        public bool IsIgnore { get; private set; }

        public ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
            this.IsPrimaryKey = false;
            this.IsIdAutoIncrease = false;
        }

        public ColumnAttribute(string columnName, bool isPrimaryKey)
        {
            this.IsPrimaryKey = isPrimaryKey;
            this.ColumnName = columnName;
            this.IsIdAutoIncrease = false;
        }

        public ColumnAttribute(string columnName, bool isPrimaryKey, bool isIdAutoIncrease)
        {
            this.IsPrimaryKey = isPrimaryKey;
            this.ColumnName = columnName;
            this.IsIdAutoIncrease = isIdAutoIncrease;
        }

        public ColumnAttribute(bool isIgnore)
        {
            this.IsIgnore = isIgnore;
        }
    }
}