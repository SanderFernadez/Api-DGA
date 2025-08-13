using Api_DGA.Application.Dtos.Auth;
using Api_DGA.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_DGA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Iniciar sesión en el sistema
        /// </summary>
        /// <param name="loginDto">Credenciales de acceso</param>
        /// <returns>Token de acceso y información del usuario</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(typeof(AuthResponseDto), 400)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new AuthResponseDto
                    {
                        Success = false,
                        Message = "Datos de entrada inválidos"
                    });
                }

                Console.WriteLine($"🔐 Intento de login para: {loginDto.Email}");
                var result = await _authService.LoginAsync(loginDto);
                
                if (!result.Success)
                {
                    Console.WriteLine($"❌ Login fallido: {result.Message}");
                    return BadRequest(result);
                }

                Console.WriteLine($"✅ Login exitoso para: {loginDto.Email}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error en login: {ex.Message}");
                return StatusCode(500, new AuthResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Registrar un nuevo usuario
        /// </summary>
        /// <param name="registerDto">Datos del nuevo usuario</param>
        /// <returns>Resultado del registro</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(typeof(AuthResponseDto), 400)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Datos de entrada inválidos"
                });
            }

            var result = await _authService.RegisterAsync(registerDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Renovar token de acceso
        /// </summary>
        /// <param name="refreshTokenDto">Refresh token</param>
        /// <returns>Nuevo token de acceso</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(typeof(AuthResponseDto), 400)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Refresh token inválido"
                });
            }

            var result = await _authService.RefreshTokenAsync(refreshTokenDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Revocar token de acceso
        /// </summary>
        /// <param name="refreshToken">Refresh token a revocar</param>
        /// <returns>Resultado de la revocación</returns>
        [HttpPost("revoke-token")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            var result = await _authService.RevokeTokenAsync(refreshToken);
            
            if (!result)
            {
                return BadRequest(new { Message = "No se pudo revocar el token" });
            }

            return Ok(new { Message = "Token revocado exitosamente" });
        }

        /// <summary>
        /// Validar token de acceso
        /// </summary>
        /// <returns>Estado de validación del token</returns>
        [HttpGet("validate")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ValidateToken()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Token no proporcionado" });
            }

            var isValid = await _authService.ValidateTokenAsync(token);
            
            if (!isValid)
            {
                return Unauthorized(new { Message = "Token inválido" });
            }

            return Ok(new { Message = "Token válido" });
        }

        /// <summary>
        /// Endpoint de prueba para verificar credenciales
        /// </summary>
        /// <returns>Información del usuario administrador</returns>
        [HttpGet("test-admin")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> TestAdminAccess()
        {
            try
            {
                var userEmail = User.Identity?.Name;
                return Ok(new { 
                    message = "Acceso de administrador verificado", 
                    user = userEmail,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error en test-admin: {ex.Message}");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener todos los usuarios (solo para administradores)
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
                
                if (users == null || !users.Any())
                {
                    return Ok(new { message = "No se encontraron usuarios", users = new List<object>() });
                }

                var userList = users.Select(u => new
                {
                    id = u.Id,
                    name = u.Name,
                    email = u.Email,
                    isActive = u.IsActive,
                    createdAt = u.CreatedAt
                }).ToList();

                return Ok(new { message = "Usuarios obtenidos exitosamente", users = userList });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error al obtener usuarios: {ex.Message}");
                return StatusCode(500, new { message = "Error interno del servidor al obtener usuarios" });
            }
        }
    }
}
