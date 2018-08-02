using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LoyalFilial.Framework.Job.Domain.ConfigSection
{
    /// <summary>
    /// 任务设置
    /// </summary>
    public sealed class JobSetting : ConfigurationElement
    {
        /// <summary>
        /// 间隔
        /// </summary>
        [ConfigurationProperty("Interval", IsRequired = true)]
        public int Interval
        {
            get { return (int)base["Interval"]; }
            set { base["Interval"] = value; }
        }

        /// <summary>
        /// 任务地址
        /// </summary>
        [ConfigurationProperty("JobUrl", IsRequired = true)]
        public string JobUrl
        {
            get { return (string)base["JobUrl"]; }
            set { base["JobUrl"] = value; }
        }

        /// <summary>
        /// 任务方法名
        /// </summary>
        [ConfigurationProperty("ServiceName", IsRequired = true)]
        public string ServiceName
        {
            get { return (string)base["ServiceName"]; }
            set { base["ServiceName"] = value; }
        }

        /// <summary>
        /// 任务方法名
        /// </summary>
        [ConfigurationProperty("MethodName", IsRequired = true)]
        public string MethodName
        {
            get { return (string)base["MethodName"]; }
            set { base["MethodName"] = value; }
        }

        /// <summary>
        /// 值集
        /// </summary>
        [ConfigurationProperty("Valus", IsRequired = true)]
        public string Valus
        {
            get { return (string)base["Valus"]; }
            set { base["Valus"] = value; }
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)base["Name"]; }
            set { base["Name"] = value; }
        }
    }
}
