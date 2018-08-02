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
    /// 实体类 auth_loginlog, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("carservice", "auth_login")]
    public partial class AuthLoginDO 
    {
        #region 实体属性

        /// <summary>
        /// UserId
        /// </summary>
        [Column("UserId", true, false)]
        public string UserId { get; set; }

        /// <summary>
        /// LoginIP
        /// </summary>
        public string LoginIP { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        #endregion

    }
}