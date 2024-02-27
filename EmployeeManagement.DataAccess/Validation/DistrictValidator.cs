using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Utils;
using FluentValidation;

namespace EmployeeManagement.DataAccess.Validation;

public class DistrictValidator : GenericValidator<District>
{
    private readonly IGenericRepository<District> _districtRepository;
    public DistrictValidator(IGenericRepository<District> districtRepository)
    {
        _districtRepository = districtRepository;

        RuleFor(p => p.Name).Must(Inspect.IsValidNameWithDiacritics).WithMessage("Name must contain only letters");
        RuleFor(p => p.Name).Must((district, name) => !Inspect.IsDuplicatedName(name,district.Id, _districtRepository.GetEntityList().Result)).WithMessage("Name is already exists");
    }
}