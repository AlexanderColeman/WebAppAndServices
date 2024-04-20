using AdminService.Model;
using ModelSharingService.DTO;

namespace AdminService.Manager.Interface
{
    public interface IAuthManager
    {
        Task<RegistrationRequestResponse> Register(RegistrationRequestResponseDTO userRegistrationRequestDTO);
        Task<LoginRequestResponse> Login(UserLoginRequestResponseDTO requestDTO);
        string GenerateJwtToken(User user);
    }
}
