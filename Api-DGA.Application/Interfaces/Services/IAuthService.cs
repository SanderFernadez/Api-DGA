using Api_DGA.Application.Dtos.Auth;
using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task<bool> RevokeTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
