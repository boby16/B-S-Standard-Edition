using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace LoyalFilial.Framework.Core.Data
{
    public interface ITableCommandGenerate
    {
        string ParaPrefix { get; }
        #region Query

        string Select(string[] fields);

        string From(string tableName);

        string Where(string whereConditions);

        string OrderBy(string[] fields, string orderby);

        string Page(int PageSize, int pageIndex);

        string CommandText(ITableCommand command);

        #endregion

        #region Update

        string Update(string tableName);
        string Set(string[] fields);
        string Where(string[] fields);

        string Update<T>(List<T> entityList, string tableAlias, out List<DbParameter> pList,
                         params Expression<Func<T, object>>[] columnNameFileterExps) where T : class;
        string CommandText(ITableUpdateCommand command);

        #endregion

        #region Insert

        string Insert(string tableName);
        string IntoValue(string[] fields);
        string IntoValue<T>(List<T> entityList, string tableAlias, out List<DbParameter> pList);
        string CommandText(ITableInsertCommand command);

        #endregion

        #region Delete

        string Delete(string tableName);
        string CommandText(ITableDeleteCommand command);

        #endregion
    }
}
