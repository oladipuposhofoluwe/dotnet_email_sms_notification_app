using NotificationApp.Dto;

namespace NotificationApp.Services.NotificationService
{
    public interface ISendNotification
    {
        Task<NotificationResponse<string>> SendNotification(SendRequestDto sendRequestDto);
    }
}
