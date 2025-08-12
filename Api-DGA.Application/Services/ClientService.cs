using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Application.Dtos;
using Api_DGA.Core.Entities;
using AutoMapper;
using Api_DGA.Application.Dtos.Client;

namespace Api_DGA.Application.Services
{
    /// <summary>
    /// Servicio específico para la entidad Client
    /// </summary>
    public class ClientService : GenericService<CreateClientDto, UpdateClientDto, GetClientDto, Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper) : base(clientRepository, mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene un cliente por su email
        /// </summary>
        /// <param name="email">Email del cliente</param>
        /// <returns>Cliente encontrado o null</returns>
        public async Task<GetClientDto?> GetByEmailAsync(string email)
        {
            var client = await _clientRepository.GetByEmailAsync(email);
            return _mapper.Map<GetClientDto>(client);
        }

        /// <summary>
        /// Obtiene un cliente por su teléfono
        /// </summary>
        /// <param name="phone">Teléfono del cliente</param>
        /// <returns>Cliente encontrado o null</returns>
        public async Task<GetClientDto?> GetByPhoneAsync(string phone)
        {
            var client = await _clientRepository.GetByPhoneAsync(phone);
            return _mapper.Map<GetClientDto>(client);
        }

        /// <summary>
        /// Verifica si existe un cliente con el email especificado
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _clientRepository.ExistsByEmailAsync(email);
        }

        /// <summary>
        /// Obtiene clientes por nombre (búsqueda parcial)
        /// </summary>
        /// <param name="name">Nombre del cliente a buscar</param>
        /// <returns>Lista de clientes que coinciden con el nombre</returns>
        public async Task<List<GetClientDto>> GetByNameAsync(string name)
        {
            var clients = await _clientRepository.GetByNameAsync(name);
            return _mapper.Map<List<GetClientDto>>(clients);
        }

        /// <summary>
        /// Verifica si existe un cliente con el nombre especificado
        /// </summary>
        /// <param name="name">Nombre a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _clientRepository.ExistsByNameAsync(name);
        }
    }
}
