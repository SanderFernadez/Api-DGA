using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Seeders
{
    /// <summary>
    /// Seeder para poblar la tabla de productos
    /// </summary>
    public class ProductSeeder : IDataSeeder
    {
        private readonly InfrastructureContext _context;

        public ProductSeeder(InfrastructureContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Verificar si ya existen productos
            if (await _context.Products.AnyAsync())
                return;

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop HP Pavilion",
                    Description = "Laptop de 15 pulgadas con procesador Intel i5, 8GB RAM, 256GB SSD",
                    Price = 699.99m,
                    Stock = 25
                },
                new Product
                {
                    Name = "Mouse Inalámbrico Logitech",
                    Description = "Mouse inalámbrico con sensor óptico de alta precisión",
                    Price = 29.99m,
                    Stock = 50
                },
                new Product
                {
                    Name = "Teclado Mecánico Corsair",
                    Description = "Teclado mecánico con switches Cherry MX Red y retroiluminación RGB",
                    Price = 129.99m,
                    Stock = 15
                },
                new Product
                {
                    Name = "Monitor Samsung 24\"",
                    Description = "Monitor LED de 24 pulgadas con resolución Full HD",
                    Price = 199.99m,
                    Stock = 30
                },
                new Product
                {
                    Name = "Auriculares Sony WH-1000XM4",
                    Description = "Auriculares inalámbricos con cancelación de ruido activa",
                    Price = 349.99m,
                    Stock = 20
                },
                new Product
                {
                    Name = "Webcam Logitech C920",
                    Description = "Webcam HD con micrófono integrado para videoconferencias",
                    Price = 79.99m,
                    Stock = 40
                },
                new Product
                {
                    Name = "Disco Duro Externo Seagate 1TB",
                    Description = "Disco duro externo portátil de 1TB con conexión USB 3.0",
                    Price = 59.99m,
                    Stock = 35
                },
                new Product
                {
                    Name = "Memoria RAM Kingston 16GB",
                    Description = "Módulo de memoria RAM DDR4 de 16GB a 3200MHz",
                    Price = 89.99m,
                    Stock = 25
                },
                new Product
                {
                    Name = "Tarjeta Gráfica NVIDIA RTX 3060",
                    Description = "Tarjeta gráfica gaming con 12GB GDDR6 y ray tracing",
                    Price = 399.99m,
                    Stock = 8
                },
                new Product
                {
                    Name = "Impresora HP LaserJet",
                    Description = "Impresora láser monocromática con WiFi y escáner integrado",
                    Price = 249.99m,
                    Stock = 12
                },
                new Product
                {
                    Name = "Tablet Samsung Galaxy Tab A",
                    Description = "Tablet Android de 10.1 pulgadas con 32GB de almacenamiento",
                    Price = 199.99m,
                    Stock = 18
                },
                new Product
                {
                    Name = "Cable HDMI Premium",
                    Description = "Cable HDMI de alta velocidad compatible con 4K y HDR",
                    Price = 19.99m,
                    Stock = 100
                },
                new Product
                {
                    Name = "Cargador USB-C Rápido",
                    Description = "Cargador de pared USB-C de 65W con carga rápida",
                    Price = 34.99m,
                    Stock = 45
                },
                new Product
                {
                    Name = "Alfombrilla Gaming RGB",
                    Description = "Alfombrilla para mouse gaming con iluminación RGB personalizable",
                    Price = 39.99m,
                    Stock = 30
                },
                new Product
                {
                    Name = "Soporte para Monitor",
                    Description = "Soporte articulado para monitor con brazo ajustable",
                    Price = 89.99m,
                    Stock = 15
                }
            };

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }
}
