using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotificationApp.Context;
using NotificationApp.Models;

namespace NotificationApp.JWTResponse
{
    public class JwtUtil
    {
        private readonly IConfiguration configuration;
        private readonly AppDbContext dbContext;
        public JwtUtil(IConfiguration config, AppDbContext dbContext){
            this.configuration = config;
            this.dbContext = dbContext;
        }
        public static string GenerateToken(AppUser userData, IConfiguration configuration){
            
            var claims = new[] {
                        new Claim("UserId", userData.Id.ToString()),
                        new Claim("UserName", userData.UserName),
                        new Claim("UserEmail",  userData.Email),
                        new Claim("UserRoles", userData.Roles)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(5),
                        signingCredentials: signIn);
        
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static bool isPasswordAndConfirmPasswordTheSame(string password, string confrimPassword){
            return password.Equals(confrimPassword);
        }
        public static async Task<bool> isUserNameOrEmailAlreadyExist(string username, string email, AppDbContext dbContext){
            if(await dbContext.User.AnyAsync(x=>x.UserName.Trim() == username.Trim())){
                return true;
            } 
                return await dbContext.User.AnyAsync(x=>x.Email.Trim() == email.Trim());
        }
        public static bool isValidPassword(string inputedPassword, string storedPassword){
            return BCrypt.Net.BCrypt.EnhancedVerify(inputedPassword, storedPassword);
        }
    }
}