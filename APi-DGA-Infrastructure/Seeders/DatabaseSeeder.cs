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
                Console.WriteLine("🔧 Asegurando que la base de datos esté creada...");
                // Asegurar que la base de datos esté creada
                await _context.Database.EnsureCreatedAsync();
                Console.WriteLine("✅ Base de datos verificada");

                Console.WriteLine($"📋 Ejecutando {_seeders.Count()} seeders...");
                // Ejecutar seeders en orden específico
                foreach (var seeder in _seeders)
                {
                    Console.WriteLine($"🌱 Ejecutando seeder: {seeder.GetType().Name}");
                    await seeder.SeedAsync();
                    Console.WriteLine($"✅ Seeder completado: {seeder.GetType().Name}");
                }

                Console.WriteLine("💾 Guardando cambios en la base de datos...");
                await _context.SaveChangesAsync();
                Console.WriteLine("✅ Cambios guardados exitosamente");
            }
            catch (Exception ex)
            {
                // Log del error (en producción usar ILogger)
                Console.WriteLine($"❌ Error durante el seeding: {ex.Message}");
                Console.WriteLine($"📋 Stack trace: {ex.StackTrace}");
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
                _context.UserRoles.RemoveRange(_context.UserRoles);
                _context.Users.RemoveRange(_context.Users);
                _context.Roles.RemoveRange(_context.Roles);
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
