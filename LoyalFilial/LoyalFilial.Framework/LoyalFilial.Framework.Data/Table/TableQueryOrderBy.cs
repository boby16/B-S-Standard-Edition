using System;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data.ScriptExpression;

namespace LoyalFilial.Framework.Data.Table
{
    public class TableQueryOrderBy<T> : TableQueryWhere<T>, ITableQueryOrderBy<T> where T : class
    {
        public TableQueryOrderBy(IDataManager dataManager, ITableCommand command)
            : base(dataManager, command)
        {
            this.DataManager = dataManager;
            this.Command = command;
        }


        public ITableQueryOrderBy<T> OrderByAsc(
            params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameExps)
        {
            return OrderBy(true, columnNameExps);
        }

        public ITableQueryOrderBy<T> OrderByDesc(
            params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameExps)
        {
            return OrderBy(false, columnNameExps);
        }

        private TableQueryOrderBy<T> OrderBy(bool isAsc,
                                             params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameExps)
        {
            var fields = ExpressionHelper.GetPropertyMapNamesFromExps(columnNameExps);
            for (var i = 0; i < fields.Length; i++)
            {
                fields[i] = DataMap.DataMapper.FindColumnNameByPropertyName<T>(fields[i]);
            }

            if (isAsc)
            {

                this.Command.CommandText_OrderBy_Asc =
                    this.DataManager.DataProvider.TableCommandGenerator.OrderBy(fields,
                                                                                this.Command.CommandText_OrderBy_Asc);
            }
            else
            {
                this.Command.CommandText_OrderBy_Desc =
                    this.DataManager.DataProvider.TableCommandGenerator.OrderBy(fields,
                                                                                this.Command.CommandText_OrderBy_Desc);
            }
            return this;
        }

        //private string[] FindFieldList<T>(System.Linq.Expressions.Expression<Func<T, object>>[] columnNameExps)
        //    where T : class
        //{
        //    var fields = ExpressionHelper.GetPropertyMapNamesFromExps(columnNameExps);
        //    if (fields == null || fields.Length == 0)
        //        return fields;
        //    for (var i = 0; i < fields.Length; i++)
        //    {
        //        fields[i] = DataMap.DataMapper.FindColumnNameByPropertyName<T>(fields[i]);
        //    }
        //    return fields;
        //}

        //public ITableQueryWhere<T> OrderBy(System.Linq.Expressions.Expression<Func<T, object>>[] ascColumnNameExps,
        //                                   System.Linq.Expressions.Expression<Func<T, object>>[] descColumnNameExps)
        //{
        //    var ascFields = FindFieldList(ascColumnNameExps);
        //    var descFields = FindFieldList(descColumnNameExps);
        //    this.Command.CommandText_OrderBy = this.DataManager.DataProvider.TableCommandGenerator.OrderBy(ascFields,
        //                                                                                                   descFields);
        //    return new TableQueryWhere<T>(this.DataManager, this.Command);
        //}
    }
}