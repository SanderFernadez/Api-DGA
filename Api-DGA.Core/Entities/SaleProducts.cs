
namespace Api_DGA.Core.Entities
{
    public class SaleProduct
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; } = null!;

        public decimal Total { get; set; }

        // Relación con productos
        public ICollection<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
    }
}
