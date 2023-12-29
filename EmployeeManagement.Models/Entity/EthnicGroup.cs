using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class EthnicGroup : GenericEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}