using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.DataAccess.Specification;

public class DistrictDetailSpecification : Specification<District>
{
    public DistrictDetailSpecification()
    {
        AddInclude(d => d.Province!);
    }

    public DistrictDetailSpecification(int? id) : base(d => d.Id == id)
    {
        AddInclude(d => d.Province!);
    }
}