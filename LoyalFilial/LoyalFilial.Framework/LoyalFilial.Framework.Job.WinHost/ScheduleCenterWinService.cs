using System.ServiceProcess;
using System.Threading.Tasks;
using LoyalFilial.Framework.Job.Domain.Services;

namespace LoyalFilial.Framework.Job.WinHost
{
    /// <summary>
    /// 调度中心Windows服务
    /// </summary>
    public partial class ScheduleCenterWinService : ServiceBase
    {
        ScheduleService scheduleService;
        Task startTask;
        Task stopTask;

        public ScheduleCenterWinService()
        {
            InitializeComponent();
            scheduleService = new ScheduleService();
            startTask = new Task(scheduleService.StartScheduleCenter);
            stopTask = new Task(scheduleService.StopScheduleCenter);
        }

        protected override void OnStart(string[] args)
        {
            startTask.Start();
        }

        protected override void OnStop()
        {
            stopTask.Start();
        }
    }
}
