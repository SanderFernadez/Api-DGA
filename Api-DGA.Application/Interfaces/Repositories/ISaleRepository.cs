using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz específica para el repositorio de ventas
    /// </summary>
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        /// <summary>
        /// Obtiene ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Lista de ventas del cliente</returns>
        Task<List<Sale>> GetByClientAsync(int clientId);

        /// <summary>
        /// Obtiene ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Lista de ventas en el rango de fechas</returns>
        Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Obtiene ventas por rango de total
        /// </summary>
        /// <param name="minTotal">Total mínimo</param>
        /// <param name="maxTotal">Total máximo</param>
        /// <returns>Lista de ventas en el rango de total</returns>
        Task<List<Sale>> GetByTotalRangeAsync(decimal minTotal, decimal maxTotal);

        /// <summary>
        /// Obtiene el total de ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Total de ventas del cliente</returns>
        Task<decimal> GetTotalByClientAsync(int clientId);

        /// <summary>
        /// Obtiene el total de ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Total de ventas en el rango de fechas</returns>
        Task<decimal> GetTotalByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
