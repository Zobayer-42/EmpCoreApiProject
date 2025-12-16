using EmpCoreApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpCoreApiProject.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    }
}
