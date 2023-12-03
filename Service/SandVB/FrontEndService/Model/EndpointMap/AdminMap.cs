namespace FrontEndService.Model.EndpointMap
{
    public class AdminMap
    {
        private IConfiguration _configuration;
        public AdminMap(IConfiguration configuration)
        {
            _configuration = configuration;
            Admin = new AdminAdminMap(_configuration);
        }

        public string BaseUrl { get => _configuration["Admin:BaseUrl"]; }
        public AdminAdminMap Admin { get; }

        public class AdminAdminMap
        {
            private IConfiguration _configuration;
            public AdminAdminMap(IConfiguration configuration) 
            {
                _configuration = configuration;
            }

            public string SaveUpdateUser()
            {
                var url = _configuration["Admin:Admin:SaveUpdateUser"];
                return url;
            }

            public string GetUser()
            {
                var url = _configuration["Admin:Admin:GetUser"];
                return url;
            }
        }
    }
}
