using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DataAccess.Specification;
using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : GenericEntity
    {
        private readonly DatabaseContext _dbContext;

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> GetEntityById(int? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetEntityList()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec, int page, int size)
        {
            return await ApplySpecification(spec)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<T?> GetEntityByIdWithSpecification(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task CreateEntity(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEntity(T entity)
        {
            //Detach entity
            var entityAttach = await _dbContext.Set<T>().FindAsync(entity.Id);
            if (entityAttach != null)
            {
                _dbContext.Entry(entityAttach).State = EntityState.Detached;
            }

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntity(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllEntity()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task InitialiseEntity(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}