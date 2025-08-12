using Api_DGA.Application.Dtos.Product;

namespace Api_DGA.Application.Dtos.SaleProduct
{
    /// <summary>
    /// DTO para obtener información de un producto en una venta
    /// </summary>
    public class GetSaleProductDto
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
    }
}
