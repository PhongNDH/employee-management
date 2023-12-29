using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.DataAccess.SeedData
{
    public class SeedEmployee
    {
        public List<Employee> Employees { get; set; }
        public SeedEmployee()
        {
            Employees = new List<Employee>()
            {
                new()
                {
                    Id = 1,
                    Name = "David",
                    Dob = new DateTime(2001, 3, 4),
                    PhoneNumber = "0987678767",
                    CitizenIdentityCard = "345678902345",
                    EthnicGroupId = 5,
                    OccupationId = 1,
                    CommuneId = 6
                },
                new()
                {
                    Id = 2,
                    Name = "Linda",
                    Dob = new DateTime(2002, 9, 27),
                    EthnicGroupId = 2,
                    OccupationId = 3,
                    CommuneId = 7
                },
                new()
                {
                    Id = 3,
                    Name = "Kevin",
                    Dob = new DateTime(2006, 7, 10),
                    EthnicGroupId = 7,
                    OccupationId = 2,
                    PhoneNumber = "0321345676",
                    CommuneId = 124
                },
                new()
                {
                    Id = 4,
                    Name = "Jessy",
                    Dob = new DateTime(2005, 7, 10),
                    EthnicGroupId = 6,
                    OccupationId = 9,
                    CitizenIdentityCard = "456784567834",
                    CommuneId = 31490
                }
            };
        }
    }
}
