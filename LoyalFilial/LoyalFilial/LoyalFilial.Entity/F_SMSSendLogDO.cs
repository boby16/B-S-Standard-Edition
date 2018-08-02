/**********************************************************************
*  Author: yzb
*  Date:    2015-08-03
*  Purpose: 
* *********************************************************************
*  Date        Changer         Description
*  2015-08-03  yzb            Add
* ********************************************************************/
using System;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 f_smssendlog, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "f_smssendlog")]
    public partial class F_SMSSendLogDO 
    {

        #region 实体属性

        /// <summary>
        /// 发送Id
        /// </summary>
        [Column("SendId", true ,true)]
        public long SendId { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// MobileNo
        /// </summary>
        public long MobileNo { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public string ReceivedId { get; set; }

        /// <summary>
        /// ReceivedResult
        /// </summary>
        public string ReceivedResult { get; set; }

        /// <summary>
        /// 发送状态（0,：待发送；1：发送成功；-1：发送失败）
        /// </summary>
        public int State { get; set; }

        #endregion

    }
}