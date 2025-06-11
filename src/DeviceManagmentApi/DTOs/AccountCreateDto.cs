using System.ComponentModel.DataAnnotations;

namespace DeviceManagmentApi.DTOs
{
    public class AccountCreateDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
       
        public string Login { get; set; } = null!;
        
        public int TypeId { get; set; }
        
        public object EmployeeId { get; set; }
        
        [Required]
        public int RoleId { get; set; }
    }
}