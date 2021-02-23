using Autofac;
using WinservicePoC.Configuration;
using WinservicePoC.Modules;
using Serilog;
using Winservice.Modules;
using AutofacSerilogIntegration;

namespace WinservicePoC
{
    public class Startup
    {
        private static Serilog.ILogger Log = Serilog.Log.ForContext<Startup>();

        public static IContainer BuildContainer()
        {
            Log.Debug("Container: Start Configuring and Registering....");
            ContainerBuilder containerBuilder = new ContainerBuilder();
            Startup.ConfigureContainer(containerBuilder);
            Startup.RegisterConfiguration(containerBuilder);
            Startup.RegisterBoostrapper(containerBuilder);

            Log.Debug("Contgainer: Building....");
            IContainer container = containerBuilder.Build();

            return container;
        }

        public static void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<SchedulerModule>();
            containerBuilder.RegisterModule<WinservicePoC.Modules.ServicesModule>();
            containerBuilder.RegisterModule<BusinessModule>();

            //AutofacSerilogIntegration
            // TODO: how set several Loggers? 
            containerBuilder.RegisterLogger();
        }

        internal static void RegisterConfiguration(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AppSettings>().AsSelf();
        }

        private static void RegisterBoostrapper(ContainerBuilder containerBuilder)
        {
            //var configuration = new ConfigurationBuilder()
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //       .Build();

            //var startUp = new Bootstrapper.Startup(configuration);

            //IServiceCollection serviceCollection = new ServiceCollection();
            //startUp.ConfigureServices(serviceCollection);

            //startUp.ConfigureContainer(containerBuilder);
            //containerBuilder.RegisterType<Common.Configuration.AppConfiguration>()
            //    .WithParameter(new TypedParameter(typeof(IConfiguration), configuration));
            //containerBuilder.Populate(serviceCollection);
        }
    }
}
