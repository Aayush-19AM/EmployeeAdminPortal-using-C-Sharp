using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeesById(Guid id)
        {
            var employeeId = dbContext.Employees.Find(id);
            if (employeeId == null)
            {
                return NotFound("Id not found");
            }
            return Ok(employeeId);
        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {

            var EmployeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,

            };

            dbContext.Employees.Add(EmployeeEntity);
            dbContext.SaveChanges();

            return Ok(EmployeeEntity);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployees(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id, DeleteEmployeeDto deleteEmployeeDto) { 

            var employee=dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPatch]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployeeField(Guid id, PatchEmployeeDto patchEmployeeDto)
        {
            var employee= dbContext.Employees.Find(id);
            if (employee==null)
            {
                return NotFound("Already a null field");
            }
            employee.Name=patchEmployeeDto.Name;
            employee.Email = patchEmployeeDto.Email;
            employee.Phone=patchEmployeeDto.Phone;
            employee.Salary = patchEmployeeDto.Salary;

            dbContext.SaveChanges();

            return Ok(employee);
        }
    }
}
