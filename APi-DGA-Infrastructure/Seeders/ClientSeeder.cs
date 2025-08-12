using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Seeders
{
    /// <summary>
    /// Seeder para poblar la tabla de clientes
    /// </summary>
    public class ClientSeeder : IDataSeeder
    {
        private readonly InfrastructureContext _context;

        public ClientSeeder(InfrastructureContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Verificar si ya existen clientes
            if (await _context.Clients.AnyAsync())
                return;

            var clients = new List<Client>
            {
                new Client
                {
                    Name = "Juan Pérez",
                    Email = "juan.perez@email.com",
                    Phone = "+34 600 123 456"
                },
                new Client
                {
                    Name = "María García",
                    Email = "maria.garcia@email.com",
                    Phone = "+34 600 234 567"
                },
                new Client
                {
                    Name = "Carlos López",
                    Email = "carlos.lopez@email.com",
                    Phone = "+34 600 345 678"
                },
                new Client
                {
                    Name = "Ana Martínez",
                    Email = "ana.martinez@email.com",
                    Phone = "+34 600 456 789"
                },
                new Client
                {
                    Name = "Luis Rodríguez",
                    Email = "luis.rodriguez@email.com",
                    Phone = "+34 600 567 890"
                },
                new Client
                {
                    Name = "Carmen González",
                    Email = "carmen.gonzalez@email.com",
                    Phone = "+34 600 678 901"
                },
                new Client
                {
                    Name = "Pedro Fernández",
                    Email = "pedro.fernandez@email.com",
                    Phone = "+34 600 789 012"
                },
                new Client
                {
                    Name = "Lucía Moreno",
                    Email = "lucia.moreno@email.com",
                    Phone = "+34 600 890 123"
                },
                new Client
                {
                    Name = "Diego Jiménez",
                    Email = "diego.jimenez@email.com",
                    Phone = "+34 600 901 234"
                },
                new Client
                {
                    Name = "Isabel Ruiz",
                    Email = "isabel.ruiz@email.com",
                    Phone = "+34 600 012 345"
                }
            };

            await _context.Clients.AddRangeAsync(clients);
            await _context.SaveChangesAsync();
        }
    }
}
