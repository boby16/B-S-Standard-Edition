using System;
using System.Data.Common;

namespace LoyalFilial.Framework.Data.DataMap.Source
{
    public class DataReaderSource : IDataSource
    {
        public DbDataReader Data
        {
            get;
            set;
        }

        public DataReaderSource(DbDataReader data)
        {
            this.Data = data;
        }

        bool IDataSource.HasField(string fieldName)
        {
            return Data[fieldName] != null;
        }

        object IDataSource.GetFieldValue(string fieldName)
        {
            return Data[fieldName] == DBNull.Value ? null : Data[fieldName];
        }
    }
}
