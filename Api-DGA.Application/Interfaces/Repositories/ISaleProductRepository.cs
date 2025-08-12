using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz específica para el repositorio de productos en ventas
    /// </summary>
    public interface ISaleProductRepository : IGenericRepository<SaleProduct>
    {
        /// <summary>
        /// Obtiene productos de una venta específica
        /// </summary>
        /// <param name="saleId">ID de la venta</param>
        /// <returns>Lista de productos de la venta</returns>
        Task<List<SaleProduct>> GetBySaleAsync(int saleId);

        /// <summary>
        /// Obtiene ventas de un producto específico
        /// </summary>
        /// <param name="productId">ID del producto</param>
        /// <returns>Lista de ventas del producto</returns>
        Task<List<SaleProduct>> GetByProductAsync(int productId);

        /// <summary>
        /// Obtiene el total vendido de un producto
        /// </summary>
        /// <param name="productId">ID del producto</param>
        /// <returns>Total vendido del producto</returns>
        Task<decimal> GetTotalByProductAsync(int productId);

        /// <summary>
        /// Obtiene productos más vendidos
        /// </summary>
        /// <param name="limit">Límite de productos a retornar</param>
        /// <returns>Lista de productos más vendidos</returns>
        Task<List<SaleProduct>> GetTopSellingProductsAsync(int limit = 10);

        /// <summary>
        /// Obtiene productos por rango de cantidad
        /// </summary>
        /// <param name="minQuantity">Cantidad mínima</param>
        /// <param name="maxQuantity">Cantidad máxima</param>
        /// <returns>Lista de productos en el rango de cantidad</returns>
        Task<List<SaleProduct>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity);
    }
}
