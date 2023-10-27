using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;

namespace ShowCaseAPI.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Data - Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreProductRepository, StoreProductRepository>();
            services.AddScoped<IShowcaseRepository, ShowcaseRepository>();
        }
    }
}