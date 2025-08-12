using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Dtos;
using Api_DGA.Core.Entities;
using AutoMapper;
using Api_DGA.Application.Dtos.Sale;

namespace Api_DGA.Application.Services
{
    /// <summary>
    /// Servicio específico para la entidad Sale
    /// </summary>
    public class SaleService : GenericService<CreateSaleDto, UpdateSaleDto, GetSaleDto, Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper) : base(saleRepository, mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Lista de ventas del cliente</returns>
        public async Task<List<GetSaleDto>> GetByClientAsync(int clientId)
        {
            var sales = await _saleRepository.GetByClientAsync(clientId);
            return _mapper.Map<List<GetSaleDto>>(sales);
        }

        /// <summary>
        /// Obtiene ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Lista de ventas en el rango de fechas</returns>
        public async Task<List<GetSaleDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<List<GetSaleDto>>(sales);
        }

        /// <summary>
        /// Obtiene ventas por rango de total
        /// </summary>
        /// <param name="minTotal">Total mínimo</param>
        /// <param name="maxTotal">Total máximo</param>
        /// <returns>Lista de ventas en el rango de total</returns>
        public async Task<List<GetSaleDto>> GetByTotalRangeAsync(decimal minTotal, decimal maxTotal)
        {
            var sales = await _saleRepository.GetByTotalRangeAsync(minTotal, maxTotal);
            return _mapper.Map<List<GetSaleDto>>(sales);
        }

        /// <summary>
        /// Obtiene el total de ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Total de ventas del cliente</returns>
        public async Task<decimal> GetTotalByClientAsync(int clientId)
        {
            return await _saleRepository.GetTotalByClientAsync(clientId);
        }

        /// <summary>
        /// Obtiene el total de ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Total de ventas en el rango de fechas</returns>
        public async Task<decimal> GetTotalByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _saleRepository.GetTotalByDateRangeAsync(startDate, endDate);
        }

        /// <summary>
        /// Obtiene una venta con detalles completos
        /// </summary>
        /// <param name="id">ID de la venta</param>
        /// <returns>Venta con detalles completos</returns>
        public async Task<GetSaleDetailDto?> GetSaleDetailAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return null;

            return _mapper.Map<GetSaleDetailDto>(sale);
        }
    }
}
