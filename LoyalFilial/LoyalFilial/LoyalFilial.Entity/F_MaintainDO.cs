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
    /// 实体类 f_maintain, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "f_maintain")]
    public partial class F_MaintainDO 
    {

        #region 实体属性

        /// <summary>
        /// 维保记录Id
        /// </summary>
        [Column("MFMaintainId", true ,true)]
        public int MFMaintainId { get; set; }

        /// <summary>
        /// 汽修厂客户Id
        /// </summary>
        public int MFCustomerId { get; set; }

        /// <summary>
        /// 汽修厂Id
        /// </summary>
        public int MaintainFactoryId { get; set; }

        /// <summary>
        /// 维保项目
        /// </summary>
        public string ServiceItem { get; set; }

        /// <summary>
        /// 提醒日期
        /// </summary>
        public DateTime RemindDate { get; set; }

        /// <summary>
        /// 维保日期
        /// </summary>
        public DateTime MaintainDate { get; set; }

        /// <summary>
        /// 是否有效
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