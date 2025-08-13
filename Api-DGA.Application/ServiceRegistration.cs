

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Services;

namespace Api_DGA.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            // Registrar AutoMapper con los perfiles de mapeo
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            #region Services
            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<ISaleProductService, SaleProductService>();
            
            // Servicios de autenticación
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtService, JwtService>();
            #endregion
        }
    }
}
