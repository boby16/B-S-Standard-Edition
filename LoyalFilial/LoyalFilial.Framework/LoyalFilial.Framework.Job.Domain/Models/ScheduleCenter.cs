using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoyalFilial.Framework.Job.Domain.Models
{
    /// <summary>
    /// 调度中心领域对象
    /// </summary>
    internal class ScheduleCenter : IAggregateRoot
    {
        #region Ctors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jobAgents"></param>
        public ScheduleCenter(IEnumerable<JobAgent> jobAgents)
        {
            Id = Guid.NewGuid();
            JobAgents = jobAgents.ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 任务线程集合
        /// </summary>
        public List<JobAgent> JobAgents { get; private set; }

        #endregion

        #region Fields

        /// <summary>
        /// 是否所有的任务都已经停止
        /// </summary>
        protected bool IsAllJobsStoped
        {
            get
            {
                return JobAgents.Any(j => !j.IsRunning);
            }
        }

        #endregion

        #region Start

        /// <summary>
        /// 启动所有任务
        /// </summary>
        public void StartAllJobs()
        {
            JobAgents.ForEach(j =>
            {
                Task.Factory.StartNew(() =>
                {
                    var random = new Random(DateTime.Now.Millisecond);
                    Thread.Sleep(random.Next(1000, 6000));
                    j.Start(true);
                });
                Thread.Sleep(100);
            });
        }

        #endregion

        #region Stop

        /// <summary>
        /// 停止所有任务
        /// </summary>
        public void StopAllJobs()
        {
            JobAgents.ForEach(j => { j.Stop(); });
            while (!IsAllJobsStoped)
            {
                Thread.Sleep(1000);
            }
        }

        #endregion
    }
}
