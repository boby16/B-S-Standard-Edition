using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data.DataMap.Map;

namespace LoyalFilial.Framework.Data.Table
{
    public class TableQueryFrom<T> : TableBaseQuery, ITableQueryFrom<T> where T : class
    {
        public ITableQueryWhere<T> From(string tableAlias)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias))
            {
                var entityType = typeof(T);
                var em = EntityMapCache.GetEntityMap(entityType);
                tableAlias = em.Table == null ? null : em.Table.TableName;
            }
            this.Command.CommandText_From = this.DataManager.DataProvider.TableCommandGenerator.From(tableAlias);
            return new TableQueryWhere<T>(this.DataManager, this.Command);
        }

        public ITableQueryWhere<T> From()
        {
            return From(null);
        }

        public TableQueryFrom(IDataManager dataManager, ITableCommand command)
        {
            this.DataManager = dataManager;
            this.Command = command;
        }
    }
}
