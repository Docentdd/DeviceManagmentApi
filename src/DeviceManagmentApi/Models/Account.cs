using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeviceManagmentApi.Models;

namespace DeviceManagementApi.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[^\d]\w*$", ErrorMessage = "Username should not start with a number")]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters")]
        // We'll validate password complexity in code or using a custom validator
        public string Password { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        // Foreign key to Role
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
        public string Login { get; set; } = null!;
    }
}