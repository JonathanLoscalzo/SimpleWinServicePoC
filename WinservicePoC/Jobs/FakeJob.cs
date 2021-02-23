using Autofac;
using Core.Business;
using Quartz;
using Serilog;
using System.Threading.Tasks;

namespace WinservicePoC.Jobs
{
    public class FakeJob : IJob
    {
        private ILogger Log = Serilog.Log.ForContext<FakeJob>();
        private IJustInCase service;

        public FakeJob()
        {
            var container = Startup.BuildContainer();
            this.service = container.Resolve<IJustInCase>();
        }

        // TODO: Analyze why autofac is not injecting. Could it be interceptor?
        //public FakeJob(IJustInCase service)
        //{
        //    this.service = service;
        //}

        public virtual Task Execute(IJobExecutionContext context)
        {
            Log.Information("Executing()");

            this.service.Execute();

            Task.Delay(5000).Wait();
            Log.Information("Ending()");

            return Task.Run(() =>
            {
                // ????
            });
        }
    }
}
