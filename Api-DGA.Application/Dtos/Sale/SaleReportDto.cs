namespace Api_DGA.Application.Dtos.Sale
{
    /// <summary>
    /// DTO para reportes de ventas
    /// </summary>
    public class SaleReportDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int ProductCount { get; set; }
    }
}
