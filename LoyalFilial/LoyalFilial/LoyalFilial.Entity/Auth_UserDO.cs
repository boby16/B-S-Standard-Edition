/**********************************************************************
*  Author: yzb
*  Date:    2015-07-25
*  Purpose: 
* *********************************************************************
*  Date        Changer         Description
*  2015-07-25  yzb            Add
* ********************************************************************/
using System;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 auth_user, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("carservice", "auth_user")]
    public partial class UserDO 
    {

        #region 实体属性

        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("UserId", true , false)]
        public string UserId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public long MobileNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 重试次数(5次机会）
        /// </summary>
        public int ReTryTimes { get; set; }

        /// <summary>
        /// 用户类型（1：维修厂；2：汽配商；3：车主）
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 汽修厂ID、汽配商ID
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        #endregion

    }
}