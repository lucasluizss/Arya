using Arya.Application;
using Arya.Domain.Entities;
using Arya.Domain.Interfaces;
using Arya.Infrastructure.Data.Repository;
using Arya.Service;
using Arya.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Tyrion;

namespace Arya.Infrastructure.Ioc
{
    public static class BootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region CQRS

            services.AddTyrion(typeof(AssemblyLocator));

            #endregion

            #region Services

            Services(services);

            #endregion

            #region Repositories

            Repositories(services);

            #endregion
        }

        private static void Repositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }

        private static void Services(IServiceCollection services)
        {
            services.AddLocalization(o => o.ResourcesPath = "Resources");

            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IUserService, UserService>();
        }
    }
}
