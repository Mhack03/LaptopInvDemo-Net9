using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public enum OperatingSystem
    {
        Windows_10,
        Windows_11,
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

    public class Laptop
    {
        [Key]
        [Required]
        [StringLength(60)]
        public string SerialNumber { get; set; } = string.Empty;
        public int LaptopId { get; set; }

        [Required]
        public Brand Brand { get; set; }

        [Required][StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required][StringLength(50)]
        public string Processor { get; set; } = string.Empty;

        [Required][StringLength(30)]
        public string Ram { get; set; } = string.Empty;

        [Required][StringLength(50)]
        public string Storage { get; set; } = string.Empty;

        [Required]
        public OperatingSystem OperatingSystem { get; set; }

        public string? Description { get; set; }

        [Required]
        public Status Status { get; set; } 
        public bool HasBag { get; set; } = false;
        public bool HasKeyboard { get; set; } = false;
        public bool HasMouse { get; set; } = false;
        public bool HasCharger { get; set; } = false;
    }
}
