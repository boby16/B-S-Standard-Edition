/**********************************************************************
*  Author: yzb
*  Date:    2015-09-05
*  Purpose: 
* *********************************************************************
*  Date        Changer         Description
*  2015-09-05  yzb            Add
* ********************************************************************/
using System;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 f_purchase, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "f_purchase")]
    public partial class F_PurchaseDO 
    {

        #region 实体属性

        /// <summary>
        /// 报价单Id
        /// </summary>
        [Column("PurchaseId", true ,true)]
        public int PurchaseId { get; set; }

        /// <summary>
        /// 报价日期
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// 汽修厂Id
        /// </summary>
        public int MaintainFactoryId { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public int CarPartsId { get; set; }

        /// <summary>
        /// 询价单Id
        /// </summary>
        public int QuotationId { get; set; }

        /// <summary>
        /// InquiryId
        /// </summary>
        public int InquiryId { get; set; }

        /// <summary>
        /// 配件清单
        /// </summary>
        public string PartsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 发货单Id
        /// </summary>
        public int DeliverId { get; set; }

        /// <summary>
        /// CreateUser
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// UpdateUser
        /// </summary>
        public string UpdateUser { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        #endregion

    }
}