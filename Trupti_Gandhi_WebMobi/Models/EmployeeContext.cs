using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trupti_Gandhi_WebMobi.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("MyDbContext") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}