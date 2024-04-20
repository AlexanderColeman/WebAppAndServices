using FrontEndService.Manager.Interface;
using FrontEndService.ViewModel.EndpointMap;
using ModelSharingService.DTO;

namespace FrontEndService.Manager
{
    public class AuthServiceManager : IAuthServiceManager
    {
        private IHttpManager _httpManager;
        private AuthMap _authMap;
        public AuthServiceManager(IHttpManager httpManager, AuthMap authMap)
        {
            _httpManager = httpManager;
            _authMap = authMap;
        }
        public async Task<RegistrationRequestResponseDTO> Register(RegistrationRequestResponseDTO userRegistrationRequestDTO)
        {
            var user = await _httpManager.MakePostAsync<RegistrationRequestResponseDTO>(_authMap.BaseUrl, _authMap.Register(), userRegistrationRequestDTO);
            return user;
        }
        public async Task<UserLoginRequestResponseDTO> Login(UserLoginRequestResponseDTO requestDTO)
        {
            var user = await _httpManager.MakePostAsync<UserLoginRequestResponseDTO>(_authMap.BaseUrl, _authMap.Login(), requestDTO);
            return user;
        }
    }
}
