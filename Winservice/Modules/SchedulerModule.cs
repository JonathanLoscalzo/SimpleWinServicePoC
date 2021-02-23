using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Extras.Quartz;
using Winservice.Interceptors;
using Winservice.Jobs;

namespace Winservice.Modules
{
    class SchedulerModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            // 1) Register IScheduler
            //var schedulerConfig = new NameValueCollection {
            //        {"quartz.threadPool.threadCount", "3"},
            //        {"quartz.threadPool.threadNamePrefix", "SchedulerWorker"},
            //        {"quartz.scheduler.threadName", "Scheduler"}
            //    };

            //containerBuilder.RegisterModule(new QuartzAutofacFactoryModule
            //{
            //    ConfigurationProvider = componentContext => schedulerConfig
            //});

            //COMMENT: en caso de no necesitar la configuración anterior, se instala por defecto con el siguiente comando
            containerBuilder.RegisterModule(new QuartzAutofacFactoryModule());

            // 2) Register jobs
            containerBuilder.RegisterModule(new QuartzAutofacJobsModule(typeof(FakeJob).Assembly));

            // 3) Register Interceptor to jobs

            containerBuilder
                .RegisterType<FakeJob>()
                .AsSelf()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(JobInterceptor));

            containerBuilder
               .RegisterType<JobInterceptor>()
               .AsSelf();
        }
    }
}
