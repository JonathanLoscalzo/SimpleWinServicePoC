using Quartz;
using System;
using System.Configuration;
using System.Diagnostics;


namespace WinservicePoC.Extensions
{
    public static class TopshelfHelper
    {
        public static ITrigger CreateJobTrigger<T>(IJobDetail job, string cronScheduleJobExpression, string typeScheduler) where T : IJob
        {
            if (typeScheduler == "now")
            {
                return TriggerBuilder.Create()
                    .WithIdentity($"{typeof(T).Name}.trigger")
                    .ForJob(job)
                    .StartNow()
                    .Build();
            }

            if (typeScheduler == "cron")
            {
                return TriggerBuilder.Create()
                    .WithIdentity($"{typeof(T).Name}.trigger")
                    .ForJob(job)
                    .StartNow() // TODO: remove future
                    .WithCronSchedule(cronScheduleJobExpression)
                    .Build();
            }

            throw new Exception("The scheduler's type in not defined");
        }

        public static IJobDetail CreateJob<T>() where T : IJob
        {
            IJobDetail job = JobBuilder
                    .Create<T>()
                    .WithIdentity(typeof(T).Name, SchedulerConstants.DefaultGroup)
                    .Build();

            return job;
        }
    }
}
