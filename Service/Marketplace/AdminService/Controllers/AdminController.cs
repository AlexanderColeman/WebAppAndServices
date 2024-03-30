using AdminService.Controllers.Filters;
using AdminService.Manager;
using AdminService.Manager.Interface;
using AdminService.Model;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;

namespace AdminService.Controllers
{
    [ServiceFilter(typeof(EventDispatcherFilter))]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminManager _adminManager;
        public AdminController(IAdminManager adminManager)
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
    }
}
