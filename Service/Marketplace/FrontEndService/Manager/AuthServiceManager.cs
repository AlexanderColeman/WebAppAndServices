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
        public async Task<UserRegistrationRequestDTO> Register(UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            var user = await _httpManager.MakePostAsync<UserRegistrationRequestDTO>(_authMap.BaseUrl, _authMap.Register(), userRegistrationRequestDTO);
            return user;
        }
    }
}
