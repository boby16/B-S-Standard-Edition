
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
    /// 实体类 auth_authcode, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "auth_authcode")]
    public partial class AuthCodeDO 
    {

        #region 实体属性

        /// <summary>
        /// PhoneNo
        /// </summary>
        [Column("MobileNo", true, false)]
        public long MobileNo { get; set; }

        /// <summary>
        /// AuthCode
        /// </summary>
        public int AuthCode { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public int State { get; set; }

        #endregion

    }
}