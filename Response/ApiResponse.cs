using NotificationApp.Dto;
using NotificationApp.Models;
using NotificationApp.Services.NotificationService;

namespace NotificationApp.Response
{
    public class ApiResponse
    {
        public static NotificationResponse<AuthenticationResponse> getResponse(string jwtToken, AppUser userData, NotificationResponse<AuthenticationResponse> response, string message)
        {
            response.Message=message;
            response.Data = new AuthenticationResponse(userData, jwtToken);
            response.Success = true; 
            return response;       
        }

        public static NotificationResponse<AuthenticationResponse> getResponse(NotificationResponse<AuthenticationResponse> response, string message)
        {
            response.Success = false;
            response.Message = message;
            return response;
        }
        public static void getResponse(NotificationResponse<AuthenticationResponse> response,AppUser appUser,  string message)
        {
            response.Success = true;
            response.Message = message;
            response.Data = new AuthenticationResponse(appUser);        
        }

        public static void getResponse(NotificationResponse<string> response,string message,string toNumber)
        {
            response.Success = true;
            response.Message = String.Format(message,toNumber);;
            response.Data = toNumber;
        }
        public static void getResponse(NotificationResponse<string> response,string message,SendRequestDto request)
        {
            response.Success = true;
            response.Message = String.Format(message,request.ToEmail);
            response.Data = String.Format("Email Subject: {0} ", request.EmailSubject);
        }
        
    }
}