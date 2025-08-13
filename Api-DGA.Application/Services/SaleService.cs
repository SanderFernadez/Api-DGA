using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Core.Entities;
using AutoMapper;
using Api_DGA.Application.Dtos.Sale;
using Api_DGA.Application.Dtos.SaleProduct;

namespace Api_DGA.Application.Services
{
    /// <summary>
    /// Servicio específico para la entidad Sale
    /// </summary>
    public class SaleService : GenericService<CreateSaleDto, UpdateSaleDto, GetSaleDto, Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, ISaleProductRepository saleProductRepository, IMapper mapper) : base(saleRepository, mapper)
        {
            _saleRepository = saleRepository;
            _saleProductRepository = saleProductRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva venta con sus productos
        /// </summary>
        /// <param name="createDto">DTO con los datos de la venta y productos</param>
        /// <returns>DTO de la venta creada</returns>
        public override async Task<GetSaleDto> CreateAsync(CreateSaleDto createDto)
        {
            // Crear la venta principal
            var sale = new Sale
            {
                ClientId = createDto.ClientId,
                Date = DateTime.Now,
                Total = 0 // Se calculará después
            };

            // Guardar la venta para obtener el ID
            var createdSale = await _saleRepository.AddAsync(sale);

            // Calcular el total y crear los productos de la venta
            decimal total = 0;
            var saleProducts = new List<SaleProduct>();

            foreach (var productDto in createDto.SaleProducts)
            {
                var saleProduct = new SaleProduct
                {
                    SaleId = createdSale.Id,
                    ProductId = productDto.ProductId,
                    Quantity = productDto.Quantity,
                    UnitPrice = productDto.UnitPrice
                };

                saleProducts.Add(saleProduct);
                total += productDto.Quantity * productDto.UnitPrice;
            }

            // Guardar los productos de la venta
            await _saleProductRepository.AddRangeAsync(saleProducts);

            // Actualizar el total de la venta
            createdSale.Total = total;
            await _saleRepository.UpdateAsync(createdSale, createdSale.Id);

            // Retornar la venta creada
            return _mapper.Map<GetSaleDto>(createdSale);
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

        /// <summary>
        /// Obtiene una venta con detalles por ID (alias para GetSaleDetailAsync)
        /// </summary>
        /// <param name="id">ID de la venta</param>
        /// <returns>Venta con detalles completos</returns>
        public async Task<GetSaleDetailDto?> GetByIdWithDetailsAsync(int id)
        {
            return await GetSaleDetailAsync(id);
        }

        /// <summary>
        /// Obtiene reporte de ventas
        /// </summary>
        /// <param name="startDate">Fecha de inicio (opcional)</param>
        /// <param name="endDate">Fecha de fin (opcional)</param>
        /// <returns>Lista de reportes de ventas</returns>
        public async Task<List<SaleReportDto>> GetSalesReportAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(
                startDate ?? DateTime.MinValue, 
                endDate ?? DateTime.MaxValue);
            
            return _mapper.Map<List<SaleReportDto>>(sales);
        }
    }
}
