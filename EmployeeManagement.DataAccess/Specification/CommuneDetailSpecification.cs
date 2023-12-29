using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Specification;

namespace EmployeeManagement.DataAccess.Specification;

public class CommuneDetailSpecification : Specification<Commune>
{
    public CommuneDetailSpecification()
    {
        AddInclude(c => c.District!);
        AddInclude("District.Province");
    }

    public CommuneDetailSpecification(int? id) : base(d => d.Id == id)
    {
        AddInclude(d => d.District!);
        AddInclude("District.Province");
    }
}