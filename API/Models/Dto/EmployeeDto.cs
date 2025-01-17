using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        [Required] 
        public string Name { get; set; } = string.Empty; 
        [Required][EmailAddress] 
        public string Email { get; set; } = string.Empty; 
        [Required] 
        public string Department { get; set; } = string.Empty; 
        [Required]
        public string Position { get; set; } = string.Empty;

        
    }
}