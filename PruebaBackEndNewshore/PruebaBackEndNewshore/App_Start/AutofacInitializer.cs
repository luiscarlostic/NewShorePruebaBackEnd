using Autofac;
using Autofac.Integration.WebApi;
using NewshoreAir.Domain.CacheService;
using NewshoreAir.Domain.ConfigurationService;
using NewshoreAir.Domain.FlightService;
using NewshoreAir.Domain.JourneyService;
using NewshoreAir.Domain.LogService;
using System.Reflection;
using System.Web.Http;

namespace PruebaBackEndNewshore.App_Start
{
    public class AutofacInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<FligthService>().As<IFlightService>().InstancePerLifetimeScope();
            builder.RegisterType<JourneyService>().As<IJourneyService>().InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<CacheService>().As<ICacheService>().InstancePerLifetimeScope();
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
