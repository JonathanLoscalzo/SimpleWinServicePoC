using Autofac;
using Business;
using Core.Business;

namespace Winservice.Modules
{
    class BusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            //var asm = typeof(JustInCaseUseCase).Assembly;
            //containerBuilder
            //    .RegisterAssemblyTypes(asm)
            //    .Where(p => p.FullName.Contains("UseCase"))
            //    .AsImplementedInterfaces();

            containerBuilder.RegisterType<JustInCaseUseCase>()
                .As<IJustInCase>()
                .InstancePerLifetimeScope();

            //TODO Add interceptor
        }
    }
}
