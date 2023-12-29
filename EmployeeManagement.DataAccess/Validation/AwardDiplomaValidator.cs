using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Utils;
using FluentValidation;
using Constant = EmployeeManagement.Utils.Constant.Constant;

namespace EmployeeManagement.DataAccess.Validation;

public class AwardDiplomaValidator : GenericValidator<AwardDiploma>
{
    private readonly IGenericRepository<AwardDiploma> _awardDiplomaRepository;

    public AwardDiplomaValidator(IGenericRepository<AwardDiploma> awardDiplomaRepository)
    {
        _awardDiplomaRepository = awardDiplomaRepository;
        RuleFor(a => a.Duration).LessThan(Constant.MaxDuration)
            .WithMessage("Duration must be from 1 to " + Constant.MaxDuration)
            .GreaterThan(0).WithMessage("Duration must be from 1 to " + Constant.MaxDuration);
        RuleFor(a => a.EmployeeId)
            .Must((awardDiploma, id) =>
                Inspect.IsValidNumberOfDiplomaOfEachEmployee(_awardDiplomaRepository.GetEntityList().Result, id,
                    awardDiploma.Id))
            .WithMessage("Number of diplomas of each employee must not exceed " +
                         Constant.MaxNumberOfDiplomasOfEachEmployee);
        RuleFor(a => a.DiplomaId)
            .Must((award, id) =>
                !Inspect.IsDuplicatedAwardDiploma(_awardDiplomaRepository.GetEntityList().Result, id, award))
            .WithMessage("This employee already has been granted this diploma");
    }
}