using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trupti_Gandhi_WebMobi.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "FirstName")]
        [StringLength(300, ErrorMessage = "Can't be more than 300 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "LastName")]
        [StringLength(300, ErrorMessage = "Can't be more than 300 characters")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "DOB is required!")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public virtual Department Departments { get; set; }
        
    }
}