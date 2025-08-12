
namespace Api_DGA.Core.Entities
{
    public class SaleProduct
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Propiedades de navegación
        public Sale Sale { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
