using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;

namespace ShowCaseAPI.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Data - Repository
            services.AddScoped<IProductRepository, ProductRepository>();



            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //});

            //services.AddTransient<IAppDbContext, ApplicationDbContext>();
        }
    }
}