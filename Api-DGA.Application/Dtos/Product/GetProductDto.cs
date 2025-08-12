namespace Api_DGA.Application.Dtos.Product
{
    /// <summary>
    /// DTO para obtener información de un producto
    /// </summary>
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
