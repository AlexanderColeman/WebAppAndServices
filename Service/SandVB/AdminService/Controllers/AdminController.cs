using AdminService.Manager;
using AdminService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        [Route("User")]
        public async Task<IEnumerable<User>> getUsers()
        {
            var users = await _adminManager.getUsers();
            return users;
        }

        [HttpPost]
        [Route("ReceiveUserData")]
        public async Task<IActionResult> ReceiveUserData([FromBody] User userData)
        {
            try
            {
                // Your logic to process the received user data
                // For example, you can save it to a database or perform other operations
                var result = userData;

                return Ok(result); // Customize the response based on your needs
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
