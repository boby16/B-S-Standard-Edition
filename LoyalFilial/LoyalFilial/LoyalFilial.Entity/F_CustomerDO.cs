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
using LoyalFilial.Common;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 实体类 f_customer, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("carservice", "f_customer")]
    public partial class F_CustomerDO 
    {

        #region 实体属性

        /// <summary>
        /// 客户ID
        /// </summary>
        [Column("MFCustomerId", true, true)]
        public int MFCustomerId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustormerName { get; set; }

        /// <summary>
        /// 维修厂Id
        /// </summary>
        public int? MaintainFactoryId { get; set; }
        /// <summary>
        /// 关联客户Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// BirthDay
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string CarBrand { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNO { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public long MobileNo { get; set; }

        /// <summary>
        /// 车架码
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// 状态：1：正式客户，0：潜在客户;
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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

        [Column(true)]
        public string BirthDayText
        {
            get
            {
                if (this.BirthDay.HasValue && this.BirthDay.Value > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.BirthDay.Value, DateType.Day);
                else return "";
            }
        }

        [Column(true)]
        public string StateText
        {
            get
            {
                switch (this.State)
                {
                    case 1:
                        return "正式客户";
                    default:
                        return "临时客户";
                }
            }
        }

        [Column(true)]
        public string MobileNoText
        {
            get
            {
                var mobileTemp = this.MobileNo.ToString();
                if (mobileTemp.Length > 7)
                {
                    return String.Format("{0}****{1}", mobileTemp.Substring(0, 3), mobileTemp.Substring(mobileTemp.Length - 4));
                }
                return "****";
            }
        }

        #endregion

    }
}