using FrontEndService.Manager.Interface;
using FrontEndService.ViewModel.EndpointMap;
using Microsoft.EntityFrameworkCore;
using ModelSharingService.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndService.Manager
{
    public class AdminServiceManager : IAdminServiceManager
    {
        private IHttpManager _httpManager;
        private AdminMap _adminMap;
        public AdminServiceManager(IHttpManager httpManager, AdminMap adminMap)
        {
            _httpManager = httpManager;
            _adminMap = adminMap;
        }

        public async Task<IEnumerable<UserDTO>> getUsers()
        {
            var users = await _httpManager.MakeGetAsync<IEnumerable<UserDTO>>(_adminMap.BaseUrl, _adminMap.GetUser());

            return  users;
        }

        public async Task<UserDTO> SaveUpdateUser(UserDTO userDTO)
        {
            var user = await _httpManager.MakePostAsync<UserDTO>(_adminMap.BaseUrl, _adminMap.SaveUpdateUser(), userDTO);
            return user;
        }

    }
}
