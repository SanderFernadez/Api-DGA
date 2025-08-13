using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithRolesAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task UpdateRefreshTokenAsync(int userId, string? refreshToken, DateTime? expiryTime);
    }
}
