

namespace Api_DGA.Core.Entities
{
    public class Product
    {
        
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Relación con ventas
        public ICollection<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
    }
}

