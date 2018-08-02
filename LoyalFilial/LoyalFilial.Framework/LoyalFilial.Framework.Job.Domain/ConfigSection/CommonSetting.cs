using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LoyalFilial.Framework.Job.Domain.ConfigSection
{
    /// <summary>
    /// 通用设置
    /// </summary>
    public sealed class CommonSetting : ConfigurationElement
    {
        /// <summary>
        /// 生效时间
        /// </summary>
        [ConfigurationProperty("EffectHour", IsRequired = true)]
        public int EffectHour
        {
            get { return (int)base["EffectHour"]; }
            set { base["EffectHour"] = value; }
        }

        /// <summary>
        /// 失效时间
        /// </summary>
        [ConfigurationProperty("ExpireHour", IsRequired = true)]
        public int ExpireHour
        {
            get { return (int)base["ExpireHour"]; }
            set { base["ExpireHour"] = value; }
        }
    }
}
