using Arya.Application;
using Arya.Domain.Entitties;
using Arya.Domain.Interfaces;
using Arya.Infrastructure.CrossCutting.Email;
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

            services.AddLocalization(o => o.ResourcesPath = "Resources");

            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IUserService, UserService>();

            #endregion

            #region Repositories

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            #endregion
        }
    }
}
