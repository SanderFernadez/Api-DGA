using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_DGA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimpleTestController : ControllerBase
    {
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult Public()
        {
            return Ok(new { message = "Endpoint público", timestamp = DateTime.UtcNow });
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Ok(new { 
                message = "Endpoint privado", 
                user = User.Identity?.Name,
                timestamp = DateTime.UtcNow 
            });
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Admin()
        {
            return Ok(new { 
                message = "Endpoint de administrador", 
                user = User.Identity?.Name,
                timestamp = DateTime.UtcNow 
            });
        }
    }
}
