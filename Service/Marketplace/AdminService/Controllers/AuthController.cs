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
        public async Task<UserRegistrationRequestDTO> Register([FromBody] UserRegistrationRequestDTO requestDTO)
        {
            var userRequestDTO = await _authManager.Register(requestDTO);
            return userRequestDTO;
        }
    }
}
