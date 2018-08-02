/**********************************************************************
*  Author: yzb
*  Date:    2015-07-26
*  Purpose: 
* *********************************************************************
*  Date        Changer         Description
*  2015-07-26  yzb            Add
* ********************************************************************/
using System;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 carservice_log, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("loyalfilial_carservice", "carservice_log")]
    public partial class LogDO 
    {

        #region 实体属性

        /// <summary>
        /// LogId
        /// </summary>
        [Column("LogId", true ,true)]
        public long LogId { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// LogTime
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Exception
        /// </summary>
        public string Exception { get; set; }

        #endregion

    }
}