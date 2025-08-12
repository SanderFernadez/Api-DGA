using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Seeders
{
    /// <summary>
    /// Seeder principal que coordina todos los seeders de la base de datos
    /// </summary>
    public class DatabaseSeeder
    {
        private readonly InfrastructureContext _context;
        private readonly IEnumerable<IDataSeeder> _seeders;

        public DatabaseSeeder(InfrastructureContext context, IEnumerable<IDataSeeder> seeders)
        {
            _context = context;
            _seeders = seeders;
        }

        /// <summary>
        /// Ejecuta todos los seeders en el orden correcto
        /// </summary>
        /// <returns>Task</returns>
        public async Task SeedAsync()
        {
            try
            {
                // Asegurar que la base de datos esté creada
                await _context.Database.EnsureCreatedAsync();

                // Ejecutar seeders en orden específico
                foreach (var seeder in _seeders)
                {
                    await seeder.SeedAsync();
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log del error (en producción usar ILogger)
                Console.WriteLine($"Error durante el seeding: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Limpia todos los datos de la base de datos
        /// </summary>
        /// <returns>Task</returns>
        public async Task ClearAsync()
        {
            try
            {
                // Eliminar datos en orden inverso para respetar las foreign keys
                _context.SaleProducts.RemoveRange(_context.SaleProducts);
                _context.Sales.RemoveRange(_context.Sales);
                _context.Products.RemoveRange(_context.Products);
                _context.Clients.RemoveRange(_context.Clients);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la limpieza: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Limpia y repuebla la base de datos
        /// </summary>
        /// <returns>Task</returns>
        public async Task ResetAsync()
        {
            await ClearAsync();
            await SeedAsync();
        }
    }
}
