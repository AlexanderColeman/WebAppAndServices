using FrontEndService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndService.Manager
{
    public class AdminManager
    {

        private readonly HttpClient _httpClient;
        public AdminManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> getUsers()
        {
            var apiUrl = "http://admin-service/Admin/User";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadAsStringAsync();

            return new List<User>();
        }

        public async Task<string> PostUserData(User userData)
        {
            try
            {
                var apiUrl = "http://admin-service/Admin/ReceiveUserData"; // Corrected endpoint

                var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userData), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, jsonContent);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

    }
}
