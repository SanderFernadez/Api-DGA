using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly InfrastructureContext _dbContext;

        public RoleRepository(InfrastructureContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _dbContext.Set<Role>()
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<List<Role>> GetActiveRolesAsync()
        {
            return await _dbContext.Set<Role>()
                .Where(r => r.IsActive)
                .ToListAsync();
        }
    }
}
