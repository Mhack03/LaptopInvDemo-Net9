using System;
using API.Data;
using API.Models.Dto;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/Assignments")]
[ApiController]
public class AssignmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AssignmentsController> _logger;
    public AssignmentsController(ApplicationDbContext context, ILogger<AssignmentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //Retrieve all assigned laptops.
    [HttpGet("GetAllAssignedLaptops")]
    public ActionResult<IEnumerable<AssignmentDto>> GetAllAssignments()
    {
        try
        {
            _logger.LogInformation("Fetching all assigned laptops.");
            var assignments = _context.Assignments
                .Include(a => a.Employee) // Include the Employee entity 
                .ToList() // Fetch the data into memory 
                .Select(e => new AssignmentDto
                {
                    AssignmentId = e.AssignmentId,
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.Employee?.Name ?? "Unknown", // Handle null Employee 
                    LaptopSerialNumber = e.LaptopSerialNumber,
                    AssignedDate = e.AssignedDate,
                    ReturnDate = e.ReturnDate
                })
                .ToList();

            _logger.LogInformation("Fetched {count} assigned laptops.", assignments.Count);

            return Ok(assignments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching assigned laptops.");
            return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
        }

    }
    //Retrieve a specific assigned laptop.
    [HttpGet("GetAssignedLaptopById/{id:int}", Name = "GetAssignedLaptopById")]
    public ActionResult<AssignmentDto> GetSingleAssignment(int id)
    {
        try
        {
            _logger.LogInformation("Fetching assigned laptop with ID {id}.", id);
            var assignment = _context.Assignments
                .Include(a => a.Employee) // Include the Employee entity 
                .FirstOrDefault(e => e.AssignmentId == id); // Fetch the data into memory 
            if (assignment == null)
            {
                _logger.LogWarning("Assigned laptop with ID {id} not found.", id);
                return NotFound();
            }
            var assignmentDto = new AssignmentDto
            {
                AssignmentId = assignment.AssignmentId,
                EmployeeId = assignment.EmployeeId,
                EmployeeName = assignment.Employee?.Name ?? "Unknown", // Handle null Employee
                LaptopSerialNumber = assignment.LaptopSerialNumber,
                AssignedDate = assignment.AssignedDate,
                ReturnDate = assignment.ReturnDate
            };

            _logger.LogInformation("Fetched assigned laptop with ID {id}.", id);
            return Ok(assignmentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching assigned laptop with ID {id}.", id);
            return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
        }
    }

    //Retrieve all assigned laptops for a specific employee.
    [HttpGet("ByEmployeeId/{employeeId:int}", Name = "GetAssignLaptopByEmployeeId")]
    public ActionResult<IEnumerable<AssignmentDto>> GetAssignLaptopByEmployeeId(int employeeID)
    {
        try
        {
            _logger.LogInformation("Fetching assigned laptops for employee with ID {employeeID}.", employeeID);
            var assignedLaptops = _context.Assignments
                .Where(a => a.EmployeeId == employeeID)
                .Include(a => a.Employee)
                .ToList()
                .Select(a => new AssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee?.Name ?? "Unknown", // Handle null Employee
                    LaptopSerialNumber = a.LaptopSerialNumber,
                    AssignedDate = a.AssignedDate,
                    ReturnDate = a.ReturnDate
                })
                .ToList();
            _logger.LogInformation("Fetched {count} assigned laptops for employee with ID {employeeID}.", assignedLaptops.Count, employeeID);
            return Ok(assignedLaptops);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching assigned laptops for employee with ID {employeeID}.", employeeID);
            return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
        }
    }
    //Retrieve all assigned laptops where laptops have not yet been returned.
    [HttpGet("Active")]
    public ActionResult<IEnumerable<AssignmentDto>> GetActiveAssignments()
    {
        try
        {
            _logger.LogInformation("Fetching all active assignments where laptops have not yet been returned.");
            var activeAssignments = _context.Assignments
                .Where(a => a.ReturnDate == null || a.ReturnDate > DateTime.Now)
                .Include(a => a.Employee)
                .ToList()
                .Select(a => new AssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee?.Name ?? "Unknown", // Handle null Employee
                    LaptopSerialNumber = a.LaptopSerialNumber,
                    AssignedDate = a.AssignedDate,
                    ReturnDate = a.ReturnDate
                })
                .ToList();

            _logger.LogInformation("Fetched {count} active assignments.", activeAssignments.Count);
            return Ok(activeAssignments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active assignments.");
            return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
        }
    }
    //Retrieve all assigned laptop for a specific laptop.
    [HttpGet("ByLaptopSerialNumber/{serialNumber}", Name = "GetAssignLaptopByLaptopSerialNumber")]
    public ActionResult<IEnumerable<AssignmentDto>> GetAssignLaptopByLaptopSerialNumber(string serialNumber)
    {
        try
        {
            _logger.LogInformation("Fetching assigned laptops with serial number {serialNumber}.", serialNumber);
            var assignedLaptops = _context.Assignments
                .Where(a => a.LaptopSerialNumber == serialNumber)
                .Include(a => a.Employee)
                .ToList()
                .Select(a => new AssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee?.Name ?? "Unknown", // Handle null Employee
                    LaptopSerialNumber = a.LaptopSerialNumber,
                    AssignedDate = a.AssignedDate,
                    ReturnDate = a.ReturnDate
                })
                .ToList();
            _logger.LogInformation("Fetched {count} assigned laptops with serial number {serialNumber}.", assignedLaptops.Count, serialNumber);
            return Ok(assignedLaptops);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching assigned laptops with serial number {serialNumber}.", serialNumber);
            return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
        }
    }
    //Retrieve all assigned laptops within a specific date range.
    [HttpGet("ByDateRange")]
    public ActionResult<IEnumerable<AssignmentDto>> GetAssignedLaptopsByDateRange(DateTime startDate, DateTime endDate)
    {
        try
        {
            _logger.LogInformation("Fetching assigned laptops within date range from {startDate} to {endDate}.", startDate, endDate);
            var assignedLaptops = _context.Assignments
                .Where(a => a.AssignedDate >= startDate && a.AssignedDate <= endDate)
                .Include(a => a.Employee)
                .ToList()
                .Select(a => new AssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee?.Name ?? "Unknown", // Handle null Employee
                    LaptopSerialNumber = a.LaptopSerialNumber,
                    AssignedDate = a.AssignedDate,
                    ReturnDate = a.ReturnDate
                })
                .ToList();

            _logger.LogInformation("Fetched {count} assigned laptops within date range from {startDate} to {endDate}.", assignedLaptops.Count, startDate, endDate);
            return Ok(assignedLaptops);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching assigned laptops within date range from {startDate} to {endDate}.", startDate, endDate);
            return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
        }
    }
}
