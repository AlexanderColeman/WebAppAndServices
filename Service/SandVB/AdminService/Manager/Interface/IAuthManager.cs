using ModelSharingService.DTO;

namespace AdminService.Manager.Interface
{
    public interface IAuthManager
    {
        Task<UserRegistrationRequestDTO> Register(UserRegistrationRequestDTO userRegistrationRequestDTO);
    }
}
