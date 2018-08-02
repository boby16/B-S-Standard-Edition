using System;
using System.Data;

namespace LoyalFilial.Framework.Data.DataMap.Source
{
    public class DataRowSource : IDataSource
    {
        public DataRow Data
        {
            get;
            set;
        }

        public DataRowSource(DataRow data)
        {
            this.Data = data;
        }

        bool IDataSource.HasField(string fieldName)
        {
            return Data.Table.Columns.Contains(fieldName);
        }

        object IDataSource.GetFieldValue(string fieldName)
        {
            return Data[fieldName] == DBNull.Value ? null : Data[fieldName];
        }
    }
}
