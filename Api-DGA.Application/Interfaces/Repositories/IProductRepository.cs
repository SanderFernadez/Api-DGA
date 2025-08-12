using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz específica para el repositorio de productos
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product>
    {
        /// <summary>
        /// Obtiene productos por nombre (búsqueda parcial)
        /// </summary>
        /// <param name="name">Nombre del producto a buscar</param>
        /// <returns>Lista de productos que coinciden con el nombre</returns>
        Task<List<Product>> GetByNameAsync(string name);

        /// <summary>
        /// Obtiene productos con stock bajo (menor o igual al valor especificado)
        /// </summary>
        /// <param name="minStock">Stock mínimo</param>
        /// <returns>Lista de productos con stock bajo</returns>
        Task<List<Product>> GetLowStockAsync(int minStock = 10);

        /// <summary>
        /// Obtiene productos por rango de precios
        /// </summary>
        /// <param name="minPrice">Precio mínimo</param>
        /// <param name="maxPrice">Precio máximo</param>
        /// <returns>Lista de productos en el rango de precios</returns>
        Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Verifica si existe un producto con el nombre especificado
        /// </summary>
        /// <param name="name">Nombre del producto</param>
        /// <returns>True si existe, false en caso contrario</returns>
        Task<bool> ExistsByNameAsync(string name);
    }
}
