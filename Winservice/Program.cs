using Autofac;
using Winservice.Configuration;
using Winservice.Extensions;

using Serilog;
using System;
using System.Diagnostics;
using Topshelf;
using Topshelf.Autofac;

namespace Winservice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // TODO: use args to force attach a process to continue...
                //while (!Debugger.IsAttached)
                //{
                //    Thread.Sleep(1000);
                //}

                IContainer container = Startup.BuildContainer();
                AppSettings settings = container.Resolve<AppSettings>();

                HostFactory.Run(hostConfigurator =>
                {
                    hostConfigurator.UseAutofacContainer(container);
                    string serviceName = settings.ServiceNameFull;

                    hostConfigurator.SetCommon(settings);
                    hostConfigurator.InstallWinServices();
                    hostConfigurator.SetEventSource(serviceName);
                    hostConfigurator.SetLogging(serviceName);
                    hostConfigurator.SetOnException();
                });
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "ReportGeneratorService";
                    eventLog.WriteEntry($"Ocurrió una excepción al iniciar: {ex.ToString()}");
                    Log.Logger.Error($"Ocurrió una excepción al iniciar: {ex.ToString()}");
                }
            }
        }
    }
}
