using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Utils;
using FluentValidation;

namespace EmployeeManagement.DataAccess.Validation;

public class DiplomaValidator : GenericValidator<Diploma>
{
    private readonly IGenericRepository<Diploma> _diplomaRepository;

    public DiplomaValidator(IGenericRepository<Diploma> diplomaRepository)
    {
        _diplomaRepository = diplomaRepository;

        RuleFor(p => p.Name).Must(Inspect.IsValidDiplomaNameWithDiacritics).WithMessage("Name must contain only letters and other special character such as : & ( ) ,");
        RuleFor(p => p.Name)
            .Must((diploma, name) =>
                !Inspect.IsDuplicatedName<Diploma>(name, diploma.Id, _diplomaRepository.GetEntityList().Result))
            .WithMessage("Name is already exists");
    }
}