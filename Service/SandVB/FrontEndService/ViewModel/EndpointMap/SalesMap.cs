namespace FrontEndService.ViewModel.EndpointMap
{
    public class SalesMap
    {
        private IConfiguration _configuration;
        public SalesMap(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrl { get => _configuration["Sales:BaseUrl"]; }

        public string saveUpdateSale()
        {
            var url = _configuration["Sales:Sales:SaveUpdateSale"];
            return url;
        }
    }
}
