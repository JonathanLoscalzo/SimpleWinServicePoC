using WinservicePoC.Configuration;
using WinservicePoC.Services;
using Serilog;
using System.Diagnostics;
using Topshelf;
using Topshelf.Autofac;

namespace WinservicePoC.Extensions
{
    static class HostConfiguratorExtensions
    {
        public static void InstallWinServices(this Topshelf.HostConfigurators.HostConfigurator hostConfigurator)
        {
            hostConfigurator.Service<MainService>(serviceConfigurator =>
            {
                //serviceConfigurator.ConstructUsing(() => new MainService());
                serviceConfigurator.ConstructUsingAutofacContainer();
                serviceConfigurator.WhenStarted(service => service.OnStart());
                serviceConfigurator.WhenStopped(service => service.OnStop());
            });
        }

        public static void SetLogging(this Topshelf.HostConfigurators.HostConfigurator hostConfigurator, string serviceName)
        {
            Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.Information()
                       .WriteTo.Console()
                       .WriteTo.EventLog(serviceName, "Application", manageEventSource: true)
                       .CreateLogger();

            hostConfigurator.UseSerilog(Log.Logger);
        }

        public static void SetCommon(this Topshelf.HostConfigurators.HostConfigurator hostConfigurator, AppSettings settings)
        {
            hostConfigurator.SetServiceName(settings.ServiceName);
            hostConfigurator.SetDisplayName(settings.ServiceNameFull);
            hostConfigurator.SetDescription(settings.Description);

            hostConfigurator.RunAsLocalSystem()
                .DependsOnEventLog()
                .StartAutomatically()
                .EnableServiceRecovery(rc => rc.RestartService(settings.DelayInMinutes));
        }

        /**
         * Este método es excepcional, no debería ocurrir que llegue por aquí, ya que va a dificultar la tarea 
         * que intente ejecutarse en ese momento: install, stop or delete. 
         * */
        public static void SetOnException(this Topshelf.HostConfigurators.HostConfigurator hostConfigurator)
        {
            hostConfigurator.OnException(ex =>
            {
                try
                {
                    throw ex;
                }
                catch (System.Exception exp)
                {
                    Log.Error("Ocurrió una excepción en el servicio: " + exp.ToString());
                }
            });
        }

        public static void SetEventSource(this Topshelf.HostConfigurators.HostConfigurator hostConfigurator, string serviceName)
        {
            if (EventLog.SourceExists(serviceName))
            {
                EventLog.DeleteEventSource(serviceName);
            }
        }
    }
}
