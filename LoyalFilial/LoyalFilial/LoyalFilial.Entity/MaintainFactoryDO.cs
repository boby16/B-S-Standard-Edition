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
    /// 实体类 b_maintainfactory, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("carservice", "b_maintainfactory")]
    public partial class MaintainFactoryDO 
    {

        #region 实体属性

        /// <summary>
        /// 汽修厂Id
        /// </summary>
        [Column("MaintainFactoryId", true ,true)]
        public int MaintainFactoryId { get; set; }

        /// <summary>
        /// 汽修厂名称
        /// </summary>
        public string MaintainName { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string TelNo { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 主营
        /// </summary>
        public string MainServices { get; set; }

        /// <summary>
        /// 联盟
        /// </summary>
        public string Alliance { get; set; }

        /// <summary>
        /// 状态（1:有效;0:无效）
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

        /// <summary>
        /// 登录用户
        /// </summary>
        [Column(true)]
        public string UserId { get; set; }
    }
}