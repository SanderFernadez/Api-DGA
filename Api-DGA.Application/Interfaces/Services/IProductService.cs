using Api_DGA.Application.Dtos.Product;

namespace Api_DGA.Application.Interfaces.Services
{
    /// <summary>
    /// Interfaz específica para el servicio de productos
    /// </summary>
    public interface IProductService : IGenericService<CreateProductDto, UpdateProductDto, GetProductDto, Core.Entities.Product>
    {
        /// <summary>
        /// Obtiene productos por nombre (búsqueda parcial)
        /// </summary>
        /// <param name="name">Nombre del producto a buscar</param>
        /// <returns>Lista de productos que coinciden con el nombre</returns>
        Task<List<GetProductDto>> GetByNameAsync(string name);

        /// <summary>
        /// Obtiene productos con stock bajo
        /// </summary>
        /// <param name="minStock">Stock mínimo</param>
        /// <returns>Lista de productos con stock bajo</returns>
        Task<List<GetProductDto>> GetLowStockAsync(int minStock = 10);

        /// <summary>
        /// Obtiene productos por rango de precios
        /// </summary>
        /// <param name="minPrice">Precio mínimo</param>
        /// <param name="maxPrice">Precio máximo</param>
        /// <returns>Lista de productos en el rango de precios</returns>
        Task<List<GetProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Verifica si existe un producto con el nombre especificado
        /// </summary>
        /// <param name="name">Nombre del producto</param>
        /// <returns>True si existe, false en caso contrario</returns>
        Task<bool> ExistsByNameAsync(string name);
    }
}
