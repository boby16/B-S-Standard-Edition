using LoyalFilial.Common;
using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Entity.VO
{
    public class F_MaintainVO : F_MaintainDO
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustormerName { get; set; }

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
        public int CState { get; set; }

        public string BirthDayText
        {
            get
            {
                if (this.BirthDay.HasValue && this.BirthDay.Value > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.BirthDay.Value, DateType.Day);
                else return "";
            }
        }

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

        [Column(true)]
        public string RemindDateText
        {
            get
            {
                if (this.RemindDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.RemindDate, DateType.Day);
                else return "";
            }
        }

        [Column(true)]
        public string MaintainDateText
        {
            get
            {
                if (this.MaintainDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.MaintainDate, DateType.Day);
                else return "";
            }
        }
    }
}
