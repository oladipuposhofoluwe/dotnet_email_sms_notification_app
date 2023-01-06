using NotificationApp.Dto;
using NotificationApp.Response;
using NotificationApp.Services.NotificationService;

namespace NotificationApp.Controller
{
    public interface IAuthentication
    {
        Task<NotificationResponse<AuthenticationResponse>> CreateUser(CreateUserDto userDto);
        Task<NotificationResponse<AuthenticationResponse>> Login(LoginRequest requestDto);

    }
}