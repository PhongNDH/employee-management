using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Entity
{
    public class Diploma : GenericEntity
    {
        public String? Name { get; set; }
    }
}
