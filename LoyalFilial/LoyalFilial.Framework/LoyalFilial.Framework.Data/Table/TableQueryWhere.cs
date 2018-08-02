using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Data.ScriptExpression;
using LoyalFilial.Framework.Data.DataMap;
using LoyalFilial.Framework.Data.DataMap.Source;


namespace LoyalFilial.Framework.Data.Table
{
    public class TableQueryWhere<T> : TableBaseQuery, ITableQueryWhere<T> where T : class
    {
        public TableQueryWhere()
        { }

        public TableQueryWhere(IDataManager dataManager, ITableCommand command)
        {
            this.DataManager = dataManager;
            this.Command = command;
        }

        public ITableQueryOrderBy<T> Where(Expression<Func<T, bool>> expr)
        {
            //TODO:analysis where expression.
            var translator = new TableQueryTranslator<T>();
            translator.Translate(expr);

            this.Command.CommandText_Where = this.DataManager.DataProvider.TableCommandGenerator.Where(translator.WhereClause);
            return new TableQueryOrderBy<T>(this.DataManager, this.Command);
        }

        public ITableQueryOrderBy<T> Where()
        {
            return new TableQueryOrderBy<T>(this.DataManager, this.Command);
        }


        public T Execute()
        {
            var dataSet = this.DataManager.DataProvider.ExecuteDataset(this.Command.ConnectionString, System.Data.CommandType.Text, this.DataManager.DataProvider.TableCommandGenerator.CommandText(this.Command));

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
            {
                var result = DataMapper.Map<T>(new DataRowSource(dataSet.Tables[0].Rows[0]));
                if (result.IsSucceed)
                    return result.Data;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public List<T> ExecuteList()
        {
            var dateSet = this.DataManager.DataProvider.ExecuteDataset(this.Command.ConnectionString, System.Data.CommandType.Text, this.DataManager.DataProvider.TableCommandGenerator.CommandText(this.Command));
            return DataMapper.MapList<T>(dateSet);
        }

        public List<T> ExecuteList(int pageSize, int pageNumber, out int totalCount)
        {
            totalCount = 0;
            this.Command.CommandText_Page = this.DataManager.DataProvider.TableCommandGenerator.Page(pageSize, pageNumber);
            var dataSet = this.DataManager.DataProvider.ExecuteDataset(this.Command.ConnectionString, System.Data.CommandType.Text, this.DataManager.DataProvider.TableCommandGenerator.CommandText(this.Command));
            if (dataSet != null && dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows != null && dataSet.Tables[1].Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            }
            return DataMapper.MapList<T>(dataSet);
        }
    }
}