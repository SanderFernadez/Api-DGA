using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Seeders
{
    public class RoleSeeder : IDataSeeder
    {
        private readonly InfrastructureContext _context;

        public RoleSeeder(InfrastructureContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Administrador",
                        Description = "Usuario con acceso completo al sistema",
                        IsActive = true
                    },
                    new Role
                    {
                        Name = "Usuario",
                        Description = "Usuario con acceso limitado",
                        IsActive = true
                    },
                    new Role
                    {
                        Name = "Vendedor",
                        Description = "Usuario con acceso a ventas",
                        IsActive = true
                    }
                };

                await _context.Roles.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
                
                Console.WriteLine("✅ Roles sembrados exitosamente");
            }
        }
    }
}
