using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Repositories
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<List<UserRole>> GetByUserIdAsync(int userId);
        Task<List<UserRole>> GetByRoleIdAsync(int roleId);
        Task<bool> UserHasRoleAsync(int userId, int roleId);
    }
}
