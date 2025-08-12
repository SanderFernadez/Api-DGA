namespace Api_DGA.Application.Dtos.Product
{
    /// <summary>
    /// DTO para búsquedas de productos
    /// </summary>
    public class ProductSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
