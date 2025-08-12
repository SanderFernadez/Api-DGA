using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Core.Entities;
using AutoMapper;
using Api_DGA.Application.Dtos.Product;

namespace Api_DGA.Application.Services
{
    /// <summary>
    /// Servicio específico para la entidad Product
    /// </summary>
    public class ProductService : GenericService<CreateProductDto, UpdateProductDto, GetProductDto, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper) : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene productos por nombre (búsqueda parcial)
        /// </summary>
        /// <param name="name">Nombre del producto a buscar</param>
        /// <returns>Lista de productos que coinciden con el nombre</returns>
        public async Task<List<GetProductDto>> GetByNameAsync(string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            return _mapper.Map<List<GetProductDto>>(products);
        }

        /// <summary>
        /// Obtiene productos con stock bajo
        /// </summary>
        /// <param name="minStock">Stock mínimo</param>
        /// <returns>Lista de productos con stock bajo</returns>
        public async Task<List<GetProductDto>> GetLowStockAsync(int minStock = 10)
        {
            var products = await _productRepository.GetLowStockAsync(minStock);
            return _mapper.Map<List<GetProductDto>>(products);
        }

        /// <summary>
        /// Obtiene productos por rango de precios
        /// </summary>
        /// <param name="minPrice">Precio mínimo</param>
        /// <param name="maxPrice">Precio máximo</param>
        /// <returns>Lista de productos en el rango de precios</returns>
        public async Task<List<GetProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<List<GetProductDto>>(products);
        }

        /// <summary>
        /// Verifica si existe un producto con el nombre especificado
        /// </summary>
        /// <param name="name">Nombre del producto</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _productRepository.ExistsByNameAsync(name);
        }

        /// <summary>
        /// Busca productos por nombre (alias para GetByNameAsync)
        /// </summary>
        /// <param name="name">Nombre del producto a buscar</param>
        /// <returns>Lista de productos que coinciden con el nombre</returns>
        public async Task<List<GetProductDto>> SearchByNameAsync(string name)
        {
            return await GetByNameAsync(name);
        }
    }
}
