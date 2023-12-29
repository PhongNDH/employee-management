using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.DataAccess.Specification;

public class AwardDiplomaDetailSpecification : Specification<AwardDiploma>
{
    public AwardDiplomaDetailSpecification()
    {
        AddInclude(a => a.Employee!);
        AddInclude(a => a.Diploma!);
        AddInclude(a => a.DiplomaGrantingUnit!);
    }

    public AwardDiplomaDetailSpecification(int? id) : base(d => d.Id == id)
    {
        AddInclude(a => a.Employee!);
        AddInclude(a => a.Diploma!);
        AddInclude(a => a.DiplomaGrantingUnit!);
    }
    
    public AwardDiplomaDetailSpecification(string employeeId) : base(d => d.EmployeeId.ToString() == employeeId)
    {
        AddInclude(a => a.Employee!);
        AddInclude(a => a.Diploma!);
        AddInclude(a => a.DiplomaGrantingUnit!);
    }
}