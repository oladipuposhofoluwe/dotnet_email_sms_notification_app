using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationApp.Dto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        // [NotMapped]
        // [CompareAttribute("Password", ErrorMessage = "Password doesnt match")]
        public string ConfirmPassword { get; set; }
        public string Roles { get; set; } = "App User";
    
    }
}