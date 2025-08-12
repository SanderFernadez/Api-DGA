
using Api_DGA.Application.Interfaces.Repositories;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly InfrastructureContext _dbContext;

        public GenericRepository(InfrastructureContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<Entity>> AddRangeAsync(List<Entity> entities)
        {
            await _dbContext.Set<Entity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            var existingEntity = await _dbContext.Set<Entity>().FindAsync(id);
            if (existingEntity == null)
                throw new ArgumentException($"Entity with id {id} not found");

            // Obtener las propiedades de la entidad
            var entityType = typeof(Entity);
            var properties = entityType.GetProperties();

            // Copiar valores de las propiedades, excluyendo la clave primaria
            foreach (var property in properties)
            {
                // Excluir propiedades que son claves primarias o navegación
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    (property.PropertyType.IsClass && property.PropertyType != typeof(string)))
                    continue;

                var value = property.GetValue(entity);
                if (value != null)
                {
                    property.SetValue(existingEntity, value);
                }
            }

            _dbContext.Entry(existingEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllListAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();//Deferred execution
        }

        public virtual IQueryable<Entity> GetAllQuery()
        {
            return _dbContext.Set<Entity>().AsQueryable();
        }

        public virtual async Task<List<Entity>> GetAllListWithIncludeAsync(List<string> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual IQueryable<Entity> GetAllQueryWithInclude(List<string> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return query.AsQueryable();
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }
    }
}
