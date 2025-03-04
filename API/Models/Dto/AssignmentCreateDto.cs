using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class AssignmentCreateDto
    {
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string LaptopSerialNumber { get; set; } = string.Empty;
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime AssignedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
