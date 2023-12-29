using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Province : GenericEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Level { get; set; }
    }
}
