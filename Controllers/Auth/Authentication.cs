using NotificationApp.Context;
using NotificationApp.Dto;
using NotificationApp.JWTResponse;
using NotificationApp.Models;
using NotificationApp.Response;
using NotificationApp.Services.NotificationService;


namespace NotificationApp.Controller
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration configuration;
        private readonly AppDbContext dbContext;
        public Authentication(IConfiguration config, AppDbContext dbContext)
        {
            this.configuration = config;
            this.dbContext = dbContext;
        }
        
        public async Task<NotificationResponse<AuthenticationResponse>> CreateUser(CreateUserDto createUserDto){
        
        var response = new NotificationResponse<AuthenticationResponse>();

        if(createUserDto.Roles != "App User"){
            return ApiResponse.getResponse(response, "Incorrect Role");
        }

        if (!JwtUtil.isPasswordAndConfirmPasswordTheSame(createUserDto.Password, createUserDto.ConfirmPassword)){
            return ApiResponse.getResponse(response, "Confirm password not the same as password");
        }

        if(await JwtUtil.isUserNameOrEmailAlreadyExist(createUserDto.UserName, createUserDto.Email, dbContext)){
            return ApiResponse.getResponse(response, "Username or Email Already Exist");
        }
            
        var appUser = new AppUser
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(createUserDto.Password),
                Roles = createUserDto.Roles  
            };

            await dbContext.User.AddAsync(appUser);
            await dbContext.SaveChangesAsync(true);

            ApiResponse.getResponse(response,appUser, "Account Created, please proceed to sign in");
            
            return response;
        }

        public async Task<NotificationResponse<AuthenticationResponse>> Login(LoginRequest requestDto){
        
        var response = new NotificationResponse<AuthenticationResponse>();
               
        var userData = dbContext.User.FirstOrDefault(x=>x.UserName == requestDto.UserName);
        
        if(userData == null){
          return  ApiResponse.getResponse(response, "Username does not exist, please try again");
        }

        if(!JwtUtil.isValidPassword(requestDto.Password, userData.Password)){
            return ApiResponse.getResponse(response, "Invalid password");
        }
        
        string jwtToken = JwtUtil.GenerateToken(userData, configuration);

        if(jwtToken == null){
          return  ApiResponse.getResponse(response, "Internal Server Error, please try again!!");
        }

            return ApiResponse.getResponse(jwtToken,userData,response, "Login Successful");
        }
    }
}