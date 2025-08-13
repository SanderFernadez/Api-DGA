using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        private readonly InfrastructureContext _dbContext;

        public UserRoleRepository(InfrastructureContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<UserRole>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Set<UserRole>()
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<UserRole>> GetByRoleIdAsync(int roleId)
        {
            return await _dbContext.Set<UserRole>()
                .Include(ur => ur.User)
                .Where(ur => ur.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<bool> UserHasRoleAsync(int userId, int roleId)
        {
            return await _dbContext.Set<UserRole>()
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
