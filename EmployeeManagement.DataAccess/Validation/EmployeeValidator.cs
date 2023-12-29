using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Repository;
using EmployeeManagement.Utils;
using EmployeeManagement.Utils.Constant;
using FluentValidation;

namespace EmployeeManagement.DataAccess.Validation
{
    public class EmployeeValidator : GenericValidator<Employee>
    {
        private readonly IGenericRepository<Employee> _employeeRepository;

        public EmployeeValidator(IGenericRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(Constant.MaxLengthOfName)
                .WithMessage($"Name cannot exceed {Constant.MaxLengthOfName} characters")
                .Must(Inspect.IsValidNameWithDiacritics).WithMessage("Name must contain only letters");

            RuleFor(e => e.Dob).NotNull().WithMessage("Date of birth is required")
                .LessThan(DateTime.Now).WithMessage("Birth date should be in the past")
                .GreaterThan(new DateTime(Constant.MinBirthdateYear, 1, 1, 0, 0,0, DateTimeKind.Utc))
                .WithMessage($"Birth date must be after 1/1/{Constant.MinBirthdateYear}");
            RuleFor(e => e.EthnicGroupId).NotEmpty().WithMessage("Ethic Group is required");
            RuleFor(e => e.OccupationId).NotEmpty().WithMessage("Occupation is required");
            //RuleFor(e => e.CommuneId).NotEmpty().WithMessage("Commune is required");
            RuleFor(e => e.PhoneNumber).MinimumLength(Constant.LengthOfPhoneNumber)
                .WithMessage($"Phone number must have {Constant.LengthOfPhoneNumber} digits")
                .MaximumLength(Constant.LengthOfPhoneNumber)
                .WithMessage($"Phone number must have {Constant.LengthOfPhoneNumber} digits")
                .Must(Inspect.IsContainOnlyNumber).WithMessage("Phone number must contain only digits")
                .Must((employee, phoneNumber) =>
                    !Inspect.IsExistent(employee, _employeeRepository.GetEntityList().Result, "PhoneNumber"))
                .WithMessage("Phone number is already exists");
            RuleFor(e => e.CitizenIdentityCard).MinimumLength(Constant.LengthOfCitizenIdentityCardNumber)
                .WithMessage($"Citizen Identity Number must have {Constant.LengthOfCitizenIdentityCardNumber} digits")
                .MaximumLength(Constant.LengthOfCitizenIdentityCardNumber).WithMessage(
                    $"Citizen Identity Number must have {Constant.LengthOfCitizenIdentityCardNumber} digits")
                .Must(Inspect.IsContainOnlyNumber).WithMessage("Citizen Identity Number must contain only digits")
                .Must((employee, phoneNumber) =>
                    !Inspect.IsExistent(employee, _employeeRepository.GetEntityList().Result, "CitizenNumber"))
                .WithMessage("Citizen Identity Number is already exists");
        }
    }
}