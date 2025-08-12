

using APi_DGA_Infrastructure.Contexts;
using APi_DGA_Infrastructure.Repositories;
using Api_DGA.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APi_DGA_Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<InfrastructureContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<InfrastructureContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(InfrastructureContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ISaleProductRepository, SaleProductRepository>();
            #endregion
        }



    }
}
