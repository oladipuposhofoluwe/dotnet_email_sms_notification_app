using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationApp.Models
{
    public class AppUser
	{
		[Key]
        public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        [Required]
		public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        [CompareAttribute("Password", ErrorMessage = "Password doesnt match")]
        public string? ConfrimPassword { get; set; }
        public string Roles { get; set; } = "App User";
	}
}

