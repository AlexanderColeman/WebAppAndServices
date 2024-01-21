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
        public async Task<UserRegistrationRequestDTO> Register([FromBody] UserRegistrationRequestDTO requestDTO)
        {
            var userRequestDTO = await _authManager.Register(requestDTO);
            return userRequestDTO;
        }

    }
}
