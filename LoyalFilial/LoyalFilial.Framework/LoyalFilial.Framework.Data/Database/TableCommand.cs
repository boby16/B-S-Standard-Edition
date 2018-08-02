using LoyalFilial.Framework.Core.Data;

namespace LoyalFilial.Framework.Data.Database
{
    public class TableQueryCommand : ITableCommand
    {
        public string CommandText_Select { get; set; }
        public string CommandText_From { get; set; }
        public string CommandText_Where { get; set; }
        public string CommandText_OrderBy_Asc { get; set; }
        public string CommandText_OrderBy_Desc { get; set; }
        public string CommandText_Page { get; set; }

        public string ConnectionString
        {
            get;
            set;
        }
    }

    public class TableUpdateCommand : ITableUpdateCommand
    {
        public string CommandText_Update { get; set; }
        public string CommandText_Set { get; set; }
        public string CommandText_Where { get; set; }
        public string ConnectionString
        {
            get;
            set;
        }
    }

    public class TableInsertCommand : ITableInsertCommand
    {
        public string CommandText_Insert { get; set; }
        public string CommandText_IntoValue { get; set; }
        public string ConnectionString
        {
            get;
            set;
        }
    }

    public class TableDeleteCommand : ITableDeleteCommand
    {
        public string CommandTextDelete { get; set; }
        public string CommandText_Where { get; set; }
        public string ConnectionString
        {
            get;
            set;
        }
    }
}
