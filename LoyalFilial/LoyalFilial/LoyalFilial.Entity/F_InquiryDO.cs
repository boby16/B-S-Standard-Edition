/**********************************************************************
*  Author: yzb
*  Date:    2015-08-09
*  Purpose: 
* *********************************************************************
*  Date        Changer         Description
*  2015-08-09  yzb            Add
* ********************************************************************/
using System;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 询价单, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "f_inquiry")]
    public partial class F_InquiryDO 
    {

        #region 实体属性

        /// <summary>
        /// 询价单Id
        /// </summary>
        [Column("InquiryId", true ,true)]
        public int InquiryId { get; set; }

        /// <summary>
        /// 询价日期
        /// </summary>
        public DateTime InquiryDate { get; set; }

        /// <summary>
        /// 汽修厂Id
        /// </summary>
        public int MaintainFactoryId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string CarBrand { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 车架码
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// 维修清单
        /// </summary>
        public string MaintainListImage { get; set; }

        /// <summary>
        /// 配件清单
        /// </summary>
        public string PartsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 联盟
        /// </summary>
        public string Alliance { get; set; }

        /// <summary>
        /// 汽配商Id
        /// </summary>
        public string CarPartsId { get; set; }

        /// <summary>
        /// 要求交货日期
        /// </summary>
        public DateTime PlanDeliveryDate { get; set; }

        /// <summary>
        /// 状态（-1：作废；0：制作中；1：有效；2：已成交）
        /// </summary>
        public int State { get; set; }

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

        /// <summary>
        /// 是否匿名发布（1：是；0：否）
        /// </summary>
        public int IsAnonymity { get; set; }

        /// <summary>
        /// 可见汽配商类型（1：指定供应商；2：收藏供应商）
        /// </summary>
        public int ToCarPartsType { get; set; }

        /// <summary>
        /// 采购单Id
        /// </summary>
        public int PurchaseId { get; set; }

        #endregion

    }
}