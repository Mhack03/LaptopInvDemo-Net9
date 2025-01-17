using API.Data;
using API.Models;
using API.Models.Dto;
using API.Models.Dtos;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(ApplicationDbContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            try
            {
                _logger.LogInformation("Fetching all employees.");
                var employee = _context.Employees
                    .Select(e => new EmployeeDto
                    {
                        EmployeeId = e.EmployeeId,
                        Name = e.Name,
                        Email = e.Email,
                        Department = e.Department,
                        Position = e.Position
                    })
                    .ToList();
                _logger.LogInformation("Fetched {count} employees.", employee.Count);
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching employees.");
                throw;
            }
        }

        [HttpGet("GetEmployeeById/{id:int}", Name = "GetSingleEmployee")]
        public ActionResult<EmployeeDto> GetSingleEmployee(int id)
        {
            try
            {
                _logger.LogInformation("Fetching employee with ID {id}.", id);
                var employee = _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department,
                    Position = e.Position
                })
                .FirstOrDefault();

                if (employee == null)
                {
                    _logger.LogWarning("Employee with ID {id} not found.", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched employee with ID {id}.", id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching employee with ID {id}.", id);
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }

        [HttpPost("CreateEmployee")]
        public ActionResult<EmployeeDto> CreateEmployee([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            _logger.LogInformation("Creating employee.");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid employee data provided.");
                return BadRequest(ModelState);
            }

            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Email.ToLower() == employeeCreateDto.Email.ToLower() && e.Name.ToLower() == employeeCreateDto.Name.ToLower());
            if (existingEmployee != null)
            {
                _logger.LogWarning("An employee with the same name and email already exists.");
                return BadRequest();
            }

            var employee = new Employee
            {
                Name = employeeCreateDto.Name,
                Email = employeeCreateDto.Email,
                Department = employeeCreateDto.Department,
                Position = employeeCreateDto.Position,
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
                Department = employee.Department,
                Position = employee.Position
            };
            _logger.LogInformation("Created employee with ID {id}.", employeeDto.EmployeeId);
            return CreatedAtAction(nameof(GetSingleEmployee), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpPut("UpdateEmployee/{id:int}", Name = "UpdateEmployee")]
        public ActionResult<EmployeeDto> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            try
            {
                _logger.LogInformation("Updating employee with ID {id}.", id);
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid employee data provided.");
                    return BadRequest(ModelState);
                }

                var employee = _context.Employees.Find(id);
                if (employee == null)
                {
                    _logger.LogWarning("Employee with ID {id} not found.", id);
                    return NotFound();
                }

                var existingEmployeeWithName = _context.Employees
                .FirstOrDefault(e => e.Name.ToLower() == employeeUpdateDto.Name.ToLower() && e.EmployeeId != id);
                if (existingEmployeeWithName != null)
                {

                    return Conflict(new { message = "An employee with the same name already exists." });
                }

                var existingEmployeeWithEmail = _context.Employees
                .FirstOrDefault(e => e.Email.ToLower() == employeeUpdateDto.Email.ToLower() && e.EmployeeId != id);
                if (existingEmployeeWithEmail != null)
                {
                    return Conflict(new { message = "An employee with the same email already exists." });
                }

                employee.Name = employeeUpdateDto.Name;
                employee.Email = employeeUpdateDto.Email;
                employee.Department = employeeUpdateDto.Department;
                employee.Position = employeeUpdateDto.Position;
                employee.UpdatedDate = DateTime.Now;

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee with ID {id}.", id);
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }

        [HttpDelete("Delete{id:int}", Name = "DeleteEmployee")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                _logger.LogInformation("Deleting employee with ID {id}.", id);
                var employee = _context.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound();
                }
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                _logger.LogInformation("Deleted employee with ID {id}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employee with ID {id}.", id);
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }
    }
}