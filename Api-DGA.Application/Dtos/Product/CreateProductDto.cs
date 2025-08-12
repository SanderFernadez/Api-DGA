namespace Api_DGA.Application.Dtos.Product
{
    /// <summary>
    /// DTO para crear un nuevo producto
    /// </summary>
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
