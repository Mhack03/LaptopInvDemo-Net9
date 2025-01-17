using API.Data;
using API.Models;
using API.Models.Dto;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/laptops")]
    [ApiController]
    public class LaptopsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LaptopsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAllLaptops")]
        public IEnumerable<LaptopDto> GetAllLaptops()
        {
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
            return laptops;
        }

        [HttpGet("{serialNumber}", Name = "GetSingleLaptops")]
        public ActionResult<LaptopDto> GetSingleLaptops(string serialNumber)
        {
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
                return NotFound();
            }
            return Ok(laptop);
        }

        [HttpPost]
        public ActionResult<LaptopDto> CreateLaptop([FromBody] LaptopCreateDto laptopCreateDto)
        {
            if (!ModelState.IsValid)
            {
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
                SerialNumber = laptopCreateDto.SerialNumber,
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

            return CreatedAtAction(nameof(GetSingleLaptops), new { serialNumber = laptopDto.SerialNumber }, laptopDto);
        }

        [HttpPut("{serialNumber}", Name = "UpdateLaptop")]
        public IActionResult UpdateLaptop(string serialNumber, [FromBody] LaptopUpdateDto laptopUpdateDto)
        {
            if (!ModelState.IsValid)
            {
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

            return NoContent();
        }


        [HttpDelete("{serialNumber}", Name = "DeleteLaptop")]
        public IActionResult DeleteLaptop(string serialNumber)
        {
            var laptop = _context.Laptops
                .FirstOrDefault(e => EF.Functions.Collate(e.SerialNumber, "SQL_Latin1_General_CP1_CI_AS") == serialNumber);

            if (laptop == null)
            {
                return NotFound(new { message = "Laptop not found." });
            }

            _context.Laptops.Remove(laptop);
            _context.SaveChanges();

            return NoContent();
        }

    }
}