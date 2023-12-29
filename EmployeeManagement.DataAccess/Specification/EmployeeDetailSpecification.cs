using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.DataAccess.Specification;

public class EmployeeDetailSpecification : Specification<Employee>
{
    public EmployeeDetailSpecification()
    {
        AddInclude(x => x.EthnicGroup!);
        AddInclude(x => x.Occupation!);
        AddInclude(x => x.Commune!);
        AddInclude("Commune.District");
        AddInclude("Commune.District.Province");
    }
    
    public EmployeeDetailSpecification(int? id) : base(x => x.Id == id)
    {
        AddInclude(x => x.EthnicGroup!);
        AddInclude(x => x.Occupation!);
        AddInclude(x => x.Commune!);
        AddInclude("Commune.District");
        AddInclude("Commune.District.Province");
    }
}