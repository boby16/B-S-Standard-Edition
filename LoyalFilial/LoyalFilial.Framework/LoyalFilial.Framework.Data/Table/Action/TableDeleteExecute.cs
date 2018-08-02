using System;
using System.Data;
using LoyalFilial.Framework.Core.Data;

namespace LoyalFilial.Framework.Data.Table.Action
{
    public class TableDeleteExecute<T> : ITableDeleteExecute<T> where T : class
    {
        public ITableDeleteCommand Command { get; private set; }
        public IDataManager DataManager { get; set; }

        public ITableActResult Execute()
        {
            var commandText = this.DataManager.DataProvider.TableCommandGenerator.CommandText(this.Command);

            try
            {
                this.DataManager.DataProvider.ExecuteNonQuery(Command.ConnectionString, CommandType.Text, commandText);
                return new TableActionResult();
            }
            catch (Exception ex)
            {
                return new TableActionResult(ex.Message);
            }
        }

        public TableDeleteExecute(IDataManager dataManager, ITableDeleteCommand command)
        {
            this.DataManager = dataManager;
            this.Command = command;
        }
    }
}
