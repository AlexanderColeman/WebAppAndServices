using AdminService.Model;
using ModelSharingService.DTO;

namespace AdminService.Manager.Interface
{
    public interface IAdminManager
    {
        Task<IEnumerable<UserDTO>> getUsers();
        Task<User> createUser(UserDTO userDTO);
    }
}
