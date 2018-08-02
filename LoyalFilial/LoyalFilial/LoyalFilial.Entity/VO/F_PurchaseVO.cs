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
    public class F_PurchaseVO : F_PurchaseDO
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

        #endregion

        /// <summary>
        /// 汽配商名称
        /// </summary>
        public string CarPartsName { get; set; }

        /// <summary>
        /// 汽修厂名
        /// </summary>
        public string MaintainName { get; set; }

        /// <summary>
        /// 实际发货单号
        /// </summary>
        public string ActualDeliverID { get; set; }

        /// <summary>
        /// 采购日期（显示）
        /// </summary>
        public string PurchaseDateText
        {
            get
            {
                if (PurchaseDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.PurchaseDate, DateType.Day);
                else return "";
            }
        }

        public string DeliverState
        {
            get
            {
                if (this.DeliverId > 0)
                {
                    if (this.DeliverReturnId > 0)
                        return "已退货";
                    else
                        return "已发货";
                }
                return "未发货";
            }
        }

        public DateTime DeliverDate { get; set; }


        public string DeliverDateText
        {
            get
            {
                if (DeliverDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.DeliverDate, DateType.Day);
                else return "";
            }
        }

        public int DeliverReturnId { get; set; }
    }
}
