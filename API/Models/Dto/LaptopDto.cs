using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public enum OperatingSystem
    {
        Windows,
        macOS,
        Linux,
        ChromeOS
    }

    public enum Status
    {
        Available,
        InUse,
        UnderMaintenance,
        Damaged
    }

    public enum Brand
    {
        Acer,
        Apple,
        Asus,
        Dell,
        HP,
        Lenovo,
        Microsoft,
        Razer,
        Samsung,
        Other
    }

    public class LaptopDto
    {
        public int LaptopId { get; set; }
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