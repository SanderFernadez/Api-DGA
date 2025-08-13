using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly InfrastructureContext _dbContext;

        public UserRepository(InfrastructureContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithRolesAsync(int id)
        {
            return await _dbContext.Set<User>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Set<User>()
                .AnyAsync(u => u.Email == email);
        }

        public async Task UpdateRefreshTokenAsync(int userId, string? refreshToken, DateTime? expiryTime)
        {
            var user = await _dbContext.Set<User>().FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = expiryTime;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
