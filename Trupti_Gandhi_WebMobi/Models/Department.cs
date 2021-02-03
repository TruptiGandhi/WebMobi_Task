using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trupti_Gandhi_WebMobi.Models
{
    [Table("Department")]
    public class Department
    { 
       [Key]
       public int DeptId { get; set; }

        [Required(ErrorMessage = "Dept Name is required")]
        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

