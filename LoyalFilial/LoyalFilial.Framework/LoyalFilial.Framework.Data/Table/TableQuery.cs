using System;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Data.ScriptExpression;
using LoyalFilial.Framework.Data.Database;
using LoyalFilial.Framework.Core.Util;

namespace LoyalFilial.Framework.Data.Table
{
    internal class TableQuery<T> : TableBaseQuery, ITableQuerySelect<T> where T : class
    {
        public string ConnectionString { get; set; }

        public ITableQueryFrom<T> Select(params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameFileterExps)
        {
            var command = new TableQueryCommand();
            if (StringHelper.IsNullOrEmptyOrBlankString(ConnectionString))
                command.ConnectionString = this.DataManager.DataProvider.ConnectionString;
            else
                command.ConnectionString = ConnectionString;
            var fields = ExpressionHelper.GetPropertyMapNamesFromExps(columnNameFileterExps);
            for (var i = 0; i < fields.Length; i++)
            {
                fields[i] = DataMap.DataMapper.FindColumnNameByPropertyName<T>(fields[i]);
            }
            command.CommandText_Select = this.DataManager.DataProvider.TableCommandGenerator.Select(fields);
            ITableQueryFrom<T> iFrom = new TableQueryFrom<T>(this.DataManager, command);
            return iFrom;
        }

        public TableQuery()
            : base()
        {

        }

        public TableQuery(string connectionString)
            : base()
        {
            ConnectionString = connectionString;
        }
    }
}
