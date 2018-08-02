using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LoyalFilial.Framework.Job.Domain.ConfigSection;
using LoyalFilial.Framework.Job.Domain.Models;

namespace LoyalFilial.Framework.Job.Domain.Services
{
    /// <summary>
    /// 调度服务
    /// </summary>
    public class ScheduleService
    {
        static ScheduleCenter scheduleCenter;

        static ScheduleService()
        {
            var scheduleSettings = ConfigurationManager.GetSection("ScheduleSettings") as ScheduleSettings;
            var jobAgents = new List<JobAgent>();

            if (scheduleSettings.JobSettings != null)
            {
                jobAgents = scheduleSettings.JobSettings.OfType<JobSetting>().Select(j => new JobAgent(j.JobUrl, j.ServiceName, j.MethodName, j.Valus, j.Interval, scheduleSettings.CommonSetting.EffectHour, scheduleSettings.CommonSetting.ExpireHour, j.Name)).ToList();
            }

            scheduleCenter = new ScheduleCenter(jobAgents);
        }

        /// <summary>
        /// 启动调度中心
        /// </summary>
        public void StartScheduleCenter()
        {
            scheduleCenter.StartAllJobs();
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void RunJob()
        {

        }

        /// <summary>
        /// 停止调度中心
        /// </summary>
        public void StopScheduleCenter()
        {
            scheduleCenter.StopAllJobs();
        }
    }
}
