using FrontEndService.Manager;
using FrontEndService.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEndService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServiceManager _adminManager;
        public AdminController(IAdminServiceManager adminManager)
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
        public async Task<UserDTO> SaveUpdateUser(UserDTO userDTO)
        {
            var user = await _adminManager.SaveUpdateUser(userDTO);
            return user;
        }
    }
}
