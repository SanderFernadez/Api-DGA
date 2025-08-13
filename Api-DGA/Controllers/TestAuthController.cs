using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_DGA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAuthController : ControllerBase
    {
        /// <summary>
        /// Endpoint público (sin autenticación)
        /// </summary>
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult Public()
        {
            return Ok(new { 
                message = "Este es un endpoint público", 
                timestamp = DateTime.UtcNow,
                status = "success"
            });
        }

        /// <summary>
        /// Endpoint protegido (requiere autenticación)
        /// </summary>
        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            var user = User.Identity?.Name ?? "Usuario desconocido";
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
            
            return Ok(new 
            { 
                message = "Este es un endpoint protegido", 
                user = user,
                userId = userId,
                email = userEmail,
                roles = roles,
                timestamp = DateTime.UtcNow,
                status = "success"
            });
        }

        /// <summary>
        /// Endpoint que requiere rol específico
        /// </summary>
        [HttpGet("admin")]
        [Authorize(Roles = "Administrador")]
        public IActionResult AdminOnly()
        {
            return Ok(new { 
                message = "Este endpoint solo es para administradores", 
                timestamp = DateTime.UtcNow,
                status = "success"
            });
        }

        /// <summary>
        /// Endpoint para verificar el estado de autenticación
        /// </summary>
        [HttpGet("status")]
        public IActionResult AuthStatus()
        {
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            var userName = User.Identity?.Name;
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            
            return Ok(new
            {
                isAuthenticated = isAuthenticated,
                userName = userName,
                claims = claims,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
