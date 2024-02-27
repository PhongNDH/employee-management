using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Helper;
using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.Models.Interface.Repository
{
    public interface IGenericRepository<T> where T : GenericEntity
    {
        Task<List<T>> GetEntityList();
        
        Task<T?> GetEntityById(int? id);
        
        Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec);
        
        // Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec, int page, int size);
        
        Task<T?> GetEntityByIdWithSpecification(ISpecification<T> spec);
        
        Task CreateEntity(T entity);
        
        Task UpdateEntity(T entity);
        
        Task DeleteEntity(T entity);
        
        Task DeleteAllEntity();
        
        Task InitialiseEntity(IEnumerable<T> entities);
        
    }
}