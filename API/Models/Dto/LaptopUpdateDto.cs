using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class LaptopUpdateDto
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

        public bool HasBag { get; set; }
        public bool HasMouse { get; set; }
        public bool HasCharger { get; set; }
        public bool HasKeyboard { get; set; }
    }
}