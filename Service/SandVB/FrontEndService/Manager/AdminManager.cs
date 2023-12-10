using FrontEndService.Manager.Interface;
using FrontEndService.Model;
using FrontEndService.Model.EndpointMap;
using Microsoft.EntityFrameworkCore;
using ModelSharingService.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndService.Manager
{
    public class AdminManager : IAdminManager
    {
        private IHttpManager _httpManager;
        private AdminMap _adminMap;
        public AdminManager(IHttpManager httpManager, AdminMap adminMap)
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
