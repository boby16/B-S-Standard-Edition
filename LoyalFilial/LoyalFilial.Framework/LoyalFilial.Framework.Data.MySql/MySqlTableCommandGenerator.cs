using LoyalFilial.Framework.Data.Database;

namespace LoyalFilial.Framework.Data.MySql
{
    public class MySqlTableCommandGenerator : SqlTableCommandGenerator
    {
        public override string Page(int PageSize, int pageIndex)
        {
            return string.Format(" LIMIT {0},{1} ", (pageIndex - 1) * PageSize, PageSize);
        }
    }
}