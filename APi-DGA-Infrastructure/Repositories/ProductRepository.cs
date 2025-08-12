using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Product
    /// </summary>
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly InfrastructureContext _dbContext;

        public ProductRepository(InfrastructureContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene productos por nombre (búsqueda parcial)
        /// </summary>
        /// <param name="name">Nombre del producto a buscar</param>
        /// <returns>Lista de productos que coinciden con el nombre</returns>
        public async Task<List<Product>> GetByNameAsync(string name)
        {
            return await _dbContext.Products
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene productos con stock bajo (menor o igual al valor especificado)
        /// </summary>
        /// <param name="minStock">Stock mínimo</param>
        /// <returns>Lista de productos con stock bajo</returns>
        public async Task<List<Product>> GetLowStockAsync(int minStock = 10)
        {
            return await _dbContext.Products
                .Where(p => p.Stock <= minStock)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene productos por rango de precios
        /// </summary>
        /// <param name="minPrice">Precio mínimo</param>
        /// <param name="maxPrice">Precio máximo</param>
        /// <returns>Lista de productos en el rango de precios</returns>
        public async Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbContext.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        /// <summary>
        /// Verifica si existe un producto con el nombre especificado
        /// </summary>
        /// <param name="name">Nombre del producto</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbContext.Products
                .AnyAsync(p => p.Name == name);
        }
    }
}
