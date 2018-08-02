using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoyalFilial.Framework.Job.Domain.Services;

namespace LoyalFilial.Framework.Job.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduleService = new ScheduleService();
            scheduleService.StartScheduleCenter();
            Console.ReadKey();
        }
    }
}
