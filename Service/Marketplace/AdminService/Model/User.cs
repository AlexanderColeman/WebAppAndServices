using Microsoft.AspNetCore.Identity;

namespace AdminService.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
