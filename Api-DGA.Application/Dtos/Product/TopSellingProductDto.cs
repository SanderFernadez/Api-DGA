namespace Api_DGA.Application.Dtos.Product
{
    /// <summary>
    /// DTO para productos más vendidos
    /// </summary>
    public class TopSellingProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
