using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Utils;
using FluentValidation;

namespace EmployeeManagement.DataAccess.Validation;

public class ProvinceValidator : GenericValidator<Province>
{
    private readonly IGenericRepository<Province> _provinceRepository;

    public ProvinceValidator(IGenericRepository<Province> provinceRepository)
    {
        _provinceRepository = provinceRepository;

        RuleFor(p => p.Name).Must(Inspect.IsValidNameWithDiacritics).WithMessage("Name must contain only letters");
        RuleFor(p => p.Name)
            .Must((province, name) =>
                !Inspect.IsDuplicatedName<Province>(name, province.Id, _provinceRepository.GetEntityList().Result))
            .WithMessage("Name is already exists");
    }
}