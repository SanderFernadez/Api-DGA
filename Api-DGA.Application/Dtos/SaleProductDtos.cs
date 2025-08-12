namespace Api_DGA.Application.Dtos
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
        public GetProductDto Product { get; set; } = null!;
    }

    /// <summary>
    /// DTO para crear un nuevo producto en una venta
    /// </summary>
    public class CreateSaleProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

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

