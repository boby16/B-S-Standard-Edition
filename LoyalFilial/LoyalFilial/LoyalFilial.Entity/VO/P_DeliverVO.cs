using LoyalFilial.Common;
using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Entity.VO
{
    /// <summary>
    /// 发货
    /// </summary>
    public class P_DeliverVO : P_DeliverDO
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
    }
}
