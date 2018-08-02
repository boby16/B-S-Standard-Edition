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
    /// 实体类 p_deliverreturn, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "p_deliverreturn")]
    public partial class P_DeliverReturnDO 
    {

        #region 实体属性

        /// <summary>
        /// 报价单Id
        /// </summary>
        [Column("DeliverReturnId", true ,true)]
        public int DeliverReturnId { get; set; }

        /// <summary>
        /// 报价日期
        /// </summary>
        public DateTime DeliverReturnDate { get; set; }

        /// <summary>
        /// 汽配商Id
        /// </summary>
        public int CarPartsId { get; set; }

        /// <summary>
        /// 询价单Id
        /// </summary>
        public int DeliverId { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public int PurchaseId { get; set; }

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