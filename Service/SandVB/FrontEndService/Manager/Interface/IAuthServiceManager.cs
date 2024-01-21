using ModelSharingService.DTO;

namespace FrontEndService.Manager.Interface
{
    public interface IAuthServiceManager
    {
        Task<UserRegistrationRequestDTO> Register(UserRegistrationRequestDTO userRegistrationRequestDTO);
    }
}
