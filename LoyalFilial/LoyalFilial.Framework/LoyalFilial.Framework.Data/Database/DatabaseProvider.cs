using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LoyalFilial.Framework.Core.Data;
using System.Data;
using System.Data.Common;
using LoyalFilial.Framework.Core.Util;

namespace LoyalFilial.Framework.Data.Database
{
    public abstract class DatabaseProvider : IDataProvider
    {
        #region db provider implementation.

        public abstract DbConnection GetDbConnection();

        public abstract DbCommand GetDbCommand();

        public abstract DbDataAdapter GetDbDataAdapter();

        public abstract DbParameter GenerateParam(object value, string parameterName);

        #endregion

        #region Connection pool/size management.

        private static readonly object _countObj = new object();

        public virtual DbConnection GetDbConnection(string connectionString)
        {
            var connSetting = ConnectionStrings.FirstOrDefault(c => c.ConnectionString == connectionString);
            if (connSetting == null)
            {
                throw new Exception("No database config.");
            }
            else
            {
                if (connSetting.MaxPools > 0 && connSetting.MaxPools < connSetting.ActiveConnectionCount)
                {
                    throw new Exception(string.Format("Database connection pool limit for {0}:{1}", connSetting.MaxPools,
                        connSetting.ActiveConnectionCount));
                }
            }
            var conn = GetDbConnection();
            conn.ConnectionString = connectionString;
            lock (_countObj)
            {
                connSetting.ActiveConnectionCount++;
            }
            return conn;
        }

        private void ActiveConnection(string connectionString, int count)
        {
            var connSetting = ConnectionStrings.FirstOrDefault(c => c.ConnectionString == connectionString);
            if (connSetting != null)
            {
                lock (_countObj)
                {
                    connSetting.ActiveConnectionCount = connSetting.ActiveConnectionCount + count;
                }
            }
        }

        #endregion

        public string ConnectionString { get; set; }

        public List<IDatabaseConnectionString> ConnectionStrings { get; set; }

        public string GetConnectionString(string key)
        {
            var cs = ConnectionStrings.FirstOrDefault(c => c.Key == key);
            if (cs != null)
                return cs.ConnectionString;
            return null;
        }

        public ITableCommandGenerate TableCommandGenerator { get; set; }

        #region Db execution.

        public int ExecuteNonQuery(CommandType type, string commandText, params DbParameter[] parameterValues)
        {
            return ExecuteNonQuery(null, type, commandText, parameterValues);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText,
            params DbParameter[] commandParameters)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                connectionString = this.ConnectionString;
            using (var conn = this.GetDbConnection(connectionString))
            {
                bool mustCloseConnection = true;
                try
                {
                    var command = this.GetDbCommand();
                    PrepareCommand(command, conn, null, commandType, commandText, commandParameters,
                        out mustCloseConnection);
                    var result = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    return result;
                }
                finally
                {
                    ActiveConnection(connectionString, -1);
                    if (mustCloseConnection)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        public DbDataReader ExecuteReader(CommandType commandType, string commandText,
            params DbParameter[] commandParameters)
        {
            return ExecuteReader(null, commandType, commandText, commandParameters);
        }

        public DbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
            params DbParameter[] commandParameters)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                connectionString = this.ConnectionString;
            using (var conn = this.GetDbConnection(connectionString))
            {
                try
                {
                    var command = this.GetDbCommand();
                    bool mustCloseConnection = true;
                    PrepareCommand(command, conn, null, commandType, commandText, commandParameters,
                        out mustCloseConnection);
                    var adapter = this.GetDbDataAdapter();
                    adapter.SelectCommand = command;
                    var dr = command.ExecuteReader(CommandBehavior.Default);
                    command.Parameters.Clear();
                    return dr;
                }
                catch (Exception)
                {
                    if (conn != null)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                    throw;
                }
                finally
                {
                    ActiveConnection(connectionString, -1);
                }
            }
        }

        public DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText,
            params DbParameter[] commandParameters)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                connectionString = this.ConnectionString;
            using (var conn = this.GetDbConnection(connectionString))
            {
                bool mustCloseConnection = true;
                try
                {
                    var command = this.GetDbCommand();
                    PrepareCommand(command, conn, null, commandType, commandText, commandParameters,
                        out mustCloseConnection);
                    var adapter = this.GetDbDataAdapter();
                    adapter.SelectCommand = command;
                    var ds = new DataSet();
                    adapter.Fill(ds);
                    command.Parameters.Clear();
                    return ds;
                }
                finally
                {
                    ActiveConnection(connectionString, -1);
                    if (mustCloseConnection)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText,
            params DbParameter[] commandParameters)
        {
            return ExecuteDataset(null, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        /// <param name="mustCloseConnection">是否需要关闭数据库连接</param>
        protected virtual void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType,
            string cmdText, DbParameter[] cmdParms, out bool mustCloseConnection)
        {
            if (conn.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                conn.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion
    }
}