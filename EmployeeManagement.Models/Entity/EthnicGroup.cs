using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Entity
{
    public class EthnicGroup : GenericEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}