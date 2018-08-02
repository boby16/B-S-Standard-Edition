using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LoyalFilial.Framework.Job.Domain.ConfigSection
{
    /// <summary>
    /// 调度中心设置
    /// </summary>
    public sealed class ScheduleSettings : ConfigurationSection
    {
        /// <summary>
        /// 任务设置集合
        /// </summary>
        [ConfigurationProperty("JobSettings")]
        public JobSettingCollection JobSettings
        {
            get
            {
                return (JobSettingCollection)base["JobSettings"];
            }
        }

        /// <summary>
        /// 通用设置
        /// </summary>

        [ConfigurationProperty("CommonSetting", IsRequired = true)]
        public CommonSetting CommonSetting
        {
            get { return (CommonSetting)base["CommonSetting"]; }
            set { base["CommonSetting"] = value; }
        }
    }
}
