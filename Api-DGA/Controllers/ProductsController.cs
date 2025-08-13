using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Dtos.Product;
using Api_DGA.Application.Dtos.Common;
using Api_DGA.Core.Entities;

namespace Api_DGA.Controllers
{
    /// <summary>
    /// Controller para gestionar productos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Proteger todo el controlador
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(new ApiResponseDto<List<GetProductDto>>
                {
                    Success = true,
                    Data = products,
                    Message = "Productos obtenidos exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener productos: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene un producto por su ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Producto encontrado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Producto no encontrado"
                    });
                }

                return Ok(new ApiResponseDto<GetProductDto>
                {
                    Success = true,
                    Data = product,
                    Message = "Producto obtenido exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener el producto: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        /// <param name="createDto">Datos del producto a crear</param>
        /// <returns>Producto creado</returns>
        [HttpPost]
        public async Task<ActionResult<GetProductDto>> Create([FromBody] CreateProductDto createDto)
        {
            try
            {
                var product = await _productService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, new ApiResponseDto<GetProductDto>
                {
                    Success = true,
                    Data = product,
                    Message = "Producto creado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al crear el producto: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <param name="updateDto">Datos actualizados del producto</param>
        /// <returns>Producto actualizado</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<GetProductDto>> Update(int id, [FromBody] UpdateProductDto updateDto)
        {
            try
            {
                var product = await _productService.UpdateAsync(id, updateDto);
                return Ok(new ApiResponseDto<GetProductDto>
                {
                    Success = true,
                    Data = product,
                    Message = "Producto actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al actualizar el producto: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Producto no encontrado"
                    });
                }

                return Ok(new ApiResponseDto
                {
                    Success = true,
                    Message = "Producto eliminado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al eliminar el producto: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Busca productos por nombre
        /// </summary>
        /// <param name="name">Nombre del producto a buscar</param>
        /// <returns>Lista de productos que coinciden con la búsqueda</returns>
        [HttpGet("search")]
        public async Task<ActionResult<List<GetProductDto>>> Search([FromQuery] string name)
        {
            try
            {
                var products = await _productService.SearchByNameAsync(name);
                return Ok(new ApiResponseDto<List<GetProductDto>>
                {
                    Success = true,
                    Data = products,
                    Message = "Búsqueda completada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error en la búsqueda: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene productos con stock bajo
        /// </summary>
        /// <param name="threshold">Umbral de stock (por defecto 10)</param>
        /// <returns>Lista de productos con stock bajo</returns>
        [HttpGet("low-stock")]
        public async Task<ActionResult<List<GetProductDto>>> GetLowStock([FromQuery] int threshold = 10)
        {
            try
            {
                var products = await _productService.GetLowStockAsync(threshold);
                return Ok(new ApiResponseDto<List<GetProductDto>>
                {
                    Success = true,
                    Data = products,
                    Message = "Productos con stock bajo obtenidos exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener productos con stock bajo: {ex.Message}"
                });
            }
        }
    }
}
