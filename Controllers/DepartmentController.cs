using EmpCoreApiProject.Data;
using EmpCoreApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpCoreApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeDbContext _dbContext;
        public DepartmentController(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var allDepartments = _dbContext.Departments
                .Include(d => d.EmployeeDetails) 
                .ThenInclude(ed => ed.Employee)
                .ToList();
            return Ok(allDepartments);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetDepartmentById(Guid id)
        {
            var department = _dbContext.Departments
               .Include(d => d.EmployeeDetails)
                .ThenInclude(ed => ed.Employee)
               .FirstOrDefault(d => d.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            var departmentEntry = new Department
            {
                Name = department.Name,
                Description = department.Description
            };
            _dbContext.Departments.Add(departmentEntry);
            _dbContext.SaveChanges();
           if(department.EmployeeDetails != null)
           {
                foreach(var empDetail in department.EmployeeDetails)
                {
                    var existEmployee = _dbContext.Employees.Find(empDetail.EmployeeId);
                    if (existEmployee != null)
                    {
                        EmployeeDetail employeeDetail = new EmployeeDetail
                        {
                            EmployeeId = existEmployee.EmployeeId,
                            DepartmentId = departmentEntry.DepartmentId
                        };
                        _dbContext.EmployeeDetails.Add(employeeDetail);
                    }
                }_dbContext.SaveChanges();
            }
            return Ok(departmentEntry);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateDepartment(Guid id, Department department)
        {
            var existingDepartment = _dbContext.Departments
                .Include(d => d.EmployeeDetails)
                .FirstOrDefault(d => d.DepartmentId == id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            existingDepartment.Name = department.Name;
            existingDepartment.Description = department.Description;
            _dbContext.EmployeeDetails.RemoveRange(existingDepartment.EmployeeDetails);
            if (department.EmployeeDetails != null)
            {
                foreach (var empDetail in department.EmployeeDetails)
                {
                    var existEmployee = _dbContext.Employees.Find(empDetail.EmployeeId);
                    if (existEmployee != null)
                    {
                        EmployeeDetail employeeDetail = new EmployeeDetail
                        {
                            EmployeeId = existEmployee.EmployeeId,
                            DepartmentId = existingDepartment.DepartmentId
                        };
                        _dbContext.EmployeeDetails.Add(employeeDetail);
                    }
                }
                _dbContext.SaveChanges();
            }
            return Ok(existingDepartment);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteDepartment(Guid id)
        {
            var existingDepartment = _dbContext.Departments
                .Include(d => d.EmployeeDetails)
                .FirstOrDefault(d => d.DepartmentId == id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            _dbContext.EmployeeDetails.RemoveRange(existingDepartment.EmployeeDetails);
            _dbContext.Departments.Remove(existingDepartment);
            _dbContext.SaveChanges();
            return Ok(existingDepartment);
        }
    }
}
