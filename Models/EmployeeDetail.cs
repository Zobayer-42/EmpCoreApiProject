namespace EmpCoreApiProject.Models
{
    public class EmployeeDetail
    {
        public Guid EmployeeDetailId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Employee? Employee { get; set; }

    }
}
