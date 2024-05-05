using FrontEndService.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Register([FromBody] RegistrationRequestResponseDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var userRequestDTO = await _authManager.Register(requestDTO);
                return Ok(userRequestDTO);
            }
            return BadRequest("Invalid request payload");
        }

        [HttpPost]
        [Route("Login")]
        [Authorize]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestResponseDTO requestDTO)
        {
            var headers = HttpContext.Request.Headers;
            if (ModelState.IsValid)
            {
                var userRequestDTO = await _authManager.Login(requestDTO);
                return Ok(userRequestDTO);
            }
            return BadRequest("Invalid request payload");
        }

    }
}
