using EmployeeManagement.Models;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Models.Interface.Specification;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.DataAccess.Service;

public class GenericService<T> : IGenericService<T> where T : GenericEntity
{
    private readonly IGenericRepository<T> _genericRepository;
    private readonly IValidator<T> _genericValidator;

    public GenericService(IGenericRepository<T> genericRepository, IValidator<T> genericValidator)
    {
        _genericRepository = genericRepository;
        _genericValidator = genericValidator;
    }

    public async Task<List<T>> GetEntityListAsync()
    {
        return await _genericRepository.GetEntityList();
    }

    public async Task<List<T>> GetEntityListAsync(int page, int size)
    {
        List<T> districtList = await _genericRepository.GetEntityList();
        return districtList
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
    }

    public async Task<T?> GetEntityByIdAsync(int? id)
    {
        return await _genericRepository.GetEntityById(id);
    }

    public async Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec)
    {
        return await _genericRepository.GetEntityListWithSpecification(spec);
    }

    public async Task<List<T>> GetEntityListWithSpecification(ISpecification<T> spec, int page, int size)
    {
        return await _genericRepository.GetEntityListWithSpecification(spec, page, size);
    }

    public async Task<T?> GetEntityByIdWithSpecification(ISpecification<T> spec)
    {
        return await _genericRepository.GetEntityByIdWithSpecification(spec);
    }

    [ValidateAntiForgeryToken]
    public async Task<bool> CreateEntityAsync(T entity)
    {
        ValidationResult result = await _genericValidator.ValidateAsync(entity);
        if (!result.IsValid) return false;
        await _genericRepository.CreateEntity(entity);
        return true;
    }

    [ValidateAntiForgeryToken]
    public async Task<bool> UpdateEntityAsync(T entity)
    {
        ValidationResult result = await _genericValidator.ValidateAsync(entity);
        if (!result.IsValid) return false;
        await _genericRepository.UpdateEntity(entity);
        return true;
    }

    public async Task DeleteEntityAsync(T entity)
    {
        await _genericRepository.DeleteEntity(entity);
    }

    public async Task DeleteAllEntityAsync()
    {
        await _genericRepository.DeleteAllEntity();
    }

    public async Task InitialiseEntityAsync(IEnumerable<T> entities)
    {
        await _genericRepository.InitialiseEntity(entities);
    }
}