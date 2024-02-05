using FrontEndService.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;

namespace FrontEndService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceManager _authManager;
        public AuthController(IAuthServiceManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDTO requestDTO)
        {
            try
            {
                var userRequestDTO = await _authManager.Register(requestDTO);
                return Ok(userRequestDTO);
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest("An error occurred during registration.");
            }
        }

    }
}
