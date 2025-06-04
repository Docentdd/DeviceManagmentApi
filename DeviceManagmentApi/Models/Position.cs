using System.ComponentModel.DataAnnotations;

namespace DeviceManagmentApi.Models
{
    public class Position
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int MinEgYears { get; set; }  // As in: Minimum Experience Years

        public decimal Salary { get; set; }

        // Navigation
        public ICollection<Employee>? Employees { get; set; }
    } 
}