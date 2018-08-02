using LoyalFilial.Common;
using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Entity.VO
{
    /// <summary>
    /// 询价单
    /// </summary>
    public class F_InquiryVO : F_InquiryDO
    {
        public string InquiryDateText
        {
            get
            {
                if (this.InquiryDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.InquiryDate, DateType.Day);
                else return "";
            }
        }

        /// <summary>
        /// 询价单状态
        /// </summary>
        public string StateText
        {
            get
            {
                switch (this.State)
                {
                    case -1:
                        return "作废";
                    case 0:
                        return "制作中";
                    case 1:
                        return "有效";
                    case 2:
                        return "已成交";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 要求交货日期
        /// </summary>
        public string PlanDeliveryDateText
        {
            get
            {
                if (this.PlanDeliveryDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.PlanDeliveryDate, DateType.Day);
                else return "";
            }
        }

        /// <summary>
        /// 报价数量
        /// </summary>
        public decimal QuotationCount { get; set; }
    }
}
