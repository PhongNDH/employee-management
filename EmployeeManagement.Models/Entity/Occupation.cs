using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Occupation : GenericEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}