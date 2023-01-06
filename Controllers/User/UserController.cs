using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApp.Dto;
using NotificationApp.Services.NotificationService;

namespace NotificationApp.Controller
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISendNotification inotificationService;
        public UserController(ISendNotification inotificationService)
        {
            this.inotificationService = inotificationService;
        }

        [HttpPost("sendNotification")]
        public async Task<IActionResult> SendNotification(SendRequestDto requestDto)
        {
            var response = await inotificationService.SendNotification(requestDto);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }  
    }
}