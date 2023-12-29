using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Utils;
using FluentValidation;

namespace EmployeeManagement.DataAccess.Validation;

public class CommuneValidator : GenericValidator<Commune>
{
    private readonly IGenericRepository<Commune> _communeRepository;

    public CommuneValidator(IGenericRepository<Commune> communeRepository)
    {
        _communeRepository = communeRepository;

        RuleFor(p => p.Name).Must(Inspect.IsValidNameWithDiacritics).WithMessage("Name must contain only letters");
        RuleFor(p => p.Name)
            .Must((commune, name) =>
                !Inspect.IsDuplicatedName<Commune>(name, commune.Id, _communeRepository.GetEntityList().Result))
            .WithMessage("Name is already exists");
    }
}