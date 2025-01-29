using System;

namespace API.Models.Dto;

public class AssignmentDto
{
    public int AssignmentId { get; set; }
    public string LaptopSerialNumber { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public required string EmployeeName { get; set; }
    public string Department { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
