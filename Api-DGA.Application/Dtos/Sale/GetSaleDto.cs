using Api_DGA.Application.Dtos.Client;

namespace Api_DGA.Application.Dtos.Sale
{
    /// <summary>
    /// DTO para obtener información básica de una venta
    /// </summary>
    public class GetSaleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
