

namespace Api_DGA.Application.Interfaces.Services
{
    /// <summary>
    /// Interfaz genérica para servicios que manejan DTOs
    /// </summary>
    /// <typeparam name="TCreateDto">DTO para crear entidades</typeparam>
    /// <typeparam name="TUpdateDto">DTO para actualizar entidades</typeparam>
    /// <typeparam name="TGetDto">DTO para obtener entidades</typeparam>
    /// <typeparam name="TEntity">Tipo de entidad</typeparam>
    public interface IGenericService<TCreateDto, TUpdateDto, TGetDto, TEntity>
        where TCreateDto : class
        where TUpdateDto : class
        where TGetDto : class
        where TEntity : class
    {
        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>DTO de la entidad encontrada</returns>
        Task<TGetDto?> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns>Lista de DTOs de entidades</returns>
        Task<List<TGetDto>> GetAllAsync();

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="createDto">DTO con los datos para crear</param>
        /// <returns>DTO de la entidad creada</returns>
        Task<TGetDto> CreateAsync(TCreateDto createDto);

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <param name="updateDto">DTO con los datos para actualizar</param>
        /// <returns>DTO de la entidad actualizada</returns>
        Task<TGetDto> UpdateAsync(int id, TUpdateDto updateDto);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>True si se eliminó correctamente</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Crea múltiples entidades
        /// </summary>
        /// <param name="createDtos">Lista de DTOs para crear</param>
        /// <returns>Lista de DTOs de entidades creadas</returns>
        Task<List<TGetDto>> CreateRangeAsync(List<TCreateDto> createDtos);

        /// <summary>
        /// Verifica si existe una entidad
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>True si existe</returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Obtiene el total de entidades
        /// </summary>
        /// <returns>Total de entidades</returns>
        Task<int> GetCountAsync();
    }
}
