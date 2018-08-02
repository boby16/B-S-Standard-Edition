using LoyalFilial.Common;
using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Entity.VO
{
    public class QuotationQueryVO : P_QuotationDO
    {
        public DateTime QuoteStartDate { get; set; }
        public DateTime QuoteEndDate { get; set; }
    }
}
