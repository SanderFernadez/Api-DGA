using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Seeders
{
    /// <summary>
    /// Seeder para poblar la tabla de ventas
    /// </summary>
    public class SaleSeeder : IDataSeeder
    {
        private readonly InfrastructureContext _context;

        public SaleSeeder(InfrastructureContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Verificar si ya existen ventas
            if (await _context.Sales.AnyAsync())
                return;

            // Obtener clientes y productos existentes
            var clients = await _context.Clients.ToListAsync();
            var products = await _context.Products.ToListAsync();

            if (!clients.Any() || !products.Any())
                return;

            var random = new Random();
            var sales = new List<Sale>();

            // Crear ventas de ejemplo para los últimos 30 días
            for (int i = 0; i < 25; i++)
            {
                var client = clients[random.Next(clients.Count)];
                var saleDate = DateTime.Now.AddDays(-random.Next(30));
                
                var sale = new Sale
                {
                    Date = saleDate,
                    ClientId = client.Id,
                    Total = 0 // Se calculará después con los productos
                };

                sales.Add(sale);
            }

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            // Crear productos de venta para cada venta
            var saleProducts = new List<SaleProduct>();

            foreach (var sale in sales)
            {
                // Agregar entre 1 y 4 productos por venta
                var numProducts = random.Next(1, 5);
                var selectedProducts = products.OrderBy(x => random.Next()).Take(numProducts).ToList();

                decimal totalSale = 0;

                foreach (var product in selectedProducts)
                {
                    var quantity = random.Next(1, 4);
                    var unitPrice = product.Price;
                    var subtotal = quantity * unitPrice;
                    totalSale += subtotal;

                    var saleProduct = new SaleProduct
                    {
                        SaleId = sale.Id,
                        ProductId = product.Id,
                        Quantity = quantity,
                        UnitPrice = unitPrice
                    };

                    saleProducts.Add(saleProduct);
                }

                // Actualizar el total de la venta
                sale.Total = totalSale;
            }

            await _context.SaleProducts.AddRangeAsync(saleProducts);
            await _context.SaveChangesAsync();
        }
    }
}
