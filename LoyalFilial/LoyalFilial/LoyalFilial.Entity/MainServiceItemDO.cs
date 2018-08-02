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
    /// 实体类 b_mainserviceitem, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("carservice", "b_mainserviceitem")]
    public partial class MainserviceitemDO 
    {

        #region 实体属性

        /// <summary>
        /// 主营id
        /// </summary>
        [Column("MainServiceItemId", true ,true)]
        public int MainServiceItemId { get; set; }

        /// <summary>
        /// 主营
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        #endregion

    }
}