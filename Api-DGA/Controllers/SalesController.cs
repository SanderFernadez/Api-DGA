using Microsoft.AspNetCore.Mvc;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Dtos.Sale;
using Api_DGA.Application.Dtos.Common;

namespace Api_DGA.Controllers
{
    /// <summary>
    /// Controller para gestionar ventas
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Obtiene todas las ventas
        /// </summary>
        /// <returns>Lista de ventas</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetSaleDto>>> GetAll()
        {
            try
            {
                var sales = await _saleService.GetAllAsync();
                return Ok(new ApiResponseDto<List<GetSaleDto>>
                {
                    Success = true,
                    Data = sales,
                    Message = "Ventas obtenidas exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener ventas: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene una venta por su ID
        /// </summary>
        /// <param name="id">ID de la venta</param>
        /// <returns>Venta encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSaleDto>> GetById(int id)
        {
            try
            {
                var sale = await _saleService.GetByIdAsync(id);
                if (sale == null)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Venta no encontrada"
                    });
                }

                return Ok(new ApiResponseDto<GetSaleDto>
                {
                    Success = true,
                    Data = sale,
                    Message = "Venta obtenida exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener la venta: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene una venta con detalles por su ID
        /// </summary>
        /// <param name="id">ID de la venta</param>
        /// <returns>Venta con detalles encontrada</returns>
        [HttpGet("{id}/details")]
        public async Task<ActionResult<GetSaleDetailDto>> GetByIdWithDetails(int id)
        {
            try
            {
                var sale = await _saleService.GetByIdWithDetailsAsync(id);
                if (sale == null)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Venta no encontrada"
                    });
                }

                return Ok(new ApiResponseDto<GetSaleDetailDto>
                {
                    Success = true,
                    Data = sale,
                    Message = "Venta con detalles obtenida exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener la venta con detalles: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Crea una nueva venta
        /// </summary>
        /// <param name="createDto">Datos de la venta a crear</param>
        /// <returns>Venta creada</returns>
        [HttpPost]
        public async Task<ActionResult<GetSaleDto>> Create([FromBody] CreateSaleDto createDto)
        {
            try
            {
                var sale = await _saleService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = sale.Id }, new ApiResponseDto<GetSaleDto>
                {
                    Success = true,
                    Data = sale,
                    Message = "Venta creada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al crear la venta: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Actualiza una venta existente
        /// </summary>
        /// <param name="id">ID de la venta</param>
        /// <param name="updateDto">Datos actualizados de la venta</param>
        /// <returns>Venta actualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GetSaleDto>> Update(int id, [FromBody] UpdateSaleDto updateDto)
        {
            try
            {
                var sale = await _saleService.UpdateAsync(id, updateDto);
                return Ok(new ApiResponseDto<GetSaleDto>
                {
                    Success = true,
                    Data = sale,
                    Message = "Venta actualizada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al actualizar la venta: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Elimina una venta
        /// </summary>
        /// <param name="id">ID de la venta</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _saleService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Venta no encontrada"
                    });
                }

                return Ok(new ApiResponseDto
                {
                    Success = true,
                    Message = "Venta eliminada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al eliminar la venta: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene ventas por cliente
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Lista de ventas del cliente</returns>
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<List<GetSaleDto>>> GetByClient(int clientId)
        {
            try
            {
                var sales = await _saleService.GetByClientAsync(clientId);
                return Ok(new ApiResponseDto<List<GetSaleDto>>
                {
                    Success = true,
                    Data = sales,
                    Message = "Ventas del cliente obtenidas exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener las ventas del cliente: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene ventas por rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Lista de ventas en el rango de fechas</returns>
        [HttpGet("date-range")]
        public async Task<ActionResult<List<GetSaleDto>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sales = await _saleService.GetByDateRangeAsync(startDate, endDate);
                return Ok(new ApiResponseDto<List<GetSaleDto>>
                {
                    Success = true,
                    Data = sales,
                    Message = "Ventas por rango de fechas obtenidas exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener las ventas por rango de fechas: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene reporte de ventas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Reporte de ventas</returns>
        [HttpGet("report")]
        public async Task<ActionResult<List<SaleReportDto>>> GetReport([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var report = await _saleService.GetSalesReportAsync(startDate, endDate);
                return Ok(new ApiResponseDto<List<SaleReportDto>>
                {
                    Success = true,
                    Data = report,
                    Message = "Reporte de ventas obtenido exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener el reporte de ventas: {ex.Message}"
                });
            }
        }
    }
}
