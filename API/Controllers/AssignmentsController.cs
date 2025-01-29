using API.Data;
using API.Models;
using API.Models.Dto;
using API.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/Assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AssignmentsController> _logger;

        public AssignmentsController(ApplicationDbContext context, IMapper mapper, ILogger<AssignmentsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all laptop assignments.
        /// </summary>
        /// <returns>A list of AssignmentDto objects representing laptop assignments.</returns>
        [HttpGet("DeployedHistory")]
        public async Task<IActionResult> GetAllLaptopAssigned()
        {
            // Retrieve all laptop assignments from the database, including laptop and employee details
            var assignmentDtos = await _context.Assignments
                .Include(a => a.Laptop) // Include laptop details
                .Include(a => a.Employee) // Include employee details
                .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider) // Map to AssignmentDto
                .ToListAsync(); // Execute query and convert to list

            // Return the list of assignments, or a 404 if the list is empty
            return assignmentDtos.Count != 0 ? Ok(assignmentDtos) : NotFound();
        }

        /// <summary>
        /// Searches for assignments based on the provided search parameters.
        /// </summary>
        /// <param name="searchById">The ID of the assignment to search for.</param>
        /// <param name="searchByEmployeeId">The ID of the employee to search for.</param>
        /// <param name="searchBySerialNumber">The serial number of the laptop to search for.</param>
        /// <returns>A list of AssignmentDto objects that match the search criteria.</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<AssignmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> SearchAssignments(
            [FromQuery] int? searchById,
            [FromQuery] int? searchByEmployeeId,
            [FromQuery] string? searchBySerialNumber)
        {
            try
            {
                // Initialize the query to retrieve all assignments
                IQueryable<Assignment> query = _context.Assignments
                    .Include(a => a.Employee) // Include employee details
                    .Include(a => a.Laptop); // Include laptop details

                // Apply search filters if provided
                if (searchById.HasValue)
                {
                    // Filter by assignment ID
                    query = query.Where(a => a.AssignmentId == searchById);
                }

                if (searchByEmployeeId.HasValue)
                {
                    // Filter by employee ID
                    query = query.Where(a => a.EmployeeId == searchByEmployeeId);
                }

                if (!string.IsNullOrEmpty(searchBySerialNumber))
                {
                    // Filter by laptop serial number
                    query = query.Where(a => a.LaptopSerialNumber == searchBySerialNumber);
                }

                // Check if any results were found
                if (!await query.AnyAsync())
                {
                    // Return a bad request response if no results were found
                    return BadRequest(new { message = "Please provide at least one valid search parameter." });
                }

                // Retrieve the filtered assignments
                var assignments = await query.ToListAsync();

                // Map the assignments to DTOs
                var assignmentDtos = _mapper.Map<List<AssignmentDto>>(assignments);

                // Return the search results
                return Ok(assignmentDtos);
            }
            catch (Exception ex)
            {
                // Log the error and return an internal server error response
                _logger.LogError(ex, "An error occurred while retrieving assignments: {Message}", ex.InnerException?.Message);
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message}");
            }
        }


        /// <summary>
        /// Retrieves a list of active assignments.
        /// </summary>
        /// <returns>A list of AssignmentDto objects representing active assignments.</returns>
        [HttpGet("Active")]
        public ActionResult<IEnumerable<AssignmentDto>> GetActiveAssignments()
        {
            try
            {
                // Retrieve active assignments from the database
                // An assignment is considered active if its return date is null or in the future
                var activeAssignments = _context.Assignments
                    .Where(a => a.ReturnDate == null || a.ReturnDate > DateTime.Now) // Filter active assignments
                    .Include(a => a.Employee) // Include employee details
                    .Include(a => a.Laptop) // Include laptop details
                    .Select(a => _mapper.Map<AssignmentDto>(a)) // Map assignments to DTOs
                    .ToList(); // Convert to list

                // Return the list of active assignments
                return Ok(activeAssignments);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }

        [HttpPost("CreateAssignment")]
        public ActionResult<AssignmentDto> CreateAssignment([FromBody] AssignmentCreateDto assignmentCreateDto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (assignmentCreateDto == null)
                {
                    return BadRequest(new { message = "The assignmentCreateDto field is required." });
                }

                var employee = _context.Employees
                    .FirstOrDefault(e => e.EmployeeId == assignmentCreateDto.EmployeeId && e.Name == assignmentCreateDto.EmployeeName);

                if (employee == null)
                {
                    return BadRequest("The employee does not exist or the EmployeeId does not match.");
                }

                var laptop = _context.Laptops
                    .FirstOrDefault(l => l.SerialNumber == assignmentCreateDto.LaptopSerialNumber);

                if (laptop == null)
                {
                    return BadRequest("The laptop does not exist.");
                }

                if (_context.Assignments.Any(a => a.LaptopSerialNumber == assignmentCreateDto.LaptopSerialNumber && a.ReturnDate == null))
                {
                    return BadRequest("The laptop is already assigned.");
                }

                var assignment = _mapper.Map<Assignment>(assignmentCreateDto);
                assignment.Department = employee.Department;
                assignment.AssignEmployee(employee, laptop);
                _context.Assignments.Add(assignment);
                _context.SaveChanges();

                transaction.Commit();

                var createdAssignmentDto = _mapper.Map<AssignmentDto>(assignment);
                return CreatedAtAction(nameof(SearchAssignments), new { searchById = assignment.AssignmentId }, createdAssignmentDto);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "An error occurred while saving the entity changes: {Message}", ex.InnerException?.Message);
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Creates a new assignment by deploying a laptop to an employee.
        /// </summary>
        /// <param name="assignmentCreateDto">The assignment creation DTO containing the employee ID, employee name, and laptop serial number.</param>
        /// <returns>A newly created assignment DTO.</returns>
        //[HttpPost("DeployLaptop")]
        //public ActionResult<AssignmentDto> CreateAssignment([FromBody] AssignmentCreateDto assignmentCreateDto)
        //{
        //    // Begin a database transaction to ensure all changes are committed or rolled back as a single unit.
        //    using var transaction = _context.Database.BeginTransaction();

        //    try
        //    {
        //        // Check if the assignment creation DTO is null.
        //        if (assignmentCreateDto == null)
        //        {
        //            // Return a bad request response if the DTO is null.
        //            return BadRequest(new { message = "The assignmentCreateDto field is required." });
        //        }

        //        // Retrieve the employee from the database based on the employee ID and name.
        //        var employee = _context.Employees
        //            .FirstOrDefault(e => e.EmployeeId == assignmentCreateDto.EmployeeId && e.Name == assignmentCreateDto.EmployeeName);

        //        // Check if the employee exists.
        //        if (employee == null)
        //        {
        //            // Return a bad request response if the employee does not exist or the employee ID does not match.
        //            return BadRequest("The employee does not exist or the EmployeeId does not match.");
        //        }

        //        // Retrieve the laptop from the database based on the laptop serial number.
        //        var laptop = _context.Laptops
        //            .FirstOrDefault(l => l.SerialNumber == assignmentCreateDto.LaptopSerialNumber);

        //        // Check if the laptop exists.
        //        if (laptop == null)
        //        {
        //            // Return a bad request response if the laptop does not exist.
        //            return BadRequest("The laptop does not exist.");
        //        }

        //        // Check if the laptop is already assigned to another employee.
        //        if (_context.Assignments.Any(a => a.LaptopSerialNumber == assignmentCreateDto.LaptopSerialNumber && a.ReturnDate == null))
        //        {
        //            // Return a bad request response if the laptop is already assigned.
        //            return BadRequest("The laptop is already assigned.");
        //        }

        //        // Map the assignment creation DTO to an assignment entity.
        //        var assignment = _mapper.Map<Assignment>(assignmentCreateDto);

        //        // Assign the employee to the laptop.
        //        assignment.AssignEmployee(employee, laptop);

        //        // Add the assignment to the database.
        //        _context.Assignments.Add(assignment);

        //        // Save the changes to the database.
        //        _context.SaveChanges();

        //        // Commit the database transaction.
        //        transaction.Commit();

        //        // Map the assignment entity to an assignment DTO.
        //        var createdAssignmentDto = _mapper.Map<AssignmentDto>(assignment);

        //        // Return the newly created assignment DTO with a 201 Created status code.
        //        return CreatedAtAction(nameof(SearchAssignments), new { searchById = assignment.AssignmentId }, createdAssignmentDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Roll back the database transaction if an exception occurs.
        //        transaction.Rollback();

        //        // Log the exception.
        //        _logger.LogError(ex, "An error occurred while saving the entity changes: {Message}", ex.InnerException?.Message);

        //        // Return a 500 Internal Server Error response with the exception message.
        //        return StatusCode(500, $"Internal server error: {ex.InnerException?.Message}");
        //    }
        //}

        /// <summary>
        /// Returns a laptop by updating the assignment and laptop status.
        /// </summary>
        /// <param name="assignmentId">The ID of the assignment to return.</param>
        /// <returns>The updated assignment details.</returns>
        [HttpPost("ReturnLaptop")]
        public ActionResult<AssignmentDto> ReturnLaptop(int assignmentId)
        {
            try
            {
                // Retrieve the assignment by ID, including the associated laptop.
                var assignment = _context.Assignments
                    .Include(a => a.Laptop)
                    .FirstOrDefault(a => a.AssignmentId == assignmentId);

                // Check if the assignment exists.
                if (assignment == null)
                {
                    // Return a bad request response if the assignment is not found.
                    return BadRequest("Assignment not found.");
                }

                // Check if the laptop has already been returned.
                if (assignment.ReturnDate != null)
                {
                    // Return a bad request response if the laptop has already been returned.
                    return BadRequest(new { message = "This laptop has already been returned." });
                }

                // Log the current status and return date for auditing purposes.
                _logger.LogInformation("Current Laptop Status: {Status}", assignment.Laptop.Status);
                _logger.LogInformation("Current Return Date: {ReturnDate}", assignment.ReturnDate);

                // Update the assignment status to returned.
                assignment.ReturnLaptop();

                // Log the updated status and return date for auditing purposes.
                _logger.LogInformation("Updated Laptop Status: {Status}", assignment.Laptop.Status);
                _logger.LogInformation("Updated Return Date: {ReturnDate}", assignment.ReturnDate);

                // Update the laptop status in the database.
                _context.Entry(assignment.Laptop).State = EntityState.Modified;
                _context.Entry(assignment.Laptop).Property(x => x.LaptopId).IsModified = false; // Exclude LaptopId
                _context.SaveChanges();

                // Return the updated assignment details.
                return Ok(assignment);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the return process.
                _logger.LogError(ex, "An error occurred while returning the laptop: {Message}", ex.InnerException?.Message);
                // Return a server error response with the error message.
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message}");
            }
        }
    }
}