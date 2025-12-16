namespace EmpCoreApiProject.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Salary { get; set; }
    }
}
