namespace EmpCoreApiProject.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<EmployeeDetail>? EmployeeDetails { get; set; }
    } 
}


