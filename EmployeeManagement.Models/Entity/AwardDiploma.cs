using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Entity
{
    public class AwardDiploma : GenericEntity
    {
        [Required]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        [Required]
        [DisplayName("Diploma")]
        public int DiplomaId { get; set; }

        public Diploma? Diploma { get; set;}

        [Required]
        [DisplayName("Diploma Granting Unit")]
        public int DiplomaGrantingUnitId { get; set; }

        public Province? DiplomaGrantingUnit { get; set; }

        [Required]
        [DisplayName("Granting Date")]
        public DateTime GrantingDate { get; set; }

        [Required]
        public int Duration { get; set; }

    }
}
