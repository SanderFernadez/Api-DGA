using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, List<string> roles);
        string GenerateRefreshToken();
        bool ValidateToken(string token);
        int GetUserIdFromToken(string token);
        string GetUserEmailFromToken(string token);
        List<string> GetUserRolesFromToken(string token);
    }
}
