using Autofac;
using ChildrenLeisure.BLL.Interfaces;
using ChildrenLeisure.BLL.Services;
using ChildrenLeisure.DAL.Data;
using ChildrenLeisure.DAL.Interfaces;
using ChildrenLeisure.DAL.UnitOfWork;
using AutoMapper;
using ChildrenLeisure.BLL.Mapping;
using System.Reflection;

namespace ChildrenLeisure.DI
{
    public class DependencyConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EntertainmentService>().As<IEntertainmentService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<PricingService>().As<IPricingService>();

            builder.Register(ctx =>
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
                return config.CreateMapper();
            }).As<IMapper>().SingleInstance();
        }
    }
}
