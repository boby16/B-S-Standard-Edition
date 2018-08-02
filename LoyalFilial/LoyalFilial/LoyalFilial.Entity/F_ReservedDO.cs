/**********************************************************************
*  Author: yzb
*  Date:    2015-08-02
*  Purpose: 
* *********************************************************************
*  Date        Changer         Description
*  2015-08-02  yzb            Add
* ********************************************************************/
using System;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 f_reserved, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "f_reserved")]
    public partial class F_ReservedDO 
    {

        #region 实体属性

        /// <summary>
        /// 预约Id
        /// </summary>
        [Column("MFReserveId", true ,true)]
        public int MFReserveId { get; set; }

        /// <summary>
        /// 维修厂的客户Id
        /// </summary>
        public int MFCustomerId { get; set; }

        /// <summary>
        /// 维修厂Id
        /// </summary>
        public int MaintainFactoryId { get; set; }

        /// <summary>
        /// 保养项目
        /// </summary>
        public string ServiceItem { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime ReservedDate { get; set; }

        /// <summary>
        /// 预约手机号
        /// </summary>
        public long MobileNo { get; set; }
        
        /// <summary>
        /// 预约方式（1：电话预约；2：短信预约；3：IM预约；4：网站预约）
        /// </summary>
        public int ReserveType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 反馈
        /// </summary>
        public string FeedBack { get; set; }

        /// <summary>
        /// 状态（-3：维修厂拒绝；-2：客户取消；-1：预约失败；0：预约中；1：预约成功；2：维保完成）
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// CreateUser
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

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