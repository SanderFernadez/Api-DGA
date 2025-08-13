using Api_DGA.Core.Entities;

namespace Api_DGA.Application.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
        Task<List<Role>> GetActiveRolesAsync();
    }
}
