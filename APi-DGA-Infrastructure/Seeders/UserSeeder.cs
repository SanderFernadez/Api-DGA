using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace APi_DGA_Infrastructure.Seeders
{
    public class UserSeeder : IDataSeeder
    {
        private readonly InfrastructureContext _context;

        public UserSeeder(InfrastructureContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                // Crear usuario administrador
                var adminUser = new User
                {
                    Name = "Administrador",
                    Email = "admin@api-dga.com",
                    PasswordHash = HashPassword("Admin123!"),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Users.AddAsync(adminUser);
                await _context.SaveChangesAsync();

                // Asignar rol de administrador
                var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Administrador");
                if (adminRole != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = adminUser.Id,
                        RoleId = adminRole.Id,
                        AssignedAt = DateTime.UtcNow
                    };

                    await _context.UserRoles.AddAsync(userRole);
                    await _context.SaveChangesAsync();
                }

                Console.WriteLine("✅ Usuario administrador sembrado exitosamente");
                Console.WriteLine("📧 Email: admin@api-dga.com");
                Console.WriteLine("🔑 Contraseña: Admin123!");
            }
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }
    }
}
