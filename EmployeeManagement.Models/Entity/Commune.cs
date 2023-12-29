using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Entity
{
    public class Commune : GenericEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Level { get; set; }

        [Required]
        [DisplayName("District")]
        public int DistrictId { get; set; }

        public District? District { get; set; }
    }
}
