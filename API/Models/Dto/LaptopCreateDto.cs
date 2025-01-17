using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class LaptopCreateDto
    {
        [Required]
        [StringLength(60)]
        [RegularExpression(@"^[A-Z0-9]+$")]
        public string SerialNumber { get; set; } = string.Empty;
        [Required]
        public Brand Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Processor { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Ram { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Storage { get; set; } = string.Empty;

        [Required]
        public OperatingSystem OperatingSystem { get; set; }

        public string? Description { get; set; }

        [Required]
        public Status Status { get; set; }

        public bool HasBag { get; set; } = false;
        public bool HasMouse { get; set; } = false;
        public bool HasCharger { get; set; } = false;
        public bool HasKeyboard { get; set; } = false;
    }
}