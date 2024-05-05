using AdminService.Controllers.Filters;
using AdminService.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;

namespace AdminService.Controllers
{
    [ServiceFilter(typeof(EventDispatcherFilter))]
    [ApiController]
    [Route("[controller]")]
    public class AuthController
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<RegistrationRequestResponseDTO> Register([FromBody] RegistrationRequestResponseDTO requestDTO)
        {
            var userRequest = await _authManager.Register(requestDTO);
            var userRequestDTO = new RegistrationRequestResponseDTO()
            {
                Result = userRequest.Result,
                Token = userRequest.Token
            };
            return userRequestDTO;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<UserLoginRequestResponseDTO> Login([FromBody] UserLoginRequestResponseDTO requestDTO)
        {
            var userRequest = await _authManager.Login(requestDTO);
            var userRequestDTO = new UserLoginRequestResponseDTO()
            {
                Token = userRequest.Token,
                Result = userRequest.Result
            };
            return userRequestDTO;
        }
    }
}
