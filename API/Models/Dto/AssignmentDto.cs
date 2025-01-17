using System;

namespace API.Models.Dto;

public class AssignmentDto
{
    public int AssignmentId { get; set; }
    public string LaptopSerialNumber { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
