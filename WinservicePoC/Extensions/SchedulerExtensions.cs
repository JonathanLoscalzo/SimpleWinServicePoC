using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinservicePoC.Extensions
{
    public static class SchedulerExtensions
    {
        public static void ScheduleJobAndTrigger<T>(this IScheduler scheduler, Configuration.AppSettings appSettings) where T : IJob
        {
            var (job, trigger) = scheduler.CreateJobAndTrigger<T>(appSettings.CronScheduleJobExpression, appSettings.ScheduleType);
            scheduler.ScheduleJob(job, trigger);
        }

        private static (IJobDetail job, ITrigger trigger) CreateJobAndTrigger<T>(this IScheduler scheduler, string CronScheduleJobExpression, string ScheduleType) where T : IJob
        {
            var job = TopshelfHelper.CreateJob<T>();
            var trigger = TopshelfHelper.CreateJobTrigger<T>(job, CronScheduleJobExpression, ScheduleType);
            return (job, trigger);
        }
    }
}
