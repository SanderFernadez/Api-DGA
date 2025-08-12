

using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api_DGA.Application
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddTransient<IIngredientpository, IngredientRepository>();
            //services.AddTransient<IOrderRepository, OrderRepository>();
            //services.AddTransient<IOrderPlateRepository, OrderPlateRepository>();
            //services.AddTransient<IPlateIngredientRepository, PlateIngredientRepository>();
            //services.AddTransient<IPlateRepository, PlateRepository>();
            //services.AddTransient<ITableRepository, TableRepository>();
            #endregion
        }



    }
}
