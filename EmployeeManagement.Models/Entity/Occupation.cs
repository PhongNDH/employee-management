using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Entity
{
    public class Occupation : GenericEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}