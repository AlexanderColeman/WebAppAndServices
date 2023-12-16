using AdminService.Data;
using AdminService.Manager.Interface;
using AdminService.Messaging;
using AdminService.Messaging.Interface;
using AdminService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelSharingService.DTO;
using ModelSharingService.Enum;
using ModelSharingService.IntegrationEvents;

namespace AdminService.Manager
{
    public class AdminManager : IAdminManager
    {
        private AdminDbContext _adminDbContext;
        private readonly IEventDispatcher _eventDispatcher;
        public AdminManager(AdminDbContext adminDbContext,IEventDispatcher eventDispatcher)
        {
            _adminDbContext = adminDbContext;
            _eventDispatcher = eventDispatcher;
        }
        public async Task<IEnumerable<UserDTO>> getUsers()
        {
            var users = await _adminDbContext.Users.ToListAsync();
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                UserDTO dto = new UserDTO() { Name = user.Name, Id = user.Id };
                userDTOs.Add(dto);
            }
            return userDTOs;
        }

        public async Task<User> createUser(UserDTO userDTO)
        {
            var newUser = new User() 
            { 
                Name = userDTO.Name,
            };

            await _adminDbContext.AddAsync(newUser);
            await _adminDbContext.SaveChangesAsync();

            UserEvent userEvent = new UserEvent
            {
                UserDTO = userDTO,
                UserEventType = UserEventTypeEnum.Created
            };
            _eventDispatcher.AddUserEvent(userEvent);

            return newUser;
        }
    }
}
