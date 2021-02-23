using Quartz;
using WinservicePoC.Configuration;
using WinservicePoC.Extensions;
using Serilog;
using System;
using WinservicePoC.Jobs;

namespace WinservicePoC.Services
{
    public class MainService
    {
        private ILogger Log = Serilog.Log.Logger;

        private IScheduler Scheduler { get; }

        private AppSettings AppSettings;

        public MainService(IScheduler scheduler, AppSettings appSettings)
        {
            this.Scheduler = scheduler;
            this.AppSettings = appSettings;
        }

        public void OnStart()
        {
            Scheduler.Start();

            // TODO: change this code to enable jobs with different configurations.
            // like: https://andrewlock.net/creating-a-quartz-net-hosted-service-with-asp-net-core/#configuring-the-job
            Scheduler.ScheduleJobAndTrigger<FakeJob>(this.AppSettings);
            //Scheduler.ScheduleJobAndTrigger<SyncStudentsDisengagementDateJob>(this.AppSettings);
            //Scheduler.ScheduleJobAndTrigger<SyncStudentsOfficeWithExternalOfficeJob>(this.AppSettings);
            //Scheduler.ScheduleJobAndTrigger<GenerateHexactaStatisticsJob>(this.AppSettings);
        }

        public void OnPaused()
        {
            Log.Information("On Pause...");
            Scheduler.PauseAll();
        }

        public void OnContinue()
        {
            Log.Information("On Continue...");
            Scheduler.ResumeAll();
        }

        public void OnStop()
        {
            Log.Information("On Stop...");
            Scheduler.Shutdown();
        }

        public bool OnShutdown()
        {
            Log.Information("On Shutdown...");
            return true;
        }
    }
}
