using ModelSharingService.DTO;

namespace FrontEndService.Manager.Interface
{
    public interface IAdminManager
    {
        Task<UserDTO> SaveUpdateUser(UserDTO userDTO);
        Task<IEnumerable<UserDTO>> getUsers();
    }
}
