using Autofac;
using WinservicePoC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinservicePoC.Modules
{
    class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainService>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
