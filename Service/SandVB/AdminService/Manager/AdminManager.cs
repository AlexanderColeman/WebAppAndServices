using AdminService.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Manager
{
    public class AdminManager
    {
        public async Task<IEnumerable<User>> getUsers()
        {
            var fakeDrivers = new List<User>();
            fakeDrivers.Add(new User() { Name = "test" });
            return fakeDrivers;
        }
    }
}
