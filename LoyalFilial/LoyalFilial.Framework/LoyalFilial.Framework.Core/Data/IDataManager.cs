using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;

namespace LoyalFilial.Framework.Core.Data
{
    public interface IDataManager : IModule
    {
        bool IsMultiOperation { get; set; }

        IDataProvider DataProvider { get; set; }

        #region Query

        ITableQuerySelect<T> TableQuery<T>() where T : class;
        ITableQuerySelect<T> TableQuery<T>(string connectionString) where T : class;

        ITableQuerySelect<T> TableQuery<T>(bool isMultiOperation) where T : class;
        ITableQuerySelect<T> TableQuery<T>(string connectionString, bool isMultiOperation) where T : class;

        #endregion

        #region Execute

        T Execute<T>(string sql) where T : class;
        T Execute<T>(string connectionString, string sql) where T : class;
        List<T> ExecuteList<T>(string sql) where T : class;
        List<T> ExecuteList<T>(string sql, out int totalCount) where T : class;
        List<T> ExecuteList<T>(string connectionString, string sql) where T : class;
        List<T> ExecuteList<T>(string connectionString, string sql, out int totalCount) where T : class;

        #endregion

        #region Insert

        ITableActResult Insert<T>(T entity, string tableAlias) where T : class;
        ITableActResult Insert<T>(T entity) where T : class;
        ITableActResult Insert<T>(string connectionString, T entity, string tableAlias) where T : class;
        ITableActResult Insert<T>(string connectionString, T entity) where T : class;

        ITableActResult Insert<T>(T entity, string tableAlias, bool includeIdAutoIncrease) where T : class;
        ITableActResult Insert<T>(T entity, bool includeIdAutoIncrease) where T : class;

        ITableActResult Insert<T>(string connectionString, T entity, string tableAlias, bool includeIdAutoIncrease)
            where T : class;

        ITableActResult Insert<T>(string connectionString, T entity, bool includeIdAutoIncrease) where T : class;

        ITableActResult Insert<T>(List<T> entityList, string tableAlias) where T : class;
        ITableActResult Insert<T>(List<T> entityList) where T : class;
        ITableActResult Insert<T>(string connectionString, List<T> entityList, string tableAlias) where T : class;
        ITableActResult Insert<T>(string connectionString, List<T> entityList) where T : class;

        #endregion

        #region Update

        ITableActResult Update<T>(T entity, string tableAlias,
            params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        ITableActResult Update<T>(T entity, params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        ITableActResult Update<T>(string connectionString, T entity, string tableAlias,
            params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        ITableActResult Update<T>(string connectionString, T entity,
            params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        ITableActResult Update<T>(List<T> entityList, string tableAlias,
            params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        ITableActResult Update<T>(List<T> entityList, params Expression<Func<T, object>>[] columnNameFileterExps)
            where T : class;

        ITableActResult Update<T>(string connectionString, List<T> entityList, string tableAlias,
            params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        ITableActResult Update<T>(string connectionString, List<T> entityList,
            params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;

        #endregion

        #region Delete

        ITableActResult Delete<T>(T entity, string tableAlias) where T : class;
        ITableActResult Delete<T>(T entity) where T : class;
        ITableActResult Delete<T>(string connectionString, T entity, string tableAlias) where T : class;
        ITableActResult Delete<T>(string connectionString, T entity) where T : class;

        ITableDelete<T> Delete<T>() where T : class;
        ITableDelete<T> Delete<T>(string connectionString) where T : class;
        ITableDelete<T> Delete<T>(string connectionString, string tableAlias) where T : class;

        #endregion

        IActTResult<T> Map<T>(NameValueCollection collection) where T : class;
    }
}