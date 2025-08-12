using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Core.Entities;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Client
    /// </summary>
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly InfrastructureContext _dbContext;

        public ClientRepository(InfrastructureContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene un cliente por su email
        /// </summary>
        /// <param name="email">Email del cliente</param>
        /// <returns>Cliente encontrado o null</returns>
        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _dbContext.Clients
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        /// <summary>
        /// Obtiene un cliente por su teléfono
        /// </summary>
        /// <param name="phone">Teléfono del cliente</param>
        /// <returns>Cliente encontrado o null</returns>
        public async Task<Client?> GetByPhoneAsync(string phone)
        {
            return await _dbContext.Clients
                .FirstOrDefaultAsync(c => c.Phone == phone);
        }

        /// <summary>
        /// Verifica si existe un cliente con el email especificado
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Clients
                .AnyAsync(c => c.Email == email);
        }
    }
}
