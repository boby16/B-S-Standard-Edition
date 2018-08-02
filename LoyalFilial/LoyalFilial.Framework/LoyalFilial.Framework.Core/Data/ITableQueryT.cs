using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LoyalFilial.Framework.Core.Data
{
    public interface IDatabaseConnect
    {
        string ConnectionString { get; set; }
    }

    public interface ITableCommand : IDatabaseConnect
    {
        string CommandText_Select { get; set; }
        string CommandText_From { get; set; }
        string CommandText_Where { get; set; }
        string CommandText_OrderBy_Asc { get; set; }
        string CommandText_OrderBy_Desc { get; set; }
        string CommandText_Page { get; set; }
    }

    public interface ITableUpdateCommand : IDatabaseConnect
    {
        string CommandText_Update { get; set; }
        string CommandText_Set { get; set; }
        string CommandText_Where { get; set; }
    }

    public interface ITableInsertCommand : IDatabaseConnect
    {
        string CommandText_Insert { get; set; }
        string CommandText_IntoValue { get; set; }
    }

    public interface ITableDeleteCommand : IDatabaseConnect
    {
        string CommandTextDelete { get; set; }
        string CommandText_Where { get; set; }
    }

    public interface ITableQueryCommand
    {
        ITableCommand Command { get; set; }

        IDataManager DataManager { get; set; }
    }

    public interface ITableQuerySelect<T> : ITableQueryCommand where T : class
    {
        ITableQueryFrom<T> Select(params Expression<Func<T, object>>[] columnNameFileterExps);
    }

    public interface ITableQueryFrom<T> : ITableQueryCommand where T : class
    {
        ITableQueryWhere<T> From(string tableAlias);
        ITableQueryWhere<T> From();
    }

    public interface ITableQueryWhere<T> : ITableQueryCommand, ITableQueryExecute<T> where T : class
    {
        ITableQueryOrderBy<T> Where(Expression<Func<T, bool>> columnNameExp);
        ITableQueryOrderBy<T> Where();
    }

    public interface ITableQueryOrderBy<T> : ITableQueryCommand, ITableQueryExecute<T> where T : class
    {
        ITableQueryOrderBy<T> OrderByAsc(params Expression<Func<T, object>>[] columnNameExps);
        ITableQueryOrderBy<T> OrderByDesc(params Expression<Func<T, object>>[] columnNameExps);
    }

    public interface ITableQueryExecute<T> where T : class
    {
        T Execute();
        List<T> ExecuteList();
        List<T> ExecuteList(int pageSize, int pageNumber, out int totalCount);
    }
}