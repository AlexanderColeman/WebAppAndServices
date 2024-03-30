using FrontEndService.Manager;
using FrontEndService.Manager.Interface;
using FrontEndService.Service.Interface;
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
        private readonly ICacheService _cacheService;
        public AdminController(IAdminServiceManager adminManager, ICacheService cacheService)
        {
            _adminManager = adminManager;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> getUsers()
        {
            var users = _cacheService.GetData<IEnumerable<UserDTO>>("users");

            if (users != null && users.Count() > 0)
            {
                return Ok(users);
            }

            users = await _adminManager.getUsers();

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData("users", users, expiryTime);

            return Ok(users);
        }

        [HttpPost]
        [Route("User")]
        public async Task<UserDTO> SaveUpdateUser(UserDTO userDTO)
        {
            var user = await _adminManager.SaveUpdateUser(userDTO);

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData($"users{userDTO.Id}", user, expiryTime);

            return user;
        }

        [HttpDelete]
        [Route("User")]
        public async Task<IActionResult> DeleteUser(UserDTO userDTO)
        {
            var user = _cacheService.GetData<IEnumerable<UserDTO>>("users");

            if (user != null)
            {
               // Send delete method to backend to delete suers 
               // _cacheService.RemoveData($"user{id}")
               // return NoCentent();
            }

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData($"users{userDTO.Id}", user, expiryTime);

            return NotFound();
        }
    }
}
