using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Dtos;
using Api_DGA.Core.Entities;
using AutoMapper;

namespace Api_DGA.Application.Services
{
    /// <summary>
    /// Servicio específico para la entidad SaleProduct
    /// </summary>
    public class SaleProductService : GenericService<CreateSaleProductDto, UpdateSaleProductDto, GetSaleProductDto, SaleProduct>, ISaleProductService
    {
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IMapper _mapper;

        public SaleProductService(ISaleProductRepository saleProductRepository, IMapper mapper) : base(saleProductRepository, mapper)
        {
            _saleProductRepository = saleProductRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene productos de una venta específica
        /// </summary>
        /// <param name="saleId">ID de la venta</param>
        /// <returns>Lista de productos de la venta</returns>
        public async Task<List<GetSaleProductDto>> GetBySaleAsync(int saleId)
        {
            var saleProducts = await _saleProductRepository.GetBySaleAsync(saleId);
            return _mapper.Map<List<GetSaleProductDto>>(saleProducts);
        }

        /// <summary>
        /// Obtiene ventas de un producto específico
        /// </summary>
        /// <param name="productId">ID del producto</param>
        /// <returns>Lista de ventas del producto</returns>
        public async Task<List<GetSaleProductDto>> GetByProductAsync(int productId)
        {
            var saleProducts = await _saleProductRepository.GetByProductAsync(productId);
            return _mapper.Map<List<GetSaleProductDto>>(saleProducts);
        }

        /// <summary>
        /// Obtiene el total vendido de un producto
        /// </summary>
        /// <param name="productId">ID del producto</param>
        /// <returns>Total vendido del producto</returns>
        public async Task<decimal> GetTotalByProductAsync(int productId)
        {
            return await _saleProductRepository.GetTotalByProductAsync(productId);
        }

        /// <summary>
        /// Obtiene productos más vendidos
        /// </summary>
        /// <param name="limit">Límite de productos a retornar</param>
        /// <returns>Lista de productos más vendidos</returns>
        public async Task<List<GetSaleProductDto>> GetTopSellingProductsAsync(int limit = 10)
        {
            var topProducts = await _saleProductRepository.GetTopSellingProductsAsync(limit);
            return _mapper.Map<List<GetSaleProductDto>>(topProducts);
        }

        /// <summary>
        /// Obtiene productos por rango de cantidad
        /// </summary>
        /// <param name="minQuantity">Cantidad mínima</param>
        /// <param name="maxQuantity">Cantidad máxima</param>
        /// <returns>Lista de productos en el rango de cantidad</returns>
        public async Task<List<GetSaleProductDto>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity)
        {
            var saleProducts = await _saleProductRepository.GetByQuantityRangeAsync(minQuantity, maxQuantity);
            return _mapper.Map<List<GetSaleProductDto>>(saleProducts);
        }
    }
}
