using NotificationApp.Models;
namespace NotificationApp.Response
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
        public AuthenticationResponse(AppUser appUser, string jwtToken)
        {
            Id = appUser.Id;
            Role = appUser.Roles;
            JwtToken = jwtToken;
            Username = appUser.UserName;
  
        }

        public AuthenticationResponse(AppUser appUser)
        {
            Id = appUser.Id;
            Username = appUser.UserName;
            Role = appUser.Roles;  
        }

    }
}