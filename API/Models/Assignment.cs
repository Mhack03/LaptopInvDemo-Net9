using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        [Required] 
        public string LaptopSerialNumber { get; set; } = string.Empty;
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime AssignedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Laptop? Laptop { get; set; }
        public Employee? Employee { get; set; }
    }
}
