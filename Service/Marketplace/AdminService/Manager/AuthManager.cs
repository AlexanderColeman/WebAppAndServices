using AdminService.Data;
using AdminService.Manager.Interface;
using AdminService.Messaging.Interface;
using AdminService.Model;
using ModelSharingService.DTO;
using ModelSharingService.Enum;
using ModelSharingService.IntegrationEvents;

namespace AdminService.Manager
{
    public class AuthManager : IAuthManager
    {
        private AdminDbContext _adminDbContext;
        private readonly IEventDispatcher _eventDispatcher;
        public AuthManager(AdminDbContext adminDbContext, IEventDispatcher eventDispatcher)
        {
            _adminDbContext = adminDbContext;
            _eventDispatcher = eventDispatcher;
        }
        public async Task<UserRegistrationRequestDTO> Register(UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            var user = _adminDbContext.Users.FirstOrDefault(u => u.Email.ToUpper() == userRegistrationRequestDTO.Email.ToUpper());

            if (user == null)
            {
                var newUser = new User()
                {
                    Name = userRegistrationRequestDTO.Name,
                    Email = userRegistrationRequestDTO.Email,
                        
                };

                await _adminDbContext.AddAsync(newUser);
                await _adminDbContext.SaveChangesAsync();

                UserEvent userEvent = new UserEvent
                {
                    UserDTO = new UserDTO() { Name = userRegistrationRequestDTO.Name },
                    UserEventType = UserEventTypeEnum.Created
                };
                _eventDispatcher.AddUserEvent(userEvent);
            }
          
            return userRegistrationRequestDTO;
        }
    }
}
