using AdminService.Config;
using AdminService.Data;
using AdminService.Manager.Interface;
using AdminService.Messaging.Interface;
using AdminService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelSharingService.DTO;
using ModelSharingService.Enum;
using ModelSharingService.IntegrationEvents;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AdminService.Manager
{
    public class AuthManager : IAuthManager
    {
        private AdminDbContext _adminDbContext;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        public AuthManager(AdminDbContext adminDbContext, IEventDispatcher eventDispatcher, UserManager<User> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _adminDbContext = adminDbContext;
            _eventDispatcher = eventDispatcher;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        public async Task<RegistrationRequestResponse> Register(RegistrationRequestResponseDTO userRegistrationRequestDTO)
        {
            var user = _adminDbContext.Users.FirstOrDefault(u => u.Email.ToUpper() == userRegistrationRequestDTO.Email.ToUpper());

            if (user == null)
            {
                var newUser = new User()
                {
                    Name = userRegistrationRequestDTO.Name,
                    Email = userRegistrationRequestDTO.Email,
                    UserName = userRegistrationRequestDTO.Email
                };

                var isCreated = await _userManager.CreateAsync(newUser, userRegistrationRequestDTO.password);

                if (isCreated.Succeeded)
                {
                    // Generate token
                    return new RegistrationRequestResponse()
                    {
                        Result = true,
                        Token = GenerateJwtToken(newUser)
                    };
                } 
                else
                {
                    throw new Exception(string.Join(", ", isCreated.Errors.Select(e => e.Description)));
                }

                //UserEvent userEvent = new UserEvent
                //{
                //    UserDTO = new UserDTO() { Name = userRegistrationRequestDTO.Name },
                //    UserEventType = UserEventTypeEnum.Created
                //};
                //_eventDispatcher.AddUserEvent(userEvent);
            }
          
            return new RegistrationRequestResponse();
        }

        public async Task<LoginRequestResponse> Login(UserLoginRequestResponseDTO requestDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(requestDTO.Email);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, requestDTO.Password);
            if (isPasswordValid)
            {
                return new LoginRequestResponse()
                {
                    Result = true,
                    Token = GenerateJwtToken(existingUser)
                };
            }

            throw new Exception("Password not valid");
        }

        public string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    // config for token
                    // can add roles in here
                    new Claim(type:"Id", value:user.Id),
                    new Claim(type:JwtRegisteredClaimNames.Sub, value:user.Email),
                    new Claim(type:JwtRegisteredClaimNames.Email, value:user.Email),
                    // can store token and unqiue Jti to create a unqiue reference 
                    // helps for when you are trying to refresh token you can check the Jti to be sure its correct token
                    new Claim(type:JwtRegisteredClaimNames.Jti, value:Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
