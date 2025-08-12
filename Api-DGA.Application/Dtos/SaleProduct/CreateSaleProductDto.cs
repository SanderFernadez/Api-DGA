namespace Api_DGA.Application.Dtos.SaleProduct
{
    /// <summary>
    /// DTO para crear un nuevo producto en una venta
    /// </summary>
    public class CreateSaleProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
