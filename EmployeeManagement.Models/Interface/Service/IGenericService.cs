using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.Models.Interface.Service;


public interface IGenericService<T> where T : GenericEntity
{
    Task<List<T>> GetEntityListAsync();
    Task<List<T>> GetEntityListAsync(int page, int size);
    Task<T?> GetEntityByIdAsync(int? id);
    Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec);
    Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec, int page, int size);
    Task<T?> GetEntityByIdWithSpecification(ISpecification<T> spec);
    Task<bool> CreateEntityAsync(T entity);
    Task<bool> UpdateEntityAsync(T entity);
    Task DeleteEntityAsync(T entity);
    Task DeleteAllEntityAsync();
    Task InitialiseEntityAsync(IEnumerable<T> entities);
    
}