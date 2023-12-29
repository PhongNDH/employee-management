using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Utils.Constant;

namespace EmployeeManagement.Models.Entity
{
    public class Employee : GenericEntity
    {
        
        [Required]
        [MaxLength(Constant.MaxLengthOfName)]
        public string? Name { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime Dob { get; set; }

        [Required]
        [DisplayName("Ethnic Group")]
        public int EthnicGroupId { get; set; }
        
        public EthnicGroup? EthnicGroup { get; set; }

        [Required]
        [DisplayName("Occupation")]
        public int OccupationId { get; set; }

        public Occupation? Occupation { get; set; }

        // [Required]
        [DisplayName("Commune")]
        public int? CommuneId { get; set; } 

        public Commune? Commune { get; set; }   

        // [StringLength(Constant.LengthOfCitizenIdentityCardNumber, MinimumLength = Constant.LengthOfCitizenIdentityCardNumber, ErrorMessage = "Citizen Identity Card Number  number must has exactly 12 digits")]
        [DisplayName("Citizen Identity Card Number")]
        public string? CitizenIdentityCard { get; set; }

        // [StringLength(Constant.LengthOfPhoneNumber, MinimumLength = Constant.LengthOfPhoneNumber, ErrorMessage = "Phone Number must has exactly 10 digits")]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }
        
    }
}
