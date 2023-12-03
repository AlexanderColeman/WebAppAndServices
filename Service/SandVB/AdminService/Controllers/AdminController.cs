using AdminService.Manager;
using AdminService.Model;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;

namespace AdminService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly AdminManager _adminManager;
        public AdminController(AdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IEnumerable<UserDTO>> getUsers()
        {
            var users = await _adminManager.getUsers();
            return users;
        }

        [HttpPost]
        [Route("User")]
        public async Task<UserDTO> createUser(UserDTO userDTO)
        {
            var user = await _adminManager.createUser(userDTO);

            var newUserDTO = new UserDTO()
            {
                Name = user.Name,
            };

            return newUserDTO;
        }

    }
}
