using API.Data;
using API.Models;
using API.Models.Dto;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employee = _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department
                })
                .ToList();
            return employee;
        }

        [HttpGet("{id:int}", Name = "GetSingleEmployee")]
        public ActionResult<EmployeeDto> GetSingleEmployee(int id)
        {
            var employee = _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department
                })
                .FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<EmployeeDto> CreateEmployee([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Email.ToLower() == employeeCreateDto.Email.ToLower() && e.Name.ToLower() == employeeCreateDto.Name.ToLower());
            if(existingEmployee != null)   
            {
                return BadRequest();
            }

            var employee = new Employee
            {
                Name = employeeCreateDto.Name,
                Email = employeeCreateDto.Email,
                Department = employeeCreateDto.Department,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department
            };

            return CreatedAtAction(nameof(GetSingleEmployee), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        public ActionResult<EmployeeDto> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            var existingEmployeeWithName = _context.Employees
            .FirstOrDefault(e => e.Name.ToLower() == employeeUpdateDto.Name.ToLower() && e.EmployeeId != id);
            if (existingEmployeeWithName != null)
            {
                return Conflict(new { message = "An employee with the same name already exists."});
            }

            var existingEmployeeWithEmail = _context.Employees
            .FirstOrDefault(e => e.Email.ToLower() == employeeUpdateDto.Email.ToLower() && e.EmployeeId != id);
            if (existingEmployeeWithEmail != null)
            {
                return Conflict(new { message = "An employee with the same email already exists."});
            }

            employee.Name = employeeUpdateDto.Name;
            employee.Email = employeeUpdateDto.Email;
            employee.Department = employeeUpdateDto.Department;
            employee.UpdatedDate = DateTime.Now;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        public ActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return NoContent();
        }
    }
}