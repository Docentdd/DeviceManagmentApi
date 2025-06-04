using System.ComponentModel.DataAnnotations;

namespace DeviceManagmentApi.Models
{
    public class DeviceType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        // Navigation
        public ICollection<Device>? Devices { get; set; }
    }
}