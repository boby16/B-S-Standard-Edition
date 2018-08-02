using LoyalFilial.Common;
using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Entity.VO
{
    /// <summary>
    /// 报价单
    /// </summary>
    public class P_QuotationVO : P_QuotationDO
    {
        #region 公共部分
        /// <summary>
        /// 计划交货日期
        /// </summary>
        public DateTime PlanDeliveryDate { get; set; }

        /// <summary>
        /// 计划交货日期（显示）
        /// </summary>
        public string PlanDeliveryDateText
        {
            get
            {
                if (PlanDeliveryDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.PlanDeliveryDate, DateType.Day);
                else return "";
            }
        }

        /// <summary>
        /// 品牌
        /// </summary>
        public string CarBrand { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// 维修清单
        /// </summary>
        public string MaintainListImage { get; set; }

        /// <summary>
        /// 配件图片
        /// </summary>
        public string PartsImage { get; set; }

        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartsName { get; set; }

        /// <summary>
        /// 询价备注
        /// </summary>
        public string InquiryRemarks { get; set; }

        #endregion

        /// <summary>
        /// 汽配商名称
        /// </summary>
        public string CarPartsName { get; set; }

        /// <summary>
        /// 询价日期
        /// </summary>
        public DateTime InquiryDate { get; set; }

        /// <summary>
        /// 询价日期（显示）
        /// </summary>
        public string InquiryDateText
        {
            get
            {
                if (InquiryDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.InquiryDate, DateType.Day);
                else return "";
            }
        }

        /// <summary>
        /// 询价单状态
        /// </summary>
        public int InquiryState { get; set; }

        /// <summary>
        /// 询价单状态（显示）
        /// </summary>
        public string InquiryStateText
        {
            get
            {
                switch (this.InquiryState)
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
        /// 已报价数量
        /// </summary>
        public decimal QuotationCount { get; set; }
    }
}
