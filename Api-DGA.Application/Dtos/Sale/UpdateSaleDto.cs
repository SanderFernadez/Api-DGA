using Api_DGA.Application.Dtos.SaleProduct;

namespace Api_DGA.Application.Dtos.Sale
{
    /// <summary>
    /// DTO para actualizar una venta existente
    /// </summary>
    public class UpdateSaleDto
    {
        public int ClientId { get; set; }
        public List<CreateSaleProductDto> SaleProducts { get; set; } = new List<CreateSaleProductDto>();
    }
}
