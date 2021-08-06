using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Swo.Chaas.Products.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
