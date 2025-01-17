using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto;

public class AssignmentUpdateDto
{
    [Required]
    public string LaptopSerialNumber { get; set; } = string.Empty;
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public DateTime? ReturnDate { get; set; }
}