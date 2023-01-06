using Microsoft.AspNetCore.Mvc;
using NotificationApp.Dto;

namespace NotificationApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        public AuthController(IAuthentication authentication){
            _authentication = authentication;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            if(ModelState.IsValid){
                var response = await _authentication.CreateUser(userDto);
                if(!response.Success){
                    return BadRequest(response);
                }
                return Ok(response);
            }

            return BadRequest(ModelState);
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if(ModelState.IsValid){
                var response = await _authentication.Login(request);
                 if(!response.Success){
                  return BadRequest(response);
              }
               return Ok(response);
            }
            return BadRequest(ModelState); 
        }
    }
}