using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad SaleProduct
    /// </summary>
    public class SaleProductRepository : GenericRepository<SaleProduct>, ISaleProductRepository
    {
        private readonly InfrastructureContext _dbContext;

        public SaleProductRepository(InfrastructureContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene productos de una venta específica
        /// </summary>
        /// <param name="saleId">ID de la venta</param>
        /// <returns>Lista de productos de la venta</returns>
        public async Task<List<SaleProduct>> GetBySaleAsync(int saleId)
        {
            return await _dbContext.SaleProducts
                .Where(sp => sp.SaleId == saleId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene ventas de un producto específico
        /// </summary>
        /// <param name="productId">ID del producto</param>
        /// <returns>Lista de ventas del producto</returns>
        public async Task<List<SaleProduct>> GetByProductAsync(int productId)
        {
            return await _dbContext.SaleProducts
                .Where(sp => sp.ProductId == productId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene el total vendido de un producto
        /// </summary>
        /// <param name="productId">ID del producto</param>
        /// <returns>Total vendido del producto</returns>
        public async Task<decimal> GetTotalByProductAsync(int productId)
        {
            return await _dbContext.SaleProducts
                .Where(sp => sp.ProductId == productId)
                .SumAsync(sp => sp.Quantity * sp.UnitPrice);
        }

        /// <summary>
        /// Obtiene productos más vendidos
        /// </summary>
        /// <param name="limit">Límite de productos a retornar</param>
        /// <returns>Lista de productos más vendidos</returns>
        public async Task<List<SaleProduct>> GetTopSellingProductsAsync(int limit = 10)
        {
            return await _dbContext.SaleProducts
                .GroupBy(sp => sp.ProductId)
                .Select(g => new SaleProduct
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(sp => sp.Quantity),
                    UnitPrice = g.Average(sp => sp.UnitPrice)
                })
                .OrderByDescending(sp => sp.Quantity)
                .Take(limit)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene productos por rango de cantidad
        /// </summary>
        /// <param name="minQuantity">Cantidad mínima</param>
        /// <param name="maxQuantity">Cantidad máxima</param>
        /// <returns>Lista de productos en el rango de cantidad</returns>
        public async Task<List<SaleProduct>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity)
        {
            return await _dbContext.SaleProducts
                .Where(sp => sp.Quantity >= minQuantity && sp.Quantity <= maxQuantity)
                .ToListAsync();
        }
    }
}
