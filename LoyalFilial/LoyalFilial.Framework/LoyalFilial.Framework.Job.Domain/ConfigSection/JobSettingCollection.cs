using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LoyalFilial.Framework.Job.Domain.ConfigSection
{
    /// <summary>
    /// 任务设置集合
    /// </summary>
    public class JobSettingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new JobSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobSetting)element).Name;
        }

        public JobSetting this[int i]
        {
            get
            {
                return (JobSetting)base.BaseGet(i);
            }
        }
    }
}
