namespace FrontEndService.ViewModel.EndpointMap
{
    public class AuthMap
    {
        private IConfiguration _configuration;
        public AuthMap(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrl { get => _configuration["Admin:BaseUrl"]; }
        public string Register()
        {
            var url = _configuration["Admin:Auth:Register"];
            return url;
        }
    }
}
