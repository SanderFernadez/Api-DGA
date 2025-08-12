using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using AutoMapper;

namespace Api_DGA.Application.Services
{
    /// <summary>
    /// Servicio genérico que maneja DTOs para operaciones CRUD
    /// </summary>
    /// <typeparam name="TCreateDto">DTO para crear entidades</typeparam>
    /// <typeparam name="TUpdateDto">DTO para actualizar entidades</typeparam>
    /// <typeparam name="TGetDto">DTO para obtener entidades</typeparam>
    /// <typeparam name="TEntity">Tipo de entidad</typeparam>
    public class GenericService<TCreateDto, TUpdateDto, TGetDto, TEntity> : IGenericService<TCreateDto, TUpdateDto, TGetDto, TEntity>
        where TCreateDto : class
        where TUpdateDto : class
        where TGetDto : class
        where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>DTO de la entidad encontrada</returns>
        public virtual async Task<TGetDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TGetDto>(entity);
        }

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns>Lista de DTOs de entidades</returns>
        public virtual async Task<List<TGetDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllListAsync();
            return _mapper.Map<List<TGetDto>>(entities);
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="createDto">DTO con los datos para crear</param>
        /// <returns>DTO de la entidad creada</returns>
        public virtual async Task<TGetDto> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<TGetDto>(createdEntity);
        }

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <param name="updateDto">DTO con los datos para actualizar</param>
        /// <returns>DTO de la entidad actualizada</returns>
        public virtual async Task<TGetDto> UpdateAsync(int id, TUpdateDto updateDto)
        {
            // Verificar que la entidad existe
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new ArgumentException($"Entity with id {id} not found");

            // Mapear el DTO a la entidad existente
            _mapper.Map(updateDto, existingEntity);
            
            // Actualizar en el repositorio
            await _repository.UpdateAsync(existingEntity, id);
            
            // Obtener la entidad actualizada
            var updatedEntity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TGetDto>(updatedEntity);
        }

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>True si se eliminó correctamente</returns>
        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return false;

                await _repository.DeleteAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Crea múltiples entidades
        /// </summary>
        /// <param name="createDtos">Lista de DTOs para crear</param>
        /// <returns>Lista de DTOs de entidades creadas</returns>
        public virtual async Task<List<TGetDto>> CreateRangeAsync(List<TCreateDto> createDtos)
        {
            var entities = _mapper.Map<List<TEntity>>(createDtos);
            var createdEntities = await _repository.AddRangeAsync(entities);
            return _mapper.Map<List<TGetDto>>(createdEntities);
        }

        /// <summary>
        /// Verifica si existe una entidad
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>True si existe</returns>
        public virtual async Task<bool> ExistsAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null;
        }

        /// <summary>
        /// Obtiene el total de entidades
        /// </summary>
        /// <returns>Total de entidades</returns>
        public virtual async Task<int> GetCountAsync()
        {
            var entities = await _repository.GetAllListAsync();
            return entities.Count;
        }
    }
}
