namespace Api_DGA.Application.Dtos
{
    /// <summary>
    /// DTO para obtener información básica de una venta
    /// </summary>
    public class GetSaleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public decimal Total { get; set; }
    }

    /// <summary>
    /// DTO para obtener información detallada de una venta con cliente y productos
    /// </summary>
    public class GetSaleDetailDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public GetClientDto Client { get; set; } = null!;
        public decimal Total { get; set; }
        public List<GetSaleProductDto> SaleProducts { get; set; } = new List<GetSaleProductDto>();
    }

    /// <summary>
    /// DTO para crear una nueva venta
    /// </summary>
    public class CreateSaleDto
    {
        public int ClientId { get; set; }
        public List<CreateSaleProductDto> SaleProducts { get; set; } = new List<CreateSaleProductDto>();
    }

    /// <summary>
    /// DTO para actualizar una venta existente
    /// </summary>
    public class UpdateSaleDto
    {
        public int ClientId { get; set; }
        public List<CreateSaleProductDto> SaleProducts { get; set; } = new List<CreateSaleProductDto>();
    }
}

