using ModelSharingService.DTO;

namespace FrontEndService.Manager.Interface
{
    public interface IAdminServiceManager
    {
        Task<UserDTO> SaveUpdateUser(UserDTO userDTO);
        Task<IEnumerable<UserDTO>> getUsers();
    }
}
