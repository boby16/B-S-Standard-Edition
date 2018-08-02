using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Data;

namespace LoyalFilial.Framework.Data
{
    public class TableActionResult : ActResult, ITableActResult
    {
        public long IdentityRowNo { get; set; }

        public TableActionResult() : base() { }

        public TableActionResult(string errorMsg)
            : base(errorMsg)
        { }

        public TableActionResult(long effectedRowNo)
            : base()
        {
            this.IdentityRowNo = effectedRowNo;
        }
    }
}
