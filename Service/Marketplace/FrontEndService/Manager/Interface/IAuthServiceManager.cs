using Microsoft.Win32;
using ModelSharingService.DTO;

namespace FrontEndService.Manager.Interface
{
    public interface IAuthServiceManager
    {
        Task<RegistrationRequestResponseDTO> Register(RegistrationRequestResponseDTO userRegistrationRequestDTO); 
        Task<UserLoginRequestResponseDTO> Login(UserLoginRequestResponseDTO requestDTO);
    }
}
