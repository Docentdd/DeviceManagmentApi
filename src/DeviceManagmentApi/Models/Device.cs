using System.ComponentModel.DataAnnotations;

namespace DeviceManagmentApi.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public bool IsEnabled { get; set; }

        // Foreign key
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; } = null!;

        // Navigation
        public ICollection<DeviceEmployee>? DeviceEmployees { get; set; }
    }
}