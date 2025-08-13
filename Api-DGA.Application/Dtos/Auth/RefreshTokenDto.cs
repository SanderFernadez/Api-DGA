using System.ComponentModel.DataAnnotations;

namespace Api_DGA.Application.Dtos.Auth
{
    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "El refresh token es requerido")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
