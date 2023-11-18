using FrontEndService.Manager;
using FrontEndService.Model;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {

        private readonly AdminManager _adminManager;
        public AdminController(AdminManager adminManger)
        {
            _adminManager = adminManger;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IEnumerable<User>> getUsers()
        {
            var users = await _adminManager.getUsers();
            return users;
        }
    }
}
