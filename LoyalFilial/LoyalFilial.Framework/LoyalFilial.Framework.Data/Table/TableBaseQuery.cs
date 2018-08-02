using LoyalFilial.Framework.Core.Data;

namespace LoyalFilial.Framework.Data.Table
{
    public class TableBaseQuery
    {
        public ITableCommand Command
        {
            get;
            set;
        }

        public IDataManager DataManager
        {
            get;
            set;
        }

        public TableBaseQuery()
        {
            //this.Command = new TableQueryCommand();
        }
    }
}
