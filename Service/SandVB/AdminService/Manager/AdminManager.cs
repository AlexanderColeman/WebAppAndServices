using AdminService.Data;
using AdminService.Model;
using Microsoft.EntityFrameworkCore;
using ModelSharingService.DTO;

namespace AdminService.Manager
{
    public class AdminManager
    {
        private AdminDbContext _adminDbContext;
        public AdminManager(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }
        public async Task<IEnumerable<User>> getUsers()
        {
            var users = await _adminDbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> createUser(UserDTO userDTO)
        {
            var newUser = new User() 
            { 
                Name = userDTO.Name,
            };

            await _adminDbContext.AddAsync(newUser);
            await _adminDbContext.SaveChangesAsync();

            return newUser;
        }
    }
}
