using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Data.ScriptExpression;
using System.Linq.Expressions;

namespace LoyalFilial.Framework.Data.Table.Action
{
    public class TableDelete<T> : ITableDelete<T> where T : class
    {
        public ITableDeleteCommand Command { get; private set; }
        public IDataManager DataManager { get; set; }

        public ITableDeleteExecute<T> Where(Expression<System.Func<T, bool>> columnNameExp)
        {
            var translator = new TableQueryTranslator<T>();
            translator.Translate(columnNameExp);

            this.Command.CommandText_Where =
                this.DataManager.DataProvider.TableCommandGenerator.Where(translator.WhereClause);
            return new TableDeleteExecute<T>(this.DataManager, this.Command);
        }

        public TableDelete(IDataManager dataManager, ITableDeleteCommand command)
        {
            this.DataManager = dataManager;
            this.Command = command;
        }
    }
}
