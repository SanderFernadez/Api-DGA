using Api_DGA.Application.Dtos.SaleProduct;

namespace Api_DGA.Application.Dtos.Sale
{
    /// <summary>
    /// DTO para crear una nueva venta
    /// </summary>
    public class CreateSaleDto
    {
        public int ClientId { get; set; }
        public List<CreateSaleProductDto> SaleProducts { get; set; } = new List<CreateSaleProductDto>();
    }
}
