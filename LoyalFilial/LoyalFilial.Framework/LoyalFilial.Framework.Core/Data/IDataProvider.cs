using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace LoyalFilial.Framework.Core.Data
{
    public interface IDataProvider
    {
        ITableCommandGenerate TableCommandGenerator { get; set; }

        string ConnectionString { get; set; }

        List<IDatabaseConnectionString> ConnectionStrings { get; set; }

        string GetConnectionString(string key);

        int ExecuteNonQuery(CommandType type, string commandText, params DbParameter[] parameterValues);

        int ExecuteNonQuery(string connectionString, CommandType type, string commandText,
            params DbParameter[] commandParameters);

        DbDataReader ExecuteReader(CommandType commandType, string commandText, params DbParameter[] commandParameters);

        DbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
            params DbParameter[] commandParameters);

        DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText,
            params DbParameter[] commandParameters);

        DataSet ExecuteDataset(CommandType commandType, string commandText, params DbParameter[] commandParameters);

        DbParameter GenerateParam(object value, string parameterName);
    }

    public interface IDatabaseConnectionString
    {
        string Key { get; set; }
        string ConnectionString { get; set; }
        bool IsDefault { get; set; }

        int MaxPools { get; set; }

        int ActiveConnectionCount { get; set; }
    }
}
