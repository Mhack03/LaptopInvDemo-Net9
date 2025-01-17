using API.Data;
using API.Models;
using API.Models.Dto;
using API.Models.Dtos;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/laptops")]
    [ApiController]
    public class LaptopsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LaptopsController> _logger;
        public LaptopsController(ApplicationDbContext context, ILogger<LaptopsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAllLaptops")]
        public IEnumerable<LaptopDto> GetAllLaptops()
        {
            try
            {
                _logger.LogInformation("Fetching all laptops.");
                var laptops = _context.Laptops
                    .Select(e => new LaptopDto
                    {
                        SerialNumber = e.SerialNumber,
                        LaptopId = e.LaptopId,
                        Brand = (Models.Dto.Brand)e.Brand,
                        Model = e.Model,
                        Processor = e.Processor,
                        Ram = e.Ram,
                        Storage = e.Storage,
                        OperatingSystem = (Models.Dto.OperatingSystem)e.OperatingSystem,
                        Description = e.Description,
                        Status = (Models.Dto.Status)e.Status,
                        HasBag = e.HasBag,
                        HasCharger = e.HasCharger,

                    })
                    .ToList();
                _logger.LogInformation("Fetched {count} laptops.", laptops.Count);
                return laptops;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching laptops.");
                throw;
            }
        }

        [HttpGet("GetSingleLaptops/{serialNumber}", Name = "GetSingleLaptops")]
        public ActionResult<LaptopDto> GetSingleLaptops(string serialNumber)
        {
            try
            {
                _logger.LogInformation("Fetching laptop with serial number {serialNumber}.", serialNumber);
                var laptop = _context.Laptops
                    .Where(e => e.SerialNumber.ToLower() == serialNumber.ToLower())
                    .Select(e => new LaptopDto
                    {
                        SerialNumber = e.SerialNumber,
                        LaptopId = e.LaptopId,
                        Brand = (Models.Dto.Brand)e.Brand,
                        Model = e.Model,
                        Processor = e.Processor,
                        Ram = e.Ram,
                        Storage = e.Storage,
                        OperatingSystem = (Models.Dto.OperatingSystem)e.OperatingSystem,
                        Description = e.Description,
                        Status = (Models.Dto.Status)e.Status,
                        HasBag = e.HasBag,
                        HasCharger = e.HasCharger
                    })
                    .FirstOrDefault();

                if (laptop == null)
                {
                    _logger.LogWarning("Laptop with serial number {serialNumber} not found.", serialNumber);
                    return NotFound();
                }
                _logger.LogInformation("Fetched laptop with serial number {serialNumber}.", serialNumber);
                return Ok(laptop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching laptop with serial number {serialNumber}.", serialNumber);
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }

        [HttpPost("CreateLaptop")]
        public ActionResult<LaptopDto> CreateLaptop([FromBody] LaptopCreateDto laptopCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating laptop.");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid laptop data provided.");
                    return BadRequest(ModelState);
                }

                // Perform case-insensitive comparison without StringComparison
                var existingSerialNumber = _context.Laptops
                    .FirstOrDefault(e => e.SerialNumber.ToLower() == laptopCreateDto.SerialNumber.ToLower());
                if (existingSerialNumber != null)
                {
                    return Conflict(new { message = "Serial number already exists." });
                }

                // Create a new Laptop entity
                var laptop = new Laptop
                {
                    SerialNumber = laptopCreateDto.SerialNumber.ToUpper(),
                    Brand = (Models.Brand)laptopCreateDto.Brand,
                    Model = laptopCreateDto.Model,
                    Processor = laptopCreateDto.Processor,
                    Ram = laptopCreateDto.Ram,
                    Storage = laptopCreateDto.Storage,
                    OperatingSystem = (Models.OperatingSystem)laptopCreateDto.OperatingSystem,
                    Description = laptopCreateDto.Description,
                    Status = (Models.Status)laptopCreateDto.Status,
                    HasBag = laptopCreateDto.HasBag,
                    HasCharger = laptopCreateDto.HasCharger
                };

                _context.Laptops.Add(laptop);
                _context.SaveChanges();

                var laptopDto = new LaptopDto
                {
                    LaptopId = laptop.LaptopId,
                    SerialNumber = laptop.SerialNumber,
                    Brand = (Models.Dto.Brand)laptop.Brand,
                    Model = laptop.Model,
                    Processor = laptop.Processor,
                    Ram = laptop.Ram,
                    Storage = laptop.Storage,
                    OperatingSystem = (Models.Dto.OperatingSystem)laptop.OperatingSystem,
                    Description = laptop.Description,
                    Status = (Models.Dto.Status)laptop.Status,
                    HasBag = laptop.HasBag,
                    HasCharger = laptop.HasCharger
                };
                _logger.LogInformation("Laptop created successfully.");
                return CreatedAtAction(nameof(GetSingleLaptops), new { serialNumber = laptopDto.SerialNumber }, laptopDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating laptop.");
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }

        [HttpPut("UpdateLaptop/{serialNumber}", Name = "UpdateLaptop")]
        public ActionResult UpdateLaptop(string serialNumber, [FromBody] LaptopUpdateDto laptopUpdateDto)
        {
            try
            {
                _logger.LogInformation("Updating laptop with serial number {serialNumber}.", serialNumber);
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid laptop data provided.");
                    return BadRequest(ModelState);
                }

                // Find the laptop by its serial number
                var laptop = _context.Laptops.FirstOrDefault(e => e.SerialNumber.ToLower() == serialNumber.ToLower());
                if (laptop == null)
                {
                    return NotFound(new { message = "Laptop not found." });
                }

                // Check for duplicate serial number, excluding the current laptop
                var existingSerialNumber = _context.Laptops
                    .FirstOrDefault(e => e.SerialNumber.ToLower() == laptopUpdateDto.SerialNumber.ToLower()
                                      && e.SerialNumber != laptop.SerialNumber.ToLower());
                if (existingSerialNumber != null)
                {
                    return Conflict(new { message = "Serial number already exists." });
                }

                // Update the laptop details
                laptop.SerialNumber = laptopUpdateDto.SerialNumber;
                laptop.Brand = (Models.Brand)laptopUpdateDto.Brand;
                laptop.Model = laptopUpdateDto.Model;
                laptop.Processor = laptopUpdateDto.Processor;
                laptop.Ram = laptopUpdateDto.Ram;
                laptop.Storage = laptopUpdateDto.Storage;
                laptop.OperatingSystem = (Models.OperatingSystem)laptopUpdateDto.OperatingSystem;
                laptop.Description = laptopUpdateDto.Description;
                laptop.Status = (Models.Status)laptopUpdateDto.Status;
                laptop.HasBag = laptopUpdateDto.HasBag;
                laptop.HasCharger = laptopUpdateDto.HasCharger;

                // Save changes to the database
                _context.SaveChanges();
                _logger.LogInformation("Laptop updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating laptop with serial number {serialNumber}.", serialNumber);
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }

        [HttpDelete("DeleteLaptop/{serialNumber}", Name = "DeleteLaptop")]
        public ActionResult DeleteLaptop(string serialNumber)
        {
            try
            {
                _logger.LogInformation("Deleting laptop with serial number {serialNumber}.", serialNumber);
                var laptop = _context.Laptops
                    .FirstOrDefault(e => EF.Functions.Collate(e.SerialNumber, "SQL_Latin1_General_CP1_CI_AS") == serialNumber);

                if (laptop == null)
                {
                    return NotFound(new { message = "Laptop not found." });
                }

                _context.Laptops.Remove(laptop);
                _context.SaveChanges();
                _logger.LogInformation("Laptop deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting laptop with serial number {serialNumber}.", serialNumber);
                return ErrorResponseUtility.GenerateErrorResponse(ex, _logger);
            }
        }


    }
}