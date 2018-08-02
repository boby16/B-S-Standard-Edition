using LoyalFilial.Common;
using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Entity.VO
{
    public class F_ReservedVO : F_ReservedDO
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
        public long CMobileNo { get; set; }

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
        public string StateText
        {
            get
            {
                switch (this.State)
                {
                    case -3:
                        return "拒绝";
                    case -2:
                        return "客户取消";
                    case -1:
                        return "预约失败";
                    case 0:
                        return "预约申请中";
                    case 1:
                        return "预约成功";
                    case 2:
                        return "维保完成";
                    default:
                        return "";
                }
            }
        }

        public string ReserveTypeText
        {
            get
            {
                switch (this.ReserveType)
                {
                    case 1:
                        return "电话预约";
                    case 2:
                        return "短信预约";
                    case 3:
                        return "网站预约";
                    case 4:
                        return "QQ预约";
                    case 5:
                        return "微信预约";
                    default:
                        return "";
                }
            }
        }

        public string ReservedDateText
        {
            get
            {
                if (this.ReservedDate > DateTime.MinValue)
                    return DateHelper.ConvertToString(this.ReservedDate, DateType.Day);
                else return "";
            }
        }
    }
}
