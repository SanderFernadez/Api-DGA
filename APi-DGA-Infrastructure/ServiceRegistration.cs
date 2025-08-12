

using APi_DGA_Infrastructure.Contexts;
using APi_DGA_Infrastructure.Repositories;
using APi_DGA_Infrastructure.Seeders;
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

            #region Seeders
            services.AddTransient<IDataSeeder, ClientSeeder>();
            services.AddTransient<IDataSeeder, ProductSeeder>();
            services.AddTransient<IDataSeeder, SaleSeeder>();
            services.AddTransient<DatabaseSeeder>();
            #endregion
        }

        /// <summary>
        /// Ejecuta el seeding automático de la base de datos al iniciar la aplicación
        /// </summary>
        /// <param name="serviceProvider">Proveedor de servicios</param>
        /// <returns>Task</returns>
        public static async Task RunAsyncSeed(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var databaseSeeder = services.GetRequiredService<DatabaseSeeder>();
                    
                    Console.WriteLine("🌱 Iniciando seeding automático de la base de datos...");
                    await databaseSeeder.SeedAsync();
                    Console.WriteLine("✅ Base de datos poblada exitosamente!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error durante el seeding automático: {ex.Message}");
                    // No lanzar la excepción para que la aplicación pueda continuar
                }
            }
        }
    }
}
