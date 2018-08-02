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
    /// 实体类 b_city, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("carservice", "b_city")]
    public partial class CityDO 
    {

        #region 实体属性

        /// <summary>
        /// 城市Id
        /// </summary>
        [Column("CityId", true ,true)]
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        #endregion

    }
}