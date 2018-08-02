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
    [Table("carservice", "Favorites")]
    public partial class FavoritesDO 
    {
        #region 实体属性

        /// <summary>
        /// 收藏Id
        /// </summary>
        [Column("FavoriteId", true, true)]
        public string FavoriteId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 收藏目标类型（1：汽配商、2：汽修厂、3：车主 ）
        /// </summary>
        public int TargetType { get; set; }

        /// <summary>
        /// 收藏目标Id(汽配商Id、汽修厂Id、车主Id)
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime FavoriteTime { get; set; }

        #endregion

    }
}