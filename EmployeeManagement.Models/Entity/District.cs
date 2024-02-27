
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Entity
{
    public class District : GenericEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Level { get; set; }

        [Required]
        [DisplayName("Province")]
        public int ProvinceId { get; set; }

        public Province? Province { get; set; }
    }
}
