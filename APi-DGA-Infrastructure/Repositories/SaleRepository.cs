using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Sale
    /// </summary>
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly InfrastructureContext _dbContext;

        public SaleRepository(InfrastructureContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Lista de ventas del cliente</returns>
        public async Task<List<Sale>> GetByClientAsync(int clientId)
        {
            return await _dbContext.Sales
                .Where(s => s.ClientId == clientId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Lista de ventas en el rango de fechas</returns>
        public async Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Sales
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene ventas por rango de total
        /// </summary>
        /// <param name="minTotal">Total mínimo</param>
        /// <param name="maxTotal">Total máximo</param>
        /// <returns>Lista de ventas en el rango de total</returns>
        public async Task<List<Sale>> GetByTotalRangeAsync(decimal minTotal, decimal maxTotal)
        {
            return await _dbContext.Sales
                .Where(s => s.Total >= minTotal && s.Total <= maxTotal)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene el total de ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Total de ventas del cliente</returns>
        public async Task<decimal> GetTotalByClientAsync(int clientId)
        {
            return await _dbContext.Sales
                .Where(s => s.ClientId == clientId)
                .SumAsync(s => s.Total);
        }

        /// <summary>
        /// Obtiene el total de ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Total de ventas en el rango de fechas</returns>
        public async Task<decimal> GetTotalByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Sales
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .SumAsync(s => s.Total);
        }
    }
}
