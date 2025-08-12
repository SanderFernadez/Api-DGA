using Microsoft.AspNetCore.Mvc;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Dtos.Client;
using Api_DGA.Application.Dtos.Common;

namespace Api_DGA.Controllers
{
    /// <summary>
    /// Controller para gestionar clientes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetClientDto>>> GetAll()
        {
            try
            {
                var clients = await _clientService.GetAllAsync();
                return Ok(new ApiResponseDto<List<GetClientDto>>
                {
                    Success = true,
                    Data = clients,
                    Message = "Clientes obtenidos exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener clientes: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClientDto>> GetById(int id)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(id);
                if (client == null)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Cliente no encontrado"
                    });
                }

                return Ok(new ApiResponseDto<GetClientDto>
                {
                    Success = true,
                    Data = client,
                    Message = "Cliente obtenido exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener el cliente: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        /// <param name="createDto">Datos del cliente a crear</param>
        /// <returns>Cliente creado</returns>
        [HttpPost]
        public async Task<ActionResult<GetClientDto>> Create([FromBody] CreateClientDto createDto)
        {
            try
            {
                var client = await _clientService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = client.Id }, new ApiResponseDto<GetClientDto>
                {
                    Success = true,
                    Data = client,
                    Message = "Cliente creado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al crear el cliente: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <param name="updateDto">Datos actualizados del cliente</param>
        /// <returns>Cliente actualizado</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GetClientDto>> Update(int id, [FromBody] UpdateClientDto updateDto)
        {
            try
            {
                var client = await _clientService.UpdateAsync(id, updateDto);
                return Ok(new ApiResponseDto<GetClientDto>
                {
                    Success = true,
                    Data = client,
                    Message = "Cliente actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al actualizar el cliente: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Elimina un cliente
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _clientService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Cliente no encontrado"
                    });
                }

                return Ok(new ApiResponseDto
                {
                    Success = true,
                    Message = "Cliente eliminado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al eliminar el cliente: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Busca un cliente por email
        /// </summary>
        /// <param name="email">Email del cliente</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<GetClientDto>> GetByEmail(string email)
        {
            try
            {
                var client = await _clientService.GetByEmailAsync(email);
                if (client == null)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Cliente no encontrado"
                    });
                }

                return Ok(new ApiResponseDto<GetClientDto>
                {
                    Success = true,
                    Data = client,
                    Message = "Cliente obtenido exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener el cliente: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Busca un cliente por teléfono
        /// </summary>
        /// <param name="phone">Teléfono del cliente</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("phone/{phone}")]
        public async Task<ActionResult<GetClientDto>> GetByPhone(string phone)
        {
            try
            {
                var client = await _clientService.GetByPhoneAsync(phone);
                if (client == null)
                {
                    return NotFound(new ApiResponseDto
                    {
                        Success = false,
                        Message = "Cliente no encontrado"
                    });
                }

                return Ok(new ApiResponseDto<GetClientDto>
                {
                    Success = true,
                    Data = client,
                    Message = "Cliente obtenido exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener el cliente: {ex.Message}"
                });
            }
        }
    }
}
