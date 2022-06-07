using Autofac;
using Module = Autofac.Module;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CarSharingInfrastructure.Config;
using CarSharingBL.Services.Service;

namespace CarSharingBL.Config
{
    public class AutofacBLConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacInfrastructureConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "CarSharingBL.QueryObjects.QueryObject")
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(BaseService<,>).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "CarSharingBL.Facades.Facade")
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutofacInfrastructureConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "CarSharingBL.QueryObjects.QueryObject")
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(BaseService<,>).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "CarSharingBL.Facades.Facade")
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
