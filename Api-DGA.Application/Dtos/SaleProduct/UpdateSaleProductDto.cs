namespace Api_DGA.Application.Dtos.SaleProduct
{
    /// <summary>
    /// DTO para actualizar un producto en una venta existente
    /// </summary>
    public class UpdateSaleProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
