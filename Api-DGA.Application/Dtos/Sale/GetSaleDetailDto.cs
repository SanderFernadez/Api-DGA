using Api_DGA.Application.Dtos.Client;
using Api_DGA.Application.Dtos.SaleProduct;

namespace Api_DGA.Application.Dtos.Sale
{
    /// <summary>
    /// DTO para obtener información detallada de una venta con cliente y productos
    /// </summary>
    public class GetSaleDetailDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<GetSaleProductDto> SaleProducts { get; set; } = new List<GetSaleProductDto>();
    }
}
