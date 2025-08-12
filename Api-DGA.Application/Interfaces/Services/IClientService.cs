using Api_DGA.Application.Dtos;
using Api_DGA.Application.Dtos.Client;

namespace Api_DGA.Application.Interfaces.Services
{
    /// <summary>
    /// Interfaz específica para el servicio de clientes
    /// </summary>
    public interface IClientService : IGenericService<CreateClientDto, UpdateClientDto, GetClientDto, Core.Entities.Client>
    {
        /// <summary>
        /// Obtiene un cliente por su email
        /// </summary>
        /// <param name="email">Email del cliente</param>
        /// <returns>Cliente encontrado o null</returns>
        Task<GetClientDto?> GetByEmailAsync(string email);

        /// <summary>
        /// Obtiene un cliente por su teléfono
        /// </summary>
        /// <param name="phone">Teléfono del cliente</param>
        /// <returns>Cliente encontrado o null</returns>
        Task<GetClientDto?> GetByPhoneAsync(string phone);

        /// <summary>
        /// Verifica si existe un cliente con el email especificado
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        Task<bool> ExistsByEmailAsync(string email);

        /// <summary>
        /// Obtiene clientes por nombre (búsqueda parcial)
        /// </summary>
        /// <param name="name">Nombre del cliente a buscar</param>
        /// <returns>Lista de clientes que coinciden con el nombre</returns>
        Task<List<GetClientDto>> GetByNameAsync(string name);

        /// <summary>
        /// Verifica si existe un cliente con el nombre especificado
        /// </summary>
        /// <param name="name">Nombre a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        Task<bool> ExistsByNameAsync(string name);
    }
}
