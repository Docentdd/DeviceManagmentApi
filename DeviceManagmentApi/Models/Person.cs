using System.ComponentModel.DataAnnotations;

namespace DeviceManagmentApi.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Picture { get; set; }

        [Required]
        [MaxLength(20)]
        public string PassportNumber { get; set; } = null!;
    }  
}