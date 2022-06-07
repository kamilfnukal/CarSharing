using Autofac;
using Module = Autofac.Module;
using CarSharingInfrastructure.Repository;
using CarSharingInfrastructure.UnitOfWork;
using CarSharingDAL;
using System.Linq;
using CarSharingInfrastructure.Queries.Query;

namespace CarSharingInfrastructure.Config
{
    public class AutofacInfrastructureConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();
            
            builder.RegisterType<UnitOfWork.UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseQuery<>).Assembly)
                .Where(t => t.Name.EndsWith("Query"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<CarSharingContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterType<UnitOfWork.UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseQuery<>).Assembly)
                .Where(t => t.Name.EndsWith("Query"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<CarSharingContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
