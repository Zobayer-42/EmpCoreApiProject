using EmpCoreApiProject.Data;
using EmpCoreApiProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpCoreApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _dbContext;
        public EmployeeController(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var allEmployee = _dbContext.Employees.ToList();
            return Ok(allEmployee);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            var employeeEntry = new Employee
            {
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary
            };
            _dbContext.Employees.Add(employeeEntry);
            _dbContext.SaveChanges();
            return Ok(employeeEntry);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _dbContext.Employees.Find(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.Salary = employee.Salary;
            _dbContext.SaveChanges();
            return Ok(existingEmployee);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var existingEmployee = _dbContext.Employees.Find(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(existingEmployee);
            _dbContext.SaveChanges();
            return Ok(existingEmployee);
        }
    }
}