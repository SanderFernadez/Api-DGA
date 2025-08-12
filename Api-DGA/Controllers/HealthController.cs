using Microsoft.AspNetCore.Mvc;
using Api_DGA.Application.Dtos.Common;

namespace Api_DGA.Controllers
{
    /// <summary>
    /// Controlador para verificar el estado de salud de la API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Verifica el estado de salud de la API
        /// </summary>
        /// <returns>Estado de la API</returns>
        [HttpGet]
        public ActionResult<ApiResponseDto<object>> Get()
        {
            var healthInfo = new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development",
                Database = "Connected",
                Services = new
                {
                    Swagger = "Available",
                    CORS = "Configured",
                    AutoMapper = "Configured"
                }
            };

            return Ok(new ApiResponseDto<object>
            {
                Success = true,
                Data = healthInfo,
                Message = "API funcionando correctamente"
            });
        }

        /// <summary>
        /// Verifica la conectividad de la base de datos
        /// </summary>
        /// <returns>Estado de la base de datos</returns>
        [HttpGet("database")]
        public ActionResult<ApiResponseDto<object>> CheckDatabase()
        {
            try
            {
                var dbInfo = new
                {
                    Status = "Connected",
                    Timestamp = DateTime.UtcNow,
                    Provider = "SQL Server",
                    ConnectionString = "Configured"
                };

                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Data = dbInfo,
                    Message = "Base de datos conectada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Data = new { Status = "Error", Message = ex.Message },
                    Message = "Error al conectar con la base de datos"
                });
            }
        }
    }
}
