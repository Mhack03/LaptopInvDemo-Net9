using System.ComponentModel.DataAnnotations;

namespace API.Models.Dtos
{
    public class EmployeeUpdateDto
    {
        [Required]
        [StringLength(60)]
        [RegularExpression(@"^[A-Z0-9]+$")]
        public string SerialNumber { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;
        [Required]
        public string Position { get; set; } = string.Empty;
    }
}
